using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult XuLySQL5()
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
        public ActionResult XuLySQL5(IFormCollection f)
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
            TempData["dataPost"] = "[" + f["txtKetQua"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            var listTable = f["txtKetQua"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            var result = "CREATE PROC PROC_MYWEBPLAY\nAS\nBEGIN";

            for (int i = 0; i < listTable.Length; i++)
            {
                result += "\n\tIF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + listTable[i]+"')\n\tBEGIN";
                result += "\n\t\tDECLARE @" + listTable[i] + " INT = (SELECT COUNT(*) FROM " + listTable[i] + ")\n\t\tPRINT @" + listTable[i] +"\n\tEND";
            }

            result += "\nEND\n\n";

            result += "--------------------SỬ DỤNG XONG HÃY NHỚ DROP PROC PROC_MYWEBPLAY---------------------\n\n\n";

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

        public ActionResult XuLySQL6()
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
        public ActionResult XuLySQL6(IFormCollection f)
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
            TempData["dataPost"] = "[" + f["txtKetQua"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            var listTable = f["txtKetQua"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            
            var result = "CREATE PROC PROC_MYWEBPLAY1\nAS\nBEGIN\n\nPRINT N'Danh sách các table chưa tồn tại : '+NCHAR(10)+NCHAR(10)\n";

            for (int i = 0; i < listTable.Length; i++)
            {
                result += "\n\tIF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + listTable[i] + "')\n\tBEGIN";
                result += "\n\t\tPRINT '" + listTable[i] + "'\n\tEND";
            }

            result += "\nEND\n\n";

            result += "--------------------SỬ DỤNG XONG HÃY NHỚ DROP PROC PROC_MYWEBPLAY1---------------------\n\n\n";

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

        public ActionResult XuLySQL7 ()
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
        public ActionResult XuLySQL7 (IFormCollection f)
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
            string table = f["txtTable"].ToString();
            var daydu = f["txtDayDu"].ToString();
            var hientai = f["txtHienTai"].ToString();

            table = table.Replace("[TAB-TPLAY]", "\t");
            table = table.Replace("[ENTER-NPLAY]", "\n");
            table = table.Replace("[ENTER-RPLAY]", "\r");

            daydu = daydu.Replace("[TAB-TPLAY]", "\t");
            daydu = daydu.Replace("[ENTER-NPLAY]", "\n");
            daydu = daydu.Replace("[ENTER-RPLAY]", "\r");
            TempData["dataPost"] = "[" + daydu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            hientai = hientai.Replace("[TAB-TPLAY]", "\t");
            hientai = hientai.Replace("[ENTER-NPLAY]", "\n");
            hientai = hientai.Replace("[ENTER-RPLAY]", "\r");

            var listDayDu = daydu.Split("\r\n");
            var result = "ALTAR TABLE " + table + " ADD";

            for (int i =0; i< listDayDu.Length; i++)
            {
                var xx = Regex.Split(listDayDu[i], "\t| ");
                if (hientai.Contains(xx[0]) == false)
                    result += "\n\t" + xx[0] + " " + xx[1];
            }

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult SessionPlay()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");

            return View();
        }

        [HttpPost]
        public ActionResult SessionPlay(IFormCollection f)
        {
            TempData["dataPost"] = "[POST]";
            HttpContext.Session.Remove("ok-data");
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");

            var session = f["txtSession"].ToString();
            var giatri = f["txtGiaTri"].ToString();
            var chon = f["txtChon"].ToString();

            ViewBag.Session = session;
            ViewBag.GiaTri = giatri;

            if (chon == "1")
            {
                if (giatri != "")
                    HttpContext.Session.SetObject(session, giatri);
                else
                    HttpContext.Session.SetObject(session, "Default Session Webplay");
            }
            else
            if (chon == "2")
            {
                ViewBag.KetQua = HttpContext.Session.GetObject<string>(session);
            }
            else
            if (chon == "3")
            {
                ViewBag.KetQua = "";
                HttpContext.Session.Remove(session);
            }
            else
            if (chon == "4")
            {
                ViewBag.KetQua = "";
                HttpContext.Session.Clear();
            }

            return View();
        }

        public ActionResult XuLySQL8()
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

        public ActionResult XuLySQL10()
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
            ViewBag.Statement = "SELECT  w2_User.*\r\n  FROM  w2_User\r\n WHERE  w2_User.user_id = @user_id\r\n   AND  w2_User.user_id_extend = @user_id_extend\r\n   AND  w2_User.user_name = @user_name\r\n   AND  w2_User.user_name_day = @user_name_day\r\n   AND  w2_User.user_age = @user_age";
            ViewBag.Replace = "@user_id\tuser001\r\n@user_id_extend\t\r\n@user_name_day\t20/06/2000\r\n@user_name\tLê Thị Mỹ Thư\r\n@user_age\t25";
            ViewBag.Param = "<Input Name=\"user_id\" Type=\"varchar\" Size=\"10\" />\r\n<Input Name=\"user_id_extend\" Type=\"char\" Size=\"50\" />\r\n<Input Name=\"user_name\" Type=\"nvarchar\" Size=\"100\" />\r\n<Input Name=\"user_name_day\" Type=\"datetime\" />\r\n<Input Name=\"user_age\" Type=\"int\" />";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL8(IFormCollection f)
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
            var fields = f["txtFields"].ToString().Split("\n");
            TempData["dataPost"] = "[" + f["txtFields"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            var table = f["txtTable"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
           

            var loai = int.Parse(f["txtLoai"].ToString());
            if (loai == 2)
            {
            var list = new List<string>();
            for (int i = 0; i < fields.Length; i++)
            {
                    if (fields[i].Contains(" = ") == false)
                        continue;

                var fi = fields[i].Split(" = ");
                    if (fi[1].Contains("<summary>") || string.IsNullOrEmpty(fi[1]))
                        continue;

                    var fix = fi[1].Split(";", StringSplitOptions.RemoveEmptyEntries);

                list.Add(fix[0].Replace("\"", "") + " nvarchar(max)");
            }
                fields = string.Join("\n", list).Split("\n");
        }

                var result = "";
            for (int i = 0; i < fields.Length;i++)
            {
                while (fields[i].Contains("  "))
                    fields[i] = fields[i].Replace("  ", " ");

                fields[i] = fields[i].Replace("\t", "").Replace("[","").Replace("]","");

                var fi = fields[i].Split(" ");

                result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '"+table+"' AND COLUMN_NAME = '" + fi[0] +"')\r\nBEGIN\r\n\tALTER TABLE "+table+" ADD " + fields[i].Replace(",","")+"\r\nEND";
            }

            result += "\n\nPRINT N'XONG, QUÁ TRÌNH XỬ LÝ HOÀN TẤT!'";

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL10(IFormCollection f)
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
            var statement = f["txtStatement"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
            var replace = f["txtReplace"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var param = f["txtParam"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var chon = f["txtChon"].ToString();

            TempData["dataPost"] = "[" + statement.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

           
            ViewBag.Statement = statement;
            ViewBag.Replace = f["txtReplace"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
            ViewBag.Param = f["txtParam"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");

            var result = statement;
            var listInput = new List<string>();
            var listOutput = new List<string>();
            for (int i = 0; i < replace.Length; i++)
            {
                var re = replace[i].Trim(' ').Trim('\t').Split("\t", StringSplitOptions.RemoveEmptyEntries);
                var re1 = re[0];
                var re2 = (re.Length < 2) ? "" : re[1];
                listInput.Add(re1);
                listOutput.Add(re2);
            }

            // Sort if ...

            for (int i = 0; i < listInput.Count - 1; i++)
                for (int j = i + 1; j < listInput.Count; j++)
                {
                    if (listInput[j].Contains(listInput[i]))
                    {
                        var t = listInput[i];
                        listInput[i] = listInput[j];
                        listInput[j] = t;

                        var tt = listOutput[i];
                        listOutput[i] = listOutput[j];
                        listOutput[j] = tt;
                    }
                }

            if (chon == "on")
            {
                var s = "";
                for (int i = 0; i < listInput.Count; i++)
                {
                    s += "<Input Name=\"" + listInput[i].Replace("@","") + "\" Type=\"nvarchar\" Size=\"MAX\" />\n";
                }
                s = s.Trim('\n');
                param = s.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                ViewBag.Param = s;
            }

            for (int i = 0; i < listInput.Count; i++)
                for (int j = 0; j < param.Length; j++)
                {
                    if (param[j].Contains("\"" + listInput[i].Replace("@", "") + "\""))
                    {
                        var pa = param[j];
                        var bien = listInput[i];
                        var tri = listOutput[i];
                        if (chon == "on")
                        {
                            result = result.Replace(bien, "N'" + tri + "'");
                        }
                        else
                        {
                            if (pa.Contains("char") == true || pa.Contains("text") == true ||
                    pa.Contains("binary") == true || pa.Contains("image") == true)
                            {
                                result = result.Replace(bien, "N'" + tri + "'");
                            }
                            else
                 if (pa.Contains("memo") || pa.Contains("single") ||
                    pa.Contains("currency") || pa.Contains("money") || pa.Contains("double") ||
                    pa.Contains("long") || pa.Contains("byte") ||
                    pa.Contains("bit") || pa.Contains("int") ||
                    pa.Contains("decimal") || pa.Contains("numeric") ||
                    pa.Contains("money") || pa.Contains("float") ||
                    pa.Contains("real"))
                            {
                                result = result.Replace(bien, tri);
                            }
                            else if (pa.Contains("date"))
                            {
                                result = result.Replace(bien, "'" + tri + "'");
                            }
                            break;
                        }
                    }
                }

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult GetColorAtPicture()
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
            ViewBag.HinhAnh = "NO";
            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Delete(true);

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Create();

            return View();
        }

        [HttpPost]
        public ActionResult GetColorAtPicture(IFormFile txtFile)
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture", txtFile.FileName);
            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtFile.CopyTo(fileStream);
            }

            ViewBag.HinhAnh = "/GetColorAtPicture/"+txtFile.FileName;


            return View();
        }

        public ActionResult LoginAdmin(string? folder, string? password, int? id, string? url)
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            HttpContext.Session.SetString("adminDirectURL", "YES");
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

                    if (password == passAd)
            {
                HttpContext.Session.SetObject("LoginAdmin", "YES");

                switch (id)
                {
                    case 1:
                        return RedirectToAction("XoaAllFile", new {password = password, url = url});
                    case 2:
                        return RedirectToAction("AllDownload", new { password = password});
                    case 3:
                        return RedirectToAction("FindSubFolders", new {folder = folder, password = password});
                }
            }
            else
            {
                HttpContext.Session.Remove("LoginAdmin");
                switch (id)
                {
                    case 1:
                        return RedirectToAction(url);
                    case 2:
                        return RedirectToAction("DownloadFile");
                    case 3:
                        return RedirectToAction("DownloadFile");
                }

            }
            return RedirectToAction("Index");
        }
    }
}
