using MailKit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics.Eventing.Reader;
using System.Formats.Tar;
using System.Globalization;
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

        public ActionResult UploadFile(int? sl = 1, int? name = 0, int? upload =0)
        {
            if (sl == null)
                ViewBag.SL = 0;

            if (name == 0)
                ViewBag.X = 0;
            else
            ViewBag.X = 1;

            if (upload == 0)
                ViewBag.Y = 0;
            else
                ViewBag.Y = 1;

            ViewBag.SL = sl;

            return View();
        }

   
        [HttpPost]
        public async Task<ActionResult> UploadFile(List<IFormFile> fileUpload, List<string> TenFile, IFormCollection f)
        {
            int flag = 0;
            if (TenFile.Count() > 0)
            {
                ViewBag.X = 1;
                ViewBag.Y = 1;
            }

            string text = f["Text"].ToString();
            string folder = f["Folder"].ToString();
            string chon = f["DuKien"].ToString();
            if (folder.Length > 0 && chon == "2" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\"+folder)).Exists == true)
            {
                ViewData["LoiX"] = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                return this.UploadFile(1, 1, 1);
            }

            if (folder.Length > 0 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Create();
            
            try
            {
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " - " + xuxu;

                    MailRequest mail = new MailRequest();
                    mail.Subject = "Send file or message from " + name;
                mail.Body = text;
                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                    mail.Attachments = new List<IFormFile>();

                    for (int i = 0; i < fileUpload.Count(); i++)
                        mail.Attachments.Add(fileUpload[i]);

                    await _mailService.SendEmailAsync(mail);
            }
            finally
            {
                if (ViewBag.X == 1 && ViewBag.Y == 1)
                {
                    for (int i = 0; i < fileUpload.Count(); i++)
                    {
                        if (fileUpload[i] != null && (TenFile[i] == null || TenFile[i].Length <= 0))
                        {
                            flag = 1;
                            break;
                        }
                    }

                    if (flag == 0)
                    {
                        for (int i = 0; i < fileUpload.Count(); i++)
                        {
                            var fileName = Path.GetFileName(fileUpload[i].FileName);

                            var path = "";
                            
                            if (folder.Length == 0)
                            path = Path.Combine(_webHostEnvironment.WebRootPath, "file", fileName);
                            else
                            path = Path.Combine(_webHostEnvironment.WebRootPath, "file\\"+folder, fileName);

                            string tenfile = TenFile[i].ToString();

                            var pth = "";
                            if (folder.Length == 0)
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile + System.IO.Path.GetExtension(path));
                            else
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file\\"+folder, tenfile + System.IO.Path.GetExtension(path));

                            if (System.IO.File.Exists(pth))
                            {
                                flag = 2;
                                break;
                            }
                        }
                    }

                    if (flag == 0)
                    {
                        for (int i = 0; i < TenFile.Count(); i++)
                        {
                            int dem = 0;
                            for (int j = 0; j < TenFile.Count(); j++)
                            {
                                if ((TenFile[j] != null && TenFile[j].Length > 0) && TenFile[j] == TenFile[i])
                                    dem++;
                            }

                            if (dem > 1)
                            {
                                flag = 3;
                                break;
                            }
                        }
                    }


                    if (flag == 0)
                    {
                        for (int i = 0; i < fileUpload.Count(); i++)
                        {

                            var fileName = Path.GetFileName(fileUpload[i].FileName);

                            var path = "";

                            if (folder.Length == 0)
                                path = Path.Combine(_webHostEnvironment.WebRootPath, "file", fileName);
                            else
                                path = Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder, fileName);

                            string tenfile = TenFile[i].ToString();

                            var pth = "";
                            if (folder.Length == 0)
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile + System.IO.Path.GetExtension(path));
                            else
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder, tenfile + System.IO.Path.GetExtension(path));


                            using (Stream fileStream = new FileStream(path, FileMode.Create))
                            {
                                fileUpload[i].CopyTo(fileStream);

                            }

                            System.IO.File.Move(path, pth);
                        }
                    }
                }
            }

            //----------------------------------

            if (ViewBag.X == 1 && ViewBag.Y == 1)
            {
                if (flag == 1)
                {
                    ViewData["Loi"] = "Lỗi hệ thống. Nếu bạn đã đăng tải một file, hãy tự đặt lại tên mới gợi nhớ của bạn cho từng file đó ...";
                    return this.UploadFile(1, 1, 1);
                }
                else if (flag == 2)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    return this.UploadFile(1, 1, 1);
                }
                else if (flag == 3)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    return this.UploadFile(1, 1, 1);
                }
            }

                //SendEmail.SendMail2Step("mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", name, name, "teinnkatajeqerfl");


                ViewBag.KetQua = ViewBag.X == 0 ? "[NO UPLOAD]" : "[YES UPLOAD]" + " - Thành công! Tất cả các file đã được đăng tải lên Server hệ thống ...";
           
                return View();
            
        }

        public ActionResult DownloadFile(int? sl, int? all = 0, string? folder = "")
        {
            if (sl == null)
                ViewBag.SL = 0;

            ViewBag.SL = sl;

            if (all == 0)
                ViewBag.All = 0;
            else if (all ==1)
                ViewBag.All = 1;
            else
                ViewBag.All = 2;

            string ketqua = "";

            if (ViewBag.All == 1 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
            {
                ketqua = "";
                int k = 0;
                var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetFiles();
                if (listFile.Length == 0)
                    ViewBag.KQF = "Path : /file" + folder + " is empty (not exists files) ...";
                else
                {
                    ViewBag.KQF = "* List all files in path /file" + folder + " : ";
                    foreach (var item in listFile)
                    {

                        ketqua += "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file" + folder + "/" + item.Name + "\" download> tại đây</a>! <br> Link xem đầy đủ : <a target=\"_blank\" style=\"color:green\"" +
                       "href=\"/file" + folder + "/" + item.Name + "\">/file" + folder + "/" + item.Name + "</a><br> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h (có thể) bạn đăng tải ...  " +
                      "<a target=\"_blank\" style=\"color:grey\" href=\"/Home/XoaFileX?file=" + folder + "/" + item.Name + "\" onclick=\"xacnhan()\">Click để xoá thủ công file này?</a><br><br>";
                        ketqua += "<br><br>";
                        ViewBag.XL = listFile.Count();
                        ViewData["KetQua" + k] = ketqua;
                        ketqua = "";
                        k++;
                    }
                }
            }
            else if (ViewBag.All == 1 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";

            if (ViewBag.All == 2 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
            {
                ketqua = "";
                int k = 0;
                var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetDirectories();
                if (listFolder.Length == 0)
                    ViewBag.KQF = "Path : /file" + folder + " is empty (not exists folder) ...";
                else
                {
                    ViewBag.KQF = "* List all folders (sub) in path /file" + folder + " (parent) : ";
                    foreach (var item in listFolder)
                    {
                        int count_folder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder + "/" + item.Name)).GetDirectories().Count();
                        int count_file = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder + "/" + item.Name)).GetFiles().Count();

                        ketqua += "<b syle=\"color:blue\">"+item.Name+"</b> (<span style=\"color:green\">"+count_folder+ "</span> folder AND <span style=\"color:green\">" + count_file+"</span> file)";

                        ViewBag.XL = listFolder.Count();
                        ViewData["KetQua" + k] = ketqua;
                        ketqua = "";
                        k++;
                    }
                }
            }
            else if(ViewBag.All == 2 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
            {
                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";
            }

                return View();
        }

       [HttpPost]
        public ActionResult DownloadFile(List<string> TenFile)
        {

            for (int i = 0; i < TenFile.Count(); i++)
            {
                string ketqua = "";

                string tenfile = TenFile[i];


                if (tenfile != null && tenfile.Length > 0)
                {
                    var pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile);
                    if (!System.IO.File.Exists(pth))
                        ketqua = "<p style=\"color:red\"> Tên file \"<span style=\"color:blue\">" + tenfile + "</span>\" bạn cần tìm để download không tồn tại trên Server (đảm bảo bạn phải nhập đủ tên file kèm theo đuôi file extension (VD : abc.txt hoặc abc.jpg hoặc abc.mp3  ...)!</p>";
                    else
                    {
                        ViewBag.XL = TenFile.Count();
                        ketqua = "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file/" + tenfile + "\" download> tại đây</a>! <br> Link xem đầy đủ : <a target=\"_blank\" style=\"color:green\"" +
                           "href=\"/file/" + tenfile + "\">/file/" + tenfile + "</a><br> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h (có thể) bạn đăng tải ...  " +
                          "<a target=\"_blank\" style=\"color:grey\" href=\"/Home/XoaFile?file="+ tenfile + "\" onclick=\"xacnhan()\">Click để xoá thủ công file này?</a><br><br>";
                    }
                }

                ViewData["KetQua" + i] = ketqua;
            }      
            return View();
        }

        public ActionResult XoaAllFile(string password)
        {
            if (string.Compare(password,"admin-VANLUAT", false) == 0)
            {
                DirectoryInfo fx = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"));
                fx.Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();
            }

            return RedirectToAction("UploadFile");
        }

        public ActionResult XoaFolder(string folder, string chon)
        {
            if (chon == "1")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
               new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Delete(true);          
            }
            else if (chon == "2")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
                {
                    var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetDirectories();
                    foreach (var fold in listFolder)
                    {
                        fold.Delete(true);
                    }
                }
            }
            else if (chon == "3")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
                {
                    var listFile= new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetFiles();
                    foreach (var file in listFile)
                    {
                        file.Delete();
                    }
                }
            }
            else if (chon == "4")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+folder)).Create();
            }

            return RedirectToAction("UploadFile");
        }

        public ActionResult XoaFile(string file)
        {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file", file));
                    f.Delete();

            return RedirectToAction("DownloadFile");
        }

        public ActionResult XoaFileX(string file)
        {
            if (new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + file)).Exists)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + file));
                f.Delete();
            }

            return RedirectToAction("UploadFile");
        }
    }
}
