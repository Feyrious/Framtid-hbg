using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service.NotifyService;

namespace Framtid_hbg.Test.NotifyService;

public class TestNotifyMessage
{
    private NotifyMessage _notifyMessage;
    
    [SetUp]
    public void Setup()
    {
        _notifyMessage = new NotifyMessage();
    }

    [Test]
    public void TestPrepareContentFromContactViewModel()
    {
        var contactViewModel = new ContactViewModel
        {
            Adress = "Testgatan 2B",
            ContactType = "Contact",
            Email = "test@email.com",
            Message = "Test Message",
            Name = "John Doe",
            PhoneNumber = "0731234567"
        };

        var message = _notifyMessage.PrepareContentFrom(contactViewModel);
        
        Assert.That(message.From, Is.EqualTo(contactViewModel.Email));
        Assert.That(message.Subject, Is.EqualTo(contactViewModel.ContactType));
        Assert.That(message.Message, Does.Contain(contactViewModel.Name));
        Assert.That(message.Message, Does.Contain(contactViewModel.Email));
        Assert.That(message.Message, Does.Contain(contactViewModel.Adress));
        Assert.That(message.Message, Does.Contain(contactViewModel.PhoneNumber));
        Assert.That(message.Message, Does.Contain(contactViewModel.Message));
    }

}