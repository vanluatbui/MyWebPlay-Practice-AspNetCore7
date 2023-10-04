using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult RemoveIpInWeb(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");
            System.IO.File.WriteAllText(path, noidung);

            TempData["Logout_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult RemoveIpConnectWeb(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");

            System.IO.File.WriteAllText(path, noidung);

            return RedirectToAction("Index");
        }

        public ActionResult LockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung = docfile(path);

            if (noidung.Contains(ip) == false)
            {
            noidung += ip + "##";
            System.IO.File.WriteAllText(path, noidung);
            }
            TempData["Lock_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult AcceptContinueUseWeb()
        {
            TempData["continue"] = "";

            khoawebsiteClient();
            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            if (HttpContext.Session.GetObject<string>("continueX") != "ok")
            {
                TempData["ok-continue-X"] = "no";
                return RedirectToAction("Index");
            }

            HttpContext.Session.Remove("continueX");

            return View();
        }

        public ActionResult UnlockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");
            System.IO.File.WriteAllText(path, noidung);
            TempData["Unlock_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult ContinueUsedWebX (string? code)
        {
            khoawebsiteClient();
            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung1 = docfile(path1);

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung2 = docfile(path2);

            string IP = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            else
                IP = HttpContext.Session.GetString("userIP");

            if ((noidung1.Contains(IP) == true && noidung2.Contains(IP) == false) && code == "1234567890qwertyuiopasdfghjklzxcvbnm")
            {
                HttpContext.Session.SetObject("continueX", "ok");
                TempData["continue"] = "OK"; 
            }
            else
                TempData["continue-X"] = "NO";

            return RedirectToAction("Index");
        }

        public ActionResult AcceptContinue(int id) 
        {
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                var d = x.AddHours(DateTime.UtcNow, 7);
                var hour = String.Join("", d.Hour.ToString("00").Reverse().ToList());
                var minute = String.Join("", d.Minute.ToString("00").Reverse().ToList());

                if (id.ToString("0000") == minute + hour)
                {
                    TempData["ok-continue"] = "yes";
                string ID = "";
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
                   ID = HttpContext.Session.GetString("userIP");

                string message = "Báo cáo hành động [tiếp tục] bật sử dụng trang web của khách hàng mới (ID client đã được đăng kí mở trước đây, yêu cầu lại cấp phép mới) :\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@end\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 1]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@1_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@1_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@1_end\r\n\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 2]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@2_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@2_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@2_end\r\n\r\nThanks!"
                .Replace("@lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + ID)
                .Replace("@unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + ID)
                .Replace("@end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + ID)
             .Replace("@1_lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + IPx)
                .Replace("@1_unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + IPx)
                .Replace("@1_end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IPx)
                 .Replace("@2_lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + Request.HttpContext.Connection.RemoteIpAddress);

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + ID + "] - " + xuxu;
                    string host = "{" + Request.Host.ToString() + "}"
                               .Replace("http://", "")
                           .Replace("https://", "")
                           .Replace("/", "");

                    SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                          "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Continue Play On Web In Client Local  In " + name, message, "teinnkatajeqerfl");
                }
            else
                TempData["ok-continue-X"] = "no";

            return RedirectToAction("Index");
        }

        public ActionResult RemoveTempData()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}
