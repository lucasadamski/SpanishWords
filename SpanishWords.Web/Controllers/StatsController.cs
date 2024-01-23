using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;
using SpanishWords.Web.Helpers;
using SpanishWords.Web.Models;
using System.Security.Claims;


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
            SetHardestWordToLearn(stats, _userId);
            SetAverageTimeForLearningPerWord(stats, _userId);
            SetAverageQuestionCountPerWord(stats, _userId);
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
        public void SetHardestWordToLearn(StatsViewModel stats, string userId)
        {
            List<Word> learntWords = _wordRepository.GetAllLearntWords(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING).ToList();
            Word result = learntWords.OrderBy(n => _wordRepository.GetWordsTimesIncorrect(n.Id)).First();
            stats.HardestWordToLearn = result;
        }
        public void SetAverageTimeForLearningPerWord(StatsViewModel stats, string userId)
        {      
            var groupsOfLearntStudyEntries = _wordRepository
                .GetAllStudyEntries(userId)
                .Where(n => n.Correct == true)
                .GroupBy(n => n.Statistic.Id)
                .Where(n => n.Count() >= SettingsHelper.CORRECT_NUMBER_FOR_LEARNING);

            if (groupsOfLearntStudyEntries.Count() == 0)
            {
                stats.AverageTimeForLearningPerWord = "No words learnt yet.";
                return;
            }

            List<StudyEntry> dateWhenWordIsLearnt = groupsOfLearntStudyEntries
                .Select(g => g.ElementAt(2))
                .ToList();

            List<TimeSpan> timesForLearningWord = dateWhenWordIsLearnt
                .Select(n => n.Date - n.Statistic.CreateDate)
                .ToList();

            double totalDays = timesForLearningWord.Sum(n => n.TotalDays);
            double averegeDaysForLearning = totalDays / timesForLearningWord.Count();
            TimeSpan averageTime = TimeSpan.FromDays(averegeDaysForLearning);

            stats.AverageTimeForLearningPerWord = $"{(int)averageTime.TotalDays} days and {averageTime.Hours} hours.";
        }
        private void SetAverageQuestionCountPerWord(StatsViewModel stats, string userId)
        {
            int questionsCount = _wordRepository.GetAllStudyEntries(userId).Count();
            int wordsCount = _wordRepository.GetAllWords(userId).Count();

            stats.AverageQuestionCountPerWord = questionsCount / wordsCount;
        }
    }
}
