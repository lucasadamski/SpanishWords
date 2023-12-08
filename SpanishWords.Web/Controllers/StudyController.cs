using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SpanishWords.Models;
using SpanishWords.Web.Models;


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
            StudyViewModel study = new StudyViewModel();
            study.RandomWord = _wordRepository.GetRandomWord();
            study.Answer = "";
            return View(study);
        }


        [HttpPost]
        public IActionResult Index(StudyViewModel study)
        {
            if (study == null) throw new InvalidDataException();


            if (study.RandomWord.Spanish == study.Answer)
            {

                StudyViewModel nextStudyWord = new StudyViewModel();
                nextStudyWord.RandomWord =  _wordRepository.GetRandomWord();
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
