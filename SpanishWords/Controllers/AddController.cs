using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            AddViewModel addModel = new AddViewModel();
            return View(addModel);
        }
    }
}
