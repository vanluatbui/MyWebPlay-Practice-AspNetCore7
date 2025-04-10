﻿

using CG.Web.MegaApiClient;
using MyWebPlay.Extension;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using MyWebPlay.Model;

namespace MyWebPlay.Models
{
    public static class MegaIo
    {
        private static MegaApiClient megaClient = new MegaApiClient();

        public static async Task<bool> UploadFile(string rootPth, List<IFormFile> listFile)
        {
            if (!megaClient.IsLoggedIn)
            {
                var check = TestMegaIO(rootPth);
                if (check == false) return false;
            }
            else
            {
                using var memoryStream = new MemoryStream();

                foreach (var file in listFile)
                {
                    await file.CopyToAsync(memoryStream);
                    IEnumerable<INode> nodes = await megaClient.GetNodesAsync();
                    var root = nodes.First(x => x.Type == NodeType.Directory && x.Name == "SAVE FILE - MY WEB PLAY");

                    if (root == null)
                    {
                        throw new Exception("Mega folder not found.");
                    }

                    await megaClient.UploadAsync(memoryStream, file.FileName, root);
                }
            }

            return true;
        }

        public static bool TestMegaIO(string rootPth)
        {
            try
            {
                if (megaClient.IsLoggedIn)
                {
                    return true;
                }

                var email = "mywebplay.savefile@gmail.com";
                var password = "vanluat12345#";

                var path = Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(rootPth.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);

                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoX = listSetting[40].Replace("[Encrypted_3275]", "").Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoX[3] != "[NULL]")
                {
                    var info = infoX[3].Split("<5828>", StringSplitOptions.RemoveEmptyEntries);
                    email = StringMaHoaExtension.Decrypt(info[0], "32752262");
                    password = StringMaHoaExtension.Decrypt(info[1], "32752262");
                }

                megaClient.Login(email, password);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void Logout()
        {
            if (megaClient.IsLoggedIn)
            {
                megaClient.Logout();
            }
        }
    }
}
