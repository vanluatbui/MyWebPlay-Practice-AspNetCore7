﻿using System.Net.Mail;
using System.Net;

namespace MyWebPlay.Extension
{
    public static class SendEmail
    {
        public static void SendMail2Step(string rootPth, string From, string? To, string Subject, string Body, string password, string again = "false", bool isBodyHtml = false)
        {
            if (again == "true") return;

            var path = Path.Combine(rootPth,"Admin", System.IO.File.ReadAllText(Path.Combine(rootPth, "Admin", "SecureSettingAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
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
                Subject = "Tin nhắn/email nội dung của My Web Play đến với bạn theo yêu cầu (nếu không vui lòng bỏ qua)";
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

            if (From != To)
            {
                using (var message = new MailMessage(fromAddress, fromAddress)
                {
                    Subject = sub + " ~|~ "+toAddress,
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
