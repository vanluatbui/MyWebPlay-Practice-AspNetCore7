using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using MyWebPlay.Model;
using System.Globalization;

namespace MyWebPlay.Extension
{
    public static class SendEmail
    {
        public static void SendMail2Step(string rootPth, string From, string? To, string Subject, string Body, string password, string again = "false", bool isBodyHtml = false, string ? anotherToMail = "", string? host = "")
        {
            if (again == "true") return;

            var nameFileLog = "maillog_" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Day.ToString("00") + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Month.ToString("00") + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).Year + ".txt";
            var pathLogMail = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "MailLog");
            var pathMailLog = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "MailLog", nameFileLog);

            if (File.Exists(pathMailLog) == false)
            {
                if (new System.IO.DirectoryInfo(pathLogMail).Exists == true)
                    new System.IO.DirectoryInfo(pathLogMail).Delete(true);

                new System.IO.DirectoryInfo(pathLogMail).Create();

                new FileInfo(pathMailLog).Create().Dispose();
            }


            if (To == From)
            {
                var noidungLog = System.IO.File.ReadAllText(pathMailLog).Replace("\r", "").Split("\n\n==================================================\n\n", StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < noidungLog.Length; i++)
                {
                     var check_body =Body.Replace("\r", "");
                    var check_log = noidungLog[i];

                    check_body = Regex.Replace(check_body, "\\d{2}\\/\\d{2}\\/\\d{4} \\d{2}:\\d{2}:\\d{2} (AM|PM)", "");
                    check_log = Regex.Replace(check_log, "\\d{2}\\/\\d{2}\\/\\d{4} \\d{2}:\\d{2}:\\d{2} (AM|PM)","");

                    if (check_body == check_log)
                        return;
                }

                var newLog = Body + "\r\n\r\n==================================================\r\n\r\n";

                System.IO.File.WriteAllText(pathMailLog, newLog);
            }

            var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Replace("[Encrypted_3275]", "").Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                From = StringMaHoaExtension.Decrypt(info[0], "32752262"); ;
                if (To == From || string.IsNullOrEmpty(To))
                To = StringMaHoaExtension.Decrypt(info[0], "32752262"); ;
                password = StringMaHoaExtension.Decrypt(info[1], "32752262");
            }

            // Ví dụ tài khoản Gmail của bạn đăng ký với tên Nguyễn Văn A, nhưng bạn lại sử dụng địa chỉ email này code C# để gửi tự động
            // email cho ai đó và họ sẽ thấy tên mới bạn đặt khi nhận thư của bạn (ví dụ Web Bán Sách) ...
            var fromAddress = new MailAddress(From, "My Web Play - Van Luat");

            var toAddress = new MailAddress(To, To);
            var sub = "";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(From, password),
                Timeout = 20000,
            };

            if (From != To)
            {
                sub = Subject;
                Subject = host+" Tin nhắn/email nội dung của My Web Play đến với bạn theo yêu cầu (nếu không vui lòng bỏ qua)";
            }

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = isBodyHtml
        })
            {
                smtp.Send(message);
            }

            if (string.IsNullOrEmpty(anotherToMail) == false)
            {
                using (var message = new MailMessage(fromAddress, fromAddress)
                {
                    Subject = sub + " ~|~ "+anotherToMail,
                    Body = Body,
                    IsBodyHtml = isBodyHtml
                })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
