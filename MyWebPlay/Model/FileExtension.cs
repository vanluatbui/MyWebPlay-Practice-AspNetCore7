namespace MyWebPlay.Model
{
    public static class FileExtension
    {
        public static string ReadFile(string path)
        {
            for(var i =0; i < 500; i++)
            {
                try
                {
                    return File.ReadAllText(path);
                }
                catch
                {
                    Thread.Sleep(500);
                    continue;
                }
            }

            return "[Lỗi khi đọc file]";
        }

        public static void WriteFile(string path, string noidung)
        {
            for (var i = 0; i < 500; i++)
            {
                try
                {
                    File.WriteAllText(path, noidung);
                    return;
                }
                catch
                {
                    Thread.Sleep(500);
                    continue;
                }
            }
        }

        public static void MoveFile(string path1, string path2)
        {
            for (var i = 0; i < 500; i++)
            {
                try
                {
                    File.Move(path1, path2);
                    return;
                }
                catch
                {
                    Thread.Sleep(500);
                    continue;
                }
            }
        }
    }
}
