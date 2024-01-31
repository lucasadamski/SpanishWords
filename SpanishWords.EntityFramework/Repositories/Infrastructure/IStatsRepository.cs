using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.EntityFramework.Repositories.Infrastructure
{
    public interface IStatsRepository
    {
        public IEnumerable<Word> GetAllNotLearntWords(string userId, int timesCorrect);
        public IEnumerable<Word> GetAllLearntWords(string userId, int timesCorrect);
        public bool SaveStats(Word word, bool isCorrect);        
        public int GetWordsTotalTrainedTimes(int id);
        public List<StudyEntry> GetAllStudyEntries(string userId);
        public IEnumerable<IGrouping<int, StudyEntry>> GetGroupsOfLearntStudyEntries
            (string userId, int correctNumberForLearning);
    }
}
