using MailKit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using System.Diagnostics.Eventing.Reader;
using System.Formats.Tar;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Security;

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

            TempData["X"] = ViewBag.X;

            if (upload == 0)
                ViewBag.Y = 0;
            else
                ViewBag.Y = 1;

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == true)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Delete(true);

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == true)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Delete(true);

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Create();

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Create();

            TempData["Y"] = ViewBag.Y;

            ViewBag.SL = sl;

            TempData["SL"] = ViewBag.SL;

            var pass = TempData["Password"];
            TempData["Password"] = pass;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UploadFile(List<IFormFile> fileUpload, List<string> TenFile, IFormCollection f)
        {
            int flag = 0;
            ViewBag.SL = TempData["SL"];
            ViewBag.X = TempData["X"];
            ViewBag.Y = TempData["Y"];

            TempData["SL"] = ViewBag.SL;
            TempData["X"] = ViewBag.X;
            TempData["Y"]  = ViewBag.Y;

            var homePass = f["Admin"].ToString() != null ? f["Admin"].ToString() : f["AdminX"].ToString();

            string folder = f["Folder"].ToString();
            string chon = f["DuKien"].ToString();
            string chonXY = f["DuKienXY"].ToString();

            string text = f["Text"].ToString();

            int flax = 0;
            if (folder.Length > 0 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Exists == false)
            {
                flax = 1;
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Create();
            }
            else
            {
                flax = 0;
            }

            try
            {
                if (fileUpload != null && fileUpload.Count() > 0)
                {
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;
                    
                    var password = homePass;
                    TempData["Password"] = password;

                    x = CultureInfo.InvariantCulture.Calendar;

                   xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    string fi = Request.HttpContext.Connection.RemoteIpAddress + "_ZipFile_" + xuxu;
                    fi = fi.Replace("\\", "");
                    fi = fi.Replace("/", "");
                    fi = fi.Replace(":", "");

                    if (password != "admin-VANLUAT")
                    {
                        if (chonXY == "1")
                        {
                            await MegaIo.UploadFile(fileUpload);

                            if (!new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Exists)
                                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Create();

                            for (int i = 0; i < fileUpload.Count(); i++)
                            {
                                var fileName = Path.GetFileName(fileUpload[i].FileName);

                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi, fileName);

                                using (Stream fileStream = new FileStream(path, FileMode.Create))
                                {
                                    fileUpload[i].CopyTo(fileStream);
                                }
                            }

                            ZipFile.CreateFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi), Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip"));

                            // IFormFile formFile = null;

                            //byte[] file = System.IO.File.ReadAllBytes(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip"));

                            // MemoryStream ms = new MemoryStream(file);
                            // formFile = new FormFile(ms, 0, ms.Length, null, fi + ".zip")
                            // {
                            //     Headers = new HeaderDictionary(),
                            //     ContentType = "application/zip"
                            // };

                            if ((Math.Ceiling((decimal)new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip")).Length) / 1024) / 1024 <= 20)
                            {
                                //mail.Attachments.Add(formFile);
                                //await _mailService.SendEmailAsync(mail);

                                MailRequest mail = new MailRequest();
                                mail.Subject = "Send file or message from " + name;
                                mail.Body = text;
                                mail.ToEmail = "mywebplay.savefile@gmail.com";

                                mail.Attachments = new List<IFormFile>();

                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUpload[i]);
                                }
                                await _mailService.SendEmailAsync(mail);
                            }
                            else
                            {
                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = "[PART " + (i + 1) + "] Send file or message from " + name;
                                    mail.Attachments.Add(fileUpload[i]);
                                    await _mailService.SendEmailAsync(mail);
                                }
                            }

                        }
                        else if (chonXY == "2")
                        {
                            if (!new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Exists)
                                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Create();

                            for (int i = 0; i < fileUpload.Count(); i++)
                            {
                                var fileName = Path.GetFileName(fileUpload[i].FileName);

                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi, fileName);

                                using (Stream fileStream = new FileStream(path, FileMode.Create))
                                {
                                    fileUpload[i].CopyTo(fileStream);
                                }
                            }

                            ZipFile.CreateFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi), Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip"));

                            // IFormFile formFile = null;

                            //byte[] file = System.IO.File.ReadAllBytes(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip"));

                            // MemoryStream ms = new MemoryStream(file);
                            // formFile = new FormFile(ms, 0, ms.Length, null, fi + ".zip")
                            // {
                            //     Headers = new HeaderDictionary(),
                            //     ContentType = "application/zip"
                            // };

                            if ((Math.Ceiling((decimal)new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip")).Length) / 1024) / 1024 <= 20)
                            {
                                //mail.Attachments.Add(formFile);
                                //await _mailService.SendEmailAsync(mail);

                                MailRequest mail = new MailRequest();
                                mail.Subject = "Send file or message from " + name;
                                mail.Body = text;
                                mail.ToEmail = "mywebplay.savefile@gmail.com";

                                mail.Attachments = new List<IFormFile>();

                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUpload[i]);
                                }
                                await _mailService.SendEmailAsync(mail);
                            }
                            else
                            {
                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = "[PART " + (i + 1) + "] Send file or message from " + name;
                                    mail.Attachments.Add(fileUpload[i]);
                                    await _mailService.SendEmailAsync(mail);
                                }
                            }
                        }
                        else
                        {
                            await MegaIo.UploadFile(fileUpload);
                        }
                    }
                }
            }
            catch
            {
                await MegaIo.UploadFile(fileUpload);
                return RedirectToAction("Index");
            }
            finally
            {
                if (ViewBag.Y == 1)
                {
                     if (flax == 0 && folder.Length > 0 && chon == "2" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Exists == true)
                    {
                        flag = 4;
                    }

                    if (flag == 0 && ViewBag.X == 1)
                    {
                        for (int i = 0; i < fileUpload.Count(); i++)
                        {
                            if (fileUpload[i] != null && (TenFile[i] == null || TenFile[i].Length <= 0))
                            {
                                flag = 1;
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
                                path = Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder, fileName);

                            string tenfile = ViewBag.X == 1 ? TenFile[i].ToString() : fileName;

                            var pth = "";
                            if (folder.Length == 0)
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile + System.IO.Path.GetExtension(path));
                            else
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder, tenfile + System.IO.Path.GetExtension(path));

                            if (System.IO.File.Exists(pth))
                            {
                                flag = 2;
                                break;
                            }
                        }
                    }

                    if (flag == 0 && ViewBag.X == 1)
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

                            string tenfile = ViewBag.X == 1 ? TenFile[i].ToString() : fileName;

                            var pth = "";
                            if (folder.Length == 0)
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile + System.IO.Path.GetExtension(path));
                            else
                                pth = Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder, tenfile + System.IO.Path.GetExtension(path));


                            using (Stream fileStream = new FileStream(path, FileMode.Create))
                            {
                                fileUpload[i].CopyTo(fileStream);

                            }

                            if (ViewBag.X == 1)
                                System.IO.File.Move(path, pth);
                        }
                    }
                }
            }

            //----------------------------------

            if (ViewBag.Y == 1)
            {
                if (ViewBag.X == 1 && flag == 1)
                {
                    ViewData["Loi"] = "Lỗi hệ thống. Nếu bạn đã đăng tải một file, hãy tự đặt lại tên mới gợi nhớ của bạn cho từng file đó ...";
                    return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 2)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (ViewBag.X == 1 && flag == 3)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 4 && flax == 0)
                {
                    ViewData["LoiX"] = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                    return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
            }

            //SendEmail.SendMail2Step("mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", name, name, "teinnkatajeqerfl");

            var xemPass = homePass == "admin-VANLUAT" ? " - OFF SAVE ADMIN IS CORRECT" : "";

                ViewBag.KetQua = ViewBag.Y == 0 ? "[NO UPLOAD] - Thành công (xử lý admin) !" : "[YES UPLOAD"+xemPass+"]" + " - Thành công! Tất cả các file đã được đăng tải lên Server hệ thống ...";
           
            return View("UploadFile", new {sl = ViewBag.SL, name = ViewBag.X, upload = ViewBag.Y});           
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
            else if (all == 2)
                ViewBag.All = 2;
            else if (all == 3)
                ViewBag.All = 3;
            else
                ViewBag.All = 4;

            ViewBag.Folder = folder;

            string ketqua = "";

            if (ViewBag.All == 4 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
            {
                ketqua = "";
                int k = 0;
                var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetFiles();
                if (listFile.Length == 0)
                    ViewBag.KQF = "Path : /file" + folder + " is empty (not exists files) ...";
                else
                {
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[IP Khách : "+Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort+" | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;


                    x = CultureInfo.InvariantCulture.Calendar;

                    xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    string fi = Request.HttpContext.Connection.RemoteIpAddress + "_ZipFile_" + folder + "_" + xuxu;
                    fi = fi.Replace("\\", "");
                    fi = fi.Replace("/", "");
                    fi = fi.Replace(":", "");

                    ViewBag.KQF = "Path : /file" + folder + " (nén các file trong folder thành zip) : ";

                    ZipFile.CreateFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder), Path.Combine(_webHostEnvironment.WebRootPath, "zip-result", fi + ".zip"));

                    ketqua += "Thành công! Xem hoặc download file của bạn (đã nén all thành 1 zip) <a style=\"color:purple\" href=\"/zip-result/" + fi + ".zip" + "\" download> tại đây</a>!<br>Hãy nhanh tải về trước khi file có thể bị xoá hoặc bị lỗi...";
                    ketqua += "<br><br>";
                    ViewBag.XL = 1;
                    ViewData["KetQua" + k] = ketqua;
                    ketqua = "";
                }
            }
            else if (ViewBag.All == 4 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";


            if (ViewBag.All == 1 && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
            {
                ketqua = "";
                int k = 0;
                var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).GetFiles();
                if (listFile.Length == 0)
                    ViewBag.KQF = "Path : /file" + folder + " is empty (not exists files) ...";
                else
                {
                    ViewBag.KQF = "* List all files in path /file" + folder + " (Số lượng : "+listFile.Length+" file) : ";
                    foreach (var item in listFile)
                    {
                        string file = folder + "/" + item.Name;

                        Calendar x = CultureInfo.InvariantCulture.Calendar;

                        ketqua += "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file" + folder + "/" + item.Name + "\" download> tại đây</a> <span style=\"color:pink\">("+ x.AddHours(item.LastWriteTime, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)+")</span> <br> Link xem đầy đủ : <a target=\"_blank\" style=\"color:green\"" +
                       "href=\"/file" + folder + "/" + item.Name + "\">/file" + folder + "/" + item.Name + "</a><br> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h (có thể) bạn đăng tải ...  " +
                      "<button style=\"color:blue\" onclick=\"xacnhan('"+ file + "')\">Click để xoá thủ công file này?</button><br><br>";
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

            if (ViewBag.All == 3)
            {
                int k = 0;
                ListFileDirectory("file", ref k);
                ViewBag.XL = k;
                ViewBag.KQF = "* Download list all file Server (toàn bộ - hiện tại có : "+k+" file)";
            }

                TempData["All"] = ViewBag.All;
            TempData["Folder"] = ViewBag.Folder;

                return View();
        }

        private void ListFileDirectory(string path, ref int k)
        {
            var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetFiles();
            string ketqua = "";
            foreach (var item in listFile)
            {
                string file = "/" + path + "/" +item.Name;

                if (new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, file.TrimStart("/".ToCharArray()))).Exists)
                {
                    Calendar x = CultureInfo.InvariantCulture.Calendar;
                    ketqua += "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/" + path + "/" + item.Name + "\" download> tại đây</a> <span style=\"color:pink\">("+ x.AddHours(item.LastWriteTime, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture) + ")</span> <br> Link xem đầy đủ : <a target=\"_blank\" style=\"color:green\"" +
                   "href=\"/" + path + "/" + item.Name + "\">/" + path + "/" + item.Name + "</a><br> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h (có thể) bạn đăng tải ...  " +
                  "<button style=\"color:blue\" onclick=\"xacnhan('" + file.Replace("/file","") + "')\">Click để xoá thủ công file này?</button><br><br>";
                    ketqua += "<br><br>";
                    ViewData["KetQua" + k] = ketqua;
                    ketqua = "";
                    k++;
                }
            }

            var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();
            foreach(var item in listFolder)
            {
                ListFileDirectory(path + "/" + item.Name, ref k);
            }
        }

        //[HttpPost]
        // public ActionResult DownloadFile(List<string> TenFile)
        // {
        //     ViewBag.All = TempData["All"];
        //     ViewBag.Folder = TempData["Folder"];
        //     TempData["Folder"] = ViewBag.Folder;
        //     TempData["All"] = ViewBag.All;

        //     for (int i = 0; i < TenFile.Count(); i++)
        //     {
        //         string ketqua = "";

        //         string tenfile = TenFile[i];


        //         if (tenfile != null && tenfile.Length > 0)
        //         {
        //             var pth = Path.Combine(_webHostEnvironment.WebRootPath, "file", tenfile);
        //             if (!System.IO.File.Exists(pth))
        //                 ketqua = "<p style=\"color:red\"> Tên file \"<span style=\"color:blue\">" + tenfile + "</span>\" bạn cần tìm để download không tồn tại trên Server (đảm bảo bạn phải nhập đủ tên file kèm theo đuôi file extension (VD : abc.txt hoặc abc.jpg hoặc abc.mp3  ...)!</p>";
        //             else
        //             {
        //                 string file = "/" + tenfile;

        //                 ViewBag.XL = TenFile.Count();
        //                 ketqua = "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file/" + tenfile + "\" download> tại đây</a> <span style=\"color:pink\">("+item.LastWriteTime+")</span> <br> Link xem đầy đủ : <a target=\"_blank\" style=\"color:green\"" +
        //                    "href=\"/file/" + tenfile + "\">/file/" + tenfile + "</a><br> Tải lại hoặc chờ một khoảng thời gian để link file được xử lý - tất cả file trên hệ thống admin sẽ tự động xoá sau 24h (có thể) bạn đăng tải ...  " +
        //                   "<button style=\"color:blue\" onclick=\"xacnhan('"+file+"')\">Click để xoá thủ công file này?</button><br><br>";
        //             }
        //         }

        //         ViewData["KetQua" + i] = ketqua;
        //     }      
        //     return View("DownloadFile",new {all =  ViewBag.All, folder = ViewBag.Folder});
        // }

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

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();
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
             FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+file));
            if (f.Exists)
            f.Delete();

            ViewBag.All = TempData["All"];
            ViewBag.Folder = TempData["Folder"];
            TempData["Folder"] = ViewBag.Folder;
            TempData["All"] = ViewBag.All;

            return RedirectToAction("DownloadFile",new { all = ViewBag.All, folder = ViewBag.Folder });
        }

        public ActionResult XoaFileX(string file)
        {
            if (new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+file)).Exists)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+file));
                f.Delete();
            }

            ViewBag.All = TempData["All"];
            ViewBag.Folder = TempData["Folder"];
            TempData["Folder"] = ViewBag.Folder;
            TempData["All"] = ViewBag.All;

            
            string[] fx = file!= null ? file.Split("/") : null;
            string fold = "";

            if (fx != null)
            {
                for (int i = 0; i < fx.Length - 1; i++)
                {
                    fold += fx[i];
                    if (i < fx.Length - 2)
                        fold += "/";
                }
            }

            return RedirectToAction("DownloadFile", new { all = 1, folder = fold });
        }
    }
}
