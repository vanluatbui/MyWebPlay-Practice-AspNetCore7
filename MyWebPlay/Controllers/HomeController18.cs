using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult RemoveIpInWeb(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");
            System.IO.File.WriteAllText(path, noidung);

            TempData["Logout_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult RemoveIpConnectWeb(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");

            System.IO.File.WriteAllText(path, noidung);

            return RedirectToAction("Index");
        }

        public ActionResult LockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung = docfile(path);

            if (noidung.Contains(ip) == false)
            {
            noidung += ip + "##";
            System.IO.File.WriteAllText(path, noidung);
            }
            TempData["Lock_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult AcceptContinueUseWeb()
        {
            TempData["continue"] = "";

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
            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            if (HttpContext.Session.GetObject<string>("continueX") != "ok")
            {
                TempData["ok-continue-X"] = "no";
                return RedirectToAction("Index");
            }

            HttpContext.Session.Remove("continueX");

            return View();
        }

        public ActionResult UnlockThisClient(string ip)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung = docfile(path);

            noidung = noidung.Replace(ip + "##", "");
            System.IO.File.WriteAllText(path, noidung);
            TempData["Unlock_Message"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult ContinueUsedWebX (string? code)
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
            if (TempData["lock"].ToString() == "true")
                return RedirectToAction("LockedWeb");

            var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
            var noidung1 = docfile(path1);

            var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
            var noidung2 = docfile(path2);

            string IP = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            else
                IP = HttpContext.Session.GetString("userIP");

            if ((noidung1.Contains(IP) == true && noidung2.Contains(IP) == false) && code == "1234567890qwertyuiopasdfghjklzxcvbnm")
            {
                HttpContext.Session.SetObject("continueX", "ok");
                TempData["continue"] = "OK"; 
            }
            else
                TempData["continue-X"] = "NO";

            return RedirectToAction("Index");
        }

        public ActionResult AcceptContinue(int id) 
        {
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                var d = x.AddHours(DateTime.UtcNow, 7);
                var hour = String.Join("", d.Hour.ToString("00").Reverse().ToList());
                var minute = String.Join("", d.Minute.ToString("00").Reverse().ToList());

                if (id.ToString("0000") == minute + hour)
                {
                    TempData["ok-continue"] = "yes";
                string ID = "";
                var IPx = "";

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    IPx = endPoint.Address.ToString();
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")))
                {
                    TempData["GetDataIP"] = "true";
                    return RedirectToAction("Index");
                }
                else
                   ID = HttpContext.Session.GetString("userIP");

                string message = "Báo cáo hành động [tiếp tục] bật sử dụng trang web của khách hàng mới (ID client đã được đăng kí mở trước đây, yêu cầu lại cấp phép mới) :\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@end\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 1]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@1_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@1_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@1_end\r\n\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 2]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@2_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@2_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@2_end\r\n\r\nThanks!"
                .Replace("@lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + ID)
                .Replace("@unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + ID)
                .Replace("@end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + ID)
             .Replace("@1_lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + IPx)
                .Replace("@1_unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + IPx)
                .Replace("@1_end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IPx)
                 .Replace("@2_lock", "https://" + Request.Host + "/Home/LockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_unlock", "https://" + Request.Host + "/Home/UnlockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_end", "https://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + Request.HttpContext.Connection.RemoteIpAddress);

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + ID + "] - " + xuxu;
                    string host = "{" + Request.Host.ToString() + "}"
                               .Replace("http://", "")
                           .Replace("https://", "")
                           .Replace("/", "");

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
                var noidungX = System.IO.File.ReadAllText(pathX);

                var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    if (info[0] == "Email_User_Continue")
                    {
                        if (info[1] == "true")
                        {
                            SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                          "mywebplay.savefile@gmail.com", host + " [THONG BAO ADMIN] Continue Play On Web In Client Local  In " + name, message, "teinnkatajeqerfl");
                        }
                        break;
                    }
                }
               }
            else
                TempData["ok-continue-X"] = "no";

            return RedirectToAction("Index");
        }

        public ActionResult RemoveTempData()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult PlayTracNghiem_Online()
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
        public ActionResult PlayTracNghiem_Online(IFormCollection f)
        {
            var noidung = "";
            Calendar xi = CultureInfo.InvariantCulture.Calendar;
            var xuxu = xi.AddHours(DateTime.UtcNow, 7);
            var path = "";
            var id = f["txtID"].ToString();
            ViewBag.ID = id;
            var mssv = f["txtMSSV"].ToString();
            ViewBag.MSSV = mssv;
            var IP = "";
            try
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

                if (string.IsNullOrEmpty(id))
                {
                    ViewData["Loi1"] = "Vui lòng nhập ID liên kết với bài trắc nghiệm bạn muốn làm";
                    return this.PlayTracNghiem_Online();
                }

                if (string.IsNullOrEmpty(mssv))
                {
                    ViewData["Loi2"] = "Vui lòng nhập mã số học sinh của bạn";
                    return this.PlayTracNghiem_Online();
                }

                IP = HttpContext.Session.GetString("userIP");

                path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline/DiemHocSinh.txt");
                noidung = System.IO.File.ReadAllText(path);
                if (noidung.Contains(mssv + "\t" + id))
                {
                    ViewData["Loi2"] = "Mã học sinh đã thực hiện và có kết quả của bài làm trắc nghiệm đợt này. Mời bạn quay lại sau!";
                    return this.PlayTracNghiem_Online();
                }
                else
                {
                    System.IO.File.WriteAllText(path, noidung + xuxu + "\t" + IP + "\t" + mssv + "\t" + id + "\t[NULL]\t[NULL]\n");
                }

                noidung = System.IO.File.ReadAllText(path);

                int n9_S = 0;
                var split = id.Split("_");
                var timelambai = int.Parse(split[2]);
                var socau = int.Parse(split[1]);
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "file/TracNghiemOnline_32752262", split[0]);
                var listTN = new DirectoryInfo(pathX).GetFiles().ToArray();
                TracNghiem[] tn = new TracNghiem[socau];
                var ssl = "";
                if (split[3] == "s")
                    ssl = "http://";
                else if (split[3] == "ss")
                    ssl = "https://";
                var tenmon = split[4];

                for (var h = 0; h < listTN.Length; h++)
                {
                    tn[h] = new TracNghiem();
                 
                    var ND_file = "";
                    try
                    {
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(ssl + Request.Host + "/file/TracNghiemOnline_32752262/" + split[0] + "/" + listTN[h].Name);
                        StreamReader reader = new StreamReader(stream);
                        ND_file = reader.ReadToEnd();
                    }
                    catch
                    {
                        ViewData["Loi1"] = "Đã xảy ra lỗi khi cố gắng liên kết với ID bài test trắc nghiệm. Vui lòng thử lại sau!";
                        noidung = noidung.Replace(xuxu + "\t" + IP + "\t" + mssv + "\t" + id + "\t[NULL]\t[NULL]\n", "");
                        System.IO.File.WriteAllText(path, noidung);
                        return this.PlayTracNghiem_Online();
                    }

                    //--------------------

                    String[] t1 = ND_file.Split("\r\n#\r\n", StringSplitOptions.RemoveEmptyEntries);

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
                    var ix = 0;
                    while (true)
                    {
                        if (dem == t1.Length)
                            break;

                        double x = 0;
                        Random r = new Random();

                        x = r.Next(0, t1.Length);

                        if (chuaxet_ch[int.Parse(x.ToString())] == 1)
                            continue;

                        chuaxet_ch[int.Parse(x.ToString())] = 1;

                        int i = int.Parse(x.ToString());
                        String[] t2 = t1[i].Split("\r\n");

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
                tnX.ch = new String[socau];
                tnX.a = new String[socau];
                tnX.b = new String[socau];
                tnX.c = new String[socau];
                tnX.d = new String[socau];
                tnX.dung = new String[socau];

                tnX.tongsocau = n9_S;

                if (socau > n9_S)
                {
                    socau = n9_S;
                }

                tnX.gioihancau = socau;
                tnX.timelambai = timelambai;
                ViewBag.TimeLamBai = tnX.timelambai;

                //=======================

                int[][] chuaxetX = new int[socau][];

                for (int i = 0; i < listTN.Length; i++)
                {
                    chuaxetX[i] = new int[tn[i].tongsocau];
                    for (int j = 0; j < chuaxetX[i].Length; j++)
                    {
                        chuaxetX[i][j] = 0;
                    }
                }
                for (int i = 0; i < tnX.gioihancau; i++)
                {
                    int chuong = 0;
                    int soluong = 0;

                    Random r = new Random();

                    do
                    {
                        chuong = r.Next(0, listTN.Length);
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

                tnX.timelambai = timelambai;
                tnX.tenmon = tenmon;

                ViewBag.TimeLamBai = tnX.timelambai;

                HttpContext.Session.SetObject("TracNghiem", tnX);

                ViewBag.TongSoCau = tnX.tongsocau;
                ViewBag.GioiHanCau = tnX.gioihancau;
                ViewBag.TimeLamBaiX = tnX.timelambai;
                ViewBag.TenMon = tnX.tenmon;

                ViewBag.CauHoi = String.Join("\r\n", tnX.ch);
                ViewBag.A = String.Join("\r\n", tnX.a);
                ViewBag.B = String.Join("\r\n", tnX.b);
                ViewBag.C = String.Join("\r\n", tnX.c);
                ViewBag.D = String.Join("\r\n", tnX.d);
                ViewBag.Dung = String.Join("\r\n", tnX.dung);

                ViewBag.KetQuaDung = "";

                TempData["TracNghiem_Online"] = xuxu + "#" + mssv + "#" + id;

                return View("PlayTracNghiem", tnX);
            }
            catch(Exception ex)
            {
                ViewData["Loi1"] = "Đã xảy ra lỗi khi cố gắng liên kết với ID bài test trắc nghiệm. Vui lòng kiểm tra và thử lại sau!";
                noidung = noidung.Replace(xuxu + "\t" + IP + "\t" + mssv + "\t" + id + "\t[NULL]\t[NULL]\n", "");
                System.IO.File.WriteAllText(path, noidung);
                return this.PlayTracNghiem_Online();
            }
        }
    }
}
