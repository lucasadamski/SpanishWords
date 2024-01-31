using EFDataAccess.DataAccess;
using EFDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpanishWords.EntityFramework.Helpers;
using SpanishWords.EntityFramework.Repositories.Infrastructure;
using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.EntityFramework.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly WordsContext _db;
        private readonly ILogger<WordRepository> _logger;

        public StatsRepository(WordsContext db, ILogger<WordRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Word> GetAllNotLearntWords(string userId, int timesCorrect)
        {
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return new List<Word>();
            }

            return result;
        }
        public IEnumerable<Word> GetAllLearntWords(string userId, int timesCorrect)
        {
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
                    .Where(a => idsOfWordsAlreadyLearnt.Contains(a.StatisticId)) //Include those Ids that are learnt
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
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                return 0;
            }
        }
        public List<StudyEntry> GetAllStudyEntries(string userId)
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
        public IEnumerable<IGrouping<int, StudyEntry>> GetGroupsOfLearntStudyEntries
           (string userId, int correctNumberForLearning)
        {
            IEnumerable<IGrouping<int, StudyEntry>> result;

            try
            {
                result = GetAllStudyEntries(userId)
                    .Where(n => n.Correct == true)
                    .GroupBy(n => n.Statistic.Id)
                    .Where(n => n.Count() >= correctNumberForLearning);
            }
            catch (Exception e)
            {
                _logger.LogError(DBExceptionHelper.EF_QUERY_ERROR + DBExceptionHelper.GetErrorMessage(e.Message));
                result = new List<IGrouping<int, StudyEntry>>();
            }

            return result;
        }

    }
}
