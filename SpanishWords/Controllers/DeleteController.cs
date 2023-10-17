using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class DeleteController : Controller
    {
        public IActionResult Index()
        {
            DeleteViewModel deleteModel = new DeleteViewModel();
            return View(deleteModel);
        }
    }
}
