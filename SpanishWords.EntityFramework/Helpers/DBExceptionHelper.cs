namespace SpanishWords.EntityFramework.Helpers
{
    public static class DBExceptionHelper
    {
        public const string EMPTY_VARIABLE = "Variable is empty or null. ";
        public const string DATABASE_CONNECTION_ERROR = "Cannot connect to database. ";
        public const string DATABASE_WRITE_ERROR = "Write operation to Data Base error.";
        public const string EF_QUERY_ERROR = "Entity Framework query returned exception. ";
        public const string METHOD_EMPTY_PARAMETER = "Method received empty parameter.";

        public static string GetErrorMessage(string exceptionMessage)
        {
            return $"Exception message: {exceptionMessage}";
        }
    }
}