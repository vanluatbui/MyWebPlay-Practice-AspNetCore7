using AppLapBangChanTri;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System.Globalization;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult BangChanTri()
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

        void nhapbang(BT[] x, int t, int n, int w)
        {
            int k = 0, dem = 0;
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                if (dem == int.Parse(Math.Pow(2, n).ToString()) / int.Parse(Math.Pow(2, w).ToString()))
                {
                    if (k == 0)
                        k = 1;
                    else
                        k = 0;
                    dem = 0;
                }
                x[t].box[i] = k;
                dem++;
            }
        }

        BT[] x1;
        int n_BT = 0;

        int n1 = 0;

        String xemlai_BT = "";

        int xuatDSbieuthuc(BT[] x, int n)
        {
            int dem = 0;
            xemlai_BT = "\r\n* Danh sách biểu thức (BT) hiện có :\r\n";
            for (int i = 0; i < n; i++)
            {
                if (x[i].ten.Length != 0)
                {
                    xemlai_BT += "\r\n- Biểu thức " + (i + 1) + " : ";

                    xemlai_BT += x[i].ten;

                    dem++;
                }
            }
            return dem;
        }

        int xetluanly(int u, int v, int lc)
        {
            if (lc == 1)
            {
                if (u == 1 && v == 1)
                    return 1;
                return 0;
            }

            if (lc == 2)
            {
                if (u == 0 && v == 0)
                    return 0;
                return 1;
            }

            if (lc == 3)
            {
                if (u == 1 && v == 0)
                    return 0;
                return 1;
            }

            if (lc == 4)
            {
                if (u == 1 && v == 1 || u == 0 && v == 0)
                    return 1;
                return 0;
            }

            return 0;
        }

        void nhapbangBT1(BT[] x, int n, int t, int u)
        {
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                if (x[t].box[i] == 0)
                    x[u].box[i] = 1;
                else
                    x[u].box[i] = 0;
            }
        }

        void nhapbangBT2(BT[] x, int n, int t1, int t2, int u, int lc)
        {
            for (int i = 0; i < int.Parse(Math.Pow(2, n).ToString()); i++)
            {
                int k = xetluanly(x[t1].box[i], x[t2].box[i], lc);
                x[u].box[i] = k;
            }
        }

        

        [HttpPost]
        public ActionResult BangChanTri(IFormCollection f)
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
            chuoi = chuoi.Replace("[PHU]", "#");
            chuoi = chuoi.Replace("[HOI]", "Λ");
            chuoi = chuoi.Replace("[TUYEN]", "∨");
            chuoi = chuoi.Replace("[KEOTHEO]", "→");
            chuoi = chuoi.Replace("[TUONGDUONG]", "⇔");
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

            string bandau = f["ThuocTinh"].ToString();
            bandau = bandau.Replace("[T-PLAY]", "\t");
            bandau = bandau.Replace("[N-PLAY]", "\n");
            bandau = bandau.Replace("[R-PLAY]", "\r");
            if (string.IsNullOrEmpty(bandau))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.BangChanTri();
            }

            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.BangChanTri();
            }

            

            string[] bd = bandau.Split('-');
            n1 = bd.Length;

            if (x1 == null)
            {
                x1 = new BT[100];
                for (int i = 0; i < 100; i++)
                    x1[i] = new BT();
            }

            for (int i = 0; i < bd.Length; i++)
            {
                x1[n_BT].ten = bandau.Split('-')[i];
                nhapbang(x1, n_BT, n1, ++n_BT);
            }

            if (n_BT == n1)
            {
                // int dem = xuatDSbieuthuc(x1, n_BT);

                int lc = 0;
                string[] ch = chuoi.Replace("\r","").Split('\n');

                for (int i =0; i < ch.Length; i++)
                {
                    if (ch[i].Contains("#") == true)
                        lc = 0;
                    else
                         if (ch[i].Contains("Λ") == true)
                        lc = 1;
                    else
                         if (ch[i].Contains("∨") == true)
                        lc = 2;
                    else
                         if (ch[i].Contains("→") == true)
                        lc = 3;
                    else
                         if (ch[i].Contains("⇔") == true)
                        lc = 4;

                    if (lc == 0)
                    {
                        int t = int.Parse(ch[i].Split('#')[1]);
                        x1[n_BT].ten = "[ #";

                        x1[n_BT].ten += x1[t - 1].ten;
                        x1[n_BT].ten += " ]";

                        nhapbangBT1(x1, n1, t - 1, n_BT);
                        n_BT++;
                    }
                    else
                    {
                        int t1 = 0; 
                        int t2 = 0;

                        if (lc == 1)
                        {
                            t1 = int.Parse(ch[i].Split('Λ')[0]);
                            t2 = int.Parse(ch[i].Split('Λ')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 2)
                        {
                            t1 = int.Parse(ch[i].Split('∨')[0]);
                            t2 = int.Parse(ch[i].Split('∨')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 3)
                        {
                            t1 = int.Parse(ch[i].Split('→')[0]);
                            t2 = int.Parse(ch[i].Split('→')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }
                        else
                        if (lc == 4)
                        {
                            t1 = int.Parse(ch[i].Split('⇔')[0]);
                            t2 = int.Parse(ch[i].Split('⇔')[1]);
                            x1[n_BT].ten = "[ " + x1[t1 - 1].ten;
                        }


                        if (lc == 1)
                            x1[n_BT].ten += " Λ " + x1[t2 - 1].ten + " ]";
                        else
                             if (lc == 2)
                            x1[n_BT].ten += " ∨ " + x1[t2 - 1].ten + " ]";
                        else
                        if (lc == 3)
                            x1[n_BT].ten += " →  " + x1[t2 - 1].ten + " ]";
                        else
                            x1[n_BT].ten += " ⇔ " + x1[t2 - 1].ten + " ]";

                        nhapbangBT2(x1, n1, t1 - 1, t2 - 1, n_BT, lc);
                        n_BT++;
                    }
                }  
               
            }

            int dem = xuatDSbieuthuc(x1, n_BT);
            string result = "\r\n";
            for (int i = 0; i < dem; i++)
                result += "BT" + (i + 1) + "\t\t";

            result += "\r\n\r\n";

            for (int i = 0; i < int.Parse(Math.Pow(2, n1).ToString()); i++)
            {
                for (int j = 0; j < dem; j++)
                    result += x1[j].box[i] + "\t\t";
                result += "\r\n";
            }

            //TextCopy.ClipboardService.SetText(xemlai_BT + "\r\n\r\n\r\n" + result.Replace("\t\t","\t"));

            string re = xemlai_BT + "\r\n\r\n\r\n"  + result;
            var nix = re;
            re = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + re + "</textarea>";
            var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" +chim+ "_dataresult.txt"); TempData["fileResult"] = xuxu.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_" + chim+  "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = re;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";  if(TempData["ConnectLinkDown"] == "true")  return Redirect("/POST_DataResult/" + TempData["fileResult"]);

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
