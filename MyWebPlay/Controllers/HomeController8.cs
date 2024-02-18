using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult SpecialX_OrderBy()
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
            ViewBag.ChuoiViDu = "1\tBùi Nguyễn Tú Dương\t01/01/2000\r\n2\tNguyễn Thị Xuân Hoài Anh\t02/01/2000\r\n3\tLê Trần Anh Huy\t03/01/2000\r\n4\tMyWebPlay Asp Net Core\t04/01/2000\r\n5\tLê Đức Anh\t05/01/2000\r\n6\tLê Anh Huy\t06/01/2000\r\n7\tLê Gia Khiêm\t07/01/2000\r\n8\tPhạm Lưu Văn Sang\t08/01/2000\r\n9\tLê Thảo Lâm\t09/01/2000\r\n10\tLý Ngọc Thảo\t10/01/2000\r\n11\tTrần Tú Anh\t11/01/2000\r\n12\tNguyễn Tấn Lộc\t12/01/2000\r\n13\tLê Ngọc Thảo\t13/01/2000\r\n14\tLê Minh Tuấn Anh\t14/01/2000\r\n15\tLê Xuân Thảo\t15/01/2000\r\n16\tPhạm Ngọc Hoài Anh\t16/01/2000\r\n17\tNguyễn Đại Lộc\t17/01/2000\r\n18\tTrần Đức Lộc\t18/01/2000\r\n19\tPhạm Đức Anh\t19/01/2000\r\n20\tPhạm Hoàng Lan\t20/01/2000\r\n21\tTrần Ngọc Khánh Xuân\t21/01/2000\r\n22\tĐỗ Tấn Huệ\t22/01/2000\r\n23\tKim Đức Anh\t23/01/2000\r\n24\tPhạm Hoàng Lan Anh\t24/01/2000\r\n25\tPhạm Thị Mỹ Linh\t25/01/2000\r\n26\tTrần Ngọc Lê Hoài Anh\t26/01/2000\r\n27\tTrần Ngọc Hoài Anh\t27/01/2000\r\n28\tPhạm Ngọc Hải Yến\t28/01/2000\r\n29\tTrần Ngọc Duy Anh\t29/01/2000\r\n30\tLê Tú Anh\t30/01/2000\r\n31\tTrần Xuân Anh\t31/01/2000\r\n32\tNguyễn Phạm Lê Hoài Anh\t01/02/2000\r\n33\tDương Thảo Lâm\t02/02/2000\r\n34\tPhạm Bình Hoài Anh\t03/02/2000\r\n35\tBùi Ngọc Tú\t04/02/2000\r\n36\tLê Anh Hào\t05/02/2000\r\n37\tPhạm Xuân Anh\t06/02/2000\r\n38\tTrần Phương Thảo\t07/02/2000";
            
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
        public ActionResult SpecialX_OrderBy(IFormCollection f)
        {
            var nix = "";
            var exter = false;
            var listIP = new List<string>();
            try
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
                var noidung = System.IO.File.ReadAllText(path);

                var listSettingS = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

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
                    //var listIP = new List<string>();

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
                }
            ViewBag.ChuoiViDu = "1\tBùi Nguyễn Tú Dương\t01/01/2000\r\n2\tNguyễn Thị Xuân Hoài Anh\t02/01/2000\r\n3\tLê Trần Anh Huy\t03/01/2000\r\n4\tMyWebPlay Asp Net Core\t04/01/2000\r\n5\tLê Đức Anh\t05/01/2000\r\n6\tLê Anh Huy\t06/01/2000\r\n7\tLê Gia Khiêm\t07/01/2000\r\n8\tPhạm Lưu Văn Sang\t08/01/2000\r\n9\tLê Thảo Lâm\t09/01/2000\r\n10\tLý Ngọc Thảo\t10/01/2000\r\n11\tTrần Tú Anh\t11/01/2000\r\n12\tNguyễn Tấn Lộc\t12/01/2000\r\n13\tLê Ngọc Thảo\t13/01/2000\r\n14\tLê Minh Tuấn Anh\t14/01/2000\r\n15\tLê Xuân Thảo\t15/01/2000\r\n16\tPhạm Ngọc Hoài Anh\t16/01/2000\r\n17\tNguyễn Đại Lộc\t17/01/2000\r\n18\tTrần Đức Lộc\t18/01/2000\r\n19\tPhạm Đức Anh\t19/01/2000\r\n20\tPhạm Hoàng Lan\t20/01/2000\r\n21\tTrần Ngọc Khánh Xuân\t21/01/2000\r\n22\tĐỗ Tấn Huệ\t22/01/2000\r\n23\tKim Đức Anh\t23/01/2000\r\n24\tPhạm Hoàng Lan Anh\t24/01/2000\r\n25\tPhạm Thị Mỹ Linh\t25/01/2000\r\n26\tTrần Ngọc Lê Hoài Anh\t26/01/2000\r\n27\tTrần Ngọc Hoài Anh\t27/01/2000\r\n28\tPhạm Ngọc Hải Yến\t28/01/2000\r\n29\tTrần Ngọc Duy Anh\t29/01/2000\r\n30\tLê Tú Anh\t30/01/2000\r\n31\tTrần Xuân Anh\t31/01/2000\r\n32\tNguyễn Phạm Lê Hoài Anh\t01/02/2000\r\n33\tDương Thảo Lâm\t02/02/2000\r\n34\tPhạm Bình Hoài Anh\t03/02/2000\r\n35\tBùi Ngọc Tú\t04/02/2000\r\n36\tLê Anh Hào\t05/02/2000\r\n37\tPhạm Xuân Anh\t06/02/2000\r\n38\tTrần Phương Thảo\t07/02/2000";


            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }

            string sapxep = f["SapXep"].ToString();
            sapxep = sapxep.Replace("[T-PLAY]", "\t");
            sapxep = sapxep.Replace("[N-PLAY]", "\n");
            sapxep = sapxep.Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }

            string tanggiam = f["TangGiam"].ToString().Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(tanggiam))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }

            string codinh = f["CoDinh"].ToString().Replace("[T-PLAY]", "\t").Replace("[N-PLAY]", "\n").Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(codinh))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Special_OrderBy();
            }

            string[] DS = Regex.Split(chuoi, "\r\n");

            string[] so = sapxep.Split('=');

            while (true)
            {
                int l = 0;

                string kq1 = String.Join("\n", DS);
                while (l < so.Length)
                {
                    for (int i = 0; i < DS.Length - 1; i++)
                    {
                        string[] phan1 = DS[i].Split('\t')[int.Parse(codinh)-1].Split(' ');
                        int x1 = 0;

                        if (so[l].Contains("N-") == true)
                        {
                            x1 = phan1.Length - int.Parse(so[l].Split('-')[1]);
                        }
                        else
                        {
                            x1 = int.Parse(so[l]);
                        }

                        string ss1 = "";
                        for (int j = 0; j < l; j++)
                        {
                            int soX = 0;
                            if (so[j].Contains("N-") == true)
                            {
                                soX = phan1.Length - int.Parse(so[j].Split('-')[1]);
                            }
                            else
                            {
                                soX = int.Parse(so[j]);
                            }
                            ss1 += phan1[soX];
                        }

                        for (int jk = i + 1; jk < DS.Length; jk++)
                        {
                            string[] phan2 = DS[jk].Split('\t')[int.Parse(codinh)-1].Split(' ');
                            int x2 = 0;

                            if (so[l].Contains("N-") == true)
                            {
                                x2 = phan2.Length - int.Parse(so[l].Split('-')[1]);
                            }
                            else
                            {
                                x2 = int.Parse(so[l]);
                            }

                            string ss2 = "";
                            for (int jj = 0; jj < l; jj++)
                            {
                                int soX = 0;
                                if (so[jj].Contains("N-") == true)
                                {
                                    soX = phan2.Length - int.Parse(so[jj].Split('-')[1]);
                                }
                                else
                                {
                                    soX = int.Parse(so[jj]);
                                }
                                ss2 += phan2[soX];
                            }

                            if (int.Parse(tanggiam) == 0)
                            {
                                if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) > 0)
                                {
                                    string t = DS[i];
                                    DS[i] = DS[jk];
                                    DS[jk] = t;
                                }
                            }
                            else
                            {
                                if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) < 0)
                                {
                                    string t = DS[i];
                                    DS[i] = DS[jk];
                                    DS[jk] = t;
                                }
                            }
                        }
                    }
                    l++;
                }
                string kq2 = String.Join("\n", DS);
                if (String.Compare(kq1, kq2) == 0)
                    break;
            }

            string dx = String.Join("\n", DS);
            //TextCopy.ClipboardService.SetText(dx);
            nix = dx;
            dx = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + dx + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var cuctac = soi.AddHours(DateTime.UtcNow, 7)+"_"+chim; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = dx;

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

        public ActionResult TronMau()
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

        //----------------------------------------------------------------


        public ActionResult Euler_X()
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

        //......................................................

        int timdinhbacle(int[,] g)
        {
            for (int i = 0; i < g.GetLength(0); i++)
            {
                int d = 0;
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    if (g[i,j] != 0)
                        d++;
                }
                if (d % 2 != 0)
                    return 0;
            }
            return 1;
        }

        int timdinhbacle_X(int[,] g)
        {
            int dem = 0;
            for (int i = 0; i < g.GetLength(0); i++)
            {
                int d = 0;
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    if (g[i, j] != 0)
                        d++;
                }
                if (d % 2 != 0)
                    dem++;
            }

            if (dem == 2)
                return 1;
            return 0;
        }

        int KiemTraDuongDi(int[,] g)
        {
            for (int i = 0; i < g.GetLength(0); i++)
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    if (g[i,j] != 0)
                        return 0;
                }
            return 1;
        }

        int dinhxuatphat(int[,] g)
        {
            for (int i = 0; i < g.GetLength(0); i++)
            {
                int d = 0;
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    if (g[i,j] != 0)
                        d++;
                }
                if (d > 0)
                    return i;
            }
            return -1;
        }

        int[,] DS;
        Stack<int> Euler = new Stack<int>();

        void TimDuongDi(int i)
        {
            for (int j = 0; j < DS.GetLength(1); j++)
            {
                //tìm con đường để đi tiếp từ đỉnh i
                if (DS[i,j] != 0)
                {
                    //khi ta đã tìm được đường j để đi tiếp từ đỉnh i thì ta phải xóa đường này (chứng tỏ đoạn đường từ i đến j ta đã đi qua rồi)
                    DS[i,j]= DS[j,i]= 0;
                    //gọi tiếp thuật đệ quy để đi tiếp con đường xuất phát từ đỉnh j và cứ như thế...
                    TimDuongDi(j);
                }
            }
            //khi tới đỉnh i nào đó không còn đường để đi tiếp,ta sẽ đưa đỉnh i này vào trong Stack.Sau đó sẽ được quay lui lại các đỉnh trước i (điều này là nhờ đệ quy ở trên) để tìm tiếp đường đi khác và cứ như thế...
            //Nên nhớ Stack hoạt động theo cơ chế vào sau ra trước...
            Euler.Push(i);
        }


        [HttpPost]
        public ActionResult Euler_X(IFormCollection f)
        {
            try
            {
                TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["locked-app"] == "true") return RedirectToAction("Error", "Home"); if (TempData["errorXY"] == "true") return Redirect("https://google.com"); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
            string chuoi = f["Chuoi"].ToString();
            chuoi = chuoi.Replace("[T-PLAY]", "\t");
            chuoi = chuoi.Replace("[N-PLAY]", "\n");
            chuoi = chuoi.Replace("[R-PLAY]", "\r");
            TempData["dataPost"] = "[" + chuoi.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
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

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Euler_X();
            }

            string dukien = f["DuKien"].ToString();
            dukien = dukien.Replace("[T-PLAY]", "\t");
            dukien = dukien.Replace("[N-PLAY]", "\n");
            dukien = dukien.Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Euler_X();
            }

            int chon = int.Parse(dukien);

            string[] s = chuoi.Split('\n');
            DS = new int[s.Length, s[0].Split(' ').Length];

            int d = 0, c = 0;
            for (int i = 0; i < s.Length; i++)
            {
                string[] ss = s[i].Split(' ');
                for (int j = 0; j < ss.Length; j++)
                {
                    DS[d, c] = int.Parse(ss[j]);
                    c++;
                }
                c = 0;
                d++;
            }

            string ketqua = "";

            if (chon ==1 && timdinhbacle(DS) == 0)
            {
                ketqua = "\r\n--> Đồ thị không có chu trình Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }
            else
                if (chon == 2 && timdinhbacle_X(DS) == 0)
            {
                ketqua = "\r\n--> Đồ thị không có đường đi Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }

            //chọn đỉnh x có bậc >0 để xuất phát
            int x = dinhxuatphat(DS);
            if (x == -1)
            {
                ketqua = "\r\n--> Đồ thị không có chu trình/đường đi Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }

            //Vì thuật toán của mình là xuất phát từ một đỉnh của đồ thị,rồi từ đỉnh đó đi theo con đường hiện có, dần dần cho tới các đỉnh khác,ta sẽ xóa hết con đường mà ta đã đi qua (vì mỗi con đường chỉ được phép đi một lần).Nhưng ở đây, việc ta xóa đường đi chỉ là để biết rằng đường đó ta đã đi qua chứ không phải muốn xóa thật.Nên hãy tạo ra đồ thị copy để việc xóa này không ảnh hưởng tới đồ thị gốc...
            int[,] t = DS;

            //Tiến hành khám phá các đường đi xuất phát từ đỉnh x tìm được
            TimDuongDi(x);

            //Trong đồ thị copy bây giờ không được tồn tại đường đi nào sau sự khám phá trên...
            if (KiemTraDuongDi(t) == 0)
            {
                ketqua = "\r\n--> Đồ thị không có chu trình/đường đi Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }

            //đỉnh đầu (xuất phát) và đỉnh cuối (điểm dừng) trong Stack phải trùng nhau mới được gọi là chu trình Euler...
            if (chon ==1 && Euler.ElementAt(Euler.Count-1) != Euler.ElementAt(0))
            {
                ketqua = "\r\n--> Đồ thị không có chu trình Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }

            // HOẶC : --- đỉnh đầu (xuất phát) và đỉnh cuối (điểm dừng) trong Stack phải khác nhau mới được gọi là đường đi Euler...
            if (chon ==2 && Euler.ElementAt(Euler.Count - 1) == Euler.ElementAt(0))
            {
                ketqua = "\r\n--> Đồ thị không có đường đi Euler!";
                ViewBag.Ketqua = ketqua;
                return View();
            }

            if (chon ==1)
            ketqua = "\r\n--> Chu trình Euler xuất phát từ đỉnh "+x+" là : ";
            else
                if (chon == 2)
                ketqua = "\r\n--> Đường đi Euler xuất phát từ đỉnh " + x + " là : ";

            while (Euler.Count != 0)
            {
                int xx = Euler.Pop();
                ketqua += xx + "";
                if (Euler.Count != 0)
                    ketqua += " -> ";
            }
            ViewBag.Ketqua = ketqua;
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
    }
}
