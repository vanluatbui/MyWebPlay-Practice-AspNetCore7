namespace MyWebPlay.Extension
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string rootPath);
    }
}
