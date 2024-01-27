using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Drawing;
using System;
using System.Formats.Tar;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using MyWebPlay.Extension;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PlayKaraoke()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
            ViewBag.Music = "";
            ViewBag.Musix = "";

            ViewBag.Karaoke = "karaoke";

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            return View();
        }

        [HttpPost]
        public ActionResult PlayKaraoke(IFormCollection f, IFormFile txtKaraoke, IFormFile txtMusic, IFormFile txtMusix)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
            var pathX1  = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX1);
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
            if (txtMusic == null || txtMusic.Length == 0)
                txtMusic = txtMusix;

            if (txtMusix == null || txtMusix.Length == 0)
                txtMusix = txtMusic;

            ViewBag.Karaoke = "";

            ViewBag.Show = "show";

            var chon = f["KaraChon"].ToString();

            if (chon == "1")
            {
                var r = new Random();
                int x = r.Next(10);

                ViewBag.Background = "/karaoke_Example/background/" + (x + 1) + ".jpg";
                ViewBag.SuDung = "";
            }
            else if (chon == "2")
            {
                var link = f["txtOnline"].ToString();
                ViewBag.Background = link;
                ViewBag.SuDung = "";
            }
            else if (chon == "3")
            {
                var link = f["txtOnline"].ToString();
                ViewBag.Background = link;
                ViewBag.SuDung = "Video";
            }
            else if (chon == "4")
            {
                var link = f["txtOnline"].ToString();
                link = link.Replace("&", "");
                link = link.Replace("loop", "");
                link = link.Replace("autoplay", "");
                link = link.Replace("controls", "");
                link = link.Replace("mute", "");
                link = link.Replace("youtu.be/", "youtube.com/embed/");
                link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                if (link.Contains("?"))
                    link += "&autoplay=1&loop=1&controls=0&mute=1";
                else
                    link += "?autoplay=1&loop=1&controls=0&mute=1";

                ViewBag.Background = link;
                ViewBag.SuDung = "Youtube";
            }

            if (f["txtChon"].ToString() != "on")
            {
                var fileName = Path.GetFileName(txtMusic.FileName);
                var nameFile = Path.GetFileName(txtMusix.FileName);

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", nameFile);

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtMusic.CopyTo(fileStream);
                }

                using (Stream fileStream = new FileStream(pathX, FileMode.Create))
                {
                    txtMusix.CopyTo(fileStream);
                }

                ViewBag.Music = "/karaoke/music/" + fileName;
                ViewBag.Musix = "/karaoke/music/" + nameFile;

                fileName = Path.GetFileName(txtKaraoke.FileName);

                path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fileName);

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtKaraoke.CopyTo(fileStream);
                }

                ViewBag.Karaoke = System.IO.File.ReadAllText(path);
            }
            else
            {
                ViewBag.Karaoke = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "VongTayNguoiAy_Text.txt"));
                ViewBag.Music = "/karaoke_Example/VongTayNguoiAy_Karaoke.mp3";
                ViewBag.Musix = "/karaoke_Example/VongTayNguoiAy.mp3";
            }

            return View();
        }

        public ActionResult CreateFile_Karaoke()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            ViewBag.LyricVD = "Tình như giấc mơ\r\nHãy giữ ai ơi cho giấc mơ còn đầy\r\nMột khi đã yêu đừng quên\r\nCon tim sẽ mang thêm bao ưu phiền.\r\nAnh hỡi chớ có thờ ơ\r\nYêu em với nụ cười đắm say\r\nBầu trời đẹp xinh\r\nYêu em với ánh mắt thắm thiết\r\nChứa chan những ân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\n[Empty]\r\nTình như giấc mơ\r\nHãy giữ ai ơi cho giấc mơ còn đầy\r\nMột khi đã yêu đừng quên\r\nCon tim sẽ mang thêm bao ưu phiền.\r\nAnh hỡi chớ có thờ ơ\r\nYêu em với nụ cười đắm say\r\nBầu trời đẹp xinh\r\nYêu em với ánh mắt thắm thiết\r\nChứa chan những ân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\nHãy cứ yêu trong sầu nhớ\r\nSẽ có những tiếng hát xanh ngời\r\nTình như khói mây, tình trong phút giây\r\nYêu trong bao chiều giông tố.\r\nRượu chưa uống nhưng tình đã say\r\nTình chưa đến xin đừng chớm bay\r\nSẽ có em mãi luôn mong chờ\r\nVòng tay người ấy với những chân tình.\r\n[Empty]";
            return View();
        }

        [HttpPost]
        public ActionResult CreateFile_Karaoke(IFormFile txtMusic, IFormCollection f)
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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

            TempData["Lyric"] = f["txtLyric"].ToString();
            TempData["Music"] = "/karaoke/music/"+txtMusic.FileName;

            var fileName = Path.GetFileName(txtMusic.FileName);

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                txtMusic.CopyTo(fileStream);
            }

            return RedirectToAction("PlayCreateFile_Karaoke");
        }

        public ActionResult PlayCreateFile_Karaoke()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
            if (TempData["ClearWebsite"] == "true" || TempData["UsingWebsite"] == "false")
            {
                HttpContext.Session.Remove("userIP"); HttpContext.Session.SetString("userIP", "0.0.0.0");
                TempData["skipIP"] = "true";
            }
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
            ViewBag.KaraX = "";
            return View();
        }

        [HttpPost]
        public ActionResult PlayCreateFile_Karaoke(IFormCollection f)
        {
            /*HttpContext.Session.Remove("ok-data");*/ TempData["dataPost"] = "[POST]"; HttpContext.Session.SetString("data-result", "true");
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
            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string fi = HttpContext.Session.GetString("userIP") + "_Karaoke_" + xuxu + ".txt";
            fi = fi.Replace("\\", "");
            fi = fi.Replace("/", "");
            fi = fi.Replace(":", "");

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fi);

            System.IO.File.WriteAllText(path, f["txtLyric"].ToString().Replace("undefined","").Replace(" *","*"));
            ViewBag.KaraX = "OK";
            ViewBag.FileKaraoke = "<p style=\"color:blue\">Thành công, một file TXT Karaoke của bạn đã được xử lý...</p><a href=\"/karaoke/text/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:deeppink\" id=\"thoigian3\" class=\"thoigian3\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>";
            return View();
        }

        public ActionResult XoaKaraoke(string? id)
        {
            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            return RedirectToAction("Share_Karaoke", new { id = id });
        }

        public ActionResult PlayKaraokeX()
        {
             TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true";  if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi");  if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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

            if (TempData["url"] == null)
                TempData["url"] = "[NOT]";

            if (TempData["baihat"] == null)
                TempData["baihat"] = "[NOT]";

            if (TempData["background"] == null)
                TempData["background"] = "[NOT]";

            if (TempData["option"] == null)
                TempData["option"] = 0;

            string? url = TempData["url"].ToString();
            string? baihat = TempData["baihat"].ToString();
            string? background = TempData["background"].ToString();
            int? option = int.Parse(TempData["option"].ToString());

            var n1 = StringMaHoaExtension.Encrypt(url);
            var n2 = StringMaHoaExtension.Encrypt(baihat);
            var n3 = StringMaHoaExtension.Encrypt(background);

            var share = n1 + "-.-" + n2 + "-.-" + n3 + "-.-" + option;
            ViewBag.LinkKaraoke = share;

            TempData["url"] = null;
            TempData["baihat"] = null;
            TempData["background"] = null;
            TempData["option"] = null;

            ViewBag.Music = "";
            ViewBag.Musix = "";

            ViewBag.Karaoke = "karaoke";

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            if (url != null && baihat != null && background != null && option != null
                && url != "[NOT]" && baihat != "[NOT]" && background != "[NOT]" && option != 0
                && url != "" && baihat != "" && background != "" && option != 0)
            {
                url = url.Replace("http://", "");
                url = url.Replace("http://", "");
                url = url.Replace("/", "");

                ViewBag.Server = url;
                ViewBag.BaiHatSV = baihat;

                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead("http://" + url + "/MyListSong.txt");
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();
                    ViewBag.ListSong = content;
                    ViewBag.Background = background;
                    ViewBag.Option = option;
                    ViewBag.Share = "YES";
                }
                catch
                {
                    ViewBag.Share = "ERROR";
                }

            }
            else
            if (url != null && url != "[NOT]" && url != "")
            {
                url = url.Replace("http://", "");
                url = url.Replace("http://", "");
                url = url.Replace("/", "");

                ViewBag.Server = url;
                ViewBag.BaiHatSV = baihat;

                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead("http://" + url + "/MyListSong.txt");
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();
                    ViewBag.ListSong = content;
                    ViewBag.Share = "OK";
                }
                catch
                {
                    ViewBag.Share = "ERROR";
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult PlayKaraokeX(IFormCollection f, IFormFile txtKaraoke, IFormFile txtMusic, IFormFile txtMusix)
        {
            TempData["urlCurrent"] = Request.Path.ToString().Replace("/Home/", ""); khoawebsiteClient(null); if (TempData["TestTuyetDoi"] == "true") TempData["TestTuyetDoi"] = "true"; if (HttpContext.Session.GetString("TuyetDoi") != null) { TempData["UyTin"] = "true"; var td = HttpContext.Session.GetString("TuyetDoi"); if (td == "true") { TempData["TestTuyetDoi"] = "true"; /*return View();*/ } else { TempData["TestTuyetDoi"] = "false"; } } if (TempData["tathoatdong"] == "true") { return RedirectToAction("Error"); } if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP"); if (HttpContext.Session.GetString("userIP") == "0.0.0.0" && TempData["skipOK"] == "false") HttpContext.Session.Remove("userIP");
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
            var pathX1 = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX1);
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

            if (txtMusic == null || txtMusic.Length == 0)
                txtMusic = txtMusix;

            if (txtMusix == null || txtMusix.Length == 0)
                txtMusix = txtMusic;

            ViewBag.Host = Request.Host;

            ViewBag.Karaoke = "";
            ViewBag.Share = f["txtShare"] + " - OK";

            ViewBag.Show = "show";

            ViewBag.LoginServer = f["txtServer"].ToString();
            ViewBag.BHServer = f["txtSong"].ToString();

            var chon = f["KaraChon"].ToString();

            ViewBag.Option = chon;

            if (chon == "1")
            {
                var r = new Random();
                int x = r.Next(10);

                ViewBag.Background = "/karaoke_Example/background/" + (x + 1) + ".jpg";
                ViewBag.SuDung = "";
            }
            else if (chon == "2")
            {
                var link = f["txtOnline"].ToString();
                ViewBag.Background = link;
                ViewBag.SuDung = "";
            }
            else if (chon == "3")
            {
                var link = f["txtOnline"].ToString();
                ViewBag.Background = link;
                ViewBag.SuDung = "Video";
            }
            else if (chon == "4")
            {
                ViewBag.BehindYoutube = "YES";

                var link = f["txtOnline"].ToString();

                link = link.Replace("&", "");
                link = link.Replace("loop", "");
                link = link.Replace("autoplay", "");
                link = link.Replace("controls", "");
                link = link.Replace("mute", "");
                link = link.Replace("youtu.be/", "youtube.com/embed/");
                link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                if (link.Contains("youtube") == false)
                    ViewBag.Share = "ERROR";

                if (link.Contains("?"))
                    link += "&autoplay=1&loop=1&controls=0&mute=1";
                else
                    link += "?autoplay=1&loop=1&controls=0&mute=1";

                ViewBag.Background = link;
                ViewBag.SuDung = "Youtube";
            }
            else if (chon == "5")
            {
                ViewBag.BehindYoutube = "YES";

                var listYoutube = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "Video_Youtube", "randomlink.txt")).Split("\r\n");

                var rand = new Random();
                var link = listYoutube[rand.Next(0, listYoutube.Length)];

                link = link.Replace("&", "");
                link = link.Replace("loop", "");
                link = link.Replace("autoplay", "");
                link = link.Replace("controls", "");
                link = link.Replace("mute", "");
                link = link.Replace("youtu.be/", "youtube.com/embed/");
                link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

                if (link.Contains("youtube") == false)
                    ViewBag.Share = "ERROR";

                if (link.Contains("?"))
                    link += "&autoplay=1&loop=1&controls=0&mute=1";
                else
                    link += "?autoplay=1&loop=1&controls=0&mute=1";

                ViewBag.Background = link;
                ViewBag.SuDung = "Youtube";
            }


            if (f["txtThietKe"] == "on")
            {
                var lUser = new List<string>();
                var lMau = new List<string>();

                var NDTK = f["txtNDThietKe1"].ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < NDTK.Length; i++)
                {
                    var x = NDTK[i].Split("=");
                    lUser.Add(x[0]);
                    lMau.Add(x[1]);
                }

                var nd = "";
                var lIndex = f["txtNDThietKe2"].ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lIndex.Length; i++)
                {
                    var x = lIndex[i].Split("=");
                    var y = int.Parse(x[1]);
                    nd += x[0] + "=" + lUser[y] + "=" + lMau[y];
                    if (i < lIndex.Length - 1)
                        nd += "\r\n";
                }
                TempData["TK-KARA"] = nd;
            }
            else
            {
                TempData["TK-KARA"] = "";
            }

            if (f["txtOnlineServer"].ToString() == "on")
            {
                var server = f["txtServer"].ToString();
                var song = f["txtSong"].ToString();

                var url_txt = server + "/" + song + "/" + song + ".txt";
                var url_goc = server + "/" + song + "/" + song + "_Goc.mp3";
                var url_kara = server + "/" + song + "/" + song + ".mp3";

                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead("http://" + url_txt);
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();

                    ViewBag.Music = "http://" + url_kara;
                    ViewBag.Musix = "http://" + url_goc;
                    ViewBag.Karaoke = content;
                }
                catch
                {
                    ViewBag.Share = "ERROR";
                }
            }
            else
            if (f["txtChon"].ToString() != "on")
            {
                var fileName = Path.GetFileName(txtMusic.FileName);
                var nameFile = Path.GetFileName(txtMusix.FileName);

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", fileName);
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music", nameFile);

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtMusic.CopyTo(fileStream);
                }

                using (Stream fileStream = new FileStream(pathX, FileMode.Create))
                {
                    txtMusix.CopyTo(fileStream);
                }

                ViewBag.Music = "/karaoke/music/" + fileName;
                ViewBag.Musix = "/karaoke/music/" + nameFile;

                fileName = Path.GetFileName(txtKaraoke.FileName);

                path = Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text", fileName);

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    txtKaraoke.CopyTo(fileStream);
                }

                ViewBag.Karaoke = System.IO.File.ReadAllText(path);
            }
            else
            {
                ViewBag.Karaoke = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "VongTayNguoiAy_Text.txt"));
                ViewBag.Music = "/karaoke_Example/VongTayNguoiAy_Karaoke.mp3";
                ViewBag.Musix = "/karaoke_Example/VongTayNguoiAy.mp3";
            }

            var n1 = StringMaHoaExtension.Encrypt(ViewBag.LoginServer);
            var n2 = StringMaHoaExtension.Encrypt(ViewBag.BHServer);
            var n3 = StringMaHoaExtension.Encrypt(ViewBag.Background);
            ViewBag.LinkKaraoke = n1 + "-.-" + n2 + "-.-" + n3 + "-.-" + chon;
            return View();
        }
    }
}
