using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult CreateFile_TracNghiem()
        {
            if (ViewBag.ChuoiVD == null)
                ViewBag.ChuoiVD = "\r\n\r\n\r\n                                               Câu số 1. 1 + 1 = ?\r\nChọn đáp án đúng :\r\nA. 1\r\nB. 2\r\n                                            C. 3\r\n\r\n\r\nD. 4\r\n\r\n                                       \r\nCâu số 2. Lan có 5 quả cam, Lan cho Hà 3 quả. Hỏi <span style=\"color:red\">Lan</span> \r\n                       \r\n\r\n                                    còn lại bao nhiêu quả cam?\r\nA. 4 quả\r\nB. 5 quả\r\nC. 2 quả                                    D. 1 quả\r\n\r\n\r\n             Câu số 3. Tìm x biết x - 10 = 20?\r\nA. x = 50       B. x = 60\r\n                              C. x = <span style=\"color:green\"> 30</span>\r\n                         \r\n\r\n                        D. x = 0\r\n\r\n\r\n\r\nCâu số 7. Hạnh phúc là gì?<br><br><img src=\"https://thegioisofa.com/wp-content/uploads/2022/07/Cung-song-tu.jpg\" alt=\"Image Error\"><br>\r\nA. Là niềm vui\r\nLà tất cả\r\nLà nụ cười     B. Là hạnh phúc\r\n\r\nLà sự bình yênC. Là nụ cười\r\nD. Là tình yêu\r\n\r\n            Là những gì bạn đang mong ước\r\n\r\n\r\n\r\nCâu số 8. Tính diện tích hình vuông có cạnh là 5 cm?\r\nA. 5 cm<sup>2</sup>   B. 10 cm<sup>2</sup>C. 15 cm<sup>2</sup>               D. 25 cm<sup>2</sup>\r\n\r\n\r\n\t\t\t\r\n                      \r\n\r\n\r\n\r\n";

            if (ViewBag.CH_VD == null)
                ViewBag.CH_VD = "1-3.7-8";

            if (ViewBag.XCH_VD == null)
                ViewBag.XCH_VD = "Câu số ";

            if (ViewBag.CHX_VD == null)
                ViewBag.CHX_VD = ". ";

            if (ViewBag.A_VD == null)
                ViewBag.A_VD = "A. ";

            if (ViewBag.B_VD == null)
                ViewBag.B_VD = "B. ";

            if (ViewBag.C_VD == null)
                ViewBag.C_VD = "C. ";

            if (ViewBag.D_VD == null)
                ViewBag.D_VD = "D. ";

            if (ViewBag.NoSwap_VD == null)
                ViewBag.NoSwap_VD = "2.5";

            if (ViewBag.DapAn_VD == null)
                ViewBag.DapAn_VD = "B\r\nC\r\nC\r\nA\r\nD";

            return View();
        }

        [HttpPost]
        public ActionResult CreateFile_TracNghiem(IFormCollection f)
        {

            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

            foreach (var file in listFile)
            {
                FileInfo fx = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                fx.Delete();
            }


            // Xoá hết Enter... #3275#

            String s = "\r\n" + f["txtChuoi"].ToString();

           // s = s.TrimStart('\n');

            //s += "\n" + s;

            s = s.Replace("\r\n", "#3275#");

            //cập nhật theo nhu cầu (trước tiên)...


            s = s.Replace(f["txtA"].ToString(), "\r\n");
            s = s.Replace(f["txtB"].ToString(), "\r\n");
            s = s.Replace(f["txtC"].ToString(), "\r\n");
            s = s.Replace(f["txtD"].ToString(), "\r\n");


            //......................................................................


            // s = s.Replace("#3275#", " ");

            // cập nhật txtDapAn...

            string d = f["txtDapAn"].ToString();

            string[] DX = Regex.Split(d, "\r\n");

            for (int i = 0; i < DX.Length; i++)
            {
                if (DX[i].ToCharArray()[0] < 'A' || DX[i].ToCharArray()[0] > 'D')
                {
                    ViewBag.KetQua = "<span style=\"color:red\">Đáp án của bạn cho từng câu hỏi chỉ được cung cấp trong khoảng A,B,C,D...</span>";
                    //this.Close();

                    ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                    ViewBag.CH_VD = f["txtNum"].ToString();

                    ViewBag.XCH_VD = f["txtX"].ToString();

                    ViewBag.CHX_VD = f["txtXX"].ToString();

                    ViewBag.A_VD = f["txtA"].ToString();

                    ViewBag.B_VD = f["txtB"].ToString();

                    ViewBag.C_VD = f["txtC"].ToString();

                    ViewBag.D_VD = f["txtD"].ToString();

                    ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                    ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                    return this.CreateFile_TracNghiem();
                }
            }

            d = d.Replace("A", "1");
            d = d.Replace("B", "2");
            d = d.Replace("C", "3");
            d = d.Replace("D", "4");

            char[] dd = { '\r','\n' };
            d = d.TrimStart(dd);
            d = d.TrimEnd(dd);

            string[] dapan = Regex.Split(d, "\r\n");

            string[] CH_STT = f["txtNum"].ToString().Split('.');

            // Cập nhật STT câu hỏi ...

            for (int i = 0; i < CH_STT.Length; i++)
            {
                string[] STT_CH = CH_STT[i].Split('-');
                for (int j = int.Parse(STT_CH[0]); j <= int.Parse(STT_CH[1]); j++)
                    s = s.Replace(f["txtX"].ToString() + (j).ToString() + f["txtXX"].ToString(), "\r\n");
            }

            s = s.Replace("#3275#", " ");

            // xoá các Enter thừa của dữ liệu

            do
            {
                s = s.Replace("\r\n\r\n", "\r\n");
            }
            while (s.IndexOf("\r\n\r\n") != -1);

            do
            {
                s = s.Replace("  ", " ");
            }
            while (s.IndexOf("  ") != -1);

            do
            {
                s = s.Replace("\t\t", "\t");
            }
            while (s.IndexOf("\t\t") != -1);

            char[] ss = {'\r','\n' };
            s = s.TrimStart(ss);
            s = s.TrimEnd(ss);

            char[] sm = { ' ','\r', '\n' };
            s = s.TrimStart(sm);

            // Kiểm tra nếu dữ liệu không đủ hợp với đáp án :

            string[] dulieu = Regex.Split(s,"\r\n");

            if (dulieu.Length / 5 != dapan.Length)
            {
                ViewBag.KetQua = "<span style=\"color:red\">Số lượng câu hỏi của bạn trong dữ liệu không khớp với đáp án của bạn (lỗi do thiếu hoặc dư)...\r\n\r\n+ Số lượng dữ liệu câu hỏi : " + dulieu.Length / 5 + "\r\n+ Số lượng đáp án : " + dapan.Length + "</span>";
                //this.Close();

                ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                ViewBag.CH_VD = f["txtNum"].ToString();

                ViewBag.XCH_VD = f["txtX"].ToString();

                ViewBag.CHX_VD = f["txtXX"].ToString();

                ViewBag.A_VD = f["txtA"].ToString();

                ViewBag.B_VD = f["txtB"].ToString();

                ViewBag.C_VD = f["txtC"].ToString();

                ViewBag.D_VD = f["txtD"].ToString();

                ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                return this.CreateFile_TracNghiem();
            }

            // Phân tích những câu không cần hoán vị...

            string[] notSwap = f["txtNoSwap"].ToString().Split('.');

            for (int i = 0; i<notSwap.Length;i++)
            {
                if (int.Parse(notSwap[i]) < 1 || int.Parse(notSwap[i]) > dapan.Length)
                {
                    ViewBag.KetQua = "<span style=\"color:red\">Một trong những câu hỏi bạn cho là không cần hoán vị đáp án đã xảy ra lỗi index...</span>";
                    //this.Close();

                    ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                    ViewBag.CH_VD = f["txtNum"].ToString();

                    ViewBag.XCH_VD = f["txtX"].ToString();

                    ViewBag.CHX_VD = f["txtXX"].ToString();

                    ViewBag.A_VD = f["txtA"].ToString();

                    ViewBag.B_VD = f["txtB"].ToString();

                    ViewBag.C_VD = f["txtC"].ToString();

                    ViewBag.D_VD = f["txtD"].ToString();

                    ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                    ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                    return this.CreateFile_TracNghiem();
                }
            }

            int[] hv = new int[dapan.Length];
            for (int i = 0; i < dapan.Length; i++)
                hv[i] = 0;

            if (f["txtNoSwap"].ToString().IndexOf(".") != -1)
            {
                for (int i = 0; i < notSwap.Length; i++)
                {
                    hv[int.Parse(notSwap[i]) - 1] = 1;
                }
            }

            // Hoàn chỉnh và copy nội dung sau khi phân tích...

            string copy = "";
            int k = 0;
            for (int i = 0; i < dulieu.Length; i = i + 5)
            {
                string CH;
                if (hv[i / 5] == 1)
                    CH = "$" + dulieu[i];
                else
                    CH = dulieu[i];

                string DA = "";
                if (int.Parse(dapan[k]) == 1)
                    DA = "[" + dulieu[i + 1] + "]";
                else
                    if (int.Parse(dapan[k]) == 2)
                    DA = "[" + dulieu[i + 2] + "]";
                else
                    if (int.Parse(dapan[k]) == 3)
                    DA = "[" + dulieu[i + 3] + "]";
                else
                    if (int.Parse(dapan[k]) == 4)
                    DA = "[" + dulieu[i + 4] + "]";

                copy += CH + "\n" + dulieu[i + 1] + "\n" + dulieu[i + 2] + "\n" + dulieu[i + 3] + "\n" + dulieu[i + 4] + "\n" + DA + "\n#\n";
                k++;
            }

            char[] cc = { '\n', '#', '\n' };
            copy = copy.TrimEnd(cc);

            Calendar x = CultureInfo.InvariantCulture.Calendar;

            string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string fi = Request.HttpContext.Connection.RemoteIpAddress + "_TracNghiem_" + xuxu + ".txt";
            fi = fi.Replace("\\","");
            fi= fi.Replace("/", "");
            fi = fi.Replace(":", "");

           var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", fi);

           System.IO.File.WriteAllText(path, copy);

            //------------------------------------

            ViewBag.ChuoiVD = "\r\n\r\n\r\n                                               Câu số 1. 1 + 1 = ?\r\nChọn đáp án đúng :\r\nA. 1\r\nB. 2\r\n                                            C. 3\r\n\r\n\r\nD. 4\r\n\r\n                                       \r\nCâu số 2. Lan có 5 quả cam, Lan cho Hà 3 quả. Hỏi <span style=\"color:red\">Lan</span> \r\n                       \r\n\r\n                                    còn lại bao nhiêu quả cam?\r\nA. 4 quả\r\nB. 5 quả\r\nC. 2 quả                                    D. 1 quả\r\n\r\n\r\n             Câu số 3. Tìm x biết x - 10 = 20?\r\nA. x = 50       B. x = 60\r\n                              C. x = <span style=\"color:green\"> 30</span>\r\n                         \r\n\r\n                        D. x = 0\r\n\r\n\r\n\r\nCâu số 7. Hạnh phúc là gì?<br><br><img src=\"https://thegioisofa.com/wp-content/uploads/2022/07/Cung-song-tu.jpg\" alt=\"Image Error\"><br>\r\nA. Là niềm vui\r\nLà tất cả\r\nLà nụ cười     B. Là hạnh phúc\r\n\r\nLà sự bình yênC. Là nụ cười\r\nD. Là tình yêu\r\n\r\n            Là những gì bạn đang mong ước\r\n\r\n\r\n\r\nCâu số 8. Tính diện tích hình vuông có cạnh là 5 cm?\r\nA. 5 cm<sup>2</sup>   B. 10 cm<sup>2</sup>C. 15 cm<sup>2</sup>               D. 25 cm<sup>2</sup>\r\n\r\n\r\n\t\t\t\r\n                      \r\n\r\n\r\n\r\n";

            ViewBag.CH_VD = "1-3.7-8";

                ViewBag.XCH_VD = "Câu số ";

                ViewBag.CHX_VD = ". ";

                ViewBag.A_VD = "A. ";

                ViewBag.B_VD = "B. ";

                ViewBag.C_VD = "C. ";

                ViewBag.D_VD = "D. ";

                ViewBag.NoSwap_VD = "2.5";

                ViewBag.DapAn_VD = "B\r\nC\r\nC\r\nA\r\nD";


            ViewBag.KetQua = "<p style=\"color:blue\">Thành công, một file TXT trắc nghiệm của bạn đã được xử lý...</p><a href=\"/tracnghiem/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:yellow\" id=\"thoigian\" class=\"thoigian\">20</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>Nếu file tải về của bạn bị lỗi hoặc chưa kịp tải về, hãy refresh/quay lại trang này và thử lại...<br><span style=\"color:aqua\">Mặc dù file này đã được thông qua một số xử lý, tuy nhiên nó vẫn có thể xảy ra lỗi và sai sót không mong muốn. Vì vậy tạm thời bạn cứ tải file này về, sử dụng file này để làm bài trắc nghiệm và hệ thống sẽ thông báo vị trí của câu hỏi đang bị nghi ngờ là lỗi, bạn hãy mở file này và Ctrl + F để tìm câu hỏi đó, quan sát xung quanh tương tự và tự chỉnh sửa file thủ công sao cho thích hợp nhé!</span></p>";

            return View();
        }

        public ActionResult XoaAllFile_X()
        {
                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    f.Delete();
                }
            return RedirectToAction("TracNghiem");
        }
    }
}
