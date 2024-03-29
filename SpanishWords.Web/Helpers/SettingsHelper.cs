﻿namespace SpanishWords.Web.Helpers
{
    public static class SettingsHelper
    {
        public const int CORRECT_NUMBER_FOR_LEARNING = 3;
        public const int MAX_LEXICAL_CATEGORY_ID = 3;
        public const int MAX_GRAMMATICAL_GENDER_ID = 2;

        public static int GetCorrectNumberForLearning(IConfiguration config)
        {
            if (config == null)
            {
                return CORRECT_NUMBER_FOR_LEARNING;
            }
            return config.GetValue<int>("NumberForCorrectsAnswersToLearn");
        }
    }
}
