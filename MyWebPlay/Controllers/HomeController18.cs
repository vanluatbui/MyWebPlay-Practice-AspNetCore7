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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            TempData["continue"] = "";

            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            HttpContext.Session.SetString("data-result", "true");
            var listIP = new List<string>();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                listIP.Add(HttpContext.Session.GetString("userIP"));
            else
            {
                TempData["GetDataIP"] = "true";
                return RedirectToAction("Index");
            }
            khoawebsiteClient(listIP);
            if (TempData["UserIP"] == "0.0.0.0")
                return RedirectToAction("LockedWeb");
            if (TempData["UserIP"] == "0.0.0.0")
                return RedirectToAction("LockedWeb");

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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
            if (TempData["BlockRegistUsing"] == "true")
                return RedirectToAction("LockedWeb");

            if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
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
            if (TempData["UserIP"] == "0.0.0.0")
                return RedirectToAction("LockedWeb");
            HttpContext.Session.SetString("data-result", "true");
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

        public ActionResult AcceptContinue(int id, string? cmt) 
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }

            if (TempData["BlockRegistUsing"] == "true")
                return RedirectToAction("LockedWeb");

            if (string.IsNullOrEmpty(cmt))
                return RedirectToAction("Index");

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


                if (ID == "0.0.0.0")
                    return RedirectToAction("LockedWeb");

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                var path1 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/InfoIPRegist.txt");
                var noidung1 = docfile(path1);

                if (ID != "0.0.0.0")
                System.IO.File.WriteAllText(path1, noidung1 + "\n" +ID + "\t" + xuxu + "\t" + "[continue] " + cmt);

                string message = "Báo cáo hành động [tiếp tục] bật sử dụng trang web của khách hàng @info (ID client đã được đăng kí mở trước đây, yêu cầu lại cấp phép mới) :\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@end\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 1]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@1_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@1_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@1_end\r\n\r\n\r\n--------------------------------------------------------\r\n\r\n[DỰ PHÒNG 2]\r\n\r\n- Khoá sử dụng website với IDs này :\r\n\r\n@2_lock\r\n\r\n- Mở khoá và cho phép sử dụng lại website với IDs này\r\n\r\n@2_unlock\r\n\r\n- Hết hạn sử dụng, yêu cầu bật lại để sử dụng :\r\n\r\n@2_end\r\n\r\nThanks!"
                .Replace("@lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + ID)
                .Replace("@info", cmt)
                .Replace("@unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + ID)
                .Replace("@end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + ID)
             .Replace("@1_lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + IPx)
                .Replace("@1_unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + IPx)
                .Replace("@1_end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + IPx)
                 .Replace("@2_lock", "http://" + Request.Host + "/Home/LockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_unlock", "http://" + Request.Host + "/Home/UnlockThisClient?ip=" + Request.HttpContext.Connection.RemoteIpAddress)
                .Replace("@2_end", "http://" + Request.Host + "/Home/RemoveIpInWeb?ip=" + Request.HttpContext.Connection.RemoteIpAddress);


                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + ID + "] - " + xuxu;
                    string host = "{" + Request.Host.ToString() + "}"
                               .Replace("http://", "")
                           .Replace("http://", "")
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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
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
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
                HttpContext.Session.Remove("ok-data");
                if (string.IsNullOrEmpty(id))
                {
                    ViewData["Loi1"] = "Vui lòng nhập ID liên kết với bài trắc nghiệm bạn muốn làm";
                    HttpContext.Session.SetString("data-result", "true"); return this.PlayTracNghiem_Online();
                }

                if (string.IsNullOrEmpty(mssv))
                {
                    ViewData["Loi2"] = "Vui lòng nhập mã số học sinh của bạn";
                    HttpContext.Session.SetString("data-result", "true"); return this.PlayTracNghiem_Online();
                }

                IP = HttpContext.Session.GetString("userIP");

                path = Path.Combine(_webHostEnvironment.WebRootPath, "TracNghiem_XOnline/DiemHocSinh.txt");
                noidung = System.IO.File.ReadAllText(path);
                if (noidung.Contains(mssv + "\t" + id))
                {
                    ViewData["Loi2"] = "Mã học sinh đã thực hiện và có kết quả của bài làm trắc nghiệm đợt này. Mời bạn quay lại sau!";
                    HttpContext.Session.SetString("data-result", "true"); return this.PlayTracNghiem_Online();
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
                if (HttpContext.Request.IsHttps == false)
                    ssl = "http://";
                else
                    ssl = "https://";
                var tenmon = split[3];

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
                        HttpContext.Session.SetString("data-result", "true"); return this.PlayTracNghiem_Online();
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
                HttpContext.Session.SetString("data-result", "true"); return this.PlayTracNghiem_Online();
            }
        }

        public ActionResult KiemTraTinTuong(string ip, string url)
        {
            if (ip == "0.0.0.0")
                return RedirectToAction("Index");

            var IPbelieve = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);

            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "Believe_IP")
                {
                    IPbelieve = info[3];
                }
            }

            if (IPbelieve.Contains(","+ip+","))
            {
                HttpContext.Session.SetString("trust-ok", "true");
            }

            if (url != null)
                return Redirect(url);
               return RedirectToAction("Index");
        }

        public ActionResult BatSuTinTuong(string url)
        {
            HttpContext.Session.SetString("trust-X-you", "true");
            HttpContext.Session.SetString("alert-trust", "true");

            if (url != null)
            return Redirect(url);
            return RedirectToAction("Index");
        }

        public ActionResult TatSuTinTuong(string url)
        {
            HttpContext.Session.Remove("trust-X-you");
            HttpContext.Session.SetString("alert-trust", "true");

            if (url != null)
                return Redirect(url);
            return RedirectToAction("Index");
        }

        //---------------------------

        public ActionResult Setting_AddIPActive(string ip)
        {
            if (string.IsNullOrEmpty(ip) == false)
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
                var noidung2 = docfile(path2);

                if (noidung2.Contains(ip + "##") == false)
                    System.IO.File.WriteAllText(path2, noidung2 + ip + "##");
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        public ActionResult Setting_RemoveIPActive(string ip)
        {
            if (string.IsNullOrEmpty(ip) == false)
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPOnWebPlay.txt");
                var noidung2 = docfile(path2);
                noidung2 = noidung2.Replace(ip + "##", "");

                System.IO.File.WriteAllText(path2, noidung2);
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        //======

        public ActionResult Setting_AddIPLockAdmin(string ip)
        {
            if (string.IsNullOrEmpty(ip) == false)
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
                var noidung2 = docfile(path2);

                if (noidung2.Contains(ip + "##") == false)
                    System.IO.File.WriteAllText(path2, noidung2 + ip + "##");
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        public ActionResult Setting_RemoveIPLockAdmin(string ip)
        {
            if (string.IsNullOrEmpty(ip) == false)
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/ListIPLock.txt");
                var noidung2 = docfile(path2);
                
                    noidung2 = noidung2.Replace(ip + "##", "");

                    System.IO.File.WriteAllText(path2, noidung2);
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        public ActionResult Setting_AddIPLockClient(string ip_pass)
        {
            if (ip_pass.Contains("<>") == false)
            {
                ip_pass = ip_pass + "<>[NOT-PASSWORD-3275]";
            }
            if (string.IsNullOrEmpty(ip_pass) == false && ip_pass.Contains("<>"))
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
                var noidung2 = docfile(path2);

                var ip = ip_pass.Split("<>")[0];

                if (noidung2.Contains(ip) == false)
                    System.IO.File.WriteAllText(path2, noidung2 + ip_pass + "##");
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        public ActionResult Setting_ChangePassLockClient(string ip_pass)
        {
            if (ip_pass.Contains("<>") == false)
            {
                ip_pass = ip_pass + "<>[NOT-PASSWORD-3275]";
            }
            if (string.IsNullOrEmpty(ip_pass) == false && ip_pass.Contains("<>"))
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
                var noidung2 = docfile(path2);

                var ip = ip_pass.Split("<>")[0];

                var sd = noidung2.Split("##");
                for (int i = 0; i < sd.Length;i++)
                {
                    if (sd[i].Contains(ip))
                    {
                        noidung2 = noidung2.Replace(sd[i], ip_pass);
                        System.IO.File.WriteAllText(path2, noidung2);
                        break;
                    }
                }
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }

        public ActionResult Setting_RemoveIPLockClient(string ip_pass)
        {
            if (string.IsNullOrEmpty(ip_pass) == false)
            {
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath, "ClientConnect/LockedIPClient.txt");
                var noidung2 = docfile(path2);
                noidung2 = noidung2.Replace(ip_pass + "##", "");

                System.IO.File.WriteAllText(path2, noidung2);
            }
            return Redirect("/Admin/SettingXYZ#labelActive");
        }
    }
}
