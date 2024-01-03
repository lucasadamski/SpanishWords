﻿namespace SpanishWords.EntityFramework.Helpers
{
    public static class ExceptionHelper
    {
        public const string EMPTY_VARIABLE = "Variable is empty or null. ";
        public const string DATABASE_CONNECTION_ERROR = "Cannot connect to database. ";
        public const string EF_QUERY_ERROR = "Entity Framework query returned exception. ";

        public static string GetErrorMessage(string exceptionMessage)
        {
            return $"Exception message: {exceptionMessage}";
        }
    }
}