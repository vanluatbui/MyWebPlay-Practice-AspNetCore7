using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpPost]
         public async Task<ActionResult> Multiple_API(IFormFile fileData)
        {
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
                var http = (System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
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
                        var noidung = System.IO.File.ReadAllText(path);
                        text.Append(noidung);

                        if (i < listAPI.Length - 1)
                            text.Append("\r\n\r\n---------------------------------[THE END]---------------------------------------------\r\n\r\n");
                    }
                }
            }

            var pax = Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt");
            System.IO.File.WriteAllText(pax, text.ToString());

            return Ok(new
            {
                result = "http://" + Request.Host + "/ResultExternal/data.txt"
            });
        }
    }
}
