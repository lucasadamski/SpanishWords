using EFDataAccess.DataAccess;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Repositories
{

    public class WordRepository : IWordRepository
    {
        private readonly WordsContext _db;

        public WordRepository(WordsContext db)
        {
            this._db = db;
        }

        public Statistic CreateAndAddStatistic()
        {
            DateTime dateTimeNow = new DateTime();
            dateTimeNow = DateTime.Now;
            Statistic statistic = new Statistic() { CreateDate = dateTimeNow, LastUpdated = dateTimeNow, TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 };
            _db.Statistics.Add(statistic);
            _db.SaveChanges();
            return _db.Statistics.Where(a => a == statistic).FirstOrDefault();
        }

        public IEnumerable<Word> GetAllWords(string userId)
        {
            if (userId == null) throw new ArgumentNullException("userId");

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
                e.Data.Add("DebugMessage", "Error occured in GetAllWords method in WordRepository.cs");
                throw;
            }

            return result;
            
        }

        

        public bool Add(Word? word)
        {
            if (word == null) return false;
            _db.Words.Add(word);
            _db.SaveChanges();
            return true;
        }
        public bool Edit(Word? word)
        {
            if (word == null) return false;
            _db.Words.Update(word);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Word? word)
        {
            if (word == null) return false;
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
                return new List<LexicalCategory>();
            }
        }


    }
}
