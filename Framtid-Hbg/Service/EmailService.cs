using System.Net.Mail;
using Framtid_hbg.Website.Models;

namespace Framtid_hbg.Website.Service;

public class EmailService
{
    public bool SendEmail(ContactViewModel contact)
    {
        try
        {
            SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential("username", "password");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            var from = new MailAddress("noreply@ryden.dev", "Noreply");
            var to = new MailAddress("support@ryden.dev", "Support");
            var myMail = new MailMessage(from, to);

            // set subject and encoding
            myMail.Subject = "Förfrågan gällande: " + contact.ContactType;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
        }
        catch (SmtpException ex)
        {
            throw new ApplicationException
                ("SmtpException has occured: " + ex.Message);
        }

        return true;
    }
}