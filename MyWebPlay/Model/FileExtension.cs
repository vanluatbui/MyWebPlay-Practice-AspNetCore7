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
                    var nan = new Random().Next(500 / 500, 300000 / 500 + 1) * 500;
                    Thread.Sleep(nan);
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
                    var nan = new Random().Next(500 / 500, 300000 / 500 + 1) * 500;
                    Thread.Sleep(nan);
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
                    var nan = new Random().Next(500 / 500, 300000 / 500 + 1) * 500;
                    Thread.Sleep(nan);
                    continue;
                }
            }
        }
    }
}
