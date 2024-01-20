﻿using EFDataAccess.Repositories;
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
        [HttpGet("translate")] //KLAUDIA PLEASE HELP: Gdy chcę zmienić nazwę na jakąkolwiek inną to "website didn't send any information" WTF? Chciałbym aby to się nazywało "translate-eng-spa"
        public List<Word> GetEnglishToSpanishTranslation(string word)
        {
            if (CheckInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return new List<Word>();
            } 
            List<Word> words = new List<Word>();
            words = _wordRepository.GetAllWords().ToList();
            List<Word> result = words.Where(n => n.English == word)
                                     .Select(n => new Word() { English = n.English, Spanish = n.Spanish, GrammaticalGenderId = n.GrammaticalGenderId, LexicalCategoryId = n.LexicalCategoryId })
                                     .ToList();
            return result;
        }

        //localhost:7057/api/TranslateApi/translate-spa-eng?word=a
        [HttpGet("translate-spa-eng")] //KLAUDIA PLEASE HELP: Nie działa ta nazwa w linku :(
        public List<Word> GetSpanishToEnglishTranslation(string word)
        {
            if (CheckInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                return new List<Word>();
            }
            List<Word> words = new List<Word>();
            words = _wordRepository.GetAllWords().ToList();
            List<Word> result = words.Where(n => n.Spanish == word)
                                     .Select(n => new Word() { English = n.English, Spanish = n.Spanish, GrammaticalGenderId = n.GrammaticalGenderId, LexicalCategoryId = n.LexicalCategoryId })
                                     .ToList();
            return result;
        }

        //KLAUDIA: Skopiuj poniższy JSON do zapytania do postman.com, w MSSQL Server Managment widać że program dodał słowo do DB
        //{"spanish":"apiTestWord","english":"a","lexicalCategoryId":1,"grammaticalGenderId":1}
        //localhost:7057/api/TranslateApi/addtranslation
        [HttpPost("addtranslation")]
        public bool AddTranslation(APIWord apiWord)
        {
            if (CheckInput(apiWord.English) != true || CheckInput(apiWord.Spanish) != true)
            {
                _logger.LogError(ExceptionHelper.API_ADD_TRANSLATION_ERROR);
                return false;
            }

            Word word = CreateWordFromAPIWord(apiWord);

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

        private Word CreateWordFromAPIWord(APIWord apiWord)
        {
            return new Word()
            {
                Spanish = apiWord.Spanish,
                English = apiWord.English,
                LexicalCategoryId = apiWord.LexicalCategoryId,
                GrammaticalGenderId = apiWord.GrammaticalGenderId,
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

    }
}
