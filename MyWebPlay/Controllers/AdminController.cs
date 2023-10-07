using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Model;

namespace MyWebPlay.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult SettingXYZ()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            SettingAdmin settingAdmin = new SettingAdmin();
            settingAdmin.Topics = new List<SettingAdmin.Topic>();
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                settingAdmin.Topics.Add(new SettingAdmin.Topic(info[0], info[2], bool.Parse(info[1])));

                if (info[0] == "Password_Admin")
                    ViewBag.Password = info[3];
            }

            return View(settingAdmin);
        }

        [HttpPost]
        public ActionResult SettingXYZ(IFormCollection f)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/SettingABC.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
       
                        var xi = "false";
                        if (f[info[0]] == "on")
                        {
                            xi = "true";
                        }


                if (info[0] != "Password_Admin")
                    noidung = noidung.Replace(info[0] + "<3275>" + info[1], info[0] + "<3275>" + xi);
                else
                {
                    var xinh = f[info[0]];
                    if (string.IsNullOrEmpty(xinh))
                        xinh = "buivanluat-ADMIN3275";

                    noidung = noidung.Replace(listSetting[i], info[0] + "<3275>" + info[1] + "<3275>" + info[2] + "<3275>" + xinh);
                 }
            }
            System.IO.File.WriteAllText(path, noidung);
            return RedirectToAction("SettingXYZ");
        }
    }
}
