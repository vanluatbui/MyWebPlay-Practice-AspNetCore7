namespace MyWebPlay.Extension
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string rootPath, string? anotherToMail = "", string? host = "", bool isLogMail = true);
        bool TestSendMail(MailRequest mailRequest, string rootPth);
    }
}
