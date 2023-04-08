using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult TracNghiem_Multiple(int? sl)
        {
            if (sl == null)
                ViewBag.SL = 0;

            ViewBag.SL = sl;

            return View();
        }

        public ActionResult TracNghiemX_Multiple()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TracNghiem_Multiple(IFormCollection f, List<IFormFile> txtFile)
        {
            int sl = int.Parse(f["txtSoLuong"].ToString());
            ViewBag.SL = sl;

            string txtSoCau = f["txtSoCau"].ToString();
            string txtTime = f["txtTime"].ToString();
            string txtMon = f["txtMon"].ToString();

            if (string.IsNullOrEmpty(txtMon))
            {
                ViewData["Loi"] = "Không được bỏ trống trường này";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            if (string.IsNullOrEmpty(txtSoCau))
            {
                ViewData["Loi2"] = "Không được bỏ trống trường này";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            if (string.IsNullOrEmpty(txtTime))
            {
                ViewData["Loi3"] = "Không được bỏ trống trường này";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            int time = int.Parse(txtTime);
            if (time < 1 || time > 1200)
            {
                ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            if (txtFile.Count() < sl)
            {
                ViewData["Loi1-" + txtFile.Count()] = "Mời bạn chọn file TXT trắc nghiệm cho chương " + (txtFile.Count() + 1) + "...";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }


            int n9_S = 0;

            //------

            TracNghiem[] tn = new TracNghiem[sl];

            for (int h = 0; h < txtFile.Count(); h++)
            {
                tn[h] = new TracNghiem();

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", Path.GetFileName(txtFile[h].FileName));

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtFile[h].CopyTo(fileStream);

                }

                String ND_file = docfile(path);

                FileInfo fx = new FileInfo(path);
                fx.Delete();

                if (ND_file.Length == 0)
                {
                    ViewData["Loi1-"+h] = "Bài kiểm tra hay file văn bản chương "+(h+1)+" của bạn hiện chưa có nội dung nào!";
                    return this.TracNghiem_Multiple(ViewBag.SL);
                }

                String[] split = { "\n#\n" };
                String[] t1 = ND_file.Split(split, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < t1.Length; i++)
                {
                    String[] t2 = t1[i].Split('\n');
                    if (t2.Length != 6)
                    {

                        string err = "WRONG INDEX QUESTION [CHƯƠNG "+(i+1)+"] : " + t2[0] + "";
                        //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";
                        ViewData["Loi1-"+h] = err;
                        return this.TracNghiem_Multiple(ViewBag.SL);
                    }

                    char[] t2_x = t2[5].ToCharArray();
                    if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                    {
                        string err = "WRONG INDEX QUESTION [CHƯƠNG "+(i+1)+"] : " + t2[0] + "";
                        //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                        ViewData["Loi1-"+h] = err;
                        return this.TracNghiem_Multiple(ViewBag.SL);
                    }
                }

                //-------------------

                for (int i = 0; i < t1.Length; i++)
                {
                    String[] t2 = t1[i].Split('\n');
                    int flag = 0;
                    String DA = t2[t2.Length - 1].Replace("[", "");
                    DA = DA.Replace("]", "");
                    for (int j = t2.Length - 2; j > 0; j--)
                    {
                        if (DA.CompareTo(t2[j]) == 0)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        //MessageBox.Show("Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi.\nXin lỗi vì sự bất tiện này! ]");
                        ViewData["Loi1-"+h] = "WRONG INDEX ANSWER OF QUESTION [CHƯƠNG " + (h + 1) + "] : " + t2[0] + "";
                        return this.TracNghiem_Multiple(ViewBag.SL);
                    }
                }

                int[] chuaxet_ch;
                int[][] chuaxet_da;

                int n9 = t1.Length;
                n9_S += n9;

                tn[h].ch = new String[t1.Length];
                tn[h].a = new String[t1.Length];
                tn[h].b = new String[t1.Length];
                tn[h].c = new String[t1.Length];
                tn[h].d = new String[t1.Length];
                tn[h].dung = new String[t1.Length];

                chuaxet_ch = new int[t1.Length];

                chuaxet_da = new int[t1.Length][];

                for (int i = 0; i < t1.Length; i++)
                    chuaxet_da[i] = new int[5];

                //=======

                int dem = 0;
                while (true)
                {
                    if (dem == t1.Length)
                        break;


                    Random r = new Random();
                    double x = r.Next(0, t1.Length);

                    if (chuaxet_ch[int.Parse(x.ToString())] == 1)
                        continue;

                    chuaxet_ch[int.Parse(x.ToString())] = 1;
                    int i = int.Parse(x.ToString());
                    String[] t2 = t1[i].Split('\n');

                    char[] CH = t2[0].ToCharArray();

                    if (CH[0] == '$')
                    {
                        t2[0].Remove(0, 1);
                        tn[h].ch[dem] = t2[0].Replace("$", "");
                        tn[h].a[dem] = t2[1];
                        tn[h].b[dem] = t2[2];
                        tn[h].c[dem] = t2[3];
                        tn[h].d[dem] = t2[4];
                        String DAx = t2[5].Replace("[", "");
                        DAx = DAx.Replace("]", "");
                        tn[h].dung[dem] = DAx;
                    }
                    else
                    {
                        int aa, bb, cc, dd;

                        tn[h].ch[dem] = t2[0];

                        do
                        {
                            aa = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(aa.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(aa.ToString())] = 1;


                        tn[h].a[dem] = t2[aa];

                        do
                        {
                            bb = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(bb.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(bb.ToString())] = 1;


                        tn[h].b[dem] = t2[bb];

                        do
                        {
                            cc = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(cc.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(cc.ToString())] = 1;

                        tn[h].c[dem] = t2[cc];

                        do
                        {
                            dd = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(dd.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(dd.ToString())] = 1;

                        tn[h].d[dem] = t2[dd];
                        String DA = t2[5].Replace("[", "");
                        DA = DA.Replace("]", "");
                        tn[h].dung[dem] = DA;
                    }
                    dem++;
                }
                tn[h].tongsocau = n9;
            }

            TracNghiem tnX = new TracNghiem();
            tnX.ch = new String[int.Parse(txtSoCau)];
            tnX.a = new String[int.Parse(txtSoCau)];
            tnX.b = new String[int.Parse(txtSoCau)];
            tnX.c = new String[int.Parse(txtSoCau)];
            tnX.d = new String[int.Parse(txtSoCau)];
            tnX.dung = new String[int.Parse(txtSoCau)];

            tnX.tongsocau = n9_S;
            tnX.gioihancau = int.Parse(txtSoCau);


            if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9_S)
            {
                ViewData["Loi2"] = "Giới hạn số câu bạn cần làm của vượt quá số lượng tất cả mà đang hiện có...";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            int[][] chuaxetX = new int[sl][];

            for (int i =0; i< sl;i++)
            {
                chuaxetX[i] = new int[tn[i].tongsocau];
                for (int j = 0; j < chuaxetX[i].Length; j++)
                {
                    chuaxetX[i][j] = 0;
                }
            }


            for (int i =0;i <tnX.gioihancau;i++)
            {
                Random r = new Random();
                int chuong;
                int soluong;
                do
                {
                    chuong = r.Next(0, sl);
                    soluong = r.Next(0, tn[chuong].tongsocau);
                }
                while (chuaxetX[chuong][soluong] == 1);

                chuaxetX[chuong][soluong] = 1;

                tnX.ch[i] = tn[chuong].ch[soluong];
                tnX.a[i] = tn[chuong].a[soluong];
                tnX.b[i] = tn[chuong].b[soluong];
                tnX.c[i] = tn[chuong].c[soluong];
                tnX.d[i] = tn[chuong].d[soluong];
                tnX.dung[i] = tn[chuong].dung[soluong];
            }

            tnX.timelambai = int.Parse(txtTime);
            tnX.tenmon = txtMon;

           ViewBag.TimeLamBai = tnX.timelambai;

            HttpContext.Session.SetObject("TracNghiem", tnX);
            ViewBag.KetQuaDung = "";

            return View("PlayTracNghiem", tnX);
        }

        [HttpPost]
        public ActionResult TracNghiemX_Multiple(IFormCollection f, List<IFormFile> txtFile)
        {
            int sl = txtFile.Count();
            string txtSoCau = f["txtSoCau"].ToString();
            string txtTime = f["txtTime"].ToString();
            string txtMon = f["txtMon"].ToString();

            if (string.IsNullOrEmpty(txtMon))
            {
                ViewData["Loi"] = "Không được bỏ trống trường này";
                return this.TracNghiemX_Multiple();
            }

            if (string.IsNullOrEmpty(txtSoCau))
            {
                ViewData["Loi2"] = "Không được bỏ trống trường này";
                return this.TracNghiemX_Multiple();
            }

            if (string.IsNullOrEmpty(txtTime))
            {
                ViewData["Loi3"] = "Không được bỏ trống trường này";
                return this.TracNghiemX_Multiple();
            }

            int time = int.Parse(txtTime);
            if (time < 1 || time > 1200)
            {
                ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                return this.TracNghiemX_Multiple();
            }

            if (txtFile.Count() <= 0)
            {
                ViewData["Loi1"] = "Mời bạn chọn file TXT trắc nghiệm (có thể chọn nhiều file thể hiện một môn học trắc nghiệm có nhiều chương/mục/phần/bài)...";
                return this.TracNghiemX_Multiple();
            }


            int n9_S = 0;

            //------

            TracNghiem[] tn = new TracNghiem[sl];

            for (int h = 0; h < txtFile.Count(); h++)
            {
                tn[h] = new TracNghiem();

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", Path.GetFileName(txtFile[h].FileName));

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtFile[h].CopyTo(fileStream);

                }

                String ND_file = docfile(path);

                FileInfo fx = new FileInfo(path);
                fx.Delete();

                if (ND_file.Length == 0)
                {
                    ViewData["Loi1"] = "Bài kiểm tra hay file văn bản chương (hoặc file của bạn tải lên thứ) " + (h + 1) + " của bạn hiện chưa có nội dung nào!";
                    return this.TracNghiemX_Multiple();
                }

                String[] split = { "\n#\n" };
                String[] t1 = ND_file.Split(split, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < t1.Length; i++)
                {
                    String[] t2 = t1[i].Split('\n');
                    if (t2.Length != 6)
                    {

                        string err = "WRONG INDEX QUESTION [CHƯƠNG/FILE " + (i + 1) + "] : " + t2[0] + "";
                        //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";
                        ViewData["Loi1"] = err;
                        return this.TracNghiemX_Multiple();
                    }

                    char[] t2_x = t2[5].ToCharArray();
                    if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                    {
                        string err = "WRONG INDEX QUESTION [CHƯƠNG/FILE " + (i + 1) + "] : " + t2[0] + "";
                        //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                        ViewData["Loi1"] = err;
                        return this.TracNghiemX_Multiple();
                    }
                }

                //-------------------

                for (int i = 0; i < t1.Length; i++)
                {
                    String[] t2 = t1[i].Split('\n');
                    int flag = 0;
                    String DA = t2[t2.Length - 1].Replace("[", "");
                    DA = DA.Replace("]", "");
                    for (int j = t2.Length - 2; j > 0; j--)
                    {
                        if (DA.CompareTo(t2[j]) == 0)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        //MessageBox.Show("Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi.\nXin lỗi vì sự bất tiện này! ]");
                        ViewData["Loi1"] = "WRONG INDEX ANSWER OF QUESTION [CHƯƠNG/FILE " + (h + 1) + "] : " + t2[0] + "";
                        return this.TracNghiem_Multiple(ViewBag.SL);
                    }
                }

                int[] chuaxet_ch;
                int[][] chuaxet_da;

                int n9 = t1.Length;
                n9_S += n9;

                tn[h].ch = new String[t1.Length];
                tn[h].a = new String[t1.Length];
                tn[h].b = new String[t1.Length];
                tn[h].c = new String[t1.Length];
                tn[h].d = new String[t1.Length];
                tn[h].dung = new String[t1.Length];

                chuaxet_ch = new int[t1.Length];

                chuaxet_da = new int[t1.Length][];

                for (int i = 0; i < t1.Length; i++)
                    chuaxet_da[i] = new int[5];

                //=======

                int dem = 0;
                while (true)
                {
                    if (dem == t1.Length)
                        break;


                    Random r = new Random();
                    double x = r.Next(0, t1.Length);

                    if (chuaxet_ch[int.Parse(x.ToString())] == 1)
                        continue;

                    chuaxet_ch[int.Parse(x.ToString())] = 1;
                    int i = int.Parse(x.ToString());
                    String[] t2 = t1[i].Split('\n');

                    char[] CH = t2[0].ToCharArray();

                    if (CH[0] == '$')
                    {
                        t2[0].Remove(0, 1);
                        tn[h].ch[dem] = t2[0].Replace("$", "");
                        tn[h].a[dem] = t2[1];
                        tn[h].b[dem] = t2[2];
                        tn[h].c[dem] = t2[3];
                        tn[h].d[dem] = t2[4];
                        String DAx = t2[5].Replace("[", "");
                        DAx = DAx.Replace("]", "");
                        tn[h].dung[dem] = DAx;
                    }
                    else
                    {
                        int aa, bb, cc, dd;

                        tn[h].ch[dem] = t2[0];

                        do
                        {
                            aa = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(aa.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(aa.ToString())] = 1;


                        tn[h].a[dem] = t2[aa];

                        do
                        {
                            bb = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(bb.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(bb.ToString())] = 1;


                        tn[h].b[dem] = t2[bb];

                        do
                        {
                            cc = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(cc.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(cc.ToString())] = 1;

                        tn[h].c[dem] = t2[cc];

                        do
                        {
                            dd = r.Next(1, 5);
                        }
                        while (chuaxet_da[dem][int.Parse(dd.ToString())] == 1);
                        chuaxet_da[dem][int.Parse(dd.ToString())] = 1;

                        tn[h].d[dem] = t2[dd];
                        String DA = t2[5].Replace("[", "");
                        DA = DA.Replace("]", "");
                        tn[h].dung[dem] = DA;
                    }
                    dem++;
                }
                tn[h].tongsocau = n9;
            }

            TracNghiem tnX = new TracNghiem();
            tnX.ch = new String[int.Parse(txtSoCau)];
            tnX.a = new String[int.Parse(txtSoCau)];
            tnX.b = new String[int.Parse(txtSoCau)];
            tnX.c = new String[int.Parse(txtSoCau)];
            tnX.d = new String[int.Parse(txtSoCau)];
            tnX.dung = new String[int.Parse(txtSoCau)];

            tnX.tongsocau = n9_S;
            tnX.gioihancau = int.Parse(txtSoCau);


            if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9_S)
            {
                ViewData["Loi2"] = "Giới hạn số câu bạn cần làm của vượt quá số lượng tất cả mà đang hiện có...";
                return this.TracNghiem_Multiple(ViewBag.SL);
            }

            int[][] chuaxetX = new int[sl][];

            for (int i = 0; i < sl; i++)
            {
                chuaxetX[i] = new int[tn[i].tongsocau];
                for (int j = 0; j < chuaxetX[i].Length; j++)
                {
                    chuaxetX[i][j] = 0;
                }
            }


            for (int i = 0; i < tnX.gioihancau; i++)
            {
                Random r = new Random();
                int chuong;
                int soluong;
                do
                {
                    chuong = r.Next(0, sl);
                    soluong = r.Next(0, tn[chuong].tongsocau);
                }
                while (chuaxetX[chuong][soluong] == 1);

                chuaxetX[chuong][soluong] = 1;

                tnX.ch[i] = tn[chuong].ch[soluong];
                tnX.a[i] = tn[chuong].a[soluong];
                tnX.b[i] = tn[chuong].b[soluong];
                tnX.c[i] = tn[chuong].c[soluong];
                tnX.d[i] = tn[chuong].d[soluong];
                tnX.dung[i] = tn[chuong].dung[soluong];
            }

            tnX.timelambai = int.Parse(txtTime);
            tnX.tenmon = txtMon;

            ViewBag.TimeLamBai = tnX.timelambai;

            HttpContext.Session.SetObject("TracNghiem", tnX);
            ViewBag.KetQuaDung = "";

            return View("PlayTracNghiem", tnX);
        }

        public ActionResult HD_Web_AspNet()
        {
            return View();
        }
    }
}
