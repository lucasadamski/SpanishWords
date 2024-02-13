using EFDataAccess.DataAccess;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using SpanishWords.EntityFramework.Helpers;
using AutoMapper;
using SpanishWords.Models.DTOs;
using SpanishWords.Models.Tables;
using SpanishWords.Models.Views;

namespace EFDataAccess.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly WordsContext _db;
        private readonly ILogger<WordRepository> _logger;
        private readonly IMapper _mapper;

        public WordRepository(WordsContext db, ILogger<WordRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public CreateStatisticDTO CreateStatistic(int numberOfAnswersToLearnTheWord) // statdto + bool
        {
            CreateStatisticDTO result = new CreateStatisticDTO();

            DateTime dateTimeNow = new DateTime();
            dateTimeNow = DateTime.Now;
            Statistic statistic = new Statistic()
            {
                CreateDate = dateTimeNow,
                LastUpdated = dateTimeNow,
                CorrectAnswersToLearn = numberOfAnswersToLearnTheWord
            };

            try
            {
                _db.Statistics.Add(statistic);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.DATABASE_WRITE_ERROR);
                return new CreateStatisticDTO() { Statistic = new Statistic(), Success = false };
            }
            return new CreateStatisticDTO() { Statistic = statistic, Success = true};
        }
        public IEnumerable<Word> GetAllWords(string userId)
        {         
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<Word>();
            }

            return result;
        }
        public IEnumerable<v_Words_Stats> GetAllWordsWithStatsFromView(string userId)
        {
            try
            {
                return _db.vWordsStats.Where(n => n.UserId == userId).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<v_Words_Stats>();
            }
        }
        public bool Add(Word? word)
        {
            try
            {
                _db.Words.Add(word);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.DATABASE_WRITE_ERROR);
                return false;
            }
            return true;
        }
        public bool Edit(Word? word)
        {
            try
            {
                _db.Words.Update(word);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.DATABASE_WRITE_ERROR);
                return false;
            }
            return true;
        }
        public bool Delete(Word? word)
        {
            try
            {
                _db.Words.Remove(word);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.DATABASE_WRITE_ERROR);
                return false;
            }
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new Word();
            }
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
            }
            catch(Exception e)
            {
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return false;
            }
            return true;
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
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return false;
            }
            return true;
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return 0;
            }
        }
        public List<StudyEntry> GetStudyEntries(int wordId)
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<StudyEntry>();
            }
        }
        public List<WordDTO> GetWordDTOsByWordText(string word, bool isEnglish)
        {
            List<WordDTO> result;
            List<Word> words;
            try
            {
                words = _db.Words
                    .Include(n => n.Statistic)
                    .Where(n => isEnglish ? n.English == word : n.Spanish == word)
                    .Where(n => n.Statistic.DeleteTime == null)
                    .ToList();

                result = _mapper.Map <List<WordDTO>>(words);
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<WordDTO>();
            }
            return result;
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<StudyEntry>();
            }
        }
    }
}