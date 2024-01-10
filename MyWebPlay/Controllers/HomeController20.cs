using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System.Net;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult c2c73fb518fd41a153bb0615e91369d6()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "Play_EncodeUrl")
                {
                    if (info[1] == "false")
                        return RedirectToAction("Error");
                }

                if (info[0] == "Encode_Url")
                {
                        ViewBag.Link = info[3];
                        break;
                }
            }

            return View();
        }

        // Check link url exist and respone OK?

        public bool UrlIsValid(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000;
                request.Method = "HEAD";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400)
                    {
                        return true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510)
                    {
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
