using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        // Check link url exist and respone OK?

        public ActionResult DownloadFile_ClearWeb()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC_DarkBVL.txt");
            var noidung = System.IO.File.ReadAllText(path);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7);

            if (xuxu.Hour >= 6 && xuxu.Hour <= 17)
            {
                TempData["mau_background"] = "white";
                TempData["mau_text"] = "black"; TempData["mau_nen"] = "dodgerblue";
            }
            else
            {
                TempData["mau_background"] = "black";
                TempData["mau_text"] = "white"; TempData["mau_nen"] = "rebeccapurple";
            }

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[44].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
            var infoY = listSetting[3].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);


            if (infoY[1] == "false" || infoX[3] == "[NULL]")
                return RedirectToAction("Error");

            var listFile = infoX[3].Split(",", StringSplitOptions.RemoveEmptyEntries);
            ViewBag.CountFile = listFile.Length;

            for (int i = 0; i < listFile.Length; i++)
            {
                TempData["file-" + i] = listFile[i];
            }

                return View();
        }

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
