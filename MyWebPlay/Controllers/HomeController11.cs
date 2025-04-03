using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Regex_Replace_Multiple()
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

        [HttpPost]
        public ActionResult Regex_Replace_Multiple(IFormCollection f, IFormFile fileData)
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

                if (exter == false)
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
                string chuoi = f["Chuoi"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");
                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

                if (exter == false)
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

                if (string.IsNullOrEmpty(chuoi))
                {
                    ViewData["Loi1"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string dukien1 = f["DuKien1"].ToString();
                dukien1 = dukien1.Replace("[T-PLAY]", "\t");
                dukien1 = dukien1.Replace("[N-PLAY]", "\n");
                dukien1 = dukien1.Replace("[R-PLAY]", "\r");
                if (string.IsNullOrEmpty(dukien1))
                {
                    ViewData["Loi4"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string input = f["Input"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                input = input.Replace("[T-PLAY]", "\t");
                input = input.Replace("[N-PLAY]", "\n");
                input = input.Replace("[R-PLAY]", "\r");
                if (string.IsNullOrEmpty(input))
                {
                    ViewData["Loi2"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string yes = f["DuKien3"].ToString();
                yes = yes.Replace("[T-PLAY]", "\t");
                yes = yes.Replace("[N-PLAY]", "\n");
                yes = yes.Replace("[R-PLAY]", "\r");
                if (string.IsNullOrEmpty(yes))
                {
                    ViewData["Loi6"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string dukien2 = f["DuKien2"].ToString();
                dukien2 = dukien2.Replace("[T-PLAY]", "\t");
                dukien2 = dukien2.Replace("[N-PLAY]", "\n");
                dukien2 = dukien2.Replace("[R-PLAY]", "\r");
                if (string.IsNullOrEmpty(dukien2))
                {
                    ViewData["Loi5"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string dukien4 = f["DuKien4"].ToString();
                dukien4 = dukien4.Replace("[T-PLAY]", "\t");
                dukien4 = dukien4.Replace("[N-PLAY]", "\n");
                dukien4 = dukien4.Replace("[R-PLAY]", "\r");
                if (string.IsNullOrEmpty(dukien4))
                {
                    ViewData["Loi7"] = "Trường này không được để trống!";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                string output = f["Output"].ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
                output = output.Replace("[T-PLAY]", "\t");
                output = output.Replace("[N-PLAY]", "\n");
                output = output.Replace("[R-PLAY]", "\r");
                //if (string.IsNullOrEmpty(output))
                //{
                //    ViewData["Loi3"] = "Trường này không được để trống!";
                //    HttpContext.Session.SetString("data-result", "true"); return this.Regex_Replace_Multiple();
                //}

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
                    chuoi = apiValue[0].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    input = apiValue[1].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    yes = apiValue[2].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    output = apiValue[3].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    dukien1 = apiValue[4].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    dukien2 = apiValue[5].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                    dukien4 = apiValue[6].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                }

                var listInput = Regex.Split(input, "\r\n");
                var listOutput = Regex.Split(output, "\r\n");

                if (listInput.Length != listOutput.Length)
                {
                    ViewData["Loi3"] = "Số lượng thành phần pattern input và output không tương xứng...";
                    HttpContext.Session.SetString("data-result", "true");
                    return this.Regex_Replace_Multiple();
                }

                if (yes == "0")
                {
                    for (int i = 0; i < listInput.Length - 1; i++)
                    {
                        for (int j = i + 1; j < listInput.Length; j++)
                        {
                            if (listInput[j].Contains(listInput[i]))
                            {
                                string t = listInput[i];
                                listInput[i] = listInput[j];
                                listInput[j] = t;

                                string tt = listOutput[i];
                                listOutput[i] = listOutput[j];
                                listOutput[j] = tt;
                            }
                        }
                    }
                }

                string result = "\r\n";

                if (int.Parse(dukien4) != 0)
                {
                    for (int i = 0; i < listInput.Length; i++)
                    {
                        for (int k = 0; k < int.Parse(dukien4); k++)
                        {
                            listInput[i] = listInput[i].Replace("\\n", "\n");
                            listInput[i] = listInput[i].Replace("\\r\\n", "\r\n");
                            listInput[i] = listInput[i].Replace("\\t", "\t");

                            listOutput[i] = listOutput[i].Replace("\\n", "\n");
                            listOutput[i] = listOutput[i].Replace("\\r\\n", "\r\n");
                            listOutput[i] = listOutput[i].Replace("\\t", "\t");

                            Regex regex = new Regex(listInput[i]);

                            if (int.Parse(dukien1) == 1)
                            {
                                while (listInput[i].Contains(@"\\") == true)
                                {
                                    listInput[i] = listInput[i].Replace(@"\\", @"\");
                                  }

                                while (listOutput[i].Contains(@"\\") == true)
                                {
                                    listOutput[i] = listOutput[i].Replace(@"\\", @"\");
                                  }

                                if (int.Parse(dukien2) == -1)
                                {
                                    chuoi = regex.Replace(chuoi, listOutput[i]);
                                    result = chuoi;
                                }
                                else
                                {
                                    chuoi = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                                    result = chuoi;
                                }
                            }
                            else
                            {
                                chuoi = chuoi.Replace(listInput[i], listOutput[i]);
                                result = chuoi;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < listInput.Length; i++)
                    {
                        while (chuoi.Contains(listInput[i]) == true)
                        {
                            listInput[i] = listInput[i].Replace("\\n", "\n");
                            listInput[i] = listInput[i].Replace("\\r\\n", "\r\n");
                            listInput[i] = listInput[i].Replace("\\t", "\t");

                            listOutput[i] = listOutput[i].Replace("\\n", "\n");
                            listOutput[i] = listOutput[i].Replace("\\r\\n", "\r\n");
                            listOutput[i] = listOutput[i].Replace("\\t", "\t");
                            Regex regex = new Regex(listInput[i]);

                            if (int.Parse(dukien1) == 1)
                            {
                                while (listInput[i].Contains(@"\\") == true)
                                {
                                    listInput[i] = listInput[i].Replace(@"\\", @"\");
                                  }

                                while (listOutput[i].Contains(@"\\") == true)
                                {
                                    listOutput[i] = listOutput[i].Replace(@"\\", @"\");
                                  }

                                if (int.Parse(dukien2) == -1)
                                {
                                    chuoi = regex.Replace(chuoi, listOutput[i]);
                                    result = chuoi;
                                }
                                else
                                {
                                    chuoi = regex.Replace(chuoi, listOutput[i], int.Parse(dukien2));
                                    result = chuoi;
                                }
                            }
                            else
                            {
                                chuoi = chuoi.Replace(listInput[i], listOutput[i]);
                                result = chuoi;
                            }
                        }
                    }
                }

                //TextCopy.ClipboardService.SetText(result);
                nix = result;
                result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

                Calendar soi = CultureInfo.InvariantCulture.Calendar;
                var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";
                var cuctac = soi.AddHours(DateTime.UtcNow, 7) + "_" + chim;
                var sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                FileExtension.WriteFile(sox, nix);
                ViewBag.Result = result;

                ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
                if (TempData["ConnectLinkDown"] == "true") return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);

                if (exter == false)
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
                if (exter == false)
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
    }
}