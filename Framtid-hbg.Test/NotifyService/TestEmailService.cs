using Framtid_hbg.Website.Service.Interface;
using Framtid_hbg.Website.Service.NotifyService;

namespace Framtid_hbg.Test.NotifyService;

public class TestEmailService
{
    private EmailService _service;
    
    [SetUp]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("SMTP_CONTACT_RECIPIENT", "recipient@test.com");
        _service = new EmailService("test.host", 587, "testuser", "testpass");
    }

    [Test]
    public void TestPreparingEmailMessageFromNotifyMessage()
    {
        var sender = "test@email.com";
        var recipient = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT") ?? "";
        var subject = "Testing";
        var message = "Testing sending a message";
        
        var notifyMessage = new NotifyMessage
        {
            From = sender,
            Recipient = recipient,
            Subject = subject,
            Message = message
        };

        var emailMessage = _service.PrepareEmailFrom(notifyMessage);
        
        Assert.That(emailMessage.From?.Address, Is.EqualTo(sender));
        Assert.That(emailMessage.To[0].Address, Is.EqualTo(recipient));
        Assert.That(emailMessage.Subject, Is.EqualTo(subject));
        Assert.That(emailMessage.Body, Is.EqualTo(message));
    }
}