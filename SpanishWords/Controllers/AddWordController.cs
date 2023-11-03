using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Web.Controllers
{
    public class AddWordController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Add()
        {
            WordViewModel word = new WordViewModel();
            return View(word);
        }
    }
}
