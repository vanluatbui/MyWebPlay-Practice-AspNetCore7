using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyWebPlay.Extension;
using System;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Xml.Linq;
using TextCopy;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PlayOnWebInLocalX(string key, string? info)
        {
            if (key == "true" && (info == null || info == ""))
            {
                return View("LockedWeb");
            }

            khoawebsiteClient();
            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            string IP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IP = endPoint.Address.ToString();
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung2 = docfile(path2);

            if (key == "true" && (noidung.Contains(IP) == true || noidung2.Contains(IP) == true))
            {
                return RedirectToAction("Index");
            }

            string keyX = "Win32_BIOS";
            ManagementObjectSearcher selectvalue = new ManagementObjectSearcher("select * from " + keyX);
            string ID = "";
            foreach (ManagementObject getserial in selectvalue.Get())
            {
                ID += getserial["SerialNumber"].ToString();
            }

            string message = "Báo cáo hành động bật trang web @host của khách hàng @info (ID : @id) :\r\n\r\n* RIÊNG TƯ :\r\n\r\n+ Link khoá sử dụng website (riêng tư) :\r\n\r\n@lock_private\r\n\r\n+ Link mở khoá cho phép tiếp tục sử dụng website (riêng tư)\r\n\r\n@unlock_private\r\n\r\n____________________________________________\r\n\r\n* CỤC BỘ :\r\n\r\n+ Link khoá sử dụng website (cục bộ) :\r\n\r\n@lock_enter\r\n\r\n+ Link mở khoá cho phép tiếp tục sử dụng website (cục bộ)\r\n\r\n@unlock_enter\r\n\r\n____________________________________________\r\n\r\n* ĐĂNG XUẤT :\r\n\r\n+ Xoá quyền truy cập, yêu cầu đăng nhập lại (tất cả)\r\n\r\n@end\r\n\r\n"
                .Replace("@id", ID)
                .Replace("@info", info)
                .Replace("@lock_private", "https://" + Request.Host + "/Home/LockThisClient?ip=" + ID)
                .Replace("@lock_enter", "https://" + Request.Host + "/Home/LockThisClient?ip=" + IP)
                 .Replace("@unlock_private", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + ID)
                .Replace("@unlock_enter", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + IP)
                 .Replace("@end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + ID+"*"+IP)
                .Replace("@host", "https://" + Request.Host);

            if (key == "true")
            {
                TempData["PlayOnWebInLocal"] = "true";
                noidung += ID + "##";
                System.IO.File.WriteAllText(path, noidung);
            }
            else if (key == "false")
            {
                noidung = noidung.Replace(ID + "##", "");
                System.IO.File.WriteAllText(path, noidung);
                TempData["PlayOnWebInLocal"] = "false";
            }

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + localIP + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("https://", "")
                   .Replace("/", "");

            if (key == "true")
            {
                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                       "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Play On Web In Client Local  In " + name, message, "teinnkatajeqerfl");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Share_Karaoke(string? id)
        {
            khoawebsiteClient();
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
            khoawebsiteClient();
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + localIP + "] - " + xuxu;
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
            khoawebsiteClient();
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + localIP + "] - " + xuxu;
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
            khoawebsiteClient();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                email = "mywebplay.savefile@gmail.com";
            }
            ViewBag.Email = email;


                ViewBag.XuLy = "<br><a href=\"#\" onclick=\"chuyendoi()\">...</a><br>";

                return View();
        }

        //----------------------------------------------------------------

        public ActionResult RandomLayout()
        {
            khoawebsiteClient();
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
            khoawebsiteClient();
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
                string localIP;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }

                //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + localIP + "] - " + xuxu;
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

        public ActionResult LockedWeb()
        {
            string IP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IP = endPoint.Address.ToString();
            }

            string key = "Win32_BIOS";
            ManagementObjectSearcher selectvalue = new ManagementObjectSearcher("select * from " + key);
            string ID = "";
            foreach (ManagementObject getserial in selectvalue.Get())
            {
                ID += getserial["SerialNumber"].ToString();
            }

            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung1 = docfile(path1);

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung2 = docfile(path2);

            if ((noidung1.Contains(IP) == true || noidung1.Contains(ID) == true ) 
                && noidung2.Contains(IP) == false
               && noidung2.Contains(ID) == false)
                return RedirectToAction("Index");
                return View();
        }

        public ActionResult LockedWebClient(string? LockedClientID)
        {
            khoawebsiteClient();

            if (TempData["lock"] == "true")
                return RedirectToAction("Index");

            string key = "Win32_BIOS";
            ManagementObjectSearcher selectvalue = new ManagementObjectSearcher("select * from " + key);
            string IP = "";
            foreach (ManagementObject getserial in selectvalue.Get())
            {
                IP += getserial["SerialNumber"].ToString();
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
            var noidung = docfile(path);
            if (LockedClientID == null || LockedClientID == "")
            {
                if (noidung.Contains(IP) == false)
                System.IO.File.WriteAllText(path, noidung + IP + "<>[NOT-PASSWORD-3275]##");
                return View();
            }
            else
            {
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                var d = x.AddHours(DateTime.UtcNow, 7);

                var day = String.Join("", d.Day.ToString("00").Reverse().ToList());
                var month = String.Join("", d.Month.ToString("00").Reverse().ToList());
                var hour = String.Join("",d.Hour.ToString("00").Reverse().ToList());
                var minute = String.Join("", d.Minute.ToString("00").Reverse().ToList());

                var passOK = "00"+hour + month + day + minute;

                if (noidung.Contains(IP + "<>[NOT-PASSWORD-3275]##") == true)
                {
                    if (LockedClientID == passOK)
                        noidung = noidung.Replace(IP + "<>[NOT-PASSWORD-3275]##", "");
                }
                else
                {
                        noidung = noidung.Replace(IP + "<>" + LockedClientID + "##", "");
                }

                System.IO.File.WriteAllText(path, noidung);
                return RedirectToAction("Index");
            }
        }
    }
}
