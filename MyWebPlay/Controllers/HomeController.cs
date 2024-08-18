using AppFindMainKey_CSDL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using MyWebPlay.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using MD5 = MyWebPlay.Extension.MD5;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IMailService mailService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _mailService = mailService;
        }

        private void XoaDirectoryNull(string path)
        {
            var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetFiles();
            var folders = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();

            var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();
            foreach (var item in listFolder)
            {
                XoaDirectoryNull(path + "/" + item.Name);
            }

            if (listFile.Length == 0 && folders.Length == 0 && path != "file" && path != "#fileclose")
            {
                System.IO.Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path), true);
            }
        }

        public int SoSanh2Ngay(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            if (d1 == d2 && m1 == m2 && y1 == y2)
                return 0;

            if (y1 < y2)
                return -1;

            if (y1 == y2)
            {
                if (m1 == m2)
                {
                    if (d1 < d2)
                        return -1;
                }
                else
                if (m1 < m2)
                    return -1;
            }
            return 1;
        }

        public ActionResult Error(string? onoff, string? encrypt)
        {
            try
            {
                if (onoff != null)
                {
                    var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                    var nd = System.IO.File.ReadAllText(pth);

                    if (onoff == "on")
                        nd = nd.Replace("ADMINSETTING_OFF", "ADMINSETTING_ON");
                    else
                    if (onoff == "off")
                        nd = nd.Replace("ADMINSETTING_ON", "ADMINSETTING_OFF");

                    System.IO.File.WriteAllText(pth, nd);
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var onEncryptSetting = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[9];
                if (onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_ON")
                {
                    if (noidung.Contains("[ENCRYPT]") == false || encrypt == "true")
                    {
                        noidung = StringMaHoaExtension.Encrypt(noidung, "32752262");
                        System.IO.File.WriteAllText(path, "[ENCRYPT]" + noidung);
                    }
                    TempData["HTML-visible"] = "0";
                    return View();
                }
                else if (onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_OFF" || encrypt == "false")
                {
                    if (noidung.Contains("[ENCRYPT]"))
                    {
                        noidung = noidung.Replace("[ENCRYPT]", "");
                        noidung = StringMaHoaExtension.Decrypt(noidung, "32752262");
                        System.IO.File.WriteAllText(path, noidung);
                    }
                }

                if (((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) != null && ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) != "" && System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[15] == "APPROVE_ALL_IP_USER_REGIST_WHEN_GO_TO_PAGE_ERROR_ON")
                {
                    var pathSD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListUserIPApproveWaiting.txt");
                    var noidungSD = docfile(pathSD);

                    if (noidungSD.Contains(((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")).ToString() + "##"))
                    {
                        TempData["approve-IP"] = "true";
                        HttpContext.Session.SetString("approve-IP", "true");
                    }
                }

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXA = listSetting[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoXA[1] == "true")
                {
                    return Redirect("https://stackoverflow.com/questions/tagged/c%23");
                }

                var infoX = listSetting[30].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                {
                    TempData["chiendich"] = "true";
                }
                else
                {
                    TempData["chiendich"] = "false";
                }

                var info = listSetting[31].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[1] == "true")
                {
                    TempData["not-believe"] = "true";
                }
                else
                {
                    TempData["not-believe"] = "false";
                }

                var infoYA = listSetting[56].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoYA[0] == "HTML_Visible")
                {
                    TempData["HTML-visible"] = infoYA[3];
                }

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }

        private void ghilogrequest(IFormCollection? f)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = System.IO.File.ReadAllText(path);

            var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX1 = listSettingS[23].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);


            var infoX2 = listSettingS[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            var path0 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
            var noidung0 = docfile(path0);

            var socox = noidung0.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
            if (socox.Length >= 100)
            {
                System.IO.File.WriteAllText(path0, "");

                if (infoX1[1] == "true")
                {

                    string host = "{" + Request.Host.ToString() + "}"
                      .Replace("http://", "")
                      .Replace("https://", "")
                      .Replace("/", "");

                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", host + "[ADMIN] Báo cáo tự động danh sách các IP user đã ghé thăm và request từng chức năng của trang web (tất cả/có thể chưa đầy đủ) In " + xuxu, noidung0, "teinnkatajeqerfl");
                }
                noidung0 = "";
            }

            if (infoX2[1] == "true")
            {
                //Lưu trữ IP tất cả khách hàng từng ghé thăm trang web (dù bật hay chưa sử dụng)

                var fi = "";

                if (f != null)
                {
                    fi = "[";
                    foreach (var key in f.Keys)
                    {
                        var s = "";
                        if (f[key].ToString().Length > 100)
                            s = f[key].ToString().Substring(0, 100);
                        else
                            s = f[key].ToString();

                        fi += key + " : " + s.Replace("\r","").Replace("\n","") + "...\t---\t";
                    }
                    fi += "]";
                }

                var noidungZ = noidung0 + "\n" + DateTime.Now + "\t" + this.Request.GetDisplayUrl() + "\t" + fi;

                System.IO.File.WriteAllText(path0, noidungZ.Trim('\n'));
            }
        }

        public ActionResult KiemTraMini(string? test, string? url)
        {
            try
            {
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");
                var mini = System.IO.File.ReadAllText(pathX).Replace("\r", "").Split("\n")[1];
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                var key = listSetting[60].Split("<3275>")[3];
                if (StringMaHoaExtension.Decrypt(mini, key) == test)
                {
                    TempData["mini-web"] = "true";
                    HttpContext.Session.SetString("mini-web", "true");
                    if (HttpContext.Session.GetString("userIP") == null)
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                }
                else
                {
                    TempData["mini-web"] = "false";
                    HttpContext.Session.Remove("mini-web");
                }

                if (url == null || url == "" || url == "/")
                    url = "Index";
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }

            return RedirectToAction(url);
        }

        public bool KiemTraWebNhacNen()
        {
            if (Request.Path != "/Home/PlayKaraoke" && Request.Path != "/Home/ToPlayKaraoke")
                return true;

            if ((Request.Path == "/Home/PlayKaraoke" || Request.Path == "/Home/ToPlayKaraoke") && Request.Method != "post")
                return true;

            return false;
        }

        public void khoawebsiteClient(List<string> listIP)
        {
            try
            {
                var urlCurrent = TempData["urlCurrent"];
                TempData["urlCurrent"] = urlCurrent;

                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "");
                if (HttpContext.Session.GetString("mini-web") == "true")
                {
                    TempData["mini-web"] = "true";
                }

                //HTML notice
                var htmlNotice = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r","");
                var htmlNoticeSpan = htmlNotice.Split("\n###\n");

                var pthY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var ndY = System.IO.File.ReadAllText(pthY);
                var onoffY = ndY.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                if (onoffY == "file_TAT")
                {
                    TempData["fileSuDung"] = "false";
                }
                else
                {
                    TempData["fileSuDung"] = "true";
                }

                    if (htmlNoticeSpan.Length > 1)
                TempData["HTML-notice"] = htmlNoticeSpan[1];

                //Notice
                var notice = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[12].ToString().Replace("NOTICE : ", "").Split("<3275>");

                TempData["notice-play"] = notice[0];
                if (notice.Length > 1)
                    TempData["code-notice"] = notice[1];
                else
                    TempData["code-notice"] = "";

                TempData["not-locked-web-client-play"] = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[13] == "NOT_USE_LOCKED_CLIENT_WEB_ON") ? "true" : "false";

                TempData["opacity-body-css"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_","");

                TempData["unvisibled_sub_menu"] = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[17] == "UNVISIBLED_SUB_MENU_ON") ? "true" : "false";

                var sao = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n");
                var nhac = (sao.Length > 22 && string.IsNullOrEmpty(sao[22]) == false) ? sao[22] : "";

                TempData["nhacnendemo"] = nhac;

                // Send mail try again - karaoke with member

                string hostt = "{" + Request.Host.ToString() + " - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "}"
                  .Replace("http://", "")
                  .Replace("https://", "")
                  .Replace("/", "");

                if (HttpContext.Session.GetString("kara-demox-3275") != null)
                {
                    var adsetPath = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");

                    var adsetTitlle = System.IO.File.ReadAllText(adsetPath);

                    var karaxcu = adsetTitlle.Replace("\r", "").Split("\n")[5];

                    adsetTitlle = adsetTitlle.Replace(karaxcu, HttpContext.Session.GetString("kara-demox-3275")).Replace("\"", "");

                    System.IO.File.WriteAllText(adsetPath, adsetTitlle);

                    HttpContext.Session.Remove("kara-demox-3275");
                }

                Calendar xx = CultureInfo.InvariantCulture.Calendar;

                string xuxuu = xx.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                // Email karaoke member
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("mail-karaoke")) == false)
                {
                    var copy = HttpContext.Session.GetString("mail-karaoke");
                    var bool_copy = false;
                    try
                    {
                        var sax = StringMaHoaExtension.Decrypt(copy);
                        bool_copy = true;
                    }
                    catch
                    {
                        bool_copy = false;
                    }

                    if (bool_copy)
                    {
                        copy = StringMaHoaExtension.Decrypt(copy);
                    }

                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", hostt + " Báo cáo bản text được tạo lại (với sự phân đoạn hát của các member tham gia) Karaoke của user lúc " + xuxuu, copy, "teinnkatajeqerfl");
                    HttpContext.Session.Remove("mail-karaoke");
                }

                // Email update trắc nghiệm
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("mail_update_TN")) == false)
                {
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
                        localIP = "0.0.0.0";
                    }
                    else
                        localIP = HttpContext.Session.GetString("userIP");

                    string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxuu;

                    var copy = HttpContext.Session.GetString("mail_update_TN");
                    var bool_copy = false;
                    try
                    {
                        var sax = StringMaHoaExtension.Decrypt(copy);
                        bool_copy = true;
                    }
                    catch
                    {
                        bool_copy = false;
                    }

                    if (bool_copy)
                    {
                        copy = StringMaHoaExtension.Decrypt(copy);
                    }

                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                      "mywebplay.savefile@gmail.com", hostt + " Save Temp Create/Update Trac Nghiem File In " + name, copy, "teinnkatajeqerfl");
                    HttpContext.Session.Remove("mail_update_TN");
                }

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (lockedApp[3].Contains(xi))
                    {
                        TempData["locked-app"] = "true";
                    }
                    else
                    {
                        TempData["locked-app"] = "false";
                    }
                }

                var flag = 0;
                var flx = 0;
                var flix = 0;
                var flox = 0;
                var tintuong = "";

                var yes = false;

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (i != 33 && yes == false && info.Length == 3 && i < 45)
                    {
                        if (info[1] == "true")
                            yes = true;
                    }

                    if (info[0] == "Color_BackgroundAndText")
                    {
                        if (info[3] == "[NULL]")
                        {
                            TempData["demo-mau"] = "false";
                        }
                        else
                        {
                            TempData["demo-mau"] = "true";
                            TempData["demo-bg"] = info[3].Split("<>", StringSplitOptions.RemoveEmptyEntries)[0];
                            TempData["demo-text"] = info[3].Split("<>", StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                    }

                    if (info[0] == "Color_TracNghiem")
                    {
                        TempData["mau-TN"] = info[3];
                    }

                    if (info[0] == "HTML_Visible")
                    {
                        TempData["HTML-visible"] = info[3] + "";
                    }

                    if (info[0] == "Time_Waiting")
                    {
                        TempData["Time_Waiting"] = info[3];
                    }

                    if (info[0] == "Get_Blocked" && Request.Method == "GET")
                    {
                        if (info[1] == "true")
                        {
                            TempData["errorXY"] = "true";
                        }
                    }

                    if (info[0] == "OffWebsite_All")
                    {
                        if (info[1] == "true")
                        {
                            TempData["tathoatdong"] = "true";
                        }
                        else
                        {
                            TempData["tathoatdong"] = "false";
                        }

                        if (HttpContext.Session.GetString("TuyetDoi") == "true")
                            TempData["tathoatdong"] = "false";
                    }

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            TempData["tathetweb"] = "true";
                        }
                        else
                        {
                            TempData["tathetweb"] = "false";
                        }
                    }

                    if (info[0] == "IPUser_UnVisible")
                    {
                        if (info[1] == "true")
                        {
                            TempData["IPUser_UnVisible"] = "true";
                        }
                        else
                        {
                            TempData["IPUser_UnVisible"] = "false";
                        }
                    }

                    if (info[0] == "Karaoke_RandomImage")
                    {
                        if (info[1] == "true")
                        {
                            TempData["Karaoke_RandomImage"] = "true";
                        }
                        else
                        {
                            TempData["Karaoke_RandomImage"] = "false";
                        }
                    }

                    if (info[0] == "TangHinh_UseUploadFile")
                    {
                        if (info[1] == "true")
                        {
                            TempData["clear_uploadfile"] = "true";
                        }
                        else
                        {
                            TempData["clear_uploadfile"] = "false";
                        }
                    }

                    if (info[0] == "NotUse_Believe")
                    {
                        if (info[1] == "true")
                        {
                            TempData["not-believe"] = "true";
                        }
                        else
                        {
                            TempData["not-believe"] = "false";
                        }
                    }

                    if (info[0] == "Block_RegistUsing")
                    {
                        if (info[1] == "true")
                        {
                            TempData["BlockRegistUsing"] = "true";
                        }
                        else
                        {
                            TempData["BlockRegistUsing"] = "false";
                        }
                    }

                    if (info[0] == "FullScreen_Web")
                    {
                        if (info[1] == "true")
                        {
                            TempData["toanmanhinh"] = "true";
                        }
                        else
                        {
                            TempData["toanmanhinh"] = "false";
                        }
                    }

                    if (info[0] == "Using_Website")
                    {
                        if (info[1] == "false")
                        {
                            TempData["UsingWebsite"] = "false";
                        }
                        else
                        {
                            TempData["UsingWebsite"] = "true";
                        }

                        if (HttpContext.Session.GetString("trust-X-you") == "true" || HttpContext.Session.GetString("TuyetDoi") == "true")
                        {
                            TempData["UsingWebsite"] = "true";
                        }
                    }

                    if (info[0] == "Believe_IP")
                    {
                        tintuong = info[3];
                    }

                    if (info[0] == "MatDoTuyetDoi")
                    {
                        var please = info[3].Split("<>");
                        TempData["keytuyetdoi"] = please[0];
                        TempData["MDTT"] = please[1];
                    }

                    if (info[0] == "Clear_Website")
                    {
                        if (info[1] == "false")
                        {
                            TempData["ClearWebsite"] = "false";
                        }
                        else
                        {
                            flix = 1;
                            TempData["ClearWebsite"] = "true";
                        }
                    }

                    if (info[0] == "Using_QuickData")
                    {
                        if (info[1] == "true")
                        {
                            flx = 1;
                        }
                    }

                    if (flox == 0)
                        TempData["NotAlertQuickData"] = "false";

                    if (flix == 1 && flx == 1 && info[0] == "NotAlert_QuickData")
                    {
                        if (info[1] == "false")
                        {
                            TempData["NotAlertQuickData"] = "false";
                        }
                        else
                        {
                            TempData["NotAlertQuickData"] = "true";
                            flox = 1;
                        }
                    }

                    if (flox == 1)
                    {
                        if (info[0] == "Auto_Excute")
                        {
                            if (info[1] == "true")
                            {
                                TempData["AutoExcute"] = "true";
                            }
                        }
                    }
                    else
                    {
                        TempData["AutoExcute"] = "false";
                    }

                    if (flox == 1 && HttpContext.Session.GetString("ok-data") == "true")
                    {
                        TempData["BabyData"] = "true";
                        /*HttpContext.Session.Remove("ok-data");*/
                        TempData["dataPost"] = "[POST]";
                        HttpContext.Session.SetString("data-result", "true");
                    }
                    else
                    {
                        TempData["BabyData"] = "false";
                    }

                    if (info[0] == "Alert_UsingWebsite")
                    {
                        if (info[1] == "false")
                        {
                            TempData["AlertUsingWebsite"] = "false";
                        }
                        else
                        {
                            TempData["AlertUsingWebsite"] = "true";
                        }

                        if (HttpContext.Session.GetString("trust-X-you") == "true")
                        {
                            TempData["AlertUsingWebsite"] = "true";
                        }
                    }

                    if (info[0] == "ViewSite_Pattern")
                    {
                        if (info[1] == "false")
                        {
                            TempData["AdminControl"] = "false";
                        }
                        else
                        {
                            TempData["AdminControl"] = "true";
                        }
                    }

                    //if (info[0] == "Website_Admin")
                    //{
                    //    if (info[1] == "false")
                    //    {
                    //        TempData["WebsitePrivate"] = "false";
                    //    }
                    //    else
                    //    {
                    //        TempData["WebsitePrivate"] = "true";
                    //    }
                    //}

                    if (info[0] == "ViewSite_Basic")
                    {
                        if (info[1] == "false")
                        {
                            TempData["ViewSiteBasic"] = "false";
                        }
                        else
                        {
                            TempData["ViewSiteBasic"] = "true";
                        }
                    }

                    if (info[0] == "Post_Clipboard")
                    {
                        if (info[1] == "false")
                        {
                            TempData["PostResult"] = "false";
                        }
                        else
                        {
                            TempData["PostResult"] = "true";
                        }
                    }

                    if (info[0] == "Connect_LinkDown")
                    {
                        if (info[1] == "true")
                        {
                            TempData["ConnectLinkDown"] = "true";
                        }
                        else
                        {
                            TempData["ConnectLinkDown"] = "false";
                        }
                    }

                    if (info[0] == "Save_ComeHere")
                    {
                        if (info[1] == "false")
                        {
                            TempData["SaveComeHere"] = "false";
                        }
                        else
                        {
                            TempData["SaveComeHere"] = "true";
                        }
                    }

                    if (info[0] == "All_People")
                    {
                        if (info[1] == "false")
                            TempData["AllConnect"] = "false";
                        else
                            TempData["AllConnect"] = "true";
                    }

                    if (info[0] == "TabTittle_NoiDung")
                    {
                        TempData["TabTittleView"] = info[3];
                    }

                    if (info[0] == "Off_CallIP")
                    {
                        if (info[1] == "false")
                        {
                            //    if (HttpContext.Session.GetString("userIP") == "0.0.0.0")
                            //    {
                            //        HttpContext.Session.Remove("userIP");
                            //        TempData["skipIP"] = "false";
                            //        TempData["GetDataIP"] = "true";
                            //    }
                            //    TempData["skipOK"] = "false";
                        }
                        else
                        {
                            HttpContext.Session.Remove("userIP");
                            HttpContext.Session.SetString("userIP", "0.0.0.0");
                            TempData["skipIP"] = "true";
                            TempData["skipOK"] = "true";
                            TempData["GetDataIP"] = "false";
                        }
                    }

                    if (info[0] == "Mail_ReportUrl")
                    {
                        if (info[1] == "false")
                        {
                            TempData["MailReportUrl"] = "false";
                        }
                        else
                        {
                            TempData["MailReportUrl"] = "true";
                        }
                    }

                    if (info[0] == "Admin_MiniWeb")
                    {
                        if (info[1] == "true")
                        {
                            TempData["errorXY"] = "true";
                        }
                        else
                        {
                            if (TempData["errorXY"] != "true")
                                TempData["errorXY"] = "false";
                        }
                    }

                    if (info[0] == "Off_RandomTab")
                    {
                        if (info[1] == "false")
                        {
                            var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Image.txt");
                            var hinh = System.IO.File.ReadAllText(pathX1).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin/RandomTab/RandomTab_Tittle.txt");
                            var tittle = System.IO.File.ReadAllText(pathX2).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                            var r = new Random();
                            var ix = r.Next(0, hinh.Length);
                            var iy = r.Next(0, tittle.Length);

                            TempData["OffRandomTab"] = "false";
                            TempData["Tab_Image"] = hinh[ix];
                            TempData["Tab_Tittle"] = tittle[iy];
                        }
                        else
                        {
                            TempData["OffRandomTab"] = "true";
                        }
                    }

                    if (flag == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flag = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flag = 0;
                        }
                    }
                }

                if (TempData["errorXY"] != "true")
                {
                    if (yes == false)
                        TempData["errorXY"] = "true";
                    else
                        TempData["errorXY"] = "false";
                }

                if (HttpContext.Session.GetString("mini-error") == "true")
                {
                    TempData["errorXY"] = "false";
                    HttpContext.Session.SetString("mini-error", "true");
                }

                // cho phép

                var chophep = listSetting[50].Split("<3275>");
                if (chophep[3] != "[NULL]")
                {
                    var xi = Request.Path;
                    if (xi == "" || xi == "/" || xi == null) xi = "/Home/Index";
                    if (chophep[3].Contains(xi))
                    {
                        TempData["tathoatdong"] = "false";
                        TempData["UsingWebsite"] = "true";
                        TempData["AlertUsingWebsite"] = "true";
                        //TempData["AdminControl"] = "true";
                        TempData["errorXY"] = "false";
                    }
                }

                //var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                //var nd = System.IO.File.ReadAllText(pth).Replace("\r","").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                //TempData["key-admin"] = nd[0];
                //TempData["value-admin"] = nd[1];

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                var xuxu = x.AddHours(DateTime.UtcNow, 7);

                if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                {
                    TempData["mau_background"] = "white";
                    TempData["mau_text"] = "black";
                    TempData["mau_nen"] = "dodgerblue";
                    TempData["winx"] = "❤";
                    TempData["TMH"] = "🌞";
                }
                else
                {
                    TempData["mau_background"] = "black";
                    TempData["mau_text"] = "white";
                    TempData["mau_nen"] = "rebeccapurple";
                    TempData["winx"] = "❤";
                    TempData["TMH"] = "🌛";
                }

                if (listIP == null)
                {
                    return;
                    //if (HttpContext.Session.GetString("TuyetDoi") == "true")
                    // {
                    //     listIP = new List<string>();
                    //     listIP.Add("0.0.0.0");
                    // }
                    // else
                    // {
                    //     return;
                    // }
                }

                TempData["userIP"]= listIP[0];
                TempData["fileResult"] = null;

                if (HttpContext.Session.GetString("userIP") == "0.0.0.0")
                    TempData["belix"] = "true";
                else
                    TempData["belix"] = "false";

                if (HttpContext.Session.GetString("data-result") != null && HttpContext.Session.GetString("data-result") == "true")
                {
                    TempData["data-result"] = "true";
                    HttpContext.Session.Remove("data-result");
                }
                else
                {
                    TempData["data-result"] = "false";
                }

                if (HttpContext.Session.GetString("alert-trust") != null)
                {
                    TempData["alert-trust"] = "true";
                }
                else
                {
                    TempData["alert-trust"] = "false";
                }

                HttpContext.Session.Remove("alert-trust");

                if (HttpContext.Session.GetString("trust-ok") != null && HttpContext.Session.GetString("trust-ok") == "true")
                {
                    TempData["ok-tin-IP"] = "true";
                    HttpContext.Session.Remove("trust-ok");
                }
                else
                {
                    TempData.Remove("ok-tin-IP");
                    //HttpContext.Session.Remove("trust-X-you");
                }

                TempData["current"] = HttpContext.Request.GetDisplayUrl();

                TempData["directURL"] = "false";

                if (tintuong.Contains("," + listIP[0] + ",") == false)
                    TempData["trust-about"] = "false";
                else
                    TempData.Remove("trust-about");

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    listIP.Add(endPoint.Address.ToString());
                }

                listIP.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString());

                var path0 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidung0 = docfile(path0);

                var socox = noidung0.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                if (socox.Length >= 100)
                {
                    System.IO.File.WriteAllText(path0, "");

                    if (TempData["MailReportUrl"] == "true")
                    {

                        string host = "{" + Request.Host.ToString() + "}"
                          .Replace("http://", "")
                          .Replace("https://", "")
                          .Replace("/", "");

                        SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com",
                          "mywebplay.savefile@gmail.com", host + "[ADMIN] Báo cáo tự động danh sách các IP user đã ghé thăm và request từng chức năng của trang web (tất cả/có thể chưa đầy đủ) In " + xuxu, noidung0, "teinnkatajeqerfl");
                    }
                    noidung0 = "";
                }

                if (TempData["SaveComeHere"] == "true" && HttpContext.Session.GetString("trust-X-you") == null)
                {
                    //Lưu trữ IP tất cả khách hàng từng ghé thăm trang web (dù bật hay chưa sử dụng)

                    if (TempData.ContainsKey("dataPost") == false || TempData["dataPost"] == null || TempData["dataPost"] == "")
                        TempData["dataPost"] = "[GET]";

                    var sa = TempData["dataPost"].ToString().TrimStart('[').TrimEnd(']');

                    if (string.IsNullOrEmpty(sa) || string.IsNullOrWhiteSpace(sa))
                        TempData["dataPost"] = "[POST - EMPTY OR NULL]";
                    else
                    if (sa.Length > 200)
                        TempData["dataPost"] = "[POST - " + sa.Substring(0, 200) + "...]";

                    var noidungZ = noidung0 + "\n" + listIP[0] + "\t" + DateTime.Now + "\t" + TempData["current"] + "\t" + TempData["dataPost"];
                    TempData.Remove("dataPost");

                    System.IO.File.WriteAllText(path0, noidungZ.Trim('\n'));
                }

                TempData["IP_Client"] = listIP[0];

                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPLock.txt");
                var noidung2 = docfile(path2);

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListIPOnWebPlay.txt");
                var noidung = docfile(path);

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/LockedIPClient.txt");
                var noidung1 = docfile(path1);

                if (noidung1.Contains(listIP[0]))
                {
                    TempData["lockedClient"] = "true";
                }
                else
                {
                    TempData["lockedClient"] = "false";
                }

                for (int i = 0; i < listIP.Count; i++)
                {
                    if (noidung2.Contains(listIP[i]))
                    {
                        TempData["lock"] = "true";
                        TempData["data-result"] = "true";
                        break;
                    }
                    else
                    {
                        TempData["lock"] = "false";
                    }
                }

                if (noidung.Contains(listIP[0]) == false)
                {
                    TempData["PlayOnWebInLocal-X"] = "false";
                    TempData["InError"] = "true";
                }
                else
                {
                    TempData["VisibleX"] = "true";
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                //return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult QuyTacKiTuJapan()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://gogle.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";

                    return RedirectToAction("Index");
                }

                khoawebsiteClient(listIP);
                TempData["rule-JP"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin","ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt")).Replace("\n", "<br>").Replace("\t", " ➡ ");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        public ActionResult Index(string? mini)
        {
            try
            {
                var pay = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungPay = System.IO.File.ReadAllText(pay);

                var onEncryptSetting = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[9];
                if ((onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_OFF" && noidungPay.Contains("[ENCRYPT]")) || onEncryptSetting == "ENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_ON")
                {
                    return RedirectToAction("Error");
                }

                if (string.IsNullOrEmpty(mini) == false && mini == "true" || HttpContext.Session.GetString("mini-error") == "true")
                {
                    TempData["errorXY"] = "false";
                    HttpContext.Session.SetString("mini-error", "true");
                }
                //BanNhap();
                // Change JAPAN (question)
                var jp = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin","ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt"));
                if (string.IsNullOrEmpty(jp))
                {
                    var rs = chuyendoiJapan();
                    System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin","ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt"), rs);
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Delete(true);

                var pthY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var ndY = System.IO.File.ReadAllText(pthY);
                var onoffY = ndY.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Create();

                if (onoffY == "file_MO")
                {
                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == false)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();

                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == true)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Delete(true);
                }
                else if (onoffY == "file_TAT")
                {
                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == false)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Create();

                    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == true)
                        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Delete(true);
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Delete(true);

                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt"), "");

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Create();

                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    f.Delete();
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Delete(true);

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Create();

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth);
                var onoff = nd.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

                var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"));

                var xa = "";

                if (onoff == "file_TAT")
                {
                    infoFile = infoFile.Replace("file/", "#fileclose/");
                    xa = "#fileclose";
                }
                else
                if (onoff == "file_MO")
                {
                    infoFile = infoFile.Replace("#fileclose/", "file/");
                    xa = "file";
                }

                var files = infoFile.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                for (int xx = 0; xx < files.Length; xx++)
                {
                    if (files[xx] == "") continue;

                    var fi = files[xx].Split("\t");

                    var today = xuxu.Split("/");
                    var hethan = fi[1].Split("/");

                    var d1 = int.Parse(today[0]);
                    var m1 = int.Parse(today[1]);
                    var y1 = int.Parse(today[2]);

                    var d2 = int.Parse(hethan[0]);
                    var m2 = int.Parse(hethan[1]);
                    var y2 = int.Parse(hethan[2]);

                    if (SoSanh2Ngay(d1, m1, y1, d2, m2, y2) >= 0 || new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0])).Exists == false)
                    {
                        FileInfo fx = new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0].TrimStart('/')));
                        fx.Delete();
                        infoFile = infoFile.Replace(fi[0] + "\t" + fi[1] + "\n", "");
                    }

                }
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"), infoFile);
                try
                {
                    XoaDirectoryNull(xa);
                }
                catch (Exception ex)
                {
                    ViewBag.Loi = "";
                    return View();
                }

                //-------------------------------------------------------------INDEX---

                //TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                }
                else
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
                    HttpContext.Session.Remove("TracNghiem");

                    if (HttpContext.Session.GetString("approve-IP") == "true")
                    {
                        var pathSD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "ClientConnect/ListUserIPApproveWaiting.txt");
                        var noidungSD = docfile(pathSD);

                        noidungSD = noidungSD.Replace(((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")).ToString() + "##", "");

                        System.IO.File.WriteAllText(pathSD, noidungSD);
                        HttpContext.Session.Remove("approve-IP");
                    }
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }

            HttpContext.Session.Remove("hanhdong_3275");
            return View();
        }

        [HttpGet]
        public ActionResult CheckText()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
                    TempData["skipIP"] = "true";
                }
                var listIP = new List<string>();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                    listIP.Add(HttpContext.Session.GetString("userIP"));
                else
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                khoawebsiteClient(listIP);
                TempData["checkText"] = "Hạnh phúc là gì?\r\n\r\n             là những điều TUYỆT\t\t vỜI NHẤT của chúng ta, NHƯng ta Lại qUÊn mất rằng bạn xứng đáng:được HẠNH PHÚC.cuộc sống là những : \"Niềm vui\"...\r\n\r\n\r\n\r\n\r\n    bên cạnh những người thân YÊU,CÙNG nhau cố gắng phát triển bản thân. hơn nữa, chúng ta hãy cười mỗi ngày.\r\n    hãy cười lên nhé!rồi một ngày nào đó,Nỗ lực của bạn sẽ được đền đáp XỨng đÁNg.";
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        private int kiemtrakitu_codau(String x)
        {
            if (x == "à" || x == "á" || x == "ả" || x == "ã" || x == "ạ")
                return 1;
            if (x == "ă" || x == "ằ" || x == "ắ" || x == "ẳ" || x == "ẵ" || x == "ặ")
                return 1;
            if (x == "â" || x == "ầ" || x == "ấ" || x == "ẩ" || x == "ẫ" || x == "ậ")
                return 1;
            if (x == "đ")
                return 1;
            if (x == "é" || x == "è" || x == "ẻ" || x == "ẽ" || x == "ẹ")
                return 1;
            if (x == "ê" || x == "ề" || x == "ế" || x == "ể" || x == "ễ" || x == "ệ")
                return 1;
            if (x == "í" || x == "ì" || x == "ỉ" || x == "ĩ" || x == "ị")
                return 1;
            if (x == "ó" || x == "ò" || x == "ỏ" || x == "õ" || x == "ọ")
                return 1;
            if (x == "ô" || x == "ồ" || x == "ố" || x == "ổ" || x == "ỗ" || x == "ộ")
                return 1;
            if (x == "ơ" || x == "ờ" || x == "ớ" || x == "ở" || x == "ỡ" || x == "ợ")
                return 1;
            if (x == "ù" || x == "ú" || x == "ủ" || x == "ũ" || x == "ụ")
                return 1;
            if (x == "ư" || x == "ừ" || x == "ứ" || x == "ử" || x == "ữ" || x == "ự")
                return 1;
            if (x == "ý" || x == "ỳ" || x == "ỷ" || x == "ỹ" || x == "ỵ")
                return 1;

            return 0;
        }

        private String chuyendoi_2(String input)
        {

            input = input.Replace(" .#3275#. ", "");
            input = input.Replace("\r\n", " .#3275#. ");
            input = input.ToLower();
            char[] s = input.ToCharArray();
            s[0] = char.ToUpper(s[0]);

            int ss = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] >= 'a' && s[i] <= 'z' || kiemtrakitu_codau(s[i].ToString()) == 1)
                {
                    ss = i;
                    break;
                }
            }

            for (int i = 1; i < ss; i++)
            {
                if (s[i] == '.' || s[i] == '?' || s[i] == '!')
                {
                    i++;
                    int j = i;
                    while (true)
                    {
                        if (s[j] >= 'a' && s[j] <= 'z' || kiemtrakitu_codau(s[j].ToString()) == 1)
                        {
                            s[j] = char.ToUpper(s[j]);
                            break;
                        }
                        j++;
                        i = j;
                    }
                }
            }
            return new String(s).Replace(" .#3275#. ", "\r\n");
        }

        private String chuyendoi_3(String input)
        {
            input = input.Trim();
            char[] s = input.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ' || s[i] == '\n')
                {
                    i++;
                    int j = i;
                    while (true)
                    {
                        if (s[j] != ' ' && s[j] != '\n')
                            break;

                        s[j] = '#';
                        j++;
                        i = j;
                    }
                }
            }
            return new String(s).Replace("#", "");
        }

        [HttpPost]
        public ActionResult CheckText(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                TempData["checkText"] = f["Chuoi"].ToString();

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = x.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                }

                if (string.IsNullOrEmpty(chuoi))
                {
                    ViewData["Loi"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.CheckText();
                }

                string s = chuoi;

                // viết hoa chữ cái đầu mỗi câu...
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);

                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var flag = 0;
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (flag == 0 && (info[0] == "Email_Upload_User" ||
                        info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                        info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                        info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                        info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                    {
                        if (info[1] == "false")
                        {

                            TempData["mau_winx"] = "red";
                            flag = 1;
                        }
                        else
                        {

                            TempData["mau_winx"] = "deeppink";
                            flag = 0;
                        }
                    }
                }

                s = chuyendoi_2(s);

                // nếu sau dấu .  ? : ! , ; không có dấu cách thì thêm sau nó

                s = s.Replace(".", ". ");
                s = s.Replace("?", "? ");
                s = s.Replace(":", ": ");
                s = s.Replace("!", "! ");
                s = s.Replace(",", ", ");
                s = s.Replace(";", "; ");

                // xoá khoảng cách dư thừa

                s = chuyendoi_3(s);

                // nếu sau { [ ( " có dấu cách thì xoá nó.

                s = s.Replace("{ ", "{");
                s = s.Replace("[ ", "[");
                s = s.Replace("( ", "(");
                s = s.Replace("\" ", "\"");

                // nếu trước } ] ) " và . , ; ? ! : có dấu cách thì xoá nó.

                s = s.Replace(" }", "}");
                s = s.Replace(" ]", "]");
                s = s.Replace(" )", ")");
                //s = s.Replace(" \"", "\"");

                s = s.Replace(" .", ".");
                s = s.Replace(" ,", ",");
                s = s.Replace(" ;", ";");
                s = s.Replace(" !", "!");
                s = s.Replace(" ?", "?");
                s = s.Replace(" :", ":");

                //...

                while (s.Contains("\r\n\r\n\r\n") == true)
                    s = s.Replace("\r\n\r\n\r\n", "\r\n\r\n");

                while (s.Contains(" \r\n") == true)
                    s = s.Replace(" \r\n", "\r\n");

                while (s.Contains("  ") == true)
                    s = s.Replace("  ", " ");

                while (s.Contains("\t\t") == true)
                    s = s.Replace("\t\t", "\t");

                //TextCopy.ClipboardService.SetText(s);

                // s = "<p style=\"color:blue\"" + s + "</p>";
                nix = s;
                s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix);
                ViewBag.Result = s;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }

           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        [HttpGet]
        public ActionResult TextToColumn1()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult TextToColumn1(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    // var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");
                int n = int.Parse(f["Number"].ToString());

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
                {
                    ViewData["Loi1"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn1();
                }

                if (string.IsNullOrEmpty(f["Number"].ToString()))
                {
                    ViewData["Loi2"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn1();
                }

                string[] s = Regex.Split(chuoi, "\r\n");

                if (s.Length % n != 0)
                {
                    ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn1();
                }

                string ss = "";
                for (int i = 0; i < s.Length; i = i + n)
                {
                    int dem = 0;
                    for (int j = i; dem < n; j++)
                    {
                        ss += s[j] + "\t";
                        dem++;
                    }
                    char[] xx = {
            '\t'
          };
                    ss = ss.TrimEnd(xx);
                    ss += "\r\n";
                }

                char[] x = {
          '\r',
          '\n'
        };
                ss = ss.TrimEnd(x);

                //TextCopy.ClipboardService.SetText(ss);

                //ss = "\r\n" + ss;
                //ss = ss.Replace("\r\n", "<br>");
                nix = ss;
                ss = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix);
                ViewBag.Result = ss;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        [HttpGet]
        public ActionResult TextToColumn2()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult TextToColumn2(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();

            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    // var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");
                int n = int.Parse(f["Number"].ToString());

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
                {
                    ViewData["Loi1"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn2();
                }

                if (string.IsNullOrEmpty(f["Number"].ToString()))
                {
                    ViewData["Loi2"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn2();
                }

                string[] s = Regex.Split(chuoi, "\r\n");

                if (s.Length % n != 0)
                {
                    ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextToColumn2();
                }

                int nn = s.Length / n;

                string ss = "";
                for (int i = 0; i < n; i++)
                {
                    int dem = 0;
                    for (int j = i; dem < nn; j = j + n)
                    {
                        ss += s[j] + "\t";
                        dem++;
                    }
                    char[] xx = {
            '\t'
          };
                    ss = ss.TrimEnd(xx);
                    ss += "\r\n";
                }

                char[] x = {
          '\r',
          '\n'
        };
                ss = ss.TrimEnd(x);

                //TextCopy.ClipboardService.SetText(ss);

                //ss = "\r\n" + ss;
                //ss = ss.Replace("\r\n", "<br>");
                nix = ss;
                ss = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix);
                ViewBag.Result = ss;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        [HttpGet]
        public ActionResult ReadNumber()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult ReadNumber(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
                {
                    ViewData["Loi"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.ReadNumber();
                }

                string[] number = Regex.Split(chuoi, "\r\n");
                string result = "";
                string re = "";
                for (int i = 0; i < number.Length; i++)
                {
                    if (long.Parse(number[i]) < 0 || number[i].Length > 12)
                    {
                        ViewBag.KetQua = "Lỗi! Các số không được âm và có hơn 12 chữ số...";
                        HttpContext.Session.SetString("data-result", "true");
                        return this.ReadNumber();
                    }

                    if (long.Parse(number[i]) == 0)
                    {
                        result += "0 : Không\r\n";
                        re += "Không\r\n";
                    }
                    else
                    {
                        result += number[i] + " : " + Models.ReadNumber.hienthicachdocso(long.Parse(number[i])) + "\r\n";
                        re += Models.ReadNumber.hienthicachdocso(long.Parse(number[i])) + "\r\n";
                    }
                }

                //TextCopy.ClipboardService.SetText(result);

                result = "\r\n" + result + "\r\n\r\n" + re;
                result = result.Replace("\r","").Replace("\n", "<br>");
                nix = result;
                result = "<p>" + result + "</p>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix.Replace("<br>", "\n"));
                ViewBag.Result = result;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }

               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        [HttpGet]
        public ActionResult TextConvertX()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult TextConvertX(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");
                string start = f["Start"].ToString();
                start = start.Replace("[T-PLAY]", "\t");
                start = start.Replace("[N-PLAY]", "\n");
                start = start.Replace("[R-PLAY]", "\r");
                string end = f["End"].ToString();
                end = end.Replace("[T-PLAY]", "\t");
                end = end.Replace("[N-PLAY]", "\n");
                end = end.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(chuoi))
                {
                    ViewData["Loi"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.TextConvertX();
                }

                String[] sx = Regex.Split(chuoi, "\r\n");
                String noidungD = "";
                for (int i = 0; i < sx.Length; i++)
                {
                    noidungD += start + sx[i] + end + "\r\n";
                }

                ////TextCopy.ClipboardService.SetText(noidung);

                //noidung = "\r\n" + noidung;
                //noidung= noidung.Replace("\r\n", "<br>").Replace(" ","&nbsp;");
                nix = noidungD;
                noidung = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + noidungD + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix);
                ViewBag.Result = noidung;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true) return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        public ActionResult CSDL_MainKey()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
            return View();
        }

        int n;
        DS[] x = new DS[100];

        BD y = new BD();

        void nhapdebai(string chuoi)
        {
            String[] s1 = chuoi.Split(',');
            for (int i = 0; i < s1.Length; i++)
            {
                String[] s2 = s1[i].Split('>');
                y.de1[i] = s2[0].Trim();
                y.de2[i] = s2[1].Trim();
            }
            n = s1.Length;
        }

        void sapxep(char[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                char x = a[i];
                int j = i - 1;
                while (j >= 0 && a[j] > x)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = x;
            }
            y.tt = new String(a);
        }

        int KT_trung(char[] a, char x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (x == a[i])
                    return 0;
            }
            return 1;
        }

        int KT_TRUNG(String a, char x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (x == a.ToCharArray()[i])
                {
                    return 0;
                }
            }
            return 1;
        }

        void thuoctinh1()
        {
            int k = 0;
            char[] x = new char[100];
            for (int i = 0; i < 100; i++)
                x[i] = ' ';

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < y.de1[i].Length; j++)
                {
                    if (KT_trung(x, y.de1[i][j]) == 1)
                    {
                        x[k] = y.de1[i][j];
                        k++;
                    }
                }
            }

            y.tt = new String(x).Trim();

            //------------------------------------------------------

            int kK = y.tt.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < y.de2[i].Length; j++)
                {
                    if (KT_trung(x, y.de2[i][j]) == 1)
                    {
                        x[kK] = y.de2[i][j];
                        kK++;
                    }
                }
            }
            y.tt = new String(x).Trim();
            sapxep(y.tt.ToCharArray());
        }

        void khoitaonguon()
        {
            int k = 0;
            char[] xx = new char[100];

            for (int i = 0; i < 100; i++)
                xx[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                int flag = 0;
                for (int j = 0; j < n; j++)
                {
                    char[] x = y.de2[j].ToCharArray();
                    if (KT_trung(x, y.tt[i]) == 0)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    xx[k] = y.tt[i];
                    k++;
                }
            }
            y.nguon = new String(xx).Trim();

        }

        void khoitaodich()
        {
            int k = 0;
            char[] xx = new char[100];

            for (int i = 0; i < 100; i++)
                xx[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                int flag = 0;
                for (int j = 0; j < n; j++)
                {
                    char[] x = y.de1[j].ToCharArray();
                    if (KT_trung(x, y.tt[i]) == 0)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    xx[k] = y.tt[i];
                    k++;
                }
            }
            y.dich = new String(xx).Trim();
        }

        void khoitaotrunggian()
        {
            int k = 0;
            char[] x = new char[100];

            for (int i = 0; i < 100; i++)
                x[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                if (KT_trung(y.nguon.ToCharArray(), y.tt[i]) == 0 ||
                  KT_trung(y.dich.ToCharArray(), y.tt[i]) == 0)
                    continue;

                x[k] = y.tt[i];
                k++;
            }
            y.trunggian = new String(x).Trim();
        }

        void nhapbangchantri(DS[] x, int[][] a)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());

            char[] ku = new char[100];

            for (int i = 0; i < 100; i++)
                ku[i] = ' ';

            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < y.trunggian.Length; j++)
                {
                    ku[j] = (char)(a[i][j] + '0');
                }
                x[i].b = new String(ku).Trim();
            }
        }

        void lapbangchantri(DS[] x, int nn)
        {
            double ss = 2 * Math.Pow(2, nn - 1);
            int s = int.Parse(ss.ToString());
            int[][] a = new int[100][];

            for (int i = 0; i < 100; i++)
                a[i] = new int[100];

            for (int i = 0; i < nn; i++)
            {
                int d = 0, k = 0;
                for (int j = 0; j < s; j++)
                {
                    double dd = Math.Pow(2, nn - 1 - i);
                    int dx = int.Parse(dd.ToString());
                    if (d == dx)
                    {
                        if (k == 0)
                            k++;
                        else
                            k--;
                        d = 0;
                    }
                    a[j][i] = k;
                    d++;
                }
            }

            nhapbangchantri(x, a);

        }

        void Play_On1(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());

            for (int i = 0; i < s; i++)
            {
                char[] xx = new char[100];

                for (int II = 0; II < 100; II++)
                    xx[II] = ' ';

                int k = 0;

                for (int j = 0; j < y.trunggian.Length; j++)
                {
                    if (x[i].b[j] == '1')
                    {
                        xx[k] = y.trunggian[j];
                        k++;
                    }
                }
                x[i].xi = new String(xx).Trim();
                // MessageBox.Show(x[i].xi+"-");
            }

        }

        void Play_On2(BD y)
        {
            double sx = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(sx.ToString());
            for (int i = 0; i < s; i++)
            {
                x[i].x1 = y.nguon + x[i].xi;
                x[i].xf = x[i].x1;
            }
        }

        void Play_On3(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);

            int s = int.Parse(ss.ToString());
            for (int a = 0; a < s; a++)
            {
                String ou = x[a].xf;
                for (int i = 0; i < n; i++)
                {
                    int d = 0;
                    for (int j = 0; j < y.de1[i].Length; j++)
                    {
                        if (KT_TRUNG(ou, y.de1[i].ToCharArray()[j]) == 0)
                            d++;
                    }
                    if (d == y.de1[i].Length)
                    {
                        for (int j = 0; j < y.de2[i].Length; j++)
                        {
                            if (KT_TRUNG(ou, y.de2[i][j]) == 1)
                            {
                                ou += y.de2[i][j];
                                i = -1;
                                break;
                            }
                        }
                    }
                    x[a].xf = ou;
                }
                //MessageBox.Show(x[a].xf);
            }
        }

        string xuatDS(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            String noidung = "";
            int s = int.Parse(ss.ToString());
            for (int i = 0; i < s; i++)
            {
                if (x[i].xi.Length == 0)
                    noidung += "\r\n" + x[i].b + "\t\tNULL\t\t" + x[i].x1 + "\t\t" + x[i].xf;
                else
                    noidung += "\r\n" + x[i].b + "\t\t" + x[i].xi + "\t\t" + x[i].x1 + "\t\t" + x[i].xf;
            }
            return noidung;
        }

        int Test(char[] x, char[] y)
        {
            int d = 0;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    if (x[i] == y[j])
                        d++;
                }
            }
            if (d == x.Length)
                return 0;
            return 1;
        }

        int TEST(int k, char[] y)
        {
            for (int i = 0; i < k; i++)
            {
                if (Test(x[i].khoa.ToCharArray(), y) == 0)
                    return 0;
            }
            return 1;
        }

        int k = 0;

        void sieukhoa(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());
            int m = y.tt.Length;
            for (int i = 0; i < s; i++)
            {
                if (x[i].xf.Length == m)
                {
                    int d = 0;
                    for (int a = 0; a < k; a++)
                    {
                        /*
                        for (int u = 0; u < x[i].x1.Length; u++)
                        {
                            //if (x[a].khoa[u] == x[i].x1[u])
                               // d++;
                        }
                        */
                    }
                    if (d != y.tt.Length)
                    {
                        if (TEST(k, x[i].x1.ToCharArray()) == 1)
                        {
                            x[k].khoa += x[k].khoa + x[i].x1;
                            k++;
                        }

                    }
                }
            }
        }

        string xuatsieukhoa(int k)
        {
            String s = "";
            for (int i = 0; i < k; i++)
                s += x[i].khoa + " , ";
            return s;
        }

        [HttpPost]
        public ActionResult CSDL_MainKey(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

                var listIP = new List<string>();
               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");

                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }

                string chuoi = f["Chuoi"].ToString();
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                        {
                            if (info[1] == "false")
                            {

                                TempData["mau_winx"] = "red";
                                flag = 1;
                            }
                            else
                            {

                                TempData["mau_winx"] = "deeppink";
                                flag = 0;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(chuoi))
                {
                    ViewData["Loi"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.CSDL_MainKey();
                }

                chuoi = chuoi.ToUpper();

                for (int i = 0; i < 100; i++)
                    x[i] = new DS();

                nhapdebai(chuoi);

                string s = "ĐỀ BÀI : " + chuoi.Replace(",", ", ");
                s = s.Replace(">", " -> ");

                thuoctinh1();
                //thuoctinh2();
                s += "\r\n\r\n- Có " + y.tt.Length + " thuộc tính tham gia : " + y.tt + "\r\n";

                khoitaonguon();
                if (y.nguon.Length == 0)
                    s += "\r\n+ Các thuộc tính nguồn : NULL\n";
                else
                    s += "\r\n+ Các thuộc tính nguồn : " + y.nguon + "\r\n";

                khoitaodich();
                if (y.dich.Length == 0)
                    s += "\r\n+ Các thuộc tính đích : NULL\r\n";
                else
                    s += "\r\n+ Các thuộc tính đích : " + y.dich + "\r\n";

                khoitaotrunggian();
                var sox = "";
                var cuctac = "";
                var chim = "";
                if (y.trunggian.Length == 0)
                {
                    s += "\r\n+ Các thuộc tính trung gian : NULL\r\n";
                    s += "\r\n==> Không có kết quả Key phù hợp hoặc có thể có một Key duy nhất là khoá từ tập nguồn : " + y.nguon + " !";

                    //TextCopy.ClipboardService.SetText(s);

                    //s = "\r\n" + s.Replace("\r\n", "<br>");
                    nix = s;

                    s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";
                    Calendar x = CultureInfo.InvariantCulture.Calendar;

                    cuctac = x.AddHours(DateTime.UtcNow, 7).ToString();
                    chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                    if (string.IsNullOrEmpty(chim)) chim = "Default";

                    sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim + "_dataresult.txt");
                    TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim + "_dataresult.txt";
                    new FileInfo(sox).Create().Dispose();
                    System.IO.File.WriteAllText(sox, nix);
                    ViewBag.Result = s;

                    ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                    if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);

                    return View();
                }
                else
                {
                    s += "\n+ Các thuộc tính trung gian : " + y.trunggian + "\n";
                }

                lapbangchantri(x, y.trunggian.Length);

                Play_On1(y);

                Play_On2(y);
                Play_On3(y);

                s += "\r\n\r\n --> BẢNG KẾT QUẢ (SO SÁNH) :\r\n\r\n";

                s += xuatDS(y);

                sieukhoa(y);
                s += "\r\n\r\n==> Kết quả các siêu khoá bé nhất là : " + xuatsieukhoa(k);

                //TextCopy.ClipboardService.SetText(s);

                nix = s;

                s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

                Calendar xis = CultureInfo.InvariantCulture.Calendar;
                chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";

                cuctac = xis.AddHours(DateTime.UtcNow, 7).ToString() + "_" + chim;

                sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                System.IO.File.WriteAllText(sox, nix);
                ViewBag.Result = s;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/POST_DataResult/" + TempData["fileResult"]);

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = "true"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
           ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true)
                    return Redirect("/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

    }
}