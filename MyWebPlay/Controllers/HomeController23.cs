using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult ExportCalculatorSTForMe()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                khoawebsiteClient(null);
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + "");
                return RedirectToAction("Error", new
                {
                    exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes " : "show-error(true)"
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult ExportCalculatorSTForMe(IFormCollection f, IFormFile fileData)
        {
            var nix = "";
            var exter = false;
            var linkdown = false;
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSettingS = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;
                var infoY = listSettingS[8].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (infoY[1] == "true") linkdown = true;

                var listIP = new List<string>();
                ghilogrequest(f); if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", "");
                    khoawebsiteClient(null);
                    var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
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

                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("userIP")) == false)
                        listIP.Add(HttpContext.Session.GetString("userIP"));
                    else
                    {
                        TempData["GetDataIP"] = "true";
                        return RedirectToAction("Index");
                    }
                }

                string chuoi = f["Chuoi"].ToString();
                chuoi = chuoi.Replace("[T-PLAY]", "\t");
                chuoi = chuoi.Replace("[N-PLAY]", "\n");
                chuoi = chuoi.Replace("[R-PLAY]", "\r");


                string locra = f["LocRa"].ToString();
                locra = locra.Replace("[T-PLAY]", "\t");
                locra = locra.Replace("[N-PLAY]", "\n");
                locra = locra.Replace("[R-PLAY]", "\r");

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
                    locra = apiValue[1].Replace("[Empty]", "").Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
                }

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";

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
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
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

                var sox = "";
                var cuctac = "";
                var chim = "";

                var s = "";

                if (string.IsNullOrEmpty(locra))
                {
                    var txtChuoi = chuoi.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                    var txtData = new List<STData>();

                    for (int i = 0; i < txtChuoi.Length; i++)
                    {
                        var span = txtChuoi[i].Split("\t");
                        var data = new STData();
                        for (int j = 0; j < span.Length; j++)
                        {
                            data.PJ = span[0];
                            data.TA = span[1];
                            data.PC = span[2];
                            data.ES = double.Parse(span[5]);
                            data.AL = double.Parse(span[6]);
                        }
                        txtData.Add(data);
                    }

                    var sortDataByTA = txtData.OrderBy(dat => dat.TA).ToList();
                    s = "PJ\tName\tMucDo\tES\tCode\tFix\tRun\tAll";

                    var name = "";
                    var pj = "";
                    for (int i = 0; i < sortDataByTA.Count(); i++)
                    {
                        if (name == sortDataByTA[i].TA)
                        {
                            continue;
                        }
                        else
                        {
                            name = sortDataByTA[i].TA;
                            pj = sortDataByTA[i].PJ;
                        }

                        var calES = sortDataByTA.Where(dat => dat.PJ == pj && dat.TA == name).Sum(dat => dat.ES);
                        var calESByCode = sortDataByTA.Where(dat => dat.PJ == pj && dat.TA == name && (dat.PC == "CODING" || dat.PC == "INVESTIGATE" || dat.PC == "FIX BUG")).Sum(dat => dat.AL);
                        var calESByFix = sortDataByTA.Where(dat => dat.PJ == pj && dat.TA == name && dat.PC.Contains("FIX") && dat.PC != "FIX BUG").Sum(dat => dat.AL);
                        var calESByRun = sortDataByTA.Where(dat => dat.PJ == pj && dat.TA == name && dat.PC == "RUN TEST").Sum(dat => dat.AL);


                        s += "\r\n" + pj + "\t" + name + "\t\t" + calES + "\t" + calESByCode + "\t" + calESByFix + "\t" + calESByRun + "\t" + (calESByFix + calESByCode + calESByRun);
                    }

                    nix = s;
                }
                else
                {
                    var txtChuoi = chuoi.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    var txtLocRa = locra.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    var locras = new List<string>();
                    foreach(var lr in txtLocRa)
                    {
                        var lrx = lr.Split('\t');
                        locras.Add(lrx[0] + "\t" + lrx[1]);
                    }

                    foreach (var lora in locras)
                    {
                        var lr1 = lora.Split("\t")[0];
                        var lr2 = lora.Split("\t")[1];

                        var cot0 = "";
                        var cot1 = "";
                        var cot2 = "";
                        var cot3 = "";

                        var cot4 = 0m;
                        var cot5 = 0m;
                        var cot6 = 0m;
                        var cot7 = 0m;
                        var cot8 = 0m;
                        var cot9 = 0m;
                        var cot10 = 0m;
                        var cot11 = 0m;
                        var cot12 = 0m;
                        var cot13 = 0m;
                        var cot14 = 0m;
                        var cot15 = 0m;
                        var cot16 = 0m;

                        foreach (var chi in txtChuoi)
                        {
                            var chix = chi.Split("\t");
                             if (lr1 == chix[1] && lr2 == chix[2])
                             {
                                 cot0 += chix[0] + " , ";
                                 cot1 = chix[1];
                                 cot2 = chix[2];
                                 cot3= chix[3];

                                  cot4 += decimal.Parse(chix[4]);
                                    cot5 += decimal.Parse(chix[5]);
                                        cot6 += decimal.Parse(chix[6]);
                                        cot7 += decimal.Parse(chix[7]);
                                        cot8 += decimal.Parse(chix[8]);
                                        cot9 += decimal.Parse(chix[9]);
                                        cot10 += decimal.Parse(chix[10]);
                                        cot11 += decimal.Parse(chix[11]);
                                        cot12 += decimal.Parse(chix[12]);
                                        cot13 += decimal.Parse(chix[13]);
                                        cot14 += decimal.Parse(chix[14]);
                                        cot15 += decimal.Parse(chix[15]);
                                        cot16 += decimal.Parse(chix[16]);
                                }
                        }

                        s += cot0 + "\t" + cot1 + "\t" + cot2 + "\t" + cot3 + "\t" + cot4 + "\t" + cot5 + "\t" + cot6 + "\t" + cot7 + "\t" + cot8 + "\t" + cot9 + "\t" + cot10 + "\t" + cot11 + "\t" + cot12 + "\t" + cot13 + "\t" + cot14 + "\t" + cot15 + "\t" + cot16 + "\r\n";
                    }

                    nix = s;
                }

                s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button>&nbsp;&nbsp;&nbsp;<a style=\"color:deeppink\" href=\""+  HttpContext.Request.Path.ToString() + "\">Làm mới</a><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";
                Calendar x = CultureInfo.InvariantCulture.Calendar;

                cuctac = x.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).ToString();
                chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home", "");
                if (string.IsNullOrEmpty(chim)) chim = "Default";

                sox = (f.ContainsKey("resultX") == false || f["resultX"] == "false") ? Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim + "_dataresult.txt") : Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
                TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim + "_dataresult.txt";
                new FileInfo(sox).Create().Dispose();
                FileExtension.WriteFile(sox, nix);
                ViewBag.Result = s;

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

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + "");
                ghilogrequest(f); if (exter == false)
                    return RedirectToAction("Error", new
                    {
                        exception = (((int)new Random().Next(0, 100)) % 2 == 0) ? "yes " : "show-error(true)"
                    });
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
            ghilogrequest(f); if (exter == false)
                return View();
            else
            {
                if (linkdown == true)
                    return Redirect("/Home/ViewFile?path=/POST_DataResult/" + TempData["fileResult"]);
                return Ok(new
                {
                    result = "http://" + Request.Host + "/POST_DataResult/" + TempData["fileResult"].ToString().Replace(" ", "%20")
                });
            }
        }

        public ActionResult OnOffAPIHtml(string? isEnabled)
        {
            var htmlAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.html");

            if (System.IO.File.Exists(htmlAPI))
            {
                if (isEnabled == "32752262")
                {
                    System.IO.File.SetAttributes(htmlAPI, FileAttributes.Normal);
                }
                else
                {
                    System.IO.File.SetAttributes(htmlAPI, FileAttributes.Hidden);
                }
            }

            return Ok(new { result = "Đã xử lý theo yêu cầu.", datetime = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7) });
        }

        [HttpPost]
        public ActionResult WriteNoteLog(string? text, IFormFile file)
        {
            var noteLog = Path.Combine(_webHostEnvironment.WebRootPath, "note", "notelog.txt");
            Calendar xz = CultureInfo.InvariantCulture.Calendar;
            var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));

            if (text == null)
            {
                text = "";
            }

            if (file != null && file.FileName.EndsWith(".txt"))
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string content = reader.ReadToEnd();
                    text += "\r\n\r\n" + content;
                }
             }

            var noidung = "[" + xuxuz + "]\r\n\r\n" + text.Replace("[T-PLAY", "\t").Replace("[R-PLAY", "\r").Replace("[N-PLAY", "\n") + "\r\n----------------------------------------------------------------------------\r\n";
            var read = FileExtension.ReadFile(noteLog);
            FileExtension.WriteFile(noteLog, read + "\r\n" + noidung);

            return Ok(new { result = "Đã xử lý thành công.", datetime = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7) });
        }

        [HttpPost]
        public ActionResult EncodeUrlData(string url)
        {
            return Ok(new { url = StringMaHoaExtension.Encrypt(url) });
        }

        public ActionResult RedirectUrlEncode(string? id, string code)
        {
            return Redirect(StringMaHoaExtension.Decrypt(code));
        }

        public class STData
        {
            public string PJ { get; set; }
            public string TA { get; set; }
            public string PC { get; set; }
            public double ES { get; set; }
            public double AL { get; set; }
        }

        [HttpPost]
        public ActionResult AddNewReplace(IFormFile file, string replace="false")
        {
            var noteLog = Path.Combine(_webHostEnvironment.WebRootPath, "note", "notelog.txt");
            Calendar xz = CultureInfo.InvariantCulture.Calendar;
            var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));

            if (file != null && file.FileName.EndsWith(".txt"))
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string content = reader.ReadToEnd();
                    var pathReplace = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Others", "ReplaceManager.txt");

                    if (replace == "false")
                    {
                        var readReplace = FileExtension.ReadFile(pathReplace);
                        FileExtension.WriteFile(pathReplace, readReplace + "\r\n" + content);
                    }
                    else
                    {
                        FileExtension.WriteFile(pathReplace, content);
                    }
                }
            }

            return Ok(new { result = "Đã xử lý thành công.", datetime = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7) });
        }

        public ActionResult ShowErrorLog()
        {
            var errorPath = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt");
            return Ok(new { message = FileExtension.ReadFile(errorPath) });
        }
    }
}
