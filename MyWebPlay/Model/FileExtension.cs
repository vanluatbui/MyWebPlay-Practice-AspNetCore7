namespace MyWebPlay.Model
{
    public static class FileExtension
    {
        public static string ReadFile(string path)
        {
            var delay = 1000;
            for(var i =0; i < 500; i++)
            {
                try
                {
                    return File.ReadAllText(path);
                }
                catch
                {
                    Thread.Sleep(delay);
                    delay *= 2;
                }
            }

            return "[Lỗi khi đọc file]";
        }

        public static void WriteFile(string path, string noidung)
        {
            var delay = 1000;
            for (var i = 0; i < 500; i++)
            {
                try
                {
                    File.WriteAllText(path, noidung);
                    return;
                }
                catch
                {
                    Thread.Sleep(delay);
                    delay *= 2;
                }
            }
        }

        public static void MoveFile(string path1, string path2)
        {
            var delay = 1000;
            for (var i = 0; i < 500; i++)
            {
                try
                {
                    File.Move(path1, path2);
                    return;
                }
                catch
                {
                    Thread.Sleep(delay);
                    delay *= 2;
                }
            }
        }
    }
}
