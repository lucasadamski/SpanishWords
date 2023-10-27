using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
