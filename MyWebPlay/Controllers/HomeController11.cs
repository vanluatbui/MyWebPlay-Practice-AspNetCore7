using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Regex_Replace_Multiple()
        {
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult Regex_Replace_Multiple(IFormCollection f)
        {

            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string dukien1 = f["DuKien1"].ToString();
            dukien1 = dukien1.Replace("[TAB-TPLAY]", "\t");
            dukien1 = dukien1.Replace("[ENTER-NPLAY]", "\n");
            dukien1 = dukien1.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string input = f["Input"].ToString();
            input = input.Replace("[TAB-TPLAY]", "\t");
            input = input.Replace("[ENTER-NPLAY]", "\n");
            input = input.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(input))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string yes = f["DuKien3"].ToString();
            yes = yes.Replace("[TAB-TPLAY]", "\t");
            yes = yes.Replace("[ENTER-NPLAY]", "\n");
            yes = yes.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(yes))
            {
                ViewData["Loi6"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string dukien2 = f["DuKien2"].ToString();
            dukien2 = dukien2.Replace("[TAB-TPLAY]", "\t");
            dukien2 = dukien2.Replace("[ENTER-NPLAY]", "\n");
            dukien2 = dukien2.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi5"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string dukien4 = f["DuKien4"].ToString();
            dukien4 = dukien4.Replace("[TAB-TPLAY]", "\t");
            dukien4 = dukien4.Replace("[ENTER-NPLAY]", "\n");
            dukien4 = dukien4.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(dukien4))
            {
                ViewData["Loi7"] = "Trường này không được để trống!";
                return this.Regex_Replace_Multiple();
            }

            string output = f["Output"].ToString();
            output = output.Replace("[TAB-TPLAY]", "\t");
            output = output.Replace("[ENTER-NPLAY]", "\n");
            output = output.Replace("[ENTER-RPLAY]", "\r");
            //if (string.IsNullOrEmpty(output))
            //{
            //    ViewData["Loi3"] = "Trường này không được để trống!";
            //    return this.Regex_Replace_Multiple();
            //}

            var listInput = Regex.Split(input, "\r\n");
            var listOutput = Regex.Split(output, "\r\n");

            if (listInput.Length != listOutput.Length)
            {
                ViewData["Loi3"] = "Số lượng thành phần pattern input và output không tương xứng...";
                return this.Regex_Replace_Multiple();
            }

            if (yes == "0")
            {
                for (int i = 0; i < listInput.Length - 1; i++)
                {
                    for (int j = i + 1; j < listInput.Length; j++)
                    {
                        if (listInput[j].Contains(listInput[i]))
                        {
                            string t = listInput[i];
                            listInput[i] = listInput[j];
                            listInput[j] = t;

                            string tt = listOutput[i];
                            listOutput[i] = listOutput[j];
                            listOutput[j] = tt;
                        }
                    }
                }
            }

            string result = "\r\n";

            if (int.Parse(dukien4) != 0)
            {
                    for (int i = 0; i < listInput.Length; i++)
                    {
                      for (int k = 0; k < int.Parse(dukien4); k++)
                      {
                        listInput[i] = listInput[i].Replace("\\n", "\n");
                        listInput[i] = listInput[i].Replace("\\r\\n", "\r\n");
                        listInput[i] = listInput[i].Replace("\\t", "\t");

                        listOutput[i] = listOutput[i].Replace("\\n", "\n");
                        listOutput[i] = listOutput[i].Replace("\\r\\n", "\r\n");
                        listOutput[i] = listOutput[i].Replace("\\t", "\t");

                        Regex regex = new Regex(listInput[i]);

                        if (int.Parse(dukien1) == 1)
                        {
                            while (listInput[i].Contains(@"\\") == true)
                            {
                                listInput[i] = listInput[i].Replace(@"\\", @"\");
                            }

                            while (listOutput[i].Contains(@"\\") == true)
                            {
                                listOutput[i] = listOutput[i].Replace(@"\\", @"\");
                            }

                            if (int.Parse(dukien2) == -1)
                            {
                                chuoi = regex.Replace(chuoi, listOutput[i]);
                                result = chuoi;
                            }
                            else
                            {
                                chuoi = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                                result = chuoi;
                            }
                        }
                        else
                        {
                            chuoi = chuoi.Replace(listInput[i], listOutput[i]);
                            result = chuoi;
                        }
                    }
                }
            }
            else
            {
                    for (int i = 0; i < listInput.Length; i++)
                    {
                      while (chuoi.Contains(listInput[i]) == true)
                       {
                        listInput[i] = listInput[i].Replace("\\n", "\n");
                        listInput[i] = listInput[i].Replace("\\r\\n", "\r\n");
                        listInput[i] = listInput[i].Replace("\\t", "\t");

                        listOutput[i] = listOutput[i].Replace("\\n", "\n");
                        listOutput[i] = listOutput[i].Replace("\\r\\n", "\r\n");
                        listOutput[i] = listOutput[i].Replace("\\t", "\t");
                        Regex regex = new Regex(listInput[i]);

                        if (int.Parse(dukien1) == 1)
                        {
                            while (listInput[i].Contains(@"\\") == true)
                            {
                                listInput[i] = listInput[i].Replace(@"\\", @"\");
                            }

                            while (listOutput[i].Contains(@"\\") == true)
                            {
                                listOutput[i] = listOutput[i].Replace(@"\\", @"\");
                            }

                            if (int.Parse(dukien2) == -1)
                            {
                                chuoi = regex.Replace(chuoi, listOutput[i]);
                                result = chuoi;
                            }
                            else
                            {
                                chuoi = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                                result = chuoi;
                            }
                        }
                        else
                        {
                            chuoi = chuoi.Replace(listInput[i], listOutput[i]);
                            result = chuoi;
                        }
                    }
                }
            }

            TextCopy.ClipboardService.SetText(result);

            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";


            return View();
        }
    }
}
