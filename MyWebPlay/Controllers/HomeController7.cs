using AppLapBangChanTri;
using Microsoft.AspNetCore.Mvc;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult BangChanTri()
        {
            khoawebsiteClient();
            return View();
        }

        void nhapbang(BT[] x, int t, int n, int w)
        {
            int k = 0, dem = 0;
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                if (dem == int.Parse(Math.Pow(2, n).ToString()) / int.Parse(Math.Pow(2, w).ToString()))
                {
                    if (k == 0)
                        k = 1;
                    else
                        k = 0;
                    dem = 0;
                }
                x[t].box[i] = k;
                dem++;
            }
        }

        BT[] x1;
        int n_BT = 0;

        int n1 = 0;

        String xemlai_BT = "";

        int xuatDSbieuthuc(BT[] x, int n)
        {
            int dem = 0;
            xemlai_BT = "\r\n* Danh sách biểu thức (BT) hiện có :\r\n";
            for (int i = 0; i < n; i++)
            {
                if (x[i].ten.Length != 0)
                {
                    xemlai_BT += "\r\n- Biểu thức " + (i + 1) + " : ";

                    xemlai_BT += x[i].ten;

                    dem++;
                }
            }
            return dem;
        }

        int xetluanly(int u, int v, int lc)
        {
            if (lc == 1)
            {
                if (u == 1 && v == 1)
                    return 1;
                return 0;
            }

            if (lc == 2)
            {
                if (u == 0 && v == 0)
                    return 0;
                return 1;
            }

            if (lc == 3)
            {
                if (u == 1 && v == 0)
                    return 0;
                return 1;
            }

            if (lc == 4)
            {
                if (u == 1 && v == 1 || u == 0 && v == 0)
                    return 1;
                return 0;
            }

            return 0;
        }

        void nhapbangBT1(BT[] x, int n, int t, int u)
        {
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                if (x[t].box[i] == 0)
                    x[u].box[i] = 1;
                else
                    x[u].box[i] = 0;
            }
        }

        void nhapbangBT2(BT[] x, int n, int t1, int t2, int u, int lc)
        {
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                int k = xetluanly(x[t1].box[i], x[t2].box[i], lc);
                x[u].box[i] = k;
            }
        }

        

        [HttpPost]
        public ActionResult BangChanTri(IFormCollection f)
        {
            string bandau = f["ThuocTinh"].ToString();
            bandau = bandau.Replace("[TAB-TPLAY]", "\t");
            bandau = bandau.Replace("[ENTER-NPLAY]", "\n");
            bandau = bandau.Replace("[ENTER-RPLAY]", "\r");
            if (string.IsNullOrEmpty(bandau))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.BangChanTri();
            }

            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            chuoi = chuoi.Replace("[PHU]", "#");
            chuoi = chuoi.Replace("[HOI]", "Λ");
            chuoi = chuoi.Replace("[TUYEN]", "∨");
            chuoi = chuoi.Replace("[KEOTHEO]", "→");
            chuoi = chuoi.Replace("[TUONGDUONG]", "⇔");

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.BangChanTri();
            }

            string[] bd = bandau.Split('-');
            n1 = bd.Length;

            if (x1 == null)
            {
                x1 = new BT[100];
                for (int i = 0; i < 100; i++)
                    x1[i] = new BT();
            }

            for (int i = 0; i < bd.Length; i++)
            {
                x1[n_BT].ten = bandau.Split('-')[i];
                nhapbang(x1, n_BT, n1, ++n_BT);
            }

            if (n_BT == n1)
            {
                // int dem = xuatDSbieuthuc(x1, n_BT);

                int lc = 0;
                string[] ch = chuoi.Split('\n');

                for (int i =0; i < ch.Length; i++)
                {
                    if (ch[i].Contains("#") == true)
                        lc = 0;
                    else
                         if (ch[i].Contains("Λ") == true)
                        lc = 1;
                    else
                         if (ch[i].Contains("∨") == true)
                        lc = 2;
                    else
                         if (ch[i].Contains("→") == true)
                        lc = 3;
                    else
                         if (ch[i].Contains("⇔") == true)
                        lc = 4;

                    if (lc == 0)
                    {
                        int t = int.Parse(ch[i].Split('#')[1]);
                        x1[n_BT].ten = "[ #";

                        x1[n_BT].ten += x1[t - 1].ten;
                        x1[n_BT].ten += " ]";

                        nhapbangBT1(x1, n1, t - 1, n_BT);
                        n_BT++;
                    }
                    else
                    {
                        int t1 = 0; 
                        int t2 = 0;

                        if (lc == 1)
                        {
                            t1 = int.Parse(ch[i].Split('Λ')[0]);
                            t2 = int.Parse(ch[i].Split('Λ')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 2)
                        {
                            t1 = int.Parse(ch[i].Split('∨')[0]);
                            t2 = int.Parse(ch[i].Split('∨')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 3)
                        {
                            t1 = int.Parse(ch[i].Split('→')[0]);
                            t2 = int.Parse(ch[i].Split('→')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 4)
                        {
                            t1 = int.Parse(ch[i].Split('⇔')[0]);
                            t2 = int.Parse(ch[i].Split('⇔')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }


                        if (lc == 1)
                            x1[n_BT].ten += " Λ " + x1[t2 - 1].ten + " ]";
                        else
                             if (lc == 2)
                            x1[n_BT].ten += " ∨ " + x1[t2 - 1].ten + " ]";
                        else
                        if (lc == 3)
                            x1[n_BT].ten += " →  " + x1[t2 - 1].ten + " ]";
                        else
                            x1[n_BT].ten += " ⇔ " + x1[t2 - 1].ten + " ]";

                        nhapbangBT2(x1, n1, t1 - 1, t2 - 1, n_BT, lc);
                        n_BT++;
                    }
                }  
               
            }

            int dem = xuatDSbieuthuc(x1, n_BT);
            string result = "\r\n";
            for (int i = 0; i < dem; i++)
                result += "BT" + (i + 1) + "\t\t";

            result += "\r\n\r\n";

            for (int i = 0; i < int.Parse(Math.Pow(2, n1).ToString()); i++)
            {
                for (int j = 0; j < dem; j++)
                    result += x1[j].box[i] + "\t\t";
                result += "\r\n";
            }

            TextCopy.ClipboardService.SetText(xemlai_BT + "\r\n\r\n\r\n" + result.Replace("\t\t","\t"));

            string re = xemlai_BT + "\r\n\r\n\r\n"  + result;

            re = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + re + "</textarea>";
            ViewBag.Result = re;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }
     }
}
