using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult String_Reverse()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
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
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                return RedirectToAction("Error", new { exception = "true" });
            }
        }


        [HttpPost]
        public ActionResult String_Reverse(IFormCollection f)
        {
            var nix = "";
            var exter = false; var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true; var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries); if (infoY[1] == "true") linkdown = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                    {
                        HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
            string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
            chuoi = chuoi.Replace("[N-PLAY]", "\n");
            chuoi = chuoi.Replace("[R-PLAY]", "\r");

            TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User"
                            || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                            || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                            || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                            || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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
                }


            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Reverse();
            }
           

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
                s.Add(ds[i]);

            s.Reverse();

            string x = String.Join("\r\n", s);

            //TextCopy.ClipboardService.SetText(x);

            //x = "\r\n" + x;
            //x = x.Replace("\r\n", "<br>");
            nix = x;
            x = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + x + "</textarea>";

            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" +chim+ "_dataresult.txt"); TempData["fileResult"] = xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim+  "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = x;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";  if(TempData["ConnectLinkDown"] == "true")  return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                if (exter == false)
                    return View();
               else { if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]); return Ok(new { result = "http://"+Request.Host+ "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ","%20") }); }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }

        [HttpGet]
        public ActionResult String_Reverse2()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
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
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        [HttpPost]
        public ActionResult String_Reverse2(IFormCollection f)
        {
            var nix = "";
            var exter = false; var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true; var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries); if (infoY[1] == "true") linkdown = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                    {
                        HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
            HttpContext.Session.Remove("ok-data");
            string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
            chuoi = chuoi.Replace("[N-PLAY]", "\n");
            chuoi = chuoi.Replace("[R-PLAY]", "\r");

            TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                if (exter == false)
                {
                    khoawebsiteClient(listIP);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User"
                            || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                            || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                            || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                            || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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
                }

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Reverse2();
            }
           

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
            {
                List<string> winx = ds[i].Split(' ').ToList();
               
                
                winx.Reverse();
                s.Add(String.Join(" ",winx));
            }

            string x = String.Join("\r\n", s);

            //TextCopy.ClipboardService.SetText(x);

           nix = x;
            x = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + x + "</textarea>";

            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" +chim+ "_dataresult.txt"); TempData["fileResult"] = xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim+  "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = x;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";  if(TempData["ConnectLinkDown"] == "true")  return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                if (exter == false)
                    return View();
               else { if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]); return Ok(new { result = "http://"+Request.Host+ "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ","%20") }); }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }

        [HttpGet]
        public ActionResult Special_OrderBy()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
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
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        [HttpPost]
        public ActionResult Special_OrderBy(IFormCollection f)
        {
            var nix = "";
            var exter = false; var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true; var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries); if (infoY[1] == "true") linkdown = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null);  var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[14];                 if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")                 {                     if (this.HttpContext.Request.Method == "GET")                     {                         if (HttpContext.Session.GetObject<int>("google-trick-web") == null)                         {                             HttpContext.Session.SetObject("google-trick-web", 1);                             return Redirect("https://google.com");                         }                         else                         {                             var lan = HttpContext.Session.GetObject<int>("google-trick-web");                             if (lan != 10)                             {                                 HttpContext.Session.SetObject("google-trick-web", lan + 1);                                 return Redirect("https://google.com");                             }                         }                     }                 } if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                    {
                        HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
            string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
            chuoi = chuoi.Replace("[N-PLAY]", "\n");
            chuoi = chuoi.Replace("[R-PLAY]", "\r");

            TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User"
                            || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                            || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                            || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                            || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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
                }

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }
           

            string sapxep = f["SapXep"].ToString();
            sapxep = sapxep.Replace("[T-PLAY]", "\t");
            sapxep = sapxep.Replace("[N-PLAY]", "\n");
            sapxep = sapxep.Replace("[R-PLAY]", "\r");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }

            string[] DS = Regex.Split(chuoi, "\r\n");

            string[] so = sapxep.Split('=');

            while (true)
            {
                int l = 0;

                string kq1 = String.Join("\n", DS);
                while (l < so.Length)
                {
                    for (int i = 0; i < DS.Length - 1; i++)
                    {
                        string[] phan1 = DS[i].Split(' ');
                        int x1 = 0;

                        if (so[l].Contains("N-") == true)
                        {
                            x1 = phan1.Length - int.Parse(so[l].Split('-')[1]);
                        }
                        else
                        {
                            x1 = int.Parse(so[l]);
                        }

                        string ss1 = "";
                        for (int j = 0; j < l; j++)
                        {
                            int soX = 0;
                            if (so[j].Contains("N-") == true)
                            {
                                soX = phan1.Length - int.Parse(so[j].Split('-')[1]);
                            }
                            else
                            {
                                soX = int.Parse(so[j]);
                            }
                            ss1 += phan1[soX];
                        }

                        for (int jk = i + 1; jk < DS.Length; jk++)
                        {
                            string[] phan2 = DS[jk].Split(' ');
                            int x2 = 0;

                            if (so[l].Contains("N-") == true)
                            {
                                x2 = phan2.Length - int.Parse(so[l].Split('-')[1]);
                            }
                            else
                            {
                                x2 = int.Parse(so[l]);
                            }

                            string ss2 = "";
                            for (int jj = 0; jj < l; jj++)
                            {
                                int soX = 0;
                                if (so[jj].Contains("N-") == true)
                                {
                                    soX = phan2.Length - int.Parse(so[jj].Split('-')[1]);
                                }
                                else
                                {
                                    soX = int.Parse(so[jj]);
                                }
                                ss2 += phan2[soX];
                            }

                            if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) > 0)
                            {
                                string t = DS[i];
                                DS[i] = DS[jk];
                                DS[jk] = t;
                            }
                        }
                    }
                    l++;
                }
                string kq2 = String.Join("\n", DS);
                if (String.Compare(kq1, kq2) == 0)
                    break;
            }

            string dx = String.Join("\n", DS);

            //TextCopy.ClipboardService.SetText(dx);

            //String sql = "\r\n" + String.Join("\r\n", DS);

            //sql = sql.Replace("\r\n", "<br>");
            nix = dx;
            dx = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + dx + "</textarea>";

           var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
            TempData["fileResult"] = xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; 
            new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); 
            ViewBag.Result = dx;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";  if(TempData["ConnectLinkDown"] == "true")  return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                if (exter == false)
                    return View();
               else { if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]); return Ok(new { result = "http://"+Request.Host+ "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ","%20") }); }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }
    }
}
