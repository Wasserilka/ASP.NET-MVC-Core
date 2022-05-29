namespace Lesson_8.Messenger.Configuration
{
    public sealed class MailGatewayOptions
    {
        private const int DefaultPort = 25;
        public MailGatewayOptions()
        {
            Port = DefaultPort;
        }
        public string SenderName { get; set; }
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
