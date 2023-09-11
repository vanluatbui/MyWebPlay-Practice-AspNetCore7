using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using System.Formats.Tar;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult CreateFile_TracNghiem()
        {
            khoawebsiteClient();

            if (ViewBag.HoanVi_VD == null)
                ViewBag.HoanVi_VD = "đều đúng\r\nđều sai\r\nA,B và C\r\nA và B\r\ntất cả\r\nđáp án";

            if (ViewBag.ChuoiVD == null)
                ViewBag.ChuoiVD = "\r\n\r\n\r\n                                               Câu số 1. 1 + 1 = ?\r\nChọn đáp án đúng :\r\nA. 1\r\nB. 2\r\n                                            C. 3\r\n\r\n\r\nD. 4\r\n\r\n                                       \r\nCâu số 2. Lan có 5 quả cam, Lan cho Hà 3 quả. Hỏi <span style=\"color:red\">Lan</span> \r\n                       \r\n\r\n                                    còn lại bao nhiêu quả cam?\r\nA. 4 quả\r\nB. 5 quả\r\nC. 2 quả                                    D. 1 quả\r\n\r\n\r\n             Câu số 3. Tìm x biết x - 10 = 20?\r\nA. x = 50       B. x = 60\r\n                              C. x = <span style=\"color:green\"> 30</span>\r\n                         \r\n\r\n                        D. x = 0\r\n\r\n\r\n\r\nCâu số 7. Hạnh phúc là gì?<br><img name=\"hinhcau3\" src=\"https://thegioisofa.com/wp-content/uploads/2022/07/Cung-song-tu.jpg\" alt=\"Image Error\" width=\"300\" height=\"500\" /><br>\r\nA. Là niềm vui\r\nLà tất cả\r\nLà nụ cười     B. Là hạnh phúc\r\n\r\nLà sự bình yênC. Là nụ cười\r\nD. Là tình yêu\r\n\r\n            Là những gì bạn đang mong ước\r\n\r\n\r\n\r\nCâu số 8. Tính diện tích hình vuông có cạnh là 5 cm?\r\nA. 5 cm<sup>2</sup>   B. 10 cm<sup>2</sup>C. 15 cm<sup>2</sup>               D. 25 cm<sup>2</sup>\r\n\r\n     Câu số 9. Chu vi hình vuông có cạnh 4 cm là?\r\n\r\nA. 16 cm\r\nB. 160 mm\t\tC. 1.6 dm\r\n\r\n           D. Tất cả đáp án đều đúng\r\n\t\t\t\r\n                      \r\n\r\n\r\n\r\n";

            if (ViewBag.CH_VD == null)
                ViewBag.CH_VD = "1-3.7-9";

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
                ViewBag.DapAn_VD = "B\r\nC\r\nC\r\nA\r\nD\r\nD";

            return View();
        }

        [HttpPost]
        public ActionResult CreateFile_TracNghiem(IFormCollection f)
        {
            string s = "\r\n" + f["txtChuoi"].ToString();
            bool err = true;
            var tick = f["Tick"].ToString();
            try
            {
                if (string.IsNullOrEmpty(f["txtNux"].ToString()))
                    ViewData["Loi"] = "Trường này là bắt buộc, bạn có thể click nút trên để xử lý...";

                var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo fx = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    fx.Delete();
                }

                // Xoá hết Enter... #3275#

               // String s = "\r\n" + f["txtChuoi"].ToString();

                // s = s.TrimStart('\n');

                //s += "\n" + s;

                s = s.Replace("\r\n", "#3275#");

                //cập nhật theo nhu cầu (trước tiên)...


                s = s.Replace(f["txtA"].ToString(), "\r\n");
                s = s.Replace(f["txtB"].ToString(), "\r\n");
                s = s.Replace(f["txtC"].ToString(), "\r\n");
                s = s.Replace(f["txtD"].ToString(), "\r\n");


                //......................................................................


                s = s.Replace("#3275#", "<br>");

               // System.IO.File.WriteAllText("D:/XemCode/ma.txt", s);

                // cập nhật txtDapAn...

                string d = f["txtDapAn"].ToString();
                d = d.TrimEnd("\r\n".ToCharArray()).TrimStart("\r\n".ToCharArray());

                string[] DX = Regex.Split(d, "\r\n");
        
                for (int i = 0; i < DX.Length; i++)
                {
                    if (DX[i].ToCharArray()[0] < 'A' || DX[i].ToCharArray()[0] > 'D')
                    {
                        ViewBag.KetQua = "<span style=\"color:red\">Đáp án của bạn cho từng câu hỏi chỉ được cung cấp trong khoảng A,B,C,D...</span>";
                        //this.Close();

                        ViewBag.HoanVi_VD = f["txtHoanVi"].ToString();

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

                if (DX.Length < int.Parse(f["txtNux"].ToString()))
                {
                    for (int i = DX.Length; i < int.Parse(f["txtNux"].ToString()); i++)
                        d += "\r\nA";
                }

                d = d.Replace("A", "1");
                d = d.Replace("B", "2");
                d = d.Replace("C", "3");
                d = d.Replace("D", "4");

                char[] dd = { '\r', '\n' };
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

                char[] ss = { '\r', '\n' };
                s = s.TrimStart(ss);
                s = s.TrimEnd(ss);

                char[] sk = { '<', 'b', 'r', '>' };
                s = s.TrimStart(sk);
                s = s.TrimEnd(sk);

                char[] sm = { ' ', '\r', '\n' };
                s = s.TrimStart(sm);

                char[] sx = { '\t', '\r', '\n' };
                s = s.TrimStart(sx);

                // Kiểm tra nếu dữ liệu không đủ hợp với đáp án :

                s = s.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");


                string[] dulieu = Regex.Split(s, "\r\n");

                //if (dulieu.Length / 5 != dapan.Length)
                //{
                //    ViewBag.KetQua = "<span style=\"color:red\">Số lượng câu hỏi của bạn trong dữ liệu không khớp với đáp án của bạn (lỗi do thiếu hoặc dư)...\r\n\r\n+ Số lượng dữ liệu câu hỏi : " + dulieu.Length / 5 + "\r\n+ Số lượng đáp án : " + dapan.Length + "</span>";
                //    //this.Close();

                //    ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                //    ViewBag.HoanVi_VD = f["txtHoanVi"].ToString();

                //    ViewBag.CH_VD = f["txtNum"].ToString();

                //    ViewBag.XCH_VD = f["txtX"].ToString();

                //    ViewBag.CHX_VD = f["txtXX"].ToString();

                //    ViewBag.A_VD = f["txtA"].ToString();

                //    ViewBag.B_VD = f["txtB"].ToString();

                //    ViewBag.C_VD = f["txtC"].ToString();

                //    ViewBag.D_VD = f["txtD"].ToString();

                //    ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                //    ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                //    return this.CreateFile_TracNghiem();
                //}


                // Phân tích những câu không cần hoán vị...

                string[] notSwap = f["txtNoSwap"].ToString().Split(".", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < notSwap.Length; i++)
                {
                    if (int.Parse(notSwap[i]) < 1 || int.Parse(notSwap[i]) > dapan.Length)
                    {
                        ViewBag.KetQua = "<span style=\"color:red\">Một trong những câu hỏi bạn cho là không cần hoán vị đáp án đã xảy ra lỗi index...</span>";
                        //this.Close();

                        ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                        ViewBag.HoanVi_VD = f["txtHoanVi"].ToString();

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

                string[] hoanvi = f["txtHoanVi"].ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < dulieu.Length; i = i + 5)
                {
                    for (int u = 0; u < hoanvi.Length; u++)
                    {
                        if (dulieu[i + 1].ToUpper().Contains(hoanvi[u].ToUpper()) == true || dulieu[i + 2].ToUpper().Contains(hoanvi[u].ToUpper()) == true
                            || dulieu[i + 3].ToUpper().Contains(hoanvi[u].ToUpper()) == true || dulieu[i + 4].ToUpper().Contains(hoanvi[u].ToUpper()) == true)
                        {
                            hv[i / 5] = 1;
                            break;
                        }
                    }

                    string CH;
                    if (hv[i / 5] == 1)
                        CH = "$" + dulieu[i];
                    else
                        CH = dulieu[i];

                    string DA = "";
                    if (tick != "on")
                    {
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
                    }
                    else
                    {
                        DA = "[<?" + k + "?>]";
                    }


                    copy += CH + "\n" + dulieu[i + 1] + "\n" + dulieu[i + 2] + "\n" + dulieu[i + 3] + "\n" + dulieu[i + 4] + "\n" + DA + "\n#\n";
                    k++;
                }

                char[] cc = { '\n', '#', '\n' };
                copy = copy.TrimEnd(cc);

                Calendar x = CultureInfo.InvariantCulture.Calendar;

                string xuxu = x.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string fi = Request.HttpContext.Connection.RemoteIpAddress + "_TracNghiem_" + xuxu + ".txt";
                fi = fi.Replace("\\", "");
                fi = fi.Replace("/", "");
                fi = fi.Replace(":", "");

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", fi);

                System.IO.File.WriteAllText(path, copy);

                //------------------------------------

                ViewBag.ChuoiVD = "\r\n\r\n\r\n                                               Câu số 1. 1 + 1 = ?\r\nChọn đáp án đúng :\r\nA. 1\r\nB. 2\r\n                                            C. 3\r\n\r\n\r\nD. 4\r\n\r\n                                       \r\nCâu số 2. Lan có 5 quả cam, Lan cho Hà 3 quả. Hỏi <span style=\"color:red\">Lan</span> \r\n                       \r\n\r\n                                    còn lại bao nhiêu quả cam?\r\nA. 4 quả\r\nB. 5 quả\r\nC. 2 quả                                    D. 1 quả\r\n\r\n\r\n             Câu số 3. Tìm x biết x - 10 = 20?\r\nA. x = 50       B. x = 60\r\n                              C. x = <span style=\"color:green\"> 30</span>\r\n                         \r\n\r\n                        D. x = 0\r\n\r\n\r\n\r\nCâu số 7. Hạnh phúc là gì?<br><img name=\"hinhcau3\" src=\"https://thegioisofa.com/wp-content/uploads/2022/07/Cung-song-tu.jpg\" alt=\"Image Error\" width=\"300\" height=\"500\" /><br>\r\nA. Là niềm vui\r\nLà tất cả\r\nLà nụ cười     B. Là hạnh phúc\r\n\r\nLà sự bình yênC. Là nụ cười\r\nD. Là tình yêu\r\n\r\n            Là những gì bạn đang mong ước\r\n\r\n\r\n\r\nCâu số 8. Tính diện tích hình vuông có cạnh là 5 cm?\r\nA. 5 cm<sup>2</sup>   B. 10 cm<sup>2</sup>C. 15 cm<sup>2</sup>               D. 25 cm<sup>2</sup>\r\n\r\n     Câu số 9. Chu vi hình vuông có cạnh 4 cm là?\r\n\r\nA. 16 cm\r\nB. 160 mm\t\tC. 1.6 dm\r\n\r\n           D. Tất cả đáp án đều đúng\r\n\t\t\t\r\n                      \r\n\r\n\r\n\r\n";

                ViewBag.CH_VD = "1-3.7-9";

                ViewBag.HoanVi_VD = "đều đúng\r\nđều sai\r\nA,B và C\r\nA và B\r\ntất cả\r\nđáp án";

                ViewBag.XCH_VD = "Câu số ";

                ViewBag.CHX_VD = ". ";

                ViewBag.A_VD = "A. ";

                ViewBag.B_VD = "B. ";

                ViewBag.C_VD = "C. ";

                ViewBag.D_VD = "D. ";

                ViewBag.NoSwap_VD = "2.5";

                ViewBag.DapAn_VD = "B\r\nC\r\nC\r\nA\r\nD\r\nD";

                if (tick != "on")
                {
                    //DateTime dt = DateTime.ParseExact(x.AddHours(DateTime.UtcNow, 7).ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string name = "[IP Khách : " + Request.HttpContext.Connection.RemoteIpAddress + ":" + Request.HttpContext.Connection.RemotePort + " | IP máy chủ : " + Request.HttpContext.Connection.LocalIpAddress + ":" + Request.HttpContext.Connection.LocalPort + "] - " + xuxu;

                    string host = "{"+Request.Host.ToString()+"}"
                        .Replace("http://","")
                    .Replace("https://", "")
                    .Replace("/", "");

                    SendEmail.SendMail2Step("mywebplay.savefile@gmail.com",
                 "mywebplay.savefile@gmail.com", host+" Save Temp Create Trac Nghiem File In " + name, copy, "teinnkatajeqerfl");
                }

                err = false;

                var so = "";
                if (tick == "on")
                    so = "Lưu ý : file trắc nghiệm của bạn hiện tại chưa có đáp án. Bạn có thể tự điều chỉnh lại đáp án của mình hoặc sử dụng tính năng cập nhật đáp án sau đó!";
                ViewBag.KetQua = "<p style=\"color:blue\">Thành công, một file TXT trắc nghiệm của bạn đã được xử lý...</p><a href=\"/tracnghiem/" + fi + "\" download>Click vào đây để tải về</a><br><p style=\"color:red\">Hãy nhanh tay tải về vì sau <span style=\"color:yellow\" id=\"thoigian1\" class=\"thoigian1\">30</span> giây nữa, file này sẽ bị xoá hoặc sẽ bị lỗi nếu có!<br>Nếu file tải về của bạn bị lỗi hoặc chưa kịp tải về, hãy refresh/quay lại trang này và thử lại...<br><span style=\"color:aqua\">Mặc dù file này đã được thông qua một số xử lý, tuy nhiên nó vẫn có thể xảy ra lỗi và sai sót không mong muốn. Vì vậy tạm thời bạn cứ tải file này về, sử dụng file này để làm bài trắc nghiệm và hệ thống sẽ thông báo vị trí của câu hỏi đang bị nghi ngờ là lỗi, bạn hãy mở file này và Ctrl + F để tìm câu hỏi đó, quan sát xung quanh tương tự và tự chỉnh sửa file thủ công sao cho thích hợp nhé!<br></span><span style=\"color:pink\">"+so+"</span></p>";
            }
            catch
            {
                ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                ViewBag.HoanVi_VD = f["txtHoanVi"].ToString();

                ViewBag.CH_VD = f["txtNum"].ToString();

                ViewBag.XCH_VD = f["txtX"].ToString();

                ViewBag.CHX_VD = f["txtXX"].ToString();

                ViewBag.A_VD = f["txtA"].ToString();

                ViewBag.B_VD = f["txtB"].ToString();

                ViewBag.C_VD = f["txtC"].ToString();

                ViewBag.D_VD = f["txtD"].ToString();

                ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                string s_err = "";
                    string[] err_s = Regex.Split(s, "\r\n");

                    int dem = 0;
                    for (int i = 0; i < err_s.Length; i++)
                    {
                        s_err += err_s[i] + "\r\n";
                        dem++;
                        if (dem == 5)
                        {
                            dem = 0;
                            s_err += "\r\n";
                        }
                    }

                    string find_Error = "<br><br><h3 style=\"color:aqua\">[ERROR] </h3><br><h3 style=\"color:red\">HỆ THỐNG GỢI Ý CHO BẠN NHỮNG CÂU HỎI/ĐÁP ÁN CÓ THỂ GÂY RA LỖI SẼ GIÚP BẠN XÁC ĐỊNH VÀ CHỈNH SỬA THỦ CÔNG (bằng cách Ctrl + F) - THỰC HIỆN LẠI (Nếu vẫn chưa giải quyết được vấn đề, bạn có thể xem thử bản nháp đã được phân tích hiện tại ở cuối trang web)... </h3>";

                    for (int i = 0; i < err_s.Length; i = i + 5)
                    {
                        if (i % 5 == 0)
                            err_s[i] = Regex.Replace(err_s[i], "[0-9]", "?");

                        if (i + 1 >= err_s.Length || i + 2 >= err_s.Length || i + 3 >= err_s.Length || i + 4 >= err_s.Length)
                            find_Error += "<br> + Câu hỏi/Đáp án Error (copy 1 phần) :  " + err_s[i] + "<br><br>";
                        else
                        if ((err_s[i + 1].Contains("?") || err_s[i + 1].Contains(":")) ||
                            (err_s[i + 2].Contains("?") || err_s[i + 2].Contains(":")) ||
                            (err_s[i + 3].Contains("?") || err_s[i + 3].Contains(":")) ||
                            (err_s[i + 4].Contains("?") || err_s[i + 4].Contains(":")))
                        {
                            find_Error += "<br> + Câu hỏi/Đáp án Error (copy 1 phần) :  " + err_s[i] + "<br><br>";
                        }
                    }

                    ViewBag.KetQua += find_Error;
                    ViewBag.KetQua += "<h3 style=\"color:pink\"> --> Trượt đến tận cuối trang để có thể xem thử lại bản nháp đã phân tích hiện tại (nếu muốn)...</h3><br><br>";
                    ViewBag.Temp_TN = "<h3 style=\"color:aqua\">[ERROR] </h3><br><h4 style=\"color:red\">LƯU Ý : Đây chỉ là bản nháp mà file trắc nghiệm đã phân tích được ở thời điểm hiện tại, việc phân tích của hệ thống dù thành công hay thất bại..., tại đây bạn có thể tự kiểm tra lại và thực hiện chỉnh sửa thủ công sau!</h4><br><p style=\"color:orange\">Lưu ý thêm : Dữ liệu này chỉ là bản nháp để giúp bạn có thể kiểm tra và xác định câu hỏi đang bị lỗi và bạn sẽ tự chỉnh sửa thủ công --> Không sử dụng lại dữ liệu này để phân tích lại file trắc nghiệm của bạn...</p><br><br><textarea cols=\"150\" rows=\"200\" >" + s_err + "</textarea>";

            }
            finally
            {
                if (err == true)
                {
                    ViewBag.ChuoiVD = f["txtChuoi"].ToString();

                    ViewBag.HoanVi_VD = f["txtHoanVi"].ToString();

                    ViewBag.CH_VD = f["txtNum"].ToString();

                    ViewBag.XCH_VD = f["txtX"].ToString();

                    ViewBag.CHX_VD = f["txtXX"].ToString();

                    ViewBag.A_VD = f["txtA"].ToString();

                    ViewBag.B_VD = f["txtB"].ToString();

                    ViewBag.C_VD = f["txtC"].ToString();

                    ViewBag.D_VD = f["txtD"].ToString();

                    ViewBag.NoSwap_VD = f["txtNoSwap"].ToString();

                    ViewBag.DapAn_VD = f["txtDapAn"].ToString();

                    string s_err = "";
                    string[] err_s = Regex.Split(s, "\r\n");

                    int dem = 0;
                    for (int i = 0; i < err_s.Length; i++)
                    {
                        s_err += err_s[i] + "\r\n";
                        dem++;
                        if (dem == 5)
                        {
                            dem = 0;
                            s_err += "\r\n";
                        }
                    }

                    string find_Error = "<br><br><h3 style=\"color:aqua\">[ERROR] </h3><br><h3 style=\"color:red\">HỆ THỐNG GỢI Ý CHO BẠN NHỮNG CÂU HỎI/ĐÁP ÁN CÓ THỂ GÂY RA LỖI SẼ GIÚP BẠN XÁC ĐỊNH VÀ CHỈNH SỬA THỦ CÔNG (bằng cách Ctrl + F) - THỰC HIỆN LẠI (Nếu vẫn chưa giải quyết được vấn đề, bạn có thể xem thử bản nháp đã được phân tích hiện tại ở cuối trang web)... </h3>";

                    for (int i = 0; i < err_s.Length; i = i + 5)
                    {
                        if (i % 5 == 0)
                            err_s[i] = Regex.Replace(err_s[i], "[0-9]", "?");

                        if (i +1 >= err_s.Length || i + 2 >= err_s.Length || i + 3 >= err_s.Length || i + 4 >= err_s.Length)
                            find_Error += "<br> + Câu hỏi/Đáp án Error (copy 1 phần) :  " + err_s[i] + "<br><br>";
                        else
                        if ((err_s[i + 1].Contains("?") || err_s[i + 1].Contains(":")) ||
                            (err_s[i + 2].Contains("?") || err_s[i + 2].Contains(":")) ||
                            (err_s[i + 3].Contains("?") || err_s[i + 3].Contains(":")) ||
                            (err_s[i + 4].Contains("?") || err_s[i + 4].Contains(":")))
                        {
                            find_Error += "<br> + Câu hỏi/Đáp án Error (copy 1 phần) :  " + err_s[i] + "<br><br>";
                        }
                    }

                    ViewBag.KetQua += find_Error;
                    ViewBag.KetQua += "<br><br><h3 style=\"color:aqua\">[ERROR] </h3><h3 style=\"color:pink\"> --> Trượt đến tận cuối trang để có thể xem thử lại bản nháp đã phân tích hiện tại (nếu muốn)...</h3><br><br>";
                    ViewBag.Temp_TN = "<h3 style=\"color:aqua\">[ERROR] </h3><br><h4 style=\"color:red\">LƯU Ý : Đây chỉ là bản nháp mà file trắc nghiệm đã phân tích được ở thời điểm hiện tại, việc phân tích của hệ thống dù thành công hay thất bại..., tại đây bạn có thể tự kiểm tra lại và thực hiện chỉnh sửa thủ công sau!</h4><br><p style=\"color:orange\">Lưu ý thêm : Dữ liệu này chỉ là bản nháp để giúp bạn có thể kiểm tra và xác định câu hỏi đang bị lỗi và bạn sẽ tự chỉnh sửa thủ công --> Không sử dụng lại dữ liệu này để phân tích lại file trắc nghiệm của bạn...</p><br><br><textarea cols=\"150\" rows=\"200\" >" + s_err + "</textarea>";
                }
            }
            return View();
        }

        public ActionResult XoaAllFile_X1()
        {
            var listFile = System.IO.Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem"));

                foreach (var file in listFile)
                {
                    FileInfo f = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "tracnghiem", file));
                    f.Delete();
                }
            return RedirectToAction("TracNghiemX_Multiple");
        }

        public ActionResult Menu()
        {
            return PartialView();
        }
    }
}
