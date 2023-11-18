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
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        [HttpPost]
        public ActionResult XuLySQL5(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            TempData["dataPost"] = "[" + f["txtKetQua"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

        public ActionResult XuLySQL6()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        [HttpPost]
        public ActionResult XuLySQL6(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            TempData["dataPost"] = "[" + f["txtKetQua"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

        public ActionResult XuLySQL7 ()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        [HttpPost]
        public ActionResult XuLySQL7 (IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
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
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

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
            else
            if (chon == "5")
            {
                ViewBag.KetQua = "";
                TempData.Clear();
            }

            return View();
        }

        public ActionResult XuLySQL8()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        public ActionResult XuLySQL10()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            ViewBag.Statement = "SELECT  w2_User.*\r\n  FROM  w2_User\r\n WHERE  w2_User.user_id = @user_id\r\n   AND  w2_User.user_id_extend = @user_id_extend\r\n   AND  w2_User.user_name = @user_name\r\n   AND  w2_User.user_name_day = @user_name_day\r\n   AND  w2_User.user_age = @user_age";
            ViewBag.Replace = "@user_id\tuser001\r\n@user_id_extend\t\r\n@user_name_day\t20/06/2000\r\n@user_name\tLê Thị Mỹ Thư\r\n@user_age\t25";
            ViewBag.Param = "<Input Name=\"user_id\" Type=\"varchar\" Size=\"10\" />\r\n<Input Name=\"user_id_extend\" Type=\"char\" Size=\"50\" />\r\n<Input Name=\"user_name\" Type=\"nvarchar\" Size=\"100\" />\r\n<Input Name=\"user_name_day\" Type=\"datetime\" />\r\n<Input Name=\"user_age\" Type=\"int\" />";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL8(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
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
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL10(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
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
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult GetColorAtPicture()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        public ActionResult XuLySQL11()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

            ViewBag.Table = "w2_User";
            ViewBag.Constants = "public const string FIELD_USER_USER_ID = \"user_id\";                                         // ユーザID\r\n\t\tpublic const string FIELD_USER_USER_KBN = \"user_kbn\";                                       // 顧客区分\r\n\t\tpublic const string FIELD_USER_MALL_ID = \"mall_id\";                                         // モールID\r\n\t\tpublic const string FIELD_USER_NAME = \"name\";                                               // 氏名\r\n\t\tpublic const string FIELD_USER_NAME1 = \"name1\";                                             // 氏名1\r\n\t\tpublic const string FIELD_USER_NAME2 = \"name2\";                                             // 氏名2\r\n\t\tpublic const string FIELD_USER_NAME_KANA = \"name_kana\";                                     // 氏名かな\r\n\t\tpublic const string FIELD_USER_NAME_KANA1 = \"name_kana1\";                                   // 氏名かな1\r\n\t\tpublic const string FIELD_USER_NAME_KANA2 = \"name_kana2\";                                   // 氏名かな2\r\n\t\tpublic const string FIELD_USER_NICK_NAME = \"nick_name\";                                     // ニックネーム\r\n\t\tpublic const string FIELD_USER_MAIL_ADDR = \"mail_addr\";                                     // メールアドレス\r\n\t\tpublic const string FIELD_USER_MAIL_ADDR2 = \"mail_addr2\";                                   // メールアドレス2";
            ViewBag.Script = "[user_id] [nvarchar] (30) NOT NULL,\r\n\t[user_kbn] [nvarchar] (10) NOT NULL DEFAULT (N'PC_USER'),\r\n\t[mall_id] [nvarchar] (30) NOT NULL DEFAULT (N'OWN_SITE'),\r\n\t[name] [nvarchar] (40) NOT NULL DEFAULT (N''),\r\n\t[name1] [nvarchar] (20) NOT NULL DEFAULT (N''),\r\n\t[name2] [nvarchar] (20) NOT NULL DEFAULT (N''),\r\nALTER TABLE [w2_User] ADD [mail_addr2] datetime NOT NULL DEFAULT (N'');\r\nALTER TABLE [w2_User] ADD [name_kana] float NOT NULL DEFAULT (N'');";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL11(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            var table = f["txtTable"].ToString();
            var constants = f["txtConstants"].ToString().Trim('\t').Trim(' ').Split("\r\n");
            var script = f["txtScript"].ToString().Trim('\t').Trim(' ').Split("\r\n");

            ViewBag.Table = table;
            ViewBag.Constants = f["txtConstants"].ToString();
            ViewBag.Script = f["txtScript"].ToString();

            TempData["dataPost"] = "[" + f["txtConstants"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            var chon = f["txtChon"].ToString();

            var result = "";

            var listFieldConstants = new List<string>();

            for (int i =0; i < constants.Length; i++)
            {
                if (constants[i].Contains("<summary>"))
                    continue;

                var xy = constants[i].Split(" = \"");
                var xyy = xy[1].Split("\";");

                listFieldConstants.Add(xyy[0]);
            }

            for (int i = 0; i<script.Length; i++)
            {
                if (script[i].Contains("ALTER TABLE"))
                {
                    script[i] = script[i].Replace("[", "").Replace("]", "").Replace(";","");
                    var xo = script[i].Split("ADD ");
                    var xp = xo[1].Split(" ");

                    if (chon == "on")
                       
                    result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + xp[0] + "')\r\nBEGIN\r\n\tPRINT '" + script[i].Replace("ALTER TABLE " + table + " ADD ", "") + "," + "'\r\nEND";
                    else
                    result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + xp[0] + "')\r\nBEGIN\r\n\t" + script[i] + "\r\nEND";
                    listFieldConstants.Remove(xp[0]);
                }
                else
                {
                    script[i] = script[i].Replace(",", "").Replace("[", "").Replace("]", "").Trim('\t').Trim(' ');
                    var xo = script[i].Split(" ");
                    if (chon == "on")
                        result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + xo[0] + "')\r\nBEGIN\r\n\tPRINT '" + script[i] + "," + "'\r\nEND";
                    else
                        result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + xo[0] + "')\r\nBEGIN\r\n\tALTER TABLE " + table + " ADD " + script[i] + "\r\nEND";
                    listFieldConstants.Remove(xo[0]);
                }
            }

            for (var i = 0; i<listFieldConstants.Count;i++)
            {
                if (chon == "on")
                result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + listFieldConstants[i] + "')\r\nBEGIN\r\n\tPRINT '" + listFieldConstants[i] + " nvarchar(max) NOT NULL DEFAULT (N'')" + "," + "'\r\nEND";
                else
                    result += "\n\nIF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "' AND COLUMN_NAME = '" + listFieldConstants[i] + "')\r\nBEGIN\r\n\tALTER TABLE " + table + " ADD " + listFieldConstants[i] + " nvarchar(max) NOT NULL DEFAULT (N'')\r\nEND";
            }

            if (chon != "on")
            result += "\n\nPRINT N'XONG, QUÁ TRÌNH XỬ LÝ HOÀN TẤT!'";
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult XuLySQL12()
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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

        [HttpPost]
        public ActionResult XuLySQL12(IFormCollection f)
        {
            khoawebsiteClient(null); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            var noidung = f["txtNoiDung"].ToString();

            TempData["dataPost"] = "[" + f["txtNoiDung"].ToString().Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
            khoawebsiteClient(listIP);
            HttpContext.Session.Remove("ok-data");
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

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

            var xuan = noidung.Split("\r\n");

            var result = "";
            for (var i =0; i <xuan.Length; i++)
            {
                if (string.IsNullOrEmpty(xuan[i]) || string.IsNullOrWhiteSpace(xuan[i]))
                {
                    result += " + CHAR(13) + CHAR(10)";
                    continue;
                }

                result += "\r\n";

                if (i == 0)
                {
                    result += "N'" + xuan[i] + "'";
                }
                else
                {
                    result += "[SPACE]+ '" + xuan[i] + "'";
                }
            }
            var nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var cuctac = soi.AddHours(DateTime.UtcNow, 7); var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }
    }
}
