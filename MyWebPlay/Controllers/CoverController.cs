using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Text;

namespace MyWebPlay.Controllers
{
    public class CoverController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoverController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Ee90d45ca0d59031d2a3b6dc488187c00()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 1)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[0];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E41fb870a2ab7c2470cb35f51171e20ee()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 2)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[1];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E8ecb5a3a2b4fdbda327335d19f3ca7fa()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 3)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[2];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E003e16661a80c9451f46587afb2ec5c3()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 4)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[3];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult Ef8e2d510133438f980423324078e0939()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 5)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[4];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E72fd1425ee00a7192a7261c5b45fcab2()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 6)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[5];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E86a31cf62149eaf28543a8a639d91309()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 7)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[6];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E8b5931c6ba81548e4b0a06e07fdd5282()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 8)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[7];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult Ee98b70f046cbdf12b3eee2d65e625373()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 9)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[8];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult E10f628a33fcf828bb57d497794a34094()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }

                    if (info[0] == "Play_EncodeUrl")
                    {
                        if (info[1] == "false")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Encode_Url")
                    {
                        if (info[3].Split("***", StringSplitOptions.RemoveEmptyEntries).Length < 10)
                            return RedirectToAction("Error", "Home");

                        ViewBag.Link = info[3].Split("***", StringSplitOptions.RemoveEmptyEntries)[9];
                        break;
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context: HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSessionTemp(IFormFile file, string? indexSaved)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            string content;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                content = await reader.ReadToEndAsync();
            }

            HttpContext.Session.SetString("save_session_tmp", content);

            var co = "";
            if (string.IsNullOrEmpty(indexSaved) == false)
            {
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", indexSaved + ".txt"), content);
                co = "(đã lưu vào file tương ứng - nếu có)";
            }

            return Ok(new { result = true, message = "Đã xử lý và đưa nội dung của bạn vào session thành công" + co + "." });
        }

        public ActionResult ShowMyHtml1(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "text/html");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "text/html");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "text/html");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "1.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "text/html");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult ShowMyHtml2(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "text/html");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "text/html");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "text/html");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "2.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "text/html");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult ShowMyHtml3(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "text/html");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "text/html");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "text/html");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "3.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "text/html");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult ShowMyHtml4(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "application/json");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "application/json");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "application/json");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "4.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "application/json");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult ShowMyHtml5(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "application/json");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "application/json");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "application/json");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "5.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "application/json");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult ShowMyHtml6(string path, string isRoot = "true")
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                if (isRoot == "true")
                {
                    var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, path));
                    return Content(noidung, "application/json");
                }
                else
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string noidung = reader.ReadToEnd();
                    return Content(noidung, "application/json");
                }
            }
            else
            {
                var content = HttpContext.Session.GetString("save_session_tmp");
                if (content != null && string.IsNullOrEmpty(content) == false)
                {
                    return Content(content, "application/json");
                }

                var fileTmp = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "SavedTempSession", "6.txt"));
                if (string.IsNullOrEmpty(fileTmp) == false)
                {
                    return Content(fileTmp, "application/json");
                }
            }

            return Content("Đã xảy ra lỗi.");
        }

        public ActionResult GetDateTimeServer(string? format = "")
        {
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
            var partDelayTime = delayTime.Split("#");
            var hourDL = partDelayTime[0].Replace("H", "");
            var minDL = partDelayTime[1].Replace("M", "");
            var secDL = partDelayTime[2].Replace("S", "");

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            return Ok(new { data = xuxu.ToString(format) });
        }

        public ActionResult GetDataResult()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
            if (System.IO.File.Exists(path) == false)
            {
                return Content("Dữ liệu không tồn tại.");
            }

            var noidung = FileExtension.ReadFile(path);
            return Content(noidung);
        }

        public ActionResult Error_Exception()
        {
            var check = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[24].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[24].Split("===")[1] == "DEBUG";
            var error = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"));
            // HttpContext.Session.Clear();
            HttpContext.Session.SetString("error_exception_log", error);
            if (error == null || error == "" || check == false)
            {
                return RedirectToAction("Error", "Home");
            }

            TempData["html_method_root"] = Request.Method;
            return View("Error_Exception", error.Replace("\r", "").Replace("\n", "<br />"));
        }

        [HttpPost]
        public ActionResult AddSession(IFormCollection f)
        {
            HttpContext.Session.SetString(f["key"].ToString(), f["value"].ToString());
            return Ok(new { result = "Đã xử lý thành công." });
        }

        public ActionResult EncryptOrDecrypt(string value, string type, string? key = "buivanluat-ADMIN3275")
        {
            var data = "";
            if (type == "0")
            {
                data = StringMaHoaExtension.Encrypt(value, key);
            }
            else
            {
                data = StringMaHoaExtension.Decrypt(value, key);
            }

            return Ok(new { data = data });
        }

        public ActionResult ChangeFirstUrl(string url, string ID, string Password)
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var password = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];
            var Id = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[0];

            var key = listSetting[60].Split("<3275>")[3];
            var oldUrl = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[20];
            var document = FileExtension.ReadFile(pth);
            if (StringMaHoaExtension.Decrypt(password, key) == Password && StringMaHoaExtension.Decrypt(Id, key) == ID)
            {
                document = document.Replace(oldUrl, url.TrimStart('/').Replace("/", "---"));
                FileExtension.WriteFile(pth, document);
                return Ok(new { message = "Đã thay đổi thành công. Tuy nhiên bạn có thể phải đợi thêm vài giờ để nó chính thức hoạt động !" });
            }

            return Ok(new { message = "Đã xảy ra lỗi." });
        }

        public ActionResult ReadFileExternal(string path)
        {
            var noidung = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal", path));
            return Ok(new { data = noidung });
        }

        public ActionResult ReadCurrentLogMail()
        {
            if (HttpContext.Session.GetString("32752262") != "read_current_log_mail")
            {
                return Ok(new { result = false, message = "Lỗi xác thực, vui lòng kiểm tra lại." });
            }

            var pathLogMail = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "MailLog")).GetFiles();
            var noidung = new StringBuilder();
            foreach (var file in pathLogMail)
            {
                noidung.AppendLine();
                noidung.AppendLine();
                noidung.AppendLine();
                noidung.Append(FileExtension.ReadFile(pathLogMail + "/" + file.Name + ".txt"));
                noidung.AppendLine();
                noidung.AppendLine();
                noidung.AppendLine();
            }

            return Content(noidung.ToString(), "text/html");
        }

        public ActionResult ReadCurrentError()
        {
            if (HttpContext.Session.GetString("32752262") != "read_current_error")
            {
                return Ok(new { result = false, message = "Lỗi xác thực, vui lòng kiểm tra lại." });
            }

            var read = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"));
            return Content(read, "text/html");
        }

        public async Task<string> PostDataWithQueryParams(string url, Dictionary<string, string> queryParams)
        {
            using (HttpClient client = new HttpClient())
            {
                var uriBuilder = new UriBuilder(url)
                {
                    Query = string.Join("&", queryParams.Select(p => $"{p.Key}={p.Value}"))
                };

                HttpResponseMessage response = await client.PostAsync(uriBuilder.ToString(), null);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Request failed with status: " + response.StatusCode);
                }
            }
        }

        public async Task<string> PostDataWithFormBody(string url, Dictionary<string, string> formData)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(formData);

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Request failed with status: " + response.StatusCode);
                }
            }
        }

        public async Task<ActionResult> SecureCallAPI(string url, string[] keys, string[] values, string type = "0", string encrypt="0", string format= "application/json")
        {
            if (keys.Length != values.Length)
            {
                return Ok(new { error = "Đã xảy ra lỗi, vui lòng thử lại sau..." });
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = FileExtension.ReadFile(path);

            var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
            var keyEncrypt = listSetting[60].Split("<3275>")[3];

            url = (encrypt == "0") ? url : StringMaHoaExtension.Decrypt(url, keyEncrypt);

            var result = string.Empty;
            var formData = new Dictionary<string, string>();
            for (var i = 0; i < keys.Length; i++)
            {
                var key = (encrypt == "0") ? keys[i] : StringMaHoaExtension.Decrypt(keys[i], keyEncrypt);
                var value = (encrypt == "0") ? values[i] : StringMaHoaExtension.Decrypt(values[i], keyEncrypt);
                formData.Add(key, value);
            }

            if (type == "0")
            {
                result = await PostDataWithFormBody(url, formData);
            }
            else if (type == "1")
            {
                result = await PostDataWithQueryParams(url, formData);
            }

            if (result.ToLower().Contains("html") || result.ToLower().Contains("body")) result = "Một kết quả HTML không thể hiển thị...";
            return Content(result, format);
        }

        [HttpPost]
        public ActionResult CheckingIsAdmin(string ip = "")
        {
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Session.GetString("userIP");
            }

            var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
            var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];
            if (valuePam == ip)
            return Ok(new { result = true});
            return Ok(new { result = false });
        }

        public ActionResult CustomerPagePlay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerPageShow(string code)
        {
            var nd = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "CustomerPage_Play.txt")).Replace("\r", "").Split("\n#3275#\n");

            var data = "";

            for (var i = 0; i < nd.Length; i++)
            {
                var ndx = nd[i].Split("\n||\n");
                if (ndx[0] == code)
                {
                    data += ndx[1];
                }
            }

            if (data == "")
                return Ok(new { result = false });
            return Ok(new { result = true, html = data });
        }

        public ActionResult DownloadCopyAdminSetting()
        {
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            var files = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Copied")).GetFiles();
            var index = 0;
            foreach (var file in files)
            {
                index++;
                Calendar xz = CultureInfo.InvariantCulture.Calendar;
                var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Copied", file.Name);
                var nd = FileExtension.ReadFile(path2);
                SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] DOWNLOAD COPY FILES ADMIN SETTING In " + xuxuz + " === { "+"["+index+"/"+files.Count()+"] "+ file.Name+" }", nd, "teinnkatajeqerfl");
            }

           return Content("Quá trình xử lý đang diễn ra, vui lòng đợi một lát...", "text/html");
        }

        public ActionResult DownloadCopyHTMLPlay()
        {
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            var files = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Copied_HTML")).GetFiles();
            var index = 0;
            foreach (var file in files)
            {
                index++;
                Calendar xz = CultureInfo.InvariantCulture.Calendar;
                var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Copied_HTML", file.Name);
                var nd = FileExtension.ReadFile(path2);
                SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] DOWNLOAD COPY FILES HTML PLAY In " + xuxuz + " === { " + "[" + index + "/" + files.Count() + "] "+ file.Name + " }", nd, "teinnkatajeqerfl");
            }

            return Content("Quá trình xử lý đang diễn ra, vui lòng đợi một lát...", "text/html");
        }

        [HttpPost]
        public ActionResult ExectFilePower(IFormCollection f)
        {
            try
            {
                var ID = f["ID_Admin"].ToString();
                var Pass = f["Pass_Admin"].ToString();
                var path = f["Path"].ToString();
                var ten = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4];
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", ten);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var password = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];
                var Id = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[0];

                var key = listSetting[60].Split("<3275>")[3];
                if (StringMaHoaExtension.Decrypt(password, key) != Pass ||
                    StringMaHoaExtension.Decrypt(Id, key) != ID
                    || path.Contains(ten)
                    || path.Contains("SecureSettingAdmin.txt")) return Ok(new { result = true });

                var type = f["Type"].ToString();
                var action = f["Action"].ToString();
                var content = f["Content"].ToString();

                var pathFull = type == "ROOT"
                    ? Path.Combine(_webHostEnvironment.WebRootPath, path)
                    : Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), path);
               
                switch(action)
                {
                    case "WRITE":
                        FileExtension.WriteFile(pathFull, content);
                        return Ok(new { result = true });

                    case "REMOVE":
                        System.IO.File.Delete(pathFull);
                        return Ok(new { result = true });

                    case "READ":
                        var data = FileExtension.ReadFile(pathFull);
                        return Ok(new { noidung = data });

                    case "LIST-FILE":
                        var listFile = new DirectoryInfo(pathFull).GetFiles();
                        return Ok(new { list = string.Join("\n", listFile.ToList()) });

                    case "DOWNLOAD":
                        if (type == "ROOT")
                            return Ok(new { link = pathFull });

                        var nu = pathFull.Split("/").Reverse().ToArray();
                       var nam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "OtherFile" , DateTime.Now.ToString().Replace("/","").Replace(":","") + "_" + nu[0]);
                       System.IO.File.Copy(pathFull, nam, overwrite: true);
                       return Ok(new { link = nam });
                }
                return Ok(new { result = true });
            }
            catch
            {
                return Ok(new { result = false });
            }
        }
    }
}