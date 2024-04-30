using Microsoft.AspNetCore.Mvc;
using MyWebPlay.Extension;
using MyWebPlay.Model;
using System.Globalization;

namespace MyWebPlay.Controllers
{
    public partial class AdminController : Controller
    {
        public ActionResult ListWebPage(string kbn)
        {
            try
            {
                if (HttpContext.Session.GetString("adminSetting") == null)
                {
                    return RedirectToAction("LoginSettingAdmin");
                }

                if (string.IsNullOrEmpty(kbn))
                    return RedirectToAction("Error", "Home");

                khoawebsiteAdmin();
                if (TempData["locked-app"] == "true")
                    return RedirectToAction("Error", "Home");

                var pth = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
                var onoff = System.IO.File.ReadAllText(pth).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[2];

                if (onoff == "OFF")
                    return Redirect("https://google.com");

                if (kbn == "0")
                {
                    TempData["KbnPage"] = "* Vui lòng tick chọn các page web không cho phép sử dụng ở bên dưới vả nhấn nút OK để áp dụng : ";
                    ViewBag.PageKbn = "0";
                }
                else
if (kbn == "1")
                {
                    TempData["KbnPage"] = "* Vui lòng tick chọn các page web cho phép sử dụng ở bên dưới vả nhấn nút OK để áp dụng : ";
                    ViewBag.PageKbn = "1";
                }


                var listPage = new List<string>();
                var ptha = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "ListPageSetting.txt");
                var page = System.IO.File.ReadAllText(ptha).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                return RedirectToAction("Error", new { exception = "true" });
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
                var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
                var noidungX = System.IO.File.ReadAllText(pathX);
                var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

                var lockedApp = listSetting[43].Split("<3275>");
                if (lockedApp[3] != "[NULL]")
                {
                    var xin = Request.Path; if (xin == "" || xin == "/" || xin == null) xin = "/Home/Index"; if (lockedApp[3].Contains(xin))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                var key = listSetting[60].Split("<3275>")[3];
                var pth = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
                if (f["ID"].ToString() != StringMaHoaExtension.Decrypt(pth[0], key) || f["Key"].ToString() != StringMaHoaExtension.Decrypt(pth[1], key))
                {
                    return Ok(new { error = "Đã xảy ra lỗi, vui lòng thử lại sau !" });
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);

                var ndSet = "OffWebsite_All<3275>false<3275><br />Tắt hoạt động web site (<b style=\"color:blue\">áp dụng tất cả <span style=\"color:green\">Home</span></b>, sẽ không thông báo và tự động chuyển hướng sang trang lỗi (<span style=\"color:red\">404</span>))\r\nUsing_Website<3275>false<3275><br />Hoạt động website <span style=\"color:blue\">[MASTER]</span> : sẽ không có tác dụng (sẽ luôn như là tắt) khi 3 setting \"Request data nhanh, chế độ tàng hình và bỏ qua xác minh [ADMIN] bên dưới được bật cùng lúc\".\r\nAlert_UsingWebsite<3275>false<3275><br />Thông báo khi website tắt hoạt động (nếu tắt thì sẽ chuyển hướng sang website tạm khác (*) và không giải thích gì thêm/user sẽ khó biết được vấn đề) - kể cả bị lock website bởi admin hoặc client nội bộ,...vv... Hãy thật cẩn thận khi muốn tắt setting này!\r\nClear_Website<3275>false<3275><br />[Admin] Chế độ tàng hình website (một số tính năng sẽ rất khó hoặc không thể sử dụng). Nếu bạn bật setting này, hệ thống sẽ không get IP của user (sẽ lấy mặc định : <span style=\"color:orangered\">0.0.0.0</span>).\r\nUsing_QuickData<3275>false<3275><br />[Admin] Áp dụng dịch vụ request POST excute data nhanh (nếu tắt sẽ được chuyển hướng đến website tạm khác (*)). Lưu ý, đối với kí tự sẽ tự động replace tượng trưng cho dấu ngoặc kép <span style=\"color:red\">\"</span>, bạn nên điền <span style=\"color:red\">[ngoackep_0000]</span> nhé!\r\nNotAlert_QuickData<3275>false<3275><br />[Admin] Bỏ qua bước xác minh - request POST excute data nhanh (chỉ dùng khi bạn bật setting UsingQuickData và chế độ tàng hình website - và phải bật hoạt động website)\r\nRandom_Layout<3275>false<3275><br />Bật sử dụng RandomLayout ngẫu nhiên có sẵn (dành cho luồng REQUEST QUICK POST DATA - admin)\r\nPost_Clipboard<3275>false<3275><br />Download nội dung kết quả POST từ file .txt (thay vì mặc định copy vào clipboard/giám thị hoạt động cho luồng gửi data quick request POST và chế độ tàng hình + không xác minh [ADMIN], còn bình thường cũng có thể áp dụng tất cả...)\r\nConnect_LinkDown<3275>false<3275><br />Chuyển hướng trang kết quả POST sang link file TXT result, có thể sử dụng cho cả dịch vụ Admin quick request data hoặc API (nhưng khuyên bạn nên hạn chế dùng tính năng này nếu dữ liệu của bạn có chứa kí tự UTF8/Tiếng Việt)\r\nOff_RandomTab<3275>false<3275><br />Tắt chế độ random view tab website\r\nAuto_Excute<3275>false<3275><br />Nếu các setting : request POST data quick, chế độ tàng hình và bỏ qua xác minh [ADMIN] trên được bật cùng lúc thì sau khi đến trang kết quả POST sẽ tự động copy/download result và chuyển hướng đến trang tạm khác (*)\r\nViewSite_Pattern<3275>false<3275><br />Bật chế độ giao diện pattern (không header)\r\nViewSite_Basic<3275>false<3275><br />Bật chế độ giao diện cơ bản - xấu tệ (cho phép website với mọi người)\r\nEmail_Upload_User<3275>false<3275><br />Thông báo email khi user upload file\r\nMegaIo_Upload_User<3275>false<3275><br />Lưu trữ các file upload của user tại Mega.Io\r\nEmail_TracNghiem_Create<3275>false<3275><br />Thông báo email bài trắc nghiệm được tạo\r\nEmail_TracNghiem_Update<3275>false<3275><br />Thông báo email bài trắc nghiệm đã update (không đảm bảo)\r\nEmail_Question<3275>false<3275><br />Thông báo email bài question được tạo\r\nEmail_User_Website<3275>false<3275><br />Thông báo email phát hiện user bật website\r\nEmail_User_Continue<3275>false<3275><br />Thông báo email phát hiện user tiếp tục sử dụng website\r\nBlock_RegistUsing<3275>false<3275><br />Tạm ngưng nhận đơn đăng kí sử dụng web site tiếp theo\r\nEmail_Note<3275>false<3275><br />Thông báo email note text (bản nháp ghi chú) mới\r\nSave_ComeHere<3275>false<3275><br />Save request of user/list request used (khi tắt các dữ liệu mới sẽ không được lưu trữ và các dữ liệu cũ vẫn sẽ được bảo toàn)\r\nMail_ReportUrl<3275>false<3275><br />Auto send mail with IP request come used (if data than 100) - nếu bạn tắt thì dữ liệu vẫn sẽ bị khôi phục nhưng không gửi mail report\r\nPlay_EncodeUrl<3275>false<3275><br />Cho phép sử dụng chức năng nội bộ website từ link url được mã hoá (hãy nhập tiếp url hệ thống bạn muốn sử dụng <a id=\"encode-set\" href=\"#set-encode\">ở setting dưới</a>)\r\nOff_CallIP<3275>false<3275><br />Tắt kết nối tìm IP user (cho phép website với mọi người)\r\nAll_People<3275>false<3275><br />Cho phép website với mọi người (nhưng vẫn get thông tin IP; và IP vẫn sẽ bị khoá nếu có bởi client/admin)\r\nFullScreen_Web<3275>false<3275><br />[Trò đùa/Tự động web] Toàn màn hình khi sử dụng (chỉ khi user có sự tương tác mỗi khi truy cập), một số tính năng có thể bị hạn chế\r\nEmail_Karaoke<3275>false<3275><br />Thông báo email/lưu trữ dữ liệu bản tạo mới Karaoke của user (sẽ gửi 2 bản email : mp3 và text Karaoke)\r\nEmail_Karaoke_OnlyText<3275>false<3275><br />Nếu bạn đã bật setting nhận thông báo email khi user tạo bản mới Karaoke trên (chỉ nhận dữ liệu bản Text karaoke, không cần mp3) - nên bật cái này vì các bản mp3 dung lượng có thể lớn làm ảnh hưởng phát sinh\r\nChienDich_LocalStorage<3275>false<3275><br />Mở chiến dịch tự động xoá hết dữ liệu localStorage từ JS của local khách hàng đối với website khi truy cập/kể cả admin nếu có - chiến dịch chỉ hoạt động mỗi khi bị truy cập page <b style=\"color:red\">Error</b> (có nghĩa là : sau khi kết thúc chiến dịch, những dữ liệu của localStorage JS của admin/user đã từng lưu trữ sẽ bị xoá hoàn toàn khi được truy cập lại website vào lần tới)\r\nNotUse_Believe<3275>false<3275><br />Không cho phép sử dụng tính năng mật độ tuyệt đối và các IP tin tưởng được phép truy cập website\r\nTangHinh_UseUploadFile<3275>false<3275><br />[Khi bạn bật chế độ tàng hình web site] Khi bạn truy cập đến trang <span style=\"color:blue\">UploadFile</span>, sẽ tự động chuyển hướng sang trang upload file dành cho Admin. Sau đó thử click đại đâu đó bất kỳ cũng sẽ tự động mở cửa sổ chọn file và submit dữ liệu (chỉ <span style=\"color:red\">Gmail</span>) sau đó.\r\nLockedAll_Web<3275>false<3275><b style=\"color:brown\"><br />[Đặc biệt] Khoá trang web tuyệt đối/vĩnh viễn => error page (tất cả, không trang nào truy cập được, kể cả admin)</b>\r\nPassword_Admin<3275>true<3275><br />Mật khẩu admin (nếu để trống mật khẩu mặc định sẽ là : <b style=\"color:red\">mywebplay-ADMIN</b>)<3275>mywebplay-ADMIN\r\nCode_LockedClient<3275>true<3275><br />Nhập mật mã dự phòng (mã mở khoá dự phòng dành cho LockedWebClient - trừ khi user có thiết kế mật khẩu riêng, mặc định nếu để trống sẽ là <b style=\"color:red\">abc-xyz</b>).Thật ra cái này cũng nên hạn chế nói cho user biết...<3275>abc-xyz\r\nBelieve_IP<3275>true<3275><br />IP tin tưởng - bỏ qua xác minh và báo cáo hành động user (lưu ý vì có nhiều đối tượng có cùng nhóm, nội bộ và địa chỉ IP nên chỉ các thiết bị cần thiết nên bật <b>chế độ tin tưởng</b> để có sự tác dụng này, mỗi IP liệt kê phân biệt nhau bởi dấu phẩy (và bạn nên tự để ý coi chừng liệt kê trùng IP hoặc đã có sẵn nhé 😁) : nếu IP sự tin tưởng nào đã bị xoá khỏi DS bên dưới thì các thiết bị từng set IP tin tưởng này trước đây sẽ bị xoá - và xin lưu ý, nếu IP của bạn bị khoá bởi admin hay một số vấn đề khác vẫn có thể bị xác minh như thường), và cũng lưu ý tính năng này sẽ không thể tác dụng ngay lập tức, hãy vui lòng thử lại nhiều lần, xin lỗi vì sự bất tiện này 😎...<3275>[NULL]\r\nMatDoTuyetDoi<3275>true<3275><br />[Mật độ tuyệt đối] là phiên bản nâng cao của việc sử dụng trang web đối với thiết bị tin tưởng (vì điều này có phần quan ngại và có chút nguy hiểm ⚠, cho nên bạn <b style=\"color:red\">hãy suy nghĩ thật kỹ</b> trước khi áp dụng - mà thường sử dụng cho chính bản thân bạn là chính 😀; ở đây nó chỉ khác ở chỗ là admin sẽ khó khoá sử dụng hoặc xác nhận info (tự do), nhưng mọi hành động vẫn có thể gửi báo cáo về admin nếu muốn). Cách sử dụng : trình duyệt bạn muốn làm chế độ tin tưởng, F12 để mở giao diện DEV TOOL, sang Console bạn gõ lệnh : <b style=\"color:deeppink\">window.localStorage.setItem(\"<span style=\"color:green\">Key</span>\",\"<span style=\"color:green\">Value</span>\");</b>.<br>Trong đó, <span style=\"color:brown\">key&lt;&gt;value</span> tại đây nếu không nhập thì mặc định sẽ là <b style=\"color:blue\">matdotuyetdoi&lt;&gt;believix-123</b>. Nếu bạn vô tình thay đổi key hoặc value của mã mật độ khác tại setting này, các thiết bị đã set tin tưởng trước đó sẽ bị xoá...<3275>matdotuyetdoi<>believix-123\r\nEncode_Url<3275>true<3275><br /><a id=\"set-encode\" href=\"#encode-set\">Sử dụng chế độ tính năng bằng url thông qua trang web với link được mã hoá</a> (lưu ý chỉ riêng nội bộ hợp lệ của hệ t<span style=\"color:deeppink\">mà cho phép sử dụng</span>hống. Giả sử bạn chỉ cần nhập : <span style=\"color:blue\">/Home/PlayTracNghiem</span>), hãy nhập tại đây chính xác và tồn tại một url [bắt đầu] của hệ thống mà bạn muốn sử dụng...<br />Bạn có giới hạn 10 link có thể sử dụng theo lần lượt (link 1 : <a target=\"_blank\" href=\"/Cover/Ee90d45ca0d59031d2a3b6dc488187c00\" style=\"color:red\">/Cover/Ee90d45ca0d59031d2a3b6dc488187c00</a>, link 2 : <a target=\"_blank\" href=\"/Cover/E41fb870a2ab7c2470cb35f51171e20ee\" style=\"color:red\">/Cover/E41fb870a2ab7c2470cb35f51171e20ee</a>, link 3 : <a target=\"_blank\" href=\"/Cover/E8ecb5a3a2b4fdbda327335d19f3ca7fa\" style=\"color:red\">/Cover/E8ecb5a3a2b4fdbda327335d19f3ca7fa</a>, link 4 : <a target=\"_blank\" href=\"/Cover/E003e16661a80c9451f46587afb2ec5c3\" style=\"color:red\">/Cover/E003e16661a80c9451f46587afb2ec5c3</a>, link 5 : <a target=\"_blank\" href=\"/Cover/Ef8e2d510133438f980423324078e0939\" style=\"color:red\">/Cover/Ef8e2d510133438f980423324078e0939</a>, link 6 : <a target=\"_blank\" href=\"/Cover/E72fd1425ee00a7192a7261c5b45fcab2\" style=\"color:red\">/Cover/E72fd1425ee00a7192a7261c5b45fcab2</a>, link 7 : <a target=\"_blank\" href=\"/Cover/E86a31cf62149eaf28543a8a639d91309\" style=\"color:red\">/Cover/E86a31cf62149eaf28543a8a639d91309</a>, link 8 : <a target=\"_blank\" href=\"/Cover/E8b5931c6ba81548e4b0a06e07fdd5282\" style=\"color:red\">/Cover/E8b5931c6ba81548e4b0a06e07fdd5282</a>, link 9 : <a target=\"_blank\" href=\"/Cover/Ee98b70f046cbdf12b3eee2d65e625373\" style=\"color:red\">/Cover/Ee98b70f046cbdf12b3eee2d65e625373</a>, link 10 : <a target=\"_blank\" href=\"/Cover/E10f628a33fcf828bb57d497794a34094\" style=\"color:red\">/Cover/E10f628a33fcf828bb57d497794a34094</a>), được liệt kê tách nhau bởi <span style=\"color:green\">***</span><3275>/Home/Index***/Home/SecretWeb***/Admin/QuickDataInWeb***/Home/UploadFile***/Home/UploadFile?type=1<splix>0<splix>0***/Home/DownloadFile***/Home/EditTextNote***/Home/DownloadFile_ClearWeb\r\nInfo_Email<3275>true<3275><br />Nhập thông tin email (chỉ <span style=\"color:blue\">Gmail</span>) dùng để gửi thông báo liên quan dịch vụ/lưu trữ admin (nếu không nhập mặc định sẽ lấy email và mật khẩu mặc định hiện đang được lấy data setting trong file <b>appsettings.json</b> (nó có thể sẽ hết hạn) [?]). Nếu bạn nhập ở đây, các thông tin sẽ được thay thế và sử dụng để xử lý. Mời bạn nhập theo kiểu : <b style=\"color:red\">Email<5828>Mật khẩu</b> (sẽ tự động mã hoá sau đó) <3275>72aYAo76lcpRlZL1WdKp91iq6iJts1VmRjhBI0Aj9ng68fIbsEHJ/bdmASwYIiE90T/hCPfznYXUKPb1flRqrw==<5828>52LY6RvmKCJZCsWbXOdT4fWjuDlojLJacCa17UVAL5gDVcHW1ut9AwKstqHBdXpz[Encrypted_3275]\r\nInfo_MegaIO<3275>true<3275><br />Nhập thông tin email tài khoản <span style=\"color:blue\">MegaIO</span> để lưu trữ các file upload + Admin (nếu không nhập mặc định sẽ lấy email và mật khẩu mặc định hiện đang được lấy data setting trong file <b>appsettings.json</b> (nó có thể sẽ hết hạn)). Nếu bạn nhập ở đây, các thông tin sẽ được thay thế và sử dụng để xử lý. Mời bạn nhập theo kiểu : <b style=\"color:red\">Email<5828>Mật khẩu</b> (sẽ tự động mã hoá sau đó) <3275>72aYAo76lcpRlZL1WdKp91iq6iJts1VmRjhBI0Aj9ng68fIbsEHJ/bdmASwYIiE90T/hCPfznYXUKPb1flRqrw==<5828>9WwfyhJev7z/YhEj0BfrkxZt8I6U2nbp/e7JGIVdHdo=[Encrypted_3275]\r\nColor_BackgroundAndText<3275>true<3275><br /><span id=\"demo-X\">[Hạn chế sử dụng] Hãy nhập với định dạng : <span style=\"color:red\">Mã màu background<>Mã màu text</span> để thiết kế hiển thị website (xem/sửa demo <a href=\"#background-demo\">ở bên dưới</a>). Nếu bạn để trống trường này tức là không sử dụng và mặc định sẽ sử dụng nền sáng/tối của hệ thống...<3275>[NULL]\r\nColor_TracNghiem<3275>true<3275><br />[Để phù hợp hiển thị] Hãy nhập tại đây mã màu/tên màu để quét đáp án mà user đã chọn cho câu hỏi đó (dành cho tính năng Play Trắc Nghiệm), hiện tại mặc định là màu xanh lá - <span style=\"color:green\">green</span> (nếu bạn có tác động edit setting của màu nền và text website ở trên)...<3275>green\r\nAppWeb_LockedUse<3275>true<3275><br />Danh sách các <span style=\"color:red\">/Controller/ActionName</span> của các dịch vụ web, tiện ích hiện có <span style=\"color:yellow\">mà không thể sử dụng</span> hoặc truy cập được ngay lúc này (liệt kê cách nhau bởi dấu phẩy) => sẽ sang trang error page nếu tiện ích đó bị chặn (không khả dụng với các page : <span style=\"color:blue\">/Home/SessionPlay_DarkAdmin</span>, page error và 10 link của dịch vụ url mã hoá,...) và hãy cẩn thận khi muốn áp dụng<3275>[NULL]\r\nDownloadFile_ClearWeb<3275>true<3275><br />[Khi bạn bật chế độ tàng hình website] Liệt kê các tên file - chỉ cần tên file và đuôi mở rộng (mỗi cái tách nhau bởi dấu phẩy). Hướng dẫn sử dụng : tải lên các file này (ngay thư mục của demo <span style=\"color:red\">/file/Folder1/Folder2/</span>) của trang <span style=\"color:blue\">UploadFile</span>, sau đó nếu bạn truy cập đến trang <span style=\"color:blue\">/Home/DownloadFile_ClearWeb</span> sẽ lần lượt được tự động click tải xuống các file này...<3275>[NULL]\r\nSessionPlay_Unenabled<3275>false<3275><br /><hr><br /><b>[SUB]</b> Không cho phép sử dụng trang Session Play (setting này bạn phải tự bật/tắt thủ công)\r\nDownload_Quick<3275>false<3275><br /><b>[SUB]</b> Cho phép sử dụng mẫu page tạm để download file upload. Hướng dẫn sử dụng : upload cho mỗi lần duy nhất 1 file lên server (nếu không vui lòng nén hết nó) - [nhiều file cũng được nhưng phải liệt kê nhiều 😁] và phải tải lên thư mục demo : /Folder1/Folder2. Sau đó; bạn tự tạo project hay file HTML của riêng bạn (chỉ để mạo diện) và gán thẻ <span style=\"color:blue\">&lt;a href=\"localhost/Home/DLQ?path=tên file của bạn; VD : filedemo.zip (tốt nhất link nên giả diện bằng bom.so)\" download&gt;Tải xuống&lt;/a&gt;</span> và bạn tự click vào link button này để tải xuống - hạn chế tự ý truy cập link này một cách trực tiếp (setting này bạn tự tắt/bật thủ công)\r\nExternal_Unenable<3275>false<3275><br /><b>[SUB]</b> Không cho phép sử dụng form HTML mạo diện và external sử dụng dịch vụ (save file, send log mail admin) - cũng tự thủ công nhé\r\nExternal_Post<3275>false<3275><br /><b>[SUB]</b> Sử dụng POST DATA EXTERNAL và result sang dạng JSON (tự setting thủ công) - khi setting này bật, trang POST data đó không quan tâm dữ liệu IP user hay web site nếu không hoạt động (cũng tựa như API - ngoại trừ một số tính năng)\r\nEncrypt_Data<3275>false<3275><br /><b>[SUB]</b> Mã hoá nội dung tải xuống file TXT của các dịch vụ (trắc nghiệm, question, karaoke text), với key mã hoá là <span style=\"color:deeppink\">buivanluat-ADMIN3275</span> - tự setting thủ công\r\nAccept_ListUrl<3275>true<3275><br /><b>[SUB]</b> Liệt kê các <span style=\"color:red\">/Controller/ActionName</span> của các dịch vụ web, tiện ích hiện có <span style=\"color:yellow\">mà cho phép sử dụng</span> ngay cả khi web không hỗ trợ/hoạt động (cách nhau bởi dấu phẩy). Khi áp dụng, user sẽ phải tự truy cập bằng link này bằng cách url trực tiếp.<3275>[NULL]\r\nTabTittle_NoiDung<3275>true<3275><br /><b>[SUB]</b> Nội dung hiển thị cơ bản của Tab Tittle Web tại site Home (mặc định là : <span style=\"color:blue\">C# | Asp Net Core | Công nghệ thông tin</span> - ngoại trừ một số page (tự setting thủ công)<3275>C# | Asp Net Core | Công nghệ thông tin\r\nUploadFile_MaxSize<3275>true<3275><br /><b>[SUB]</b> Số lượng size max upload file mỗi lần có thể tải lên server (đơn vị KB), cơ bản là <span style=\"color:red\">2000000 KB</span> - tự setting thủ công<3275>0\r\nKaraoke_RandomImage<3275>false<3275><br /><b>[SUB]</b> Khi Play Karaoke nếu chọn phát background với ảnh ngẫu nhiên của hệ thống (chỉ với <span style=\"color:red\">ToPlayKaraoke</span>), sẽ tự động sau mỗi 5 giây sẽ hiển thị sang ảnh khác và lặp lại - tự setting thủ công\r\nIPUser_UnVisible<3275>false<3275><br /><b>[SUB]</b> Ẩn nội dung IP của user hiển thị trên phần header, thay thế hiển thị bằng <span style=\"color:chartreuse\">###</span> - tự setting thủ công\r\nTime_Waiting<3275>true<3275><br /><b>[SUB]</b> [Chỉ số nhân - ảo tưởng] Chỉ số thời gian chờ đại diện waiting cho các process tạo các file trắc nghiệm - question - karaoke play *.TXT, với các phần tử nhân tương ứng lần lượt là : <span style=\"color:red\">3 - 3 - 6</span> - tự setting thủ công<3275>0\r\nHTML_Visible<3275>true<3275><br /><b>[SUB]</b> Ẩn tất cả nội dung HTML hiển thị của các trang web hệ thống/tính năng (trở thành trang web trắng, lưu ý một số luồng sẽ không còn hoạt động đúng cách) - tự setting thủ công # với cách thiết lập (<b>0</b> : không áp dụng || <b>1</b> : chỉ áp dụng cho các site Home (ngoại trừ trang SessionPlay) và các trang tính năng hiện có của Admin || <b>2</b> : áp dụng tất cả và không ngoại lệ - <span style=\"color:red\">cẩn thận và tự giác 😎</span>)<3275>0\r\nGet_Blocked<3275>false<3275><br /><b>[SUB]</b> Chặn tất cả các trang web có phương thức GET (phương thức POST không thể chặn vì được sử dụng với các dịch vụ External/API)\r\nAdmin_Setting<3275>true<3275><br /><b>[SUB]</b> An toàn - Tên file điều khiển admin setting đang sử dụng (mặc định là : <span style=\"color:red\">SettingABC_DarkBVL.txt</span>)<3275>SettingABC_DarkBVL.txt\r\nAdmin_MiniWeb<3275>false<3275><br /><b>[SUB]</b> Cấp quyền sử dụng mini web admin (trang web tạm thời sẽ không hoạt động và chỉ dành cho admin)\r\nKeyEncrypt_Admin<3275>true<3275><br /><b>[SUB]</b> Key dùng để mã hoá/giải mã sử dụng cho thông tin nhận dạng Admin (secure setting) - khi bạn thay đổi, thông tin ID và mật khẩu admin cũng sẽ được cập nhật lại tương ứng (mặc định là : <span style=\"color:red\">buivanluat-ADMIN3275</span>)<3275>buivanluat-ADMIN3275\r\nTwoStep_Admin<3275>false<3275><br /><b>[SUB]</b> Sử dụng gửi email để nhận mã xác thực 2 bước bí mật mỗi khi đăng nhập vào dịch vụ Admin Setting";

                System.IO.File.WriteAllText(path, ndSet);
                var px = Path.Combine(_webHostEnvironment.WebRootPath, "Admin/Setting_Status.txt");

                Calendar xi = CultureInfo.InvariantCulture.Calendar;

                var xuxu = xi.AddHours(DateTime.UtcNow, 7);

                System.IO.File.WriteAllText(px, "Đã tắt hết các item setting (kể cả các setting phụ) # " + xuxu);


                return Ok(new { result = "Thành công, dữ liệu đã được khôi phục !" });
            }
            catch(Exception ex)
            {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11];                 if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON")                 {                     Calendar xz = CultureInfo.InvariantCulture.Calendar;                      string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);                       string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", "");                      SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com","mywebplay.savefile@gmail.com", hostz + "[ADMIN - "+TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz,"[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx, "teinnkatajeqerfl");                 }
                return Ok(new { error = HttpContext.Session.GetObject<string>("error_exception_log") });
            }
        }

        public ActionResult CapNhatStatusSetting (string st)
        {
            Calendar xi = CultureInfo.InvariantCulture.Calendar;

            var xuxu = xi.AddHours(DateTime.UtcNow, 7);

            System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin/Setting_Status.txt"), st + " # "+ xuxu + " (trạng thái do admin tự đặt)");
            return RedirectToAction("SettingXYZ_DarkAdmin");
        }

        [HttpPost]
        public ActionResult ThayDoiSetting (IFormCollection f)
        {
            try
            {
            var fix = "";
            foreach (var item in f.Keys)
            {
                fix += string.Format("{0} : {1}\n", item, f[item]);
            }

            HttpContext.Session.SetString("hanhdong_3275", fix);

            var pthX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt");
            var password = System.IO.File.ReadAllText(pthX).Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)[1];

            var pathX = Path.Combine(_webHostEnvironment.WebRootPath, "Admin", System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)[4]);
            var noidungX = System.IO.File.ReadAllText(pathX);
            var listSetting = noidungX.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var key = listSetting[60].Split("<3275>")[3];

            if (StringMaHoaExtension.Decrypt(password, key) == f["txtPassword"].ToString())
            {
                var logset = f["txtSetting"].ToString();
                System.IO.File.WriteAllText(pthX, logset);
            }
          }
          catch (Exception ex)
          {
                var req = Request.Path;

                if (req == "/" || string.IsNullOrEmpty(req))
                    req = "/Home/Index";

                var errx = (HttpContext.Session.GetString("hanhdong_3275") != null) ? HttpContext.Session.GetString("hanhdong_3275") : string.Empty; HttpContext.Session.SetObject("error_exception_log", "[Exception/error log - " + req + " - " + Request.Method + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); System.IO.File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "EXCEPTION_ERROR_LOG.txt"), "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n====================\n\n" + errx); var mailError = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Admin", "SecurePasswordAdmin.txt")).Replace("\r", "").Split("\n")[11]; if (mailError == "SEND_MAIL_WHEN_ERROR_EXCEPTION_ON") { Calendar xz = CultureInfo.InvariantCulture.Calendar; string xuxuz = xz.AddHours(DateTime.UtcNow, 7).ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture); string hostz = "{" + Request.Host.ToString() + "}".Replace("http://", "").Replace("https://", "").Replace("/", ""); SendEmail.SendMail2Step(_webHostEnvironment.WebRootPath, "mywebplay.savefile@gmail.com", "mywebplay.savefile@gmail.com", hostz + "[ADMIN - " + TempData["userIP"] + "] REPORT ERROR/ECEPTION LOG OF USER In " + xuxuz, "[Exception/error log - " + req + " - " + Request.Method + " - " + DateTime.Now + " - " + ex.Source + "] : " + ex.Message + "\n\n" + ex.StackTrace, "teinnkatajeqerfl"); }
                return RedirectToAction("Error", new { exception = "true" });
          }

            return Redirect("SettingXYZ_DarkAdmin#changesetting");
        }
    }
}
