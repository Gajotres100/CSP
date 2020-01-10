namespace SaaSApi
{
    public class Messages
    {
        public string Language { get; set; }
        public string Message { get; set; }

        public Messages()
        {

        }

        public Messages(string language, string message)
        {
            Language = language;
            Message = message;
        }
    }
}
