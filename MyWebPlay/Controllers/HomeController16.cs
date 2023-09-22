using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult XuLySQL5()
        {
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL5(IFormCollection f)
        {
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
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL6(IFormCollection f)
        {
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
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL7 (IFormCollection f)
        {
            string table = f["txtTable"].ToString();
            var daydu = f["txtDayDu"].ToString();
            var hientai = f["txtHienTai"].ToString();

            table = table.Replace("[TAB-TPLAY]", "\t");
            table = table.Replace("[ENTER-NPLAY]", "\n");
            table = table.Replace("[ENTER-RPLAY]", "\r");

            daydu = daydu.Replace("[TAB-TPLAY]", "\t");
            daydu = daydu.Replace("[ENTER-NPLAY]", "\n");
            daydu = daydu.Replace("[ENTER-RPLAY]", "\r");

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
            return View();
        }

        [HttpPost]
        public ActionResult SessionPlay(IFormCollection f)
        {
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
            khoawebsiteClient();
            return View();
        }

        public ActionResult XuLySQL10()
        {
            khoawebsiteClient();
            ViewBag.Statement = "SELECT  w2_User.*\r\n  FROM  w2_User\r\n WHERE  w2_User.user_id = @user_id\r\n   AND  w2_User.user_id_extend = @user_id_extend\r\n   AND  w2_User.user_name = @user_name\r\n   AND  w2_User.user_name_day = @user_name_day\r\n   AND  w2_User.user_age = @user_age";
            ViewBag.Replace = "@user_id\tuser001\r\n@user_id_extend\tABCDE\r\n@user_name_day\t20/06/2000\r\n@user_name\tLê Thị Mỹ Thư\r\n@user_age\t25";
            ViewBag.Param = "<Input Name=\"user_id\" Type=\"varchar\" Size=\"10\" />\r\n<Input Name=\"user_id_extend\" Type=\"char\" Size=\"50\" />\r\n<Input Name=\"user_name\" Type=\"nvarchar\" Size=\"100\" />\r\n<Input Name=\"user_name_day\" Type=\"datetime\" />\r\n<Input Name=\"user_age\" Type=\"int\" />";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL8(IFormCollection f)
        {
            var table = f["txtTable"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
            var fields = f["txtFields"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n");

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
            var statement = f["txtStatement"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
            var replace = f["txtReplace"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var param = f["txtParam"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

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
                var re2 = re[1];
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

            for (int i = 0; i < listInput.Count; i++)
                for (int j = 0; j < param.Length; j++)
                {
                    if (param[j].Contains("\"" + listInput[i].Replace("@", "") + "\""))
                    {
                        var pa = param[j];
                        var bien = listInput[i];
                        var tri = listOutput[i];
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


            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult GetColorAtPicture()
        {
            khoawebsiteClient();
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
            khoawebsiteClient();
            if (password == "admin-VANLUAT3275")
            {
                HttpContext.Session.SetObject("LoginAdmin", "YES");

                switch(id)
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
