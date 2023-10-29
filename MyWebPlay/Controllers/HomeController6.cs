using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Globalization;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult ResultCheck_Regex()
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
            ViewBag.ViDu = "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFGHIJKLMNOPQRSTUVWXYZ\t[\\]^_`\r\nabcdefghijklmnopqrstuvwxyz{|}~";
            return View();
        }

        [HttpPost]
        public ActionResult ResultCheck_Regex(IFormCollection f)
        {
            //HttpContext.Session.Remove("ok-data");
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

            ViewBag.ViDu = "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFGHIJKLMNOPQRSTUVWXYZ\t[\\]^_`\r\nabcdefghijklmnopqrstuvwxyz{|}~";
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.ResultCheck_Regex();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string pattern = f["Pattern"].ToString();
            pattern = pattern.Replace("[TAB-TPLAY]", "\t");
            pattern = pattern.Replace("[ENTER-NPLAY]", "\n");
            pattern = pattern.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(pattern))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.ResultCheck_Regex();
            }

            string dukien2 = f["DuKien2"].ToString();
            dukien2 = dukien2.Replace("[TAB-TPLAY]", "\t");
            dukien2 = dukien2.Replace("[ENTER-NPLAY]", "\n");
            dukien2 = dukien2.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.ResultCheck_Regex();
            }

            while (pattern.Contains(@"\\") == true)
            {
               pattern = pattern.Replace(@"\\", @"\");
            }

            string result = "\r\nChuỗi :\r\n\r\n"+chuoi+ "\r\n\r\nPattern :\r\n\r\n"+pattern+ "\r\n\r\n**********************************************************************\r\n\r\n";

            Regex regExp = new Regex(pattern);

            int stt = 0;
            foreach (Match match in regExp.Matches(chuoi))
            {
                stt++;
                if (int.Parse(dukien2) == 1)
                    result += "\r\n* Kết quả tìm kiếm thứ " + stt + " :\r\n\r\n" + match.Value.Replace(" ", "[Space]").Replace("\t", "[Tab]").Replace("\n","[Enter]") + "\r\n\r\n+ Vị trí xuất hiện : " + match.Index + "\r\n+ Độ dài : " + match.Length + "\r\n------------------------------------------------------\r\n\r\n";
                else
                    result += match.Value + "\r\n";
            }
            result += "\r\n==> Có " + stt + " kết quả tìm kiếm!";

            //TextCopy.ClipboardService.SetText(result);

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        //--------------------------------------------------

        [HttpGet]
        public ActionResult Replace_Regex()
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
        public ActionResult Replace_Regex(IFormCollection f)
        {
            //HttpContext.Session.Remove("ok-data");
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
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string dukien1 = f["DuKien1"].ToString();
            dukien1 = dukien1.Replace("[TAB-TPLAY]", "\t");
            dukien1 = dukien1.Replace("[ENTER-NPLAY]", "\n");
            dukien1 = dukien1.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string input = f["Input"].ToString();
            input = input.Replace("[TAB-TPLAY]", "\t");
            input = input.Replace("[ENTER-NPLAY]", "\n");
            input = input.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(input))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string dukien2 = f["DuKien2"].ToString();
            dukien2 = dukien2.Replace("[TAB-TPLAY]", "\t");
            dukien2 = dukien2.Replace("[ENTER-NPLAY]", "\n");
            dukien2 = dukien2.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi5"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string dukien3 = f["DuKien3"].ToString();
            dukien3 = dukien3.Replace("[TAB-TPLAY]", "\t");
            dukien3 = dukien3.Replace("[ENTER-NPLAY]", "\n");
            dukien3 = dukien3.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien3))
            {
                ViewData["Loi6"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string output = f["Output"].ToString();
            output = output.Replace("[TAB-TPLAY]", "\t");
            output = output.Replace("[ENTER-NPLAY]", "\n");
            output = output.Replace("[ENTER-RPLAY]", "\r");
            //if (string.IsNullOrEmpty(output))
            //{
            //    ViewData["Loi3"] = "Trường này không được để trống!";
            //    return this.Replace_Regex();
            //}

            Regex regex = new Regex(input);
            string result = "\r\n";

            if (int.Parse(dukien3) != 0)
            {
                for (int i = 0; i < int.Parse(dukien3); i++)
                {
                    if (int.Parse(dukien1) == 1)
                    {
                        while (input.Contains(@"\\") == true)
                        {
                            input = input.Replace(@"\\", @"\");
                        }

                        while (output.Contains(@"\\") == true)
                        {
                            output = output.Replace(@"\\", @"\");
                        }

                        if (int.Parse(dukien2) == -1)
                        {
                            chuoi = regex.Replace(chuoi, input);
                            result = chuoi;
                        }
                        else
                        {
                            chuoi = regex.Replace(chuoi, output, int.Parse(dukien2));
                            result = chuoi;
                        }
                    }
                    else
                    {
                        chuoi = chuoi.Replace(input, output);
                        result = chuoi;
                    }
                }
            }
            else
            {
                while (chuoi.Contains(input) == true)
                {
                    if (int.Parse(dukien1) == 1)
                    {
                        while (input.Contains(@"\\") == true)
                        {
                            input = input.Replace(@"\\", @"\");
                        }

                        while (output.Contains(@"\\") == true)
                        {
                            output = output.Replace(@"\\", @"\");
                        }

                        if (int.Parse(dukien2) == -1)
                        {
                            chuoi = regex.Replace(chuoi, input);
                            result = chuoi;
                        }
                        else
                        {
                            chuoi = regex.Replace(chuoi, output, int.Parse(dukien2));
                            result = chuoi;
                        }
                    }
                    else
                    {
                        chuoi = chuoi.Replace(input, output);
                        result = chuoi;
                    }
                }
            }

            //TextCopy.ClipboardService.SetText(result);

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

    }
}
