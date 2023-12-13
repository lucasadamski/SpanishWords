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
        private IWordRepository _wordRepository;
        private int _randomNumber;

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
            if (LoadWordsToAnswer(study) == false) return View("NoWordsToStudy");
            if (study.WordsToAnswer.Count() == 0) throw new ArgumentOutOfRangeException(); //TODO: Dodać okienko ostrzegawcze: "Użytkownik nie dodał żadnych słów, nie można rozpocząć Study Session".
            //generated randomNumber for the first Word
            _randomNumber = RandomNumberGenerator.GetInt32(study.WordsToAnswer.Count());
            study.IndexesOfWordsAnswered.Add(_randomNumber);
            study.RandomWord = study.WordsToAnswer[_randomNumber];
            
            return View(study);
        }

        [HttpPost]
        public IActionResult Index(StudyViewModel study)
        {
            study.Answer = study.Answer.Trim();
            if (study == null || study.Answer == null || study.Answer == "")  throw new InvalidDataException(); //TODO: obsłużyć, zcrashuje aplikację
            if (LoadWordsToAnswer(study) == false) return View("NoWordsToStudy");
            if (study.RandomWord.Spanish == study.Answer)
            {
                //If every words in collection has been answered then terminate the study session
                if(study.IndexesOfWordsAnswered.Count() == study.WordsToAnswer.Count())
                {
                    //Zakończenie testu
                    return View("StudyComplete");
                }
                //Generate random number until it's the one that has not been already answered
                while (true)
                {
                    _randomNumber = RandomNumberGenerator.GetInt32(study.WordsToAnswer.Count());
                    if (study.IndexesOfWordsAnswered.Contains(_randomNumber) == true) continue;
                    else break;
                }
                study.IndexesOfWordsAnswered.Add(_randomNumber);
                /********************************
                Inicjalizuję nową propercję RandomWord i wysyłam do formularza, ale ten nieszczęsny formularz generuje starą wartość w linii 22,23 i 24
                **********************************/
                ModelState.Clear();
                study.RandomWord = study.WordsToAnswer[_randomNumber];
        
                return View(study);
            }
            else
            {
                return View(study);
            }
        }

        private bool LoadWordsToAnswer(StudyViewModel study)
        {
            study.WordsToAnswer = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            if (study.WordsToAnswer == null || study.WordsToAnswer.Count() == 0) return false;
            return true;
        }


    }

}
