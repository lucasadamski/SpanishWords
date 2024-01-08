using Microsoft.AspNetCore.Mvc;
using SpanishWords.Web.Models;


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
        public IActionResult Index()
        {
            StatsViewModel stats = new StatsViewModel();
            return View(stats);
        }
    }
}
