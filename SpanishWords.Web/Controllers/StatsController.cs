using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;
using SpanishWords.Web.Helpers;
using SpanishWords.Web.Models;
using System.Security.Claims;
using System.Text;


/*
 * Utworzyć widok podsumowujący aktywność użytkownika 
Widok powinien przedstawiać różnorodne statystyki np. 
-ilość nauczonych słówek total 
-średnia ilość czasu potrzebna na nauczenie słowa 
-najtrudniejsze słowo (po najdłuższym czasie nauki) 
-średnia ilość pytań per słówko 
-gdziekolwiek indziej poniesie Cię fantazja 
*/

namespace SpanishWords.Web.Controllers
{
    public class StatsController : Controller
    {
        private IWordRepository _wordRepository;
        private string _userId;

        public StatsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public IActionResult Index()
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            StatsViewModel stats = new StatsViewModel();
            SetTotalLearntWords(stats, _userId);
            stats.HardestWordToLearn = SetHardestWordToLearn(stats, _userId);
            stats.AverageTimeForLearningPerWord = SetAverageTimeForLearningPerWord(stats, _userId);
            stats.AverageQuestionCountPerWord = SetAverageQuestionCountPerWord(stats, _userId);
            return View(stats);
        }
        public void SetTotalLearntWords(StatsViewModel stats, string userId)
        {
            int notLearntWords;
            int learntWords;
            int totalWords;

            notLearntWords = _wordRepository.GetAllNotLearntWords(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING).Count();
            totalWords = _wordRepository.GetAllWords(userId).Count();
            learntWords = totalWords - notLearntWords;

            stats.TotalWords = totalWords;
            stats.LearntWords = learntWords;
            stats.NotLearntWords = notLearntWords;
        }
        public Word SetHardestWordToLearn(StatsViewModel stats, string userId)
        {
            List<Word> learntWords = _wordRepository.GetAllLearntWords(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING).ToList();
            if(learntWords.Count() == 0)
            {
                return new Word()
                {
                    English = MessageHelper.NO_WORDS_LEARNT,
                    Spanish = MessageHelper.NO_WORDS_LEARNT
                };
            }
            return learntWords.OrderBy(n => _wordRepository.GetWordsTimesIncorrect(n.Id)).First();            
        }
        public string SetAverageTimeForLearningPerWord(StatsViewModel stats, string userId)
        {
            IEnumerable<IGrouping<int, StudyEntry>> groupsOfLearntStudyEntries = _wordRepository
                .GetAllStudyEntries(userId)
                .Where(n => n.Correct == true)
                .GroupBy(n => n.Statistic.Id)
                .Where(n => n.Count() >= SettingsHelper.CORRECT_NUMBER_FOR_LEARNING);

            if (groupsOfLearntStudyEntries.Count() == 0)            
                return MessageHelper.NO_WORDS_LEARNT;            

            TimeSpan averageTime = GenerateAverageTime(groupsOfLearntStudyEntries, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING);

            return GenerateStringFromAverageTime(averageTime);
        }
        private TimeSpan GenerateAverageTime(IEnumerable<IGrouping<int, StudyEntry>> groupsOfLearntStudyEntries, int numberForLearing)
        {
            numberForLearing--;
            List<StudyEntry> dateWhenWordIsLearnt = groupsOfLearntStudyEntries
                .Select(g => g.ElementAt(numberForLearing))
                .ToList();

            List<TimeSpan> timesForLearningWord = dateWhenWordIsLearnt
                .Select(n => n.Date - n.Statistic.CreateDate)
                .ToList();

            double totalDays = timesForLearningWord.Sum(n => n.TotalDays);
            double averegeDaysForLearning = totalDays / timesForLearningWord.Count();
            return TimeSpan.FromDays(averegeDaysForLearning);
        }
        private string GenerateStringFromAverageTime(TimeSpan averageTime)
        {
            if ((int)averageTime.TotalDays != 0) return $"{(int)averageTime.TotalDays} days";
            else if (averageTime.Hours != 0) return $"{averageTime.Hours} hours";
            else return $"{averageTime.Minutes} minutes";
        }
        private double SetAverageQuestionCountPerWord(StatsViewModel stats, string userId)
        {
            int questionsCount = _wordRepository.GetAllStudyEntries(userId).Count();
            if (questionsCount == 0) 
                return 0D;
            int wordsCount = _wordRepository.GetAllWords(userId).Count();
            if (wordsCount == 0)
                return 0D;

            return questionsCount / wordsCount;                
        }
    }
}
