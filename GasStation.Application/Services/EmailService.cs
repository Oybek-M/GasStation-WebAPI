using GasStation.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;


namespace GasStation.Application.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    private readonly IConfiguration _config = configuration.GetSection("");

    public async Task SendMessageAsync(string to, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = message};



        var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config["EmailAddres"], _config["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
