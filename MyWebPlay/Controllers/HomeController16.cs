using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System;
using System.Collections;
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
            var listTable = f["txtKetQua"].ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            var result = "CREATE PROC PROC_MYWEBPLAY\nAS\nBEGIN";

            for (int i = 0; i < listTable.Length; i++)
            {
                result += "\n\tIF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + listTable[i]+"')\n\tBEGIN";
                result += "\n\t\tDECLARE @" + listTable[i] + " INT = (SELECT COUNT(*) FROM " + listTable[i] + ")\n\t\tPRINT @" + listTable[i] +"\n\tEND";
            }

            result += "\nEND\n\n";

            result += "--------------------SỬ DỤNG XONG HÃY NHỚ DROP PROC PROC_MYWEBPLAY---------------------\n\n\n";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

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
            var listTable = f["txtKetQua"].ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            var result = "CREATE PROC PROC_MYWEBPLAY1\nAS\nBEGIN\n\nPRINT N'Danh sách các table chưa tồn tại : '+NCHAR(10)+NCHAR(10)\n";

            for (int i = 0; i < listTable.Length; i++)
            {
                result += "\n\tIF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + listTable[i] + "')\n\tBEGIN";
                result += "\n\t\tPRINT '" + listTable[i] + "'\n\tEND";
            }

            result += "\nEND\n\n";

            result += "--------------------SỬ DỤNG XONG HÃY NHỚ DROP PROC PROC_MYWEBPLAY1---------------------\n\n\n";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

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

            var listDayDu = daydu.Split("\r\n");
            var result = "ALTAR TABLE " + table + " ADD";

            for (int i =0; i< listDayDu.Length; i++)
            {
                var xx = Regex.Split(listDayDu[i], "\t| ");
                if (hientai.Contains(xx[0]) == false)
                    result += "\n\t" + xx[0] + " " + xx[1];
            }

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

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
    }
}
