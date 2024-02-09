namespace SpanishWords.Web.Models
{
    public class AddWordErrorViewModel
    {
        public string MessageTitle { get; set; } = "Ups! Sorry, something went wrong...";
        public List<string> Messages { get; set; } = new List<string>() { " We have some technical problem here, we are working as fast as we can to solve it.",
        "We are real sorry for this inconvinience. If you have any questions contact support team <a href=\"https://www.technicalsupport.com/\">here</a>."};

        public bool ClearMessages()
        {
            MessageTitle = "";
            try
            {
                Messages.Clear();
            }
            catch (Exception)
            {
                return false;
            }      
            return true;
        }
    }
}
