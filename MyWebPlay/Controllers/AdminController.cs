using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;

namespace MyWebPlay.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult SettingXYZ()
        {
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            SettingAdmin settingAdmin = new SettingAdmin();
            settingAdmin.Topics = new List<SettingAdmin.Topic>();
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                settingAdmin.Topics.Add(new SettingAdmin.Topic(info[0], info[2], bool.Parse(info[1])));

                if (info[0] == "Password_Admin")
                    ViewBag.Password = info[3];
            }

            return View(settingAdmin);
        }

        [HttpPost]
        public ActionResult SettingXYZ(IFormCollection f)
        {
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
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

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
       
                        var xi = "false";
                        if (f[info[0]] == "on")
                        {
                            xi = "true";
                        }

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note"))
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


                if (info[0] != "Password_Admin")
                    noidung = noidung.Replace(info[0] + "<3275>" + info[1], info[0] + "<3275>" + xi);
                else
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "mywebplay-ADMIN";

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                 }
            }
            System.IO.File.WriteAllText(path, noidung);
            return RedirectToAction("SettingXYZ");
        }

        //-------------------------------------------------

        private String docfile(String filename)
        {
            string[] a = System.IO.File.ReadAllLines(filename);
            String s = "";
            for (int i = 0; i < a.Length; ++i)
            {
                s = s + a[i];
                if (i < a.Length - 1)
                    s = s + "\n";
            }
            return s;
        }

        public void khoawebsiteClient(List<string> listIP)
        {
            TempData["current"] = HttpContext.Request.GetDisplayUrl();
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                TempData["nav_link"] = "text-dark"; TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                TempData["nav_link"] = "text-light"; TempData["winx"] = "❤";
            }

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
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
                }

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note"))
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

        public ActionResult TracNghiemOnline_ViewMark()
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.Text1 = System.IO.File.ReadAllText(path);
                ViewBag.Text2 = "<p id=\"preX\" style=\"color:" + TempData["mau_text"] + ";font-size:22px; display:none\">" + ViewBag.Text1.Replace("\n", "<br>") + "</p>";
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                ViewBag.DateTime = x.AddHours(file.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return View();
        }

        public ActionResult EditStudentMark()
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");
            if (System.IO.File.Exists(path))
            {
                ViewBag.Text = System.IO.File.ReadAllText(path);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditStudentMark(string? txtText)
        {
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                TempData["nav_link"] = "text-dark"; TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                TempData["nav_link"] = "text-light"; TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note"))
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (txtText != null)
                System.IO.File.WriteAllText(path, txtText);

            return RedirectToAction("TracNghiemOnline_ViewMark");
        }

        public ActionResult XoaViewMark()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            if (System.IO.File.Exists(path))
            {
                var noidung = "Thời gian bắt đầu\tIP Address\tMSSV\tID Link\tThời gian kết thúc\tĐiểm\r\n";
                System.IO.File.WriteAllText(path, noidung);
            }
            return RedirectToAction("TracNghiemOnline_ViewMark");
        }
    }
}
