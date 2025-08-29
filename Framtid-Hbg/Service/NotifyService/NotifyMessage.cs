using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Service.NotifyService;

public class NotifyMessage : INotifyMessage
{
    public string From { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<MemoryStream> Attachments { get; set; } = [];

    public INotifyMessage PrepareContentFrom(ContactViewModel model)
    {
        if (model.Email == null || model.ContactType == null || model.Message == null)
            throw new ArgumentException("");
        
        From = model.Email;
        Recipient = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT") ?? string.Empty;
        Subject = model.ContactType;
        Message = $"Namn: {model.Name}{Environment.NewLine}";
        Message += $"Email: {model.Email}{Environment.NewLine}";
        
        if (model.PhoneNumber != null)
            Message += $"Telefon: {model.PhoneNumber}{Environment.NewLine}";
        
        if (model.Adress != null)
            Message += $"{Environment.NewLine}" +
                               $"Adress:{Environment.NewLine}" +
                               $"{model.Adress}{Environment.NewLine}";
        
        Message += $"{Environment.NewLine}" +
                           $"Meddelande:{Environment.NewLine}" +
                           $"{model.Message}{Environment.NewLine}";

        return this;
    }
}