using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MyWebPlay.Extension
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest, string rootPth)
        {
            var path = Path.Combine(rootPth,"Admin", System.IO.File.ReadAllText(Path.Combine(rootPth, "Admin", "SecureSettingAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Replace("[Encrypted_3275]","").Split("<5828>",StringSplitOptions.RemoveEmptyEntries);
                _mailSettings.Mail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                if (mailRequest.ToEmail == _mailSettings.Mail || string.IsNullOrEmpty(mailRequest.ToEmail))
                mailRequest.ToEmail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                _mailSettings.Password = StringMaHoaExtension.Decrypt(info[1], "32752262");
            }

            var email1 = new MimeMessage();
            email1.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email1.To.Add(MailboxAddress.Parse(_mailSettings.Mail));

            email1.Subject = mailRequest.Subject;
            var flag = 0;
            if (mailRequest.ToEmail != _mailSettings.Mail)
            {
                email1.Subject += " ~|~ " + mailRequest.ToEmail;
                flag = 1;
            }

            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email1.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email1);
            smtp.Disconnect(true);

            //-------------------------------------

            if (flag == 1)
            {
                var email2= new MimeMessage();
                email2.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email2.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

                email2.Subject = "Tin nhắn/email nội dung của My Web Play đến với bạn theo yêu cầu (nếu không vui lòng bỏ qua)";

                var builderX = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builderX.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                builderX.HtmlBody = "";
                email2.Body = builderX.ToMessageBody();
                using var smtpX = new SmtpClient();
                smtpX.CheckCertificateRevocation = false;
                smtpX.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtpX.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtpX.SendAsync(email2);
                smtp.Disconnect(true);
            }

        }
    }
}