using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;

namespace MyWebPlay.Controllers
{
    public partial class AdminController : Controller
    {
        public ActionResult ListWebPage(string kbn)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                if (string.IsNullOrEmpty(kbn))
                    return RedirectToAction("Error", "Home");

                khoawebsiteAdmin();
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "OFF")
                    return Redirect("https://google.com");
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace);
                return RedirectToAction("Error", new { exception = "true" });
            }

            if (kbn == "0")
            {
                TempData["KbnPage"] = "* Vui lòng tick chọn các page web không cho phép sử dụng ở bên dưới vả nhấn nút OK để áp dụng : ";
                ViewBag.PageKbn = "0";
            }
            else
            if (kbn == "1")
            {
                TempData["KbnPage"] = "* Vui lòng tick chọn các page web cho phép sử dụng ở bên dưới vả nhấn nút OK để áp dụng : ";
                ViewBag.PageKbn = "1";
            }


            var listPage = new List<string>();
            var ptha = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "ListPageSetting.txt");
            var page = System.IO.File.ReadAllText(ptha).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in  page)
            {
                listPage.Add(item);
            }
            return View(listPage);

        }
    }
}
