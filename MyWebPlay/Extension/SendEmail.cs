using System.Net.Mail;
using System.Net;

namespace MyWebPlay.Extension
{
    public static class SendEmail
    {
        public static void SendMail2Step(string From, string To, string Subject, string Body, string password)
        {
            // Ví dụ tài khoản Gmail của bạn đăng ký với tên Nguyễn Văn A, nhưng bạn lại sử dụng địa chỉ email này code C# để gửi tự động
            // email cho ai đó và họ sẽ thấy tên mới bạn đặt khi nhận thư của bạn (ví dụ Web Bán Sách) ...
            var fromAddress = new MailAddress(From, "My Web Play - Save Text Note File");

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
