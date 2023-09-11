using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult LockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ListIPLock.txt");
            var noidung = docfile(path);

            if (noidung.Contains(ip) == false)
            {
            noidung += ip + "##";
            System.IO.File.WriteAllText(path, noidung);
            }
            return Redirect("https://google.com");
        }

        public ActionResult UnlockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ListIPLock.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");
            System.IO.File.WriteAllText(path, noidung);
            return Redirect("https://google.com");
        }
    }
}
