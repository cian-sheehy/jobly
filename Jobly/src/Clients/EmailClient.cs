using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Jobly.Clients;

public class EmailClient
{
    private readonly MailMessage _mailMessage;

    public EmailClient()
    {
        _mailMessage = new MailMessage();
    }

    public async Task Send(string from, string to, string subject, string body)
    {
        _mailMessage.From = new MailAddress(from);
        _mailMessage.To.Add(to);
        _mailMessage.Subject = subject;
        _mailMessage.Body = body;
        _mailMessage.IsBodyHtml = true;

        using var smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Timeout = 5000;
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(@from, Environment.GetEnvironmentVariable("JOBLY_GOOGLE_APP_PASSWORD"));
        await smtp.SendMailAsync(_mailMessage, CancellationToken.None);
    }
}