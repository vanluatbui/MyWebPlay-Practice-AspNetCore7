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
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult HD_Web_AspNetCore()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult UploadFile(string? type, int? sl = 1, int? name = 0, int? upload =1)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }


        [HttpPost]
        public async Task<ActionResult> UploadFile(List<IFormFile> fileUpload, List<IFormFile> fileUploadX, List<string> TenFile, IFormCollection f, string? txtExternal = "false", string? External = "false")
        {
            try
            {
                var pthX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                var nd = System.IO.File.ReadAllText(pthX);
                var onoff = nd.Replace("\r","").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                var email = f["txtMail"].ToString();

                if (txtExternal == "true" || External == "true")
                    TempData["Y"] = 0;

                if (onoff == "file_TAT" && TempData["Y"].ToString() == "1")
                {
                    if (External == "false")
                    return RedirectToAction("UploadFile");
                    return Ok(new { error = "Dịch vụ đang tạm ngừng, mời bạn thử lại sau!" });
                }

                if (External == "false")
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                }

                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flah = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flah == 0 && (info[0] == "Email_Upload_User"
                            || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                            || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                            || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                            || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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

                        if (info[0] == "External_Unenable")
                        {
                            if (txtExternal == "true" && info[1] == "true")
                                return Redirect("https://google.com");
                        }

                        if (info[0] == "External_Post")
                        {
                            if (External == "true" && info[1] == "false")
                                return Ok(new { error = "Bạn không được phép liên lạc tính năng này!" });
                        }
                    }

            var gmail = true;
            var mega = true;
            var passAd = "";

            if (TempData["Y"].ToString() == "1")
            {
                var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX1 = System.IO.File.ReadAllText(pathX1);

                var listSetting1 = noidungX1.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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

                        if (ViewBag.Y == 0)
                        {
                            gmail = true;
                            mega = true;
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

                 
            
            //==============================================
                
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

                var chonYX = "";
                if (string.IsNullOrEmpty(f["DuKienYX"]))
                    chonYX = "1";
                else
                    chonYX = f["DuKienYX"].ToString();

                if (chonYX == "1")
                formFile = new List<IFormFile>(fileUpload);
            else
                formFile = new List<IFormFile>(fileUploadX);

            var say = fileUpload.Count + fileUploadX.Count;

                var homePass = f["Admin"].ToString() != null ? f["Admin"].ToString() : f["AdminX"].ToString();

            string folder = f["Folder"].ToString();
            string chon = f["DuKien"].ToString();
            string chonXY = "";
                if (string.IsNullOrEmpty(f["DuKienXY"]))
                    chonXY = "2";
                else
                    chonXY = f["DuKienXY"].ToString();

                if (folder.Contains("/file/") || folder.Contains("/#fileclose/"))
                        return RedirectToAction("Error");

                var text = "";
                if (string.IsNullOrEmpty(f["Text"]))
                    text = "External upload file [mywebplay] - admin";
                else
            text = xanh + " - " + f["Text"].ToString() + "\n\n#############################################################################\n\n";

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

                    if ((External == "false" && txtExternal == "false") && string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
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

                    if (password != passAd && ViewBag.Y == 1 || ViewBag.Y == 0)
                    {
                        if (chonXY == "1")
                        {
                            if (mega == true)
                                await MegaIo.UploadFile(_webHostEnvironment.WebRootPath, formFile);

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
                                //await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);

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
                                mail.ToEmail = email;

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
                                await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
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
                                    mail.ToEmail = email;

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + " - with MegaIo] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUpload[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
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
                                    mail.ToEmail = email;

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + " - with MegaIo] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUploadX[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
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
                                //await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);

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
                                mail.ToEmail = email;

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
                                    await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
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
                                    mail.ToEmail = email;

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + "] Send file or message from " + name + "("+say+" files uploaded)";
                                    mail.Attachments.Add(fileUpload[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
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
                                    mail.ToEmail = email;

                                    mail.Attachments = new List<IFormFile>();
                                    mail.Subject = host + " [PART " + (i + 1) + "] Send file or message from " + name + "(" + say + " files uploaded)";
                                    mail.Attachments.Add(fileUploadX[i]);
                                    if (gmail == true)
                                        await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
                                }
                            }
                        }
                        else
                        {
                            if (mega == true)
                            {
                                await MegaIo.UploadFile(_webHostEnvironment.WebRootPath, fileUpload);
                                await MegaIo.UploadFile(_webHostEnvironment.WebRootPath, fileUploadX);
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
                            mail.ToEmail = email;
                            if (gmail == true)
                                await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                if (mega == true)
                {
                    await MegaIo.UploadFile(_webHostEnvironment.WebRootPath, fileUpload);
                    await MegaIo.UploadFile(_webHostEnvironment.WebRootPath, fileUploadX);
                }
                MailRequest mail = new MailRequest();
                    var req = Request.Path;

                    if (req == "/" || string.IsNullOrEmpty(req))
                        req = "/Home/Index";
                    mail.Subject = host + " Send file or message from " + "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace+"]" + " - File Upload In MegaIO:"+mega+" (" + say + " files uploaded)";
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
                mail.ToEmail = email;
                if (gmail == true)
                    await _mailService.SendEmailAsync(mail, _webHostEnvironment.WebRootPath);
                return RedirectToAction("Index");
            }
            finally
            {
                if (ViewBag.Y == 1)
                {
                        var infoX = listSetting[52].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                        if (flag == 0 && homePass != passAd && tong > int.Parse(infoX[3]))
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
                                var files = infoFile.Replace("\r","").Split("\n");

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

                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 2)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    ViewBag.KetQua = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn thực hiện lại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (ViewBag.X == 1 && flag == 3)
                {
                    ViewData["Loi"] = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    ViewBag.KetQua = "Một trong những file bạn sắp tải - tên file bạn sắp upload (tên mới bạn tự đặt) đã tồn tại hoặc bị trùng!\r\nTất cả các file đã bị lỗi khi đăng tải, mời bạn kiểm tra và thực hiện lại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 4 && flax == 0)
                {
                    ViewData["LoiX"] = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                    ViewBag.KetQua = "Lỗi hệ thống - theo yêu cầu của bạn. Tên path thư mục đã tồn tại ...";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 5 && homePass != passAd)
                {
                    ViewData["LoiY"] = "Vui lòng chọn ngày hết hạn các file này sau ngày hôm nay và thời hạn các file của bạn được phép tồn tại trên Server hệ thống là 7 ngày!";
                    ViewBag.KetQua = "Vui lòng chọn ngày hết hạn các file này sau ngày hôm nay và thời hạn các file của bạn được phép tồn tại trên Server hệ thống là 7 ngày!";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
                else if (flag == 6 && homePass != passAd)
                {
                    var infoX = listSetting[52].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                        var max = int.Parse(infoX[3]);
                    ViewData["Loi"] = "⚠️ Hiện tại mỗi lượt bạn chỉ có thể tải lên hệ thống các file tổng kích thước tối đa không quá "+max+" KB.";
                    ViewBag.KetQua = "⚠️ Hiện tại mỗi lượt bạn chỉ có thể tải lên hệ thống các file tổng kích thước tối đa không quá "+max+" KB.";
                    HttpContext.Session.SetString("data-result", "true"); return this.UploadFile("",ViewBag.SL, ViewBag.X, ViewBag.Y);
                }
            }

            //SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", name, name, "teinnkatajeqerfl");
            
                var xemPass = homePass ==  passAd ? " - OFF SAVE ADMIN IS CORRECT" : "";

                ViewBag.KetQua = ViewBag.Y == 0 ? "[NO UPLOAD] - Thành công (xử lý admin) !" : "[YES UPLOAD"+xemPass+"]" + " - Thành công! Tất cả các file đã được đăng tải lên Server hệ thống ...";

                if (External == "false")
                {
                    if (TempData["clear_uploadfile"] == "true" && TempData["ClearWebsite"] == "true" || txtExternal == "true")
                    { var back = f["txtReturn"].ToString(); if (string.IsNullOrEmpty(back) == false) return Redirect(back+ ""); return Redirect("https://google.com"); }
                    return View("UploadFile", new { sl = ViewBag.SL, name = ViewBag.X, upload = ViewBag.Y });
                }
                else
                {
                    var back = f["txtReturn"].ToString(); if (string.IsNullOrEmpty(back) == false) return Redirect(back);

                    if (back == ".")
                    return Redirect("https://google.com");
                    return Ok(new { success = "Đã xử lý thành công !" });
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                if (External == "false")
                return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }

        public ActionResult FindSubFolders (string folder, string password)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
            catch(Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult AllDownload(string password)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult DownloadFile(int? sl, string? all = "0", string? folder = "")
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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

                    var xa = "";

                    var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                    var nd = System.IO.File.ReadAllText(pth);
                    var onoff = nd.Replace("\r","").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                    var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"));

                    if (onoff == "file_TAT")
                        xa = "#fileclose";
                    else
                     if (onoff == "file_MO")
                        xa = "file";

                    int k = 0;
                ListFileDirectory(xa, ref k);
                ViewBag.XL = k;
                ViewBag.KQF = "* Download list all file Server (toàn bộ - hiện tại có : "+k+" file)";
                HttpContext.Session.Remove("LoginAdmin");
            }

                TempData["All"] = ViewBag.All;
            TempData["Folder"] = ViewBag.Folder;

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
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
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
           
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
                    var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                    var nd = System.IO.File.ReadAllText(pth);
                    var onoff = nd.Replace("\r","").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                    var xa = "";

                    if (onoff == "file_TAT")
                        xa = "#fileclose";
                    else
                     if (onoff == "file_MO")
                        xa = "file";

                    HttpContext.Session.Remove("LoginAdmin");
                DirectoryInfo fx = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, xa));
                fx.Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, xa)).Create();

                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"), "");
                TempData["LoginAdmin"] = "Admin delete all file in Server successful!";
            }
            return RedirectToAction(url);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult XoaFolder(string folder, string chon)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult XoaFile(string file)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file"+file));
            if (f.Exists)
            f.Delete();

            ViewBag.All = TempData["All"];
            ViewBag.Folder = TempData["Folder"];
            TempData["Folder"] = ViewBag.Folder;
            TempData["All"] = ViewBag.All;

            return RedirectToAction("DownloadFile",new { folder = ViewBag.Folder + "<split>"+ViewBag.All });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult XoaFileX(string file)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult MatDoTuyetDoi(string? mdtd)
        {
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath,"Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[31].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    return RedirectToAction("Error");

                if (mdtd == TempData["MDTT"].ToString())
            {
                HttpContext.Session.SetString("TuyetDoi", "true");
            }
            else
            {
                HttpContext.Session.SetString("TuyetDoi", "false");
                TempData.Remove("UyTin");
                HttpContext.Session.Remove("userIP");
            }
            return Redirect(TempData["urlCurrent"].ToString());
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }
    }
}
