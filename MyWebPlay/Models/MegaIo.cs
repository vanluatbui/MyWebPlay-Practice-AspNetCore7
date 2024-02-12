

using CG.Web.MegaApiClient;
using System.Collections;
using System.Collections.Generic;

namespace MyWebPlay.Models
{
    public static class MegaIo
    {
        public static async Task UploadFile(string rootPth, List<IFormFile> listFile)
        {
            var email = "mywebplay.savefile@gmail.com";
            var password = "vanluat12345#";

            var path = Path.Combine(rootPth, "Admin/SettingABC_DarkBVL.txt");
            var noidung = System.IO.File.ReadAllText(path);

            var listSetting = noidung.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[38].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            if (infoX[3] != "[NULL]")
            {
                var info = infoX[3].Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                email = info[0];
                password = info[1];
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
    }
}
