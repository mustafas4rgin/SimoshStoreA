using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace SimoshStore;

public class SmtpConfiguration
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}

public class SmtpEmailService : IEmailService
{
    private readonly string _smtpServer = "smtp.gmail.com";  // Outlook SMTP sunucusu
    private readonly int _smtpPort = 587;  // TLS portu
    private readonly string _username = "simoshstoreco@gmail.com";  // Outlook e-posta adresiniz
    private readonly string _password = "lnqr khna jkbx ffyq";  // Outlook şifreniz

    public async Task<IServiceResult> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_username),
                Subject = subject,
                Body = body,
                IsBodyHtml = false // Eğer HTML içeriği gönderecekseniz true yapın
            };

            mailMessage.To.Add(to);

            using (var smtpClient = new SmtpClient(_smtpServer))
            {
                smtpClient.Port = _smtpPort;
                smtpClient.Credentials = new NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true; // TLS'yi etkinleştir
                smtpClient.Timeout = 60000;  // 30 saniye
                await smtpClient.SendMailAsync(mailMessage);
                return new ServiceResult(true, "E-posta başarıyla gönderildi.");
            }
        }
        catch (Exception ex)
        {
            return new ServiceResult(false, $"{ex.Message}{ex.StackTrace}{ex.InnerException?.Message}");
        }
    }
}

