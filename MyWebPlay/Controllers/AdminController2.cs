using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;
using System.Web;
using Org.BouncyCastle.Security.Certificates;
using MyWebPlay.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class AdminController : Controller
    {
        public ActionResult XemXetShowSetting(string? code)
        {
            // if (HttpContext.Session.GetString("adminSetting") == null)
            //{
            //    return RedirectToAction("LoginSettingAdmin");
            //}

            if (string.IsNullOrEmpty(code) || code != "32752262")
                return RedirectToAction("Error", "Home", "Home");

            khoawebsiteAdmin();
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }
            if (TempData["locked-app"] == "true")
                return RedirectToAction("Error", "Home", "Home");

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

            if (onoff == "ADMINSETTING_OFF")
                return Redirect("https://google.com");

            HttpContext.Session.SetString("show-setting", "true");

            return RedirectToAction("LoginSettingAdmin", "Admin");
        }
        public ActionResult ListWebPage(string kbn)
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                if (string.IsNullOrEmpty(kbn))
                    return RedirectToAction("Error", "Home", "Home");

                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                if (kbn == "0")
                {
                    TempData["KbnPage"] = "* Vui lòng tick chọn các page web không cho phép sử dụng ở bên dưới và nhấn nút OK để áp dụng : ";
                    ViewBag.PageKbn = "0";
                }
                else
                if (kbn == "1")
                {
                    TempData["KbnPage"] = "* Vui lòng tick chọn các page web cho phép sử dụng ở bên dưới và nhấn nút OK để áp dụng : ";
                    ViewBag.PageKbn = "1";
                }

                var listPage = new List<string>();
                var ptha = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "ListPageSetting.txt");
                var page = FileExtension.ReadFile(ptha).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in page)
                {
                    listPage.Add(item);
                }
                return View(listPage);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

        }

        [HttpPost]
        public ActionResult KhoiPhucSetting_Admin(IFormCollection f)
        {
            try
            {
                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xin = Request.Path;
                    if (xin == "" || xin == "/" || xin == null) xin = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                    if (lockedApp[3].Contains(xin))
                    {
                        return RedirectToAction("Error", "Home", "Home");
                    }
                }
                var key = listSetting[60].Split("<3275>")[3];
                var pth = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                if (f["ID"].ToString() != StringMaHoaExtension.Decrypt(pth[0], key) || f["Key"].ToString() != StringMaHoaExtension.Decrypt(pth[1], key))
                {
                    return Ok(new
                    {
                        error = "Đã xảy ra lỗi, vui lòng thử lại sau !",
                        datetime = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7)
                    });
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);

                var ndSet = "OffWebsite_All<3275>false<3275><br />Tắt hoạt động web site (<b style=\"color:blue\">áp dụng tất cả <span style=\"color:green\">Home</span></b>, sẽ không thông báo và tự động chuyển hướng sang trang lỗi (<span style=\"color:red\">404</span>))\r\nUsing_Website<3275>false<3275><br />Hoạt động website <span style=\"color:blue\">[MASTER]</span> : sẽ không có tác dụng (sẽ luôn như là tắt) khi 3 setting \"Request data nhanh, chế độ tàng hình và bỏ qua xác minh [ADMIN] bên dưới được bật cùng lúc\".\r\nAlert_UsingWebsite<3275>false<3275><br />Thông báo khi website tắt hoạt động (nếu tắt thì sẽ chuyển hướng sang website tạm khác (*) và không giải thích gì thêm/user sẽ khó biết được vấn đề) - kể cả bị lock website bởi admin hoặc client nội bộ,...vv... Hãy thật cẩn thận khi muốn tắt setting này!\r\nClear_Website<3275>false<3275><br />[Admin] Chế độ tàng hình website (một số tính năng sẽ rất khó hoặc không thể sử dụng). Nếu bạn bật setting này, hệ thống sẽ không get IP của user (sẽ lấy mặc định : <span style=\"color:orangered\">0.0.0.0</span>).\r\nUsing_QuickData<3275>false<3275><br />[Admin] Áp dụng dịch vụ request POST excute data nhanh (nếu tắt sẽ được chuyển hướng đến website tạm khác (*)). Lưu ý, đối với kí tự sẽ tự động replace tượng trưng cho dấu ngoặc kép <span style=\"color:red\">\"</span>, bạn nên điền <span style=\"color:red\">[ngoackep_0000]</span> nhé!\r\nNotAlert_QuickData<3275>false<3275><br />[Admin] Bỏ qua bước xác minh - request POST excute data nhanh (chỉ dùng khi bạn bật setting UsingQuickData và chế độ tàng hình website - và phải bật hoạt động website)\r\nRandom_Layout<3275>false<3275><br />Bật sử dụng RandomLayout ngẫu nhiên có sẵn (dành cho luồng REQUEST QUICK POST DATA - admin)\r\nPost_Clipboard<3275>false<3275><br />Download nội dung kết quả POST từ file .txt (thay vì mặc định copy vào clipboard/giám thị hoạt động cho luồng gửi data quick request POST và chế độ tàng hình + không xác minh [ADMIN], còn bình thường cũng có thể áp dụng tất cả...)\r\nConnect_LinkDown<3275>false<3275><br />Chuyển hướng trang kết quả POST sang link file TXT result, có thể sử dụng cho cả dịch vụ Admin quick request data hoặc API (nhưng khuyên bạn nên hạn chế dùng tính năng này nếu dữ liệu của bạn có chứa kí tự UTF8/Tiếng Việt)\r\nOff_RandomTab<3275>false<3275><br />Tắt chế độ random view tab website\r\nAuto_Excute<3275>false<3275><br />Nếu các setting : request POST data quick, chế độ tàng hình và bỏ qua xác minh [ADMIN] trên được bật cùng lúc thì sau khi đến trang kết quả POST sẽ tự động copy/download result và chuyển hướng đến trang tạm khác (*)\r\nViewSite_Pattern<3275>false<3275><br />Bật chế độ giao diện pattern (không header)\r\nViewSite_Basic<3275>false<3275><br />Bật chế độ giao diện cơ bản - xấu tệ (cho phép website với mọi người)\r\nEmail_Upload_User<3275>false<3275><br />Thông báo email khi user upload file\r\nMegaIo_Upload_User<3275>false<3275><br />Lưu trữ các file upload của user tại Mega.Io\r\nEmail_TracNghiem_Create<3275>false<3275><br />Thông báo email bài trắc nghiệm được tạo\r\nEmail_TracNghiem_Update<3275>false<3275><br />Thông báo email bài trắc nghiệm đã update (không đảm bảo)\r\nEmail_Question<3275>false<3275><br />Thông báo email bài question được tạo\r\nEmail_User_Website<3275>false<3275><br />Thông báo email phát hiện user bật website\r\nEmail_User_Continue<3275>false<3275><br />Thông báo email phát hiện user tiếp tục sử dụng website\r\nBlock_RegistUsing<3275>false<3275><br />Tạm ngưng nhận đơn đăng kí sử dụng web site tiếp theo\r\nEmail_Note<3275>false<3275><br />Thông báo email note text (bản nháp ghi chú) mới\r\nSave_ComeHere<3275>false<3275><br />Save request of user/list request used (khi tắt các dữ liệu mới sẽ không được lưu trữ và các dữ liệu cũ vẫn sẽ được bảo toàn)\r\nMail_ReportUrl<3275>false<3275><br />Auto send mail with IP request come used (if data than <@@100@@>) - nếu bạn tắt thì dữ liệu vẫn sẽ bị khôi phục nhưng không gửi mail report\r\nPlay_EncodeUrl<3275>false<3275><br />Cho phép sử dụng chức năng nội bộ website từ link url được mã hoá (hãy nhập tiếp url hệ thống bạn muốn sử dụng <a id=\"encode-set\" href=\"#set-encode\">ở setting dưới</a>)\r\nOff_CallIP<3275>false<3275><br />Tắt kết nối tìm IP user (cho phép website với mọi người)\r\nAll_People<3275>false<3275><br />Cho phép website với mọi người (nhưng vẫn get thông tin IP; và IP vẫn sẽ bị khoá nếu có bởi client/admin)\r\nFullScreen_Web<3275>false<3275><br />[Trò đùa/Tự động web] Toàn màn hình khi sử dụng (chỉ khi user có sự tương tác mỗi khi truy cập), một số tính năng có thể bị hạn chế\r\nEmail_Karaoke<3275>false<3275><br />Thông báo email/lưu trữ dữ liệu bản tạo mới Karaoke của user (sẽ gửi 2 bản email : mp3 và text Karaoke)\r\nEmail_Karaoke_OnlyText<3275>false<3275><br />Nếu bạn đã bật setting nhận thông báo email khi user tạo bản mới Karaoke trên (chỉ nhận dữ liệu bản Text karaoke, không cần mp3) - nên bật cái này vì các bản mp3 dung lượng có thể lớn làm ảnh hưởng phát sinh\r\nChienDich_LocalStorage<3275>false<3275><br />Mở chiến dịch tự động xoá hết dữ liệu localStorage từ JS của local khách hàng đối với website khi truy cập/kể cả admin nếu có - chiến dịch chỉ hoạt động mỗi khi bị truy cập page <b style=\"color:red\">Error</b> (có nghĩa là : sau khi kết thúc chiến dịch, những dữ liệu của localStorage JS của admin/user đã từng lưu trữ sẽ bị xoá hoàn toàn khi được truy cập lại website vào lần tới)\r\nNotUse_Believe<3275>false<3275><br />Không cho phép sử dụng tính năng mật độ tuyệt đối và các IP tin tưởng được phép truy cập website\r\nTangHinh_UseUploadFile<3275>false<3275><br />[Khi bạn bật chế độ tàng hình web site] Khi bạn truy cập đến trang <span style=\"color:blue\">UploadFile</span>, sẽ tự động chuyển hướng sang trang upload file dành cho Admin. Sau đó thử click đại đâu đó bất kỳ cũng sẽ tự động mở cửa sổ chọn file và submit dữ liệu (chỉ <span style=\"color:red\">Gmail</span>) sau đó.\r\nLockedAll_Web<3275>false<3275><b style=\"color:brown\"><br />[Đặc biệt] Khoá trang web tuyệt đối/vĩnh viễn => error page (tất cả, không trang nào truy cập được, kể cả admin)</b>\r\nPassword_Admin<3275>true<3275><br />Mật khẩu admin (nếu để trống mật khẩu mặc định sẽ là : <b style=\"color:red\">mywebplay-ADMIN</b>)<3275>mywebplay-ADMIN\r\nCode_LockedClient<3275>true<3275><br />Nhập mật mã dự phòng (mã mở khoá dự phòng dành cho LockedWebClient - trừ khi user có thiết kế mật khẩu riêng, mặc định nếu để trống sẽ là <b style=\"color:red\">abc-xyz</b>).Thật ra cái này cũng nên hạn chế nói cho user biết...<3275>abc-xyz\r\nBelieve_IP<3275>true<3275><br />IP tin tưởng - bỏ qua xác minh và báo cáo hành động user (lưu ý vì có nhiều đối tượng có cùng nhóm, nội bộ và địa chỉ IP nên chỉ các thiết bị cần thiết nên bật <b>chế độ tin tưởng</b> để có sự tác dụng này, mỗi IP liệt kê phân biệt nhau bởi dấu phẩy (và bạn nên tự để ý coi chừng liệt kê trùng IP hoặc đã có sẵn nhé 😁) : nếu IP sự tin tưởng nào đã bị xoá khỏi DS bên dưới thì các thiết bị từng set IP tin tưởng này trước đây sẽ bị xoá - và xin lưu ý, nếu IP của bạn bị khoá bởi admin hay một số vấn đề khác vẫn có thể bị xác minh như thường), và cũng lưu ý tính năng này sẽ không thể tác dụng ngay lập tức, hãy vui lòng thử lại nhiều lần, xin lỗi vì sự bất tiện này 😎...<3275>[NULL]\r\nMatDoTuyetDoi<3275>true<3275><br />[Mật độ tuyệt đối] là phiên bản nâng cao của việc sử dụng trang web đối với thiết bị tin tưởng (vì điều này có phần quan ngại và có chút nguy hiểm ⚠, cho nên bạn <b style=\"color:red\">hãy suy nghĩ thật kỹ</b> trước khi áp dụng - mà thường sử dụng cho chính bản thân bạn là chính 😀; ở đây nó chỉ khác ở chỗ là admin sẽ khó khoá sử dụng hoặc xác nhận info (tự do), nhưng mọi hành động vẫn có thể gửi báo cáo về admin nếu muốn). Cách sử dụng : trình duyệt bạn muốn làm chế độ tin tưởng, F12 để mở giao diện DEV TOOL, sang Console bạn gõ lệnh : <b style=\"color:deeppink\">window.localStorage.setItem(\"<span style=\"color:green\">Key</span>\",\"<span style=\"color:green\">Value</span>\");</b>.<br>Trong đó, <span style=\"color:brown\">key&lt;&gt;value</span> tại đây nếu không nhập thì mặc định sẽ là <b style=\"color:blue\">matdotuyetdoi&lt;&gt;believix-123</b>. Nếu bạn vô tình thay đổi key hoặc value của mã mật độ khác tại setting này, các thiết bị đã set tin tưởng trước đó sẽ bị xoá...<3275>matdotuyetdoi<>believix-123\r\nEncode_Url<3275>true<3275><br /><a id=\"set-encode\" href=\"#encode-set\">Sử dụng chế độ tính năng bằng url thông qua trang web với link được mã hoá</a> (lưu ý chỉ riêng nội bộ hợp lệ của hệ thống <span style=\"color:deeppink\">mà cho phép sử dụng</span>. Giả sử bạn chỉ cần nhập : <span style=\"color:blue\">/Home/PlayTracNghiem</span>), hãy nhập tại đây chính xác và tồn tại một url [bắt đầu] của hệ thống mà bạn muốn sử dụng...<br />Bạn có giới hạn 10 link có thể sử dụng theo lần lượt (link 1 : <a target=\"_blank\" href=\"/Cover/Ee90d45ca0d59031d2a3b6dc488187c00\" style=\"color:red\">/Cover/Ee90d45ca0d59031d2a3b6dc488187c00</a>, link 2 : <a target=\"_blank\" href=\"/Cover/E41fb870a2ab7c2470cb35f51171e20ee\" style=\"color:red\">/Cover/E41fb870a2ab7c2470cb35f51171e20ee</a>, link 3 : <a target=\"_blank\" href=\"/Cover/E8ecb5a3a2b4fdbda327335d19f3ca7fa\" style=\"color:red\">/Cover/E8ecb5a3a2b4fdbda327335d19f3ca7fa</a>, link 4 : <a target=\"_blank\" href=\"/Cover/E003e16661a80c9451f46587afb2ec5c3\" style=\"color:red\">/Cover/E003e16661a80c9451f46587afb2ec5c3</a>, link 5 : <a target=\"_blank\" href=\"/Cover/Ef8e2d510133438f980423324078e0939\" style=\"color:red\">/Cover/Ef8e2d510133438f980423324078e0939</a>, link 6 : <a target=\"_blank\" href=\"/Cover/E72fd1425ee00a7192a7261c5b45fcab2\" style=\"color:red\">/Cover/E72fd1425ee00a7192a7261c5b45fcab2</a>, link 7 : <a target=\"_blank\" href=\"/Cover/E86a31cf62149eaf28543a8a639d91309\" style=\"color:red\">/Cover/E86a31cf62149eaf28543a8a639d91309</a>, link 8 : <a target=\"_blank\" href=\"/Cover/E8b5931c6ba81548e4b0a06e07fdd5282\" style=\"color:red\">/Cover/E8b5931c6ba81548e4b0a06e07fdd5282</a>, link 9 : <a target=\"_blank\" href=\"/Cover/Ee98b70f046cbdf12b3eee2d65e625373\" style=\"color:red\">/Cover/Ee98b70f046cbdf12b3eee2d65e625373</a>, link 10 : <a target=\"_blank\" href=\"/Cover/E10f628a33fcf828bb57d497794a34094\" style=\"color:red\">/Cover/E10f628a33fcf828bb57d497794a34094</a>), được liệt kê tách nhau bởi <span style=\"color:green\">***</span><3275>/Home/Index***/Home/SecretWeb***/Admin/QuickDataInWeb***/Home/UploadFile***/Home/UploadFile?type=1<splix>0<splix>0***/Home/DownloadFile***/Home/EditTextNote***/Home/DownloadFile_ClearWeb\r\nInfo_Email<3275>true<3275><br />Nhập thông tin email (chỉ <span style=\"color:blue\">Gmail</span>) dùng để gửi thông báo liên quan dịch vụ/lưu trữ admin (nếu không nhập mặc định sẽ lấy email và mật khẩu mặc định hiện đang được lấy data setting trong file <b>appsettings.json</b> (nó có thể sẽ hết hạn) [?]). Nếu bạn nhập ở đây, các thông tin sẽ được thay thế và sử dụng để xử lý. Mời bạn nhập theo kiểu : <b style=\"color:red\">Email<5828>Mật khẩu</b> (sẽ tự động mã hoá sau đó) <3275>72aYAo76lcpRlZL1WdKp91iq6iJts1VmRjhBI0Aj9ng68fIbsEHJ/bdmASwYIiE90T/hCPfznYXUKPb1flRqrw==<5828>52LY6RvmKCJZCsWbXOdT4fWjuDlojLJacCa17UVAL5gDVcHW1ut9AwKstqHBdXpz[Encrypted_3275]\r\nInfo_MegaIO<3275>true<3275><br />Nhập thông tin email tài khoản <span style=\"color:blue\">MegaIO</span> để lưu trữ các file upload + Admin (nếu không nhập mặc định sẽ lấy email và mật khẩu mặc định hiện đang được lấy data setting trong file <b>appsettings.json</b> (nó có thể sẽ hết hạn)). Nếu bạn nhập ở đây, các thông tin sẽ được thay thế và sử dụng để xử lý. Mời bạn nhập theo kiểu : <b style=\"color:red\">Email<5828>Mật khẩu</b> (sẽ tự động mã hoá sau đó) <3275>72aYAo76lcpRlZL1WdKp91iq6iJts1VmRjhBI0Aj9ng68fIbsEHJ/bdmASwYIiE90T/hCPfznYXUKPb1flRqrw==<5828>9WwfyhJev7z/YhEj0BfrkxZt8I6U2nbp/e7JGIVdHdo=[Encrypted_3275]\r\nColor_BackgroundAndText<3275>true<3275><br /><span id=\"demo-X\">[Hạn chế sử dụng] Hãy nhập với định dạng : <span style=\"color:red\">Mã màu background<>Mã màu text</span> để thiết kế hiển thị website (xem/sửa demo <a href=\"#background-demo\">ở bên dưới</a>). Nếu bạn để trống trường này tức là không sử dụng và mặc định sẽ sử dụng nền sáng/tối của hệ thống...<3275>[NULL]\r\nColor_TracNghiem<3275>true<3275><br />[Để phù hợp hiển thị] Hãy nhập tại đây mã màu/tên màu để quét đáp án mà user đã chọn cho câu hỏi đó (dành cho tính năng Play Trắc Nghiệm), hiện tại mặc định là màu xanh lá - <span style=\"color:green\">green</span> (nếu bạn có tác động edit setting của màu nền và text website ở trên)...<3275>green\r\nAppWeb_LockedUse<3275>true<3275><br />Danh sách các <span style=\"color:red\">/Controller/ActionName</span> của các dịch vụ web, tiện ích hiện có <span style=\"color:orangered\">mà không thể sử dụng</span> hoặc truy cập được ngay lúc này (liệt kê cách nhau bởi dấu phẩy) => sẽ sang trang error page nếu tiện ích đó bị chặn (không khả dụng với các page : <span style=\"color:blue\">/Home/SessionPlay_DarkAdmin</span>, page error và 10 link của dịch vụ url mã hoá,...) và hãy cẩn thận khi muốn áp dụng<3275>[NULL]\r\nDownloadFile_ClearWeb<3275>true<3275><br />[Khi bạn bật chế độ tàng hình website] Liệt kê các tên file - chỉ cần tên file và đuôi mở rộng (mỗi cái tách nhau bởi dấu phẩy). Hướng dẫn sử dụng : tải lên các file này (ngay thư mục của demo <span style=\"color:red\">/file/Folder1/Folder2/</span>) của trang <span style=\"color:blue\">UploadFile</span>, sau đó nếu bạn truy cập đến trang <span style=\"color:blue\">/Home/DownloadFile_ClearWeb</span> sẽ lần lượt được tự động click tải xuống các file này...<3275>[NULL]\r\nSessionPlay_Unenabled<3275>false<3275><br /><hr><br /><b>[SUB]</b> Không cho phép sử dụng trang Session Play (setting này bạn phải tự bật/tắt thủ công)\r\nDownload_Quick<3275>false<3275><br /><b>[SUB]</b> Cho phép sử dụng mẫu page tạm để download file upload. Hướng dẫn sử dụng : upload cho mỗi lần duy nhất 1 file lên server (nếu không vui lòng nén hết nó) - [nhiều file cũng được nhưng phải liệt kê nhiều 😁] và phải tải lên thư mục demo : /Folder1/Folder2. Sau đó; bạn tự tạo project hay file HTML của riêng bạn (chỉ để mạo diện) và gán thẻ <span style=\"color:blue\">&lt;a href=\"localhost/Home/DLQ?path=tên file của bạn; VD : filedemo.zip (tốt nhất link nên giả diện bằng bom.so)\" download&gt;Tải xuống&lt;/a&gt;</span> và bạn tự click vào link button này để tải xuống - hạn chế tự ý truy cập link này một cách trực tiếp (setting này bạn tự tắt/bật thủ công)\r\nExternal_Unenable<3275>false<3275><br /><b>[SUB]</b> Không cho phép sử dụng form HTML mạo diện và external sử dụng dịch vụ (save file, send log mail admin) - cũng tự thủ công nhé\r\nExternal_Post<3275>false<3275><br /><b>[SUB]</b> Sử dụng POST DATA EXTERNAL và result sang dạng JSON (tự setting thủ công) - khi setting này bật, trang POST data đó không quan tâm dữ liệu IP user hay web site nếu không hoạt động (cũng tựa như API - ngoại trừ một số tính năng)\r\nEncrypt_Data<3275>false<3275><br /><b>[SUB]</b> Mã hoá nội dung tải xuống file TXT của các dịch vụ (trắc nghiệm, question, karaoke text), với key mã hoá là <span style=\"color:deeppink\">buivanluat-ADMIN3275</span> - tự setting thủ công\r\nAccept_ListUrl<3275>true<3275><br /><b>[SUB]</b> Liệt kê các <span style=\"color:red\">/Controller/ActionName</span> của các dịch vụ web, tiện ích hiện có <span style=\"color:orangered\">mà cho phép sử dụng</span> ngay cả khi web không hỗ trợ/hoạt động (cách nhau bởi dấu phẩy). Khi áp dụng, user sẽ phải tự truy cập bằng link này bằng cách url trực tiếp.<3275>[NULL]\r\nTabTittle_NoiDung<3275>true<3275><br /><b>[SUB]</b> Nội dung hiển thị cơ bản của Tab Tittle Web tại site Home (mặc định là : <span style=\"color:blue\">C# | Asp Net Core | Công nghệ thông tin</span> - ngoại trừ một số page (tự setting thủ công)<3275>C# | Asp Net Core | Công nghệ thông tin\r\nUploadFile_MaxSize<3275>true<3275><br /><b>[SUB]</b> Số lượng size max upload file mỗi lần có thể tải lên server (đơn vị KB), cơ bản là <span style=\"color:red\">2000000 KB</span> - tự setting thủ công<3275>0\r\nKaraoke_RandomImage<3275>false<3275><br /><b>[SUB]</b> Khi Play Karaoke nếu chọn phát background với ảnh ngẫu nhiên của hệ thống (chỉ với <span style=\"color:red\">ToPlayKaraoke</span>), sẽ tự động sau mỗi 5 giây sẽ hiển thị sang ảnh khác và lặp lại - tự setting thủ công\r\nIPUser_UnVisible<3275>false<3275><br /><b>[SUB]</b> Ẩn nội dung IP của user hiển thị trên phần header, thay thế hiển thị bằng <span style=\"color:chartreuse\">###</span> - tự setting thủ công\r\nTime_Waiting<3275>true<3275><br /><b>[SUB]</b> [Chỉ số nhân - ảo tưởng] Chỉ số thời gian chờ đại diện waiting cho các process tạo các file trắc nghiệm - question - karaoke play *.TXT, với các phần tử nhân tương ứng lần lượt là : <span style=\"color:red\">3 - 3 - 6</span> - tự setting thủ công<3275>0\r\nHTML_Visible<3275>true<3275><br /><b>[SUB]</b> Ẩn tất cả nội dung HTML hiển thị của các trang web hệ thống/tính năng (trở thành trang web trắng, lưu ý một số luồng sẽ không còn hoạt động đúng cách) - tự setting thủ công # với cách thiết lập (<b>0</b> : không áp dụng || <b>1</b> : chỉ áp dụng cho các site Home (ngoại trừ trang SessionPlay) và các trang tính năng hiện có của Admin || <b>2</b> : áp dụng tất cả và không ngoại lệ - <span style=\"color:red\">cẩn thận và tự giác 😎</span>)<3275>0\r\nGet_Blocked<3275>false<3275><br /><b>[SUB]</b> Chặn tất cả các trang web có phương thức GET (phương thức POST không thể chặn vì được sử dụng với các dịch vụ External/API)\r\nAdmin_Setting<3275>true<3275><br /><b>[SUB]</b> An toàn - Tên file điều khiển admin setting đang sử dụng (mặc định là : <span style=\"color:red\">SettingABC_DarkBVL.txt</span>)<3275>SettingABC_DarkBVL.txt\r\nAdmin_MiniWeb<3275>false<3275><br /><b>[SUB]</b> Cấp quyền sử dụng mini web admin (trang web tạm thời sẽ không hoạt động và chỉ dành cho admin)\r\nKeyEncrypt_Admin<3275>true<3275><br /><b>[SUB]</b> Key dùng để mã hoá/giải mã sử dụng cho thông tin nhận dạng Admin (secure setting) - khi bạn thay đổi, thông tin ID và mật khẩu admin cũng sẽ được cập nhật lại tương ứng (mặc định là : <span style=\"color:red\">buivanluat-ADMIN3275</span>)<3275>buivanluat-ADMIN3275\r\nTwoStep_Admin<3275>false<3275><br /><b>[SUB]</b> Sử dụng gửi email để nhận mã xác thực 2 bước bí mật mỗi khi đăng nhập vào dịch vụ Admin Setting";

                FileExtension.WriteFile(path, ndSet);
                var px = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/Setting_Status.txt");

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
                var partDelayTime = delayTime.Split("#");
                var hourDL = partDelayTime[0].Replace("H", "");
                var minDL = partDelayTime[1].Replace("M", "");
                var secDL = partDelayTime[2].Replace("S", "");

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                if (hourDL.Contains("-"))
                {
                    xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
                }
                else
                {
                    xuxu = xuxu.AddHours(int.Parse(hourDL));
                }

                if (minDL.Contains("-"))
                {
                    xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
                }
                else
                {
                    xuxu = xuxu.AddHours(int.Parse(minDL));
                }

                if (secDL.Contains("-"))
                {
                    xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
                }
                else
                {
                    xuxu.AddSeconds(int.Parse(secDL));
                }

                FileExtension.WriteFile(px, "Đã tắt hết các item setting (kể cả các setting phụ) # " + xuxu);

                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Html_Play.txt"), "\"ALL\"---GET\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n#3275#\r\n\"ONLY_HOME\"---POST\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n#3275#\r\n\"ONLY_COVER\"---GET\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n#3275#\r\nCHIBAOGOM---NULL---/Home/ABC,/Admin/DEF,/Cover/XYZ\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n#3275#\r\nBOQUA---POST---/Home/ABC,/Admin/DEF,/Cover/XYZ\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n#3275#\r\n/Home/ABC---GET\r\n||\r\n<h1 style=\"color:blue\">BUI VAN LUAT</h1>\r\n<br />\r\n<input type=\"text\" size=\"20\" />\r\n#3275#\r\n/Admin/XYZ\r\n||\r\n<h2 style=\"color:red\">BUI VAN LUAT</h2>\r\n<br />\r\n<script>\r\n var loading = 0;\r\n        \r\n        setInterval(function()\r\n        {\r\n            if (loading == 1) return;\r\n        \r\n              var formData = new FormData();\r\n               formData.append(\"key\", \"value\");\r\n        \r\n         $.ajax({\r\n            url: \"/Cover/XXX\",\r\n            type: \"POST\",\r\n            data: formData,\r\n            contentType: false,\r\n            processData: false,\r\n            success: function(so) {\r\n                if (so.result == true)\r\n              {\r\n                  alert(so.data);\r\n             }\r\n                loading = 1;\r\n            }});\r\n        \r\n        },1000);\r\n</script>");

                return Ok(new
                {
                    result = "Thành công, dữ liệu đã được khôi phục !",
                    datetime = CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7)
                });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return Ok(new
                {
                    error = HttpContext.Session.GetObject<string>("error_exception_log")
                });
            }
        }

        public ActionResult CapNhatStatusSetting(string st)
        {
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var delayTime = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "");
            var partDelayTime = delayTime.Split("#");
            var hourDL = partDelayTime[0].Replace("H", "");
            var minDL = partDelayTime[1].Replace("M", "");
            var secDL = partDelayTime[2].Replace("S", "");

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            if (hourDL.Contains("-"))
            {
                xuxu = xuxu.AddHours(-1 * int.Parse(hourDL.Replace("-", "")));
            }
            else
            {
                xuxu = xuxu.AddHours(int.Parse(hourDL));
            }

            if (minDL.Contains("-"))
            {
                xuxu = xuxu.AddMinutes(-1 * int.Parse(minDL.Replace("-", "")));
            }
            else
            {
                xuxu = xuxu.AddHours(int.Parse(minDL));
            }

            if (secDL.Contains("-"))
            {
                xuxu = xuxu.AddSeconds(-1 * int.Parse(secDL.Replace("-", "")));
            }
            else
            {
                xuxu.AddSeconds(int.Parse(secDL));
            }

            FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/Setting_Status.txt"), st + " # " + xuxu + " (trạng thái do admin tự đặt)");
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        [HttpPost]
        public ActionResult ThayDoiSetting(IFormCollection f)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var pthX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var password = FileExtension.ReadFile(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];


                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var key = listSetting[60].Split("<3275>")[3];

                if (StringMaHoaExtension.Decrypt(password, key) == f["txtPassword"].ToString())
                {
                    var logset = f["txtSetting"].ToString();

                    var spanset = logset.Replace("\r", "").Split("\n");
                    var nuna = FileExtension.ReadFile(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);


                        if (spanset[0] != nuna[0]
                            || spanset[1] != nuna[1]
                            || spanset[18] != nuna[18])
                        {
                            return Redirect("/Admin/SettingXYZ_DarkAdmin#da-xay-ra-loi");
                        }

                    FileExtension.WriteFile(pthX, logset);
                }
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

            return Redirect("SettingXYZ_DarkAdmin#changesetting");
        }

        public ActionResult EditFileAdminStatic()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("fileDataAdmin"))) return RedirectToAction("Error", "Home");
                var pathXY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");

                var file = HttpContext.Session.GetString("fileDataAdmin");

                HttpContext.Session.Remove("fileDataAdmin");
                TempData["hohoFile"] = file;

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xa = Request.Path;
                    if (xa == "" || xa == "/" || xa == null)  xa = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                    if (lockedApp[3].Contains(xa))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = FileExtension.ReadFile(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (infoXWW[1] == "true" && (yes_log || HttpContext.Session.GetString("NoAdmin_YesLog") == "true"))
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = FileExtension.ReadFile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                        + "\t" + this.Request.Path + "\t[GET]";

                    FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                }

                var nd = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin" + file));
                TempData["TextInFileAd"] = nd;

                return View();

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult EditFileAdminStatic(IFormCollection f)
        {
            try
            {
                var pathXY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");

                var fix = "";
                foreach (var item in f.Keys)
                {
                    fix += string.Format("{0} : {1}\n", item, f[item]);
                }

                HttpContext.Session.SetString("hanhdong_3275", fix);

                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xa = Request.Path;
                    if (xa == "" || xa == "/" || xa == null)  xa = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                    if (lockedApp[3].Contains(xa))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = FileExtension.ReadFile(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (infoXWW[1] == "true" && (yes_log || HttpContext.Session.GetString("NoAdmin_YesLog") == "true"))
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = FileExtension.ReadFile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                        + "\t" + this.Request.Path + "\t[GET]";

                    FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                }

                var file = f["txtFile"].ToString();
                var pathFile = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin" + file);
                var nd = f["txtNoiDung"].ToString();

                FileExtension.WriteFile(pathFile, nd);

               return Content("Phiên bản này đã kết thúc !");

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

        }

        public ActionResult ReadAdminFileByStatic(string? file, string? keyX, bool? admin = false, string isHTML = "true")
        {
            try
            {
                var pathXY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");
                var matpassAd = FileExtension.ReadFile(pathXY).Replace("\r", "").Split("\n")[1];

                if (admin == false && keyX != matpassAd)
                {
                    return RedirectToAction("Error", "Home");
                }

                if (string.IsNullOrEmpty(file) == false)
                {
                    HttpContext.Session.SetString("fileDataAdmin", file);
                }

                var settingFile = FileExtension.ReadFile(pathXY).Replace("\r", "").Split("\n")[4];
                if (admin == false && (file.Contains(settingFile) || file.Contains("SecureSettingAdmin") || file.Contains("SettingAdminLoginConnect")))
                {
                    return RedirectToAction("Error", "Home");
                }

                if (admin == false && string.IsNullOrEmpty(file)) return RedirectToAction("Error", "Home");


                var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = FileExtension.ReadFile(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xa = Request.Path;
                    if (xa == "" || xa == "/" || xa == null)  xa = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                    if (lockedApp[3].Contains(xa))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = FileExtension.ReadFile(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                var yes_log = true;

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];

                if (HttpContext.Session.GetString("admin-userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (infoXWW[1] == "true" && (yes_log || HttpContext.Session.GetString("NoAdmin_YesLog") == "true"))
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = FileExtension.ReadFile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                        + "\t" + this.Request.Path + "\t[GET]";

                    FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
                }

                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                for (int i = 0; i < listSetting.Length; i++)
                {
                    var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                    if (info[0] == "LockedAll_Web")
                    {
                        if (info[1] == "true")
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                }

                if (admin == true)
                {
                    var ndx = HttpContext.Session.GetString("nd-file-admin");
                    if (ndx == null) return RedirectToAction("Error", "Home");
                    HttpContext.Session.Remove("nd-file-admin");

                    var noidux = (isHTML == "true") ? ndx.Replace("\r", "").Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;") : HttpUtility.HtmlEncode(ndx).Replace("\r", "").Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");

                    TempData["nd-file-admin"] = noidux;
                    return View();
                }

                var nd = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin" + file));

                HttpContext.Session.SetString("nd-file-admin", nd);
                HttpContext.Session.SetString("nd-file-admin-session", nd);

                return RedirectToAction("ReadAdminFileByStatic", "Admin", new { admin = true, isHTML = isHTML });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult BackupDataAdmin(string? admincode)
        {
            if (admincode == "32752262")
            {
                var formatMail = "----------------- karaoke_font ----------------- :\r\n\r\n@karaoke_font\r\n\r\n----------------- ListIPComeToHereTheFirst ----------------- :\r\n\r\n@ListIPComeToHereTheFirst\r\n\r\n----------------- quy-tac-ki-tu-chuyen-doi-JAPAN ----------------- :\r\n\r\n@quy-tac-ki-tu-chuyen-doi-JAPAN\r\n\r\n----------------- InfoIPRegist ----------------- :\r\n\r\n@InfoIPRegist\r\n\r\n----------------- ListIPComeHere ----------------- :\r\n\r\n@ListIPComeHere\r\n\r\n----------------- ListIPLock ----------------- :\r\n\r\n@ListIPLock\r\n\r\n----------------- ListIPOnWebPlay ----------------- :\r\n\r\n@ListIPOnWebPlay\r\n\r\n----------------- ListUserIPApproveWaiting ----------------- :\r\n\r\n@ListUserIPApproveWaiting\r\n\r\n----------------- LockedIPClient ----------------- :\r\n\r\n@LockedIPClient\r\n\r\n----------------- InfoWebFile ----------------- :\r\n\r\n@InfoWebFile\r\n\r\n----------------- DiemHocSinh ----------------- :\r\n\r\n@DiemHocSinh\r\n\r\n----------------- SecureSettingAdmin ----------------- :\r\n\r\n@SecureSettingAdmin\r\n\r\n----------------- NoteLog ----------------- :\r\n\r\n@NoteLog\r\n\r\n----------------- ReplaceManager ----------------- :\r\n\r\n@ReplaceManager\r\n\r\n----------------- AdminSetting ----------------- :\r\n\r\n@AdminSetting\r\n";

                var karaoke_font = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "karaoke_font.txt"));
                formatMail = formatMail.Replace("@karaoke_font", karaoke_font);

                var ListIPComeToHereTheFirst = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "ListIPComeToHereTheFirst.txt"));
                formatMail = formatMail.Replace("@ListIPComeToHereTheFirst", ListIPComeToHereTheFirst);


                var chuyendoiJAPAN = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ChangeJapan", "quy-tac-ki-tu-chuyen-doi.txt"));
                formatMail = formatMail.Replace("@quy-tac-ki-tu-chuyen-doi-JAPAN", chuyendoiJAPAN);

                var InfoIPRegist = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "InfoIPRegist.txt"));
                formatMail = formatMail.Replace("@InfoIPRegist", InfoIPRegist);

                var ListIPComeHere = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "ListIPComeHere.txt"));
                formatMail = formatMail.Replace("@ListIPComeHere", ListIPComeHere);

                var ListIPLock = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "ListIPLock.txt"));
                formatMail = formatMail.Replace("@ListIPLock", ListIPLock);

                var ListIPOnWebPlay = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "ListIPOnWebPlay.txt"));
                formatMail = formatMail.Replace("@ListIPOnWebPlay", ListIPOnWebPlay);

                var ListUserIPApproveWaiting = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "ListUserIPApproveWaiting.txt"));
                formatMail = formatMail.Replace("@ListUserIPApproveWaiting", ListUserIPApproveWaiting);

                var LockedIPClient = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect", "LockedIPClient.txt"));
                formatMail = formatMail.Replace("@LockedIPClient", LockedIPClient);

                var InfoWebFile = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "InfoWebFile", "InfoWebFile.txt"));
                formatMail = formatMail.Replace("@InfoWebFile", InfoWebFile);

                var DiemHocSinh = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "TracNghiem_XOnline", "DiemHocSinh.txt"));
                formatMail = formatMail.Replace("@DiemHocSinh", DiemHocSinh);

                var SecureSettingAdmin = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt"));
                formatMail = formatMail.Replace("@SecureSettingAdmin", SecureSettingAdmin);

                var NoteLog = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, "note", "notelog.txt"));
                formatMail = formatMail.Replace("@NoteLog", NoteLog);

                var ReplaceManager = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Others", "ReplaceManager.txt"));
                formatMail = formatMail.Replace("@ReplaceManager", ReplaceManager);

                var AdminSetting = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]));
                formatMail = formatMail.Replace("@AdminSetting", AdminSetting);

                Calendar xz = CultureInfo.InvariantCulture.Calendar;
                var xuxu = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", "(" + HttpContext.Session.GetString("admin-userIP") + " --- " + Request.Host + ") MAIL - Backup data for admin in " + xuxu, formatMail, "teinnkatajeqerfl");
            }

            return RedirectToAction("LoginSettingAdmin");
        }

        public ActionResult FrameNoticeAdmin(int? notice)
        {
            if (notice == null || HttpContext.Session.GetString("accept_notice") == null || HttpContext.Session.GetString("accept_notice") != "32752262")
            {
                return RedirectToAction("Error", "Home");
            }

            HttpContext.Session.Remove("accept_notice");

            switch (notice)
            {
                case 0:
                    TempData["alert_admin"] = "⚠ Website hiện đang không hoạt động nên bây giờ bạn sẽ không thể sử dụng. Vui lòng quay lại sau, hoặc liên hệ tại email : mywebplay.website@gmail.com. Xin cảm ơn !";
                    break;

                case 1:
                    TempData["alert_admin"] = "❤ [ADMIN] Đã [KHOÁ] kết nối trang web theo ID bạn yêu cầu !";
                    break;

                case 2:
                    TempData["alert_admin"] = "❤ [ADMIN] Đã [MỞ KHOÁ] kết nối trang web theo ID bạn yêu cầu !";
                    break;

                case 3:
                    TempData["alert_admin"] = "❤ [ADMIN] Đã đăng xuất tất cả kết nối theo ID bạn yêu cầu, họ sẽ phải bật lại trang web !";
                    break;

                case 4:
                    TempData["alert_admin"] = "Bạn đã bật trang web MyWebPlay thành công, bạn có thể sử dụng các tính năng tại trang web 😊!";
                    break;

                case 5:
                    TempData["alert_admin"] = "Đã cấp phép thành công, bạn có thể tiếp tục sử dụng các tính năng tại trang web trên IDs mới này 😎 !";
                    break;

                case 6:
                    TempData["alert_admin"] = "Để tiếp tục sử dụng, vui lòng tìm cách để có lại được sự cấp phép. Nếu không, hãy <a href=\"/Home/PlayOnWebInLocalX?key=false\">tắt sử dụng trang web</a> và yêu cầu bật sử dụng lại (nếu bạn đã được admin approve trước đó và đang chờ đợi, hệ thống sẽ cho phép bạn quay trở lại trang Index ngay).\nHãy liên hệ lại với admin nếu sau đó bạn cũng không được chuyển hướng và cho phép tiếp tục sử dụng dịch vụ web.";
                    break;

                case 7:
                    TempData["alert_admin"] = "⚠ Trang web hiện đang bị khoá bởi người đảm quyền, không thể kết nối đối với một số trường hợp. Vì vậy, vui lòng chờ đợi sau khi admin duyệt và mở khoá để có thể tiếp tục. Nếu bạn muốn gửi email xin phép, liên hệ tại : mywebplay.website@gmail.com";
                    break;

                case 8:
                    TempData["alert_admin"] = "Bạn đã tắt trang web MyWebPlay thành công, để sử dụng lại hãy thực hiện các bước để bật 😎 !";
                    break;

                case 9:
                    TempData["alert_admin"] = "⚠ Tất cả kết nối phiên bản của bạn hiện đã hết hạn sử dụng hoặc chưa được bật, vui lòng bật lại trang web để sử dụng. Còn nếu bạn đã bật, vui lòng kiên nhẫn chờ đợi...";
                    break;

                case 10:
                    TempData["alert_admin"] = "⚠ Khách hàng có mật độ tin tưởng cũng không được phép vào website khi không hoạt động, bị khoá hoặc chưa đăng ký (hoặc cũng có thể website vẫn đang hoạt động bình thường, nhưng có điều hãy xoá khoá ghi nhớ mật độ trên máy tính của bạn). Vui lòng thông cảm và thử lại sau...";
                    break;


                default:
                    TempData["alert_admin"] = "";
                    break;
            }

            return View();
        }


        [HttpPost]
        public ActionResult Karaoke_Ajax_Call(string url, int index)
        {
            if (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[24] == "AJAX_JAVASCRIPT_OFF")
            {
                return Ok(new { error = "true" });
            }

            var baihat = "";
            var hasSinger = "false";
            var tkKara = "";
            var noidung = "";

            WebClient client = new WebClient();
            var https = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
            Stream stream = client.OpenRead(https + "://" + url + "/MyListSong.txt");
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();

            var listSong = content.Replace("\r", "").Split("\n");
            baihat = listSong[index - 1];

            var server = url;
            var song = baihat;

            var url_txt = server + "/" + song + "/" + song + ".txt";
            var url_goc = server + "/" + song + "/" + song + "_Goc.mp3";
            var url_kara = server + "/" + song + "/" + song + ".mp3";

            https = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
            stream = client.OpenRead(https + "://" + url_txt);
            reader = new StreamReader(stream);
            content = reader.ReadToEnd();

            var encrypt = false;
            try
            {
                StringMaHoaExtension.Decrypt(content);
                encrypt = true;
            }
            catch
            {
                encrypt = false;
            }

            if (encrypt == true)
            {
                content = StringMaHoaExtension.Decrypt(content);
            }

            var httpx = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";

            ViewBag.Music = httpx + "://" + url_kara;
            ViewBag.Musix = httpx + "://" + url_goc;

            if (content.Contains("<>") == false)
            {
                //ViewBag.Karaoke = content;
                //TempData["TK-KARA"] = "";
                //if (TempData["hassinger"] != "true")
                //    TempData["hassinger"] = "false";

                tkKara = "";
                if (hasSinger != "true")
                    hasSinger = "false";
            }
            else
            {
                var xa = content.Replace("\r", "").Split("\n");
                for (int i = 0; i < xa.Length; i++)
                {
                    if (xa[i].Contains("<>"))
                    {
                        var xb = xa[i].Split("<>");
                        var xc = xb[1].Split("#");
                        var xd = xb[0].Split("-");
                        noidung += xc[0] + "=" + xd[0] + "=" + xd[1];

                        if (xd[0] == "[SINGER]")
                        {
                            hasSinger = "true";
                        }
                        else
                        {
                            //if (TempData["hassinger"] != "true")
                            //    TempData["hassinger"] = "false";

                            if (hasSinger != "true")
                                hasSinger = "false";
                        }

                        content = content.Replace(xb[0] + "<>", "");

                        if (i < xa.Length - 1)
                            noidung += "\n";
                    }
                }
            }

            return Ok(new
            {
                mp3_goc = https + "://" + url_goc,
                mp3_kara = https + "://" + url_kara,
                text_kara = content,
                tenbaihat = baihat,
                singer = hasSinger,
                text_content = noidung,
                error = "false",
            });
        }

        [HttpPost]
        public ActionResult ListSongKaraoke_Call(string url)
        {
            if (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[24] == "AJAX_JAVASCRIPT_OFF")
            {
                return Ok(new { error = "true" });
            }

            WebClient client = new WebClient();
            var https = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";
            Stream stream = client.OpenRead(https + "://" + url + "/MyListSong.txt");
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();

            return Ok(new { content = content, error = "false" });
        }

        [HttpPost]
        public ActionResult EncryptPasswordByKey_Call(string password)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = FileExtension.ReadFile(path);

            var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            var key = listSetting[60].Split("<3275>")[3];

            var newPassword = StringMaHoaExtension.Encrypt(password, key);

            return Ok(new { password = newPassword });
        }

        [HttpPost]
        public ActionResult DecryptPasswordByKey_Call(string password)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidung = FileExtension.ReadFile(path);

            var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoX = listSetting[39].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            var key = listSetting[60].Split("<3275>")[3];

            var newPassword = StringMaHoaExtension.Decrypt(password, key);

            return Ok(new { password = newPassword });
        }

        public ActionResult APIPageSetting()
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }


                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "API")).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "API")).Create();

                var listPage = new List<string>();

                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "API_WebPage", "API-Menu.txt");
                var apiMenu = FileExtension.ReadFile(path).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                var selectedAPI = string.Empty;
                for (int i = 0; i < apiMenu.Length; i++)
                {
                    var partApi = apiMenu[i].Split("***", StringSplitOptions.RemoveEmptyEntries);
                    listPage.Add(partApi[0].Replace("[", "").Replace("]", "").Replace("<Selected>", ""));
                    if (partApi[0].Contains("<Selected>"))
                    {
                        selectedAPI += partApi[0].Replace("<Selected>", "") + "\r\n";
                    }
                }

                TempData["selected_api_list"] = selectedAPI;

                return View(listPage);
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

        }

        [HttpPost]
        public ActionResult APIPageSetting(IFormCollection f)
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "ADMINSETTING_OFF")
                    return Redirect("https://google.com");

                var pathAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API");

                DirectoryInfo dir = new DirectoryInfo(pathAPI);

                // Kiểm tra xem thư mục có tồn tại không
                if (dir.Exists)
                {
                    // Lấy danh sách tất cả các file và thư mục con
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        file.Delete(); // Xoá từng file
                    }

                    foreach (DirectoryInfo subdir in dir.GetDirectories())
                    {
                        subdir.Delete(true); // Xoá từng thư mục con (bao gồm cả nội dung bên trong)
                    }
                }


                var data = f["txtData"].ToString();

                if (data.Length > 0)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "API_WebPage", "API-Menu.txt");
                    var apiMenu = FileExtension.ReadFile(path).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

                    var htmlFormat = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "API_WebPage", "API-Format.txt"));

                    var https = (FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[10] == "LINK_HTTPS_ON") ? "https" : "http";

                    var host = https + "://" + Request.Host;
                    var menuDivTab = string.Empty;
                    var divTabPlay = string.Empty;
                    var jsSetResult = string.Empty;


                    var selectedAPI = new List<string>();
                    var unselectedAPI = new List<string>();

                    for (int i = 0; i < apiMenu.Length; i++)
                    {
                        var partApi = apiMenu[i].Split("***", StringSplitOptions.RemoveEmptyEntries);
                        if (data.Contains(partApi[0].Replace("<Selected>", "")) == false)
                        {
                            unselectedAPI.Add(partApi[0]);
                            continue;
                        }

                        selectedAPI.Add(partApi[0].Replace("<Selected>", ""));

                        menuDivTab += string.Format("<button class=\"tablinks\" onclick=\"openTab(event, '{0}')\">{0}</button>", partApi[0].Replace("[", "").Replace("]", "").Replace("/Home/", "").Replace("<Selected>", "")) + "\r\n";

                        var phan2 = partApi[1].Split("<>");
                        var partTabPlay = string.Empty;
                        var jsAppend = string.Empty;

                        for (var j = 0; j < phan2.Length; j++)
                        {
                            var name = phan2[j].Split("><")[0];
                            var type = phan2[j].Split("><")[1];
                            var title = phan2[j].Split("><")[2];

                            if (type == "textarea")
                            {
                                partTabPlay += string.Format("<textarea rows=\"10\" name=\"{0}\" title=\"{1}\"></textarea><br />", name, title) + "\r\n";
                            }
                            else if (type == "file")
                            {
                                partTabPlay += string.Format("<input type=\"file\" multiple id=\"{0}\" title=\"{1}\" /><br />\r\n", name, title);
                            }
                            else
                            {
                                partTabPlay += string.Format("<input type=\"{2}\" name=\"{0}\" title=\"{1}\" /><br />\r\n", name, title, type);
                            }

                            // JS Append
                            if (type == "file")
                            {
                                jsAppend += string.Format("const fileInput = document.getElementById('{0}'); \r\nconst files = fileInput.files; for (let i = 0; i < files.length; i++) \r\n&lt;\r\n data.append('{0}', files[i]);\r\n &gt;\r\n", name)
                                    .Replace("&lt;", "{").Replace("&gt;", "}");
                            }
                            else
                            {
                                jsAppend += string.Format("var chonCheck = false;\r\nvar value =\"\";\r\nvar listName = document.getElementsByName('{0}');\r\nfor(var i = 0; i < listName.length; i++)\r\n&lt;\r\n@@ set_check_prepare @@\r\nif (listName[i].value != \"\")\r\n&lt;\r\nvalue = listName[i].value;\r\nbreak;\r\n&gt;\r\n&gt;\r\n@@ replace_value_checkbox @@\r\ndata.append(\"{0}\", value);\r\n", name)
                                    .Replace("&lt;", "{").Replace("&gt;", "}");

                                if (type == "checkbox")
                                {
                                    jsAppend = jsAppend.Replace("@@ replace_value_checkbox @@", "if (chonCheck == true)\r\n{\r\nvalue = \"on\";\r\n}");
                                    jsAppend = jsAppend.Replace("@@ set_check_prepare @@", "if (chonCheck == false && listName[i].checked == true)\r\n{\r\nchonCheck = true;\r\n}");
                                }
                                else
                                {
                                    jsAppend = jsAppend.Replace("@@ replace_value_checkbox @@", "");
                                    jsAppend = jsAppend.Replace("@@ set_check_prepare @@", "");
                                }
                            }
                        }

                        divTabPlay += string.Format(
                            "<div id=\"{0}\" class=\"tabcontent\">\r\n    <h3>{0}</h3>\r\n{1}\r\n<div class=\"button-group\">\r\n        <button onclick=\"setresult{0}()\">SET RESULT</button>\r\n        <button onclick=\"getresult()\">GET RESULT</button>\r\n    </div>\r\n</div>",
                            partApi[0].Replace("[", "").Replace("]", "").Replace("/Home/", "").Replace("<Selected>", ""),
                            partTabPlay) + "\r\n\r\n";


                        jsSetResult += string.Format(
                            "\r\n\r\nfunction setresult{0}() &lt;\r\n    const data = new URLSearchParams();\r\n{1}data.append(\"resultX\", \"true\");\r\n    \r\n    fetch(\"{2}{3}\", &lt;\r\n        method: 'post',\r\n        body: data,\r\n    &gt;);\r\n&gt;",
                            partApi[0].Replace("[", "").Replace("]", "").Replace("/Home/", "").Replace("<Selected>", ""),
                            jsAppend,
                            https + "://" + Request.Host,
                            partApi[0].Replace("[", "").Replace("]", "").Replace("<Selected>", "")).Replace("&lt;", "{").Replace("&gt;", "}");
                    }

                    htmlFormat = htmlFormat
                        .Replace("@@ Web_Host @@", host)
                        .Replace("@@ MENU_DIV_TAB @@", menuDivTab)
                        .Replace("@@ DIV_TAB_PLAY @@", divTabPlay)
                        .Replace("@@ JS_SetResult @@", jsSetResult);

                    //Save selected
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "API_WebPage", "API-Menu.txt");
                    var format = FileExtension.ReadFile(pathX);
                    for (int i = 0; i < selectedAPI.Count; i++)
                    {
                        format = format.Replace(selectedAPI[i], "<Selected>" + selectedAPI[i]);
                    }

                    for (int i = 0; i < unselectedAPI.Count; i++)
                    {
                        format = format.Replace(unselectedAPI[i], unselectedAPI[i].Replace("<Selected>", ""));
                    }

                    FileExtension.WriteFile(pathX, format);

                    //Create file HTML

                    var txtAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.txt");

                    var htmlAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.html");

                    using (FileStream fs = System.IO.File.Create(txtAPI))
                    {
                        // Ghi dữ liệu vào file (nếu cần)
                        byte[] info = new UTF8Encoding(true).GetBytes(htmlFormat);
                        fs.Write(info, 0, info.Length);
                    }

                    System.IO.File.Move(txtAPI, htmlAPI);
                }
                else
                {
                    var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "API_WebPage", "API-Menu.txt");
                    var format = FileExtension.ReadFile(pathX);

                    FileExtension.WriteFile(pathX, format.Replace("<Selected>", ""));
                }

                return Ok(new { result = true });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }

        }

        public ActionResult EditAPIPageSetting()
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }


                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home", "Home");


                var txtAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.txt");

                var htmlAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.html");

                if (System.IO.File.Exists(htmlAPI) == false && System.IO.File.Exists(txtAPI) == false)
                {
                    return RedirectToAction("Error", "Home");
                }

                if (System.IO.File.Exists(htmlAPI))
                {
                    System.IO.File.Move(htmlAPI, txtAPI);
                }

                TempData["edit_html_api"] = FileExtension.ReadFile(txtAPI);

                System.IO.File.Move(txtAPI, htmlAPI);

                return View();
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult EditAPIPageSetting(IFormCollection f)
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }


                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home", "Home");


                var txtAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.txt");

                var htmlAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.html");

                if (System.IO.File.Exists(txtAPI) == false && System.IO.File.Exists(htmlAPI) == false)
                {
                    return Ok(new { result = false });
                }

                System.IO.File.Move(htmlAPI, txtAPI);

                var data = f["txtData"].ToString();

                FileExtension.WriteFile(txtAPI, data);

                System.IO.File.Move(txtAPI, htmlAPI);

                return Ok(new { result = true });
            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        public ActionResult RedirectToAPIPage()
        {
            var txtAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.txt");

            var htmlAPI = Path.Combine(_webHostEnvironment.WebRootPath, "API", "API.html");

            if (System.IO.File.Exists(htmlAPI) == false && System.IO.File.Exists(txtAPI) == false)
            {
                return RedirectToAction("Error", "Home");
            }

            if (System.IO.File.Exists(txtAPI))
            {
                System.IO.File.Move(txtAPI, htmlAPI);
            }

            return Redirect("/API/API.html");
        }

        public ActionResult SetIPForAdmin(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return RedirectToAction("Error", "Home");


            HttpContext.Session.SetString("admin-userIP", ip);

            return RedirectToAction("LoginSettingAdmin", "Admin");
        }

        [HttpPost]
        public ActionResult CheckingAutoLoginAdmin(string? ip, string ID, string Password)
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var password = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];
            var Id = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[0];

            var key = listSetting[60].Split("<3275>")[3];

            if (StringMaHoaExtension.Decrypt(password, key) == Password && StringMaHoaExtension.Decrypt(Id, key) == ID)
            {
                var adminIP = HttpContext.Session.GetString("admin-userIP");
                var pamX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                var valuePam = FileExtension.ReadFile(pamX).Split("<>");

                var valuePamX = valuePam[0];
                var test = ip.Split("-");
                if (valuePam.Length > 2 && valuePamX == MD5.CreateMD5(test[0]) && MD5.CreateMD5(test[1]) == valuePam[2])
                {
                    HttpContext.Session.SetString("admin-userIP", test[0]);
                    adminIP = test[0];
                }

                if (adminIP != null)
                {
                    if (valuePamX == MD5.CreateMD5(adminIP))
                    {
                        HttpContext.Session.SetString("adminSetting", "true");
                        HttpContext.Session.Remove("xacthuc2buoc-ADMIN");
                        HttpContext.Session.SetString("IsAdminUsing", "true");
                        return Ok(new { result = true });
                    }
                }
            }

            return Ok(new { result = false });
        }

        [HttpPost]
        public ActionResult GetInfoKaraokeServerForShare(string url, string baihat, string option)
        {
            var listYoutube = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath, "karaoke_Example", "Video_Youtube", "randomlink.txt")).Replace("\r", "").Split("\n");

            var rand = new Random();
            var link = listYoutube[rand.Next(0, listYoutube.Length)];

            link = link.Replace("&", "");
            link = link.Replace("loop", "");
            link = link.Replace("autoplay", "");
            link = link.Replace("controls", "");
            link = link.Replace("mute", "");
            link = link.Replace("youtu.be/", "youtube.com/embed/");
            link = link.Replace("youtube.com/watch?v=", "youtube.com/embed/");

            var n1 = StringMaHoaExtension.Encrypt(url);
            var n2 = StringMaHoaExtension.Encrypt(baihat);
            var n3 = StringMaHoaExtension.Encrypt(link);


            var share = n1 + "-.-" + n2 + "-.-" + n3 + "-.-" + option;

            return Ok(new { result = share });
        }

        [HttpPost]
        public ActionResult DiChuyenFileExternal(string file, string pass)
        {
            var passAd = "";
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);

            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "Password_Admin")
                {
                    passAd = info[3];
                }
            }

            if (string.Compare(pass, passAd, false) == 0)
            {
                var fileSplit = file.Replace("\\", "/").Replace("/file", "").Trim('/').Split("/", StringSplitOptions.RemoveEmptyEntries);

                var filePath = "";
                for (var i = 0; i < fileSplit.Length - 1; i++)
                {
                    filePath += fileSplit[i];

                    if (i < fileSplit.Length - 2)
                    {
                        filePath += "/";
                    }
                }

                if (new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal", filePath)).Exists == false)
                    new System.IO.DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal", filePath)).Create();

                var goc = Path.Combine(_webHostEnvironment.WebRootPath, "file", filePath, fileSplit[fileSplit.Length - 1]);
                var ngon = Path.Combine(_webHostEnvironment.WebRootPath, "FileExternal", filePath, fileSplit[fileSplit.Length - 1]);
                System.IO.File.Move(goc, ngon);


                return Ok(new { result = true });
            }

            return Ok(new { result = false });
        }

        public ActionResult PublicSecureAdmin(string? pass)
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }

                var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidung = FileExtension.ReadFile(path);
                var matpassAd = FileExtension.ReadFile(pam).Replace("\r", "").Split("\n")[1];
                var listSetting = noidung.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var key = listSetting[60].Split("<3275>")[3];

                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (pass != StringMaHoaExtension.Decrypt(matpassAd, key))
                {
                    return Redirect("/Admin/SettingXYZ_DarkAdmin#da-xay-ra-loi");
                }

                var pamCu = FileExtension.ReadFile(pam).Replace("\r", "").Split("\n");

                var pam1 = "";
                for (int i = 0; i < 2; i++)
                {
                    pam1 += pamCu[i] + "\r\n";
                }

                var backupCodeCurrent = pamCu[18];
                var pam2 = "ADMINSETTING_ON\r\nfile_MO\r\nSettingABC_DarkBVL.txt\r\ndemokaraoke.bsite.net\r\nCOLOR_DIV_TRAC_NGHIEM_OFF\r\nCOLOR_DIV_QUESTION_OFF\r\nSESSION_PLAY_LOGIN_ON\r\nENCRYPT_LOCK_FILE_ADMIN_SETTING_WHEN_GO_TO_PAGE_ERROR_OFF\r\nLINK_HTTPS_ON\r\nSEND_MAIL_WHEN_ERROR_EXCEPTION_ON\r\nNOTICE : [NULL]\r\nNOT_USE_LOCKED_CLIENT_WEB_ON\r\nDIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_OFF\r\nAPPROVE_ALL_IP_USER_REGIST_WHEN_GO_TO_PAGE_ERROR_OFF\r\nOPACITY_CSS_BODY_1\r\nUNVISIBLED_SUB_MENU_ON\r\n" + backupCodeCurrent + "\r\nUSE_BACKUPCODE_AND_NOT_SEND_MAIL_FOR_SETTING_ADMIN_TWO_STEP_OFF\r\nHome--Index\r\nhttp://localhost:5000-http://localhost:5001\r\nFORM_IP_USER_REGIST_OFF\r\nJS_AUTO_CLOSE_WINDOW_OFF\r\nAJAX_JAVASCRIPT_ON\r\nDELAY_DATETIME:0H#0M#0S";
                FileExtension.WriteFile(pam, pam1 + pam2);

                return Redirect("/Admin/SettingXYZ_DarkAdmin#changesetting");

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }


        public ActionResult Admin_ChangePassword()
        {
            try
            {
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }

                var pathSecure = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var pathSetting = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungSecure = FileExtension.ReadFile(pathSecure);
                var noidungSetting = FileExtension.ReadFile(pathSetting);
                var listSetting = noidungSetting.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var key = listSetting[60].Split("<3275>")[3];

                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                return View();

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult Admin_ChangePassword(string? txtIDAdminOld, string? txtIDAdminNew, string? txtPasswordSecureOld, string? txtPasswordSecureNew, string? txtBackupAdminOld, string? txtBackupAdminNew, string? txtSecure)
        {
            try
            {
                var no_logout = false;
                TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                khoawebsiteAdmin();
                var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
                if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
                {
                    if (this.HttpContext.Request.Method == "GET")
                    {
                        if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                        {
                            HttpContext.Session.SetObject("google-trick-web", 1);
                            return Redirect("https://google.com");
                        }
                        else
                        {
                            var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                            if (lan != 10)
                            {
                                HttpContext.Session.SetObject("google-trick-web", lan + 1);
                                return Redirect("https://google.com");
                            }
                        }
                    }
                }

                var pathSecure = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
                var pathSetting = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungSecure = FileExtension.ReadFile(pathSecure);
                var noidungSetting = FileExtension.ReadFile(pathSetting);
                var listSetting = noidungSetting.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var key = listSetting[60].Split("<3275>")[3];

                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                if (string.IsNullOrEmpty(txtPasswordSecureOld) == false && string.IsNullOrEmpty(txtPasswordSecureNew) == false)
                {
                    var newValueEncrypt = StringMaHoaExtension.Encrypt(txtPasswordSecureNew, key);
                    var oldValueEncrypt = StringMaHoaExtension.Encrypt(txtPasswordSecureOld, key);
                    if (noidungSecure.Replace("\r", "").Split("\n")[1] == oldValueEncrypt)
                    {
                        noidungSecure = noidungSecure.Replace(oldValueEncrypt, newValueEncrypt);
                    }
                }

                if (string.IsNullOrEmpty(txtIDAdminOld) == false && string.IsNullOrEmpty(txtIDAdminNew) == false)
                {
                    var newValueEncrypt = StringMaHoaExtension.Encrypt(txtIDAdminNew, key);
                    var oldValueEncrypt = StringMaHoaExtension.Encrypt(txtIDAdminOld, key);
                    if (noidungSecure.Replace("\r", "").Split("\n")[0] == oldValueEncrypt)
                    {
                        noidungSecure = noidungSecure.Replace(oldValueEncrypt, newValueEncrypt);
                    }
                }

                if (string.IsNullOrEmpty(txtBackupAdminOld) == false && string.IsNullOrEmpty(txtBackupAdminNew) == false)
                {
                    var newValueEncrypt = StringMaHoaExtension.Encrypt(txtBackupAdminNew, "32752262");
                    var oldValueEncrypt = StringMaHoaExtension.Encrypt(txtBackupAdminOld, "32752262");
                    if (noidungSecure.Replace("\r", "").Split("\n")[18] == string.Format("{0}{1}", "SKIP_TWOSTEP_SETTING_ADMIN_WITH_BACKUP_CODE-", oldValueEncrypt))
                    {
                        noidungSecure = noidungSecure.Replace(oldValueEncrypt, newValueEncrypt);
                    }
                }

                if (string.IsNullOrEmpty(txtSecure) == false)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                    var pax = FileExtension.ReadFile(path).Split("<>");

                    if (pax.Length > 1)
                    {
                        var noidung = pax[0] + "<>" + pax[1] + "<>" + MD5.CreateMD5(txtSecure);
                        FileExtension.WriteFile(path, noidung);
                        no_logout = true;
                    }

                }
                else
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                    var pax = FileExtension.ReadFile(path).Split("<>");

                    if (pax.Length > 1)
                    {
                        var noidung = pax[0] + "<>" + pax[1];
                        FileExtension.WriteFile(path, noidung);
                    }
                }

                FileExtension.WriteFile(pathSecure, noidungSecure);

                if (no_logout == false)
                {
                    var pamXD = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
                    FileExtension.WriteFile(pamXD, "");
                }

                return Ok(new { result = true });

            }
            catch (Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty;
                HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                FileExtension.WriteFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx);
                var mailError = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[11];
                if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON" && HttpContext.Session.GetString("IsAdminUsing") != "true")
                {
                    Calendar xz = CultureInfo.InvariantCulture.Calendar;
                    var xuxuz = xz.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""));
                    string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");
                    SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + ((HttpContext.Session.GetString("admin-userIP") != null) ? HttpContext.Session.GetString("admin-userIP") : HttpContext.Session.GetString("admin-userIP")) + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", "")) + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");
                }
                return RedirectToAction("Error", "Home", new
                {
                    exception = "true"
                });
            }
        }

        [HttpPost]
        public ActionResult LoginAdminTemp(string? key)
        {
            var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
            try
            {
                var userIP = SetUserIPClientWhenAPI();
                var yes_log = true;

                var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];

                if (userIP != null)
                {
                    if (valuePam == MD5.CreateMD5(userIP)) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                if (HttpContext.Session.GetString("userIP") != null)
                {
                    if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
                }

                var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungWW = FileExtension.ReadFile(pathWW);

                var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (infoXWW[1] == "true" && (yes_log || HttpContext.Session.GetString("NoAdmin_YesLog") == "true"))
                {
                    var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                    var noidungS = FileExtension.ReadFile(pathS);

                    var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                        + "\t" + this.Request.Path + "\t[GET]";

                    FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
                }

                var valuePay = FileExtension.ReadFile(pam).Split("<>")[1];
                if (key == valuePay)
                {
                    HttpContext.Session.SetString("adminSetting", "true");
                    HttpContext.Session.SetString("IsLoginAdminTemp", "true");
                    HttpContext.Session.SetString("admin-userIP", SetUserIPClientWhenAPI());
                    HttpContext.Session.SetString("keyTempAdmin", key);

                    HttpContext.Session.SetString("open-admin", "true");
                    HttpContext.Session.SetString("open-admin-yes", "true");
                    TempData["admin-open"] = "true";

                    return Ok(new { result = "Thành công !" });
                }
                else
                {
                    HttpContext.Session.Remove("adminSetting");
                    HttpContext.Session.Remove("admin-userIP");
                    HttpContext.Session.Remove("IsLoginAdminTemp");

                    HttpContext.Session.Remove("open-admin");
                    HttpContext.Session.Remove("open-admin-yes");
                    TempData.Remove("admin-open");

                    return Ok(new { result = "Thất bại !" });
                }
            }
            catch
            {
                HttpContext.Session.Remove("adminSetting");
                HttpContext.Session.Remove("admin-userIP");
                HttpContext.Session.Remove("IsLoginAdminTemp");

                HttpContext.Session.Remove("open-admin");
                HttpContext.Session.Remove("open-admin-yes");
                TempData.Remove("admin-open");

                return Ok(new { result = "Thất bại rồi !" });
            }
        }

        public string SetUserIPClientWhenAPI()
        {
            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            string ipAddress = string.Empty;
            if (true)
            {
                //using (var client = new HttpClient())
                //{
                //    var response = await client.GetAsync("https://api.ipify.org?format=text");
                //    ipAddress = await response.Content.ReadAsStringAsync();
                //}

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.Headers["X-Forwarded-For"];
                }

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }

            return ipAddress;
        }

        public ActionResult ChangeKeyLoginAdminTemp(string? keyX, bool remove = false)
        {
            var pathXY = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin/SecureSettingAdmin.txt");
            var matpassAd = FileExtension.ReadFile(pathXY).Replace("\r", "").Split("\n")[1];

            if (keyX != matpassAd)
            {
                return RedirectToAction("Error", "Home");
            }

            var settingFile = FileExtension.ReadFile(pathXY).Replace("\r", "").Split("\n")[4];


            var pathX = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = FileExtension.ReadFile(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var lockedApp = listSetting[43].Split("<3275>");
            if (lockedApp[3] != "[NULL]")
            {
                var xa = Request.Path;
                if (xa == "" || xa == "/" || xa == null)  xa = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                if (lockedApp[3].Contains(xa))
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            var pathWW = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungWW = FileExtension.ReadFile(pathWW);

            var listSettingSWW = noidungWW.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var infoXWW = listSettingSWW[22].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

            var yes_log = true;

            var pam = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
            var valuePam = FileExtension.ReadFile(pam).Split("<>")[0];

            if (HttpContext.Session.GetString("admin-userIP") != null)
            {
                if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("admin-userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
            }

            if (HttpContext.Session.GetString("userIP") != null)
            {
                if (valuePam == MD5.CreateMD5(HttpContext.Session.GetString("userIP"))) yes_log = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===").Length > 1 && FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[23].Split("===")[1].Split("###")[0] == "NOT_IS_ADMINUSING" ? true : false;
            }

            if (infoXWW[1] == "true" && (yes_log || HttpContext.Session.GetString("NoAdmin_YesLog") == "true"))
            {
                var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
                var noidungS = FileExtension.ReadFile(pathS);

                var noidungZ = noidungS + "\n" + HttpContext.Session.GetString("admin-userIP") + "\t" + CultureInfo.InvariantCulture.Calendar.AddHours(DateTime.UtcNow, 7).SendToDelaySetting(FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[25].Replace("DELAY_DATETIME:", ""))
                    + "\t" + this.Request.Path + "\t[GET]";

                FileExtension.WriteFile(pathS, noidungZ.Trim('\n'));
            }

            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

            if (onoff == "ADMINSETTING_OFF")
                return Redirect("https://google.com");

            for (int i = 0; i < listSetting.Length; i++)
            {
                var info = listSetting[i].Split("<3275>", StringSplitOptions.RemoveEmptyEntries);

                if (info[0] == "LockedAll_Web")
                {
                    if (info[1] == "true")
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*_-=+?:;'{}[]|\\() /,.<>\"`~";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                var no = chars[random.Next(chars.Length)];
                if ((i == stringChars.Length - 1 && no == '\\') || (i == 0 && no == '<'))
                {
                    i--;
                    continue;
                }

                stringChars[i] = no;
            }

            var key = new String(stringChars);

            var pax = FileExtension.ReadFile(pam);
            if (string.IsNullOrEmpty(pax) == false)
            {
                var pac = pax.Split("<>");
                var moi = "";
                if (remove == false)
                {
                    moi = pac[0] + "<>" + key;
                    if (pac.Length > 2)
                    {
                        moi += "<>" + pac[2];
                    }
                }
                else
                {
                    moi = pac[0];
                }

                FileExtension.WriteFile(pam, moi);
            }

            return Ok(new { result = "Successful !" });
        }

        [HttpPost]
        public ActionResult ShowHtmlPlay(string? website, string? method)
        {
                var nd = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "Html_Play.txt")).Replace("\r", "").Split("\n#3275#\n");
                var urlRoot = "/" + FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[20].Replace("--", "/");
                 if (website == "/" | website == "/Home" || website == "/Home/") website = urlRoot;
                 for (var i = 0; i < nd.Length; i++)
                {
                    var ndx = nd[i].Split("\n||\n");
                    var ndy = ndx[0].Split("---");
                    if (((ndy[0] == website) || (ndy[0] == "ONLY_HOME" && website.Contains("/Home/"))
                    || (ndy[0] == "ONLY_ADMIN" && website.Contains("/Admin/")) || (ndy[0] == "ONLY_COVER" && website.Contains("/Cover/")) || (ndy[0] == "ALL") || 
                        (ndy[0] == "BOQUA") || (ndy[0] == "CHIBAOGOM"))
                            && (string.IsNullOrEmpty(method) || ndy.Length < 2 || string.IsNullOrEmpty(method) == false && ndy.Length > 1 && ndy[1] == method || ndy[1] == "NULL"))
                    {
                        if (ndy.Length > 2 && ((ndy[0] == "BOQUA" && ndy[2].Contains(website)) || (ndy[0] == "CHIBAOGOM" && ndy[2].Contains(website) == false)))
                        {
                             return Ok(new { result = false });
                        }

                        return Ok(new { result = true, html = ndx[1] });
                    }
                }

            return Ok(new { result = false });
        }

        [HttpPost]
        public ActionResult GetDataAdminSetting(string? ID, string? code, string? type, string? key)
        {
            try
            {
                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");

                var set1 = FileExtension.ReadFile(path1).Replace("\r", "").Split("\n");
                var set2 = FileExtension.ReadFile(path2).Replace("\r", "").Split("\n");

                var keyX = set1[60].Split("<3275>")[3];

                if (set2[0] == StringMaHoaExtension.Encrypt(ID, keyX) && set2[1] == StringMaHoaExtension.Encrypt(code, keyX))
                {
                    if (type == "1")
                    {
                        var value = set1[int.Parse(key)].Split("<3275>");
                        if (value.Length > 3)
                        {
                            return Ok(new { result = true, data = value[3] });
                        }
                        else
                        {
                            return Ok(new { result = true, data = value[1] });
                        }
                    }
                    else if (type == "2")
                    {
                        var value = set2[int.Parse(key)];
                        return Ok(new { result = true, data = value });
                    }
                }
            }
            catch(Exception ex)
            {
                return Ok(new { result = false, data = ex.Message });
            }

            return Ok(new { result = false });
        }

        [HttpPost]
        public ActionResult TestSendMailOrMegaIO(string? type)
        {
            if (type == "1")
            {
                MailRequest mail = new MailRequest();
                var data = _mailService.TestSendMail(mail, _webHostEnvironment.WebRootPath);
                return Ok(new { result = data });
            }
            else if (type == "2")
            {
                var data = MegaIo.TestMegaIO(_webHostEnvironment.WebRootPath);
                return Ok(new { result = data });
            }

            return Ok(new { result = false });
        }

        [HttpPost]
        public ActionResult GetDataSession(string ID, string code, string type, string key)
        {
            var data = "";
            try
            {
                var path1 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var path2 = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");

                var set1 = FileExtension.ReadFile(path1).Replace("\r", "").Split("\n");
                var set2 = FileExtension.ReadFile(path2).Replace("\r", "").Split("\n");

                var keyX = set1[60].Split("<3275>")[3];

                if (set2[0] == StringMaHoaExtension.Encrypt(ID, keyX) && set2[1] == StringMaHoaExtension.Encrypt(code, keyX))
                {
                    switch (type)
                    {
                        case "1":
                            data = HttpContext.Session.GetString(key);
                            break;

                        case "2":
                            data = HttpContext.Session.GetObject<string>(key);
                            break;

                        case "3":
                            data = TempData[key].ToString();
                            return Ok(new { result = true, value = data });
                    }
                }
            }
            catch
            {
                return Ok(new { result = false });
            }

            return Ok(new { result = true, value = data });
        }

        [HttpPost]
        public ActionResult ReloadIPComeHere()
        {
            TempData["opacity-body-css"] = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[16].Replace("OPACITY_CSS_BODY_", "");
            if (HttpContext.Session.GetString("adminSetting") == null)
            {
                return RedirectToAction("LoginSettingAdmin");
            }


            khoawebsiteAdmin();
            var dua = FileExtension.ReadFile(Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n")[14];
            if (dua == "DIRECT_GOOGLE.COM_10_TIMES_AFTER_TO_COME_MYWEBPLAY_ON")
            {
                if (this.HttpContext.Request.Method == "GET")
                {
                    if (HttpContext.Session.GetObject<int>("google-trick-web") == null)
                    {
                        HttpContext.Session.SetObject("google-trick-web", 1);
                        return Redirect("https://google.com");
                    }
                    else
                    {
                        var lan = HttpContext.Session.GetObject<int>("google-trick-web");
                        if (lan != 10)
                        {
                            HttpContext.Session.SetObject("google-trick-web", lan + 1);
                            return Redirect("https://google.com");
                        }
                    }
                }
            }

            if (TempData["locked-app"] == "true")
                return RedirectToAction("Error", "Home", "Home");

            var pth = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt");
            var onoff = FileExtension.ReadFile(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

            if (onoff == "ADMINSETTING_OFF")
                return Redirect("https://google.com");

            var pathS = Path.Combine(_webHostEnvironment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "ClientConnect/ListIPComeHere.txt");
            var data = FileExtension.ReadFile(pathS);

            data = Regex.Replace(data, @"^.*\[DEBUG\].*\n?", string.Empty, RegexOptions.Multiline);
            data = data.Replace("\r", "");
            do
            {
                data = data.Replace("\n\n", "\n");
            }
            while(data.Contains("\n\n"));

            return Ok(new { result = data });
        }
    }
}