namespace SpanishWords.Web.Helpers
{
    public static class ExceptionHelper
    {
        public const string EMPTY_VARIABLE = "Variable is empty or null.";
        public const string DATABASE_CONNECTION_ERROR = "Cannot connect to database.";

        public static string GetErrorMessage(string exceptionMessage)
        {
            return $"Exception message: {exceptionMessage}";
        }
    }
}