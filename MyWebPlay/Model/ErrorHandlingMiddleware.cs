using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using MyWebPlay.Extension;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Globalization;

namespace MyWebPlay.Model
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            var json = await SerializeHttpContextAsync(context);
            try
            {
                if (context.Request.QueryString.ToString().Contains("show-error") == false)
                {
                    var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungWW = FileExtension.ReadFile(pathWW);

                    if (noidungWW.Contains("[ENCRYPT]"))
                    {
                        noidungWW = noidungWW.Replace("[ENCRYPT]", "");
                        noidungWW = StringMaHoaExtension.Decrypt(noidungWW, "32752262");
                        FileExtension.WriteFile(pathWW, noidungWW);
                    }

                    var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                    var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];


                    var yes_log = true;
                    var ip = SetUserIPClient(context);

                    if (string.IsNullOrEmpty(ip) == false)
                    {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "ListIPComeToHereTheFirst.txt");

                    var listIP = FileExtension.ReadFile(path);

                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
                    var partDelayTime = delayTime.Split("#");
                    var hourDL = partDelayTime[0].Replace("H", "");
                    var minDL = partDelayTime[1].Replace("M", "");
                    var secDL = partDelayTime[2].Replace("S", "");
                    var xuxu = x.AddHours(DateTime.UtcNow, 7);
                    if (hourDL.Contains("-"))
                    {
                        xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(hourDL));
                    }

                    if (minDL.Contains("-"))
                    {
                        xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(minDL));
                    }

                    if (secDL.Contains("-"))
                    {
                        xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu.AddSeconds(int.Parse(secDL));
                    }

                        if (listIP.Contains(ip) == false)
                        {
                            // Random number and character string

                            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            var stringChars = new char[10];
                            var random = new Random();

                            for (int i = 0; i < stringChars.Length; i++)
                            {
                                stringChars[i] = chars[random.Next(chars.Length)];
                            }

                            var finalString = new String(stringChars);

                            if (string.IsNullOrEmpty(listIP))
                                FileExtension.WriteFile(path, ip + "\t" + xuxu + "\t" + "[" + finalString + "]");
                            else
                                FileExtension.WriteFile(path, listIP + "\r\n" + ip + "\t" + xuxu + "\t" + "[" + finalString + "]");
                        }

                    if (context.Session.GetString("admin-userIP") != null)
                    {
                        if (valuePam == MD5.CreateMD5(context.Session.GetString("admin-userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                    }

                    if (context.Session.GetString("userIP") != null)
                    {
                        if (valuePam == MD5.CreateMD5(context.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                    }

                    if (valuePam == MD5.CreateMD5(ip)) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;

                    if (infoXWW[1] == "true" && context.Request.Path.ToString().Contains("ReloadIPComeHere") == false && (yes_log || context.Session.GetString("NoAdmin_YesLog") == "true"))
                    {
                        var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                        var noidungS = FileExtension.ReadFile(pathS);

                        var data = await GetRequestFormAsync(context);
                        var info = string.Format("Method :{0} ||| Path : {1} ||| Refferrer : {2} ||| User-agent : {3} ||| sec-ch-ua-platform : {4} ||| sec-ch-ua : {5} ||| status code : {6} ||| query : {7} ||| data : {8} ||| file : {9}",
                            context.Request.Method,
                            context.Request.Path,
                            context.Request.GetTypedHeaders().Referer,
                            context.Request.Headers["User-Agent"].ToString(),
                            context.Request.Headers["Sec-CH-UA-Platform"].ToString(),
                            context.Request.Headers["Sec-CH-UA"].ToString(),
                            context.Response.StatusCode,
                            context.Request.QueryString,
                            "(" + string.Join("  =&&= ", data.Where(it => it.Key.Contains("RequestVerificationToken") == false).Select(kv =>
                            {
                                string value = kv.Value ?? ""; // Xử lý nếu null
                                value = value.Replace("\r", "").Replace("\n", "\\n"); // Thay thế ký tự xuống dòng

                                return (value.Length < 200) ? $"{kv.Key} :=: {value}" : $"{kv.Key} :=: {value.Substring(0, 200)}...";
                            })) + ")",
                            "(" + GetRequestFiles(context) + ")");
                        var noidungZ = noidungS + "\n" + SetUserIPClient(context) + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                            + "\t[DEBUG : " + info + "]";

                        var khao = noidungS.Replace("\r", "").Split("\n");
                        if (context.Request.Path.ToString().Contains("EncryptPasswordByKey_Call") == false
                            && context.Request.Path.ToString().Contains("ReloadIPComeHere") == false)
                        {
                            FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
                        }
                    }

                    if (yes_log || context.Session.GetString("NoAdmin_YesLog") == "true")
                    {
                        var lockedApp = listSettingSWW[43].Split("<3275>");
                        if (lockedApp[3] != "[NULL]")
                        {
                            var xi = context.Request.Path;
                            if (xi == "" || xi == "/" || xi == null) xi = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                            if (lockedApp[3].Contains(xi))
                            {
                                context.Response.Redirect("/Home/Error#debug");
                            }
                        }
                    }

                    var onEncryptSetting = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[9];
                    if (onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_ON")
                    {
                        if (noidungWW.Contains("[ENCRYPT]") == false)
                        {
                            noidungWW = StringMaHoaExtension.Encrypt(noidungWW, "32752262");
                            FileExtension.WriteFile(pathWW, "[ENCRYPT]" + noidungWW);
                        }
                    }

                    var fileOption = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[3];
                    if (fileOption == "file_TAT")
                    {
                        if (context.Request.HasFormContentType && context.Request.Form?.Files?.Any() == true)
                        {
                            if (context.Request.Form.Files.All(file => file.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) == false)
                            {
                                throw new Exception("Không thể xử lý file do bị giới hạn tải lên server !");
                            }
                        }
                    }
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                var req = context.Request.Path;

                var urlDefault = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[20].Split("--")[1];
                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/"+urlDefault;

                var errx = (context.Session.GetString("hanhdong_3275") != null) ? context.Session.GetString("hanhdong_3275") : string.Empty;
                context.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + context.Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx + "\n\n\n##########\n\n\n" + json);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + context.Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx + "\n\n\n##########\n\n\n" + json);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && context.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + context.Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((context.Session.GetString("admin-userIP") != null) ? context.Session.GetString("admin-userIP") : context.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + context.Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx + "\n\n\n##########\n\n\n" + json, "teinnkatajeqerfl", context:context);
                }

                context.Response.Redirect("/Home/Error?exception=" +  ((((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(yes)"));
            }
        }

        public string SetUserIPClient(HttpContext context)
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var check = false;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "All_People" && info[1] == "true")
                {
                    check = true;
                    break;
                }
            }

            string ipAddress = string.Empty;
            if (check)
            {
                //using (var client = new HttpClient())
                //{
                //    var response = await client.GetAsync("https://api.ipify.org?format=text");
                //    ipAddress = await response.Content.ReadAsStringAsync();
                //}

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = context.Request.Headers["X-Forwarded-For"];
                }

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = context.Connection.RemoteIpAddress.ToString();
                }
            }

            return ipAddress;
        }

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
    }
}
