using Microsoft.AspNetCore.Http;
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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
            if (TempData["BlockRegistUsing"] == "true")
                return RedirectToAction("LockedWeb");

            if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung2 = docfile(path2);

            string IP = "";

            var IPx = "";

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IPx = endPoint.Address.ToString();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            else
                IP = HttpContext.Session.GetString("userIP");

            if (IP == "0.0.0.0")
                return RedirectToAction("LockedWeb");


            if (key == "true" && (noidung.Contains(IP) == true || noidung2.Contains(IP) == true))
            {
                return RedirectToAction("Index");
            }

            if (key == "true" && (info == null || info == ""))
            {
                return View("LockedWeb");
            }

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/InfoIPRegist.txt");
            var noidung1 = docfile(path1);

            if (IP != "0.0.0.0")
            System.IO.File.WriteAllText(path1, noidung1 +"\n" + IP + "\t" + xuxu + "\t" + "[regist] " + info);

            string message = "Báo cáo hành động bật trang web của khách hàng @info :\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@end\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 1]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@1_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@1_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@1_end\r\n\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 2]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@2_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@2_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@2_end\r\n\r\nThanks!"
                .Replace("@info", info)
                .Replace("@lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + IP)
                .Replace("@unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + IP)
                .Replace("@end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IP)
             .Replace("@1_lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + IPx)
                .Replace("@1_unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + IPx)
                .Replace("@1_end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IPx)
                 .Replace("@2_lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + Request.HttpContext.Connection.RemoteIpAddress);

            if (key == "true")
            {
                TempData["PlayOnWebInLocal"] = "true";
                noidung += IP + "##";
                System.IO.File.WriteAllText(path, noidung);
            }
            else if (key == "false")
            {
                noidung = noidung.Replace(IP + "##", "");
                System.IO.File.WriteAllText(path, noidung);
                TempData["PlayOnWebInLocal-X"] = "false";
            }

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx +" *** " + IP + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");

            if (key == "true")
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidungX = System.IO.File.ReadAllText(pathX);

                var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var infox = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    if (infox[0] == "Email_User_Website")
                    {
                        if (infox[1] == "true")
                        {
                            SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
                        "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Play On Web In Client Local  In " + name, message, "teinnkatajeqerfl");
                        }
                        break;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Share_Karaoke(string? id)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            //TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            //var listIP = new List<string>();

            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
            //    listIP.Add(HttpContext.Session.GetString("userIP"));
            //else
            //{
            //    TempData["GetDataIP"] = "true";
            //    return RedirectToAction("Index");
            //}
            //khoawebsiteClient(listIP);

            HttpContext.Session.SetString("data-result", "true");
            if (id != null)
            {
                var noidung = id.Split("-.-", StringSplitOptions.RemoveEmptyEntries);

                if (noidung.Length == 4)
                {
                    var url = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[0]) : noidung[0].Replace("[NONE]", "");
                    var baihat = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[1]) : noidung[1].Replace("[NONE]", "");
                    var background = (id.Contains("[NONE]") == false) ? StringMaHoaExtension.Decrypt(noidung[2]) : noidung[2];
                    var option = int.Parse(noidung[3]);

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
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP = "";
            var IPx = "";

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IPx = endPoint.Address.ToString();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            else
                localIP = HttpContext.Session.GetString("userIP");

            if (IPx == localIP)
                IPx = "";
            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");

            var loi = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                {
                    email = "mywebplay.savefile@gmail.com";
                }

                SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
                    "mywebplay.savefile@gmail.com", host + " [~1 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                }
            }

            return Redirect("https://stackoverflow.com/questions");
        }

        public ActionResult SendMailSave2 (string? email)
        {
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
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP = "";
            var IPx = "";

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IPx = endPoint.Address.ToString();
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            else
                localIP = HttpContext.Session.GetString("userIP");

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");

            var message = TextCopy.ClipboardService.GetText();

            var loi = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                {
                    email = "mywebplay.savefile@gmail.com";
                }

                SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
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
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
                    "mywebplay.savefile@gmail.com", host + " [~2 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                }
            }

            return Redirect("https://stackoverflow.com/questions");
        }

        public ActionResult SecretWeb (string? email)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            var listIP = new List<string>();

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");
            TempData["RandomLayout"] = docfile(path);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                listIP.Add(HttpContext.Session.GetString("userIP"));
            else
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            khoawebsiteClient(listIP);
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                email = "mywebplay.savefile@gmail.com";
            }
            ViewBag.Email = email;

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "Alert_UsingWebsite")
                {
                    if (info[1] == "false")
                    {
                        TempData["AlertUsingWebsite"] = "false";
                    }
                    else
                    {
                        TempData["AlertUsingWebsite"] = "true";
                    }

                    if (HttpContext.Session.GetString("trust-X-you") == "true")
                    {
                        TempData["AlertUsingWebsite"] = "true";
                    }
                }


                if (info[0] == "Off_RandomTab")
                {
                    if (info[1] == "false")
                    {
                        var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Image.txt");
                        var hinh = System.IO.File.ReadAllText(pathX1).Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Tittle.txt");
                        var tittle = System.IO.File.ReadAllText(pathX2).Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
            
            if (TempData["RandomLayout"].ToString().Contains("onclick=\"chuyendoi()\"") == false)
            ViewBag.XuLy = "<br><a href=\"#\" onclick=\"chuyendoi()\">5828</a><br>";

                return View();
        }

        //----------------------------------------------------------------

        public ActionResult RandomLayout()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");
            ViewBag.RandomLayOut = docfile(path); 

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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");
            if (System.IO.File.Exists(path))
            {
                ViewBag.Text = System.IO.File.ReadAllText(path);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditLayout(string? txtText)
        {
            if (string.IsNullOrEmpty(txtText))
            {
                txtText = "<img src=\"/Image_Play/Google.png\" width=\"100%\" heigth=\"100%\" onclick=\"chuyendoi()\">";
            }
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");

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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.File.WriteAllText(path, "<img src=\"/Image_Play/Google.png\" width=\"100%\" heigth=\"100%\" onclick=\"chuyendoi()\">");
            return RedirectToAction("RandomLayout");
        }

        //-----------------------------------------------------------------

        [HttpPost]
        public ActionResult SecretWeb (IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");

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
                string localIP = "";
                var IPx = "";

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    IPx = endPoint.Address.ToString();
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                else
                    localIP = HttpContext.Session.GetString("userIP");
                //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;
                string host = "{" + Request.Host.ToString() + "}"
                           .Replace("http://", "")
                       .Replace("http://", "")
                       .Replace("/", "");

                var loi = 0;
                try
                {
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
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
                        SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
                        "mywebplay.savefile@gmail.com", host + " [~3 THONG BAO ADMIN" + err + "] Quick Send Text Mail By Url To Save In " + name, message, "teinnkatajeqerfl");
                    }
                }
            }
            return Redirect("https://stackoverflow.com/questions");
        }

        public ActionResult LockedWeb()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }

            var pathXY = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungXY = System.IO.File.ReadAllText(pathXY);
            var listSettingM = noidungXY.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSettingM.Length; i++)
            {
                var info = listSettingM[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }

                if (HttpContext.Session.GetString("trust-X-you") == null)
                {
                    if (info[0] == "Alert_UsingWebsite")
                    {
                        if (info[1] == "false")
                            TempData["rehosting"] = "true";
                    }
                }

                if (info[0] == "Off_RandomTab")
                {
                    if (info[1] == "false")
                    {
                        var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Image.txt");
                        var hinh = System.IO.File.ReadAllText(pathX1).Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Tittle.txt");
                        var tittle = System.IO.File.ReadAllText(pathX2).Split("\n", StringSplitOptions.RemoveEmptyEntries);

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

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
            }

            if (HttpContext.Session.GetString("boqua") != "true")
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidungX = System.IO.File.ReadAllText(pathX);

                var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    if (HttpContext.Session.GetString("TuyetDoi") != "true" && info[0] == "Using_Website")
                    {
                        if (info[1] == "true")
                        {
                            HttpContext.Session.SetString("boqua", "true");
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            HttpContext.Session.Remove("boqua");

            if (TempData["UsingWebsite"] == "true")
            {
                TempData["lock"] = "";
                TempData["PlayOnWebInLocal-X"] = "";
                TempData["GetDataIP-X"] = "true";
                var listIP = new List<string>();
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                }

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    listIP.Add(endPoint.Address.ToString());
                }

                listIP.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString());

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
                var noidung1 = docfile(path1);

                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
                var noidung2 = docfile(path2);
                var flag = 0;
                for (int i = 0; i < listIP.Count; i++)
                {
                    if (noidung1.Contains(listIP[i]) == true &&
                        noidung2.Contains(listIP[i]) == false)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 1)
                    return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult LockedWebClient(string? IP)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);


                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }


                if (HttpContext.Session.GetString("trust-X-you") == null)
                {
                    if (info[0] == "Alert_UsingWebsite")
                    {
                        if (info[1] == "false")
                            TempData["rehosting"] = "true";
                    }
                }

                if (info[0] == "Off_RandomTab")
                {
                    if (info[1] == "false")
                    {
                        var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Image.txt");
                        var hinh = System.IO.File.ReadAllText(pathX1).Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Tittle.txt");
                        var tittle = System.IO.File.ReadAllText(pathX2).Split("\n", StringSplitOptions.RemoveEmptyEntries);

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

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
            }

            TempData["lockedClient"] = "";

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
            if (TempData["UserIP"] == "0.0.0.0")
                return RedirectToAction("Index");

            if (TempData["lock"] == "true")
                return RedirectToAction("Index");

            if (string.IsNullOrEmpty(IP))
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                else
                    IP = HttpContext.Session.GetString("userIP");
            }
            else
            {
                HttpContext.Session.SetString("checkedID", IP);
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
            var noidung = docfile(path);

            if (noidung.Contains(IP) == false)
                System.IO.File.WriteAllText(path, noidung + IP + "<>[NOT-PASSWORD-3275]##");

            return View();
        }

        [HttpPost]
        public ActionResult LockedWebClient(IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            if (TempData["UserIP"] == "0.0.0.0")
                return RedirectToAction("Index");
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            var code = "";
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }

                if (info[0] == "Code_LockedClient")
                    code = info[3];

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note"))
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

            string LockedClientID = f["LockedClientID"].ToString();

            if (string.IsNullOrEmpty(LockedClientID))
                return RedirectToAction("LockedWebClient");

            string IP = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("checkedID")) == false)
            {
                IP = HttpContext.Session.GetString("checkedID");
                HttpContext.Session.Remove("checkedID");
            }
            else
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                else
                IP = HttpContext.Session.GetString("userIP");
            }
            
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
            var noidung = docfile(path);

            Calendar x = CultureInfo.InvariantCulture.Calendar;
        var d = x.AddHours(DateTime.UtcNow, 7);

        var day = String.Join("", d.Day.ToString("00").Reverse().ToList());
        var month = String.Join("", d.Month.ToString("00").Reverse().ToList());
        var hour = String.Join("", d.Hour.ToString("00").Reverse().ToList());
        var minute = String.Join("", d.Minute.ToString("00").Reverse().ToList());

        var passOK = "00" + hour + month + day + minute;

                if (noidung.Contains(IP + "<>[NOT-PASSWORD-3275]##") == true)
                {
                    if (LockedClientID == passOK || LockedClientID == code)
                        noidung = noidung.Replace(IP + "<>[NOT-PASSWORD-3275]##", "");
                }
                else
                {
                        noidung = noidung.Replace(IP + "<>" + LockedClientID + "##", "");
                }

System.IO.File.WriteAllText(path, noidung);
return RedirectToAction("Index");
        }

        public ActionResult GetUserIP(string ip, string currentUrl)
        {
            TempData["GetDataIP"] = "false";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                HttpContext.Session.SetString("userIP", ip);
            }
            return RedirectToAction(currentUrl);
        }
    }
}
