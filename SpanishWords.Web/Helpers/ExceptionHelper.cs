namespace SpanishWords.Web.Helpers
{
    public static class ExceptionHelper
    {
        public const string EMPTY_VARIABLE = "Variable is empty or null.";
        public const string DATABASE_CONNECTION_ERROR = "Cannot connect to database.";
        public const string METHOD_EMPTY_PARAMETER = "Method received empty argument.";
        public const string API_ADD_TRANSLATION_ERROR = "API add translation error.";

        public static string GetErrorMessage(string exceptionMessage)
        {
            return $"Exception message: {exceptionMessage}";
        }
    }
}