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
        // Check link url exist and respone OK?


        [HttpPost]
        public ActionResult ApiUpload(List<IFormFile> fileUpload)
        {
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

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
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
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

                var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

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

                var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

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
