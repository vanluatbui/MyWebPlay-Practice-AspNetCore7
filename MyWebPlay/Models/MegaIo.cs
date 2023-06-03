

using CG.Web.MegaApiClient;
using System.Collections;
using System.Collections.Generic;

namespace MyWebPlay.Models
{
    public static class MegaIo
    {
        public static async Task UploadFile(List<IFormFile> listFile)
        {
            using var memoryStream = new MemoryStream();
            var megaClient = new MegaApiClient();

            foreach (var file in listFile)
            {
                await file.CopyToAsync(memoryStream);
                await megaClient.LoginAsync("mywebplay.savefile@gmail.com", "vanluat12345#");
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
