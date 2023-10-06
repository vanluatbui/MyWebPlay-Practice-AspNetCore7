using AppFindMainKey_CSDL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using MyWebPlay.Models;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;
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

        private void XoaDirectoryNull (string path)
        {
            var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetFiles();
            var folders = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();

            var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();
            foreach (var item in listFolder)
            {
                XoaDirectoryNull(path + "/" + item.Name);
            }

            if (listFile.Length == 0 && folders.Length == 0 && path != "file")
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

        public void khoawebsiteClient(List<string> listIP)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                listIP.Add(endPoint.Address.ToString());
            }

            listIP.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString());

                TempData["IP_Client"] = listIP[0];

                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
                var noidung2 = docfile(path2);

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
                var noidung = docfile(path);

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
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
        public ActionResult Index()
        {
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

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Delete(true);

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Delete(true);

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

                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

                var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"));

                var files = infoFile.Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
                System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "InfoWebFile", "InfoWebFile.txt"), infoFile);
                try
                {
                    XoaDirectoryNull("file");
                }
                catch (Exception ex)
                {
                    ViewBag.Loi = "";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CheckText()
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
            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.CheckText();
            }

            string s = chuoi;

            // viết hoa chữ cái đầu mỗi câu...

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

            TextCopy.ClipboardService.SetText(s);

           // s = "<p style=\"color:blue\"" + s + "</p>";

            s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

            ViewBag.Result = s;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult TextToColumn1()
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

        [HttpPost]
        public ActionResult TextToColumn1(IFormCollection f)
        {
            if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.TextToColumn1();
            }

            if (string.IsNullOrEmpty(f["Number"].ToString()))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.TextToColumn1();
            }

            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            int n = int.Parse(f["Number"].ToString());

            string[] s = Regex.Split(chuoi, "\r\n");

            if (s.Length % n != 0)
            {
                ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
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
                char[] xx = { '\t' };
                ss = ss.TrimEnd(xx);
                ss += "\r\n";
            }

            char[] x = { '\r','\n' };
            ss = ss.TrimEnd(x);

            TextCopy.ClipboardService.SetText(ss);

            //ss = "\r\n" + ss;
            //ss = ss.Replace("\r\n", "<br>");

            ss = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

            ViewBag.Result = ss;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult TextToColumn2()
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

        [HttpPost]
        public ActionResult TextToColumn2(IFormCollection f)
        {
            if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.TextToColumn2();
            }

            if (string.IsNullOrEmpty(f["Number"].ToString()))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.TextToColumn2();
            }

            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            int n = int.Parse(f["Number"].ToString());

            string[] s = Regex.Split(chuoi, "\r\n");

            if (s.Length % n != 0)
            {
                ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
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
                char[] xx = { '\t' };
                ss = ss.TrimEnd(xx);
                ss += "\r\n";
            }

            char[] x = { '\r','\n' };
            ss = ss.TrimEnd(x);

            TextCopy.ClipboardService.SetText(ss);


            //ss = "\r\n" + ss;
            //ss = ss.Replace("\r\n", "<br>");

            ss = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

            ViewBag.Result = ss;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult ReadNumber()
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

        [HttpPost]
        public ActionResult ReadNumber(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.ReadNumber();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string[] number = Regex.Split(chuoi, "\r\n");
            string result = "";
            string re = "";
            for (int i = 0; i < number.Length; i++)
            {
                if (long.Parse(number[i]) < 0 || number[i].Length > 12)
                {
                    ViewBag.KetQua = "Lỗi! Các số không được âm và có hơn 12 chữ số...";
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

            TextCopy.ClipboardService.SetText(result);

            result = "\r\n" + result+"\r\n\r\n"+re;
            result = result.Replace("\r\n", "<br>");

            result = "<p style=\"color:yellow\"" + result + "</p>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        [HttpGet]
        public ActionResult TextConvertX()
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

        [HttpPost]
        public ActionResult TextConvertX (IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            string start = f["Start"].ToString();
            start = start.Replace("[TAB-TPLAY]", "\t");
            start = start.Replace("[ENTER-NPLAY]", "\n");
            start = start.Replace("[ENTER-RPLAY]", "\r");
            string end = f["End"].ToString();
            end = end.Replace("[TAB-TPLAY]", "\t");
            end = end.Replace("[ENTER-NPLAY]", "\n");
            end = end.Replace("[ENTER-RPLAY]", "\r");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.TextConvertX();
            }


            String[] sx = Regex.Split(chuoi, "\r\n");
            String noidung = "";
            for (int i = 0; i < sx.Length; i++)
            {
                noidung += start + sx[i] + end + "\r\n";
            }

            TextCopy.ClipboardService.SetText(noidung);

            //noidung = "\r\n" + noidung;
            //noidung= noidung.Replace("\r\n", "<br>").Replace(" ","&nbsp;");

            noidung = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + noidung + "</textarea>";

            ViewBag.Result = noidung;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult CSDL_MainKey()
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
                if (KT_trung(y.nguon.ToCharArray(), y.tt[i]) == 0
                || KT_trung(y.dich.ToCharArray(), y.tt[i]) == 0)
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
        public ActionResult CSDL_MainKey (IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.CSDL_MainKey();
            }

            chuoi = chuoi.ToUpper();

            for (int i = 0; i < 100; i++)
                x[i] = new DS();

            nhapdebai(chuoi);

            string s = "ĐỀ BÀI : "+chuoi.Replace(",",", ");
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
            if (y.trunggian.Length == 0)
            {
                s += "\r\n+ Các thuộc tính trung gian : NULL\r\n";
                s += "\r\n==> Không có kết quả Key phù hợp hoặc có thể có một Key duy nhất là khoá từ tập nguồn : " + y.nguon + " !";
                
                TextCopy.ClipboardService.SetText(s);

                //s = "\r\n" + s.Replace("\r\n", "<br>");

                s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

                ViewBag.Result = s;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

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

           s  += xuatDS(y);

            sieukhoa(y);
            s += "\r\n\r\n==> Kết quả các siêu khoá bé nhất là : "+xuatsieukhoa(k);

            TextCopy.ClipboardService.SetText(s);



            s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

            ViewBag.Result = s;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

    }
}