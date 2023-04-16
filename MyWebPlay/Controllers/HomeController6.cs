using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult ResultCheck_Regex()
        {
            ViewBag.ViDu = "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFGHIJKLMNOPQRSTUVWXYZ\t[\\]^_`\r\nabcdefghijklmnopqrstuvwxyz{|}~";
            return View();
        }

        [HttpPost]
        public ActionResult ResultCheck_Regex(IFormCollection f)
        {
            ViewBag.ViDu = "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFGHIJKLMNOPQRSTUVWXYZ\t[\\]^_`\r\nabcdefghijklmnopqrstuvwxyz{|}~";
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.ResultCheck_Regex();
            }

            string pattern = f["Pattern"].ToString();
            if (string.IsNullOrEmpty(pattern))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.ResultCheck_Regex();
            }

            string dukien2 = f["DuKien2"].ToString();
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

            TextCopy.ClipboardService.SetText(result);

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        //--------------------------------------------------

        [HttpGet]
        public ActionResult Replace_Regex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Replace_Regex(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string dukien1 = f["DuKien1"].ToString();
            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string input = f["Input"].ToString();
            if (string.IsNullOrEmpty(input))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string dukien2 = f["DuKien2"].ToString();
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi5"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string dukien3 = f["DuKien3"].ToString();
            if (string.IsNullOrEmpty(dukien3))
            {
                ViewData["Loi6"] = "Trường này không được để trống!";
                return this.Replace_Regex();
            }

            string output = f["Output"].ToString();
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

            TextCopy.ClipboardService.SetText(result);

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }

    }
}
