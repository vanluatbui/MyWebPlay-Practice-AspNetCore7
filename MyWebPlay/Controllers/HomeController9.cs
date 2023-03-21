using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;
using System.IO;

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
         public ActionResult UploadFile(IFormCollection f, IFormFile fileUpload)
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

            ViewBag.KetQua = "Thành công! Xem hoặc download file của bạn <a style=\"color:red\" href=\"/file/" + tenfile + System.IO.Path.GetExtension(path)+"\" download> tại đây</a>! <br> Link xem đầy đủ : <p style=\"color:green\">" + 
                Request.Host.Host + ":" + Request.Host.Port + "/file/" + tenfile + System.IO.Path.GetExtension(path) + "</p> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - vui lòng giữ lại liên kết này...";

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
                Request.Host.Host + ":" + Request.Host.Port + "/file/" + tenfile + "</p> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - vui lòng giữ lại liên kết này...";

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
    }
}
