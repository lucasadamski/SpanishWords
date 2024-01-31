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
        bool Add(Word word); //w, a -> w
        Word GetWordById(int id); //w
        public IEnumerable<Word> GetAllWords(string userId); // s, w  -> w
        public IEnumerable<Word> GetAllWords(); //s, w -> w
        //public IEnumerable<Word> GetAllNotLearntWords(string userId, int timesCorrect);//study, s -> s//
        //public IEnumerable<Word> GetAllLearntWords(string userId, int timesCorrect);//study, s -> s//
        bool Edit(Word word); //w
        bool Delete(Word word); //w
        Statistic CreateAndAddStatistic(int numberOfAnswersToLearnTheWord); //w, a -> w
        public IEnumerable<GrammaticalGender> GetGrammaticalGenders(); //w
        public IEnumerable<LexicalCategory> GetLexicalCategories(); //w
        public Word GetRandomWord(); // -> w private
        //public bool SaveStats(Word word, bool isCorrect);//study -> s//
        public bool RestartProgressForAll(string userId); //w
        public bool RestartProgress(int id); //w
        public int GetWordsTimesCorrect(int id); //s, w -> w
        public int GetWordsTimesIncorrect(int id); //s, w -> w
        //public int GetWordsTotalTrainedTimes(int id); //s//
        //public List<StudyEntry> GetAllStudyEntries(string userId);//s//
        public List<StudyEntry> GetStudyEntries(int wordId); // -> w private
        public List<WordDTO> GetWordDTOsByWordText(string word, bool isEnglish);//a -> w
        //public IEnumerable<IGrouping<int, StudyEntry>> GetGroupsOfLearntStudyEntries(string userId, int correctNumberForLearning); //s//
    }
}
