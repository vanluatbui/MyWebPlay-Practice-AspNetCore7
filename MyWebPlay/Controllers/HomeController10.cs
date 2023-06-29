using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using Newtonsoft.Json.Linq;
using System.Formats.Tar;
using System.Globalization;
using System.IO;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult TracNghiem()
        {
            HttpContext.Session.Remove("TracNghiem");

            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                f.Delete();
            }

            return View();
        }

        private String docfile(String filename)
        {
            string[] a = System.IO.File.ReadAllLines(filename);
            String s = "";
            for (int i = 0; i < a.Length; ++i)
            {
                s = s + a[i];
                if (i < a.Length - 1)
                    s = s + "\n";
            }
            return s;
        }

        [HttpPost]
        public ActionResult TracNghiem(IFormCollection f, IFormFile txtFile)
        {
            string txtSoCau = f["txtSoCau"].ToString();
            string txtTime = f["txtTime"].ToString();
            string txtMon = f["txtMon"].ToString();
           

            if (txtFile == null || txtFile.FileName.Length == 0)
            {
                ViewData["Loi1"] = "Mời bạn chọn file TXT trắc nghiệm...";
                return this.TracNghiem();
            }

            if (string.IsNullOrEmpty(txtMon))
            {
                ViewData["Loi"] = "Không được bỏ trống trường này";
                return this.TracNghiem();
            }

            if (string.IsNullOrEmpty(txtSoCau) )
            {
                ViewData["Loi2"] = "Không được bỏ trống trường này";
                return this.TracNghiem();
            }

            if (string.IsNullOrEmpty(txtTime))
            {
                ViewData["Loi3"] = "Không được bỏ trống trường này";
                return this.TracNghiem();
            }

            int time = int.Parse(txtTime);
            if (time < 1 || time > 1200)
            {
                ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                return this.TracNghiem();
            }

            //------

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", Path.GetFileName(txtFile.FileName));

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtFile.CopyTo(fileStream);

            }

            String ND_file = docfile(path);

            FileInfo fx = new FileInfo(path);
            fx.Delete();

            if (ND_file.Length == 0)
            {
                ViewData["Loi1"] = "Bài kiểm tra hay file văn bản của bạn hiện chưa có nội dung nào!";
                return this.TracNghiem();
            }

            String[] split = { "\n#\n" };
            String[] t1 = ND_file.Split(split, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < t1.Length; i++)
            {
                String[] t2 = t1[i].Split('\n');
                if (t2.Length != 6)
                {

                    string err = "WRONG INDEX QUESTION : " + t2[0] + "";
                    //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";
                    ViewData["Loi1"] = err;
                    return this.TracNghiem();
                }

                char[] t2_x = t2[5].ToCharArray();
                if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                {
                    string err = "WRONG INDEX QUESTION : " + t2[0] + "";
                    //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                    ViewData["Loi1"] = err;
                    return this.TracNghiem();
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
                     ViewData["Loi1"] = "WRONG INDEX ANSWER OF QUESTION : " + t2[0] + "";
                    return this.TracNghiem();
                }
            }

            int[] chuaxet_ch;
            int[][] chuaxet_da;

            int n9 = t1.Length;

            if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9)
            {
                txtSoCau = n9.ToString();
            }

            TracNghiem tn = new TracNghiem();

            tn.ch = new String[t1.Length];
            tn.a = new String[t1.Length];
            tn.b = new String[t1.Length];
            tn.c = new String[t1.Length];
            tn.d = new String[t1.Length];
            tn.dung = new String[t1.Length];

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
                    tn.ch[dem] = t2[0].Replace("$", "");
                    tn.a[dem] = t2[1];
                    tn.b[dem] = t2[2];
                    tn.c[dem] = t2[3];
                    tn.d[dem] = t2[4];
                    String DAx = t2[5].Replace("[", "");
                    DAx = DAx.Replace("]", "");
                    tn.dung[dem] = DAx;
                }
                else
                {
                    int aa, bb, cc, dd;

                    tn.ch[dem] = t2[0];

                    do
                    {
                        aa = r.Next(1, 5);
                    }
                    while (chuaxet_da[dem][int.Parse(aa.ToString())] == 1);
                    chuaxet_da[dem][int.Parse(aa.ToString())] = 1;


                    tn.a[dem] = t2[aa];

                    do
                    {
                        bb = r.Next(1, 5);
                    }
                    while (chuaxet_da[dem][int.Parse(bb.ToString())] == 1);
                    chuaxet_da[dem][int.Parse(bb.ToString())] = 1;


                    tn.b[dem] = t2[bb];

                    do
                    {
                        cc = r.Next(1, 5);
                    }
                    while (chuaxet_da[dem][int.Parse(cc.ToString())] == 1);
                    chuaxet_da[dem][int.Parse(cc.ToString())] = 1;

                    tn.c[dem] = t2[cc];

                    do
                    {
                        dd = r.Next(1, 5);
                    }
                    while (chuaxet_da[dem][int.Parse(dd.ToString())] == 1);
                    chuaxet_da[dem][int.Parse(dd.ToString())] = 1;

                    tn.d[dem] = t2[dd];
                    String DA = t2[5].Replace("[", "");
                    DA = DA.Replace("]", "");
                    tn.dung[dem] = DA;
                }
                dem++;
            }

            tn.tongsocau = n9;
           tn.gioihancau = int.Parse(txtSoCau);
            tn.timelambai = int.Parse(txtTime);
            tn.tenmon = txtMon;

           ViewBag.TimeLamBai = tn.timelambai;

            HttpContext.Session.SetObject("TracNghiem", tn);
            ViewBag.KetQuaDung = "";

            ViewBag.TongSoCau = tn.tongsocau;
            ViewBag.GioiHanCau = tn.gioihancau;
            ViewBag.TimeLamBaiX = tn.timelambai;
            ViewBag.TenMon = tn.tenmon;

            ViewBag.CauHoi = String.Join("\n", tn.ch);
            ViewBag.A = String.Join("\n", tn.a);
            ViewBag.B = String.Join("\n", tn.b);
            ViewBag.C = String.Join("\n", tn.c);
            ViewBag.D = String.Join("\n", tn.d);
            ViewBag.Dung = String.Join("\n", tn.dung);

            return View("PlayTracNghiem", tn);
        }

        public ActionResult PlayTracNghiem(TracNghiem tn)
        {
            if (ViewBag.ND_File != null)
            {
                var ND_File = ViewBag.ND_File;
                ViewBag.ND_File = ND_File;
            }
            else
                ViewBag.ND_File = null;

            HttpContext.Session.SetObject("TracNghiem", tn);

            return View(tn);
        }

        [HttpPost, ActionName("PlayTracNghiem")]
        public ActionResult TracNghiemPlay(IFormCollection f)
        {
            TracNghiem tn;

            if (HttpContext.Session.GetObject<TracNghiem>("TracNghiem") == null)
            {
                tn = new TracNghiem();
                tn.tongsocau = int.Parse(f["TongSoCau"].ToString());
                tn.gioihancau = int.Parse(f["GioiHanCau"].ToString());
                tn.timelambai = int.Parse(f["TimeLamBai"].ToString());
                tn.tenmon = f["TenMon"].ToString();

                tn.ch = f["CauHoi"].ToString().Split("\n");
                tn.a = f["A"].ToString().Split("\n");
                tn.b = f["B"].ToString().Split("\n");
                tn.c = f["C"].ToString().Split("\n");
                tn.d = f["D"].ToString().Split("\n");
                tn.dung = f["Dung"].ToString().Split("\n");
            }
            else
                tn = HttpContext.Session.GetObject<TracNghiem>("TracNghiem");

            if (tn == null)
                return RedirectToAction("TracNghiemX_Multiple");

            if (f["File_ND"] == "")
            {
                int dung = 0;
                int sai = 0;
                int chualam = 0;

                for (int i = 0; i < tn.gioihancau; i++)
                {
                    string da = f["dapan-" + i].ToString();

                    int flag = 0;
                    if (da == "")
                    {
                        chualam++;

                        ViewData["KetQua-" + i] = "<h2 style=\"color:orange\">CHƯA TRẢ LỜI</h2>";
                    }
                    else if (da == "A")
                    {
                        ViewData["dapandachon-" + i] = "<b><span style=\"color:deeppink\">Đáp án bạn đã chọn</span> : A. " + tn.a[i] + "</b>";

                        if (tn.dung[i] == tn.a[i])
                        {
                            dung++;
                            flag = 1;
                            ViewData["KetQua-" + i] = "<h2 style=\"color:green\">ĐÚNG RỒI</h2>";
                        }
                        else
                        {
                            ViewData["KetQua-" + i] = "<h2 style=\"color:red\">SAI RỒI</h2>";
                            sai++;
                        }
                    }
                    else if (da == "B")
                    {
                        ViewData["dapandachon-" + i] = "<b><span style=\"color:deeppink\">Đáp án bạn đã chọn</span> : B. " + tn.b[i] + "</b>";

                        if (tn.dung[i] == tn.b[i])
                        {
                            dung++;
                            flag = 1;
                            ViewData["KetQua-" + i] = "<h2 style=\"color:green\">ĐÚNG RỒI</h2>";
                        }
                        else
                        {
                            ViewData["KetQua-" + i] = "<h2 style=\"color:red\">SAI RỒI</h2>";
                            sai++;
                        }
                    }
                    else if (da == "C")
                    {
                        ViewData["dapandachon-" + i] = "<b><span style=\"color:deeppink\">Đáp án bạn đã chọn</span> : C. " + tn.c[i] + "</b>";

                        if (tn.dung[i] == tn.c[i])
                        {
                            dung++;
                            flag = 1;
                            ViewData["KetQua-" + i] = "<h2 style=\"color:green\">ĐÚNG RỒI</h2>";
                        }
                        else
                        {
                            ViewData["KetQua-" + i] = "<h2 style=\"color:red\">SAI RỒI</h2>";
                            sai++;
                        }
                    }
                    else if (da == "D")
                    {
                        ViewData["dapandachon-" + i] = "<b><span style=\"color:deeppink\">Đáp án bạn đã chọn</span> : D. " + tn.d[i] + "</b>";

                        if (tn.dung[i] == tn.d[i])
                        {
                            dung++;
                            flag = 1;
                            ViewData["KetQua-" + i] = "<h2 style=\"color:green\">ĐÚNG RỒI</h2>";
                        }
                        else
                        {
                            ViewData["KetQua-" + i] = "<h2 style=\"color:red\">SAI RỒI</h2>";
                            sai++;
                        }

                    }
                    if (flag == 0)
                    {
                        if (tn.dung[i] == tn.a[i])
                            ViewData["dapandung-" + i] = "<b><span style=\"color:deeppink\">Đáp án đúng</span> : A. " + tn.a[i] + "</b>";
                        else if (tn.dung[i] == tn.b[i])
                            ViewData["dapandung-" + i] = "<b><span style=\"color:deeppink\">Đáp án đúng</span> : B. " + tn.b[i] + "</b>";
                        else if (tn.dung[i] == tn.c[i])
                            ViewData["dapandung-" + i] = "<b><span style=\"color:deeppink\">Đáp án đúng</span> : C. " + tn.c[i] + "</b>";
                        else if (tn.dung[i] == tn.d[i])
                            ViewData["dapandung-" + i] = "<b><span style=\"color:deeppink\">Đáp án đúng</span> : D. " + tn.d[i] + "</b>";
                    }
                }


                ViewBag.KetQuaDung = "Số câu đúng : " + dung;
                ViewBag.KetQuaSai = "Số câu sai : " + sai;
                ViewBag.KetQuaChuaLam = "Số câu chưa làm : " + chualam;

                double diem = ((double)10 / (double)tn.gioihancau) * dung;

                diem = Math.Round(diem, 1);

                ViewBag.KetQuaDiem = "Điểm Đánh Giá : " + diem + "/10";
                return View(tn);
            }
            else
            {
                var ND_File = f["File_ND"].ToString();
                ViewBag.ND_File = null;

                TempData["Socola"] = "hay";
                var flag = 0;

                for (int i = 0; i < tn.gioihancau; i++)
                {
                    string da = f["dapan-" + i].ToString();

                    if (da == "")
                    {
                        ND_File = ND_File.Replace("["+tn.dung[i]+"]", "[<?" + i + "?>]");
                        flag = 1;
                    }

                    if (da == "A")
                    {
                        ND_File = ND_File.Replace("<?" + i + "?>", tn.a[i]).Replace("[" + tn.dung[i] + "]", "["+tn.a[i]+"]");
                    }
                    else if (da == "B")
                    {
                        ND_File = ND_File.Replace("<?" + i + "?>", tn.b[i]).Replace("[" + tn.dung[i] + "]", "[" + tn.b[i] + "]");
                    }
                    else if (da == "C")
                    {
                        ND_File = ND_File.Replace("<?" + i + "?>", tn.c[i]).Replace("[" + tn.dung[i] + "]", "[" + tn.c[i] + "]");
                    }
                    else if (da == "D")
                    {
                        ND_File = ND_File.Replace("<?" + i + "?>", tn.d[i]).Replace("[" + tn.dung[i] + "]", "[" + tn.d[i] + "]");
                    }
                }

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string fi = Request.HttpContext.Connection.RemoteIpAddress + "_TracNghiem_" + xuxu + ".txt";
                fi = fi.Replace("\\", "");
                fi = fi.Replace("/", "");
                fi = fi.Replace(":", "");

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", fi);

                    System.IO.File.WriteAllText(path, ND_File);

                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;

                if (flag == 0)
                SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
              "mywebplay.savefile@gmail.com", "Save Temp Create/Update Trac Nghiem File In " + name, ND_File, "teinnkatajeqerfl");

                    ViewBag.KetQua = "<p style=\"color:blue\">Thành công, một file TXT trắc nghiệm của bạn đã được xử lý/cập nhật...</p><a href=\"/tracnghiem/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:yellow\" id=\"thoigian1\" class=\"thoigian1\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>Nếu file tải về của bạn bị lỗi hoặc chưa kịp tải về, hãy refresh/quay lại trang này và thử lại...<br><span style=\"color:aqua\">Mặc dù file này đã được thông qua một số xử lý, tuy nhiên nó vẫn có thể xảy ra lỗi và sai sót không mong muốn. Vì vậy tạm thời bạn cứ tải file này về, sử dụng file này để làm bài trắc nghiệm và hệ thống sẽ thông báo vị trí của câu hỏi đang bị nghi ngờ là lỗi, bạn hãy mở file này và Ctrl + F để tìm câu hỏi đó, quan sát xung quanh tương tự và tự chỉnh sửa file thủ công sao cho thích hợp nhé!<br></span></p>";
             
                return View();
            }
        }
     }
}
