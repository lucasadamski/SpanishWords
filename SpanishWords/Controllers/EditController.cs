using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index()
        {
            EditViewModel editModel = new EditViewModel();
            return View(editModel);
        }
    }
}
