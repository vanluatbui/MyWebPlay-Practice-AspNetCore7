using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult SQL_InsertDoc()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
                ViewBag.VD = "SV001\tMyWebPlay Asp Net Core\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        int[] listType = new int[100];
        int nx = 0;

        [HttpPost]
        public ActionResult SQL_InsertDoc(IFormCollection f, IFormFile fileData)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    // var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string dulieu = f["DuLieu"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");

                TempData["dataPost"] = "[" + dulieu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
                    var partDelayTime = delayTime.Split("#");
                    var hourDL = partDelayTime[0].Replace("H", "");
                    var minDL = partDelayTime[1].Replace("M", "");
                    var secDL = partDelayTime[2].Replace("S", "");

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (hourDL.Contains("-"))
                    {
                       xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(hourDL));
                    }

                    if (minDL.Contains("-"))
                    {
                        xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(minDL));
                    }

                    if (secDL.Contains("-"))
                    {
                        xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu.AddSeconds(int.Parse(secDL));
                    }

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = FileExtension.ReadFile(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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
                }

                ViewBag.VD = "SV001\tMyWebPlay Asp Net Core\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
                string table = f["Table"].ToString();
                string trangthai = f["TrangThai"].ToString();

                if (fileData == null && string.IsNullOrEmpty(table))
                {
                    ViewData["Loi1"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.SQL_InsertDoc();
                }

                if (fileData == null && string.IsNullOrEmpty(trangthai))
                {
                    ViewData["Loi2"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.SQL_InsertDoc();
                }

                if (fileData == null && string.IsNullOrEmpty(dulieu))
                {
                    ViewData["Loi3"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.SQL_InsertDoc();
                }

                table = table.Replace("[T-PLAY]", "\t");
                table = table.Replace("[N-PLAY]", "\n");
                table = table.Replace("[R-PLAY]", "\r");

                trangthai = trangthai.Replace("[T-PLAY]", "\t");
                trangthai = trangthai.Replace("[N-PLAY]", "\n");
                trangthai = trangthai.Replace("[R-PLAY]", "\r");

                dulieu = dulieu.Replace("[T-PLAY]", "\t");
                dulieu = dulieu.Replace("[N-PLAY]", "\n");
                dulieu = dulieu.Replace("[R-PLAY]", "\r");

                if (f.ContainsKey("txtAPI") || (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false))
                {
                    var txtAPI = f["txtAPI"].ToString().Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    if (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false)
                    {
                        if (fileData.FileName.EndsWith(".txt"))
                        {
                            using (var reader = new StreamReader(fileData.OpenReadStream()))
                            {
                                string content = reader.ReadToEnd();
                                txtAPI = content;
                            }
                        }
                    }
                    var apiValue = txtAPI.ToString().Replace("\r", "").Split("\n||\n");
                    table = apiValue[0].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    trangthai = apiValue[1].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    dulieu = apiValue[2].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                }

                string[] ds_trangthai = trangthai.Split('-');
                for (int i = 0; i < ds_trangthai.Length; i++)
                    listType[nx++] = int.Parse(ds_trangthai[i]);

                String sql = "\r\n\r\nset dateformat dmy\r\n\r\n";

                String[] s1 = Regex.Split(dulieu, "\r\n");

                for (int i = 0; i < s1.Length; i++)
                {
                    String[] s2 = s1[i].Split('\t');

                    String s = "INSERT INTO " + table + " VALUES (";

                    for (int j = 0; j < s2.Length; j++)
                    {

                        if (listType[j] == 1)
                            s += s2[j];
                        else
                        if (listType[j] == 2)
                            s += "N'" + s2[j] + "'";
                        else
                        if (listType[j] == 3)
                            s += "'" + s2[j] + "'";

                        if (j < s2.Length - 1)
                            s += ",";
                        else
                            s += ")";
                    }

                    sql += s + "\r\n";
                }

                //TextCopy.ClipboardService.SetText(sql);

                // // sql = sql.Replace("\r\n", "<br>");

                nix = sql;
                sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                FileExtension.WriteFile(sox, nix);
                ViewBag.Result = sql;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
               ghilogrequest(f); if (exter == false)
                    return View();
                else
                {
                    if (linkdown == true) return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
                    return Ok(new
                    {
                        result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                    });
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
        }

        //---------------------------------------------------

        [HttpGet]
        public ActionResult JSON_InsertDoc()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                if (HttpContext.Session.GetString("TuyetDoi") != null)
                {
                    TempData["UyTin"] = "true";
                    var td = HttpContext.Session.GetString("TuyetDoi");
                    if (td == "true")
                    {
                        TempData["TestTuyetDoi"] = "true"; /*return View();*/
                    }
                    else
                    {
                        TempData["TestTuyetDoi"] = "false";
                    }
                }
                if (TempData["tathoatdong"] == "true")
                {
                    return RedirectToAction("Error");
                }
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                {
                    HttpContext.Session.Remove("userIP");
                    HttpContext.Session.SetString("userIP", "0.0.0.0");
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
                ViewBag.VD = "SV001\tMyWebPlay Asp Net Core\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                });
            }
        }

        int[] listTypeX = new int[100];
        String[] nameType = new String[100];
        int nX = 0;
        int nY = 0;

        [HttpPost]
        public ActionResult JSON_InsertDoc(IFormCollection f, IFormFile fileData)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            var listIP = new List<string>();
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

               ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                    if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                    {
                        if (this.HttpContext.Request.Method == "GET")
                        {
                            if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                            {
                                HttpContext.Session.SetObject("google-trick-web", 1);
                                return Redirect("https://google.com");
                            }
                            else
                            {
                                var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                                if (lan != 10)
                                {
                                    HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                    return Redirect("https://google.com");
                                }
                            }
                        }
                    }
                    if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
                    if (TempData["errorXY"] == "true") return Redirect("https://google.com");
                    if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
                    if (HttpContext.Session.GetString("TuyetDoi") != null)
                    {
                        TempData["UyTin"] = "true";
                        var td = HttpContext.Session.GetString("TuyetDoi");
                        if (td == "true")
                        {
                            TempData["TestTuyetDoi"] = "true"; /*return View();*/
                        }
                        else
                        {
                            TempData["TestTuyetDoi"] = "false";
                        }
                    }
                    if (TempData["tathoatdong"] == "true")
                    {
                        return RedirectToAction("Error");
                    }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
                    {
                        HttpContext.Session.Remove("userIP");
                        HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]";
                    HttpContext.Session.SetString("data-result", "true");
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    //  var listIP = new List<string>();

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                string dulieu = f["DuLieu"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                dulieu = dulieu.Replace("[T-PLAY]", "\t");
                dulieu = dulieu.Replace("[N-PLAY]", "\n");
                dulieu = dulieu.Replace("[R-PLAY]", "\r");

                TempData["dataPost"] = "[" + dulieu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

               ghilogrequest(f); if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");
                    Calendar xi = CultureInfo.InvariantCulture.Calendar;

                    var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
                    var partDelayTime = delayTime.Split("#");
                    var hourDL = partDelayTime[0].Replace("H", "");
                    var minDL = partDelayTime[1].Replace("M", "");
                    var secDL = partDelayTime[2].Replace("S", "");

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (hourDL.Contains("-"))
                    {
                       xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(hourDL));
                    }

                    if (minDL.Contains("-"))
                    {
                        xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu = xuxu.AddHours(int.Parse(minDL));
                    }

                    if (secDL.Contains("-"))
                    {
                        xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
                    }
                    else
                    {
                        xuxu.AddSeconds(int.Parse(secDL));
                    }

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black";
                        TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white";
                        TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                    var noidungX = FileExtension.ReadFile(pathX);
                    var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    var flag = 0;
                    for (int i = 0; i < listSetting.Length; i++)
                    {
                        var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                        if (flag == 0 && (info[0] == "Email_Upload_User" ||
                            info[0] == "MegaIo_Upload_User" || info[0] == "Email_TracNghiem_Create" ||
                            info[0] == "Email_TracNghiem_Update" || info[0] == "Email_Question" ||
                            info[0] == "Email_User_Website" || info[0] == "Email_User_Continue" ||
                            info[0] == "Email_Note" || info[0] == "Email_Karaoke"))
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
                }

                ViewBag.VD = "SV001\tMyWebPlay Asp Net Core\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
                string trangthai = f["TrangThai"].ToString();

                if (fileData == null && string.IsNullOrEmpty(trangthai))
                {
                    ViewData["Loi2"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.JSON_InsertDoc();
                }

                if (fileData == null && string.IsNullOrEmpty(dulieu))
                {
                    ViewData["Loi3"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.JSON_InsertDoc();
                }

                trangthai = trangthai.Replace("[T-PLAY]", "\t");
                trangthai = trangthai.Replace("[N-PLAY]", "\n");
                trangthai = trangthai.Replace("[R-PLAY]", "\r");

                if (f.ContainsKey("txtAPI") || (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false))
                {
                    var txtAPI = f["txtAPI"].ToString().Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    if (fileData != null && string.IsNullOrEmpty(fileData.FileName) == false)
                    {
                        if (fileData.FileName.EndsWith(".txt"))
                        {
                            using (var reader = new StreamReader(fileData.OpenReadStream()))
                            {
                                string content = reader.ReadToEnd();
                                txtAPI = content;
                            }
                        }
                    }
                    var apiValue = txtAPI.ToString().Replace("\r", "").Split("\n||\n");
                    trangthai = apiValue[0].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    dulieu = apiValue[1].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                }


                string[] ds_trangthai = trangthai.Split(",");

                for (int i = 0; i < ds_trangthai.Length; i++)
                {
                    string[] DS = ds_trangthai[i].Split("-");
                    nameType[nX++] = DS[0];
                    listTypeX[nY++] = int.Parse(DS[1]);
                }

                String sql = "\r\n\r\n[\r\n";

                String[] s1 = Regex.Split(dulieu, "\r\n");

                for (int i = 0; i < s1.Length; i++)
                {
                    String[] s2 = s1[i].Split('\t');

                    String s = "  {\r\n    ";

                    for (int j = 0; j < s2.Length; j++)
                    {

                        if (listTypeX[j] == 1)
                            s += "\"" + nameType[j] + "\" : " + s2[j];
                        else
                        if (listTypeX[j] == 2)
                            s += "\"" + nameType[j] + "\" : \"" + s2[j] + "\"";
                        else
                        if (listTypeX[j] == 3)
                            s += "\"" + nameType[j] + "\" : '" + s2[j] + "'";

                        if (j < s2.Length - 1)
                            s += ",\r\n    ";
                        else
                            s += "\r\n  }";
                    }

                    if (i < s1.Length - 1)
                        s += ",\r\n";
                    else
                        s += "\r\n]";
                    sql += s;
                }

                //TextCopy.ClipboardService.SetText(sql);

                //// sql = sql.Replace("\r\n", "<br>");
                nix = sql;
                sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                FileExtension.WriteFile(sox, nix);
                ViewBag.Result = sql;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
               ghilogrequest(f); if (exter == false)
                    return View();
                else
                {
                    if (linkdown == true) return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
                    return Ok(new
                    {
                        result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                    });
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((TempData["userIP"] != null) ? TempData["userIP"] : HttpContext.Session.GetString("userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl", context:HttpContext);
                }
               ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes" : "show-error(true)"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
        }

        //--------------------------------------------

        [HttpGet]
        public ActionResult Copy_CreateDatabase()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("create database SinhVien\r\non\r\n  (name ='SinhVien _DATA', filename = 'C:\\SinhVien.MDF')\r\nlog on\r\n   (name ='SinhVien_LOG', filename = 'C:\\SinhVien.LDF')\r\n\r\nuse SinhVien");
            String sql = "\r\n\r\ncreate database [SinhVien]\r\non\r\n  (name ='SinhVien _DATA', filename = 'C:\\SinhVien.MDF')\r\nlog on\r\n   (name ='SinhVien_LOG', filename = 'C:\\SinhVien.LDF')\r\n\r\nuse [SinhVien]";
            //// sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_BackupDatabase1()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("backup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'");

            String sql = "\r\n\r\nbackup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_BackupDatabase2()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("backup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'\r\nwith password = '12345'");
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);

            String sql = "\r\n\r\nbackup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'\r\nwith password = '12345'";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase1()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'");
            String sql = "\r\n\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase2()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak', replace");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak', replace";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase3()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345'");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345'";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase4()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345', replace");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345', replace";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_AttachDatabase()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("create database SinhVien\r\non\r\n  (filename='C:\\SinhVien.MDF')\r\nfor attach");
            String sql = "\r\ncreate database SinhVien\r\non\r\n  (filename='C:\\SinhVien.MDF')\r\nfor attach";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_DetachDatabase()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("sp_detach_db SinhVien");
            String sql = "\r\nsp_detach_db SinhVien";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Index()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("CREATE INDEX <Tên index> ON <Tên Table> (<Nhóm các cột> ASC|DESC)\r\n");
            String sql = "\r\nCREATE INDEX &lt;Tên index&gt; ON &lt;Tên Table&gt; (&lt;Nhóm các cột&gt; ASC|DESC)\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_View()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("CREATE VIEW <Tên View>\r\nAS\r\n\t<Câu lệnh Select>\r\n\r\n-- Thực thi View\r\nSELECT * FROM <Tên View đã tạo>\r\n");
            String sql = "\r\nCREATE VIEW &lt;Tên View&gt;\r\nAS\r\n\t&lt;Câu lệnh Select&gt;\r\n\r\n-- Thực thi View\r\nSELECT * FROM &lt;Tên View đã tạo&gt;\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Procedure()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("\r\nCREATE PROC <Tên Procedure> (@<Danh sách các tham số và kiểu dữ liệu> OUTPUT)\r\nAS\r\n<Câu lệnh truy vấn>\r\n\r\n-- Thực thi PROC\r\nEXECUTE <Tên Procedure> <Danh sách các giá trị của tham số>)");
            String sql = "\r\n\r\nCREATE PROC &lt;Tên Procedure&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt; OUTPUT)\r\nAS\r\n&lt;Câu lệnh truy vấn&gt;\r\n\r\n-- Thực thi PROC\r\nEXECUTE &lt;Tên Procedure&gt; &lt;Danh sách các giá trị của tham số&gt;)";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Function()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("-- Trả về giá trị\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS <Kiểu dữ liệu trả về>\r\nAS\r\n\r\n\tBEGIN\r\n\r\nDECLARE @<Danh sách các biến và kiểu dữ liệu>\r\nSET @<Tên biến>= <Giá Trị Gán>\r\nIF (<...>)\r\n(<...>)\r\nELSE\r\n<...>\r\nRETURN <Biến cần trả về>\r\n\r\n\tEND\r\n\r\n-- Trả về Table (không có điều kiện)\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS Table\r\nAS\r\nRETURN (<Câu lệnh Select>)\r\n\r\n-- Trả về Table (có điều kiện)\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS @<Tên biến bảng> Table (<Danh sách các cột cần xuất cùng kiểu dữ liệu>)\r\nAS\r\n\tBEGIN\r\n\r\nIF(<...>) INSERT INTO @<Tên biến bảng>\r\n<Câu lệnh Select - Chỉ Select với đúng tên và đúng số lượng cột đã khai báo ở trên>\r\nELSE\r\n<...Tương tự...>\r\n\r\n\tEND\r\n\r\n---- Thực thi Function\r\nSELECT DBO.<Tên Function>(@<Danh sách các giá trị của tham số)\r\n");
            String sql = "\r\n- Trả về giá trị\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS &lt;Kiểu dữ liệu trả về&gt;\r\nAS\r\n\r\n\tBEGIN\r\n\r\nDECLARE @&lt;Danh sách các biến và kiểu dữ liệu&gt;\r\nSET @&lt;Tên biến&gt; = &lt;Giá Trị Gán&gt;\r\nIF (&lt;...&gt;)\r\n(&lt;...&gt;)\r\nELSE\r\n&lt;...&gt;\r\nRETURN &lt;Biến cần trả về&gt;\r\n\r\n\tEND\r\n\r\n-- Trả về Table (không có điều kiện)\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS Table\r\nAS\r\nRETURN (&lt;Câu lệnh Select&gt;)\r\n\r\n-- Trả về Table (có điều kiện)\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS @&lt;Tên biến bảng&gt; Table (&lt;Danh sách các cột cần xuất cùng kiểu dữ liệu&gt;)\r\nAS\r\n\tBEGIN\r\n\r\nIF(&lt;...&gt;) INSERT INTO @&lt;Tên biến bảng&gt;\r\n&lt;Câu lệnh Select - Chỉ Select với đúng tên và đúng số lượng cột đã khai báo ở trên&gt;\r\nELSE\r\n&lt;...Tương tự...&gt;\r\n\r\n\tEND\r\n\r\n---- Thực thi Function\r\nSELECT DBO.&lt;Tên Function&gt;(@&lt;Danh sách các giá trị của tham số)\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Trigger()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("CREATE TRIGGER <Tên Trigger> ON <Tên Table>\r\nFOR <INSERT | UPDATE | DELETE>\r\nAS\r\nIF UPDATE(<Tên Cột Của Bảng Nếu Muốn Sửa Sẽ Phải Gặp Trigger bên dưới- ?chỉ dành cho [for update]?>) -- Không thì có thể bỏ qua dòng này\r\nBEGIN\r\n\tIF (SELECT COUNT(*) FROM <INSERTED || DELETED || Table Khác> <..>) <...>\r\n\t--- (INSERTED : Các dữ liệu mới vừa Insert Into hay Dữ liệu mới vừa Set Cập Nhật Update)\r\n\t--- (DELETED : Các dữ liệu cũ trước khi Update Set Thành Giá Trị mới hoặc Giá Trị vừa mới bị Delete)\r\n\tBEGIN\r\n\tROLLBACK TRAN | <Hoặc công việc nào đó>\r\n\tEND\r\nEND\r\n");
            String sql = "\r\nCREATE TRIGGER &lt;Tên Trigger&gt; ON &lt;Tên Table&gt;\r\nFOR &lt;INSERT | UPDATE | DELETE&gt;\r\nAS\r\nIF UPDATE(&lt;Tên Cột Của Bảng Nếu Muốn Sửa Sẽ Phải Gặp Trigger bên dưới- ?chỉ dành cho [for update]?&gt;) -- Không thì có thể bỏ qua dòng này\r\nBEGIN\r\n\tIF (SELECT COUNT(*) FROM &lt;INSERTED || DELETED || Table Khác&gt; &lt;..&gt;) &lt;...&gt;\r\n\t--- (INSERTED : Các dữ liệu mới vừa Insert Into hay Dữ liệu mới vừa Set Cập Nhật Update)\r\n\t--- (DELETED : Các dữ liệu cũ trước khi Update Set Thành Giá Trị mới hoặc Giá Trị vừa mới bị Delete)\r\n\tBEGIN\r\n\tROLLBACK TRAN | &lt;Hoặc công việc nào đó&gt;\r\n\tEND\r\nEND\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_AddColumn()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("ALTER TABLE SinhVien ADD\r\nNgaySinh Date,\r\nDiemTB float,\r\nGioiTinh Bit");
            String sql = "\r\nALTER TABLE SinhVien ADD\r\nNgaySinh Date,\r\nDiemTB float,\r\nGioiTinh Bit";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_DeleteColumn()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("ALTER TABLE SinhVien DROP\r\nNgaySinh,\r\nDiemTB,\r\nGioiTinh");
            String sql = "\r\nALTER TABLE SinhVien DROP\r\nNgaySinh,\r\nDiemTB,\r\nGioiTinh";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn1()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("ALTER TABLE <TênTable> ALTER COLUMN <TênCột> <Kiểu dữ liệu mới>");
            String sql = "\r\nALTER TABLE &lt;TênTable&gt; ALTER COLUMN &lt;TênCột&gt; &lt;Kiểu dữ liệu mới&gt;";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn2()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("-- Thay đổi ràng buộc cho cột là không được phép NULL\r\nALTER TABLE <TênTable> ALTER COLUMN <TênCột> <KiểuDữLiệu> NOT NULL\r\n-- ...P/s : Nếu nhiều cột cần làm khoá chính thì hãy tương tự cho các cột khác\r\n\r\n-- Cập nhật các cột làm khoá chính (phải chạy dòng trên trước)\r\nALTER TABLE <TênTable> ADD CONSTRAINT <TênConstraint> PRIMARY KEY (<Nhóm các cột cần làm khoá chính>)\r\n");
            String sql = "\r\n-- Thay đổi ràng buộc cho cột là không được phép NULL\r\nALTER TABLE &lt;TênTable&gt; ALTER COLUMN &lt;TênCột&gt; &lt;KiểuDữLiệu&gt; NOT NULL\r\n-- ...P/s : Nếu nhiều cột cần làm khoá chính thì hãy tương tự cho các cột khác\r\n\r\n-- Cập nhật các cột làm khoá chính (phải chạy dòng trên trước)\r\nALTER TABLE &lt;TênTable&gt; ADD CONSTRAINT &lt;TênConstraint&gt; PRIMARY KEY (&lt;Nhóm các cột cần làm khoá chính&gt;)\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn3()
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
            khoawebsiteClient(null);
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home");
            if (TempData["errorXY"] == "true") return Redirect("https://google.com");
            if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";
            if (HttpContext.Session.GetString("TuyetDoi") != null)
            {
                TempData["UyTin"] = "true";
                var td = HttpContext.Session.GetString("TuyetDoi");
                if (td == "true")
                {
                    TempData["TestTuyetDoi"] = "true"; /*return View();*/
                }
                else
                {
                    TempData["TestTuyetDoi"] = "false";
                }
            }
            if (TempData["tathoatdong"] == "true")
            {
                return RedirectToAction("Error");
            }
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */ )
            {
                HttpContext.Session.Remove("userIP");
                HttpContext.Session.SetString("userIP", "0.0.0.0");
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
            //TextCopy.ClipboardService.SetText("ALTER TABLE <Tên Table> ADD CONSTRAINT <Tên Ràng buộc>\r\nFOREIGN KEY (<Cột cần làm khoá ngoại>) REFERENCES <Table Cha> (<Cột của bảng cha cần nối kết khoá ngoại>)\r\n");
            String sql = "\r\nALTER TABLE &lt;Tên Table&gt; ADD CONSTRAINT &lt;Tên Ràng buộc&gt;\r\nFOREIGN KEY (&lt;Cột cần làm khoá ngoại&gt;) REFERENCES &lt;Table Cha&gt; (&lt;Cột của bảng cha cần nối kết khoá ngoại&gt;)\r\n";
            // sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            Calendar soi = CultureInfo.InvariantCulture.Calendar;
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
            if (string.IsNullOrEmpty(chim)) chim = "Default";
            var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
            var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt");
            TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
            new FileInfo(sox).Create().Dispose();
            FileExtension.WriteFile(sox, nix);
            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
            return View("SQL_InsertDoc", "Home");
        }
    }
}