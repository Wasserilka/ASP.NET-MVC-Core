using Lesson_8.Messenger.Configuration;
using Lesson_8.Messenger.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Lesson_8.Messenger
{
    public class MailGateway : IDisposable
    {
        private readonly MailGatewayOptions _options;
        private readonly SmtpClient _client = new SmtpClient();

        public MailGateway(MailGatewayOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
            _client.Connect(options.SMTPServer, options.Port);
            _client.Authenticate(options.Sender, options.Password);
        }

        public async Task SendMessage(Message message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.SenderName,
            _options.Sender));
            emailMessage.To.Add(new MailboxAddress(message.Name,
            message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(message.IsHtml 
                ? MimeKit.Text.TextFormat.Html 
                : MimeKit.Text.TextFormat.Text)

            {
                Text = message.Body
            };

            await _client.SendAsync(emailMessage);
        }

        public void Dispose()
        {
            _client.Disconnect(true);
            _client.Dispose();
        }
    }
}
