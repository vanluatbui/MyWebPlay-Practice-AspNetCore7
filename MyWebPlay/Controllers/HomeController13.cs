using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult ViewNoteFile()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "note", "textnote.txt");

           var file = new FileInfo(path);

            if (System.IO.File.Exists(path))
            {
                ViewBag.Text1 = System.IO.File.ReadAllText(path);
                ViewBag.Text2 = "<p id=\"preX\" name=\"colorX\" style=\"color:" + TempData["mau_text"]+";font-size:22px; display:none\">"+ViewBag.Text1.Replace("\n", "<br>")+"</p>";
                Calendar x = CultureInfo.InvariantCulture.Calendar;
                ViewBag.DateTime = x.AddHours(file.LastWriteTimeUtc, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult EditTextNote()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "note", "textnote.txt");
            if (System.IO.File.Exists(path))
            {
                ViewBag.Text = System.IO.File.ReadAllText(path);
            }

            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        [HttpPost]
        public ActionResult EditTextNote(string? txtText)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                {
                    if (info[1] == "false")
                    {
                        
                        TempData["mau_winx"] = "red";
                        flag = 1;
                    }
                    else
                    {
                        
                        TempData["mau_winx"] = "deeppink";
                        flag = 0;
                    }
                }
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "note", "textnote.txt");
            
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            if (txtText != null)
            System.IO.File.WriteAllText(path, txtText);


            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP = "";
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
                localIP = HttpContext.Session.GetString("userIP");

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;

            if (txtText != null)
            {
                string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");
                var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidungX1 = System.IO.File.ReadAllText(pathX1);

                var listSetting1 = noidungX1.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < listSetting1.Length; i++)
                {
                    var info = listSetting1[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                    if (info[0] == "Email_Note")
                    {
                        if (info[1] == "true" && HttpContext.Session.GetString("trust-X-you") == null)
                        {
                            SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
"mywebplay.savefile@gmail.com", host + " Save Temp - Edit Text Note In " + name, txtText, "teinnkatajeqerfl");
                        }
                        break;
                    }
                }
 
            }

            return RedirectToAction("ViewNoteFile");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult XoaTextNote()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "note", "textnote.txt");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("ViewNoteFile");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult PlayQuestion_Multiple()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            HttpContext.Session.Remove("TracNghiem");

            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                f.Delete();
            }

            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        [HttpPost]
        public ActionResult PlayQuestion_Multiple (IFormCollection f, List<IFormFile> txtFile)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flax = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flax == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                {
                    if (info[1] == "false")
                    {
                        
                        TempData["mau_winx"] = "red";
                        flax = 1;
                    }
                    else
                    {
                        
                        TempData["mau_winx"] = "deeppink";
                        flax = 0;
                    }
                }
            }
            int sl = txtFile.Count();
            string txtSoCau = f["txtSoCau"].ToString();
            string txtTime = f["txtTime"].ToString();
            string txtMon = f["txtMon"].ToString();

            if (string.IsNullOrWhiteSpace(txtMon))
            {
                txtMon = txtFile[0].FileName;
            }

            if (string.IsNullOrEmpty(txtSoCau))
            {
                ViewData["Loi2"] = "Không được bỏ trống trường này";
                HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
            }

            if (string.IsNullOrEmpty(txtTime))
            {
                ViewData["Loi3"] = "Không được bỏ trống trường này";
                HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
            }

            int time = int.Parse(txtTime);
            if (time < 1 || time > 1200)
            {
                ViewData["Loi3"] = "Thời gian làm bài phải tối thiểu 1 phút và không vượt quá 20 giờ...";

                HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
            }

            if (txtFile.Count() <= 0)
            {
                ViewData["Loi1"] = "Mời bạn chọn file TXT trắc nghiệm (có thể chọn nhiều file thể hiện một môn học trắc nghiệm có nhiều chương/mục/phần/bài)...";
                HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
            }

            if (f["txtChangeJapan"].ToString() == "on")
            {
                TempData["chuyendoiJapan"] = "true";
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
                    HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
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
                        HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
                    }

                    char[] t2_x = t2[5].ToCharArray();
                    if (t2_x[0] != '[' || t2_x[t2[5].Length - 1] != ']')
                    {
                        string err = "WRONG INDEX QUESTION [CHƯƠNG/FILE " + (i + 1) + "] : " + t2[0] + "";
                        //err += "Định dạng file bạn đã chọn không đúng cú pháp (vui lòng kiểm tra và thử chọn lại file văn bản hoặc liên hệ Admin)! \n\n[CHÚ Ý : Kí tự # dùng để báo hiệu khoảng cách biệt mỗi câu, vì vậy tránh sử dụng # xuất hiện trong mỗi phần câu hỏi, còn $ dùng đế xác định câu hỏi không cần hoán vị đáp án. Xin lỗi vì sự bất tiện này! ]";

                        ViewData["Loi1"] = err;
                        HttpContext.Session.SetString("data-result", "true"); return this.PlayQuestion_Multiple();
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
                        HttpContext.Session.SetString("data-result", "true"); return this.TracNghiem_Multiple(ViewBag.SL);
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

            if (txtSoCau.Length > 0 && int.Parse(txtSoCau) > n9_S)
            {
                txtSoCau = n9_S.ToString();
            }

            tnX.gioihancau = int.Parse(txtSoCau);

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

            TempData["replaceJapan"] = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt"));

            return View("PlayQuestion", tnX);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult PlayQuestion(TracNghiem tn)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            HttpContext.Session.SetObject("TracNghiem", tn);
            return View(tn);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public string chuyendoiJapan()
        {
            var s = "";


            s += "--a\tァ\r\n";
            s += "--u\tゥ\r\n";
            s += "--e\tェ\r\n";
            s += "--o\tォ\r\n";
            s += "--ya\tャ\r\n";
            s += "--yu\tュ\r\n";
            s += "--yo\tョ\r\n";
            s += "--tsu\tﾂ\r\n";
            s += "--i\tィ\r\n";

            s += "-a\tぁ\r\n";
            s += "-u\tぅ\r\n";
            s += "-e\tぇ\r\n";
            s += "-o\tぉ\r\n";
            s += "-ya\tゃ\r\n";
            s += "-yu\tゅ\r\n";
            s += "-yo\tょ\r\n";
            s += "-tsu\tっ\r\n";
            s += "-i\tぃ\r\n";

            s += "[\t「\r\n";
            s += "]\t」\r\n";
            s += ".\t。\r\n";

            s += "^mr\t゜\r\n";
            s += "^tt\t゛\r\n";

            s += "-=-\tー\r\n";

            //==================================================

            s += "=ga\tガ\r\n";
            s += "=gi\tギ\r\n";
            s += "=gu\tグ\r\n";
            s += "=ge\tゲ\r\n";
            s += "=go\tゴ\r\n";

            s += "=za\tザ\r\n";
            s += "=zi\tジ\r\n";
            s += "=zu\tズ\r\n";
            s += "=ze\tゼ\r\n";
            s += "=zo\tゾ\r\n";

            s += "=da\tダ\r\n";
            s += "=di\tヂ\r\n";
            s += "=du\tヅ\r\n";
            s += "=de\tデ\r\n";
            s += "=do\tド\r\n";

            s += "=ba\tバ\r\n";
            s += "=bi\tビ\r\n";
            s += "=bu\tブ\r\n";
            s += "=be\tベ\r\n";
            s += "=bo\tボ\r\n";

            s += "=pa\tパ\r\n";
            s += "=pi\tピ\r\n";
            s += "=pu\tプ\r\n";
            s += "=pe\tペ\r\n";
            s += "=po\tポ\r\n";

            //-------------------------------

            s += "ga\tが\r\n";
            s += "gi\tぎ\r\n";
            s += "gu\tぐ\r\n";
            s += "ge\tげ\r\n";
            s += "go\tご\r\n";

            s += "za\tざ\r\n";
            s += "zi\tじ\r\n";
            s += "zu\tず\r\n";
            s += "ze\tぜ\r\n";
            s += "zo\tぞ\r\n";

            s += "da\tだ\r\n";
            s += "di\tぢ\r\n";
            s += "du\tづ\r\n";
            s += "de\tで\r\n";
            s += "do\tど\r\n";

            s += "ba\tば\r\n";
            s += "bi\tび\r\n";
            s += "bu\tぶ\r\n";
            s += "be\tべ\r\n";
            s += "bo\tぼ\r\n";

            s += "pa\tぱ\r\n";
            s += "pi\tぴ\r\n";
            s += "pu\tぷ\r\n";
            s += "pe\tぺ\r\n";
            s += "po\tぽ\r\n";


            //........................................................................

            s += "=ka\tカ\r\n";
            s += "=ki\tキ\r\n";
            s += "=ku\tク\r\n";
            s += "=ke\tケ\r\n";
            s += "=ko\tコ\r\n";

            s += "=ta\tタ\r\n";
            s += "=chi\tチ\r\n";
            s += "=tsu\tツ\r\n";
            s += "=te\tテ\r\n";
            s += "=to\tト\r\n";

            s += "=sa\tサ\r\n";
            s += "=shi\tシ\r\n";
            s += "=su\tス\r\n";
            s += "=se\tセ\r\n";
            s += "=so\tソ\r\n";

            s += "=na\tナ\r\n";
            s += "=ni\tニ\r\n";
            s += "=nu\tヌ\r\n";
            s += "=ne\tネ\r\n";
            s += "=no\tノ\r\n";

            s += "=ha\tハ\r\n";
            s += "=hi\tヒ\r\n";
            s += "=fu\tフ\r\n";
            s += "=he\tヘ\r\n";
            s += "=ho\tホ\r\n";

            s += "=ma\tマ\r\n";
            s += "=mi\tミ\r\n";
            s += "=mu\tム\r\n";
            s += "=me\tメ\r\n";
            s += "=mo\tモ\r\n";

            s += "=ya\tヤ\r\n";
            s += "=yu\tユ\r\n";
            s += "=yo\tヨ\r\n";
            s += "=wo\tヲ\r\n";
            s += "=wa\tワ\r\n";

            s += "=ra\tラ\r\n";
            s += "=ri\tリ\r\n";
            s += "=ru\tル\r\n";
            s += "=re\tレ\r\n";
            s += "=ro\tロ\r\n";
            s += "=nn\tン\r\n";
            s += "=a\tア\r\n";
            s += "=i\tイ\r\n";
            s += "=u\tウ\r\n";
            s += "=e\tエ\r\n";
            s += "=o\tオ\r\n";

            //----------------------------------------------------------

            s += "ka\tか\r\n";
            s += "ki\tき\r\n";
            s += "ku\tく\r\n";
            s += "ke\tけ\r\n";
            s += "ko\tこ\r\n";

            s += "ta\tた\r\n";
            s += "chi\tち\r\n";
            s += "tsu\tつ\r\n";
            s += "te\tて\r\n";
            s += "to\tと\r\n";

            s += "sa\tさ\r\n";
            s += "shi\tし\r\n";
            s += "su\tす\r\n";
            s += "se\tせ\r\n";
            s += "so\tそ\r\n";

            s += "na\tな\r\n";
            s += "ni\tに\r\n";
            s += "nu\tぬ\r\n";
            s += "ne\tね\r\n";
            s += "no\tの\r\n";

            s += "ha\tは\r\n";
            s += "hi\tひ\r\n";
            s += "fu\tふ\r\n";
            s += "he\tへ\r\n";
            s += "ho\tほ\r\n";

            s += "ma\tま\r\n";
            s += "mi\tみ\r\n";
            s += "mu\tむ\r\n";
            s += "me\tめ\r\n";
            s += "mo\tも\r\n";

            s += "ya\tや\r\n";
            s += "yu\tゆ\r\n";
            s += "yo\tよ\r\n";
            s += "wo\tを\r\n";
            s += "wa\tわ\r\n";

            s += "ra\tら\r\n";
            s += "ri\tり\r\n";
            s += "ru\tる\r\n";
            s += "re\tれ\r\n";
            s += "ro\tろ\r\n";
            s += "nn\tん\r\n";

            s += "a\tあ\r\n";
            s += "i\tい\r\n";
            s += "u\tう\r\n";
            s += "e\tえ\r\n";
            s += "o\tお";

            return s;
        }

        [HttpPost]
        public ActionResult PlayQuestion(IFormCollection f)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                {
                    if (info[1] == "false")
                    {
                        
                        TempData["mau_winx"] = "red";
                        flag = 1;
                    }
                    else
                    {
                        
                        TempData["mau_winx"] = "deeppink";
                        flag = 0;
                    }
                }
            }

            TracNghiem tn;

            if (HttpContext.Session.GetObject<TracNghiem>("TracNghiem") == null)
            {
                tn = new TracNghiem();
                tn.tongsocau = int.Parse(f["TongSoCau"].ToString());
                tn.gioihancau = int.Parse(f["GioiHanCau"].ToString());
                tn.timelambai = int.Parse(f["TimeLamBai"].ToString());
                tn.tenmon = f["TenMon"].ToString();

                tn.ch = f["CauHoi"].ToString().Split("\r\n");
                tn.a = f["A"].ToString().Split("\r\n");
                tn.b = f["B"].ToString().Split("\r\n");
                tn.c = f["C"].ToString().Split("\r\n");
                tn.d = f["D"].ToString().Split("\r\n");
                tn.dung = f["Dung"].ToString().Split("\r\n");
            }
            else
                tn = HttpContext.Session.GetObject<TracNghiem>("TracNghiem");

            if (tn == null)
                return RedirectToAction("PlayQuestion");

            int dung = 0;
            int sai = 0;
            int chualam = 0;

            for (int i = 0; i < tn.gioihancau; i++)
            {
                string da = f["dapan-" + i].ToString();

                if (da == "")
                {
                    chualam++;

                    ViewData["KetQua-" + i] = "<h2 style=\"color:orange\">CHƯA TRẢ LỜI</h2>";
                    ViewData["dapandung-" + i] = "<br><b><span style=\"color:deeppink\">Câu trả lời đúng</span> : <input type=\"text\" readonly size=\"100\" value=\"" +tn.dung[i] + "\"></input></b>";
                }
                else if (da == tn.dung[i])
                {
                    dung++;
                    ViewData["dapandachon-" + i] = "<b><span style=\"color:deeppink\">Câu trả lời của bạn</span> : <input type=\"text\" readonly size=\"100\" value=\"" + da + "\"></input></b>";
                    ViewData["KetQua-" + i] = "<br><h2 style=\"color:green\">ĐÚNG RỒI</h2><br>";
                }
                else if (da != tn.dung[i])
                {
                    ViewData["KetQua-" + i] = "<h2 style=\"color:red\">SAI RỒI</h2><b style=\"color:purple\">Nội dung answer của bạn có thể đúng, nhưng cách mà bạn nhập nó không match fit với answer trong file của bạn đã tải lên<br>(bao gồm phân biệt kí tự in hoa/thường, có dấu, các khoảng trắng,...vv...)</b><br>";
                    ViewData["dapandachon-" + i] = "<br><b><span style=\"color:deeppink\">Câu trả lời của bạn </span> : <input type=\"text\" readonly size=\"100\" value=\"" + da + "\"></input></b>";
                    ViewData["dapandung-" + i] = "<br><b><span style=\"color:deeppink\">Câu trả lời đúng</span> : <input type=\"text\" readonly size=\"100\" value=\"" + tn.dung[i] + "\" ></input></b>";
                    sai++;
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
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult CreateFile_Question()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
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
            if (ViewBag.ChuoiVD == null)
                ViewBag.ChuoiVD = "1+1=?\r\n2\r\nHà có 5 quả cam, Hà được Lan cho thêm 3 quả cam. Hỏi Hà có tất cả bao nhiêu quả cam?\r\n8 quả\r\nTìm x biết x * 2 = 18?\r\nx = 9\r\nĐây là ai trong nhóm Winx?<br><img src=\"http://i.redd.it/dlrwc6cqztg61.jpg\" alt=\"Image Error\"><br>\r\nStella\r\n<span style=\"color:red\">Hạnh phúc</span> là gì?\r\nLà niềm vui, là sự bình yên trong tâm hồn, là những ước mơ...";

            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        [HttpPost]
        public ActionResult CreateFile_Question(IFormCollection f)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            /*HttpContext.Session.Remove("ok-data");*/
            TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu1 = xi.AddHours(DateTime.UtcNow, 7);

            if (xuxu1.Hour >= 6 && xuxu1.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black";TempData["mau_nen"] = "dodgerblue";
                 TempData["winx"] = "❤";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white";TempData["mau_nen"] = "rebeccapurple";
                 TempData["winx"] = "❤";
            }
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var flag = 0;
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (flag == 0 && (info[0] == "Email_Upload_User"
                    || info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create"
                    || info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question"
                    || info[0] == "Email_User_Website" || info[0] == "Email_User_Continue"
                    || info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
                {
                    if (info[1] == "false")
                    {
                        
                        TempData["mau_winx"] = "red";
                        flag = 1;
                    }
                    else
                    {
                        
                        TempData["mau_winx"] = "deeppink";
                        flag = 0;
                    }
                }
            }
            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo fx = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                fx.Delete();
            }

            String[] ss = Regex.Split(f["txtChuoi"].ToString(),"\r\n");

            String s = "";
            for (int i =0; i<ss.Length;i = i+2)
            {
                s += ss[i] + "\n\n\n\n" + ss[i + 1] + "\n[" + ss[i + 1] + "]\n#\n";
            }

            char[] dd = { '\n' , '#', '\n' };
            s = s.TrimEnd(dd);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string fi = HttpContext.Session.GetString("userIP") + "_Question_" + xuxu + ".txt";
            fi = fi.Replace("\\", "");
            fi = fi.Replace("/", "");
            fi = fi.Replace(":", "");

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", fi);

            System.IO.File.WriteAllText(path, s);

            //------------------------------------

            ViewBag.ChuoiVD = "1+1=?\r\n2\r\nHà có 5 quả cam, Hà được Lan cho thêm 3 quả cam. Hỏi Hà có tất cả bao nhiêu quả cam?\r\n8 quả\r\nTìm x biết x * 2 = 18?\r\nx = 9\r\nĐây là ai trong nhóm Winx?<br><img src=\"http://i.redd.it/dlrwc6cqztg61.jpg\" alt=\"Image Error\"><br>\r\nStella\r\n<span style=\"color:red\">Hạnh phúc</span> là gì?\r\nLà niềm vui, là sự bình yên trong tâm hồn, là những ước mơ...";

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string localIP = "";
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
                localIP = HttpContext.Session.GetString("userIP");

            //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string name = "[Nox.IP : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " ~ " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + " - " + IPx + " *** " + localIP + "] - " + xuxu;
            string host = "{" + Request.Host.ToString() + "}"
                       .Replace("http://", "")
                   .Replace("http://", "")
                   .Replace("/", "");
            var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX1 = System.IO.File.ReadAllText(pathX1);

            var listSetting1 = noidungX1.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting1.Length; i++)
            {
                var info = listSetting1[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "Email_Question")
                {
                    if (info[1] == "true" && HttpContext.Session.GetString("trust-X-you") == null)
                    {
                        SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath,"mywebplay.savefile@gmail.com",
"mywebplay.savefile@gmail.com", host + " Save Temp Create Question Answer File In " + name, s, "teinnkatajeqerfl");
                    }
                    break;
                }
            }

            ViewBag.KetQua = "<p style=\"color:blue\">Thành công, một file TXT question/answer của bạn đã được xử lý...</p><a href=\"/tracnghiem/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:deeppink\" id=\"thoigian2\" class=\"thoigian2\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>Nếu file tải về của bạn bị lỗi hoặc chưa kịp tải về, hãy refresh/quay lại trang này và thử lại...<br><span style=\"color:aqua\">Mặc dù file này đã được thông qua một số xử lý, tuy nhiên nó vẫn có thể xảy ra lỗi và sai sót không mong muốn...</span></p>";

            return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }

        public ActionResult XoaAllFile_X2()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return RedirectToAction("Error"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                f.Delete();
            }
            return RedirectToAction("PlayQuestion_Multiple");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }
        }
    }
}
