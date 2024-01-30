using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpanishWords.EntityFramework.Helpers;
using SpanishWords.Models;
using SpanishWords.Web.Helpers;

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
        [HttpGet("translate")] 
        public List<WordDTO> GetEnglishToSpanishTranslation(string word)
        {
            if (CheckInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return new List<WordDTO>();
            }

            return GetWords(word, true);
        }

        //localhost:7057/api/TranslateApi/translate-spa-eng?word=a
        [HttpGet("translate-spa-eng")] 
        public List<WordDTO> GetSpanishToEnglishTranslation(string word)
        {
            if (CheckInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return new List<WordDTO>();
            }

            return GetWords(word, false);
        }

        //Skopiuj poniższy JSON do zapytania do postman.com, w MSSQL Server Managment widać że program dodał słowo do DB
        //{"spanish":"apiTestWord","english":"a","lexicalCategoryId":1,"grammaticalGenderId":1}
        //localhost:7057/api/TranslateApi/addtranslation
        [HttpPost("addtranslation")]
        public bool AddTranslation(WordDTO apiWord)
        {
            if (CheckInput(apiWord.English) != true || CheckInput(apiWord.Spanish) != true)
            {
                _logger.LogError(ExceptionHelper.API_ADD_TRANSLATION_ERROR);
                return false;
            }

            Word word = CreateWordFromWordDTO(apiWord);

            if (_wordRepository.Add(word) == true)
            {
                return true;
            }
            else
            {
                _logger.LogError(ExceptionHelper.API_ADD_TRANSLATION_ERROR);
                return false;
            }
        }

        private Word CreateWordFromWordDTO(WordDTO word)
        {
            return new Word()
            {
                Spanish = word.Spanish,
                English = word.English,
                LexicalCategoryId = word.LexicalCategoryId,
                GrammaticalGenderId = word.GrammaticalGenderId,
                UserId = ApiHelper.API_USER_ID,
                StatisticId = _wordRepository.CreateAndAddStatistic(ApiHelper.API_TIMES_CORRECT_TO_LEARN).Id
            };
        }

        private bool CheckInput(string input)
        {
            if (input.IsNullOrEmpty() == true) return false;
            input = input.Trim();
            input = input.ToLower();
            if (input.IsNullOrEmpty() == true) return false;
            return true;
        }

        private List<WordDTO> GetWords(string word, bool isEnglish)
        {
            if (word == null || isEnglish == null)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return new List<WordDTO>();
            }
            List<WordDTO> result = _wordRepository.GetWordDTOsByWordText(word, isEnglish);
            return result;
        }
    }
}
