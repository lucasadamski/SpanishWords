using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            StartViewModel startModel = new StartViewModel();
            return View(startModel);
        }
    }
}
