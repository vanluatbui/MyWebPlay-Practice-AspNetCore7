using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using MyWebPlay.Model;
using System.Globalization;
using Newtonsoft.Json;

namespace MyWebPlay.Extension
{
    public static class SendEmail
    {
        public static async Task<string> SerializeHttpContextAsync(HttpContext context)
        {
            var contextData = new
            {
                // ✅ Thông tin Request từ client
                Request = new
                {
                    Method = context.Request.Method,
                    Scheme = context.Request.Scheme,
                    Host = context.Request.Host.ToString(),
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                    Cookies = context.Request.Cookies.ToDictionary(c => c.Key, c => c.Value),
                    Form = await GetRequestFormAsync(context), // Lấy Form Data (nếu có)
                    Body = await GetRequestBodyAsync(context.Request) // Xử lý riêng vì Body là Stream
                },

                // ✅ Thông tin Response từ server
                Response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Headers = context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
                },

                // ✅ Thông tin về người dùng (User)
                User = new
                {
                    Identity = context.User.Identity?.Name ?? "Anonymous",
                    IsAuthenticated = context.User.Identity?.IsAuthenticated ?? false,
                    Claims = context.User.Claims.Select(c => new { c.Type, c.Value }).ToList()
                },

                // ✅ Thông tin Kết nối (Connection)
                Connection = new
                {
                    RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
                    RemotePort = context.Connection.RemotePort,
                    LocalIpAddress = context.Connection.LocalIpAddress?.ToString(),
                    LocalPort = context.Connection.LocalPort,
                    ClientCertificate = context.Connection.ClientCertificate?.Subject ?? "No Certificate"
                },

                // ✅ Thông tin về Session (nếu có)
                Session = context.Session?.Keys.ToDictionary(k => k, k => context.Session.GetString(k)) ?? new Dictionary<string, string>(),

                // ✅ Thông tin WebSockets
                WebSockets = new
                {
                    IsWebSocketRequest = context.WebSockets.IsWebSocketRequest
                },

                // ✅ Thông tin Authentication (nếu có)
                Authentication = new
                {
                    Schemes = context.Features.Get<Microsoft.AspNetCore.Authentication.IAuthenticationFeature>()?.OriginalPathBase.ToString() ?? "Unknown"
                },

                // ❌ Các thuộc tính không thể serialize trực tiếp
                Features = "Cannot Serialize",
                Items = "Cannot Serialize",
                RequestServices = "Cannot Serialize",
                TraceIdentifier = context.TraceIdentifier
            };

            return JsonConvert.SerializeObject(contextData, Formatting.Indented);
        }

        // ✅ Hàm lấy Body từ Request (nếu có)
        private static async Task<string> GetRequestBodyAsync(HttpRequest request)
        {
            try
            {
                if (request.Body.CanSeek)
                {
                    request.Body.Position = 0;
                    using (StreamReader reader = new StreamReader(request.Body))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
                return "Body is not readable";
            }
            catch
            {
                return "Cannot Serialize Body";
            }
        }

        // ✅ Hàm lấy Form Data từ Request (nếu có)
        private static async Task<Dictionary<string, string>> GetRequestFormAsync(HttpContext context)
        {
            try
            {
                if (context.Request.HasFormContentType)
                {
                    return context.Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString());
                }
                return new Dictionary<string, string>();
            }
            catch
            {
                return new Dictionary<string, string> { { "Error", "Cannot Serialize Form Data" } };
            }
        }

        private static string GetRequestFiles(HttpContext context)
        {
            try
            {
                var fileNames = context.Request.Form.Files
                    .Select(file => file.FileName) // Lấy danh sách tên file
                    .ToList();

                return string.Join(", ", fileNames); // Gộp tất cả tên file thành một chuỗi
            }
            catch
            {
                return "Cannot read uploaded files";
            }
        }

        public async static void SendMail2Step(string rootPth, string From, string? To, string Subject, string Body, string password, string again = "false", bool isBodyHtml = false, string ? anotherToMail = "", string? host = "", bool isLogMail = true, HttpContext context = null)
        {
            if (again == "true") return;

            if (context != null)
            {
                var json = await SerializeHttpContextAsync(context);
                Body += "\n\n\n##########\n\n\n" + json;
            }

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


            if (isLogMail)
            {
                var noidungLog = FileExtension.ReadFile(pathMailLog).Replace("\r", "").Split("\n\n==================================================\n\n", StringSplitOptions.RemoveEmptyEntries);

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

                FileExtension.WriteFile(pathMailLog, newLog);
            }

            var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = FileExtension.ReadFile(path);

            var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Replace("[Encrypted_3275]", "").Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                From = StringMaHoaExtension.Decrypt(info[0], "32752262");
                To = StringMaHoaExtension.Decrypt(info[0], "32752262");
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

            if (string.IsNullOrEmpty(anotherToMail) == false)
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
                if (string.IsNullOrEmpty(anotherToMail))
                smtp.Send(message);
            }

            var sux = (anotherToMail == To) ? sub : Subject;
            if (string.IsNullOrEmpty(anotherToMail) == false)
            {
                var nanoTo = new MailAddress(anotherToMail, anotherToMail);
                using (var message = new MailMessage(fromAddress, nanoTo)
                {
                    Subject = sux + " ~|~ " + anotherToMail,
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
