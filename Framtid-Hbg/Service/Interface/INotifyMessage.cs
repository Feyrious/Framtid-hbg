namespace Framtid_hbg.Website.Service.Interface;

public interface INotifyMessage
{
    public string Sender { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public List<MemoryStream> Attachments { get; set; }
}