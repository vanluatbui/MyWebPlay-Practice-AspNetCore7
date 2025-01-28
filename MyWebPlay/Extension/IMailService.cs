namespace MyWebPlay.Extension
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string rootPath, string? anotherToMail = "", string? host = "");
        bool TestSendMail(MailRequest mailRequest, string rootPth);
    }
}
