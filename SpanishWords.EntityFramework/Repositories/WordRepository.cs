using EFDataAccess.DataAccess;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Word> GetAllWords() => _db.Words.Include(a => a.GrammaticalGender)
                                             .Include(a => a.LexicalCategory)
                                             .Include(a => a.User)
                                             .Include(a => a.Statistic)
                                             .ToList();

        

        public bool Add(Word word)
        {
            _db.Words.Add(word);
            _db.SaveChanges();
            return true;
        }
        public bool Edit(Word word)
        {
            _db.Words.Update(word);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Word word)
        {
            _db.Words.Remove(word);
            _db.SaveChanges();
            return true;
        }

        public int GetLastStaticticId()
        {
            return _db.Words.OrderBy(a => a.StatisticId).LastOrDefault().StatisticId;
        }

        public Word GetWordById(int id)
        {
               return _db.Words.Where(a => a.Id == id).FirstOrDefault();
        }

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
