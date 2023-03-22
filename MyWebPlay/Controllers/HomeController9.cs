using MailKit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Formats.Tar;
using System.IO;
using System.Net.Mail;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult HD_API()
        {
            return View();
        }

        public ActionResult HD_Web_AspNetCore()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
         public async Task<ActionResult> UploadFile(IFormCollection f, IFormFile fileUpload)
        {

            if (fileUpload == null || fileUpload != null && fileUpload.FileName.Length == 0)
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.UploadFile();
            }

            string tenfile = f["TenFile"].ToString();
            if (string.IsNullOrEmpty(tenfile))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.UploadFile();
            }

            var fileName = Path.GetFileName(fileUpload.FileName);

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "file", fileName);

            var pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile + System.IO.Path.GetExtension(path));

            if (System.IO.File.Exists(pth))
            {
                ViewData["Loi1"] = "Tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!";
                return this.UploadFile();
            }

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                fileUpload.CopyTo(fileStream);

            }

            System.IO.File.Move(path, pth);

            string name = Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort+" - "+DateTime.Now+" - "+ tenfile + System.IO.Path.GetExtension(path);

            MailRequest mail = new MailRequest();
            mail.Subject = "Send file from " + name;
            mail.Body = "Send file from " + name;
            mail.ToEmail = "mywebplay.savefile@gmail.com";

            if (fileUpload != null)
            mail.Attachments.Add(fileUpload);

            await _mailService.SendEmailAsync(mail);

            //SendEmail.SendMail2Step("mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", name, name, "teinnkatajeqerfl");

            ViewBag.KetQua = "Thành công! Xem hoặc download file của bạn <a style=\"color:red\" href=\"/file/" + tenfile + System.IO.Path.GetExtension(path) + "\" download> tại đây</a>! <br> Link xem đầy đủ : <p style=\"color:green\">" +
                Request.Host.Host + ":" + Request.Host.Port + "/file/" + tenfile + System.IO.Path.GetExtension(path) + "</p> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h bạn đăng tải... ";
                

            return View();
        }

        public ActionResult DownloadFile()
        {
            return View();
        }

       [HttpPost]
        public ActionResult DownloadFile(IFormCollection f)
        {
            string tenfile = f["TenFile"].ToString();
            if (string.IsNullOrEmpty(tenfile))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.DownloadFile();
            }

            var pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile);

            if (!System.IO.File.Exists(pth))
            {
                ViewData["Loi1"] = "Tên file bạn cần tìm để download không tồn tại trên Server (đảm bảo bạn phải nhập đủ tên file kèm theo đuôi file extension (VD : abc.txt hoặc abc.jpg hoặc abc.mp3 ...)!";
                return this.DownloadFile();
            }

            ViewBag.KetQua = "Thành công! Xem hoặc download file của bạn <a style=\"color:red\" href=\"/file/" + tenfile + "\" download> tại đây</a>! <br> Link xem đầy đủ : <p style=\"color:green\">" +
                Request.Host.Host + ":" + Request.Host.Port + "/file/" + tenfile  + "</p> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h bạn đăng tải...  " +
                "<a style=\"color:grey\" href=\"/Home/XoaFile?file=" + tenfile+ "\" onclick=\"xacnhan()\">Click để xoá thủ công file này?</a><br><br>";

            return View();
        }

        public ActionResult XoaAllFile(string password)
        {
            if (string.Compare(password,"admin-VANLUAT", false) == 0)
            {
                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "file"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file", file));
                    f.Delete();
                }
            }

            return RedirectToAction("UploadFile");
        }

        public ActionResult XoaFile(string file)
        {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file", file));
                    f.Delete();

            return RedirectToAction("UploadFile");
        }
    }
}
