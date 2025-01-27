

using CG.Web.MegaApiClient;
using MyWebPlay.Extension;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyWebPlay.Models
{
    public static class MegaIo
    {
        public static async Task UploadFile(string rootPth, List<IFormFile> listFile)
        {
            var email = "mywebplay.savefile@gmail.com";
            var password = "vanluat12345#";

            var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[40].Replace("[Encrypted_3275]", "").Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                email = StringMaHoaExtension.Decrypt(info[0], "32752262");
                password = StringMaHoaExtension.Decrypt(info[1], "32752262");
            }

            using var memoryStream = new MemoryStream();
            var megaClient = new MegaApiClient();

            foreach (var file in listFile)
            {
                await file.CopyToAsync(memoryStream);
                await megaClient.LoginAsync(email, password);
                IEnumerable<INode> nodes = await megaClient.GetNodesAsync();
                var root = nodes.First(x => x.Type == NodeType.Directory && x.Name == "SAVE FILE - MY WEB PLAY");

                if (root == null)
                {
                    throw new Exception("Mega folder not found.");
                }

                await megaClient.UploadAsync(memoryStream, file.FileName, root);
                await megaClient.LogoutAsync();
            }
        }

        public static bool TestMegaIO(string rootPth)
        {
            try
            {
                var email = "mywebplay.savefile@gmail.com";
                var password = "vanluat12345#";

                var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", System.IO.File.ReadAllText(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = System.IO.File.ReadAllText(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[40].Replace("[Encrypted_3275]", "").Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[3] != "[NULL]")
                {
                    var info = infoX[3].Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                    email = StringMaHoaExtension.Decrypt(info[0], "32752262");
                    password = StringMaHoaExtension.Decrypt(info[1], "32752262");
                }

                var megaClient = new MegaApiClient();
                megaClient.Login(email, password);
                if (megaClient.IsLoggedIn == false) return false;
                megaClient.Logout();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
