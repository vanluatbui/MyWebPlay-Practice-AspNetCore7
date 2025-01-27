namespace MyWebPlay.Extension
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string rootPath);
        bool TestSendMail(MailRequest mailRequest, string rootPth);
    }
}
