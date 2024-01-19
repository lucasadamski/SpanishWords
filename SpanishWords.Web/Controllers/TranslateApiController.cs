using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpanishWords.EntityFramework.Helpers;
using SpanishWords.Models;

namespace SpanishWords.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateApiController : ControllerBase
    {
        private IWordRepository _wordRepository;
        private readonly ILogger<WordRepository> _logger;

        public TranslateApiController(IWordRepository wordRepository, ILogger<WordRepository> logger)
        {
            _wordRepository = wordRepository;
            _logger = logger;
        }

        //localhost:7057/api/TranslateApi/translate?word=a
        [HttpGet("translate")] //KLAUDIA PLEASE HELP: Gdy chcę zmienić nazwę na jakąkolwiek inną to "website didn't send any information" WTF? Chciałbym aby to się nazywało "translate-eng-spa"
        public string GetEnglishToSpanishTranslation(string word)
        {
            if (CheckInput(ref word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return "";
            } 
            List<Word> words = new List<Word>();
            words = _wordRepository.GetAllWords().ToList();
            Word result = words.Where(n => n.English == word).FirstOrDefault();
            if (result == null) return "";

            return result.Spanish;
        }

        //localhost:7057/api/TranslateApi/translate-spa-eng?word=a
        [HttpGet("translate-spa-eng")] 
        public string GetSpanishToEnglishTranslation(string word)
        {
            if (CheckInput(ref word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return "";
            }
            List<Word> words = new List<Word>();
            words = _wordRepository.GetAllWords().ToList();
            Word result = words.Where(n => n.Spanish == word).FirstOrDefault();
            if (result == null) return "";

            return result.English;
        }

        private bool CheckInput(ref string input)
        {
            if (input.IsNullOrEmpty() == true) return false;
            input = input.Trim();
            input = input.ToLower();
            if (input.IsNullOrEmpty() == true) return false;
            return true;
        }

    }
}
