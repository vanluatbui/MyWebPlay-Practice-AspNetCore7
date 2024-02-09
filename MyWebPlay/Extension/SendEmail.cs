using System.Net.Mail;
using System.Net;

namespace MyWebPlay.Extension
{
    public static class SendEmail
    {
        public static void SendMail2Step(string rootPth, string From, string To, string Subject, string Body, string password)
        {
            var path = Path.Combine(rootPth, "Admin/SettingABC_DarkBVL.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[36].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                From = info[0];
                To = info[0];
                password = info[1];
            }

            // Ví dụ tài khoản Gmail của bạn đăng ký với tên Nguyễn Văn A, nhưng bạn lại sử dụng địa chỉ email này code C# để gửi tự động
            // email cho ai đó và họ sẽ thấy tên mới bạn đặt khi nhận thư của bạn (ví dụ Web Bán Sách) ...
            var fromAddress = new MailAddress(From, "My Web Play - Van Luat");

            var toAddress = new MailAddress(To, To);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(From, password),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Subject,
                Body = Body,
        })
            {
                smtp.Send(message);
            }
        }
    }
}
