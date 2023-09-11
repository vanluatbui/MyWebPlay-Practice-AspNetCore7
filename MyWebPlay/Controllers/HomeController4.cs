using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult String_Reverse()
        {
            khoawebsiteClient();
            return View();
        }


        [HttpPost]
        public ActionResult String_Reverse(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.String_Reverse();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
                s.Add(ds[i]);

            s.Reverse();

            string x = String.Join("\r\n", s);

            TextCopy.ClipboardService.SetText(x);

            //x = "\r\n" + x;
            //x = x.Replace("\r\n", "<br>");

            x = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + x + "</textarea>";

            ViewBag.Result = x;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult String_Reverse2()
        {
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult String_Reverse2(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.String_Reverse2();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
            {
                List<string> winx = ds[i].Split(' ').ToList();
               
                
                winx.Reverse();
                s.Add(String.Join(" ",winx));
            }

            string x = String.Join("\r\n", s);

            TextCopy.ClipboardService.SetText(x);


            x = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + x + "</textarea>";

            ViewBag.Result = x;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult Special_OrderBy()
        {
            khoawebsiteClient();
            return View();
        }

        [HttpPost]
        public ActionResult Special_OrderBy(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");

            string sapxep = f["SapXep"].ToString();
            sapxep = sapxep.Replace("[TAB-TPLAY]", "\t");
            sapxep = sapxep.Replace("[ENTER-NPLAY]", "\n");
            sapxep = sapxep.Replace("[ENTER-RPLAY]", "\r");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }

            string[] DS = Regex.Split(chuoi, "\r\n");

            string[] so = sapxep.Split('=');

            while (true)
            {
                int l = 0;

                string kq1 = String.Join("\n", DS);
                while (l < so.Length)
                {
                    for (int i = 0; i < DS.Length - 1; i++)
                    {
                        string[] phan1 = DS[i].Split(' ');
                        int x1 = 0;

                        if (so[l].Contains("N-") == true)
                        {
                            x1 = phan1.Length - int.Parse(so[l].Split('-')[1]);
                        }
                        else
                        {
                            x1 = int.Parse(so[l]);
                        }

                        string ss1 = "";
                        for (int j = 0; j < l; j++)
                        {
                            int soX = 0;
                            if (so[j].Contains("N-") == true)
                            {
                                soX = phan1.Length - int.Parse(so[j].Split('-')[1]);
                            }
                            else
                            {
                                soX = int.Parse(so[j]);
                            }
                            ss1 += phan1[soX];
                        }

                        for (int jk = i + 1; jk < DS.Length; jk++)
                        {
                            string[] phan2 = DS[jk].Split(' ');
                            int x2 = 0;

                            if (so[l].Contains("N-") == true)
                            {
                                x2 = phan2.Length - int.Parse(so[l].Split('-')[1]);
                            }
                            else
                            {
                                x2 = int.Parse(so[l]);
                            }

                            string ss2 = "";
                            for (int jj = 0; jj < l; jj++)
                            {
                                int soX = 0;
                                if (so[jj].Contains("N-") == true)
                                {
                                    soX = phan2.Length - int.Parse(so[jj].Split('-')[1]);
                                }
                                else
                                {
                                    soX = int.Parse(so[jj]);
                                }
                                ss2 += phan2[soX];
                            }

                            if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) > 0)
                            {
                                string t = DS[i];
                                DS[i] = DS[jk];
                                DS[jk] = t;
                            }
                        }
                    }
                    l++;
                }
                string kq2 = String.Join("\n", DS);
                if (String.Compare(kq1, kq2) == 0)
                    break;
            }

            string dx = String.Join("\n", DS);

            TextCopy.ClipboardService.SetText(dx);

            //String sql = "\r\n" + String.Join("\r\n", DS);

            //sql = sql.Replace("\r\n", "<br>");

            dx = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + dx + "</textarea>";

            ViewBag.Result = dx;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }
    }
}
