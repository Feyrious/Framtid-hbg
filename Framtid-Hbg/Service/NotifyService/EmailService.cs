using System.Net;
using System.Net.Mail;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service.NotifyService;

public class EmailService : INotifyService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;
    
    /// <summary>
    /// Set SMTP variables from Environmental variables.
    /// </summary>
    public EmailService()
    {
        _host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? string.Empty;
        _port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0");
        _username = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? string.Empty;
        _password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
    }

    /// <summary>
    /// Set SMTP variables from custom variables.
    /// </summary>
    /// <param name="host">Set the SMTP Host</param>
    /// <param name="port">Set the SMTP Port</param>
    /// <param name="username">Set the SMTP Username</param>
    /// <param name="password">Set the SMTP Password</param>
    public EmailService(string host, int port, string username, string password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notifyMessage"></param>
    /// <returns>Boolean if the message was sent or encountered a error</returns>
    public bool SendMessage(MailMessage mailMessage)
    {
        using var mailClient = new SmtpClient(_host, _port);
        mailClient.EnableSsl = true;
        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        mailClient.Credentials = new NetworkCredential(_username, _password);
        
        try
        {
            mailClient.Send(mailMessage);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine("Message was not sent: " + ex);
            return false;
        }

        return true;
    }

    public MailMessage PrepareEmailFrom(INotifyMessage notifyMessage)
    {
        var message =
            new MailMessage(
                notifyMessage.From, 
                notifyMessage.Recipient, 
                notifyMessage.Subject, 
                notifyMessage.Message);

        if (notifyMessage.Attachments.Count <= 0) 
            return message;
        
        for (var index = 0; index < notifyMessage.Attachments.Count; index++)
        {
            var notifyMessageAttachment = notifyMessage.Attachments[index];
            message.Attachments.Add(
                new Attachment(
                    notifyMessageAttachment,
                    $"image{index + 1}.jpeg",
                    "image/jpeg"));
        }

        return message;
    }
}