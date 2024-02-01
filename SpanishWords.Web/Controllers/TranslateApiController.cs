using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public APIWordsDTO GetEnglishToSpanishTranslation(string word)
        {
            APIWordsDTO response = new APIWordsDTO();
            if (CheckStringInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                response.APIResponse.SetAllFields(ApiHelper.API_INPUT_PROCESS_ERROR, success: false, error: true);
                response.Words = new List<WordDTO>();
                return response;
            }
            response.Words = GetWords(word, true);
            SetSuccessfulResponse(response);

            return response;
        }

        //localhost:7057/api/TranslateApi/translate-spa-eng?word=a
        [HttpGet("translate-spa-eng")] 
        public APIWordsDTO GetSpanishToEnglishTranslation(string word)
        {
            APIWordsDTO response = new APIWordsDTO();
            if (CheckStringInput(word) == false)
            {
                _logger.LogError(ExceptionHelper.METHOD_EMPTY_PARAMETER);
                response.APIResponse.SetAllFields(ApiHelper.API_INPUT_PROCESS_ERROR, success: false, error: true);
                response.Words = new List<WordDTO>();
                return response;
            }
            response.Words = GetWords(word, false);
            SetSuccessfulResponse(response);

            return response;
        }

        //Skopiuj poniższy JSON do zapytania do postman.com, w MSSQL Server Managment widać że program dodał słowo do DB
        //{"spanish":"apiTestWord","english":"a","lexicalCategoryId":1,"grammaticalGenderId":1}
        //localhost:7057/api/TranslateApi/addtranslation
        [HttpPost("addtranslation")]
        public APIResponseInfo AddTranslation(WordDTO apiWord)
        {
            APIResponseInfo response = new APIResponseInfo();
            if (ValidateInputAndSetResponse(apiWord, response) != true)
            {
                _logger.LogError(ExceptionHelper.API_ADD_TRANSLATION_ERROR);
                return response;
            }
            TrimAndLowerWords(apiWord);
            Word word = CreateWordFromWordDTO(apiWord);
            if (_wordRepository.Add(word) == true)
            {
                response.SetAllFields(ApiHelper.InputSuccessful(word.English, word.Spanish));
                return response;
            }
            else
            {
                _logger.LogError(ExceptionHelper.API_ADD_TRANSLATION_ERROR);
                response.SetAllFields(ApiHelper.API_INPUT_PROCESS_ERROR, success: false, error: true);
                return response;
            }
        }

        private Word CreateWordFromWordDTO(WordDTO word)
        {
            int statisticId = _wordRepository.CreateAndAddStatistic(ApiHelper.API_TIMES_CORRECT_TO_LEARN).Id;
            if (statisticId == -1)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return new Word();
            }

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

        private bool CheckStringInput(string input)
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

        private void SetSuccessfulResponse(APIWordsDTO response)
        {
            int wordsCount = response.Words.Count();
            if (wordsCount == 0)
                response.APIResponse.SetAllFields(ApiHelper.API_RESPONSE_EMPTY);
            else
                response.APIResponse.SetAllFields(ApiHelper.ResponseSuccessful(response.Words.Count()));
        }      
        
        private bool ValidateInputAndSetResponse(WordDTO word, APIResponseInfo response)
        {
            bool isProblem = false;
            response.Data = "";
            if (CheckStringInput(word.English) != true)
            {
                response.Data += ApiHelper.API_INPUT_EN_ERROR;
                isProblem = true;
            }
            if (CheckStringInput(word.Spanish) != true)
            {
                response.Data += ApiHelper.API_INPUT_ES_ERROR;
                isProblem = true;
            }
            if (word.LexicalCategoryId == null || word.LexicalCategoryId < 1 || word.LexicalCategoryId > SettingsHelper.MAX_LEXICAL_CATEGORY_ID)
            {
                response.Data += ApiHelper.API_INPUT_LEX_ERROR;
                isProblem = true;
            }
            if (word.GrammaticalGenderId == null || word.GrammaticalGenderId < 1 || word.GrammaticalGenderId > SettingsHelper.MAX_GRAMMATICAL_GENDER_ID)
            {
                response.Data += ApiHelper.API_INPUT_GRA_ERROR;
                isProblem = true;
            }
            if (isProblem == true)
            {
                response.Success = false;
                response.Error = true;
                return false;
            }
            return true;
        }

        private void TrimAndLowerWords(WordDTO word)
        {
            word.English = word.English.Trim();
            word.Spanish = word.Spanish.Trim();
            word.English = word.English.ToLower();
            word.Spanish = word.Spanish.ToLower();
        }
    }
}
