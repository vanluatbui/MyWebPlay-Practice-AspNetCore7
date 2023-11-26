using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult SQL_CreateTable()
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
        public ActionResult SQL_CreateTable(IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            string dulieu = f["DuLieu"].ToString();

            TempData["dataPost"] = "[" + dulieu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
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
                    || info[0] == "Email_Note"))
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

            string tableX = f["Table"].ToString();
            string key = f["Key"].ToString();
           

            if (string.IsNullOrEmpty(tableX))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.SQL_CreateTable();
            }

            if (string.IsNullOrEmpty(key))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.SQL_CreateTable();
            }

            if (string.IsNullOrEmpty(dulieu))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.SQL_CreateTable();
            }

            dulieu = dulieu.Replace("[TAB-TPLAY]", "\t");
            dulieu = dulieu.Replace("[ENTER-NPLAY]", "\n");
            dulieu = dulieu.Replace("[ENTER-RPLAY]", "\r");

            key = key.Replace("[TAB-TPLAY]", "\t");
            key = key.Replace("[ENTER-NPLAY]", "\n");
            key = key.Replace("[ENTER-RPLAY]", "\r");

            tableX = tableX.Replace("[TAB-TPLAY]", "\t");
            tableX = tableX.Replace("[ENTER-NPLAY]", "\n");
            tableX = tableX.Replace("[ENTER-RPLAY]", "\r");

            string[] hx = Regex.Split(dulieu, "\r\n\r\n");
            string sql = "";

            string[] primary = key.Split('-');
            string[] table = tableX.Split('-');

            for (int u = 0; u < hx.Length; u++)
            {
                sql += "\r\n\r\nCREATE TABLE " + table[u] + "\r\n(\r\n   ";
                string[] s = Regex.Split(hx[u], "\r\n");
                List<string> k = new List<string>();
                List<string> f1 = new List<string>();
                List<string> f2 = new List<string>();
                List<string> fx = new List<string>();

                for (int i = 0; i < s.Length; i++)
                {
                    string[] ss = s[i].Split(' ');

                    if (i < int.Parse(primary[u]))
                        k.Add(ss[0]);

                    if (ss.Length > 2)
                    {
                        f1.Add(ss[2]);
                        f2.Add(ss[3]);
                        fx.Add(ss[0]);
                    }

                    sql += ss[0] + " " + ss[1] + ",\r\n   ";

                    if (i == int.Parse(primary[u]))
                    {
                        for (int j = 0; j < int.Parse(primary[u]); j++)
                        {
                            if (j == 0)
                                sql += "primary key (";

                            sql += k[j];

                            if (j != int.Parse(primary[u]) - 1)
                                sql += ", ";
                            else
                                sql += "),\r\n   ";
                        }
                    }
                }

                for (int i = 0; i < f1.Count(); i++)
                {
                    sql += "foreign key (" + fx[i] + ") references " + f1[i].Substring(1, f1[i].Length - 2) + "(" + f2[i] + ")\r\n   ";
                    sql += "ON UPDATE NO ACTION\r\n   ON DELETE NO ACTION,\r\n   ";
                }

                char[] v = ",\r\n   ".ToCharArray();
                sql = sql.TrimEnd(v);

                char[] vv = "\r\n\r\n".ToCharArray();
                sql = sql.TrimStart(v);

                sql += "\r\n)";
            }

            //TextCopy.ClipboardService.SetText(sql);

            //sql = "\r\n" + sql;
            //sql = sql.Replace("\r\n", "<br>");
            var nix = sql;
            sql = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var cuctac = soi.AddHours(DateTime.UtcNow, 7)+"_"+chim; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = sql;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        //-----------------------------------------

        [HttpGet]
        public ActionResult Cxap_CreateClass()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
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
                    || info[0] == "Email_Note"))
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

        List<string> DS_Kieudulieu = new List<string>();
        List<string> DS_thuoctinh = new List<string>();

        [HttpPost]
        public ActionResult Cxap_CreateClass(IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            string dulieu = f["DuLieu"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");

            TempData["dataPost"] = "[" + dulieu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
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
                    || info[0] == "Email_Note"))
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

            string tenclass = f["TenClass"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r");
            
            if (string.IsNullOrEmpty(tenclass))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.JSON_InsertDoc();
            }

            if (string.IsNullOrEmpty(dulieu))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.JSON_InsertDoc();
            }

            string[] DS = Regex.Split(dulieu, "\r\n");
            for (int i = 0; i < DS.Length; i++)
            {
                string[] ds = Regex.Split(DS[i], "-");

                DS_Kieudulieu.Add(ds[0]);

                DS_thuoctinh.Add(ds[1]);
            }

            string s = "\r\npublic class " + tenclass + "\r\n{\r\n";

            for (int i = 0; i < DS_Kieudulieu.Count; i++)
            {
                if (DS_Kieudulieu[i].Contains("#") == false)
                {
                    s += "      public " + DS_Kieudulieu[i] + " " + DS_thuoctinh[i] + " {get; set;}\r\n\r\n";
                }
                else
                {
                    string[] dx = DS_Kieudulieu[i].Split('#');
                    s += "      public " + dx[1] + " " + DS_thuoctinh[i] + " {get; set;}\r\n\r\n";
                }
            }

            s += "     public " + tenclass + "()\r\n     {\r\n";
            for (int i = 0; i < DS_Kieudulieu.Count; i++)
            {
                if (DS_Kieudulieu[i].Contains("<") == true)
                    s += "         this." + DS_thuoctinh[i] + " = new " + DS_Kieudulieu[i] + "();\r\n\r\n";
                else
                if (DS_Kieudulieu[i].Contains("#") == true)
                {
                    string[] dx = DS_Kieudulieu[i].Split('#');
                    s += "         this." + DS_thuoctinh[i] + " = new " + dx[1] + "();\r\n\r\n";
                }
                else
                if (DS_Kieudulieu[i].Contains("[,") == true)
                {
                    int index = DS_Kieudulieu[i].IndexOf("[,");
                    s += "         this." + DS_thuoctinh[i] + " = new " + DS_Kieudulieu[i].Substring(0, index) + "[100,100];\r\n\r\n";
                }
                else
                if (DS_Kieudulieu[i].Contains("[") == true)
                {
                    int index = DS_Kieudulieu[i].IndexOf("[");
                    s += "         this." + DS_thuoctinh[i] + " = new " + DS_Kieudulieu[i].Substring(0, index) + "[100];\r\n\r\n";
                }
            }
            s += "     }\r\n\r\n";

            s += "     public " + tenclass + " (";
            for (int i = 0; i < DS_Kieudulieu.Count; i++)
            {
                if (DS_Kieudulieu[i].Contains("#") == true)
                {
                    string[] dx = DS_Kieudulieu[i].Split('#');
                    s += dx[1] + " " + DS_thuoctinh[i];
                }
                else
                    s += DS_Kieudulieu[i] + " " + DS_thuoctinh[i];

                if (i < DS_Kieudulieu.Count - 1)
                    s += ", ";
            }

            s += ")\r\n     {\r\n";

            for (int i = 0; i < DS_Kieudulieu.Count; i++)
            {
                s += "         this." + DS_thuoctinh[i] + " = " + DS_thuoctinh[i] + ";\r\n\r\n";
            }
            s += "   }\r\n\r\n";
            s += "   public " + tenclass + " (" + tenclass + " " + tenclass.ToLower() + ")\r\n   {\r\n";
            for (int i = 0; i < DS_Kieudulieu.Count; i++)
                s += "         this." + DS_thuoctinh[i] + " = " + tenclass.ToLower() + "." + DS_thuoctinh[i] + ";\r\n\r\n";

            s += "   }\r\n";

            s += "}";

            //TextCopy.ClipboardService.SetText(s);

            //s = s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br>");
            var nix = s;
            s = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + s + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var cuctac = soi.AddHours(DateTime.UtcNow, 7)+"_"+chim; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = s;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        //---------------------------------------------------------------------

        [HttpGet]
        public ActionResult Cxap_InsertValueClass()
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
        public ActionResult Cxap_InsertValueClass (IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
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
            string dulieu = f["DuLieu"].ToString();

            TempData["dataPost"] = "[" + dulieu.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t") + "]";
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
                    || info[0] == "Email_Note"))
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

            string tenclass = f["TenClass"].ToString();
            

            string dukien1 = f["DuKien1"].ToString();
            string dukien2 = f["DuKien2"].ToString();

            if (string.IsNullOrEmpty(tenclass))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Cxap_InsertValueClass();
            }

            if (string.IsNullOrEmpty(dukien1))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Cxap_InsertValueClass();
            }
            if (string.IsNullOrEmpty(dukien2))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Cxap_InsertValueClass();
            }
            if (string.IsNullOrEmpty(dulieu))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                HttpContext.Session.SetString("data-result", "true"); return this.Cxap_InsertValueClass();
            }

            tenclass = tenclass.Replace("[TAB-TPLAY]", "\t");
            tenclass = tenclass.Replace("[ENTER-NPLAY]", "\n");
            tenclass = tenclass.Replace("[ENTER-RPLAY]", "\r");

            dulieu = dulieu.Replace("[TAB-TPLAY]", "\t");
            dulieu = dulieu.Replace("[ENTER-NPLAY]", "\n");
            dulieu = dulieu.Replace("[ENTER-RPLAY]", "\r");

            dukien1 = dukien1.Replace("[TAB-TPLAY]", "\t");
            dukien1 = dukien1.Replace("[ENTER-NPLAY]", "\n");
            dukien1 = dukien1.Replace("[ENTER-RPLAY]", "\r");

            dukien2 = dukien2.Replace("[TAB-TPLAY]", "\t");
            dukien2 = dukien2.Replace("[ENTER-NPLAY]", "\n");
            dukien2 = dukien2.Replace("[ENTER-RPLAY]", "\r");

            int d = 0;
            int c = 0;

            List<int> dang = new List<int>();
            int[,] dangObject = new int[200, 200];
            List<string> listObject = new List<string>();


            string[] ds1 = Regex.Split(dukien1, "-");
            for (int i  = 0; i< ds1.Length;i++)
                dang.Add(int.Parse(ds1[i]));

            string[] ds2 = Regex.Split(dukien2, ",");

            for (int i = 0;i < ds2.Length;i++)
            {
                string[] ds3 = ds2[i].Split('.');

                listObject.Add(ds3[0]);

                string[] ds4 = ds3[1].Split("-");
                for (int j = 0; j < ds4.Length; j++)
                {
                    dangObject[d, c] = int.Parse(ds4[j]);
                    c++;
                }
                d++;
                c = 0;
            }

            string[] value = Regex.Split(dulieu, "\r\n#\r\n");

            string ss = "\r\n";
            for (int i = 0; i < value.Length; i++)
            {
                int k = 0;
                int dong = 0;
                string[] vl = Regex.Split(value[i], "\r\n");
                int kk = 0;
                string s = "new " + tenclass + "(";
                for (int j = 0; j < vl.Length; j++)
                {
                    if (dang[k] == 1)
                    {
                        s += vl[j];
                    }
                    else if (dang[k] == 2)
                    {
                        s += "\"" + vl[j] + "\"";
                    }
                    else if (dang[k] == 3)
                    {
                        s += "new " + listObject[kk] + "(";

                        string[] val_object = Regex.Split(vl[j], "##");

                        for (int cot = 0; cot < val_object.Length; cot++)
                        {
                            if (dangObject[dong, cot] == 1)
                            {
                                s += val_object[cot];
                            }
                            else
                            if (dangObject[dong, cot] == 2)
                            {
                                s += "\"" + val_object[cot] + "\"";
                            }

                            if (cot < val_object.Length - 1)
                                s += ", ";
                        }
                        dong++;
                        s += ")";

                        kk++;
                    }

                    if (j < vl.Length - 1)
                        s += ", ";
                    k++;
                }

                s += ");";
                ss += s + "\r\n\r\n";
            }

            //TextCopy.ClipboardService.SetText(ss);

            //ss = ss.Replace("\r\n", "<br>");
            var nix = ss;
            ss = "<button id=\"click_copy\" onclick=\"copyResult()\"><b style=\"color:red\">COPY RESULT</b></button><br><br><textarea id=\"txtResultX\" style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + ss + "</textarea>";

              Calendar soi = CultureInfo.InvariantCulture.Calendar; var chim = HttpContext.Request.Path.ToString().Replace("/", "").Replace("Home",""); if (string.IsNullOrEmpty(chim)) chim = "Default"; var cuctac = soi.AddHours(DateTime.UtcNow, 7)+"_"+chim; var sox = Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult", cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "") + "_dataresult.txt"); TempData["fileResult"] = cuctac.ToString().Replace("\\", "").Replace("/", "").Replace(":", "")  + "_dataresult.txt"; new FileInfo(sox).Create().Dispose(); System.IO.File.WriteAllText(sox, nix); ViewBag.Result = ss;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }
    }
}
