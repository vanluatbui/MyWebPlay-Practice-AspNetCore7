using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult String_Split_Regex()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
        public ActionResult String_Split_Regex(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var listIP = new List<string>();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                    {
                        HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
                if (exter == false)
                {
                    khoawebsiteClient(listIP);
                    HttpContext.Session.Remove("ok-data");

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
                }

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            }

            string dukien1 = f["DuKien1"].ToString();
            dukien1 = dukien1.Replace("[T-PLAY]", "\t");
            dukien1 = dukien1.Replace("[N-PLAY]", "\n");
            dukien1 = dukien1.Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            }

            string pattern = f["Pattern"].ToString();
            if (string.IsNullOrEmpty(pattern))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            }

            pattern = pattern.Replace("[T-PLAY]", "\t");
            pattern = pattern.Replace("[N-PLAY]", "\n");
            pattern = pattern.Replace("[R-PLAY]", "\r");

            string dukien2 = f["DuKien2"].ToString();
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            }

            dukien2 = dukien2.Replace("[T-PLAY]", "\t");
            dukien2 = dukien2.Replace("[N-PLAY]", "\n");
            dukien2 = dukien2.Replace("[R-PLAY]", "\r");

            string X = f["X"].ToString();

            X = X.Replace("[T-PLAY]", "\t");
            X = X.Replace("[N-PLAY]", "\n");
            X = X.Replace("[R-PLAY]", "\r");
            //if (string.IsNullOrEmpty(X))
            //{
            //    ViewData["Loi5"] = "Trường này không được để trống!";
            //    HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            //}

            string Y = f["Y"].ToString();
            Y = Y.Replace("[T-PLAY]", "\t");
            Y = Y.Replace("[N-PLAY]", "\n");
            Y = Y.Replace("[R-PLAY]", "\r");
            //if (string.IsNullOrEmpty(Y))
            //{
            //    ViewData["Loi6"] = "Trường này không được để trống!";
            //    HttpContext.Session.SetString("data-result", "true"); return this.String_Split_Regex();
            //}

            string[] DS = Regex.Split(chuoi, "\r\n");
            Regex regex = new Regex(pattern);

            string result = "\r\n";

            int stt = 0;
            for (int i = 0; i < DS.Length; i++)
            {
                string[] ds;
                if (int.Parse(dukien1) == 1)
                    ds = regex.Split(DS[i]);
                else
                {
                    ds = DS[i].Split(pattern.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                }

                int x, y;

                if (int.Parse(dukien2) == 1)
                {
                    x = int.Parse(X);

                    if (Y.ToCharArray()[0] == 'N')
                    {
                        if (Y.Length > 1 && Y.ToCharArray()[1] == '-')
                            y = ds.Length - int.Parse(Y.ToCharArray()[2].ToString());
                        else
                            y = ds.Length;
                    }
                    else
                        y = int.Parse(Y);
                }
                else
                if (int.Parse(dukien2) == 2)
                {
                    x = int.Parse(X);
                    y = ds.Length - int.Parse(Y);
                }
                else
               if (int.Parse(dukien2) == 3)
                {
                    x = int.Parse(X);
                    y = x + 1;
                }
                else
                //if (radioButton3.Checked == true)
                {
                    x = ds.Length - (int.Parse(Y) + 1);
                    y = ds.Length;
                }

                //int j = x;
                //while ((radioButton4.Checked != true && j < y) || (radioButton4.Checked == true && j >=y-1))
                //{
                //    txtOutput.Text += ds[j] + " ";
                //    if (radioButton4.Checked == true)
                //        j--;
                //    else
                //        j++;
                //}
                stt++;

                for (int j = x; j < y; j++)
                {
                    result += ds[j];
                    if (j < y - 1)
                        result += " ";
                }
                if (i < DS.Length - 1)
                    result += "\r\n";
            }

            //TextCopy.ClipboardService.SetText(result);

            //            string re = "\r\n" + result;

            //re = re.Replace("\r\n", "<br>");
            nix = result;
            result = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" +chim+ "_dataresult.txt"); TempData["fileResult"] = xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim+  "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";  if(TempData["ConnectLinkDown"] == "true")  return Redirect("/POST_DataResult/" + TempData["fileResult"]);

                if (exter == false)
                    return View();
                return Ok(new { result = nix.Replace("\r", "[R-PLAY]").Replace("\n", "[N-PLAY]").Replace("\t", "[T-PLAY]").Replace("\"", "[NGOACKEP]") });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }

        //--------------------------------------------

        [HttpGet]
        public ActionResult Check_Regex()
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
        public ActionResult Check_Regex(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var listIP = new List<string>();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSettingS[48].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[1] == "true")
                    exter = true;

                if (exter == false)
                {
                    TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } }
                    if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); }
                    if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
                    if (TempData["ClearWebsite"] == "true" /*|| TempData["UsingWebsite"] == "false" */)
                    {
                        HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                        TempData["skipIP"] = "true";
                    }
                    /*HttpContext.Session.Remove("ok-data");*/
                    TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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

                    var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                    if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
                    {
                        TempData["mau_background"] = "white";
                        TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
                        TempData["winx"] = "❤";
                    }
                    else
                    {
                        TempData["mau_background"] = "black";
                        TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
                        TempData["winx"] = "❤";
                    }
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                    var noidungX = System.IO.File.ReadAllText(pathX);
                    var listSetting = noidungX.Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
                }

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Check_Regex();
            }
           

            string pattern = f["Pattern"].ToString();
            pattern = pattern.Replace("[T-PLAY]", "\t");
            pattern = pattern.Replace("[N-PLAY]", "\n");
            pattern = pattern.Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(pattern))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Check_Regex();
            }

            while (pattern.Contains(@"\\") == true)
            {
                pattern = pattern.Replace(@"\\", @"\");
            }

            bool result = Regex.IsMatch(chuoi, pattern);
            
            ViewBag.KetQua = "Chuỗi : "+chuoi.Replace(" ","[Space]").Replace("\t","[Tab]")+" |-| Pattern : "+pattern.Replace(" ", "[Space]").Replace("\t", "[Tab]") + " |-| ==> KẾT QUẢ :  " + result;
                nix = ViewBag.KetQua;
                if (exter == false)
                    return View();
                return Ok(new { result = nix.Replace("\r", "[R-PLAY]").Replace("\n", "[N-PLAY]").Replace("\t", "[T-PLAY]").Replace("\"", "[NGOACKEP]") });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                if (exter == false)
                    return RedirectToAction("Error", new { exception = "true" });
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }
    }
}
