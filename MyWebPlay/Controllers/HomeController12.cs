﻿using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Text.RegularExpressions;
using Ganss.Xss;
using System;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult TracNghiem_Multiple(int? sl)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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
                HttpContext.Session.Remove("TracNghiem");

                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    if (f.Exists)
                    f.Delete();
                }

                if (sl == null)
                    ViewBag.SL = 0;

                ViewBag.SL = sl;

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult TracNghiemX_Multiple()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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
                HttpContext.Session.Remove("TracNghiem");
                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    if (f.Exists)
                        f.Delete();
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
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        [HttpPost]
        public ActionResult TracNghiem_Multiple(IFormCollection f, List<IFormFile> txtFile)
        {
            try
            {
                //var pathSecure = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                //var noidungSecure = FileExtension.ReadFile(pathSecure);
                //var fileMOTAT = noidungSecure.Replace("\r", "").Split("\n")[3];
                //if (fileMOTAT == "file_TAT" && txtFile != null && txtFile.Count > 0) return RedirectToAction("Error", "Home");

                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                int i = 0;
                foreach (var item in txtFile)
                {
                    fix += string.Format("txtFile[{0}] : {1}\n", i + "", item.FileName);
                    i++;
                }
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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
                var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
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
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flax = 0;
                for (i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (flax == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flax = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flax = 0;
                        }
                    }
                }
                int sl = int.Parse(f["txtSoLuong"].ToString());
                ViewBag.SL = sl;

                string txtSoCau = f["txtSoCau"].ToString();
                string txtTime = f["txtTime"].ToString();
                string txtMon = f["txtMon"].ToString();
                if (string.IsNullOrWhiteSpace(txtMon))
                {
                    txtMon = txtFile[0].FileName;
                }

                if (string.IsNullOrEmpty(txtSoCau))
                {
                    ViewData["Loi2"] = "Không được bỏ trống trường này";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiem_Multiple(ViewBag.SL);
                }

                if (string.IsNullOrEmpty(txtTime))
                {
                    ViewData["Loi3"] = "Không được bỏ trống trường này";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiem_Multiple(ViewBag.SL);
                }

                int time = int.Parse(txtTime);
                if (time < 1 || time > 1200)
                {
                    ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiem_Multiple(ViewBag.SL);
                }

                if (txtFile.Count() < sl)
                {
                    ViewData["Loi1-" + txtFile.Count()] = "Mời bạn chọn file TXT trắc nghiệm cho chương " + (txtFile.Count() + 1) + "...";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiem_Multiple(ViewBag.SL);
                }

                int n9_S = 0;

                //------

                TracNghiem[] tn = new TracNghiem[sl];

                for (int h = 0; h < txtFile.Count(); h++)
                {
                    tn[h] = new TracNghiem();

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", Path.GetFileName(txtFile[h].FileName));

                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        txtFile[h].CopyTo(fileStream);

                    }

                    var xu = FileExtension.ReadFile(path);

                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(xu).Replace("\r\n", "\n");
                        encrypt = true;
                    }
                    catch
                    {
                        encrypt = false;
                    }

                    if (encrypt == true)
                    {
                        xu = StringMaHoaExtension.Decrypt(xu).Replace("\r\n", "\n");
                    }

                    String ND_file = xu;

                    var sanitizer = new HtmlSanitizer();
                    sanitizer.AllowedTags.Clear();
                    sanitizer.AllowedAttributes.Clear();
                    var pathSD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Others", "HtmlSanitizerAccept.txt");
                    var noidungSD = FileExtension.ReadFile(pathSD).Replace("\r", "").Split("\n==========\n");
                    for (var iss = 0; iss < noidungSD.Length; iss++)
                    {
                        var htmlSanitizerAccept = noidungSD[iss].Replace("\r", "").Split("\n");
                        for (var joo = 0; joo < htmlSanitizerAccept.Length; joo++)
                        {
                            if (iss == 0)
                            {
                                sanitizer.AllowedTags.Add(htmlSanitizerAccept[joo]);
                            }
                            else
                            {
                                sanitizer.AllowedAttributes.Add(htmlSanitizerAccept[joo]);
                            }
                        }
                    }

                    // 🔹 Chặn "javascript:" và "expression()" trong style
                    sanitizer.RemovingAttribute += (s, e) =>
                    {
                        string lowerValue = e.Attribute.Value.ToLower();
                        if (lowerValue.Contains("javascript:") || lowerValue.Contains("expression("))
                        {
                            e.Cancel = true; // Hủy bỏ giá trị nguy hiểm
                        }
                    };

                    var socautatca = ND_file.Replace("\r", "").Split("\n#\n").Length;
                    for (var index = 0; index < socautatca; index++)
                    {
                        ND_file = ND_file.Replace("[<?" + index + "?>]", "DARKAXNA_TRACNGHIEM_" + index);
                    }

                    ND_file = sanitizer.Sanitize(ND_file);
                    for (var index = 0; index < socautatca; index++)
                    {
                        ND_file = ND_file.Replace("DARKAXNA_TRACNGHIEM_" + index, "[<?" + index + "?>]");
                    }

                    FileInfo fx = new FileInfo(path);
                    if (fx.Exists)
                        fx.Delete();

                    if (ND_file.Length == 0)
                    {
                        ViewData["Loi1-" + h] = "Bài kiểm tra hay file văn bản chương " + (h + 1) + " của bạn hiện chưa có nội dung nào!";
                        HttpContext.Session.SetString("data-result", "true");
                        return this.TracNghiem_Multiple(ViewBag.SL);
                    }

                    String[] split = {
            "\n#\n"
          };
                    String[] t1 = ND_file.Replace("\r", "").Split(split, StringSplitOptions.RemoveEmptyEntries);

                    for (i = 0; i < t1.Length; i++)
                    {
                        String[] t2 = t1[i].Replace("\r", "").Split('\n');
                        if (t2.Length != 6)
                        {

                            string err = "WRONG INDEX QUESTION [CHƯƠNG " + (i + 1) + "] : " + t2[0] + "";
                            //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";
                            ViewData["Loi1-" + h] = err;
                            HttpContext.Session.SetString("data-result", "true");
                            return this.TracNghiem_Multiple(ViewBag.SL);
                        }

                        char[] t2_x = t2[5].ToCharArray();
                        if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                        {
                            string err = "WRONG INDEX QUESTION [CHƯƠNG " + (i + 1) + "] : " + t2[0] + "";
                            //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                            ViewData["Loi1-" + h] = err;
                            HttpContext.Session.SetString("data-result", "true");
                            return this.TracNghiem_Multiple(ViewBag.SL);
                        }
                    }

                    //-------------------

                    for (i = 0; i < t1.Length; i++)
                    {
                        String[] t2 = t1[i].Replace("\r", "").Split('\n');
                        int flag = 0;
                        String DA = t2[t2.Length - 1].Replace("[", "");
                        DA = DA.Replace("]", "");
                        for (int j = t2.Length - 2; j > 0; j--)
                        {
                            if (DA.CompareTo(t2[j]) == 0)
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            //MessageBox.Show("Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi.\nXin lỗi vì sự bất tiện này! ]");
                            ViewData["Loi1-" + h] = "WRONG INDEX ANSWER OF QUESTION [CHƯƠNG " + (h + 1) + "] : " + t2[0] + "";
                            HttpContext.Session.SetString("data-result", "true");
                            return this.TracNghiem_Multiple(ViewBag.SL);
                        }
                    }

                    if (f["txtMucDox"].ToString() == "on")
                    {
                        var s = 0;
                        for (int ij = 0; ij < txtFile.Count; ij++)
                        {
                            s += int.Parse(f["txtMucDo-" + ij].ToString());
                        }
                        if (s != 100)
                        {
                            ViewData["LoiMucDo"] = "Vui lòng chỉ định từng mức độ phân phối các câu hỏi cho mỗi chương sao cho tổng là 100%";
                            HttpContext.Session.SetString("data-result", "true");
                            return this.TracNghiem_Multiple(ViewBag.SL);
                        }
                    }

                    int[] chuaxet_ch;
                    int[][] chuaxet_da;

                    int n9 = t1.Length;
                    n9_S += n9;

                    tn[h].ch = new String[t1.Length];
                    tn[h].a = new String[t1.Length];
                    tn[h].b = new String[t1.Length];
                    tn[h].c = new String[t1.Length];
                    tn[h].d = new String[t1.Length];
                    tn[h].dung = new String[t1.Length];

                    chuaxet_ch = new int[t1.Length];

                    chuaxet_da = new int[t1.Length][];

                    for (i = 0; i < t1.Length; i++)
                        chuaxet_da[i] = new int[5];

                    //=======

                    int dem = 0;
                    while (true)
                    {
                        if (dem == t1.Length)
                            break;

                        Random r = new Random();
                        double x = r.Next(0, t1.Length);

                        if (chuaxet_ch[int.Parse(x.ToString())] == 1)
                            continue;

                        chuaxet_ch[int.Parse(x.ToString())] = 1;
                        i = int.Parse(x.ToString());
                        String[] t2 = t1[i].Replace("\r", "").Split('\n');

                        char[] CH = t2[0].ToCharArray();

                        if (CH[0] == '$')
                        {
                            t2[0].Remove(0, 1);
                            tn[h].ch[dem] = t2[0].Replace("$", "");
                            tn[h].a[dem] = t2[1];
                            tn[h].b[dem] = t2[2];
                            tn[h].c[dem] = t2[3];
                            tn[h].d[dem] = t2[4];
                            String DAx = t2[5].Replace("[", "");
                            DAx = DAx.Replace("]", "");
                            tn[h].dung[dem] = DAx;
                        }
                        else
                        {
                            int aa, bb, cc, dd;

                            tn[h].ch[dem] = t2[0];

                            do
                            {
                                aa = r.Next(1, 5);
                            }
                            while (chuaxet_da[dem][int.Parse(aa.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(aa.ToString())] = 1;

                            tn[h].a[dem] = t2[aa];

                            do
                            {
                                bb = r.Next(1, 5);
                            }
                            while (chuaxet_da[dem][int.Parse(bb.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(bb.ToString())] = 1;

                            tn[h].b[dem] = t2[bb];

                            do
                            {
                                cc = r.Next(1, 5);
                            }
                            while (chuaxet_da[dem][int.Parse(cc.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(cc.ToString())] = 1;

                            tn[h].c[dem] = t2[cc];

                            do
                            {
                                dd = r.Next(1, 5);
                            }
                            while (chuaxet_da[dem][int.Parse(dd.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(dd.ToString())] = 1;

                            tn[h].d[dem] = t2[dd];
                            String DA = t2[5].Replace("[", "");
                            DA = DA.Replace("]", "");
                            tn[h].dung[dem] = DA;
                        }
                        dem++;
                    }
                    tn[h].tongsocau = n9;
                }

                TracNghiem tnX = new TracNghiem();
                tnX.ch = new String[int.Parse(txtSoCau)];
                tnX.a = new String[int.Parse(txtSoCau)];
                tnX.b = new String[int.Parse(txtSoCau)];
                tnX.c = new String[int.Parse(txtSoCau)];
                tnX.d = new String[int.Parse(txtSoCau)];
                tnX.dung = new String[int.Parse(txtSoCau)];

                tnX.tongsocau = n9_S;

                if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9_S)
                {
                    txtSoCau = n9_S.ToString();
                }

                tnX.gioihancau = int.Parse(txtSoCau);

                int[][] chuaxetX = new int[sl][];

                for (i = 0; i < sl; i++)
                {
                    chuaxetX[i] = new int[tn[i].tongsocau];
                    for (int j = 0; j < chuaxetX[i].Length; j++)
                    {
                        chuaxetX[i][j] = 0;
                    }
                }

                var listMucDo = new int[sl];
                var checkMucDo = new int[sl];
                if (f["txtMucDox"].ToString() == "on")
                {
                    for (int y = 0; y < sl; y++)
                    {
                        var socau = (tnX.gioihancau * (int.Parse(f["txtMucDo-" + y]))) / 100;
                        if (socau > tn[y].tongsocau)
                        {
                            ViewData["LoiMucDo"] = "Hệ thống không thể xử lý dựa trên các mức độ phân phối mà bạn đã đặt ra, vui lòng kiểm tra lại...";
                            HttpContext.Session.SetString("data-result", "true");
                            return this.TracNghiem_Multiple(ViewBag.SL);
                        }
                        listMucDo[y] = socau;
                        checkMucDo[y] = 0;
                    }

                    var s = 0;
                    for (int y = 0; y < sl; y++)
                    {
                        s += listMucDo[y];
                    }

                    if (s != tnX.gioihancau)
                    {
                        Random r = new Random();
                        for (int z = 0; z < tnX.gioihancau - s; z++)
                        {
                            int chuong = r.Next(0, sl);
                            listMucDo[chuong]++;

                        }
                    }
                }

                for (i = 0; i < tnX.gioihancau; i++)
                {
                    if (f["txtMucDox"].ToString() == "on")
                    {
                        Random r = new Random();
                        int chuong;
                        int soluong;
                        do
                        {
                            chuong = r.Next(0, sl);
                            soluong = r.Next(0, tn[chuong].tongsocau);
                        }
                        while (chuaxetX[chuong][soluong] == 1 || checkMucDo[chuong] == listMucDo[chuong]);

                        chuaxetX[chuong][soluong] = 1;
                        checkMucDo[chuong]++;

                        tnX.ch[i] = tn[chuong].ch[soluong];
                        tnX.a[i] = tn[chuong].a[soluong];
                        tnX.b[i] = tn[chuong].b[soluong];
                        tnX.c[i] = tn[chuong].c[soluong];
                        tnX.d[i] = tn[chuong].d[soluong];
                        tnX.dung[i] = tn[chuong].dung[soluong];
                    }
                    else
                    {
                        Random r = new Random();
                        int chuong;
                        int soluong;
                        do
                        {
                            chuong = r.Next(0, sl);
                            soluong = r.Next(0, tn[chuong].tongsocau);
                        }
                        while (chuaxetX[chuong][soluong] == 1);

                        chuaxetX[chuong][soluong] = 1;

                        tnX.ch[i] = tn[chuong].ch[soluong];
                        tnX.a[i] = tn[chuong].a[soluong];
                        tnX.b[i] = tn[chuong].b[soluong];
                        tnX.c[i] = tn[chuong].c[soluong];
                        tnX.d[i] = tn[chuong].d[soluong];
                        tnX.dung[i] = tn[chuong].dung[soluong];
                    }
                }

                tnX.timelambai = int.Parse(txtTime);
                tnX.tenmon = txtMon;

                ViewBag.TimeLamBai = tnX.timelambai;

                HttpContext.Session.SetObject("TracNghiem", tnX);

                ViewBag.TongSoCau = tnX.tongsocau;
                ViewBag.GioiHanCau = tnX.gioihancau;
                ViewBag.TimeLamBaiX = tnX.timelambai;
                ViewBag.TenMon = tnX.tenmon;

                ViewBag.CauHoi = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.ch), "32752006");
                ViewBag.A = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.a), "32752006");
                ViewBag.B = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.b), "32752006");
                ViewBag.C = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.c), "32752006");
                ViewBag.D = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.d), "32752006");
                ViewBag.Dung = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.dung), "32752006");

                ViewBag.KetQuaDung = "";

                return View("PlayTracNghiem", tnX);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        [HttpPost]
        public ActionResult TracNghiemX_Multiple(IFormCollection f, List<IFormFile> txtFile)
        {
            try
            {
                //var pathSecure = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                //var noidungSecure = FileExtension.ReadFile(pathSecure);
                //var fileMOTAT = noidungSecure.Replace("\r", "").Split("\n")[3];
                //if (fileMOTAT == "file_TAT" && txtFile != null && txtFile.Count > 0) return RedirectToAction("Error", "Home");

                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                int i = 0;
                foreach (var item in txtFile)
                {
                    fix += string.Format("txtFile[{0}] : {1}\n", i + "", item.FileName);
                    i++;
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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

                var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
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
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flax = 0;
                for (i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (flax == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flax = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flax = 0;
                        }
                    }
                }
                int sl = txtFile.Count();
                var cFile = "";

                string txtSoCau = f["txtSoCau"].ToString();
                var tick = f["txtTick"].ToString();

                string txtTime = f["txtTime"].ToString();
                string txtMon = f["txtMon"].ToString();

                if (f["cbCoSan"].ToString() != "on")
                {
                    if (tick != "on" && string.IsNullOrWhiteSpace(txtMon))
                    {
                        txtMon = txtFile[0].FileName;
                    }
                }
                else
                {
                    txtMon = "Mẫu trắc nghiệm của hệ thống";
                }

                if (tick != "on" && string.IsNullOrEmpty(txtSoCau))
                {
                    ViewData["Loi2"] = "Không được bỏ trống trường này";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiemX_Multiple();
                }

                if (tick != "on" && string.IsNullOrEmpty(txtTime))
                {
                    ViewData["Loi3"] = "Không được bỏ trống trường này";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TracNghiemX_Multiple();
                }

                if (tick != "on")
                {
                    int time = int.Parse(txtTime);
                    if (time < 1 || time > 1200)
                    {
                        ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                        HttpContext.Session.SetString("data-result", "true");
                        return this.TracNghiemX_Multiple();
                    }
                }

                if (f["cbCoSan"].ToString() != "on")
                {
                    if (tick != "on" && txtFile.Count() <= 0)
                    {
                        ViewData["Loi1"] = "Mời bạn chọn file TXT trắc nghiệm (có thể chọn nhiều file thể hiện một môn học trắc nghiệm có nhiều chương/mục/phần/bài)...";
                        HttpContext.Session.SetString("data-result", "true");
                        return this.TracNghiemX_Multiple();
                    }
                }

                if (f["cbCoSan"].ToString() != "on")
                {
                    if (tick == "on" && txtFile.Count() != 1)
                    {
                        ViewData["Loi1"] = "Xin lỗi nếu bạn sử dụng tính năng này để setting lại answer cho file trắc nghiệm của bạn, bạn chỉ có thể tải lên tương đương 1 file - vui lòng tải lên 1 file trắc nghiệm của bạn!";
                        HttpContext.Session.SetString("data-result", "true");
                        return this.TracNghiemX_Multiple();
                    }
                }

                int n9_S = 0;

                //------

                if (f["cbCoSan"].ToString() == "on")
                {
                    sl = 1;
                }

                TracNghiem[] tn = new TracNghiem[sl];

                var solug = 1;
                if (f["cbCoSan"].ToString() != "on")
                {
                    solug = txtFile.Count();
                }

                for (int h = 0; h < solug; h++)
                {
                    tn[h] = new TracNghiem();

                    var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Others", "FormatTracNghiem.txt");

                    if (f["cbCoSan"].ToString() != "on")
                    {
                       path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", Path.GetFileName(txtFile[h].FileName));

                        using (Stream fileStream = new FileStream(path, FileMode.Create))
                        {
                            txtFile[h].CopyTo(fileStream);

                        }
                    }

                    var xu = FileExtension.ReadFile(path);
                    var encrypt = false;
                    try
                    {
                        StringMaHoaExtension.Decrypt(xu).Replace("\r\n", "\n");
                        encrypt = true;
                    }
                    catch
                    {
                        encrypt = false;
                    }

                    if (encrypt == true)
                    {
                        xu = StringMaHoaExtension.Decrypt(xu).Replace("\r\n", "\n");
                    }

                    String ND_file = xu;
                    cFile = ND_file;

                    var sanitizer = new HtmlSanitizer();
                    sanitizer.AllowedTags.Clear();
                    sanitizer.AllowedAttributes.Clear();
                    var pathSD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Others", "HtmlSanitizerAccept.txt");
                    var noidungSD = FileExtension.ReadFile(pathSD).Replace("\r", "").Split("\n==========\n");
                    for (var iss = 0; iss < noidungSD.Length; iss++)
                    {
                        var htmlSanitizerAccept = noidungSD[iss].Replace("\r", "").Split("\n");
                        for (var joo = 0; joo < htmlSanitizerAccept.Length; joo++)
                        {
                            if (iss == 0)
                            {
                                sanitizer.AllowedTags.Add(htmlSanitizerAccept[joo]);
                            }
                            else
                            {
                                sanitizer.AllowedAttributes.Add(htmlSanitizerAccept[joo]);
                            }
                        }
                    }

                    // 🔹 Chặn "javascript:" và "expression()" trong style
                    sanitizer.RemovingAttribute += (s, e) =>
                    {
                        string lowerValue = e.Attribute.Value.ToLower();
                        if (lowerValue.Contains("javascript:") || lowerValue.Contains("expression("))
                        {
                            e.Cancel = true; // Hủy bỏ giá trị nguy hiểm
                        }
                    };

                    var socautatca = ND_file.Replace("\r", "").Split("\n#\n").Length;
                    for (var index = 0; index < socautatca; index++)
                    {
                        ND_file = ND_file.Replace("[<?" + index + "?>]", "DARKAXNA_TRACNGHIEM_" + index);
                    }

                    ND_file = sanitizer.Sanitize(ND_file);
                    for (var index = 0; index < socautatca; index++)
                    {
                        ND_file = ND_file.Replace("DARKAXNA_TRACNGHIEM_" + index, "[<?" + index + "?>]");
                    }

                    if (f["cbCoSan"].ToString() != "on")
                    {
                        FileInfo fx = new FileInfo(path);
                        if (fx.Exists)
                            fx.Delete();
                    }

                    if (ND_file.Length == 0)
                    {
                        ViewData["Loi1"] = "Bài kiểm tra hay file văn bản chương (hoặc file của bạn tải lên thứ) " + (h + 1) + " của bạn hiện chưa có nội dung nào!";
                        HttpContext.Session.SetString("data-result", "true");
                        return this.TracNghiemX_Multiple();
                    }

                    String[] split = {
            "\n#\n"
          };
                    String[] t1 = ND_file.Replace("\r", "").Split(split, StringSplitOptions.RemoveEmptyEntries);

                    if (tick != "on")
                    {
                        for (i = 0; i < t1.Length; i++)
                        {
                            String[] t2 = t1[i].Replace("\r", "").Split('\n');
                            if (t2.Length != 6)
                            {

                                string err = "WRONG INDEX QUESTION [CHƯƠNG/FILE " + (i + 1) + "] : " + t2[0] + "";
                                //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";
                                ViewData["Loi1"] = err;
                                HttpContext.Session.SetString("data-result", "true");
                                return this.TracNghiemX_Multiple();
                            }

                            char[] t2_x = t2[5].ToCharArray();
                            if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                            {
                                string err = "WRONG INDEX QUESTION [CHƯƠNG/FILE " + (i + 1) + "] : " + t2[0] + "";
                                //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                                ViewData["Loi1"] = err;
                                HttpContext.Session.SetString("data-result", "true");
                                return this.TracNghiemX_Multiple();
                            }
                        }

                        //-------------------

                        for (i = 0; i < t1.Length; i++)
                        {
                            String[] t2 = t1[i].Replace("\r", "").Split('\n');
                            int flag = 0;
                            String DA = t2[t2.Length - 1].Replace("[", "");
                            DA = DA.Replace("]", "");
                            for (int j = t2.Length - 2; j > 0; j--)
                            {
                                if (DA.CompareTo(t2[j]) == 0)
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 0)
                            {
                                //MessageBox.Show("Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi.\nXin lỗi vì sự bất tiện này! ]");
                                ViewData["Loi1"] = "WRONG INDEX ANSWER OF QUESTION [CHƯƠNG/FILE " + (h + 1) + "] : " + t2[0] + "";
                                HttpContext.Session.SetString("data-result", "true");
                                return this.TracNghiem_Multiple(ViewBag.SL);
                            }
                        }
                    }

                    var hvix = "";

                    int[] chuaxet_ch;
                    int[][] chuaxet_da;

                    int n9 = t1.Length;
                    n9_S += n9;

                    tn[h].ch = new String[t1.Length];
                    tn[h].a = new String[t1.Length];
                    tn[h].b = new String[t1.Length];
                    tn[h].c = new String[t1.Length];
                    tn[h].d = new String[t1.Length];
                    tn[h].dung = new String[t1.Length];

                    chuaxet_ch = new int[t1.Length];

                    chuaxet_da = new int[t1.Length][];

                    for (i = 0; i < t1.Length; i++)
                        chuaxet_da[i] = new int[5];

                    //=======

                    int dem = 0;
                    var ix = 0;
                    while (true)
                    {
                        if (dem == t1.Length)
                            break;

                        double x = 0;
                        Random r = new Random();

                        if (tick != "on")
                        {
                            x = r.Next(0, t1.Length);
                        }
                        else
                        {
                            x = ix;
                            ix++;
                        }

                        if (chuaxet_ch[int.Parse(x.ToString())] == 1)
                            continue;

                        chuaxet_ch[int.Parse(x.ToString())] = 1;

                        i = int.Parse(x.ToString());
                        String[] t2 = t1[i].Replace("\r", "").Split('\n');

                        char[] CH = t2[0].ToCharArray();

                        if (CH[0] == '$')
                        {
                            hvix += "YES\r\n";
                            t2[0].Remove(0, 1);
                            tn[h].ch[dem] = t2[0].Replace("$", "");
                            tn[h].a[dem] = t2[1];
                            tn[h].b[dem] = t2[2];
                            tn[h].c[dem] = t2[3];
                            tn[h].d[dem] = t2[4];
                            String DAx = t2[5].Replace("[", "");
                            DAx = DAx.Replace("]", "");
                            tn[h].dung[dem] = DAx;
                        }
                        else
                        {

                            hvix += "NO\r\n";
                            int aa, bb, cc, dd;

                            tn[h].ch[dem] = t2[0];

                            do
                            {
                                if (tick != "on")
                                    aa = r.Next(1, 5);
                                else
                                    aa = 1;
                            }
                            while (chuaxet_da[dem][int.Parse(aa.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(aa.ToString())] = 1;

                            tn[h].a[dem] = t2[aa];

                            do
                            {
                                if (tick != "on")
                                    bb = r.Next(1, 5);
                                else
                                    bb = 2;
                            }
                            while (chuaxet_da[dem][int.Parse(bb.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(bb.ToString())] = 1;

                            tn[h].b[dem] = t2[bb];

                            do
                            {
                                if (tick != "on")
                                    cc = r.Next(1, 5);
                                else
                                    cc = 3;
                            }
                            while (chuaxet_da[dem][int.Parse(cc.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(cc.ToString())] = 1;

                            tn[h].c[dem] = t2[cc];

                            do
                            {
                                if (tick != "on")
                                    dd = r.Next(1, 5);
                                else
                                    dd = 4;
                            }
                            while (chuaxet_da[dem][int.Parse(dd.ToString())] == 1);
                            chuaxet_da[dem][int.Parse(dd.ToString())] = 1;

                            tn[h].d[dem] = t2[dd];
                            String DA = t2[5].Replace("[", "");
                            DA = DA.Replace("]", "");
                            tn[h].dung[dem] = DA;
                        }
                        dem++;
                    }
                    tn[h].tongsocau = n9;
                    HttpContext.Session.SetString("hoanvis", hvix);
                }

                if (tick == "on")
                    txtSoCau = n9_S.ToString();

                TracNghiem tnX = new TracNghiem();
                tnX.ch = new String[int.Parse(txtSoCau)];
                tnX.a = new String[int.Parse(txtSoCau)];
                tnX.b = new String[int.Parse(txtSoCau)];
                tnX.c = new String[int.Parse(txtSoCau)];
                tnX.d = new String[int.Parse(txtSoCau)];
                tnX.dung = new String[int.Parse(txtSoCau)];

                tnX.tongsocau = n9_S;

                if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9_S)
                {
                    txtSoCau = n9_S.ToString();
                }

                if (tick != "on")
                    tnX.gioihancau = int.Parse(txtSoCau);
                else
                    tnX.gioihancau = n9_S;

                int[][] chuaxetX = new int[sl][];

                for (i = 0; i < sl; i++)
                {
                    chuaxetX[i] = new int[tn[i].tongsocau];
                    for (int j = 0; j < chuaxetX[i].Length; j++)
                    {
                        chuaxetX[i][j] = 0;
                    }
                }

                int xx = 0;
                for (i = 0; i < tnX.gioihancau; i++)
                {
                    int chuong = 0;
                    int soluong = 0;
                    if (tick != "on")
                    {
                        Random r = new Random();

                        do
                        {
                            chuong = r.Next(0, sl);
                            soluong = r.Next(0, tn[chuong].tongsocau);
                        }
                        while (chuaxetX[chuong][soluong] == 1);
                    }
                    else
                    {
                        soluong = xx;
                        xx++;
                    }

                    chuaxetX[chuong][soluong] = 1;

                    tnX.ch[i] = tn[chuong].ch[soluong];
                    tnX.a[i] = tn[chuong].a[soluong];
                    tnX.b[i] = tn[chuong].b[soluong];
                    tnX.c[i] = tn[chuong].c[soluong];
                    tnX.d[i] = tn[chuong].d[soluong];
                    tnX.dung[i] = tn[chuong].dung[soluong];
                }

                if (tick != "on")
                    tnX.timelambai = int.Parse(txtTime);
                tnX.tenmon = txtMon;

                if (tick != "on")
                    ViewBag.TimeLamBai = tnX.timelambai;

                HttpContext.Session.SetObject("TracNghiem", tnX);

                ViewBag.TongSoCau = tnX.tongsocau;
                ViewBag.GioiHanCau = tnX.gioihancau;
                ViewBag.TimeLamBaiX = tnX.timelambai;
                ViewBag.TenMon = tnX.tenmon;

                ViewBag.CauHoi = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.ch), "32752006");
                ViewBag.A = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.a), "32752006");
                ViewBag.B = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.b), "32752006");
                ViewBag.C = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.c), "32752006");
                ViewBag.D = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.d), "32752006");
                ViewBag.Dung = StringMaHoaExtension.Encrypt(String.Join("\r\n", tnX.dung), "32752006");

                ViewBag.KetQuaDung = "";

                var maudivPlay = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[6];
                if (maudivPlay == "COLOR_DIV_TRAC_NGHIEM_ON")
                {
                    for (i = 0; i < tnX.gioihancau; i++)
                    {
                        if (i % 2 == 0)
                            TempData["mau-trac-nghiem-" + i] = "orangered";
                        else
                            TempData["mau-trac-nghiem-" + i] = "hotpink";
                    }
                }

                if (tick == "on")
                    ViewBag.ND_File = cFile;
                else
                    ViewBag.ND_File = null;

                if (tick == "on")
                {
                    txtSoCau = n9_S.ToString();
                    ViewBag.TongCau = n9_S;

                    Regex regExp = new Regex(@"<img.*>");

                    ViewBag.HinhAnh = "";

                    for (i = 0; i < tnX.tongsocau; i++)
                    {
                        string result = "";
                        foreach (Match match in regExp.Matches(tnX.ch[i]))
                        {
                            result += "<br>" + match.Value.ToString() + "<br>";
                            break;
                        }
                        ViewBag.HinhAnh += String.Join("\r\n", result.Split("<br><br>")).Replace("<br>", "") + "\n\n";
                    }

                    ViewBag.HinhAnh = ViewBag.HinhAnh.TrimEnd("\n\n".ToCharArray());

                    var s = "";
                    for (i = 0; i < tnX.tongsocau; i++)
                    {
                        tnX.ch[i] = Regex.Replace(tnX.ch[i], "<br><img title=\"hinhcau" + i + "\".*><br>", "");
                        ViewBag.ND_File = Regex.Replace(ViewBag.ND_File, "<br><img title=\"hinhcau" + i + "\".*><br>", "");

                        if (tnX.dung[i] == tnX.a[i])
                            s += "1\n";
                        else if (tnX.dung[i] == tnX.b[i])
                            s += "2\n";
                        else if (tnX.dung[i] == tnX.c[i])
                            s += "3\n";
                        else if (tnX.dung[i] == tnX.d[i])
                            s += "4\n";
                        else
                            s += "0\n";
                    }

                    s = s.TrimEnd('\n');
                    ViewBag.SettingAnswer = s;

                    ViewBag.HoanVis = HttpContext.Session.GetString("hoanvis");
                    HttpContext.Session.Remove("hoanvis");
                }

                return View("PlayTracNghiem", tnX);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        public ActionResult HD_Web_AspNet()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }
    }
}