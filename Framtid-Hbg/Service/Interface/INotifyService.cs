using System.Net.Mail;

namespace Framtid_hbg.Website.Service.Interface;

public interface INotifyService
{
    public bool SendMessage(MailMessage mailMessage);

    public MailMessage PrepareEmailFrom(INotifyMessage notifyMessage);
}