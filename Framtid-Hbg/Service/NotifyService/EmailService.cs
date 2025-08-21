using System.Net;
using System.Net.Mail;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service;

public class EmailService : INotifyService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;
    private readonly string _to;
    
    public EmailService()
    {
        _host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? string.Empty;
        _port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0");
        _username = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? string.Empty;
        _password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
        _to = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT") ?? string.Empty;
    }
    
    public bool SendMessage(INotifyMessage notifyMessage)
    {
        using var mailClient = new SmtpClient(_host, _port);
        mailClient.EnableSsl = true;
        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        mailClient.Credentials = new NetworkCredential(_username, _password);
        using var message =
            new MailMessage(
                notifyMessage.Sender, 
                _to, 
                notifyMessage.Subject, 
                notifyMessage.Message);

        try
        {
            if (notifyMessage.Attachments.Count > 0)
            {
                for (var index = 0; index < notifyMessage.Attachments.Count; index++)
                {
                    var notifyMessageAttachment = notifyMessage.Attachments[index];
                    message.Attachments.Add(
                        new Attachment(
                            notifyMessageAttachment,
                            $"image{index + 1}.jpeg",
                            "image/jpeg"));
                }
            }

            mailClient.Send(message);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine("Message was not sent: " + ex);
            return false;
        }
        catch (IOException ex)
        {
            Console.WriteLine("Failed adding attachment: " + ex);
            return false;
        }

        return true;
    }
}