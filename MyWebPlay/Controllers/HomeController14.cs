using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Formats.Tar;
using System.Globalization;
using System.IO;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PlayKaraoke()
        {
            ViewBag.Music = "";

            ViewBag.Karaoke = "karaoke";

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            return View();
        }

        [HttpPost]
        public ActionResult PlayKaraoke(IFormCollection f, IFormFile txtKaraoke, IFormFile txtMusic)
        {
            var fileName = Path.GetFileName(txtMusic.FileName);

            ViewBag.Karaoke = "";

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtMusic.CopyTo(fileStream);
            }

            ViewBag.Music = "/karaoke/music/" + fileName;
            ViewBag.Text = f["txtBH"].ToString();

            fileName = Path.GetFileName(txtKaraoke.FileName);

            path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fileName);

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtKaraoke.CopyTo(fileStream);
            }

            ViewBag.Karaoke = System.IO.File.ReadAllText(path);

            return View();
        }

        public ActionResult CreateFile_Karaoke()
        {
            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            ViewBag.LyricVD = "Tình như giấc mơ\r\nHãy giữ ai ơi cho giấc mơ còn đầy\r\nMột khi đã yêu đừng quên\r\nCon tim sẽ mang thêm bao ưu phiền.\r\nAnh hỡi chớ có thờ ơ\r\nYêu em với nụ cười đắm say\r\nBầu trời đẹp xinh\r\nYêu em với ánh mắt thắm thiết\r\nChứa chan những ân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\n[Empty]\r\nTình như giấc mơ\r\nHãy giữ ai ơi cho giấc mơ còn đầy\r\nMột khi đã yêu đừng quên\r\nCon tim sẽ mang thêm bao ưu phiền.\r\nAnh hỡi chớ có thờ ơ\r\nYêu em với nụ cười đắm say\r\nBầu trời đẹp xinh\r\nYêu em với ánh mắt thắm thiết\r\nChứa chan những ân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\n[Empty]";
            return View();
        }

        [HttpPost]
        public ActionResult CreateFile_Karaoke(IFormFile txtMusic, IFormCollection f)
        {
            TempData["Lyric"] = f["txtLyric"].ToString();
            TempData["Music"] = "/karaoke/music/"+txtMusic.FileName;

            var fileName = Path.GetFileName(txtMusic.FileName);

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtMusic.CopyTo(fileStream);
            }

            return RedirectToAction("PlayCreateFile_Karaoke");
        }

        public ActionResult PlayCreateFile_Karaoke()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlayCreateFile_Karaoke(IFormCollection f)
        {
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string fi = Request.HttpContext.Connection.RemoteIpAddress + "_Karaoke_" + xuxu + ".txt";
            fi = fi.Replace("\\", "");
            fi = fi.Replace("/", "");
            fi = fi.Replace(":", "");

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fi);

            System.IO.File.WriteAllText(path, f["txtLyric"].ToString().Replace("undefined",""));

            ViewBag.FileKaraoke = "<p style=\"color:blue\">Thành công, một file TXT Karaoke của bạn đã được xử lý...</p><a href=\"/karaoke/text/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:yellow\" id=\"thoigian3\" class=\"thoigian3\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>";
            return View();
        }

        public ActionResult XoaKaraoke()
        {
            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            return RedirectToAction("PlayKaraoke");
        }

    }
}
