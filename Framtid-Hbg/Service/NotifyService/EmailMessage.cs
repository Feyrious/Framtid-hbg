using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service;

public class EmailMessage : INotifyMessage
{
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<MemoryStream> Attachments { get; set; } = [];

    public void PrepareMessage(ContactViewModel model)
    {
        var recipiant = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT");
        
        if (recipiant == null || model.Email == null || model.ContactType == null || model.Message == null)
            throw new ArgumentException("");
        
        Recipient = recipiant;
        Subject = model.ContactType;
        Message = $"Namn: " + model.Name + Environment.NewLine;
        Message += $"Email: " + model.Email + Environment.NewLine;
        if (model.PhoneNumber > 999999)
            Message += $"Telefon: " + model.PhoneNumber + Environment.NewLine;
        if (model.Adress != null)
            Message += $"Adress: " + model.Adress + Environment.NewLine;
        Message += $"Meddelande: " + model.Message + Environment.NewLine;
    }
}