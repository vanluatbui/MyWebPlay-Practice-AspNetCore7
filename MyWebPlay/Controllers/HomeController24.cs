using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using MyWebPlay.Extension;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpPost]
         public async Task<ActionResult> Multiple_API(IFormFile fileData)
        {
            ghilogrequest(null);
            var txtAPI = string.Empty;
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

            txtAPI = txtAPI.Replace("\r", "");
            var listAPI = txtAPI.Split("\n<||>\n");

            var text = new StringBuilder();
            for (var i = 0; i < listAPI.Length; i++)
            {
                var api = listAPI[i].Split("\n<**>\n");
                var http = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
                var url = http + "://" + Request.Host  + api[0];
                var body = api[1];

                var formData = new Dictionary<string, string>
                {
                    { "txtAPI", body }
                };


                var content = new FormUrlEncodedContent(formData);

                // Gửi yêu cầu POST
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonData = JsonConvert.DeserializeObject(responseContent);

                        var pay = jsonData.result.ToString().Replace("http://" + Request.Host + "/", "").Replace("%20", " ");
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, pay);
                        var noidung = FileExtension.ReadFile(path);
                        text.Append(noidung);

                        if (i < listAPI.Length - 1)
                            text.Append("\r\n\r\n---------------------------------[THE END]---------------------------------------\r\n\r\n");
                    }
                }
            }

            var pax = Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
            FileExtension.WriteFile(pax, text.ToString());

            return Ok(new
            {
                result = "http://" + Request.Host + "/ResultExternal/data.txt"
            });
        }

        [HttpPost]
        public ActionResult ExcuteKaraokeText(string nd)
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

                var encrypt = false;
                try
                {
                    StringMaHoaExtension.Decrypt(nd);
                    encrypt = true;
                }
                catch
                {
                    encrypt = false;
                }

                if (encrypt == true)
                {
                    nd = StringMaHoaExtension.Decrypt(nd);
                }

                var ndX = string.Empty;
                var tkKara = string.Empty;
                var hasSinger = "false";
                if (nd.Contains("<>") == false)
                {
                    ndX = nd;
                    tkKara = "";

                    if (hasSinger != "true")
                        hasSinger = "false";

                    return Ok(new
                    {
                        result = true,
                        text = ndX,
                        tk = tkKara,
                        singer = hasSinger
                    });
                }
                else
                {
                    var xa = nd.Replace("\r", "").Split("\n");
                    var noidung = "";
                    for (int i = 0; i < xa.Length; i++)
                    {
                        if (xa[i].Contains("<>"))
                        {
                            var xb = xa[i].Split("<>");
                            var xc = xb[1].Split("#");
                            var xd = xb[0].Split("-");
                            noidung += xc[0] + "=" + xd[0] + "=" + xd[1];

                            if (xd[0] == "[SINGER]")
                            {
                                hasSinger = "true";
                            }
                            else
                            {
                                if (hasSinger != "true")
                                    hasSinger = "false";
                            }

                            nd = nd.Replace(xb[0] + "<>", "");

                            if (i < xa.Length - 1)
                                noidung += "\n";
                        }
                    }
                    tkKara = noidung;

                    return Ok(new
                    {
                        result = true,
                        text = nd,
                        tk = noidung,
                        singer = hasSinger
                    });
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + "");
                return RedirectToAction("Error", new
                {
                    exception = "true"
                });
            }
        }
    }
}
