using AppFindMainKey_CSDL;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CheckText()
        {
            return View();
        }

        private int kiemtrakitu_codau(String x)
        {
            if (x == "à" || x == "á" || x == "ả" || x == "ã" || x == "ạ")
                return 1;
            if (x == "ă" || x == "ằ" || x == "ắ" || x == "ẳ" || x == "ẵ" || x == "ặ")
                return 1;
            if (x == "â" || x == "ầ" || x == "ấ" || x == "ẩ" || x == "ẫ" || x == "ậ")
                return 1;
            if (x == "đ")
                return 1;
            if (x == "é" || x == "è" || x == "ẻ" || x == "ẽ" || x == "ẹ")
                return 1;
            if (x == "ê" || x == "ề" || x == "ế" || x == "ể" || x == "ễ" || x == "ệ")
                return 1;
            if (x == "í" || x == "ì" || x == "ỉ" || x == "ĩ" || x == "ị")
                return 1;
            if (x == "ó" || x == "ò" || x == "ỏ" || x == "õ" || x == "ọ")
                return 1;
            if (x == "ô" || x == "ồ" || x == "ố" || x == "ổ" || x == "ỗ" || x == "ộ")
                return 1;
            if (x == "ơ" || x == "ờ" || x == "ớ" || x == "ở" || x == "ỡ" || x == "ợ")
                return 1;
            if (x == "ù" || x == "ú" || x == "ủ" || x == "ũ" || x == "ụ")
                return 1;
            if (x == "ư" || x == "ừ" || x == "ứ" || x == "ử" || x == "ữ" || x == "ự")
                return 1;
            if (x == "ý" || x == "ỳ" || x == "ỷ" || x == "ỹ" || x == "ỵ")
                return 1;

            return 0;
        }

        private String chuyendoi_2(String input)
        {

            input = input.Replace(" .#3275#. ", "");
            input = input.Replace("\r\n", " .#3275#. ");
            input = input.ToLower();
            char[] s = input.ToCharArray();
            s[0] = char.ToUpper(s[0]);

            int ss = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] >= 'a' && s[i] <= 'z' || kiemtrakitu_codau(s[i].ToString()) == 1)
                {
                    ss = i;
                    break;
                }
            }

            for (int i = 1; i < ss; i++)
            {
                if (s[i] == '.' || s[i] == '?' || s[i] == '!')
                {
                    i++;
                    int j = i;
                    while (true)
                    {
                        if (s[j] >= 'a' && s[j] <= 'z' || kiemtrakitu_codau(s[j].ToString()) == 1)
                        {
                            s[j] = char.ToUpper(s[j]);
                            break;
                        }
                        j++;
                        i = j;
                    }
                }
            }
            return new String(s).Replace(" .#3275#. ", "\r\n");
        }

        private String chuyendoi_3(String input)
        {
            input = input.Trim();
            char[] s = input.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ' || s[i] == '\n')
                {
                    i++;
                    int j = i;
                    while (true)
                    {
                        if (s[j] != ' ' && s[j] != '\n')
                            break;

                        s[j] = '#';
                        j++;
                        i = j;
                    }
                }
            }
            return new String(s).Replace("#", "");
        }

        [HttpPost]
        public ActionResult CheckText(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.CheckText();
            }

            string s = chuoi;

            // viết hoa chữ cái đầu mỗi câu...

            s = chuyendoi_2(s);

            // nếu sau dấu .  ? : ! , ; không có dấu cách thì thêm sau nó

            s = s.Replace(".", ". ");
            s = s.Replace("?", "? ");
            s = s.Replace(":", ": ");
            s = s.Replace("!", "! ");
            s = s.Replace(",", ", ");
            s = s.Replace(";", "; ");

            // xoá khoảng cách dư thừa

            s = chuyendoi_3(s);

            // nếu sau { [ ( " có dấu cách thì xoá nó.

            s = s.Replace("{ ", "{");
            s = s.Replace("[ ", "[");
            s = s.Replace("( ", "(");
            s = s.Replace("\" ", "\"");

            // nếu trước } ] ) " và . , ; ? ! : có dấu cách thì xoá nó.

            s = s.Replace(" }", "}");
            s = s.Replace(" ]", "]");
            s = s.Replace(" )", ")");
            s = s.Replace(" \"", "\"");

            s = s.Replace(" .", ".");
            s = s.Replace(" ,", ",");
            s = s.Replace(" ;", ";");
            s = s.Replace(" !", "!");
            s = s.Replace(" ?", "?");
            s = s.Replace(" :", ":");

            //...

            while (s.Contains("\r\n\r\n\r\n") == true)
                s = s.Replace("\r\n\r\n\r\n", "\r\n\r\n");

            while (s.Contains(" \r\n") == true)
                s = s.Replace(" \r\n", "\r\n");

            TextCopy.ClipboardService.SetText(s);

           // s = "<p style=\"color:blue\"" + s + "</p>";

            s = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

            ViewBag.Result = s;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult TextToColumn1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextToColumn1(IFormCollection f)
        {
            if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.TextToColumn1();
            }

            if (string.IsNullOrEmpty(f["Number"].ToString()))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.TextToColumn1();
            }

            string chuoi = f["Chuoi"].ToString();
            int n = int.Parse(f["Number"].ToString());

            string[] s = Regex.Split(chuoi, "\r\n");

            if (s.Length % n != 0)
            {
                ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
                return this.TextToColumn1();
            }

            string ss = "";
            for (int i = 0; i < s.Length; i = i + n)
            {
                int dem = 0;
                for (int j = i; dem < n; j++)
                {
                    ss += s[j] + "\t";
                    dem++;
                }
                char[] xx = { '\t' };
                ss = ss.TrimEnd(xx);
                ss += "\r\n";
            }

            char[] x = { '\r','\n' };
            ss = ss.TrimEnd(x);

            TextCopy.ClipboardService.SetText(ss);

            //ss = "\r\n" + ss;
            //ss = ss.Replace("\r\n", "<br>");

            ss = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

            ViewBag.Result = ss;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult TextToColumn2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextToColumn2(IFormCollection f)
        {
            if (string.IsNullOrEmpty(f["Chuoi"].ToString()))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.TextToColumn2();
            }

            if (string.IsNullOrEmpty(f["Number"].ToString()))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.TextToColumn2();
            }

            string chuoi = f["Chuoi"].ToString();
            int n = int.Parse(f["Number"].ToString());

            string[] s = Regex.Split(chuoi, "\r\n");

            if (s.Length % n != 0)
            {
                ViewBag.KetQua = "Đã xảy ra lỗi, mời bạn kiểm tra dữ liệu và thử lại sau!";
                return this.TextToColumn2();
            }

            int nn = s.Length / n;

            string ss = "";
            for (int i = 0; i < n; i++)
            {
                int dem = 0;
                for (int j = i; dem < nn; j = j + n)
                {
                    ss += s[j] + "\t";
                    dem++;
                }
                char[] xx = { '\t' };
                ss = ss.TrimEnd(xx);
                ss += "\r\n";
            }

            char[] x = { '\r','\n' };
            ss = ss.TrimEnd(x);

            TextCopy.ClipboardService.SetText(ss);


            //ss = "\r\n" + ss;
            //ss = ss.Replace("\r\n", "<br>");

            ss = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

            ViewBag.Result = ss;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        [HttpGet]
        public ActionResult ReadNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadNumber(IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.ReadNumber();
            }

            string[] number = Regex.Split(chuoi, "\r\n");
            string result = "";
            for (int i = 0; i < number.Length; i++)
            {
                if (long.Parse(number[i]) < 0 || number[i].Length > 12)
                {
                    ViewBag.KetQua = "Lỗi! Các số không được âm và có hơn 12 chữ số...";
                    return this.ReadNumber();
                }

                if (long.Parse(number[i]) == 0)
                    result += "0 : Không\r\n";
                else
                    result +=  number[i]+" : "+Models.ReadNumber.hienthicachdocso(long.Parse(number[i])) + "\r\n";
            }

            TextCopy.ClipboardService.SetText(result);

            result = "\r\n" + result;
            result = result.Replace("\r\n", "<br>");

            result = "<p style=\"color:blue\"" + result + "</p>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        [HttpGet]
        public ActionResult TextConvertX()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextConvertX (IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();
            string start = f["Start"].ToString();
            string end = f["End"].ToString();

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.TextConvertX();
            }


            String[] sx = Regex.Split(chuoi, "\r\n");
            String noidung = "";
            for (int i = 0; i < sx.Length; i++)
            {
                noidung += start + sx[i] + end + "\r\n";
            }

            TextCopy.ClipboardService.SetText(noidung);

            //noidung = "\r\n" + noidung;
            //noidung= noidung.Replace("\r\n", "<br>").Replace(" ","&nbsp;");

            noidung = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + noidung + "</textarea>";

            ViewBag.Result = noidung;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult CSDL_MainKey()
        {
            return View();
        }

        int n;
        DS[] x = new DS[100];

        BD y = new BD();

        void nhapdebai(string chuoi)
        {
            String[] s1 = chuoi.Split(',');
            for (int i = 0; i < s1.Length; i++)
            {
                String[] s2 = s1[i].Split('>');
                y.de1[i] = s2[0].Trim();
                y.de2[i] = s2[1].Trim();
            }
            n = s1.Length;
        }

        void sapxep(char[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                char x = a[i];
                int j = i - 1;
                while (j >= 0 && a[j] > x)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = x;
            }
            y.tt = new String(a);
        }

        int KT_trung(char[] a, char x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (x == a[i])
                    return 0;
            }
            return 1;
        }

        int KT_TRUNG(String a, char x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (x == a.ToCharArray()[i])
                {
                    return 0;
                }
            }
            return 1;
        }

        void thuoctinh1()
        {
            int k = 0;
            char[] x = new char[100];
            for (int i = 0; i < 100; i++)
                x[i] = ' ';

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < y.de1[i].Length; j++)
                {
                    if (KT_trung(x, y.de1[i][j]) == 1)
                    {
                        x[k] = y.de1[i][j];
                        k++;
                    }
                }
            }

            y.tt = new String(x).Trim();

            //------------------------------------------------------

            int kK = y.tt.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < y.de2[i].Length; j++)
                {
                    if (KT_trung(x, y.de2[i][j]) == 1)
                    {
                        x[kK] = y.de2[i][j];
                        kK++;
                    }
                }
            }
            y.tt = new String(x).Trim();
            sapxep(y.tt.ToCharArray());
        }



        void khoitaonguon()
        {
            int k = 0;
            char[] xx = new char[100];

            for (int i = 0; i < 100; i++)
                xx[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                int flag = 0;
                for (int j = 0; j < n; j++)
                {
                    char[] x = y.de2[j].ToCharArray();
                    if (KT_trung(x, y.tt[i]) == 0)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    xx[k] = y.tt[i];
                    k++;
                }
            }
            y.nguon = new String(xx).Trim();

        }

        void khoitaodich()
        {
            int k = 0;
            char[] xx = new char[100];

            for (int i = 0; i < 100; i++)
                xx[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                int flag = 0;
                for (int j = 0; j < n; j++)
                {
                    char[] x = y.de1[j].ToCharArray();
                    if (KT_trung(x, y.tt[i]) == 0)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    xx[k] = y.tt[i];
                    k++;
                }
            }
            y.dich = new String(xx).Trim();
        }

        void khoitaotrunggian()
        {
            int k = 0;
            char[] x = new char[100];

            for (int i = 0; i < 100; i++)
                x[i] = ' ';

            for (int i = 0; i < y.tt.Length; i++)
            {
                if (KT_trung(y.nguon.ToCharArray(), y.tt[i]) == 0
                || KT_trung(y.dich.ToCharArray(), y.tt[i]) == 0)
                    continue;

                x[k] = y.tt[i];
                k++;
            }
            y.trunggian = new String(x).Trim();
        }

        void nhapbangchantri(DS[] x, int[][] a)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());

            char[] ku = new char[100];

            for (int i = 0; i < 100; i++)
                ku[i] = ' ';

            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < y.trunggian.Length; j++)
                {
                    ku[j] = (char)(a[i][j] + '0');
                }
                x[i].b = new String(ku).Trim();
            }
        }

        void lapbangchantri(DS[] x, int nn)
        {
            double ss = 2 * Math.Pow(2, nn - 1);
            int s = int.Parse(ss.ToString());
            int[][] a = new int[100][];

            for (int i = 0; i < 100; i++)
                a[i] = new int[100];

            for (int i = 0; i < nn; i++)
            {
                int d = 0, k = 0;
                for (int j = 0; j < s; j++)
                {
                    double dd = Math.Pow(2, nn - 1 - i);
                    int dx = int.Parse(dd.ToString());
                    if (d == dx)
                    {
                        if (k == 0)
                            k++;
                        else
                            k--;
                        d = 0;
                    }
                    a[j][i] = k;
                    d++;
                }
            }

            nhapbangchantri(x, a);

        }

        void Play_On1(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());

            for (int i = 0; i < s; i++)
            {
                char[] xx = new char[100];

                for (int II = 0; II < 100; II++)
                    xx[II] = ' ';

                int k = 0;

                for (int j = 0; j < y.trunggian.Length; j++)
                {
                    if (x[i].b[j] == '1')
                    {
                        xx[k] = y.trunggian[j];
                        k++;
                    }
                }
                x[i].xi = new String(xx).Trim();
                // MessageBox.Show(x[i].xi+"-");
            }

        }

        void Play_On2(BD y)
        {
            double sx = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(sx.ToString());
            for (int i = 0; i < s; i++)
            {
                x[i].x1 = y.nguon + x[i].xi;
                x[i].xf = x[i].x1;
            }
        }

        void Play_On3(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);

            int s = int.Parse(ss.ToString());
            for (int a = 0; a < s; a++)
            {
                String ou = x[a].xf;
                for (int i = 0; i < n; i++)
                {
                    int d = 0;
                    for (int j = 0; j < y.de1[i].Length; j++)
                    {
                        if (KT_TRUNG(ou, y.de1[i].ToCharArray()[j]) == 0)
                            d++;
                    }
                    if (d == y.de1[i].Length)
                    {
                        for (int j = 0; j < y.de2[i].Length; j++)
                        {
                            if (KT_TRUNG(ou, y.de2[i][j]) == 1)
                            {
                                ou += y.de2[i][j];
                                i = -1;
                                break;
                            }
                        }
                    }
                    x[a].xf = ou;
                }
                //MessageBox.Show(x[a].xf);
            }
        }

        string xuatDS(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            String noidung = "";
            int s = int.Parse(ss.ToString());
            for (int i = 0; i < s; i++)
            {
                if (x[i].xi.Length == 0)
                    noidung += "\r\n" + x[i].b + "\t\tNULL\t\t" + x[i].x1 + "\t\t" + x[i].xf;
                else
                    noidung += "\r\n" + x[i].b + "\t\t" + x[i].xi + "\t\t" + x[i].x1 + "\t\t" + x[i].xf;
            }
            return noidung;
        }

        int Test(char[] x, char[] y)
        {
            int d = 0;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    if (x[i] == y[j])
                        d++;
                }
            }
            if (d == x.Length)
                return 0;
            return 1;
        }

        int TEST(int k, char[] y)
        {
            for (int i = 0; i < k; i++)
            {
                if (Test(x[i].khoa.ToCharArray(), y) == 0)
                    return 0;
            }
            return 1;
        }

        int k = 0;

        void sieukhoa(BD y)
        {
            double ss = 2 * Math.Pow(2, y.trunggian.Length - 1);
            int s = int.Parse(ss.ToString());
            int m = y.tt.Length;
            for (int i = 0; i < s; i++)
            {
                if (x[i].xf.Length == m)
                {
                    int d = 0;
                    for (int a = 0; a < k; a++)
                    {
                        /*
                        for (int u = 0; u < x[i].x1.Length; u++)
                        {
                            //if (x[a].khoa[u] == x[i].x1[u])
                               // d++;
                        }
                        */
                    }
                    if (d != y.tt.Length)
                    {
                        if (TEST(k, x[i].x1.ToCharArray()) == 1)
                        {
                            x[k].khoa += x[k].khoa + x[i].x1;
                            k++;
                        }

                    }
                }
            }
        }

        string xuatsieukhoa(int k)
        {
            String s = "";
            for (int i = 0; i < k; i++)
                s += x[i].khoa + " , ";
            return s;
        }

        [HttpPost]
        public ActionResult CSDL_MainKey (IFormCollection f)
        {
            string chuoi = f["Chuoi"].ToString();

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                return this.CSDL_MainKey();
            }

            chuoi = chuoi.ToUpper();

            for (int i = 0; i < 100; i++)
                x[i] = new DS();

            nhapdebai(chuoi);

            string s = "ĐỀ BÀI : "+chuoi.Replace(",",", ");
            s = s.Replace(">", " -> ");

            thuoctinh1();
            //thuoctinh2();
            s += "\r\n\r\n- Có " + y.tt.Length + " thuộc tính tham gia : " + y.tt + "\r\n";

            khoitaonguon();
            if (y.nguon.Length == 0)
                s += "\r\n+ Các thuộc tính nguồn : NULL\n";
            else
                s += "\r\n+ Các thuộc tính nguồn : " + y.nguon + "\r\n";

            khoitaodich();
            if (y.dich.Length == 0)
                s += "\r\n+ Các thuộc tính đích : NULL\r\n";
            else
                s += "\r\n+ Các thuộc tính đích : " + y.dich + "\r\n";

            khoitaotrunggian();
            if (y.trunggian.Length == 0)
            {
                s += "\r\n+ Các thuộc tính trung gian : NULL\r\n";
                s += "\r\n==> Không có kết quả Key phù hợp hoặc có thể có một Key duy nhất là khoá từ tập nguồn : " + y.nguon + " !";
                
                TextCopy.ClipboardService.SetText(s);

                //s = "\r\n" + s.Replace("\r\n", "<br>");

                s = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

                ViewBag.Result = s;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

                return View();
            }
            else
            {
                s += "\n+ Các thuộc tính trung gian : " + y.trunggian + "\n";
            }


            lapbangchantri(x, y.trunggian.Length);


            Play_On1(y);

            Play_On2(y);
            Play_On3(y);


            s += "\r\n\r\n --> BẢNG KẾT QUẢ (SO SÁNH) :\r\n\r\n";

           s  += xuatDS(y);

            sieukhoa(y);
            s += "\r\n\r\n==> Kết quả các siêu khoá bé nhất là : "+xuatsieukhoa(k);

            TextCopy.ClipboardService.SetText(s);



            s = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

            ViewBag.Result = s;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

    }
}