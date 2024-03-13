using EFDataAccess.Repositories;
using SpanishWords.Models.DTOs;
using SpanishWords.Models.Tables;
using SpanishWords.Models.Views;
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
        public IEnumerable<v_Words_Stats> GetAllWordsWithStatsFromView(string userId);
        bool Edit(Word word);
        bool Delete(Word word);
        public CreateStatisticDTO CreateStatistic(int numberOfAnswersToLearnTheWord);
        public IEnumerable<GrammaticalGender> GetGrammaticalGenders();
        public IEnumerable<LexicalCategory> GetLexicalCategories();
        public Word GetRandomWord();
        public bool RestartProgressForAll(string userId);
        public bool RestartProgress(int id);
        public int GetWordsTimesCorrect(int id); 
        public int GetWordsTimesIncorrect(int id); 
        public List<StudyEntry> GetStudyEntries(int wordId); 
        public List<WordDTO> GetWordDTOsByWordText(string word, bool isEnglish);
    }
}
