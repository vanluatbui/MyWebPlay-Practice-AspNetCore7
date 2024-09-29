using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using System.Reflection.Metadata.Ecma335;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Asn1;
using System.IO;

namespace MyWebPlay.Controllers
{
    public partial class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult DeleteFirstIP()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "ListIPComeToHereTheFirst.txt");

                var listIP = System.IO.File.ReadAllText(path);

                System.IO.File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        public ActionResult DeleteAll_ThisIPRegist()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect", "ListIPOnWebPlay.txt");

                var listIP = System.IO.File.ReadAllText(path);

                System.IO.File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        public ActionResult DeleteAll_ThisIPLocked()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect", "ListIPLock.txt");

                var listIP = System.IO.File.ReadAllText(path);

                System.IO.File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        public ActionResult DeleteAll_ThisIPLockedClient()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect", "LockedIPClient.txt");

                var listIP = System.IO.File.ReadAllText(path);

                System.IO.File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        [HttpPost]
        public ActionResult ReplaceUpload(List<IFormFile> txtFile, int txtChonX)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var fix = "";
                var ii = 0;
                foreach (var item in txtFile)
                {
                    fix += string.Format("txtFile[{0}] : {1}\n", ii, item.FileName);
                    ii++;
                }
                fix += string.Format("txtChonX : {0}\n", txtChonX);
                HttpContext.Session.SetString("hanhdong_3275", fix);

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                switch (txtChonX)
                {
                    case 0:
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "background");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".jpg") == false)
                                continue;

                            var filename = (i + 1) + ".jpg";

                            if (new FileInfo(path + "/" + filename).Exists)
                                new FileInfo(path + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);

                            }
                        }
                        break;

                    //-----------------------

                    case 1:
                        var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "Image_Play");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".gif") == false)
                                continue;

                            var filename = "error.gif";

                            if (new FileInfo(path1 + "/" + filename).Exists)
                                new FileInfo(path1 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path1 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    //-----------------------

                    case 2:
                        var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "Image_Play");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".gif") == false)
                                continue;

                            var filename = "waiting.gif";

                            if (new FileInfo(path2 + "/" + filename).Exists)
                                new FileInfo(path2 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path2 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    //-----------------------

                    case 3:
                        var path3 = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".png") == false)
                                continue;

                            var filename = "imagex.png";

                            if (new FileInfo(path3 + "/" + filename).Exists)
                                new FileInfo(path3 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path3 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    //-----------------------

                    case 4:
                        var path4 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "Random_Tab");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".txt") == false)
                                continue;

                            var filename = "RandomTab_Tittle.txt";

                            if (new FileInfo(path4 + "/" + filename).Exists)
                                new FileInfo(path4 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path4 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    //-----------------------

                    case 5:
                        var path5 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "Random_Tab");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".txt") == false)
                                continue;

                            var filename = "RandomTab_Image.txt";

                            if (new FileInfo(path5 + "/" + filename).Exists)
                                new FileInfo(path5 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path5 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    //-----------------------

                    case 6:
                        var path6 = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "Video_Youtube");
                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            if (txtFile[i].FileName.EndsWith(".txt") == false)
                                continue;

                            var filename = "randomlink.txt";

                            if (new FileInfo(path6 + "/" + filename).Exists)
                                new FileInfo(path6 + "/" + filename).Delete();

                            using (Stream fileStream = new FileStream(path6 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }

                            break;
                        }
                        break;

                    case 7:
                        var path7 = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara");

                        if (new System.IO.DirectoryInfo(path7).Exists)
                            new System.IO.DirectoryInfo(path7).Delete(true);

                        new System.IO.DirectoryInfo(Path.Combine(path7)).Create();

                        for (int i = 0; i < txtFile.Count; i++)
                        {
                            var filename = txtFile[i].FileName;

                            using (Stream fileStream = new FileStream(path7 + "/" + filename, FileMode.Create))
                            {
                                txtFile[i].CopyTo(fileStream);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        public ActionResult RefreshTheFistIP()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "ListIPComeToHereTheFirst.txt");

                var listIP = System.IO.File.ReadAllText(path);

                System.IO.File.WriteAllText(path, "");

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                string host = "{" + Request.Host.ToString() + "}"
                  .Replace("http://", "")
                  .Replace("https://", "")
                  .Replace("/", "");

                if (string.IsNullOrEmpty(listIP) == false)
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", host + "[ADMIN] Báo cáo  danh sách các IP user đã ghé thăm lần đầu tiên có ghi lại (tất cả/có thể chưa đầy đủ) In " + xuxu, listIP, "teinnkatajeqerfl");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        public ActionResult DongMoFileStatus()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth);
                var onoff = nd.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var fileStatus = nd.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                if (fileStatus == "file_MO")
                {
                    if (Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "file")) /*&& Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")) == false*/ )
                        Directory.Move(Path.Combine(_webHostEnvironment.WebRootPath, "file"), Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose"));
                    nd = nd.Replace("file_MO", "file_TAT");
                    System.IO.File.WriteAllText(pth, nd);
                }
                else if (fileStatus == "file_TAT")
                {
                    if (Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")) /*&& Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "file")) == false*/ )
                        Directory.Move(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose"), Path.Combine(_webHostEnvironment.WebRootPath, "file"));
                    nd = nd.Replace("file_TAT", "file_MO");
                    System.IO.File.WriteAllText(pth, nd);
                }

                return RedirectToAction("SettingXYZ_DarkAdmin");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult LoginSettingAdmin(bool? locked)
        {
            try
            {
                if (HttpContext.Session.GetString("admin-userIP") == null)
                {
                    TempData["WantToGetUserIP"] = "true";
                }
                else
                {
                    TempData["WantToGetUserIP"] = "false";
                }


                if (locked == true)
                {
                    var pamX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                    System.IO.File.WriteAllText(pamX, string.Empty);
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (HttpContext.Session.GetString("show-setting") == "true")
                {
                    TempData["show-setting"] = "true";
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (string.IsNullOrEmpty(valuePam))
                {
                    TempData["giu_dang_nhap"] = "NO";
                }
                else
                {
                    TempData["giu_dang_nhap"] = "YES";
                }

                var yes_log = true;

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[GET]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                HttpContext.Session.Remove("open-admin");
                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                }

                if (HttpContext.Session.GetString("xacthuc2buoc-ADMIN") != null)
                {
                    TempData["xacthuc"] = "true";
                }
                else
                {
                    TempData["xacthuc"] = "false";
                }

                var pathSS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/Setting_Status.txt");
                var noidungSS = System.IO.File.ReadAllText(pathSS);
                ViewBag.SettingStatus = noidungSS;

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";

                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var infoYA = listSetting[56].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoYA[0] == "HTML_Visible")
                {
                    TempData["HTML-visible"] = infoYA[3];
                }

                SettingAdmin settingAdmin = new SettingAdmin();
                settingAdmin.Topics = new List<SettingAdmin.Topic>();

                var dem = 0;
                for (int i = 0; i < listSetting.Length; i++)
                {
                    if (i >= 33 && i <= 44 || i == 50 || i == 52 || i == 51 || i == 55 || i == 56 || i == 58 || i == 60) continue;

                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    settingAdmin.Topics.Add(new SettingAdmin.Topic(info[0], info[2], bool.Parse(info[1])));
                    dem++;
                }

                TempData["count-setting"] = dem;

                HttpContext.Session.Remove("adminSetting");
                var sam = settingAdmin.Topics[23].NoiDung;
                var span = "";
                Regex regExp = new Regex("<@@.*@>");
                foreach (Match match in regExp.Matches(sam))
                {
                    span += match.Value;
                    break;
                }

                if (span != "")
                {
                    var so_span = span.Replace("<@@", "").Replace("@@>", "");
                    settingAdmin.Topics[23].NoiDung = settingAdmin.Topics[23].NoiDung.Replace(span, "<input required min=\"0\" type=\"number\" readonly style=\"width: 5em\" name=\"txtDataThan\" value=\"" + so_span + "\" />");
                }
                return View(settingAdmin);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

            //return View();
        }

        [HttpPost]
        public ActionResult LoginSettingAdmin(IFormCollection f)
        {
            try
            {
                if (HttpContext.Session.GetString("admin-userIP") == null)
                {
                    TempData["WantToGetUserIP"] = "true";
                }
                else
                {
                    TempData["WantToGetUserIP"] = "false";
                }


                var pamX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePamX = System.IO.File.ReadAllText(pamX);

                //var adminIP = HttpContext.Session.GetString("admin-userIP");
                //if (adminIP != null)
                //{
                //    if (valuePamX == MD5.CreateMD5(adminIP))
                //    {
                //        HttpContext.Session.SetString("adminSetting", "true");
                //        HttpContext.Session.Remove("xacthuc2buoc-ADMIN");
                //        return RedirectToAction("SettingXYZ_DarkAdmin", "Admin");
                //    }
                //}

                if (string.IsNullOrEmpty(valuePamX) == false)
                {
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }
                HttpContext.Session.SetString("hanhdong_3275", fix);
                var pthX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                HttpContext.Session.Remove("adminSetting");

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var infoYA = listSetting[56].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoYA[0] == "HTML_Visible")
                {
                    TempData["HTML-visible"] = infoYA[3];
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var password = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];
                var ID = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[0];

                var key = listSetting[60].Split("<3275>")[3];

                if (listSetting[61].Split("<3275>")[1] == "false")
                {
                    if (StringMaHoaExtension.Decrypt(password, key) == f["txtPassword"].ToString() && StringMaHoaExtension.Decrypt(ID, key) == f["txtID"].ToString())
                    {
                        HttpContext.Session.SetString("adminSetting", "true");

                        var userIP = HttpContext.Session.GetString("admin-userIP");
                        if (userIP != null && userIP != "")
                        {
                            Calendar xi = CultureInfo.InvariantCulture.Calendar;
                            var xuxuz = xi.AddHours(DateTime.UtcNow, 7);
                            var text = MD5.CreateMD5(userIP);
                            var pamXD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                            System.IO.File.WriteAllText(pamXD, text);
                        }
                        else
                        {
                            return RedirectToAction("LoginSettingAdmin", "Admin");
                        }
                    }
                }
                else
                {
                    if (StringMaHoaExtension.Decrypt(password, key) == f["txtPassword"].ToString() && StringMaHoaExtension.Decrypt(ID, key) == f["txtID"].ToString())
                    {
                        var backupCode = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[18].Replace("SKIP_TWOSTEP_SETTING_ADMIN_WITH_BACKUP_CODE-", "");
                        var useBackupAndNotSendMail = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[19] == "USE_BACKUPCODE_AND_NOT_SEND_MAIL_FOR_SETTING_ADMIN_TWO_STEP_ON");

                        if (useBackupAndNotSendMail && backupCode != "[NULL]")
                        {
                            HttpContext.Session.SetString("xacthuc2buoc-ADMIN", "blocked-code");
                        }
                       
                        
                        if ((HttpContext.Session.GetString("xacthuc2buoc-ADMIN") != null && HttpContext.Session.GetString("xacthuc2buoc-ADMIN") != "blocked-code") || (useBackupAndNotSendMail && backupCode != "[NULL]"))
                        {
                            var xt = HttpContext.Session.GetString("xacthuc2buoc-ADMIN");
                            var xs = f["txtXacMinh"].ToString();

                            if (backupCode != "[NULL]")
                            {
                                backupCode = StringMaHoaExtension.Decrypt(backupCode, "32752262");
                            }

                            if ((xs == xt || xs == backupCode) && xs != "blocked-code")
                            {
                                HttpContext.Session.SetString("adminSetting", "true");

                                var userIP = HttpContext.Session.GetString("admin-userIP");
                                if (userIP != null && userIP != "")
                                {
                                    Calendar xi = CultureInfo.InvariantCulture.Calendar;
                                    var xuxuz = xi.AddHours(DateTime.UtcNow, 7);
                                    var text = MD5.CreateMD5(userIP);
                                    var pamXD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                                    System.IO.File.WriteAllText(pamXD, text);
                                }
                                else
                                {
                                    return RedirectToAction("LoginSettingAdmin", "Admin");
                                }
                                HttpContext.Session.Remove("xacthuc2buoc-ADMIN");
                            }
                        }
                        else
                        {
                            string host = "{" + Request.Host.ToString() + "}"
                              .Replace("http://", "")
                              .Replace("https://", "")
                              .Replace("/", "");

                            Calendar x = CultureInfo.InvariantCulture.Calendar;

                            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                            var r = new Random();

                            var so = r.Next(100000, 999999);

                            HttpContext.Session.SetString("xacthuc2buoc-ADMIN", so.ToString());

                            var noidung = "Mã xác minh hai bước của bạn lúc này là : <b style=\"color:red\">" + so + "</b> (hãy nhanh chóng sử dụng trước khi hết hạn).<br />Vui lòng không chia sẻ cho bất kỳ ai và mã này chỉ được sử dụng một lần.<br />Nếu bạn không yêu cầu điều này, xin hãy bỏ qua và đừng làm gì cả.<br />Trân trọng cảm ơn !<br /><br />@@ MY WEB PLAY - ADMIN SETTING @@\n";

                            SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                              "mywebplay.savefile@gmail.com", host + "<>" + HttpContext.Session.GetString("admin-userIP") + "<>" + "[ADMIN] Thiết lập mã xác minh hai bước để đăng nhập dịch vụ Admin Setting - MY WEB PLAY In " + xuxu, noidung, "teinnkatajeqerfl", "false", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

            var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungWW = System.IO.File.ReadAllText(pathWW);

            var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoXWW[1] == "true")
            {
                var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidungS = docfile(pathS);

                var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[POST]";

                System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
            }

            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        public void khoawebsiteAdmin()
        {
            try
            {
                var addHtml = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "");
                var addHtmlSpan = addHtml.Split("\n:::\n");

                if (addHtmlSpan.Length > 1)
                    TempData["HTML-added"] = addHtmlSpan[1];

                HttpContext.Session.Remove("accept_notice");
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        TempData["locked-app"] = "true";
                    }
                    else
                    {
                        TempData["locked-app"] = "false";
                    }
                }

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            TempData["tathoatdong"] = "true";
                        }
                        else
                        {
                            TempData["tathoatdong"] = "false";
                        }

                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                // return RedirectToAction("Error", "Home", new { exception = "true" });
            }
        }

        public ActionResult ComeInSetting(string id, string code)
        {
            try
            {

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pthA = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pthA).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

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
                }
                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                TempData.Remove("trust-setting");

                var key = listSetting[60].Split("<3275>")[3];

                if (id == StringMaHoaExtension.Decrypt(nd[0], key) && code == StringMaHoaExtension.Decrypt(nd[1], key))
                {
                    HttpContext.Session.SetString(StringMaHoaExtension.Decrypt(nd[0], key), StringMaHoaExtension.Decrypt(nd[0], key));
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        public ActionResult SettingXYZ_DarkAdmin()
        {
            try
            {
                if (HttpContext.Session.GetString("admin-userIP") == null)
                {
                    TempData["WantToGetUserIP"] = "true";
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                TempData["root_path_web"] = _webHostEnvironment.ContentRootPath.ToString();

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (string.IsNullOrEmpty(valuePam))
                {
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[GET]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                var pthX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                var filestatus = System.IO.File.ReadAllText(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];
                if (filestatus == "file_MO")
                {
                    ViewBag.FileStatus = "đang mở";
                }
                else
                if (filestatus == "file_TAT")
                    ViewBag.FileStatus = "đang đóng";

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                if (HttpContext.Session.GetString("open-admin") == "true")
                {
                    TempData["admin-open"] = "true";
                }
                else
                if (HttpContext.Session.GetString("open-admin") == "false")
                {
                    TempData["admin-open"] = "false";
                    HttpContext.Session.Remove("open-admin");
                }

                HttpContext.Session.Remove("mini-web");
                HttpContext.Session.Remove("mini-error");
                TempData.Remove("mini-web");

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var setlog = System.IO.File.ReadAllText(pthX);
                TempData["setting-play"] = setlog;

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidung1 = docfile(path1);
                TempData["showIPCome"] = noidung1;

                TempData["rexJP"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin","ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt")).Replace("\t", " ➡ ");

                TempData["fontKara"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "karaoke_font.txt"));

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var pathSS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/Setting_Status.txt");
                var noidungSS = System.IO.File.ReadAllText(pathSS);
                ViewBag.SettingStatus = noidungSS;

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                SettingAdmin settingAdmin = new SettingAdmin();
                settingAdmin.Topics = new List<SettingAdmin.Topic>();
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    settingAdmin.Topics.Add(new SettingAdmin.Topic(info[0], info[2], bool.Parse(info[1])));

                    if (info[0] == "Password_Admin")
                        ViewBag.Password = info[3];

                    if (info[0] == "Encode_Url")
                    {
                        ViewBag.EncodeUrl = info[3];
                        ViewBag.EncodeUrl_Line = info[3].Replace("***", "\r\n");
                    }

                    if (info[0] == "MatDoTuyetDoi")
                        ViewBag.MaMatDo = info[3];

                    if (info[0] == "Code_LockedClient")
                        ViewBag.CodeSocolar = info[3];

                    if (info[0] == "Believe_IP")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.Believe = "";
                        else
                        {
                            ViewBag.Believe = info[3];
                            ViewBag.Belive_Line = info[3].Replace(",", "\r\n");
                        }
                    }

                    if (info[0] == "Info_Email")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.InfoEmail = "";
                        else
                            ViewBag.InfoEmail = info[3];
                    }

                    if (info[0] == "Info_MegaIO")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.InfoMegaIO = "";
                        else
                            ViewBag.InfoMegaIO = info[3];
                    }

                    if (info[0] == "Color_BackgroundAndText")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.SetColor = "";
                        else
                            ViewBag.SetColor = info[3];
                    }

                    if (info[0] == "AppWeb_LockedUse")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.LockedUseApp = "";
                        else
                        {
                            ViewBag.LockedUseApp = info[3];
                            ViewBag.LockedUseApp_Line = info[3].Replace(",", "\r\n");
                        }
                    }

                    if (info[0] == "DownloadFile_ClearWeb")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.DownloadFileQuick = "";
                        else
                        {
                            ViewBag.DownloadFileQuick = info[3];
                            ViewBag.DownloadFile_Line = info[3].Replace(",", "\r\n");
                        }
                    }

                    if (info[0] == "Accept_ListUrl")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.AcceptListUrl = "";
                        else
                        {
                            ViewBag.AcceptListUrl = info[3];
                            ViewBag.AcceptListUrl_Line = info[3].Replace(",", "\r\n");
                        }
                    }

                    if (info[0] == "TabTittle_NoiDung")
                    {
                        if (info[3] == "[NULL]")
                            ViewBag.TabTittle = "C# | Asp Net Core | Công nghệ thông tin";
                        else
                            ViewBag.TabTittle = info[3];
                    }

                    if (info[0] == "UploadFile_MaxSize")
                    {
                        ViewBag.MaxSizeFile = info[3];
                    }

                    if (info[0] == "Time_Waiting")
                    {
                        ViewBag.TimeWaiting = info[3];
                    }

                    if (info[0] == "Admin_Setting")
                    {
                        ViewBag.AdminSetting = info[3];
                    }

                    if (info[0] == "KeyEncrypt_Admin")
                    {
                        ViewBag.KeyEncryptAdmin = info[3];
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        ViewBag.HTMLVisible = info[3];
                    }

                    if (info[0] == "Color_TracNghiem")
                    {
                        ViewBag.TNColor = info[3];
                    }

                    if (info[0] == "Save_ComeHere")
                    {
                        if (info[1] == "false")
                        {
                            TempData["SaveComeHere"] = "false";
                        }
                        else
                        {
                            TempData["SaveComeHere"] = "true";
                        }
                    }

                    //if (info[0] == "Password_Setting")
                    //{
                    //    if (info[1] == "false")
                    //    {
                    //        TempData["SecureAdmin"] = "false";
                    //    }
                    //    else
                    //    {
                    //        TempData["SecureAdmin"] = "true";
                    //    }
                    //}

                    //if (HttpContext.Session.GetString(nd[0]) == nd[1])
                    //{
                    //    TempData["SecureAdmin"] = "false";
                    //}
                }
                var key = listSetting[60].Split("<3275>")[3];
                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                TempData["key-admin"] = StringMaHoaExtension.Decrypt(nd[0], key);
                // TempData["value-admin"] = nd[1];

                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPOnWebPlay.txt");
                var noidung2 = docfile(path2);
                TempData["ListIPActive"] = noidung2;

                var path3 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPLock.txt");
                var noidung3 = docfile(path3);
                TempData["ListIPLockAdmin"] = noidung3;

                var path4 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/LockedIPClient.txt");
                var noidung4 = docfile(path4);
                TempData["ListIPLockClient"] = noidung4;

                var sam = settingAdmin.Topics[23].NoiDung;
                var span = "";
                Regex regExp = new Regex("<@@.*@>");
                foreach (Match match in regExp.Matches(sam))
                {
                    span += match.Value;
                    break;
                }

                if (span != "")
                {
                    var so_span = span.Replace("<@@", "").Replace("@@>", "");
                    settingAdmin.Topics[23].NoiDung = settingAdmin.Topics[23].NoiDung.Replace(span, "<input required min=\"0\"  type=\"number\" style=\"width: 5em\" name=\"txtDataThan\" value=\"" + so_span + "\" />");
                }


                return View(settingAdmin);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult UnlockAllWeb(IFormCollection f)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var xinh = "false";
                if (f["LockedAll_Web"] == "on")
                {
                    xinh = "true";
                }

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[33].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                noidung = noidung.Replace(listSetting[33], infoX[0] + "<3275>" + xinh + "<3275>" + infoX[2]);
                System.IO.File.WriteAllText(path, noidung);

                return RedirectToAction("SettingXYZ_DarkAdmin");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult SettingXYZ_DarkAdmin(IFormCollection f)
        {
            try
            {
                if (HttpContext.Session.GetString("admin-userIP") == null)
                {
                    TempData["WantToGetUserIP"] = "true";
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                var testUser = HttpContext.Session.GetString("open-admin-yes");
                if (testUser == null || testUser != "true")
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (string.IsNullOrEmpty(valuePam))
                {
                    return RedirectToAction("LoginSettingAdmin", "Admin");
                }

                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                HttpContext.Session.Remove("mini-web");
                var non = TempData["SaveComeHere"];
                TempData["SaveComeHere"] = non;

                var pathXY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");
                var matpassAd = System.IO.File.ReadAllText(pathXY).Replace("\r", "").Split("\n")[1];

                var passUserEnter = f["txtMatPassAd"].ToString();

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var key = listSetting[60].Split("<3275>")[3];

                if (passUserEnter != StringMaHoaExtension.Decrypt(matpassAd, key))
                {
                    TempData["loisave"] = "true";
                    return RedirectToAction("SettingXYZ_DarkAdmin");
                }
                else
                {
                    TempData["loisave"] = "false";
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/Setting_Status.txt");

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                }

                path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);

                noidung = System.IO.File.ReadAllText(path);

                listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flag = 0;
                var cometo = "#";
                var dix = 0;

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                        continue;

                    if (info[0] == "Mail_ReportUrl")
                    {
                        if (string.IsNullOrEmpty(f["txtDataThan"].ToString()) == false)
                        {
                            var sam = info[2];
                            var span = "";
                            Regex regExp = new Regex("<@@.*@>");
                            foreach (Match match in regExp.Matches(sam))
                            {
                                span += match.Value;
                                break;
                            }

                            if (span != "" && f["txtDataThan"].ToString() != span.Replace("<@@","").Replace("@@>",""))
                            {
                                noidung = noidung.Replace(span, "<@@" + f["txtDataThan"] +"@@>");
                                cometo = "#come-" + i;
                            }
                        }
                    }

                    var baby1 = false;
                    var baby2 = false;

                    var xi = "false";
                    if (f[info[0]] == "on")
                    {
                        xi = "true";
                    }

                    if (info[0] == "Off_CallIP" && (xi != info[1]) && xi == "true")
                    {
                        baby1 = true;
                    }

                    if (info[0] == "All_People" && (xi != info[1]) && xi == "true")
                    {
                        baby2 = true;
                    }

                    if (flag == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flag = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flag = 0;
                        }
                    }

                    if (info[0] != "Password_Admin" && info[0] != "Believe_IP" && info[0] != "Code_LockedClient" && info[0] != "MatDoTuyetDoi" && info[0] != "Encode_Url" && info[0] != "Info_Email" && info[0] != "Info_MegaIO" && info[0] != "Color_BackgroundAndText" &&
                      info[0] != "Color_TracNghiem" && info[0] != "AppWeb_LockedUse" && info[0] != "DownloadFile_ClearWeb" && info[0] != "Accept_ListUrl" &&
                      info[0] != "UploadFile_MaxSize" && info[0] != "TabTittle_NoiDung" && info[0] != "Time_Waiting" && info[0] != "HTML_Visible" && info[0] != "Admin_Setting" && info[0] != "KeyEncrypt_Admin")
                    {
                        if (xi != info[1])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(info[0] + "<3275>" + info[1], info[0] + "<3275>" + xi);

                        if (baby1 == true)
                        {
                            noidung = noidung.Replace("All_People" + "<3275>" + "true", "All_People" + "<3275>" + "false");
                        }

                        if (baby2 == true)
                        {
                            noidung = noidung.Replace("Off_CallIP" + "<3275>" + "true", "Off_CallIP" + "<3275>" + "false");
                        }
                    }
                    else if (info[0] == "Encode_Url")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "/Home/Index***/Home/SecretWeb***/Admin/QuickDataInWeb***/Home/UploadFile***/Home/UploadFile?type=1<splix>0<splix>0***/Home/DownloadFile***/Home/EditTextNote***/Home/DownloadFile_ClearWeb";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Password_Admin")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "mywebplay-ADMIN";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Believe_IP")
                    {
                        var xinh = f[info[0]].ToString();
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";
                        else
                        {
                            if (xinh.StartsWith(",") == false)
                                xinh = "," + xinh;

                            if (xinh.EndsWith(",") == false)
                                xinh = xinh + ",";
                        }

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Code_LockedClient")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "abc-xyz";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "UploadFile_MaxSize")
                    {
                        var xinh = f[info[0]];

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Time_Waiting")
                    {
                        var xinh = f[info[0]];

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "KeyEncrypt_Admin")
                    {
                        var xinh = f[info[0]];

                        if (string.IsNullOrEmpty(xinh))
                            xinh = "buivanluat-ADMIN3275";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);

                        var ptj = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                        var lan = System.IO.File.ReadAllText(ptj);
                        var na = lan.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                        var id = StringMaHoaExtension.Decrypt(na[0], info[3]);
                        string code = StringMaHoaExtension.Decrypt(na[1], info[3]);

                        lan = lan.Replace(na[0], StringMaHoaExtension.Encrypt(id, xinh));
                        lan = lan.Replace(na[1], StringMaHoaExtension.Encrypt(code, xinh));
                        System.IO.File.WriteAllText(ptj, lan);
                    }
                    else if (info[0] == "HTML_Visible")
                    {
                        var xinh = f[info[0]];

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "MatDoTuyetDoi")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "matdotuyetdoi<>believix-123";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Admin_Setting")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "SettingABC_DarkBVL.txt";

                        if (xinh.ToString().EndsWith(".txt") == false)
                            xinh += ".txt";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        System.IO.File.Move(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", info[3]), Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", xinh));

                        var xpath = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                        var ndOfPath = System.IO.File.ReadAllText(xpath);

                        ndOfPath = ndOfPath.Replace(info[3], xinh);

                        System.IO.File.WriteAllText(xpath, ndOfPath);

                        path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Info_Email")
                    {
                        var xinh = f[info[0]].ToString();
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";
                        else
                        {
                            var mai = xinh.Split("<5828>");
                            if (mai[1].Contains("[Encrypted_3275]") == false)
                            {
                                xinh = StringMaHoaExtension.Encrypt(mai[0], "32752262") + "<5828>" + StringMaHoaExtension.Encrypt(mai[1], "32752262") + "[Encrypted_3275]";
                            }
                        }

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Info_MegaIO")
                    {
                        var xinh = f[info[0]].ToString();
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";
                        else
                        {
                            var mai = xinh.Split("<5828>");
                            if (mai[1].Contains("[Encrypted_3275]") == false)
                            {
                                xinh = StringMaHoaExtension.Encrypt(mai[0], "32752262") + "<5828>" + StringMaHoaExtension.Encrypt(mai[1], "32752262") + "[Encrypted_3275]";
                            }
                        }

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "AppWeb_LockedUse")
                    {
                        var xinh = f[info[0]].ToString();
                        xinh = xinh.Replace("/Home/SessionPlay_DarkAdmin", "").Replace("/Cover/Ee90d45ca0d59031d2a3b6dc488187c00", "").Replace("/Cover/E41fb870a2ab7c2470cb35f51171e20ee", "").Replace("/Cover/E8ecb5a3a2b4fdbda327335d19f3ca7fa", "").Replace("/Home/Error", "").Replace(",,", ",").Replace("/Cover/E003e16661a80c9451f46587afb2ec5c3", "").Replace("/Cover/Ef8e2d510133438f980423324078e0939", "").Replace("/Cover/E72fd1425ee00a7192a7261c5b45fcab2", "").Replace("/Cover/E86a31cf62149eaf28543a8a639d91309", "").Replace("/Cover/E8b5931c6ba81548e4b0a06e07fdd5282", "").Replace("/Cover/Ee98b70f046cbdf12b3eee2d65e625373", "").Replace("/Cover/E10f628a33fcf828bb57d497794a34094", "").Trim(',');
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Color_BackgroundAndText")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "DownloadFile_ClearWeb")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Accept_ListUrl")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "[NULL]";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "TabTittle_NoiDung")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "C# | Asp Net Core | Công nghệ thông tin";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                    else if (info[0] == "Color_TracNghiem")
                    {
                        var xinh = f[info[0]];
                        if (string.IsNullOrEmpty(xinh))
                            xinh = "green";

                        if (xinh != info[3])
                        {
                            cometo = "#come-" + i;
                            dix++;
                        }

                        noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                    }
                }
                System.IO.File.WriteAllText(path, noidung);
                HttpContext.Session.SetString("index-setting", cometo);

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[POST]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                return Redirect("/Admin/SettingXYZ_DarkAdmin" + cometo);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        //-------------------------------------------------

        private String docfile(String filename)
        {
            string[] a = System.IO.File.ReadAllLines(filename);
            String s = "";
            for (int i = 0; i < a.Length; ++i)
            {
                s = s + a[i];
                if (i < a.Length - 1)
                    s = s + "\n";
            }
            return s;
        }

        public ActionResult TracNghiemOnline_ViewMark()
        {
            try
            {
                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xa = Request.Path;
                    if (xa == "" || xa == "/" || xa == null) xa = "/Home/Index";
                    if (lockedApp[3].Contains(xa))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[GET]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var infoYA = listSetting[56].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoYA[0] == "HTML_Visible")
                {
                    TempData["HTML-visible"] = infoYA[3];
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

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
                }
                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

                if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "TracNghiem_XOnline", "DiemHocSinh.txt");

                var file = new FileInfo(path);

                if (System.IO.File.Exists(path))
                {
                    ViewBag.Text1 = System.IO.File.ReadAllText(path);
                    ViewBag.Text2 = "<p id=\"preX\" name=\"colorX\" style=\"color:" + TempData["mau_text"] + ";font-size:22px; display:none\">" + ViewBag.Text1.Replace("\n", "<br>").Replace("\t","&nbsp;&nbsp;&nbsp;&nbsp;") + "</p>";
                    Calendar x = CultureInfo.InvariantCulture.Calendar;
                    ViewBag.DateTime = x.AddHours(file.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                }
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult EditStudentMark()
        {
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            try
            {
                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[GET]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
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

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

                if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "TracNghiem_XOnline", "DiemHocSinh.txt");
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Text = System.IO.File.ReadAllText(path);
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult EditStudentMark(string? txtText)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = System.IO.File.ReadAllText(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = System.IO.File.ReadAllText(pam);

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = false;
                }

                if (infoXWW[1] == "true" && yes_log)
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = docfile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + DateTime.Now + "\t" + this.Request.Path + "\t[POST]";

                    System.IO.File.WriteAllText(pathS, noidungZ.Trim('\n'));
                }

                var fix = string.Format("txtText : {0}\n", txtText);
                HttpContext.Session.SetString("hanhdong_3275", fix);

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

                if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                }
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xa = Request.Path;
                    if (xa == "" || xa == "/" || xa == null) xa = "/Home/Index";
                    if (lockedApp[3].Contains(xa))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                var flag = 0;
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

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }

                    if (flag == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flag = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flag = 0;
                        }
                    }
                }
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "TracNghiem_XOnline", "DiemHocSinh.txt");

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (string.IsNullOrEmpty(txtText) == false)
                    System.IO.File.WriteAllText(path, txtText);
                else
                {
                    var noidung = "Thời gian bắt đầu\tIP Address\tMSSV\tID Link\tThời gian kết thúc\tĐiểm\r\n";
                    System.IO.File.WriteAllText(path, noidung);
                }

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return RedirectToAction("TracNghiemOnline_ViewMark");
        }

        public ActionResult XoaViewMark()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

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
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "TracNghiem_XOnline", "DiemHocSinh.txt");

                if (System.IO.File.Exists(path))
                {
                    var noidung = "Thời gian bắt đầu\tIP Address\tMSSV\tID Link\tThời gian kết thúc\tĐiểm\r\n";
                    System.IO.File.WriteAllText(path, noidung);
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return RedirectToAction("TracNghiemOnline_ViewMark");
        }

        public ActionResult ReportListIPComeHere()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

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
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }
                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidung1 = docfile(path1);

                System.IO.File.WriteAllText(path1, "");

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string host = "{" + Request.Host.ToString() + "}"
                  .Replace("http://", "")
                  .Replace("https://", "")
                  .Replace("/", "");

                var non = TempData["SaveComeHere"];
                TempData["SaveComeHere"] = non;

                if (string.IsNullOrEmpty(noidung1) == false)
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", host + "[ADMIN] Báo cáo thủ công danh sách các IP user đã ghé thăm và request từng chức năng của trang web (tất cả/có thể chưa đầy đủ) In " + xuxu, noidung1, "teinnkatajeqerfl");

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return RedirectToAction("SettingXYZ_DarkAdmin", new
            {
                key = "code"
            });
        }

        public ActionResult DeleteIPComeHere()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

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
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidung1 = docfile(path1);

                System.IO.File.WriteAllText(path1, "");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

            return RedirectToAction("SettingXYZ_DarkAdmin", new
            {
                key = "code"
            });
        }

        public ActionResult QuickDataInWeb(string? first)
        {
            try
            {
                HttpContext.Session.Remove("ok-data");
                if (string.IsNullOrEmpty(first) == false)
                    ViewBag.FirstGoTo = "true";
                else
                    ViewBag.FirstGoTo = "false";

                Random ri = new Random();
                ViewBag.NumberRandom = ri.Next(3) + 1;

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                var flix = 0;
                var flox = 0;
                var flx = 0;
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

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }

                    if (HttpContext.Session.GetString("TuyetDoi") != "true" && info[0] == "OffWebsite_All")
                    {
                        if (info[1] == "true")
                            return RedirectToAction("Error", "Home");
                    }

                    if (info[0] == "Post_Clipboard")
                    {
                        if (info[1] == "false")
                        {
                            TempData["PostResult"] = "false";
                        }
                        else
                        {
                            TempData["PostResult"] = "true";
                        }
                    }

                    if (info[0] == "Random_Layout")
                    {
                        if (info[1] == "false")
                        {
                            TempData["Layout_Random"] = "false";
                        }
                        else
                        {
                            TempData["Layout_Random"] = "true";
                        }
                    }

                    if (info[0] == "Clear_Website")
                    {
                        if (info[1] == "true")
                        {
                            flix = 1;
                        }
                    }

                    if (info[0] == "Using_QuickData")
                    {
                        if (info[1] == "true")
                        {
                            flx = 1;
                        }
                    }

                    if (flix == 1 && flx == 1 && info[0] == "NotAlert_QuickData")
                    {
                        if (info[1] == "true")
                        {
                            flox = 1;
                        }
                    }

                    if (flox == 0)
                    {
                        if (HttpContext.Session.GetString("TuyetDoi") != "true" && (info[0] == "Using_QuickData" || info[0] == "Using_Website"))
                        {
                            if (info[1] == "false")
                            {
                                var r = new Random();
                                var x = r.Next(0, 2);
                                if (x == 1)
                                    return Redirect("https://learn.microsoft.com/vi-vn/training/paths/get-started-c-sharp-part-1/?WT.mc_id=dotnet-35129-website");
                                return Redirect("https://stackoverflow.com/questions");
                            }
                        }
                    }

                    if (info[0] == "Off_RandomTab")
                    {
                        if (info[1] == "false")
                        {
                            var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Image.txt");
                            var hinh = System.IO.File.ReadAllText(pathX1).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Tittle.txt");
                            var tittle = System.IO.File.ReadAllText(pathX2).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var r = new Random();
                            var ix = r.Next(0, hinh.Length);
                            var iy = r.Next(0, tittle.Length);

                            TempData["OffRandomTab"] = "false";
                            TempData["Tab_Image"] = hinh[ix];
                            TempData["Tab_Tittle"] = tittle[iy];
                        }
                        else
                        {
                            TempData["OffRandomTab"] = "true";
                        }
                    }
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomLayOut.txt");
                TempData["RandomLayout"] = docfile(path);

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult RemoveAllSessionAndTempData()
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

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
                }
                HttpContext.Session.Clear();
                TempData.Clear();
                return Redirect("https://stackoverflow.com/questions");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult QuickDataInWeb(IFormCollection f)
        {
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

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
                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }
                }
                HttpContext.Session.Remove("ok-data");
                try
                {
                    var name = f["txtNoiDung"].ToString().Replace("\r", "").Split("\n|\n", StringSplitOptions.RemoveEmptyEntries);
                    TempData["Name"] = name[0];

                    var s = name[1].Replace("\r", "").Split("\n#\n", StringSplitOptions.RemoveEmptyEntries);
                    var chuoi = "";
                    for (int i = 0; i < s.Length; i++)
                    {
                        var ss = s[i].Replace("\r", "").Split("\n*\n", StringSplitOptions.RemoveEmptyEntries);
                        ss[1] = ss[1].Replace("[NULL]", "");

                        chuoi += "<textarea name=\"" + ss[0] + "\" cols=\"80\" rows=\"30\">" + ss[1].Trim('\"').Replace("[ngoackep_0000]", "\"") + "</textarea><br>\n";
                    }

                    TempData["Data"] = chuoi;

                    HttpContext.Session.SetString("ok-data", "true");

                    return RedirectToAction("PlayDataInWeb");
                }
                catch (Exception ex)
                {
                    var req = Request.Path;

                    if (req == "/" || string.IsNullOrEmpty(req))
                        req = "/Home/Index";

                    var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                    HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                    System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                    var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                    if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                    {
                        Calendar xz = CultureInfo.InvariantCulture.Calendar;
                        string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                        SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                    }

                    return Redirect("http://stackoverflow.com/questions/4733878/how-to-debug-a-stackoverflowexception-in-net");
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult PlayDataInWeb()
        {
            try
            {
                Random ri = new Random();
                ViewBag.NumberRandom = ri.Next(3) + 1;

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

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

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }

                    if (HttpContext.Session.GetString("TuyetDoi") != "true" && info[0] == "OffWebsite_All")
                    {
                        if (info[1] == "true")
                            return RedirectToAction("Error", "Home");
                    }

                    if (HttpContext.Session.GetString("ok-data") != "true")
                    {
                        return Redirect("http://stackoverflow.com/questions/206820/how-do-i-prevent-and-or-handle-a-stackoverflowexception");
                    }

                    if (TempData["NotAlertQuickData"] == "false")
                    {
                        if (HttpContext.Session.GetString("TuyetDoi") != "true" && (info[0] == "Using_QuickData" || info[0] == "Using_Website"))
                        {
                            if (info[1] == "false")
                            {
                                var r = new Random();
                                var x = r.Next(0, 2);
                                if (x == 1)
                                    return Redirect("https://learn.microsoft.com/vi-vn/training/paths/get-started-c-sharp-part-1/?WT.mc_id=dotnet-35129-website");
                                return Redirect("https://stackoverflow.com/questions");
                            }
                        }
                    }

                    if (info[0] == "Random_Layout")
                    {
                        if (info[1] == "false")
                        {
                            TempData["Layout_Random"] = "false";
                        }
                        else
                        {
                            TempData["Layout_Random"] = "true";
                        }
                    }

                    if (info[0] == "Off_RandomTab")
                    {
                        if (info[1] == "false")
                        {
                            var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Image.txt");
                            var hinh = System.IO.File.ReadAllText(pathX1).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Tittle.txt");
                            var tittle = System.IO.File.ReadAllText(pathX2).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var r = new Random();
                            var ix = r.Next(0, hinh.Length);
                            var iy = r.Next(0, tittle.Length);

                            TempData["OffRandomTab"] = "false";
                            TempData["Tab_Image"] = hinh[ix];
                            TempData["Tab_Tittle"] = tittle[iy];
                        }
                        else
                        {
                            TempData["OffRandomTab"] = "true";
                        }
                    }
                }
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomLayOut.txt");
                TempData["RandomLayout"] = docfile(path);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        public ActionResult RefreshInfoIPRegist()
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

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
                }
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/InfoIPRegist.txt");
                System.IO.File.WriteAllText(path, "IP\tDateTime\tInfo");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
        }

        private void XoaDirectoryNull(string path)
        {
            var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetFiles();
            var folders = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();

            var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();
            foreach (var item in listFolder)
            {
                XoaDirectoryNull(path + "/" + item.Name);
            }

            if (listFile.Length == 0 && folders.Length == 0 && path != "file" && path != "#fileclose")
            {
                System.IO.Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path), true);
            }
        }

        public int SoSanh2Ngay(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            if (d1 == d2 && m1 == m2 && y1 == y2)
                return 0;

            if (y1 < y2)
                return -1;

            if (y1 == y2)
            {
                if (m1 == m2)
                {
                    if (d1 < d2)
                        return -1;
                }
                else
                if (m1 < m2)
                    return -1;
            }
            return 1;
        }

        public ActionResult RefreshFileHetHan(bool chk)
        {
            try
            {

                if (chk)
                {
                    var pathE = Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal");
                    var directories = Directory.GetDirectories(pathE);

                    foreach (string directory in directories)
                    {
                        Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal", directory), true);
                    }
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

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
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pthA = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoffX = System.IO.File.ReadAllText(pthA).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoffX == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Delete(true);

                var pthY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var ndY = System.IO.File.ReadAllText(pthY);
                var onoff = ndY.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal")).Create();

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Create();
                if (onoff == "file_MO")
                {
                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == false)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();

                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == true)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Delete(true);
                }
                else if (onoff == "file_TAT")
                {
                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == false)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Create();

                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == true)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Delete(true);
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Delete(true);

                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt"), "");

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Create();

                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    f.Delete();
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Create();

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth);
                var onoffY = nd.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"));

                if (onoffY == "file_TAT")
                    infoFile = infoFile.Replace("file/", "#fileclose/");
                else
                if (onoffY == "file_MO")
                    infoFile = infoFile.Replace("#fileclose/", "file/");

                var files = infoFile.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                for (int xx = 0; xx < files.Length; xx++)
                {
                    if (files[xx] == "") continue;

                    var fi = files[xx].Split("\t");

                    var today = xuxu.Split("/");
                    var hethan = fi[1].Split("/");

                    var d1 = int.Parse(today[0]);
                    var m1 = int.Parse(today[1]);
                    var y1 = int.Parse(today[2]);

                    var d2 = int.Parse(hethan[0]);
                    var m2 = int.Parse(hethan[1]);
                    var y2 = int.Parse(hethan[2]);

                    if (SoSanh2Ngay(d1, m1, y1, d2, m2, y2) >= 0 || new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0])).Exists == false)
                    {
                        FileInfo fx = new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0].TrimStart('/')));
                        fx.Delete();
                        infoFile = infoFile.Replace(fi[0] + "\t" + fi[1] + "\n", "");
                    }

                }
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"), infoFile);
                try
                {
                    XoaDirectoryNull("file");
                }
                catch (Exception ex)
                {
                    ViewBag.Loi = "";
                    RedirectToAction("SettingXYZ_DarkAdmin");
                }

                return RedirectToAction("SettingXYZ_DarkAdmin");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult SettingStatusUpdate(string type, bool come)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/Setting_Status.txt");

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                var noidung = System.IO.File.ReadAllText(path);

                switch (type)
                {
                    case "-1":
                        System.IO.File.WriteAllText(path, "[Đặc biệt] Khoá trang web tuyệt đối/vĩnh viễn, không thể truy cập trang nào (kể cả admin) => error all web page # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "1":
                        System.IO.File.WriteAllText(path, "Tắt hoạt động web site (tất cả, không nhận đơn đăng kí tiếp theo, mở chiến dịch xoá localStorage JS), ngoại trừ mật độ tuyệt đối # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "2":
                        System.IO.File.WriteAllText(path, "Bật hoạt động web site (chỉ người đã đăng kí, bật TB, nhận đơn đăng kí tiếp theo) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "3":
                        System.IO.File.WriteAllText(path, "Bật hoạt động web site (chỉ người đã đăng kí, tắt TB, không nhận đơn đăng kí tiếp theo) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "4":
                        System.IO.File.WriteAllText(path, "Bật hoạt động website (cho phép tất cả mọi người, không get IP) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "5":
                        System.IO.File.WriteAllText(path, "Bật hoạt động website (cho phép tất cả mọi người, get IP) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "6":
                        System.IO.File.WriteAllText(path, "Tắt hoạt động web site (không nhận đơn đăng kí tiếp theo, bật thông báo), ngoại trừ các IP đã đăng kí và tin tưởng # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "7":
                        System.IO.File.WriteAllText(path, "Tắt hoạt động web site (không nhận đơn đăng kí tiếp theo, tắt thông báo), ngoại trừ các IP đã đăng kí và tin tưởng # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "8":
                        System.IO.File.WriteAllText(path, "Sử dụng POST QUICK DATA và tàng hình (copy), không get IP - cho phép mọi người # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "9":
                        System.IO.File.WriteAllText(path, "Sử dụng POST QUICK DATA và tàng hình (download *.txt), không get IP - cho phép mọi người # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "10":
                        var nd = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd[0] + " + Bật thông báo tất cả nội dung liên quan email và MegaIO File and list user get to link request (riêng karaoke sẽ nhận cả hai bản : Text và MP3) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "11":
                        var nd1 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd1[0] + " + Tắt thông báo tất cả nội dung liên quan email và MegaIO File and list user get to link request # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "12":
                        var nd2 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd2[0] + " + Bật sử dụng website with url encode secret play # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "13":
                        var nd3 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd3[0] + " + Không cho phép sử dụng mật độ tuyệt đối hay IP tin tưởng để truy cập website # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "14":
                        var nd4 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd4[0] + " + Cho phép upload file nhanh (UploadFile_ClearWeb) và chế độ tàng hình # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "15":
                        System.IO.File.WriteAllText(path, "Sử dụng POST DATA QUICK admin, cho phép mọi người, chuyển hướng đến trang file TXT của result (dành cho external) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "16":
                        var nd5 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd5[0] + " + Đã tắt hết các setting phụ (sub admin setting), các item chính vẫn giữ nguyên # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "17":
                        System.IO.File.WriteAllText(path, "Bí mật và sử dụng data External API # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "18":
                        System.IO.File.WriteAllText(path, "Đã tắt hết các item setting (kể cả các setting phụ) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "19":
                        System.IO.File.WriteAllText(path, "Đã bật hết các item setting phụ, các item chính vẫn giữ nguyên # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "20":
                        System.IO.File.WriteAllText(path, "Đã bật hết các item setting (kể cả các setting phụ) # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "21":
                        var nd6 = noidung.Split(" # ");
                        System.IO.File.WriteAllText(path, nd6[0] + " + Bật xác thực hai bước ADMIN # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "ON-ALL":
                        System.IO.File.WriteAllText(path, "Đã bật hết tất cả các item setting (ngoại trừ mục cho phép website với mọi người thì sẽ ưu tiên việc get IP; và về setting nhận thông báo email dữ liệu Karaoke của user thì nhận luôn cả hai bản : Text và MP3) và hãy cẩn thận sự mâu thuẫn giữa các item setting lúc này # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    case "OFF-ALL":
                        System.IO.File.WriteAllText(path, "Đã tắt hết tất cả các item setting  # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;

                    default:
                        System.IO.File.WriteAllText(path, "UNKNOW # " + xuxu + " (lưu ý nên kiểm tra thêm các setting phụ)");
                        break;
                }
                if (come == false || HttpContext.Session.GetString("index-setting") == null)
                    return RedirectToAction("SettingXYZ_DarkAdmin");
                else
                {
                    var cometo = HttpContext.Session.GetString("index-setting");
                    HttpContext.Session.Remove("index-setting");
                    return Redirect("/Admin/SettingXYZ_DarkAdmin" + cometo);
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult CheckMeMiniWeb(string? code)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pthA = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pthA).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var pass = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                var key = listSetting[60].Split("<3275>")[3];
                if (StringMaHoaExtension.Decrypt(pass, key) == code)
                {
                    TempData["check-me"] = "true";
                    HttpContext.Session.SetString("mini-mini", "true");
                }
                else
                {
                    TempData.Remove("check-me");
                }

                return RedirectToAction("SettingXYZ_DarkAdmin");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + (( HttpContext.Session.GetString("admin-userIP") != null) ?  HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult OpenAdmin(string? code)
        {
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var nd = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
            var key = listSetting[60].Split("<3275>")[3];
            if (code == StringMaHoaExtension.Decrypt(nd[1], key))
            {
                HttpContext.Session.SetString("open-admin", "true");
                HttpContext.Session.SetString("open-admin-yes", "true");
                TempData["admin-open"] = "true";
            }
            else
            {
                HttpContext.Session.SetString("open-admin", "false");
                TempData["admin-open"] = "false";
            }

            return RedirectToAction("SettingXYZ_DarkAdmin");
        }
    }
}