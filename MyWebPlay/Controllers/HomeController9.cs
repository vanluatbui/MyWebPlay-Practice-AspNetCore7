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
using MyWebPlay.Model;
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult HD_API()
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
            return View();
        }

        public ActionResult HD_Web_AspNetCore()
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
            return View();
        }

        public ActionResult UploadFile(string? type, int? sl = 1, int? name = 0, int? upload =1)
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

            if (string.IsNullOrEmpty(type) == false && type.Contains("<splix>"))
            {
                var po = type.Split("<splix>");
                sl = int.Parse(po[0]);
                name = int.Parse(po[1]);
                upload = int.Parse(po[2]);
            }

            if (HttpContext.Session.GetString("adminDirectURL") != null && HttpContext.Session.GetString("adminDirectURL") == "YES")
            {
                HttpContext.Session.Remove("adminDirectURL");
                TempData["directURL"] = "true";
            }
            else
            {
                TempData["directURL"] = "false";
            }

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
        public async Task<ActionResult> UploadFile(List<IFormFile> fileUpload, List<IFormFile> fileUploadX, List<string> TenFile, IFormCollection f)
        {
            /*HttpContext.Session.Remove("ok-data");*/ TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                TempData["nav_link"] = "text-dark"; TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                TempData["nav_link"] = "text-light"; TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flah = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flah == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note"))
                {
                    if (info[1] == "false")
                    {
                        
                        TempData["mau_winx"] = "red";
                        flah = 1;
                    }
                    else
                    {
                        
                        TempData["mau_winx"] = "deeppink";
                        flah = 0;
                    }
                }
            }

            var gmail = true;
            var mega = true;
            var passAd = "";

            if (TempData["Y"].ToString() == "1")
            {
                var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
                var noidungX1 = System.IO.File.ReadAllText(pathX1);

                var listSetting1 = noidungX1.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting1.Length; i++)
                {
                    var info = listSetting1[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    if (info[0] == "Email_Upload_User")
                    {
                        if (info[1] == "false")
                        {
                            gmail = false;
                        }
                        else
                        {
                            gmail = true;
                        }

                        if (HttpContext.Session.GetString("trust-X-you") == "true")
                        {
                            gmail = false;
                        }
                    }

                    if (info[0] == "MegaIo_Upload_User")
                    {
                        if (info[1] == "false")
                        {
                            mega = false;
                        }
                        else
                        {
                            mega = true;
                        }

                        if (HttpContext.Session.GetString("trust-X-you") == "true")
                        {
                            mega = false;
                        }
                    }

                    if (info[0] == "Password_Admin")
                    {
                        passAd = info[3];
                    }
                }
            }

                        DateTime ngayhethan = DateTime.Now;
            var success = DateTime.TryParse(f["txtHetHan"].ToString(), out ngayhethan);
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");

            var hethan = ngayhethan.ToString("dd/MM/yyyy").Split("/");
            var homnay = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split("/");
            var kiemtra = CultureInfo.InvariantCulture.Calendar.AddDays(DateTime.UtcNow, 7).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split("/");
            var d1 = int.Parse(hethan[0]);
            var m1 = int.Parse(hethan[1]);
            var y1 = int.Parse(hethan[2]);

            var d2 = int.Parse(homnay[0]);
            var m2 = int.Parse(homnay[1]);
            var y2 = int.Parse(homnay[2]);

            var d3 = int.Parse(kiemtra[0]);
            var m3 = int.Parse(kiemtra[1]);
            var y3 = int.Parse(kiemtra[2]);

            long tong = 0;
            for (int i = 0; i < fileUpload.Count; i++)
                tong += fileUpload[i].Length;

            for (int i = 0; i < fileUploadX.Count; i++)
                tong += fileUploadX[i].Length;

            int flag = 0;
            ViewBag.SL = TempData["SL"];
            ViewBag.X = TempData["X"];
            ViewBag.Y = TempData["Y"];

            TempData["SL"] = ViewBag.SL;
            TempData["X"] = ViewBag.X;
            TempData["Y"]  = ViewBag.Y;

            var xanh = (ViewBag.Y == 0) ? "[ADMIN]" : "[USER]";

            var formFile = new List<IFormFile>();
            if (f["DuKienYX"].ToString() == "1")
                formFile = new List<IFormFile>(fileUpload);
            else
                formFile = new List<IFormFile>(fileUploadX);

            var say = fileUpload.Count + fileUploadX.Count;

                var homePass = f["Admin"].ToString() != null ? f["Admin"].ToString() : f["AdminX"].ToString();

            string folder = f["Folder"].ToString();
            string chon = f["DuKien"].ToString();
            string chonXY = f["DuKienXY"].ToString();

            string text = xanh + " - " + f["Text"].ToString() + "\n\n#############################################################################\n\n";

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
                var password = homePass;
                TempData["Password"] = password;

                if (formFile != null && formFile.Count() > 0)
                {
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
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
                    x = CultureInfo.InvariantCulture.Calendar;

                   xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    string fi = HttpContext.Session.GetString("userIP") + "_ZipFile_" + xuxu;
                    fi = fi.Replace("\\", "");
                    fi = fi.Replace("/", "");
                    fi = fi.Replace(":", "");

                    if (password == passAd)
                    {
                        if (chonXY == "1")
                        {
                            if (mega == true)
                                await MegaIo.UploadFile(formFile);

                            if (!new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Exists)
                                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Create();

                            for (int i = 0; i < formFile.Count(); i++)
                            {
                                var fileName = Path.GetFileName(formFile[i].FileName);

                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi, fileName);

                                using (Stream fileStream = new FileStream(path, FileMode.Create))
                                {
                                        formFile[i].CopyTo(fileStream);
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
                                mail.Subject = host+" Send file or message from " + name+ " - with MegaIO:"+mega+" (" + say + " files uploaded)";
                                text += "\n\n* List file have upload ("+ fileUpload.Count + " files)  :\n\n";
                                for (int s = 0; s < fileUpload.Count; s++)
                                {
                                    text += "\n\n+ File " + (s + 1) + " : " + fileUpload[s].FileName + "\n\n";
                                }
                                text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload ("+fileUploadX.Count+" files)  :\n\n";
                                for (int u = 0; u < fileUploadX.Count; u++)
                                {
                                    text += "\n\n+ File " + (u + 1) + " : " + fileUploadX[u].FileName + "\n\n";
                                }
                                mail.Body = text;
                                mail.ToEmail = "mywebplay.savefile@gmail.com";

                                mail.Attachments = new List<IFormFile>();

                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUpload[i]);
                                }
                                for (int i = 0; i < fileUploadX.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUploadX[i]);
                                }
                                if (gmail == true)
                                await _mailService.SendEmailAsync(mail);
                            }
                            else
                            {
                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();
                                    text += "\n\n* List file have upload ("+ fileUpload.Count + " files)  :\n\n";
                                    for (int s = 0; s < fileUpload.Count; s++)
                                    {
                                        text += "\n\n+ File " + (s + 1) + " : " + fileUpload[s].FileName + "\n\n";
                                    }
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + " - with MegaIo] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUpload[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail);
                                }

                                for (int i = 0; i < fileUploadX.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();

                                    text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload ("+fileUploadX.Count+" files)  :\n\n";
                                    for (int u = 0; u < fileUploadX.Count; u++)
                                    {
                                        text += "\n\n+ File " + (u + 1) + " : " + fileUploadX[u].FileName + "\n\n";
                                    }
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + " - with MegaIo] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUploadX[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail);
                                }
                            }

                        }
                        else if (chonXY == "2")
                        {
                            if (!new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Exists)
                                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi)).Create();

                            for (int i = 0; i < formFile.Count(); i++)
                            {
                                var fileName = Path.GetFileName(formFile[i].FileName);

                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail", fi, fileName);

                                using (Stream fileStream = new FileStream(path, FileMode.Create))
                                {
                                        formFile[i].CopyTo(fileStream);
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
                                mail.Subject = host + " Send file or message from " + name + "("+say+" files uploaded)";
                                text += "\n\n* List file have upload ("+ fileUpload.Count + " files)  :\n\n";
                                for (int s = 0; s < fileUpload.Count; s++)
                                {
                                    text += "\n\n+ File " + (s + 1) + " : " + fileUpload[s].FileName + "\n\n";
                                }
                                text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload ("+fileUploadX.Count+" files)  :\n\n";
                                for (int u = 0; u < fileUploadX.Count; u++)
                                {
                                    text += "\n\n+ File " + (u + 1) + " : " + fileUploadX[u].FileName + "\n\n";
                                }
                                mail.Body = text;
                                mail.ToEmail = "mywebplay.savefile@gmail.com";

                                mail.Attachments = new List<IFormFile>();

                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUpload[i]);
                                }
                                for (int i = 0; i < fileUploadX.Count(); i++)
                                {
                                    mail.Attachments.Add(fileUploadX[i]);
                                }
                                if (gmail == true)
                                    await _mailService.SendEmailAsync(mail);
                            }
                            else
                            {
                                for (int i = 0; i < fileUpload.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();
                                    text += "\n\n* List file have upload ("+ fileUpload.Count + " files)  :\n\n";
                                    for (int s = 0; s < fileUpload.Count; s++)
                                    {
                                        text += "\n\n+ File " + (s + 1) + " : " + fileUpload[s].FileName + "\n\n";
                                    }
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + "] Send file or message from " + name + "("+say+" files uploaded)";
                                    mail.Attachments.Add(fileUpload[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail);
                                }
                                for (int i = 0; i < fileUploadX.Count(); i++)
                                {
                                    MailRequest mail = new MailRequest();
                                    text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload ("+fileUploadX.Count+" files)  :\n\n";
                                    for (int s = 0; s < fileUploadX.Count; s++)
                                    {
                                        text += "\n\n+ File " + (s + 1) + " : " + fileUploadX[s].FileName + "\n\n";
                                    }
                                    mail.Body = text;
                                    mail.ToEmail = "mywebplay.savefile@gmail.com";

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + "] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUploadX[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail);
                                }
                            }
                        }
                        else
                        {
                            if (mega == true)
                            {
                                await MegaIo.UploadFile(fileUpload);
                                await MegaIo.UploadFile(fileUploadX);
                            }

                            MailRequest mail = new MailRequest();
                            mail.Subject = host + " Send file or message from " + name+" - File Upload In MegaIO:"+mega+" ("+say+" files uploaded)";
                            text += "\n\n* List file have upload in MegaIO ("+fileUpload.Count+" files) :\n\n";
                            for (int i = 0; i < fileUpload.Count; i++)
                            {
                                text += "\n\n+ File " + (i + 1) + " : " + fileUpload[i].FileName + "\n\n";
                            }
                            text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload in MegaIO ("+fileUploadX.Count+" files) :\n\n";
                            for (int i = 0; i < fileUploadX.Count; i++)
                            {
                                text += "\n\n+ File " + (i + 1) + " : " + fileUploadX[i].FileName + "\n\n";
                            }
                            mail.Body = text;
                            mail.ToEmail = "mywebplay.savefile@gmail.com";
                            if (gmail == true)
                                await _mailService.SendEmailAsync(mail);
                        }
                    }
                }
            }
            catch
            {
                if (mega == true)
                {
                    await MegaIo.UploadFile(fileUpload);
                    await MegaIo.UploadFile(fileUploadX);
                }
                MailRequest mail = new MailRequest();
                mail.Subject = host + " Send file or message from " + "[BY ERROR]" + " - File Upload In MegaIO:"+mega+" (" + say + " files uploaded)";
                text += "\n\n* List file have upload in MegaIO ("+fileUpload.Count+" files) :\n\n";
                for (int i = 0; i < fileUpload.Count; i++)
                {
                    text += "\n\n+File " + (i + 1) + " : " + fileUpload[i].FileName + "\n\n";
                }
                text += "\n\n-----------------------------------------------------------------------------------------------\n\n* List file by use folders have upload in MegaIO ("+fileUploadX.Count+" files) :\n\n";
                for (int i = 0; i < fileUploadX.Count; i++)
                {
                    text += "\n\n+File " + (i + 1) + " : " + fileUploadX[i].FileName + "\n\n";
                }
                mail.Body = text;
                mail.ToEmail = "mywebplay.savefile@gmail.com";
                if (gmail == true)
                    await _mailService.SendEmailAsync(mail);
                return RedirectToAction("Index");
            }
            finally
            {
                if (ViewBag.Y == 1)
                {
                        if (flag == 0 && homePass != passAd && tong > 2000000)
                    {
                        flag = 6;
                    }

                     if (flax == 0 && folder.Length > 0 && chon == "2" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file\\" + folder)).Exists == true)
                    {
                        flag = 4;
                    }

                    if (flag == 0 && ViewBag.X == 1)
                    {
                        for (int i = 0; i < formFile.Count(); i++)
                        {
                            if (formFile[i] != null && (TenFile[i] == null || TenFile[i].Length <= 0))
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }

                    if (flag == 0)
                    {
                        for (int i = 0; i < formFile.Count(); i++)
                        {
                            var fileName = Path.GetFileName(formFile[i].FileName);

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
                        for (int i = 0; i < formFile.Count(); i++)
                        {

                            var fileName = Path.GetFileName(formFile[i].FileName);
             
                            var path = "";

                            if (folder.Length == 0)
                                path = Path.Combine(_webHostEnvironment.WebRootPath, "file", fileName);
                            else
                                path = Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder, fileName);

                            var pao = "";
                            if (folder.Length == 0)
                                pao = "file/" + fileName;
                            else
                                pao = "file" + folder + "/" + fileName;

                            if (homePass != passAd)
                            {
                                var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"));
                                var files = infoFile.Split("\n");

                                int flay = 0;
                                for (int x = 0; x < files.Length; x++)
                                {
                                    if (files[x] == "") continue;

                                    var fi = files[x].Split("\t");
                                    if (fi[0] == pao)
                                    {
                                        flay = 1;
                                        break;
                                    }
                                }

                                if (flay == 0)
                                {
                                    var result = pao + "\t" + DateTime.Parse(f["txtHetHan"].ToString()).ToString("dd/MM/yyyy")+"\n";
                                        if (homePass == passAd || success == true && SoSanh2Ngay(d1, m1, y1, d2, m2, y2) > 0 && SoSanh2Ngay(d1, m1, y1, d3, m3, y3) <= 0)
                                        System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"), infoFile + result);
                                }
                            }

                            string tenfile = ViewBag.X == 1 ? TenFile[i].ToString() : fileName;

                            if (homePass == passAd || success == true && SoSanh2Ngay(d1, m1, y1, d2, m2, y2) > 0 && SoSanh2Ngay(d1, m1, y1, d3, m3, y3) <= 0)
                            {
                                using (Stream fileStream = new FileStream(path, FileMode.Create))
                                {
                                    formFile[i].CopyTo(fileStream);
                                }
                            }
                        }
                    }

                    if (flag == 0 && homePass != passAd)
                    {
                        if (success == false || (success == true && (SoSanh2Ngay(d1,m1,y1,d2,m2,y2) <= 0 || SoSanh2Ngay(d1, m1, y1, d3, m3, y3) > 0)))
                        {
                            flag = 5;                     
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
                    ViewBag.KetQua = "Lỗi hệ thống. Nếu bạn đã đăng tải một file, hãy tự đặt lại tên mới gợi nhớ của bạn cho từng file đó ...";

                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 2)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    ViewBag.KetQua = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (ViewBag.X == 1 && flag == 3)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    ViewBag.KetQua = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 4 && flax == 0)
                {
                    ViewData["LoiX"] = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                    ViewBag.KetQua = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 5 && homePass != passAd)
                {
                    ViewData["LoiY"] = "Vui lòng chọn ngày hết hạn các file này sau ngày hôm nay và thời hạn các file của bạn được phép tồn tại trên Server hệ thống là 7 ngày!";
                    ViewBag.KetQua = "Vui lòng chọn ngày hết hạn các file này sau ngày hôm nay và thời hạn các file của bạn được phép tồn tại trên Server hệ thống là 7 ngày!";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 6 && homePass != passAd)
                {
                    ViewData["Loi"] = "⚠️ Hiện tại mỗi lượt bạn chỉ có thể tải lên hệ thống các file tổng kích thước tối đa không quá 2 MB!";
                    ViewBag.KetQua = "⚠️ Hiện tại mỗi lượt bạn chỉ có thể tải lên hệ thống các file tổng kích thước tối đa không quá 2 MB!";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile(ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
            }

            //SendEmail.SendMail2Step("mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", name, name, "teinnkatajeqerfl");
            
                var xemPass = homePass ==  passAd ? " - OFF SAVE ADMIN IS CORRECT" : "";

                ViewBag.KetQua = ViewBag.Y == 0 ? "[NO UPLOAD] - Thành công (xử lý admin) !" : "[YES UPLOAD"+xemPass+"]" + " - Thành công! Tất cả các file đã được đăng tải lên Server hệ thống ...";
           
            return View("UploadFile", new {sl = ViewBag.SL, name = ViewBag.X, upload = ViewBag.Y});           
        }

        public ActionResult FindSubFolders (string folder, string password)
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
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "Password_Admin")
                {
                    passAd = info[3];
                }
            }

                if (password != passAd || HttpContext.Session.GetObject<string>("LoginAdmin") != "YES")
            {
                return Redirect("/Home/DownloadFile");
            }

            return RedirectToAction("DownloadFile", new { all = "584118ea7469832675dd4799247d84fb", folder = folder });
        }

        public ActionResult AllDownload(string password)
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
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "Password_Admin")
                {
                    passAd = info[3];
                }
            }
                if (password != passAd || HttpContext.Session.GetObject<string>("LoginAdmin") != "YES")
            {
                return Redirect("/Home/DownloadFile");
            }
            return RedirectToAction("DownloadFile", new { all = "1a852f29bdcaead120eaa272889bfa54"});
        }

        public ActionResult DownloadFile(int? sl, string? all = "0", string? folder = "")
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

            if (string.IsNullOrEmpty(folder) == false &&  folder.Contains("<split>"))
            {
                var po = folder.Split("<split>", StringSplitOptions.RemoveEmptyEntries);
                folder = po[0];
                all = po[1];
            }

            if (HttpContext.Session.GetString("adminDirectURL") != null && HttpContext.Session.GetString("adminDirectURL") == "YES")
                {
                    HttpContext.Session.Remove("adminDirectURL");
                    TempData["directURL"] = "true";
                }
                else
                {
                    TempData["directURL"] = "false";
                }

            if (sl == null)
                ViewBag.SL = 0;

            ViewBag.SL = sl;

            if (all == "0")
                ViewBag.All = "0";
            else if (all == "1")
                ViewBag.All = "1";
            else if (all == "584118ea7469832675dd4799247d84fb")
                ViewBag.All = "584118ea7469832675dd4799247d84fb";
            else if (all == "1a852f29bdcaead120eaa272889bfa54")
                ViewBag.All = "1a852f29bdcaead120eaa272889bfa54";
            else
                if (all == "4")
                ViewBag.All = "4";

            ViewBag.Folder = folder;

            string ketqua = "";

            if (ViewBag.All == "4" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
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
                    string localIP = "";
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                    else
                        localIP = HttpContext.Session.GetString("userIP");

                    var IPx = "";
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        IPx = endPoint.Address.ToString();
                    }

                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;

                    x = CultureInfo.InvariantCulture.Calendar;

                    xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    string fi = HttpContext.Session.GetString("userIP") + "_ZipFile_" + folder + "_" + xuxu;
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
            else if (ViewBag.All == "4" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";


            if (ViewBag.All == "1" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
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

                        ketqua += "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file" + folder + "/" + item.Name + "\" download> tại đây</a> <span style=\"color:seagreen\">("+ x.AddHours(item.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)+")</span> <br> Link xem trực tiếp (nếu có thể) : <a target=\"_blank\" style=\"color:green\"" +
                       "href=\"/file" + folder + "/" + item.Name + "\">/file" + folder + "/" + item.Name + "</a><br><br>" +
                      "<button style=\"color:blue\" onclick=\"xacnhan('"+ file + "')\">Click để xoá thủ công file này?</button><br>";
                        ketqua += "<br><br>";
                        ViewBag.XL = listFile.Count();
                        ViewData["KetQua" + k] = ketqua;
                        ketqua = "";
                        k++;
                    }
                }
            }
            else if (ViewBag.All == "1" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";

            if (ViewBag.All == "584118ea7469832675dd4799247d84fb" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists)
            {
                if (HttpContext.Session.GetObject<string>("LoginAdmin") != "YES")
                    return RedirectToAction("DownloadFile");

                HttpContext.Session.Remove("LoginAdmin");

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
            else if(ViewBag.All == "584118ea7469832675dd4799247d84fb" && new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file" + folder)).Exists == false)
            {
                if (HttpContext.Session.GetObject<string>("LoginAdmin") != "YES")
                    return RedirectToAction("DownloadFile");

                HttpContext.Session.Remove("LoginAdmin");

                ViewBag.KQF = "Not Exists Folder Path : /file" + folder + "  ...";
            }

            if (ViewBag.All == "1a852f29bdcaead120eaa272889bfa54")
            {
                if (HttpContext.Session.GetObject<string>("LoginAdmin") != "YES")
                    return RedirectToAction("DownloadFile");

                int k = 0;
                ListFileDirectory("file", ref k);
                ViewBag.XL = k;
                ViewBag.KQF = "* Download list all file Server (toàn bộ - hiện tại có : "+k+" file)";
                HttpContext.Session.Remove("LoginAdmin");
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
                    ketqua += "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/" + path + "/" + item.Name + "\" download> tại đây</a> <span style=\"color:seagreen\">("+ x.AddHours(item.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture) + ")</span> <br> Link xem trực tiếp (nếu có thể) : <a target=\"_blank\" style=\"color:green\"" +
                   "href=\"/" + path + "/" + item.Name + "\">/" + path + "/" + item.Name + "</a><br><br>";
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
        //                 ketqua = "Thành công! Xem hoặc download file của bạn <a style=\"color:purple\" href=\"/file/" + tenfile + "\" download> tại đây</a> <span style=\"color:pink\">("+item.LastWriteTime+")</span> <br> Link xem trực tiếp (nếu có thể) : <a target=\"_blank\" style=\"color:green\"" +
        //                    "href=\"/file/" + tenfile + "\">/file/" + tenfile + "</a><br><br>" +
        //                   "<button style=\"color:blue\" onclick=\"xacnhan('"+file+"')\">Click để xoá thủ công file này?</button><br>";
        //             }
        //         }

        //         ViewData["KetQua" + i] = ketqua;
        //     }      
        //     return View("DownloadFile",new {all =  ViewBag.All, folder = ViewBag.Folder});
        // }

        public ActionResult XoaAllFile(string password, string url)
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
           
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "Password_Admin")
                {
                    passAd = info[3];
                }
            }

                if (string.Compare(password,passAd, false) == 0 && HttpContext.Session.GetObject<string>("LoginAdmin") == "YES")
            {
                HttpContext.Session.Remove("LoginAdmin");
                DirectoryInfo fx = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"));
                fx.Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();

                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"), "");
                TempData["LoginAdmin"] = "Admin delete all file in Server successful!";
            }
            return RedirectToAction(url);
        }

        public ActionResult XoaFolder(string folder, string chon)
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
            FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+file));
            if (f.Exists)
            f.Delete();

            ViewBag.All = TempData["All"];
            ViewBag.Folder = TempData["Folder"];
            TempData["Folder"] = ViewBag.Folder;
            TempData["All"] = ViewBag.All;

            return RedirectToAction("DownloadFile",new { folder = ViewBag.Folder + "<split>"+ViewBag.All });
        }

        public ActionResult XoaFileX(string file)
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

            return RedirectToAction("DownloadFile", new { folder = fold + "<split>1" });
        }
    }
}
