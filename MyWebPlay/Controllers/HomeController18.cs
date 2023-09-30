using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
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

            if (code == "1234567890qwertyuiopasdfghjklzxcvbnm")
            {
                TempData["continue"] = "OK";

                string IP;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    IP = endPoint.Address.ToString();
                }

                string message = "Báo cáo hành động [tiếp tục] bật sử dụng trang web của khách hàng mới (ID client đã được đăng kí mở trước đây, yêu cầu lại cấp phép mới) :\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@end"
               .Replace("@lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + IP)
               .Replace("@unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + IP)
               .Replace("@end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IP);

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IP + "] - " + xuxu;
                string host = "{" + Request.Host.ToString() + "}"
                           .Replace("http://", "")
                       .Replace("https://", "")
                       .Replace("/", "");

                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Continue Play On Web In Client Local  In " + name, message, "teinnkatajeqerfl");
            }
            else
                TempData["continue"] = "NO";

            return RedirectToAction("Index");
        }
    }
}
