using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using System.Text.RegularExpressions;
using MyWebPlay.Model;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MyWebPlay.Extension
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest, string rootPth, string? anotherToMail = "", string? host = "", bool isLogMail = true)
        {
            var nameFileLog = "maillog_" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Day.ToString("00") + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Month.ToString("00") + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Year + ".txt";
            var pathLogMail = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "MailLog");
            var pathMailLog = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "MailLog", nameFileLog);

            if (File.Exists(pathMailLog) == false)
            {
                if (new System.IO.DirectoryInfo(pathLogMail).Exists == true)
                    new System.IO.DirectoryInfo(pathLogMail).Delete(true);

                new System.IO.DirectoryInfo(pathLogMail).Create();

                new FileInfo(pathMailLog).Create().Dispose();
            }


            if (isLogMail == true)
            {
                var noidungLog = FileExtension.ReadFile(pathMailLog).Replace("\r", "").Split("\n\n==================================================\n\n", StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < noidungLog.Length; i++)
                {
                    var check_body = mailRequest.Body.Replace("\r", "");
                    var check_log = noidungLog[i];

                    check_body = Regex.Replace(check_body, "\\d{2}\\/\\d{2}\\/\\d{4} \\d{2}:\\d{2}:\\d{2} (AM|PM)", "");
                    check_log = Regex.Replace(check_log, "\\d{2}\\/\\d{2}\\/\\d{4} \\d{2}:\\d{2}:\\d{2} (AM|PM)","");

                    if (check_body == check_log)
                        return;
                }

            var newLog = mailRequest.Body + "\r\n\r\n==================================================\r\n\r\n";

            FileExtension.WriteFile(pathMailLog, newLog);
            }

            var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = FileExtension.ReadFile(path);

            var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Replace("[Encrypted_3275]","").Split("<5828>",StringSplitOptions.RemoveEmptyEntries);
                _mailSettings.Mail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                mailRequest.ToEmail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                _mailSettings.Password = StringMaHoaExtension.Decrypt(info[1], "32752262");
            }

            var email1 = new MimeMessage();
            email1.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email1.To.Add(MailboxAddress.Parse(_mailSettings.Mail));

            email1.Subject = mailRequest.Subject;
            var flag = 0;
            if (string.IsNullOrEmpty(anotherToMail) == false)
            {
                email1.Subject += " ~|~ " + anotherToMail;
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
                if (string.IsNullOrEmpty(anotherToMail))
                await smtp.SendAsync(email1);
                smtp.Disconnect(true);



            //-------------------------------------

            if (string.IsNullOrEmpty(anotherToMail) == false)
            {
                var email2= new MimeMessage();
                email2.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email2.To.Add(MailboxAddress.Parse(anotherToMail));

                email2.Subject = host+" Tin nhắn/email nội dung của My Web Play đến với bạn theo yêu cầu (nếu không vui lòng bỏ qua)";

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

        public bool TestSendMail(MailRequest mailRequest, string rootPth)
        {
            try
            {
                var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[3] != "[NULL]")
                {
                    var info = infoX[3].Replace("[Encrypted_3275]", "").Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                    _mailSettings.Mail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                    if (mailRequest.ToEmail == _mailSettings.Mail || string.IsNullOrEmpty(mailRequest.ToEmail))
                        mailRequest.ToEmail = StringMaHoaExtension.Decrypt(info[0], "32752262");
                    _mailSettings.Password = StringMaHoaExtension.Decrypt(info[1], "32752262");
                }

                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                smtp.Disconnect(true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}