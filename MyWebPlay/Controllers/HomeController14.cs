using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Drawing;
using System;
using System.Formats.Tar;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using MyWebPlay.Extension;
using Microsoft.Extensions.Hosting;
using MyWebPlay.Model;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Prng;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PlayKaraoke()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                ViewBag.Music = "";
                ViewBag.Musix = "";

                ViewBag.Karaoke = "karaoke";

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult PlayKaraoke(IFormCollection f, IFormFile txtKaraoke, IFormFile txtMusic, IFormFile txtMusix)
        {
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                fix += "txtKaraoke : " + ((txtKaraoke != null) ? txtKaraoke.FileName : string.Empty) + "\n";
                fix += "txtMusic : " + ((txtMusic != null) ? txtMusic.FileName : string.Empty) + "\n";
                fix += "txtMusix : " + ((txtMusix != null) ? txtMusix.FileName : string.Empty) + "\n";

                HttpContext.Session.SetString("hanhdong_3275", fix);
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                /*HttpContext.Session.Remove("ok-data");*/
                TempData["dataPost"] = "[POST]";
                HttpContext.Session.SetString("data-result", "true");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                HttpContext.Session.Remove("ok-data");
                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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
                var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX1);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flag = 0;
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

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
                if (txtMusic == null || txtMusic.Length == 0)
                    txtMusic = txtMusix;

                if (txtMusix == null || txtMusix.Length == 0)
                    txtMusix = txtMusic;

                ViewBag.Karaoke = "";

                ViewBag.Show = "show";

                var chon = f["KaraChon"].ToString();

                if (chon == "1")
                {
                    var r = new Random();

                    var n = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example/background")).GetFiles().Length;
                    int x = r.Next(n);

                    ViewBag.Background = "/karaoke_Example/background/" + (x + 1) + ".jpg";
                    ViewBag.SuDung = "";
                }
                else if (chon == "2")
                {
                    var link = f["txtOnline"].ToString();
                    ViewBag.Background = link;
                    ViewBag.SuDung = "";
                }
                else if (chon == "3")
                {
                    var link = f["txtOnline"].ToString();
                    ViewBag.Background = link;
                    ViewBag.SuDung = "Video";
                }
                else if (chon == "4")
                {
                    var link = f["txtOnline"].ToString();
                    link = link.Replace("&", "");
                    link = link.Replace("loop", "");
                    link = link.Replace("autoplay", "");
                    link = link.Replace("controls", "");
                    link = link.Replace("mute", "");
                    link = link.Replace("youtu.be/", "youtube.com/embed/");
                    link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                    if (link.Contains("?"))
                        link += "&autoplay=1&loop=1&controls=0&mute=1";
                    else
                        link += "?autoplay=1&loop=1&controls=0&mute=1";

                    ViewBag.Background = link;
                    ViewBag.SuDung = "Youtube";
                }

                if (f["txtChon"].ToString() != "on")
                {
                    var fileName = Path.GetFileName(txtMusic.FileName);
                    var nameFile = Path.GetFileName(txtMusix.FileName);

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", nameFile);

                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        txtMusic.CopyTo(fileStream);
                    }

                    using (Stream fileStream = new FileStream(pathX, FileMode.Create))
                    {
                        txtMusix.CopyTo(fileStream);
                    }

                    ViewBag.Music = "/karaoke/music/" + fileName;
                    ViewBag.Musix = "/karaoke/music/" + nameFile;

                    fileName = Path.GetFileName(txtKaraoke.FileName);

                    path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fileName);

                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        txtKaraoke.CopyTo(fileStream);
                    }

                    var xu = System.IO.File.ReadAllText(path);
                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(xu);
                        encrypt = true;
                    }
                    catch
                    {
                        encrypt = false;
                    }

                    if (encrypt == true)
                    {
                        xu = StringMaHoaExtension.Decrypt(xu);
                    }
                    ViewBag.Karaoke = xu;
                }
                else
                {
                    var xu = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara/TextDemo.txt"));
                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(xu);
                        encrypt = true;
                    }
                    catch
                    {
                        encrypt = false;
                    }

                    if (encrypt == true)
                    {
                        xu = StringMaHoaExtension.Decrypt(xu);
                    }

                    ViewBag.Karaoke = xu;
                    ViewBag.Music = "/karaoke_Example/ExamKara/KaraokeDemo.mp3";
                    ViewBag.Musix = "/karaoke_Example/ExamKara/NhacDemo.mp3";
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult CreateFile_Karaoke()
        {
            try
            {
                TempData["lyricdemo"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara/LyricDemo.txt")).Replace("\r", "").Replace("\n", "<br />");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                ViewBag.LyricVD = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara/LyricDemo.txt"));
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateFile_Karaoke(IFormFile txtMusic, IFormCollection f)
        {
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }
                fix += "txtMusic : " + ((txtMusic != null) ? txtMusic.FileName : string.Empty) + "\n";

                HttpContext.Session.SetString("hanhdong_3275", fix);

                TempData["lyricdemo"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara/LyricDemo.txt")).Replace("\r", "").Replace("\n", "<br />");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                /*HttpContext.Session.Remove("ok-data");*/
                TempData["dataPost"] = "[POST]";
                HttpContext.Session.SetString("data-result", "true");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                HttpContext.Session.Remove("ok-data");
                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                string xuxuX = xi.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flag = 0;
                var email = false;

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "Email_Karaoke")
                    {
                        if (info[1] == "true")
                            email = true;
                    }

                    if (info[0] == "Email_Karaoke_OnlyText")
                    {
                        if (info[1] == "true")
                            email = false;
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

                TempData["Lyric"] = f["txtLyric"].ToString();
                TempData["TrangTri"] = f["txtTrangTri"].ToString();

                if (f["txtCuoiCung"].ToString() == "on")
                    HttpContext.Session.SetString("CuoiCung", "true");

                if (string.IsNullOrEmpty(f["txtMusic1"].ToString()))
                {
                    TempData["Music"] = "/karaoke/music/" + txtMusic.FileName;

                    var fileName = Path.GetFileName(txtMusic.FileName);

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);

                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        txtMusic.CopyTo(fileStream);
                    }
                }
                else
                {
                    TempData["Music"] = f["txtMusic1"].ToString();
                }

                TempData["IDemail-Karaoke"] = ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "_" + HttpContext.Connection.Id + "_" + xuxuX.Replace(":", "").Replace(" ", "").Replace("/", "");

                if (email == true)
                {
                    var ID = TempData["IDemail-Karaoke"];

                    string host = "{" + Request.Host.ToString() + "}"
                      .Replace("http://", "")
                      .Replace("https://", "")
                      .Replace("/", "");

                    MailRequest mail = new MailRequest();
                    mail.Subject = host + "[ID email Karaoke : " + ID + "] Báo cáo bản mp3 tạo mới Karaoke của user";
                    mail.ToEmail = "mywebplay.savefile@gmail.com";
                    mail.Attachments = new List<IFormFile>();
                    mail.Attachments.Add(txtMusic);
                    mail.Body = "(email bản text tương ứng sẽ được gửi sau khi user tạo xong - cũng tương ứng với ID email này, lưu ý : có thể user sẽ huỷ bỏ bản tạo Karaoke này và sau đó sẽ không nhận được email tương ứng nào; hãy tự chờ đợi và kiểm tra) lúc " + xuxuX;

                    await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
                }

                return RedirectToAction("PlayCreateFile_Karaoke");
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult PlayCreateFile_Karaoke()
        {
            try
            {
                var IDemail = TempData["IDemail-Karaoke"];
                TempData["IDemail-Karaoke"] = IDemail;

                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                HttpContext.Session.SetString("data-result", "true");

                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                ViewBag.KaraX = "";

                var lyric = TempData["Lyric"].ToString();
                var trangtri = TempData["TrangTri"].ToString();
                TempData["TrangTri"] = trangtri;

                var sa = "";

                if (string.IsNullOrEmpty(trangtri) == false)
                {
                    if (HttpContext.Session.GetString("CuoiCung") == "true")
                    {
                        HttpContext.Session.SetString("karaoke_Final", "true");
                    }
                    else
                    {
                        var lyrix = lyric.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < lyrix.Length; i++)
                        {
                            if (lyrix[i] == "\t" || lyrix[i].Contains("Empty") || string.IsNullOrEmpty(lyrix[i]))
                            {
                                sa += lyrix[i];

                                if (i < lyrix.Length - 1)
                                    sa += "\r\n";

                                continue;
                            }

                            var lin = lyrix[i].Split(" ");

                            var s = trangtri + " ";
                            int index = lin.Length / 2;

                            if (lin.Length % 2 != 0)
                                index++;

                            for (int j = 0; j < index; j++)
                            {
                                s += lin[j] + " ";
                            }

                            s += trangtri + " ";

                            for (int j = index; j < lin.Length; j++)
                            {
                                s += lin[j];

                                if (j < lin.Length - 1)
                                    s += " ";
                            }

                            sa += s;

                            if (i < lyrix.Length - 1)
                                sa += "\r\n";
                        }

                        TempData["Lyric"] = sa;
                    }
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult PlayCreateFile_Karaoke(IFormCollection f)
        {
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                /*HttpContext.Session.Remove("ok-data");*/
                TempData["dataPost"] = "[POST]";
                HttpContext.Session.SetString("data-result", "true");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                HttpContext.Session.Remove("ok-data");
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
                var flag = 0;
                var email = false;
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "Email_Karaoke")
                    {
                        if (info[1] == "true")
                            email = true;
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
                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string fi = HttpContext.Connection.Id.ToString() + "_Karaoke_" + xuxu + ".txt";
                fi = fi.Replace("\\", "");
                fi = fi.Replace("/", "");
                fi = fi.Replace(":", "");

                var xanh = f["txtLyric"].ToString().Replace("undefined", "").Replace(" *", "*");

                if (HttpContext.Session.GetString("karaoke_Final") == "true")
                {
                    var trangtri = TempData["TrangTri"].ToString();

                    xanh = BoSungIconTienTo_Karaoke(xanh, trangtri);
                }

                HttpContext.Session.Remove("CuoiCung");
                HttpContext.Session.Remove("karaoke_Final");

                var infoX = listSetting[49].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    xanh = StringMaHoaExtension.Encrypt(xanh);

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fi);

                System.IO.File.WriteAllText(path, xanh);
                ViewBag.KaraX = "OK";
                ViewBag.FileKaraoke = "<p style=\"color:blue\">Thành công, một file TXT Karaoke của bạn đã được xử lý...</p><a href=\"/karaoke/text/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:deeppink\" id=\"thoigian3\" class=\"thoigian3\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>";

                if (email == true)
                {
                    var ID = TempData["IDemail-Karaoke"];

                    string host = "{" + Request.Host.ToString() + "}"
                      .Replace("http://", "")
                      .Replace("https://", "")
                      .Replace("/", "");

                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", host + "[ID email Karaoke : " + ID + "] Báo cáo bản text tạo mới Karaoke của user", "(email bản mp3 tương ứng đã được gửi trước đó / hoặc cũng có thể bạn đã setting không nhận thông báo dữ liệu bản mp3 - vui lòng kiểm tra lại, cũng phù hợp với ID email này) lúc " + xuxu + "\r\n\r\n\r\n\r\n\r\n" + f["txtLyric"].ToString().Replace("undefined", "").Replace(" * ", " * "), "teinnkatajeqerfl");
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        public string BoSungIconTienTo_Karaoke(string text, string icon)
        {
            var noidung = text.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var listText = new List<string>();

            var ok = "";

            for (int i = 0; i < noidung.Length; i++)
            {
                var nd = noidung[i].Split("#");

                if (nd[1] == "[Empty]" || nd[1] == "[Empty-X]" || nd[1].Contains("\t"))
                {
                    listText.Add("[null]");
                    continue;
                }

                var non = icon + " ";
                var kara = nd[1].Replace("*", " ").Split(" ");
                for (int j = 0; j < kara.Length; j++)
                {
                    var space = " ";

                    if (kara.Length % 2 != 0)
                    {
                        if (j == kara.Length / 2)
                            space = "*" + icon + " ";
                    }
                    else
                    {
                        if (j == (kara.Length / 2) - 1)
                            space = "*" + icon + " ";
                    }

                    if (j == kara.Length - 1)
                        space = "";

                    non += kara[j] + space;

                }

                listText.Add(non);
            }

            var listTime = new List<string>();

            var time = "";

            for (int i = 0; i < noidung.Length; i++)
            {
                var nd = noidung[i].Split("#");

                if (nd[1] == "[Empty]" || nd[1] == "[Empty-X]" || nd[1].Contains("\t"))
                {
                    listTime.Add("[null]");
                    continue;
                }

                var nd1 = nd[2].Split(",");
                var ta = int.Parse(nd1[0].Split("-")[0]);
                var tax = int.Parse(nd[0]);
                var de = 0;

                while (true)
                {
                    if (de == 3) break;

                    de++;

                    if (ta - 1 > tax - 1)
                        ta--;
                }

                var so = ta;

                var t = icon.Length + 1;
                time += so + "-" + t + ",";

                for (int j = 0; j < nd1.Length; j++)
                {
                    var nd3 = nd1[j].Split("-");

                    if (nd1.Length % 2 != 0)
                    {
                        if (j == (nd1.Length / 2) + 1)
                        {
                            var nd4 = nd1[j - 1].Split("-");
                            var ta1 = int.Parse(nd3[0]);
                            var tax1 = int.Parse(nd4[0]);
                            var de1 = 0;

                            while (true)
                            {
                                if (de1 == 3) break;

                                de1++;

                                if (ta1 - 1 > tax1)
                                    ta1--;
                            }
                            time += ta1 + "-" + (int.Parse(nd4[1]) + ((icon.Length + 1) * 2)) + ",";
                            t += icon.Length + 1;
                        }
                    }
                    else
                    {
                        if (j == nd1.Length / 2)
                        {
                            var nd4 = nd1[j - 1].Split("-");
                            var ta1 = int.Parse(nd3[0]);
                            var tax1 = int.Parse(nd4[0]);
                            var de1 = 0;

                            while (true)
                            {
                                if (de1 == 3) break;

                                de1++;

                                if (ta1 - 1 > tax1)
                                    ta1--;
                            }
                            time += ta1 + "-" + (int.Parse(nd4[1]) + ((icon.Length + 1) * 2)) + ",";
                            t += icon.Length + 1;
                        }
                    }

                    time += nd3[0] + "-" + (int.Parse(nd3[1]) + t);

                    if (j < nd1.Length - 1)
                        time += ",";

                }

                listTime.Add(time);
                time = "";

            }

            for (int i = 0; i < noidung.Length; i++)
            {
                var nd = noidung[i].Split("#");

                if (nd[1] == "[Empty]" || nd[1] == "[Empty-X]" || nd[1].Contains("\t"))
                {
                    ok += noidung[i];
                }
                else
                {
                    ok += nd[0] + "#" + listText[i] + "#" + listTime[i];
                }

                if (i < noidung.Length - 1)
                    ok += "\r\n";

            }

            return ok;
        }

        public ActionResult XoaKaraoke(string? id, bool? cancel, string? thutu, string? ip)
        {
            try
            {
                if (string.IsNullOrEmpty(ip) == false)
                {
                    HttpContext.Session.SetString("userIP", ip);
                }

                if (string.IsNullOrEmpty(thutu) == false)
                {
                    HttpContext.Session.SetString("karaoke-goto-index", thutu);
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                if (cancel == true || HttpContext.Session.GetString("auto-kara-index") == HttpContext.Session.GetString("length-list-auto"))
                {
                    HttpContext.Session.Remove("auto-kara-index");
                    HttpContext.Session.Remove("length-list-auto");
                    HttpContext.Session.Remove("content-listsong");
                }

                return RedirectToAction("Share_Karaoke", new
                {
                    id = id
                });
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult ToPlayKaraoke(string? boquathongbao = "false")
        {
            try
            {
                if (string.IsNullOrEmpty(boquathongbao))
                    boquathongbao = "false";

                HttpContext.Session.SetString("boquathongbao", boquathongbao);

                if (HttpContext.Session.GetString("length-list-auto") != null || HttpContext.Session.GetString("length-list-auto") != "")
                    TempData["length-list-auto"] = HttpContext.Session.GetString("length-list-auto");

                if (HttpContext.Session.GetString("auto-kara-index") != null || HttpContext.Session.GetString("auto-kara-index") != "")
                    TempData["index-auto"] = HttpContext.Session.GetString("auto-kara-index");

                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");

                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);

                if (TempData["url"] == null)
                    TempData["url"] = "[NOT]";

                if (TempData["baihat"] == null)
                    TempData["baihat"] = "[NOT]";

                if (TempData["background"] == null)
                    TempData["background"] = "[NOT]";

                if (TempData["option"] == null)
                    TempData["option"] = 0;

                string? url = TempData["url"].ToString();
                string? baihat = TempData["baihat"].ToString();
                string? background = TempData["background"].ToString();
                int? option = int.Parse(TempData["option"].ToString());

                if (TempData["settingR"] != null)
                {
                    var setting = TempData["settingR"];
                    TempData["settingR"] = setting;

                }

                var n1 = StringMaHoaExtension.Encrypt(url);
                var n2 = StringMaHoaExtension.Encrypt(baihat);
                var n3 = StringMaHoaExtension.Encrypt(background);

                var share = n1 + "-.-" + n2 + "-.-" + n3 + "-.-" + option;
                ViewBag.LinkKaraoke = share;

                TempData["url"] = null;
                TempData["baihat"] = null;
                TempData["background"] = null;
                TempData["option"] = null;

                //TempData["R-hang"] = null;
                //TempData["R-mau"] = null;
                //    TempData["R-mauX"] = null;
                //    TempData["R-mauY"] = null;
                //    TempData["R-kichthuoc"] = null;
                //    TempData["R-fontkieu"] = null;
                //    TempData["R-kieudemnguoc"] = null;
                //    TempData["R-maunex"] = null;
                //    TempData["R-anhmo"] = null;

                ViewBag.Music = "";
                ViewBag.Musix = "";

                ViewBag.Karaoke = "karaoke";

                if (HttpContext.Session.GetString("auto-kara-index") != null || HttpContext.Session.GetString("auto-kara-index") != "")
                {
                    TempData["index-auto"] = HttpContext.Session.GetString("auto-kara-index");
                }

                TempData["post-kara"] = "false";

                TempData["link-karaoke-demo"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[5];

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                if (url != null && baihat != null && background != null && option != null &&
                  url != "[NOT]" && baihat != "[NOT]" && background != "[NOT]" && option != 0 &&
                  url != "" && baihat != "" && background != "" && option != 0)
                {
                    url = url.Replace("http://", "");
                    url = url.Replace("https://", "");
                    url = url.Replace("/", "");

                    ViewBag.Server = url;
                    ViewBag.BaiHatSV = baihat;
                    try
                    {
                        WebClient client = new WebClient();
                        var https = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
                        Stream stream = client.OpenRead(https + "://" + url + "/MyListSong.txt");
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();
                        ViewBag.ListSong = content;
                        ViewBag.Background = background;
                        ViewBag.Option = option;
                        ViewBag.Share = "YES";

                        HttpContext.Session.SetString("content-listsong", content);

                        TempData["length-list-auto"] = (content.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).Length).ToString();
                        HttpContext.Session.SetString("length-list-auto", TempData["length-list-auto"].ToString());

                        ViewBag.ListSong = content;
                    }
                    catch
                    {
                        ViewBag.Share = "ERROR";
                    }

                }
                else
                if (url != null && url != "[NOT]" && url != "")
                {
                    url = url.Replace("http://", "");
                    url = url.Replace("https://", "");
                    url = url.Replace("/", "");

                    ViewBag.Server = url;
                    ViewBag.BaiHatSV = baihat;

                    try
                    {
                        WebClient client = new WebClient();
                        var https = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
                        Stream stream = client.OpenRead(https + "://" + url + "/MyListSong.txt");
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();
                        HttpContext.Session.SetString("content-listsong", content);

                        TempData["length-list-auto"] = (content.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).Length).ToString();
                        HttpContext.Session.SetString("length-list-auto", TempData["length-list-auto"].ToString());

                        ViewBag.ListSong = content;
                        ViewBag.Share = "OK";
                    }
                    catch
                    {
                        ViewBag.Share = "ERROR";
                    }
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult ToPlayKaraoke(IFormCollection f, IFormFile txtKaraoke, IFormFile txtMusic, IFormFile txtMusix)
        {
            try
            {
                TempData["boquathongbao"] = HttpContext.Session.GetString("boquathongbao");
                HttpContext.Session.Remove("boquathongbao");
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                fix += "txtKaraoke : " + ((txtKaraoke != null) ? txtKaraoke.FileName : string.Empty) + "\n";
                fix += "txtMusic : " + ((txtMusic != null) ? txtMusic.FileName : string.Empty) + "\n";
                fix += "txtMusix : " + ((txtMusix != null) ? txtMusix.FileName : string.Empty) + "\n";
                fix += "txtMusic1 : " + f["txtMusic1"].ToString() + "\n";

                HttpContext.Session.SetString("hanhdong_3275", fix);

                if (HttpContext.Session.GetString("length-list-auto") != null || HttpContext.Session.GetString("length-list-auto") != "")
                    TempData["length-list-auto"] = HttpContext.Session.GetString("length-list-auto");

                if (HttpContext.Session.GetString("auto-kara-index") != null || HttpContext.Session.GetString("auto-kara-index") != "")
                    TempData["index-auto"] = HttpContext.Session.GetString("auto-kara-index");

                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
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
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                /*HttpContext.Session.Remove("ok-data");*/
                TempData["dataPost"] = "[POST]";
                HttpContext.Session.SetString("data-result", "true");
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                HttpContext.Session.Remove("ok-data");
                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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
                var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX1);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flag = 0;
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

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

                if (txtMusic == null || txtMusic.Length == 0)
                {
                    txtMusic = txtMusix;
                    TempData["casi"] = "false";
                }

                if (txtMusix == null || txtMusix.Length == 0)
                {
                    txtMusix = txtMusic;
                    TempData["casi"] = "false";
                }

                TempData["casi"] = "true";

                if (TempData["settingR"] != null)
                {
                    var setting = TempData["settingR"].ToString();
                    var set = setting.Split("<>");
                    if (set[0] == "[NULL]")
                        TempData["R-hang"] = null;
                    else
                        TempData["R-hang"] = set[0];

                    if (set[1] == "[NULL]")
                        TempData["R-mau"] = null;
                    else
                        TempData["R-mau"] = set[1];

                    TempData["R-mauX"] = set[2];
                    TempData["R-mauY"] = set[3];
                    TempData["R-kichthuoc"] = set[4];
                    TempData["R-fontkieu"] = set[5];
                    TempData["R-kieudemnguoc"] = set[6];
                    TempData["R-maunex"] = set[7];
                    TempData["R-anhmo"] = set[8];
                }

                TempData["post-kara"] = "true";

                ViewBag.Host = Request.Host;

                ViewBag.Karaoke = "";
                ViewBag.Share = f["txtShare"] + " - OK";

                ViewBag.Show = "show";

                TempData["fontKara"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "karaoke_font.txt"));

                ViewBag.LoginServer = f["txtServer"].ToString();
                ViewBag.BHServer = f["txtSong"].ToString();

                var chon = f["KaraChon"].ToString();

                if (f["auto_play"].ToString() == "on")
                {
                    TempData["auto-play"] = "false";
                }
                else
                {
                    TempData["auto-play"] = "true";
                }

                if (f["auto_music"].ToString() == "on")
                {
                    TempData["auto-music"] = "true";
                }
                else
                {
                    TempData["auto-music"] = "false";
                }

                if (f["txtBehind"].ToString() == "on")
                {
                    TempData["behind"] = "true";
                    TempData["behind-number"] = f["txtBehindNumber"].ToString();
                }
                else
                {
                    TempData["behind"] = "false";
                }

                if (f["txtInHoa"].ToString() == "on")
                {
                    TempData["inhoa"] = "true";
                }
                else
                {
                    TempData["inhoa"] = "false";
                }

                if (f["txtCanGiua"].ToString() == "on")
                {
                    TempData["cangiua"] = "true";
                }
                else
                {
                    TempData["cangiua"] = "false";
                }

                if (f.ContainsKey("txtCoDinh"))
                {

                    if (f["txtCoDinh"].ToString() == "on")
                    {
                        TempData["codinh"] = "true";
                    }
                    else
                    {
                        TempData["codinh"] = "false";
                    }
                }
                else
                {
                    TempData["codinh"] = "false";
                }

                ViewBag.Option = chon;

                if (chon == "1")
                {
                    var r = new Random();

                    var n = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example/background")).GetFiles().Length;
                    ViewBag.SoLuongNgauNhien = n.ToString();
                    int x = r.Next(n);

                    if (TempData["Karaoke_RandomImage"] == "true")
                        ViewBag.Background = "/karaoke_Example/background/1.jpg";
                    else
                        ViewBag.Background = "/karaoke_Example/background/" + (x + 1) + ".jpg";

                    ViewBag.SuDung = "";
                    ViewBag.NgauNhien = "true";
                }
                else if (chon == "2")
                {
                    var link = f["txtOnline"].ToString();
                    ViewBag.Background = link;
                    ViewBag.SuDung = "";
                }
                else if (chon == "3")
                {
                    var link = f["txtOnline"].ToString();
                    ViewBag.Background = link;
                    ViewBag.SuDung = "Video";
                }
                else if (chon == "4")
                {
                    ViewBag.BehindYoutube = "YES";

                    var link = f["txtOnline"].ToString();

                    link = link.Replace("&", "");
                    link = link.Replace("loop", "");
                    link = link.Replace("autoplay", "");
                    link = link.Replace("controls", "");
                    link = link.Replace("mute", "");
                    link = link.Replace("youtu.be/", "youtube.com/embed/");
                    link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                    TempData["id-youtube"] = link.Replace("https://www.youtube.com/embed/", "");

                    var id = link.Replace("https://www.youtube.com/embed/", "");

                    try
                    {
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead("https://www.googleapis.com/youtube/v3/videos?id=" + id + "&part=contentDetails&key=AIzaSyD5bK5gubDOrvgoHvWg-OTbDqVogqe6R_8");
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();

                        var part1 = content.Split("\"duration\": ");
                        var part2 = part1[1].Split("\",");
                        var durationYoutube = part2[0].Replace("\"", "");

                        var h = 0;
                        var m = 0;
                        var s = 0;

                        if (durationYoutube.StartsWith("PT"))
                        {
                            durationYoutube = durationYoutube.Replace("PT", "");

                            if (durationYoutube.Contains("H"))
                            {
                                h = int.Parse(durationYoutube.Split("H")[0]);
                            }

                            durationYoutube = durationYoutube.Replace(h + "H", "").Replace("H", "");

                            if (durationYoutube.Contains("M"))
                            {
                                m = int.Parse(durationYoutube.Split("M")[0]);
                            }

                            durationYoutube = durationYoutube.Replace(m + "M", "").Replace("M", "");

                            if (durationYoutube.Contains("S"))
                            {
                                s = int.Parse(durationYoutube.Split("S")[0]);
                            }

                            TempData["duration-Youtube"] = (h * 3600 + m * 60 + s) + "";
                        }
                        else
                        {
                            TempData["duration-Youtube"] = "no";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    if (link.Contains("youtube") == false)
                        ViewBag.Share = "ERROR";

                    if (link.Contains("?"))
                        link += "&autoplay=1&loop=1&controls=0&mute=1";
                    else
                        link += "?autoplay=1&loop=1&controls=0&mute=1";

                    ViewBag.Background = link;
                    ViewBag.SuDung = "Youtube";
                }
                else if (chon == "5")
                {
                    ViewBag.BehindYoutube = "YES";

                    var listYoutube = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "Video_Youtube", "randomlink.txt")).Replace("\r", "").Split("\n");

                    var rand = new Random();
                    var link = listYoutube[rand.Next(0, listYoutube.Length)];

                    link = link.Replace("&", "");
                    link = link.Replace("loop", "");
                    link = link.Replace("autoplay", "");
                    link = link.Replace("controls", "");
                    link = link.Replace("mute", "");
                    link = link.Replace("youtu.be/", "youtube.com/embed/");
                    link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                    TempData["id-youtube"] = link.Replace("https://www.youtube.com/embed/", "");

                    var id = link.Replace("https://www.youtube.com/embed/", "");

                    try
                    {
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead("https://www.googleapis.com/youtube/v3/videos?id=" + id + "&part=contentDetails&key=AIzaSyD5bK5gubDOrvgoHvWg-OTbDqVogqe6R_8");
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();

                        var part1 = content.Split("\"duration\": ");
                        var part2 = part1[1].Split("\",");
                        var durationYoutube = part2[0].Replace("\"", "");

                        var h = 0;
                        var m = 0;
                        var s = 0;

                        if (durationYoutube.StartsWith("PT"))
                        {
                            durationYoutube = durationYoutube.Replace("PT", "");

                            if (durationYoutube.Contains("H"))
                            {
                                h = int.Parse(durationYoutube.Split("H")[0]);
                            }

                            durationYoutube = durationYoutube.Replace(h + "H", "").Replace("H", "");

                            if (durationYoutube.Contains("M"))
                            {
                                m = int.Parse(durationYoutube.Split("M")[0]);
                            }

                            durationYoutube = durationYoutube.Replace(m + "M", "").Replace("M", "");

                            if (durationYoutube.Contains("S"))
                            {
                                s = int.Parse(durationYoutube.Split("S")[0]);
                            }

                            TempData["duration-Youtube"] = (h * 3600 + m * 60 + s) + "";
                        }
                        else
                        {
                            TempData["duration-Youtube"] = "no";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    if (link.Contains("youtube") == false)
                        ViewBag.Share = "ERROR";

                    if (link.Contains("?"))
                        link += "&autoplay=1&loop=1&controls=0&mute=1";
                    else
                        link += "?autoplay=1&loop=1&controls=0&mute=1";

                    ViewBag.Background = link;
                    ViewBag.SuDung = "Youtube";
                }

                var ct = HttpContext.Session.GetString("content-listsong");

                if (f["txtOnlineServer"].ToString() == "on")
                {
                    var server = f["txtServer"].ToString();
                    var song = f["txtSong"].ToString();

                    var url_txt = server + "/" + song + "/" + song + ".txt";
                    var url_goc = server + "/" + song + "/" + song + "_Goc.mp3";
                    var url_kara = server + "/" + song + "/" + song + ".mp3";

                    try
                    {
                        WebClient client = new WebClient();
                        var https = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
                        Stream stream = client.OpenRead(https + "://" + url_txt);
                        StreamReader reader = new StreamReader(stream);
                        string content = reader.ReadToEnd();

                        //if (f["encrypt_data"].ToString() == "on" || HttpContext.Session.GetString("encrypt_yes") == "true")
                        //{
                        //    content = StringMaHoaExtension.Decrypt(content);
                        //}

                        var encrypt = false;
                        try
                        {
                            StringMaHoaExtension.Decrypt(content);
                            encrypt = true;
                        }
                        catch
                        {
                            encrypt = false;
                        }

                        if (encrypt == true)
                        {
                            content = StringMaHoaExtension.Decrypt(content);
                        }

                        //HttpContext.Session.Remove("encrypt_yes");

                        var httpx = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";

                        ViewBag.Music = httpx + "://" + url_kara;
                        ViewBag.Musix = httpx + "://" + url_goc;

                        if (content.Contains("<>") == false)
                        {
                            ViewBag.Karaoke = content;
                            TempData["TK-KARA"] = "";
                            if (TempData["hassinger"] != "true")
                                TempData["hassinger"] = "false";
                        }
                        else
                        {
                            var xa = content.Replace("\r", "").Split("\n");
                            var noidung = "";
                            for (int i = 0; i < xa.Length; i++)
                            {
                                if (xa[i].Contains("<>"))
                                {
                                    var xb = xa[i].Split("<>");
                                    var xc = xb[1].Split("#");
                                    var xd = xb[0].Split("-");
                                    noidung += xc[0] + "=" + xd[0] + "=" + xd[1];

                                    if (xd[0] == "[SINGER]")
                                    {
                                        TempData["hassinger"] = "true";
                                    }
                                    else
                                    {
                                        if (TempData["hassinger"] != "true")
                                            TempData["hassinger"] = "false";
                                    }

                                    content = content.Replace(xb[0] + "<>", "");

                                    if (i < xa.Length - 1)
                                        noidung += "\n";
                                }
                            }
                            TempData["TK-KARA"] = noidung;
                            ViewBag.Karaoke = content;
                        }
                    }
                    catch
                    {
                        ViewBag.Share = "ERROR";
                    }
                }
                else
                if (f["txtChon"].ToString() != "on")
                {
                    if (string.IsNullOrEmpty(f["txtMusic1"].ToString()))
                    {
                        var fileName = Path.GetFileName(txtMusic.FileName);
                        ViewBag.Music = "/karaoke/music/" + fileName;

                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);


                        using (Stream fileStream = new FileStream(path, FileMode.Create))
                        {
                            txtMusic.CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        ViewBag.Music = f["txtMusic1"].ToString();
                    }

                    if (string.IsNullOrEmpty(f["txtMusix1"].ToString()))
                    {
                        var nameFile = Path.GetFileName(txtMusix.FileName);
                        ViewBag.Musix = "/karaoke/music/" + nameFile;

                        var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", nameFile);

                        using (Stream fileStream = new FileStream(pathX, FileMode.Create))
                        {
                            txtMusix.CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        ViewBag.Musix = f["txtMusix1"].ToString();
                    }

                    //-----------------------------------------------

                    var nd = "";

                    if (string.IsNullOrEmpty(f["txtKaraoke1"].ToString()))
                    {

                        var fileName1 = Path.GetFileName(txtKaraoke.FileName);

                        var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fileName1);

                        using (Stream fileStream = new FileStream(path1, FileMode.Create))
                        {
                            txtKaraoke.CopyTo(fileStream);
                        }
                        nd = System.IO.File.ReadAllText(path1);
                    }
                    else
                    {
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(f["txtKaraoke1"].ToString());
                        StreamReader reader = new StreamReader(stream);
                        nd = reader.ReadToEnd();
                    }

                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(nd);
                        encrypt = true;
                        TempData["data-encrypt"] = "true";
                    }
                    catch
                    {
                        encrypt = false;
                        TempData["data-encrypt"] = "false";
                    }

                    if (encrypt == true)
                    {
                        nd = StringMaHoaExtension.Decrypt(nd);
                    }

                    if (nd.Contains("<>") == false)
                    {
                        ViewBag.Karaoke = nd;
                        TempData["TK-KARA"] = "";

                        if (TempData["hassinger"] != "true")
                            TempData["hassinger"] = "false";
                    }
                    else
                    {
                        var xa = nd.Replace("\r", "").Split("\n");
                        var noidung = "";
                        for (int i = 0; i < xa.Length; i++)
                        {
                            if (xa[i].Contains("<>"))
                            {
                                var xb = xa[i].Split("<>");
                                var xc = xb[1].Split("#");
                                var xd = xb[0].Split("-");
                                noidung += xc[0] + "=" + xd[0] + "=" + xd[1];

                                if (xd[0] == "[SINGER]")
                                {
                                    TempData["hassinger"] = "true";
                                }
                                else
                                {
                                    if (TempData["hassinger"] != "true")
                                        TempData["hassinger"] = "false";
                                }

                                nd = nd.Replace(xb[0] + "<>", "");

                                if (i < xa.Length - 1)
                                    noidung += "\n";
                            }
                        }
                        TempData["TK-KARA"] = noidung;
                        ViewBag.Karaoke = nd;
                    }
                }
                else
                {
                    var nd = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "ExamKara/TextDemo.txt"));
                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(nd);
                        encrypt = true;
                        TempData["data-encrypt"] = "true";
                    }
                    catch
                    {
                        encrypt = false;
                        TempData["data-encrypt"] = "false";
                    }

                    if (encrypt == true)
                    {
                        nd = StringMaHoaExtension.Decrypt(nd);
                    }

                    if (nd.Contains("<>") == false)
                    {
                        ViewBag.Karaoke = nd;
                        TempData["TK-KARA"] = "";

                        if (TempData["hassinger"] != "true")
                            TempData["hassinger"] = "false";
                    }
                    else
                    {
                        var xa = nd.Replace("\r", "").Split("\n");
                        var noidung = "";
                        for (int i = 0; i < xa.Length; i++)
                        {
                            if (xa[i].Contains("<>"))
                            {
                                var xb = xa[i].Split("<>");
                                var xc = xb[1].Split("#");
                                var xd = xb[0].Split("-");
                                noidung += xc[0] + "=" + xd[0] + "=" + xd[1];

                                if (xd[0] == "[SINGER]")
                                {
                                    TempData["hassinger"] = "true";
                                }
                                else
                                {
                                    if (TempData["hassinger"] != "true")
                                        TempData["hassinger"] = "false";
                                }

                                nd = nd.Replace(xb[0] + "<>", "");

                                if (i < xa.Length - 1)
                                    noidung += "\n";
                            }
                        }
                        TempData["TK-KARA"] = noidung;
                        ViewBag.Karaoke = nd;
                    }
                    ViewBag.Music = "/karaoke_Example/ExamKara/KaraokeDemo.mp3";
                    ViewBag.Musix = "/karaoke_Example/ExamKara/NhacDemo.mp3";
                }

                if (f["txtAutoSong"].ToString() == "on")
                {
                    if (HttpContext.Session.GetString("auto-kara-index") == null || HttpContext.Session.GetString("auto-kara-index") == "")
                    {
                        HttpContext.Session.SetString("auto-kara-index", "1");
                    }
                    else
                    {
                        int x = (HttpContext.Session.GetString("karaoke-goto-index") != null) ? int.Parse(HttpContext.Session.GetString("karaoke-goto-index")) : int.Parse(HttpContext.Session.GetString("auto-kara-index")) + 1;
                        if (HttpContext.Session.GetString("karaoke-goto-index") == null)
                            HttpContext.Session.SetString("auto-kara-index", x + "");
                        else
                            HttpContext.Session.SetString("auto-kara-index", x + "");
                        HttpContext.Session.Remove("karaoke-goto-index");

                    }
                    TempData["index-auto"] = HttpContext.Session.GetString("auto-kara-index");
                    TempData["length-list-auto"] = (ct.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries).Length).ToString();
                    HttpContext.Session.SetString("length-list-auto", TempData["length-list-auto"].ToString());
                }

                var n1 = StringMaHoaExtension.Encrypt(ViewBag.LoginServer);
                var n2 = StringMaHoaExtension.Encrypt(ViewBag.BHServer);
                var n3 = StringMaHoaExtension.Encrypt(ViewBag.Background);
                ViewBag.LinkKaraoke = n1 + "-.-" + n2 + "-.-" + n3 + "-.-" + chon;
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }
    }
}