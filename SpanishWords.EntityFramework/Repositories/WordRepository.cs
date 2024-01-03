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

        public Statistic CreateAndAddStatistic()
        {
            DateTime dateTimeNow = new DateTime();
            dateTimeNow = DateTime.Now;
            Statistic statistic = new Statistic() { CreateDate = dateTimeNow, LastUpdated = dateTimeNow, TimesCorrect = 0, TimesIncorrect = 0};
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
            if (_db.Words == null || _db.Words.Count() < 1)
            {
                _logger.LogError(ExceptionHelper.EMPTY_VARIABLE);
                return new Word();
            }
            else return (_db.Words.ToList())[RandomNumberGenerator.GetInt32(_db.Words.Count())];
        }


        public bool SaveStats(Word word, bool isCorrect)
        {
            Word wordStat = _db.Words.Where(n => n.Id == word.Id).First();
            if (wordStat == null) return false;

            if (isCorrect == true) wordStat.Statistic.TimesCorrect++;
            else wordStat.Statistic.TimesIncorrect++;

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
                result = _db.Words.Include(a => a.GrammaticalGender)
                    .Where(a => a.UserId == userId)
                    .Where(a => a.Statistic.TimesCorrect <= timesCorrect)
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
        public bool RestartProgressForAll()
        {
            try
            {
                List<Word> words = _db.Words.Include(n => n.Statistic).ToList();
                for (int i = 0; i < words.Count(); i++)
                {
                    words[i].Statistic.TimesCorrect = 0;
                    words[i].Statistic.TimesIncorrect = 0;
                    _db.Update(words[i]);
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
            try
            {
                Word word = _db.Words.Include(n => n.Statistic).Where(n => n.Id == id).FirstOrDefault();
                word.Statistic.TimesCorrect = 0;
                word.Statistic.TimesIncorrect = 0;
                _db.Update(word);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(ExceptionHelper.EF_QUERY_ERROR + ExceptionHelper.GetErrorMessage(e.Message));
                return false;
            }
        }
    }
}
