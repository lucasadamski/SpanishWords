using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.EntityFramework.Helpers;
using SpanishWords.Models;
using SpanishWords.Web.Helpers;
using SpanishWords.Web.Models;
using System.Security.Claims;
using System.Text;
using SpanishWords.EntityFramework.Repositories;
using SpanishWords.EntityFramework.Repositories.Infrastructure;


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
        private IStatsRepository _statsRepository;
        private readonly ILogger<StatsController> _logger;
        private string _userId;

        public StatsController(IWordRepository wordRepository, IStatsRepository statsRepository, ILogger<StatsController> logger)
        {
            _wordRepository = wordRepository;
            _statsRepository = statsRepository;
            _logger = logger;
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
            if (userId == null || stats == null)
            {
                _logger.LogError(DBExceptionHelper.EMPTY_VARIABLE);
                stats.TotalWords = 0;
                stats.LearntWords = 0;
                stats.NotLearntWords = 0;
                return;
            }

            int notLearntWords;
            int learntWords;
            int totalWords;

            notLearntWords = _statsRepository.GetAllNotLearntWords(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING).Count();
            totalWords = _wordRepository.GetAllWords(userId).Count();
            learntWords = totalWords - notLearntWords;

            stats.TotalWords = totalWords;
            stats.LearntWords = learntWords;
            stats.NotLearntWords = notLearntWords;
        }
        public Word SetHardestWordToLearn(StatsViewModel stats, string userId)
        {
            if (userId == null || stats == null)
            {
                _logger.LogError(DBExceptionHelper.EMPTY_VARIABLE);
                return new Word();
            }
            List<Word> learntWords = _statsRepository.GetAllLearntWords(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING).ToList();
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
            IEnumerable<IGrouping<int, StudyEntry>> groupsOfLearntStudyEntries =
                _statsRepository.GetGroupsOfLearntStudyEntries(userId, SettingsHelper.CORRECT_NUMBER_FOR_LEARNING);

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
            if (averageTime.TotalDays >= 1.0) 
                return $"{(int)averageTime.TotalDays} days";
            if (averageTime.TotalHours >= 1.0) 
                return $"{(int)averageTime.TotalHours} hours";
            
            return $"{(int)averageTime.TotalMinutes} minutes";
        }
        private double SetAverageQuestionCountPerWord(StatsViewModel stats, string userId)
        {
            int questionsCount = _statsRepository.GetAllStudyEntries(userId).Count();
            if (questionsCount == 0) 
                return 0D;
            int wordsCount = _wordRepository.GetAllWords(userId).Count();
            if (wordsCount == 0)
                return 0D;

            return questionsCount / wordsCount;                
        }
    }
}
