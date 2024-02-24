using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using Org.BouncyCastle.Ocsp;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using MyWebPlay.Extension;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {

        public ActionResult Replace_Hamorny()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                {
                    HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
            return View();
        }
        // Check link url exist and respone OK?

        [HttpPost]
        public ActionResult Replace_Hamorny(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var listIP = new List<string>();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
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


                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["txtChuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = x.AddHours(DateTime.UtcNow, 7);

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
                }

                var n = int.Parse(f["txtIndex"].ToString());

                var s = "";

                var input = f["txtInput"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                input = input.Replace("[T-PLAY]", "\t");
                input = input.Replace("[N-PLAY]", "\n");
                input = input.Replace("[R-PLAY]", "\r");

                var xj = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                for ( int i = 0; i < xj.Length; i++)
                {
                    var sd = xj[i].Split("\t", StringSplitOptions.RemoveEmptyEntries);
                    var temp = chuoi;

                    for (int j = 0; j < sd.Length; j++)
                    {
                        temp = temp.Replace("[" + (j+1) + "]", sd[j]);
                    }

                    s += temp;
                    if (i < xj.Length - 1)
                        s += "\r\n";
                }

                //TextCopy.ClipboardService.SetText(s);

                // s = "<p style=\"color:blue\"" + s + "</p>";
                nix = s;
                s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar; var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", ""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = s;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!"; if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }

            if (exter == false)
                return View();
            return Ok(new { result = nix.Replace("\r", "[R-PLAY]").Replace("\n", "[N-PLAY]").Replace("\t", "[T-PLAY]").Replace("\"", "[NGOACKEP]") });
        }


        [HttpPost]
        public ActionResult ApiUpload(List<IFormFile> fileUpload)
        {
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "false")
                    return Ok(new { error = "Dịch vụ đang bị tạm ngừng, mời quay lại sau !" });

                var formFile = new List<IFormFile>(fileUpload);
                for (int i = 0; i < formFile.Count(); i++)
                {
                    var fileName = Path.GetFileName(formFile[i].FileName);

                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload", fileName);

                    using (Stream fileStream = new FileStream(pathX, FileMode.Create))
                    {
                        formFile[i].CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
            return Ok(new { message = "Đã xử lý thành công !" });
        }

        [HttpPost]
        public ActionResult LogMail(string txtText, string? External = "false")
        {
            try
            {
                txtText = txtText.Replace("\r\n", "\n").Replace("\n", "\r\n");

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (External == "false" && info[0] == "External_Unenable")
                    {
                        if (info[1] == "true")
                            return Redirect("https://google.com");
                    }
                    else if (External == "true" && info[0] == "External_Post")
                    {
                        if (info[1] == "false")
                            return Ok(new { error = "Bạn không được phép liên lạc tính năng này!" });
                    }
                }

            SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", "[ADMIN] External send mail with data log", txtText, "teinnkatajeqerfl");
            
                if (External == "false")
                return Redirect("https://google.com");
                return Ok(new { error = "Đã xử lý thành công!" });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }


        public ActionResult DLQ(string path)
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(pathX);

                var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var info = listSetting[46].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[1] == "false") return RedirectToAction("Error");

                return Redirect("/file/Folder1/Folder2/" + path);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

            public ActionResult DownloadFile_ClearWeb()
        {
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                }

                var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[44].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                var infoY = listSetting[3].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);


                if (infoY[1] == "false" || infoX[3] == "[NULL]")
                    return RedirectToAction("Error");

                var listFile = infoX[3].Split(",", StringSplitOptions.RemoveEmptyEntries);
                ViewBag.CountFile = listFile.Length;

                for (int i = 0; i < listFile.Length; i++)
                {
                    TempData["file-" + i] = listFile[i];
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public bool UrlIsValid(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000;
                request.Method = "HEAD";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400)
                    {
                        return true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510)
                    {
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
