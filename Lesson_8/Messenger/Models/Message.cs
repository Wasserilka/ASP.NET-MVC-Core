namespace Lesson_8.Messenger.Models
{
    public class Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Name { get; set; }
        public bool IsHtml { get; set; }
    }
}
