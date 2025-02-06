using Microsoft.AspNetCore.Hosting;
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
            try
            {

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                if (noidungWW.Contains("[ENCRYPT]"))
                {
                    noidungWW = noidungWW.Replace("[ENCRYPT]", "");
                    noidungWW = StringMaHoaExtension.Decrypt(noidungWW, "32752262");
                    System.IO.File.WriteAllText(pathWW, noidungWW);
                }

                var json = JsonConvert.SerializeObject(context, Formatting.Indented);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam).Split("<>")[0];


                var yes_log = true;
                var ip = SetUserIPClientWhenAPI(context);

                if (context.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(context.Session.GetString("admin-userIP"))) yes_log = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (context.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(context.Session.GetString("userIP"))) yes_log = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (valuePam == MD5.CreateMD5(ip)) yes_log = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;

                if (infoXWW[1] == "true" && context.Request.Path.ToString().Contains("ReloadIPComeHere") == false && (yes_log || context.Session.GetString("NoAdmin_YesLog") == "true"))
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = System.IO.File.ReadAllText(pathS);

                    var noidungZ = noidungS + "\n" + context.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                        +  "\t[DEBUG]\t\t\t"+json;

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                if (yes_log || context.Session.GetString("NoAdmin_YesLog") == "true")
                {
                    var lockedApp = listSettingSWW[43].Split("<3275>");
                    if (lockedApp[3] != "[NULL]")
                    {
                        var xi = context.Request.Path;
                        if (xi == "" || xi == "/" || xi == null) xi = "/" + System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                        if (lockedApp[3].Contains(xi))
                        {
                            context.Response.Redirect("/Home/Error#debug");
                        }
                    }
                }

                var onEncryptSetting = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[9];
                if (onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_ON")
                {
                    if (noidungWW.Contains("[ENCRYPT]") == false)
                    {
                        noidungWW = StringMaHoaExtension.Encrypt(noidungWW, "32752262");
                        System.IO.File.WriteAllText(pathWW, "[ENCRYPT]" + noidungWW);
                    }
                }

                  await _next(context);
            }
            catch (Exception ex)
            {
                var req = context.Request.Path;

                var urlDefault = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[20].Split("--")[1];
                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/"+urlDefault;

                var errx = (context.Session.GetString("hanhdong_3275") != null) ? context.Session.GetString("hanhdong_3275") : string.Empty;
                context.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + context.Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + context.Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && context.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + context.Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((context.Session.GetString("admin-userIP") != null) ? context.Session.GetString("admin-userIP") : context.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + context.Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }

                context.Response.Redirect("/Home/Error?exception=yes");
            }
        }

        public string SetUserIPClientWhenAPI(HttpContext context)
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var check = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "All_People" && info[1] == "true") check++;
                if (info[0] == "External_Post" && info[1] == "true") check++;
                if (info[0] == "Get_Blocked" && info[1] == "true") check++;
            }

            string ipAddress = string.Empty;
            if (check == 3)
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
    }
}
