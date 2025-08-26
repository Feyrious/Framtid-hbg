using Framtid_hbg.Website.Service.Interface;
using Framtid_hbg.Website.Service.NotifyService;

namespace Framtid_hbg.Test.NotifyService;

public class TestEmailService
{
    private EmailService _service;
    
    [SetUp]
    public void Setup()
    {
        _service = new EmailService();
    }

    [Test]
    public void TestPeparingEmailMessageFromNotifyMessage()
    {
        var sender = "test@email.com";
        var recipient = "noreply@email.com";
        var subject = "Testing";
        var message = "Testing sending a message";
        
        var notifyMessage = new NotifyMessage
        {
            From = sender,
            Recipient = recipient,
            Subject = subject,
            Message = message
        } as INotifyMessage;

        var emailMessage = _service.PrepareEmailFrom(notifyMessage);
        
        Assert.That(emailMessage.From?.Address, Does.Contain(sender));
        Assert.That(emailMessage.Sender?.Address, Does.Contain(sender));
        Assert.That(emailMessage.To[0].Address, Does.Contain(recipient));
        Assert.That(emailMessage.Subject, Does.Contain(subject));
        Assert.That(emailMessage.Body, Does.Contain(message));
    }
}