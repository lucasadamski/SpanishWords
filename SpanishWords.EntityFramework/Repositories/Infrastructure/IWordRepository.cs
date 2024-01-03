using EFDataAccess.Repositories;
using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace EFDataAccess.Repositories.Infrastructure
{
    public interface IWordRepository
    {
        bool Add(Word word);

        Word GetWordById(int id);

        public IEnumerable<Word> GetAllWords(string userId);
        public IEnumerable<Word> GetAllNotLearntWords(string userId, int timesCorrect);

        bool Edit(Word word);
        bool Delete(Word word);

        Statistic CreateAndAddStatistic();

        public IEnumerable<GrammaticalGender> GetGrammaticalGenders();

        public IEnumerable<LexicalCategory> GetLexicalCategories();

        public Word GetRandomWord();

        public bool SaveStats(Word word, bool isCorrect);
        public bool RestartProgressForAll();
        public bool RestartProgress(int id);

    }
}
