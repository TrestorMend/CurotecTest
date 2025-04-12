namespace Application.Responses
{
    public class Notification
    {
        public string Key { get; set; }
        public int? Code { get; set; }
        public string Message { get; set; }
        public string PropertyName { get; set; }

        public Notification(string key, string message, string propertyName = "")
        {
            Key = key;
            Message = message;
            PropertyName = propertyName;
        }
    }
}
