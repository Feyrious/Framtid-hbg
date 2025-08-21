using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service;

public class EmailMessage : INotifyMessage
{
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<MemoryStream> Attachments { get; set; } = [];

    public void PrepareMessage(ContactViewModel model)
    {
        
        if (model.Email == null || model.ContactType == null || model.Message == null)
            throw new ArgumentException("");
        
        Sender = model.Email;
        Subject = model.ContactType;
        Message = $"Namn: " + model.Name + Environment.NewLine;
        Message += $"Email: " + model.Email + Environment.NewLine;
        if (model.PhoneNumber != null)
            Message += $"Telefon: " + model.PhoneNumber + Environment.NewLine;
        if (model.Adress != null)
            Message += $"Adress: " + model.Adress + Environment.NewLine;
        Message += $"Meddelande: " + model.Message + Environment.NewLine;
    }
}