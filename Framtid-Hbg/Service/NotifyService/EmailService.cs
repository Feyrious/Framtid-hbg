using System.Net;
using System.Net.Mail;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service;

public class EmailService : INotifyService
{
    public bool SendMessage(INotifyMessage notifyMessage)
    {
        var host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? string.Empty;
        var port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? string.Empty);
        var username = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? string.Empty;
        var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
        var from = Environment.GetEnvironmentVariable("SMTP_FROM") ?? string.Empty;
        
        using var mailClient = new SmtpClient(host, port);
        mailClient.Credentials = new NetworkCredential(username, password);
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