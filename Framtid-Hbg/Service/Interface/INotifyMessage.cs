﻿namespace Framtid_hbg.Website.Service.Interface;

public interface INotifyMessage
{
    public string From { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public List<MemoryStream> Attachments { get; set; }
}