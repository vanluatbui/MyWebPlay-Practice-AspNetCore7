using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using System.Reflection.Metadata.Ecma335;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace MyWebPlay.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void khoawebsiteAdmin()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        TempData["tathoatdong"] = "true";
                    }
                    else
                    {
                        TempData["tathoatdong"] = "false";
                    }

                }
            }
        }

                public ActionResult ComeInSetting (string id, string code)
              {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
            var nd = System.IO.File.ReadAllText(pth).Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            TempData.Remove("trust-setting");

            if (id == nd[0] && code == nd[1])
            {
                HttpContext.Session.SetString(nd[0], nd[1]);
            }
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        public ActionResult SettingXYZ_DarkAdmin()
        {
            khoawebsiteAdmin();
            HttpContext.Session.Remove("mini-web");
            TempData.Remove("mini-web");
            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPComeHere.txt");
            var noidung1 = docfile(path1);
            TempData["showIPCome"] = noidung1;

            TempData["rexJP"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt")).Replace("\t"," ➡ ");

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

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var pathS = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/Setting_Status.txt");
            var noidungS = System.IO.File.ReadAllText(pathS);
            ViewBag.SettingStatus = noidungS;

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            SettingAdmin settingAdmin = new SettingAdmin();
            settingAdmin.Topics = new List<SettingAdmin.Topic>();
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                settingAdmin.Topics.Add(new SettingAdmin.Topic(info[0], info[2], bool.Parse(info[1])));

                if (info[0] == "Password_Admin")
                    ViewBag.Password = info[3];

                if (info[0] == "Encode_Url")
                    ViewBag.EncodeUrl = info[3];

                if (info[0] == "MatDoTuyetDoi")
                    ViewBag.MaMatDo = info[3];

                if (info[0] == "Code_LockedClient")
                    ViewBag.CodeSocolar = info[3];

                if (info[0] == "Believe_IP")
                {
                    if (info[3] == "[NULL]")
                        ViewBag.Believe = "";
                    else
                        ViewBag.Believe = info[3];
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

                //if (info[0] == "Password_Setting")
                //{
                //    if (info[1] == "false")
                //    {
                //        TempData["SecureAdmin"] = "false";
                //    }
                //    else
                //    {
                //        TempData["SecureAdmin"] = "true";
                //    }
                //}

                var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                var nd = System.IO.File.ReadAllText(pth).Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                TempData["key-admin"] = nd[0];
                TempData["value-admin"] = nd[1];
                //if (HttpContext.Session.GetString(nd[0]) == nd[1])
                //{
                //    TempData["SecureAdmin"] = "false";
                //}
            }

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung2 = docfile(path2);
            TempData["ListIPActive"] = noidung2;

            var path3 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung3 = docfile(path3);
            TempData["ListIPLockAdmin"] = noidung3;

            var path4 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
            var noidung4 = docfile(path4);
            TempData["ListIPLockClient"] = noidung4;

            return View(settingAdmin);
        }

        [HttpPost]
        public ActionResult UnlockAllWeb(IFormCollection f)
        {
            khoawebsiteAdmin();

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
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

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var xinh = "false";
            if (f["LockedAll_Web"] == "on")
            {
                xinh = "true";
            }

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[28].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
            noidung = noidung.Replace(listSetting[28], infoX[0] + "<3275>" + xinh + "<3275>" + infoX[2]);
            System.IO.File.WriteAllText(path, noidung);
            
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        [HttpPost]
        public ActionResult SettingXYZ_DarkAdmin(IFormCollection f)
        {
            khoawebsiteAdmin();
            HttpContext.Session.Remove("mini-web");
            var non = TempData["SaveComeHere"];
            TempData["SaveComeHere"] = non;

            var pathXY = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SecurePasswordAdmin.txt");
            var matpassAd = System.IO.File.ReadAllText(pathXY).Split("\n")[1];

            var passUserEnter = f["txtMatPassAd"].ToString();

            if (passUserEnter != matpassAd)
            {
                TempData["loisave"] = "true";
                return RedirectToAction("SettingXYZ_DarkAdmin");
            }
            else
            {
                TempData["loisave"] = "false";
            }

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/Setting_Status.txt");

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");

            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            var cometo = "#";
            var dix = 0;

             for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                    continue;

                var baby1 = false;
                var baby2 = false;

                var xi = "false";
                        if (f[info[0]] == "on")
                        {
                            xi = "true";
                        }

                if (info[0] == "Off_CallIP" && (xi != info[1]) && xi == "true")
                {
                    baby1 = true;
                }

                if (info[0] == "All_People" && (xi != info[1]) && xi == "true")
                {
                    baby2 = true;
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

                if (info[0] != "Password_Admin" && info[0] != "Believe_IP" && info[0] != "Code_LockedClient" && info[0] != "MatDoTuyetDoi" && info[0] != "Encode_Url")
                {
                    if (xi != info[1])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(info[0] + "<3275>" + info[1], info[0] + "<3275>" + xi);

                        if (baby1 == true)
                        {
                            noidung = noidung.Replace("All_People" + "<3275>" + "true", "All_People" + "<3275>" + "false");
                        }

                        if (baby2 == true)
                        {
                            noidung = noidung.Replace("Off_CallIP" + "<3275>" + "true", "Off_CallIP" + "<3275>" + "false");
                        }
                }
                else if (info[0] == "Encode_Url")
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "/Home/Index***/Home/SecretWeb***/Admin/QuickDataInWeb";

                    if (xinh != info[3])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                }
                else if (info[0] == "Password_Admin")
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "mywebplay-ADMIN";

                    if (xinh != info[3])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                }
                else if (info[0] == "Believe_IP")
                {
                    var xinh = f[info[0]].ToString();
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "[NULL]";
                    else
                    {
                        if (xinh.StartsWith(",") == false)
                            xinh = "," + xinh;

                        if (xinh.EndsWith(",") == false)
                            xinh = xinh + ",";
                    }

                    if (xinh != info[3])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                }
                else if (info[0] == "Code_LockedClient")
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "abc-xyz";

                    if (xinh != info[3])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                }
                else if (info[0] == "MatDoTuyetDoi")
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "matdotuyetdoi<>believix-123";

                    if (xinh != info[3])
                    {
                        cometo = "#come-" + i;
                        dix++;
                    }

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                }
            }
            System.IO.File.WriteAllText(path, noidung);
            if (dix == listSetting.Length - 2)
                cometo = "#welcome";
            return Redirect("/Admin/SettingXYZ_DarkAdmin"+cometo);
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

        public ActionResult TracNghiemOnline_ViewMark()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
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

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.Text1 = System.IO.File.ReadAllText(path);
                ViewBag.Text2 = "<p id=\"preX\" name=\"colorX\" style=\"color:" + TempData["mau_text"] + ";font-size:22px; display:none\">" + ViewBag.Text1.Replace("\n", "<br>") + "</p>";
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                ViewBag.DateTime = x.AddHours(file.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return View();
        }

        public ActionResult EditStudentMark()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
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
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (string.IsNullOrEmpty(txtText) ==  false)
                System.IO.File.WriteAllText(path, txtText);
            else
            {
                var noidung = "Thời gian bắt đầu\tIP Address\tMSSV\tID Link\tThời gian kết thúc\tĐiểm\r\n";
                System.IO.File.WriteAllText(path, noidung);
            }


            return RedirectToAction("TracNghiemOnline_ViewMark");
        }

        public ActionResult XoaViewMark()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline", "DiemHocSinh.txt");

            if (System.IO.File.Exists(path))
            {
                var noidung = "Thời gian bắt đầu\tIP Address\tMSSV\tID Link\tThời gian kết thúc\tĐiểm\r\n";
                System.IO.File.WriteAllText(path, noidung);
            }
            return RedirectToAction("TracNghiemOnline_ViewMark");
        }

        public ActionResult ReportListIPComeHere() 
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPComeHere.txt");
            var noidung1 = docfile(path1);

            System.IO.File.WriteAllText(path1, "");

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            string host = "{" + Request.Host.ToString() + "}"
                .Replace("http://", "")
            .Replace("http://", "")
            .Replace("/", "");

            var non = TempData["SaveComeHere"];
            TempData["SaveComeHere"] = non;

            if (string.IsNullOrEmpty(noidung1) == false)
            SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
               "mywebplay.savefile@gmail.com", host + "[ADMIN] Báo cáo thủ công danh sách các IP user đã ghé thăm và request từng chức năng của trang web (tất cả/có thể chưa đầy đủ) In " + xuxu, noidung1, "teinnkatajeqerfl");

            return RedirectToAction("SettingXYZ_DarkAdmin", new { key = "code" });
        }

        public ActionResult DeleteIPComeHere()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPComeHere.txt");
            var noidung1 = docfile(path1);

            System.IO.File.WriteAllText(path1, "");
       
            return RedirectToAction("SettingXYZ_DarkAdmin", new { key = "code"});
        }

        public ActionResult QuickDataInWeb(string? first)
        {
            HttpContext.Session.Remove("ok-data");
            if (string.IsNullOrEmpty(first) == false)
                ViewBag.FirstGoTo = "true";
            else
                ViewBag.FirstGoTo = "false";

            Random ri = new Random();
            ViewBag.NumberRandom = ri.Next(3) + 1;

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flix = 0;
            var flox = 0;
            var flx = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }

                if (HttpContext.Session.GetString("TuyetDoi") != "true" && info[0] == "OffWebsite_All")
                {
                    if (info[1] == "true")
                        return RedirectToAction("Error","Home");
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

                if (info[0] == "Random_Layout")
                {
                    if (info[1] == "false")
                    {
                        TempData["Layout_Random"] = "false";
                    }
                    else
                    {
                        TempData["Layout_Random"] = "true";
                    }
                }

                if (info[0] == "Clear_Website")
                {
                    if (info[1] == "true")
                    {
                        flix = 1;
                    }
                }

                if (info[0] == "Using_QuickData")
                {
                    if (info[1] == "true")
                    {
                        flx = 1;
                    }
                }

                if (flix == 1 && flx == 1 && info[0] == "NotAlert_QuickData")
                {
                    if (info[1] == "true")
                    {
                        flox = 1;
                    }
                }

                if (flox == 0)
                {
                    if (HttpContext.Session.GetString("TuyetDoi") != "true" && (info[0] == "Using_QuickData" || info[0] == "Using_Website"))
                    {
                        if (info[1] == "false")
                        {
                            var r = new Random();
                            var x = r.Next(0, 2);
                            if (x == 1)
                                return Redirect("https://learn.microsoft.com/vi-vn/training/paths/get-started-c-sharp-part-1/?WT.mc_id=dotnet-35129-website");
                            return Redirect("https://stackoverflow.com/questions");
                        }
                    }
                }

                if (info[0] == "Off_RandomTab")
                {
                    if (info[1] == "false")
                    {
                        var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Image.txt");
                        var hinh = System.IO.File.ReadAllText(pathX1).Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Tittle.txt");
                        var tittle = System.IO.File.ReadAllText(pathX2).Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");
            TempData["RandomLayout"] = docfile(path);

            return View();
        }

        public ActionResult RemoveAllSessionAndTempData()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            HttpContext.Session.Clear();
            TempData.Clear();
            return Redirect("https://stackoverflow.com/questions");
        }

        [HttpPost]
        public ActionResult QuickDataInWeb(IFormCollection f)
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            HttpContext.Session.Remove("ok-data");
            try
            {
                var name = f["txtNoiDung"].ToString().Split("\r\n|\r\n", StringSplitOptions.RemoveEmptyEntries);
                TempData["Name"] = name[0];

                var s = name[1].Split("\r\n#\r\n", StringSplitOptions.RemoveEmptyEntries);
                var chuoi = "";
                for (int i = 0; i < s.Length; i++)
                {
                    var ss = s[i].Split("\r\n*\r\n", StringSplitOptions.RemoveEmptyEntries);
                    ss[1] = ss[1].Replace("[NULL]", "");

                    chuoi += "<textarea name=\"" + ss[0] + "\" cols=\"80\" rows=\"30\">" + ss[1].Trim('\"').Replace("[ngoackep_0000]", "\"") + "</textarea><br>\n";
                }

                TempData["Data"] = chuoi;

                HttpContext.Session.SetString("ok-data", "true");

                return RedirectToAction("PlayDataInWeb");
            }
            catch
            {
                return Redirect("http://stackoverflow.com/questions/4733878/how-to-debug-a-stackoverflowexception-in-net");
            }
        }

        public ActionResult PlayDataInWeb()
        {
            Random ri = new Random();
            ViewBag.NumberRandom = ri.Next(3) + 1;

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }

                if (HttpContext.Session.GetString("TuyetDoi") != "true" &&  info[0] == "OffWebsite_All")
                {
                    if (info[1] == "true")
                        return RedirectToAction("Error","Home");
                }

                if (HttpContext.Session.GetString("ok-data") != "true")
                {
                    return Redirect("http://stackoverflow.com/questions/206820/how-do-i-prevent-and-or-handle-a-stackoverflowexception");
                }

                if (TempData["NotAlertQuickData"] == "false")
                {
                    if (HttpContext.Session.GetString("TuyetDoi") != "true" && (info[0] == "Using_QuickData" || info[0] == "Using_Website"))
                    {
                        if (info[1] == "false")
                        {
                            var r = new Random();
                            var x = r.Next(0, 2);
                            if (x == 1)
                                return Redirect("https://learn.microsoft.com/vi-vn/training/paths/get-started-c-sharp-part-1/?WT.mc_id=dotnet-35129-website");
                            return Redirect("https://stackoverflow.com/questions");
                        }
                    }
                }

                if (info[0] == "Random_Layout")
                {
                    if (info[1] == "false")
                    {
                        TempData["Layout_Random"] = "false";
                    }
                    else
                    {
                        TempData["Layout_Random"] = "true";
                    }
                }

                if (info[0] == "Off_RandomTab")
                {
                    if (info[1] == "false")
                    {
                        var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Image.txt");
                        var hinh = System.IO.File.ReadAllText(pathX1).Split("\n", StringSplitOptions.RemoveEmptyEntries);

                        var pathX2 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomTab_Tittle.txt");
                        var tittle = System.IO.File.ReadAllText(pathX2).Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/RandomTab/RandomLayOut.txt");
            TempData["RandomLayout"] = docfile(path);
            return View();
        }

        public ActionResult RefreshInfoIPRegist()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/InfoIPRegist.txt");
            System.IO.File.WriteAllText(path, "IP\tDateTime\tInfo");
            return Redirect("/Admin/SettingXYZ_DarkAdmin#labelActive");
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

        public ActionResult RefreshFileHetHan()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
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

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Create();

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
                RedirectToAction("SettingXYZ_DarkAdmin");
            }

            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        public ActionResult SettingStatusUpdate(string type)
        {
            khoawebsiteAdmin();
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/Setting_Status.txt");

            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            var noidung = System.IO.File.ReadAllText(path);

              switch (type)
            {
                case "1":
                    System.IO.File.WriteAllText(path, "Tắt hoạt động web site (tất cả, không nhận đơn đăng kí tiếp theo), ngoại trừ mật độ tuyệt đối # " + xuxu);
                    break;

                case "2":
                    System.IO.File.WriteAllText(path, "Bật hoạt động web site (chỉ người đã đăng kí, bật TB, nhận đơn đăng kí tiếp theo) # " + xuxu);
                    break;

                case "3":
                    System.IO.File.WriteAllText(path, "Bật hoạt động web site (chỉ người đã đăng kí, tắt TB, không nhận đơn đăng kí tiếp theo) # " + xuxu);
                    break;

                case "4":
                    System.IO.File.WriteAllText(path, "Bật hoạt động website (cho phép tất cả mọi người, không get IP) # " + xuxu);
                    break;

                case "5":
                    System.IO.File.WriteAllText(path, "Bật hoạt động website (cho phép tất cả mọi người, get IP) # " + xuxu);
                    break;

                case "6":
                    System.IO.File.WriteAllText(path, "Tắt hoạt động web site (không nhận đơn đăng kí tiếp theo, bật thông báo), ngoại trừ các IP đã đăng kí và tin tưởng # " + xuxu);
                    break;

                case "7":
                    System.IO.File.WriteAllText(path, "Tắt hoạt động web site (không nhận đơn đăng kí tiếp theo, tắt thông báo), ngoại trừ các IP đã đăng kí và tin tưởng # " + xuxu);
                    break;

                case "8":
                    System.IO.File.WriteAllText(path, "Sử dụng POST QUICK DATA và tàng hình (copy), get IP - cho phép mọi người # " + xuxu);
                    break;

                case "9":
                    System.IO.File.WriteAllText(path, "Sử dụng POST QUICK DATA và tàng hình (download *.txt), get IP - cho phép mọi người # " + xuxu);
                    break;

                case "10":
                    var nd = noidung.Split(" # ");
                    System.IO.File.WriteAllText(path, nd[0] + " + Bật thông báo tất cả nội dung liên quan email và MegaIO File and list user get to link request # " + xuxu);
                    break;

                case "11":
                    var nd1 = noidung.Split(" # ");
                    System.IO.File.WriteAllText(path, nd1[0] +" + Tắt thông báo tất cả nội dung liên quan email và MegaIO File and list user get to link request # " + xuxu);
                    break;

                case "12":
                    var nd2 = noidung.Split(" # ");
                    System.IO.File.WriteAllText(path, nd2[0] + " + Bật sử dụng website with url encode secret play # " + xuxu);
                    break;

                case "ON-ALL":
                    System.IO.File.WriteAllText(path, "Đã bật hết tất cả các item setting (ngoại trừ mục cho phép website với mọi người nhưng sẽ ưu tiên việc get IP) và hãy cẩn thận sự mâu thuẫn giữa các item setting lúc này # " + xuxu);
                    break;

                case "OFF-ALL":
                    System.IO.File.WriteAllText(path, "Đã tắt hết tất cả các item setting  # " + xuxu);
                    break;

                default:
                    System.IO.File.WriteAllText(path, "UNKNOW # " + xuxu);
                    break;
            }
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }
    }
}
