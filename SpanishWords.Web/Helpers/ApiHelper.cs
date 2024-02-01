namespace SpanishWords.Web.Helpers
{
    public static class ApiHelper
    {
        public static string API_USER_ID = "0";
        public static int API_TIMES_CORRECT_TO_LEARN = 3;
        public static string API_DATABASE_CONNECTION_ERROR = "Database connection error.";

        public static string API_RESPONSE_EMPTY = "Did not found any records matching your input.";
        public static string API_RESPONSE_SUCCESSFUL = "Response successful.";

        public static string API_INPUT_PROCESS_ERROR    = "Cannot proccess provided input. ";
        public static string API_INPUT_SUCCESSFUL       = "Word added with success. ";
        public static string API_INPUT_DATABASE_ERROR   = "Error: Can't write to DataBase. ";
        public static string API_INPUT_EN_ERROR         = "Error: English word is empty or missing. ";
        public static string API_INPUT_ES_ERROR         = "Error: Spanish word is empty or missing. ";
        public static string API_INPUT_LEX_ERROR        = "Error: LexicalCategoryId is empty, missing, or out of range. ";
        public static string API_INPUT_GRA_ERROR        = "Error: GrammaticalCategoryId is empty, missing, or out of range. ";

        public static string ResponseSuccessful(int wordsFound) => API_RESPONSE_SUCCESSFUL + $"Found {wordsFound} words.";
        public static string InputSuccessful(string english, string spanish) => API_INPUT_SUCCESSFUL + $"Added EN:{english}/ES:{spanish}";
    }
}
