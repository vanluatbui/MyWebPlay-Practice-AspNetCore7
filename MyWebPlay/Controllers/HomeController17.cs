using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyWebPlay.Extension;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Xml.Linq;
using TextCopy;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PlayOnWebInLocalX(string key)
        {
            if (key == "true")
                TempData["PlayOnWebInLocal"] = "true";
            else if (key == "false")
                TempData["PlayOnWebInLocal"] = "false";

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("https://", "")
                   .Replace("/", "");

            if (key == "true")
            {
                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                       "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Play On Web In Client Local  In " + name, "", "teinnkatajeqerfl");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Share_Karaoke(string? id)
        {
            //System.IO.File.WriteAllText("D:/XemCode/ma.txt", id);
            if (id != null)
            {
                var noidung = id.Split("-.-", StringSplitOptions.RemoveEmptyEntries);

                if (noidung.Length == 4)
                {
                    var url = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[0]) : noidung[0].Replace("[NONE]", "");
                    var baihat = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[1]) : noidung[1].Replace("[NONE]", "");
                    var background = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[2]) : noidung[2];
                    var option = int.Parse(noidung[3]);

                    //System.IO.File.WriteAllText("D:/XemCode/ma.txt", url);

                    TempData["url"] = url;
                    TempData["baihat"] = baihat;
                    TempData["background"] = background;
                    TempData["option"] = option;
                }
            }
            return RedirectToAction("PlayKaraokeX");
        }

        public ActionResult SendMailSave1 (string? email, string message)
        {
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("https://", "")
                   .Replace("/", "");

            var loi = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                {
                    email = "mywebplay.savefile@gmail.com";
                }

                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
   email, host + " ~1 Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
            }
            catch(Exception ex) 
            {
                loi = 1;
            }
            finally
            {
                if (email != "mywebplay.savefile@gmail.com")
                {
                    var err = (loi == 0) ? " - SUCCESS # "+email : " - ERROR # "+email;
                    SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                    "mywebplay.savefile@gmail.com", host + " [~1 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                }
            }

            return Redirect("https://google.com");
        }

        public ActionResult SendMailSave2 (string? email)
        {
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("https://", "")
                   .Replace("/", "");

            var message = TextCopy.ClipboardService.GetText();

            var loi = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                {
                    email = "mywebplay.savefile@gmail.com";
                }

                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
   email, host + " ~2 Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
            }
            catch (Exception ex)
            {
                loi = 1;
            }
            finally
            {
                if (email != "mywebplay.savefile@gmail.com")
                {
                    var err = (loi == 0) ? " - SUCCESS # " + email : " - ERROR # " + email;
                    SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                    "mywebplay.savefile@gmail.com", host + " [~2 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                }
            }

            return Redirect("https://google.com");
        }

        public ActionResult SecretWeb (string? email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                email = "mywebplay.savefile@gmail.com";
            }
            ViewBag.Email = email;
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayOut.txt");

            var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.RandomLayout = System.IO.File.ReadAllText(path);
            }

            if (string.IsNullOrEmpty(ViewBag.RandomLayout) == false || string.IsNullOrWhiteSpace(ViewBag.RandomLayout) == false)
            {
                ViewBag.XuLy = "<br><a href=\"#\" onclick=\"chuyendoi()\">_</a><br>";
            }
                return View();
        }

        //----------------------------------------------------------------

        public ActionResult RandomLayout()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayout.txt");

            var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.Text = System.IO.File.ReadAllText(path);
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                ViewBag.DateTime = x.AddHours(file.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return View();
        }

        public ActionResult EditLayout()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayout.txt");
            if (System.IO.File.Exists(path))
            {
                ViewBag.Text = System.IO.File.ReadAllText(path);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditLayout(string? txtText)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayout.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (txtText != null)
                System.IO.File.WriteAllText(path, txtText);

            return RedirectToAction("RandomLayout");
        }

        public ActionResult XoaLayout()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayout.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("RandomLayout");
        }

        //-----------------------------------------------------------------

        [HttpPost]
        public ActionResult SecretWeb (IFormCollection f)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "RandomLayOut.txt");

            var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.RandomLayout = System.IO.File.ReadAllText(path);
            }

            var email = f["txtEmail"].ToString();
            var message = f["txtNoiDung"].ToString();

            if (string.IsNullOrWhiteSpace(message) == false || string.IsNullOrEmpty(message) == false)
            {
                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;
                string host = "{" + Request.Host.ToString() + "}"
                           .Replace("http://", "")
                       .Replace("https://", "")
                       .Replace("/", "");

                var loi = 0;
                try
                {
                    SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
       email, host + " ~3 Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                }
                catch (Exception ex)
                {
                    loi = 1;
                }
                finally
                {
                    if (email != "mywebplay.savefile@gmail.com")
                    {
                        var err = (loi == 0) ? " - SUCCESS # " + email : " - ERROR # " + email;
                        SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                        "mywebplay.savefile@gmail.com", host + " [~3 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                    }
                }
            }
            return Redirect("https://google.com");
        }
    }
}
