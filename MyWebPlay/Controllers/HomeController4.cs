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

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
                s.Add(ds[i]);

            s.Reverse();

            string x = String.Join("\n", s);

            TextCopy.ClipboardService.SetText(x);

            ViewBag.KetQua = "Thành công! Một kết quả đã được lưu copy vào Clipboard của bạn!";
            return View();
        }

        [HttpGet]
        public ActionResult String_Reverse2()
        {
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

            string[] ds = Regex.Split(chuoi, "\r\n");
            List<string> s = new List<string>();

            for (int i = 0; i < ds.Length; i++)
            {
                List<string> winx = ds[i].Split(' ').ToList();
               
                
                winx.Reverse();
                s.Add(String.Join(" ",winx));
            }

            string x = String.Join("\n", s);

            TextCopy.ClipboardService.SetText(x);

            ViewBag.KetQua = "Thành công! Một kết quả đã được lưu copy vào Clipboard của bạn!";
            return View();
        }

        [HttpGet]
        public ActionResult Special_OrderBy()
        {
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

            string sapxep = f["SapXep"].ToString();
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
        


            TextCopy.ClipboardService.SetText(String.Join("\n",DS));

            ViewBag.KetQua = "Thành công! Một kết quả đã được lưu copy vào Clipboard của bạn!";
            return View();
        }
    }
}
