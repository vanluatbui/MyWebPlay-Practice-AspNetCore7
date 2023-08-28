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

        public ActionResult GetColorAtPicture()
        {
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
