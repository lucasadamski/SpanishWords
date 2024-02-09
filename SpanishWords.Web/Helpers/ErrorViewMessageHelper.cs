namespace SpanishWords.Web.Helpers
{
    public static class ErrorViewMessageHelper
    {
        //Add words errors
        public static string ADD_WORD_TITLE_MESSAGE = "Sorry, can't add word.";
        public static string ADD_WORD_MESSAGE = "Something is missing. Please check below message:";
        public static string ADD_WORD_ENGLISH_WORD_ERROR = "English word is empty.";
        public static string ADD_WORD_SPANISH_WORD_ERROR = "Spanish word is empty.";
        public static string ADD_WORD_LEXICAL_ERROR = "Lexical category not selected.";
        public static string ADD_WORD_GRAMMATICAL_ERROR = "Grammatical category not selected.";

        //App/server error message
        public static string APP_ERROR_TITLE_MESSAGE = "Ups! Something went wrong.";
        public static string APP_ERROR_MESSAGE = "We are real sorry, but we have some problems. We are working as fast as possible to resolve this issue. Please contact the administrator for information.";
    }
}
