using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SpanishWords.Models;
using SpanishWords.Web.Models;
using System.Security.Claims;
using System.Security.Cryptography;


/*
Powyżej formularza wyświetla się słówko, które użytkownik ma przetłumaczyć 

Formularz zawiera 1 pole, które wypełniane jest przez użytkownika 

Formularz po uruchomieniu ma autofocus na pole formularza 

Jeśli użytkownik odpowie prawidłowo, powinien dostać nowe, losowe pytanie z listy słów, które utworzył, aformularz powinien sie wyczyścić 

Jeśli użytkownik odpowie nieprawidłowo, powinien do skutku otrzymywać to samo słowo do wyświetlenia 
*/


namespace SpanishWords.Web.Controllers
{
    public class StudyController : Controller
    {
        IWordRepository _wordRepository;

        public StudyController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public IActionResult Index()
        {

            /*******
             *  User aswers the collection of words. Contorller will send random word which was not answered yet (the WordsToAnswer index is 
             *  not present in IndexesOfAnsweredWords list). When the number of items in IndexesOfAnsweredWords is the same as number of items in
             *  WordsToAnswer, then it means that all the words has been aswered and the Study Session is terminated.
             * *****/


            StudyViewModel study = new StudyViewModel();
            //populates list of WordsToAnswer for study Session, based on specific User
            study.WordsToAnswer = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            if (study.WordsToAnswer.Count() == 0) throw new ArgumentOutOfRangeException(); //TODO: Dodać okienko ostrzegawcze: "Użytkownik nie dodał żadnych słów, nie można rozpocząć Study Session".
            //generated randomNumber for the first Word
            int _randomNumber = RandomNumberGenerator.GetInt32(study.WordsToAnswer.Count() - 1);
            study.IndexesOfWordsAnswered.Add(_randomNumber);
            study.RandomWord = new Word();
            study.RandomWord.English = study.WordsToAnswer[_randomNumber].English;
            study.RandomWord.Spanish = study.WordsToAnswer[_randomNumber].Spanish;
            study.RandomWord.Id = study.WordsToAnswer[_randomNumber].Id;
            study.Answer = "";

            return View(study);
        }

        [HttpPost]
        public IActionResult Index(StudyViewModel study)
        {
            int _anotherRadomNumber = default;
            study.Answer = study.Answer.Trim();
            if (study == null || study.Answer == null || study.Answer == "")  throw new InvalidDataException(); //zcrashuje aplikację

            if (study.RandomWord.Spanish == study.Answer)
            {
                StudyViewModel nextStudyWord = new StudyViewModel();
                nextStudyWord.IndexesOfWordsAnswered = study.IndexesOfWordsAnswered;
                nextStudyWord.WordsToAnswer = study.WordsToAnswer;
                
                //If every words in collection has been answered then terminate the study session
                if(nextStudyWord.IndexesOfWordsAnswered.Count() == nextStudyWord.WordsToAnswer.Count())
                {
                    //Zakończenie testu
                    return RedirectToAction("Index", "Home");
                }
                //Generate random number until it's the one that has not been already answered
                while (true)
                {
                    _anotherRadomNumber = RandomNumberGenerator.GetInt32(nextStudyWord.WordsToAnswer.Count());
                    if (nextStudyWord.IndexesOfWordsAnswered.Contains(_anotherRadomNumber) == true) continue;
                    else break;
                }
                nextStudyWord.IndexesOfWordsAnswered.Add(_anotherRadomNumber);


                /********************************
                Inicjalizuję nową propercję RandomWord i wysyłam do formularza, ale ten nieszczęsny formularz generuje starą wartość w linii 22,23 i 24
                **********************************/
                ModelState.Clear();
                nextStudyWord.RandomWord = new Word();
                nextStudyWord.RandomWord.English = nextStudyWord.WordsToAnswer[_anotherRadomNumber].English; 
                nextStudyWord.RandomWord.Spanish = nextStudyWord.WordsToAnswer[_anotherRadomNumber].Spanish;
                nextStudyWord.RandomWord.Id = nextStudyWord.WordsToAnswer[_anotherRadomNumber].Id;
                nextStudyWord.Answer = "";
                return View(nextStudyWord);
            }
            else
            {
                StudyViewModel sameStudyWord = study;
                return View(sameStudyWord);
            }

        }
    }
}
