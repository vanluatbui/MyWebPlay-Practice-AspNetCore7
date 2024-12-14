using System.Globalization;
using MyWebPlay.Model;

namespace MyWebPlay.Extension
{
    public static class BuildProgram
    {
        public static void BuildProgramPlay(IHostEnvironment environment)
        {
            DeleteBin((IWebHostEnvironment)environment);
        }

        private static void DeleteBin(IWebHostEnvironment _webHostEnvironment)
        {
            var errorPath = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt");
            System.IO.File.WriteAllText(errorPath, "");

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == true)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Delete(true);

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == true)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Delete(true);

            var pthY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var ndY = System.IO.File.ReadAllText(pthY);
            var onoff = ndY.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

            //if (apidel == true)
            //{
            //    if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Exists == true)
            //        new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Delete(true);

            //    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "apiUpload")).Create();
            //}


            var noteLog = Path.Combine(_webHostEnvironment.WebRootPath, "note", "notelog.txt");
            if (System.IO.File.Exists(noteLog) == false)
            {
                System.IO.File.Create(noteLog);
            }

            if (onoff == "file_MO")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Delete(true);
            }
            else if (onoff == "file_TAT")
            {
                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "#fileclose")).Create();

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Exists == true)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "file")).Delete(true);
            }

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Delete(true);

            System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "ResultExternal", "data.txt"), "");

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "GetColorAtPicture")).Create();

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-gmail")).Create();

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "zip-result")).Create();

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem")).Create();

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Exists == false)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "note")).Create();

            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                f.Delete();
            }

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke")).Delete(true);

            if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Exists)
                new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Delete(true);

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "POST_DataResult")).Create();

            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/music")).Create();
            new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke/text")).Create();

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var nd = System.IO.File.ReadAllText(pth);
            var onoffY = nd.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[3];

            var infoFile = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"));

            if (onoffY == "file_TAT")
                infoFile = infoFile.Replace("file/", "#fileclose/");
            else
            if (onoffY == "file_MO")
                infoFile = infoFile.Replace("#fileclose/", "file/");

            var files = infoFile.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            var xuxu = x.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            for (int xx = 0; xx < files.Length; xx++)
            {
                if (files[xx] == "") continue;

                var fi = files[xx].Split("\t");

                var today = xuxu.Split("/");
                var hethan = fi[1].Split("/");

                var d1 = int.Parse(today[0]);
                var m1 = int.Parse(today[1]);
                var y1 = int.Parse(today[2]);

                var d2 = int.Parse(hethan[0]);
                var m2 = int.Parse(hethan[1]);
                var y2 = int.Parse(hethan[2]);

                if (SoSanh2Ngay(d1, m1, y1, d2, m2, y2) >= 0 || new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0])).Exists == false)
                {
                    FileInfo fx = new System.IO.FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, fi[0].TrimStart('/')));
                    fx.Delete();
                    infoFile = infoFile.Replace(fi[0] + "\t" + fi[1] + "\n", "");
                }

            }
            System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"), infoFile);
            try
            {
                XoaDirectoryNull("file", _webHostEnvironment);
            }
            catch (Exception ex)
            {
                
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "note", "textnote.txt");
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                Calendar xz = CultureInfo.InvariantCulture.Calendar;
                var dateFile = xz.AddHours(file.LastWriteTimeUtc, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split("/");
                var nowDate = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot",""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Split("/");

                int d1 = int.Parse(dateFile[0]) + 6;
                int m1 = int.Parse(dateFile[1]);
                int y1 = int.Parse(dateFile[2]);


                int d2 = int.Parse(nowDate[0]);
                int m2 = int.Parse(nowDate[1]);
                int y2 = int.Parse(nowDate[2]);

                if (SoSanh2Ngay(d1, m1, y1, d2, m2, y2) == -1)
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        private static void XoaDirectoryNull(string path, IWebHostEnvironment _webHostEnvironment)
        {
            var listFile = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetFiles();
            var folders = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();

            var listFolder = new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, path)).GetDirectories();
            foreach (var item in listFolder)
            {
                XoaDirectoryNull(path + "/" + item.Name, _webHostEnvironment);
            }

            if (listFile.Length == 0 && folders.Length == 0 && path != "file" && path != "#fileclose")
            {
                System.IO.Directory.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path), true);
            }
        }

        private static int SoSanh2Ngay(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            if (d1 == d2 && m1 == m2 && y1 == y2)
                return 0;

            if (y1 < y2)
                return -1;

            if (y1 == y2)
            {
                if (m1 == m2)
                {
                    if (d1 < d2)
                        return -1;
                }
                else
                if (m1 < m2)
                    return -1;
            }
            return 1;
        }
    }
}
