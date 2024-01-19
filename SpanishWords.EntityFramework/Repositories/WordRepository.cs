using EFDataAccess.DataAccess;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SpanishWords.EntityFramework.Helpers;

namespace EFDataAccess.Repositories
{

    public class WordRepository : IWordRepository
    {
        private readonly WordsContext _db;
        private readonly ILogger<WordRepository> _logger;

        public WordRepository(WordsContext db, ILogger<WordRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Statistic CreateAndAddStatistic(int numberOfAnswersToLearnTheWord)
        {
            DateTime dateTimeNow = new DateTime();
            dateTimeNow = DateTime.Now;
            Statistic statistic = new Statistic() { CreateDate = dateTimeNow, LastUpdated = dateTimeNow, CorrectAnswersToLearn = numberOfAnswersToLearnTheWord };
            _db.Statistics.Add(statistic);
            _db.SaveChanges();
            return statistic;
        }

        public IEnumerable<Word> GetAllWords(string userId)
        {
            if (userId == null)
            {
                _logger.LogError(ExceptionHelper.EMPTY_VARIABLE);
                return new List<Word>();
            }

            IEnumerable<Word> result;

            try
            {
                result = _db.Words.Include(a => a.GrammaticalGender) 
                    .Where(a => a.UserId == userId)
                    .Include(a => a.LexicalCategory)
                    .Include(a => a.Statistic)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<Word>();
            }

            return result;
        }

        public IEnumerable<Word> GetAllWords()
        {
            IEnumerable<Word> result;

            try
            {
                result = _db.Words.Include(a => a.GrammaticalGender)
                    .Include(a => a.LexicalCategory)
                    .Include(a => a.Statistic)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<Word>();
            }

            return result;
        }



        public bool Add(Word? word)
        {
            _db.Words.Add(word);
            _db.SaveChanges();
            return true;
        }
        public bool Edit(Word? word)
        {
            _db.Words.Update(word);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Word? word)
        {
            _db.Words.Remove(word);
            _db.SaveChanges();
            return true;
        }


        public Word? GetWordById(int id) => _db.Words.Where(a => a.Id == id).FirstOrDefault();
        
        
        public IEnumerable<GrammaticalGender> GetGrammaticalGenders()
        {
            try
            {
                return _db.GrammaticalGenders.ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<GrammaticalGender>();
            }
        }

        public IEnumerable<LexicalCategory> GetLexicalCategories()
        {
            try
            {
                return _db.LexicalCategories.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<LexicalCategory>();
            }
        }

        public Word GetRandomWord()
        {
            try
            {
                return (_db.Words.ToList())[RandomNumberGenerator.GetInt32(_db.Words.Count())];
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new Word();
            }
        }


        public bool SaveStats(Word word, bool isCorrect)
        {
            Word wordStat = _db.Words.Include(n => n.Statistic.StudyEntry).Where(n => n.Id == word.Id).First();
            if (wordStat == null) return false;

            StudyEntry studyEntry = new StudyEntry
            {
                Correct = isCorrect,
                Date = DateTime.Now,
                Statistic = wordStat.Statistic,
                AnswerType = _db.AnswerTypes.Where(n => n.Id == 1).First(), //not implemented
                HelperType = _db.HelperTypes.Where(n => n.Id == 1).First()  //not implemented
            };

            _db.Add(studyEntry);
            _db.Update(wordStat);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Word> GetAllNotLearntWords(string userId, int timesCorrect)
        {
            if (userId == null)
            {
                _logger.LogError(ExceptionHelper.EMPTY_VARIABLE);
                return new List<Word>();
            }

            IEnumerable<Word> result;

            try
            {
                //Creates list of IDs only that are learnt
                var idsOfWordsAlreadyLearnt = _db.StudyEntries
                    .Where(n => n.Correct == true)
                    .GroupBy(n => n.Statistic.Id)
                    .Select(group => new
                    {
                        StatisticId = group.Key,
                        NoOfEntries = group.Count()
                    })
                    .Where(n => n.NoOfEntries >= timesCorrect)  //include only those ids that appear more than amount required for learning a word
                    .Select(n => n.StatisticId)
                    .ToList();

                //Create list of Word types, that are not learnt
                result = _db.Words.Include(a => a.GrammaticalGender)
                    .Where(a => a.UserId == userId)
                    .Where(a => !idsOfWordsAlreadyLearnt.Contains(a.StatisticId)) //Exclude those Ids that are learnt
                    .Include(a => a.LexicalCategory)
                    .Include(a => a.Statistic)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<Word>();
            }

            return result;
        }
        public bool RestartProgressForAll(string userId)
        {
            List<StudyEntry> studyEntriesToRemove = GetAllStudyEntries(userId);
            try
            {
                foreach (var entry in studyEntriesToRemove)
                {
                    _db.Remove(entry);
                }
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return false;
            }
        }
        public bool RestartProgress(int id)
        {
            List<StudyEntry> studyEntriesToRemove = GetStudyEntries(id);
            try
            {
                foreach (var entry in studyEntriesToRemove)
                {
                    _db.Remove(entry);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return false;
            }
        }

        public int GetWordsTimesCorrect(int id)
        {
            try
            {
                int statisticId = _db.Words.Where(n => n.Id == id).Select(n => n.StatisticId).First();
                int result = _db.StudyEntries
                    .Where(n => n.Statistic.Id == statisticId)
                    .Where(n => n.Correct == true)
                    .Count();
                return result;
            }
            catch(Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return 0;
            }
        }
        public int GetWordsTimesIncorrect(int id)
        {
            try
            {
                int statisticId = _db.Words.Where(n => n.Id == id).Select(n => n.StatisticId).First();
                int result = _db.StudyEntries
                    .Where(n => n.Statistic.Id == statisticId)
                    .Where(n => n.Correct == false)
                    .Count();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return 0;
            }
        }
        public int GetWordsTotalTrainedTimes(int id)
        {
            try
            {
                int statisticId = _db.Words.Where(n => n.Id == id).Select(n => n.StatisticId).First();
                int result = _db.StudyEntries
                    .Where(n => n.Statistic.Id == statisticId)
                    .Count();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return 0;
            }
        }

        private List<StudyEntry> GetStudyEntries(int wordId)
        {
            try
            {
                int statisticId = _db.Words.Where(n => n.Id == wordId).Select(n => n.StatisticId).First();
                List<int> studyEntryIds = _db.StudyEntries.Where(n => n.Statistic.Id == statisticId).Select(n => n.Id).ToList();
                List<StudyEntry> studyEntriesToRemove = _db.StudyEntries.Where(n => studyEntryIds.Contains(n.Id)).ToList();
                return studyEntriesToRemove;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<StudyEntry>();
            }
        }
        private List<StudyEntry> GetAllStudyEntries(string userId)
        {
            try
            {
                List<int> statisticIds = _db.Words.Where(n => n.UserId == userId).Select(n => n.StatisticId).ToList();
                List<int> studyEntryIds = _db.StudyEntries.Where(n => statisticIds.Contains(n.Statistic.Id)).Select(n => n.Id).ToList();
                List<StudyEntry> studyEntriesToRemove = _db.StudyEntries.Where(n => studyEntryIds.Contains(n.Id)).ToList();
                return studyEntriesToRemove;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return new List<StudyEntry>();
            }
        }
    }
}
