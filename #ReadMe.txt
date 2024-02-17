[Lưu ý : thiết đặt mật khẩu mới (Admin) nên tránh 1 số các kí tự đặc biệt, bị nhầm lẫn với URL và mã hoá với HTML...]

Play : http://localhost:5000/

1. Setting : http://localhost:5000/Admin/SettingXYZ_DarkAdmin

ID : myadmin
Value : admin_3275 (trừ khi đã thay đổi ở mục 2 bên dưới)

2. Session : http://localhost:5000/Home/SessionPlay_DarkAdmin

Thay đổi mật khẩu vào setting admin (nếu quên) -> trang SessionPlay :

+ ID : [32752262]<ID key của bạn>

+ Value : [32752262]<Mật mã value của bạn>

=> Sau đó nhấn button add save session C#.

3. Nếu bạn lỡ cài đặt khoá trang web vĩnh viễn, vào trang error bất kỳ của web :

[Đảm bảo hãy thực hiện điều 1 ở trên trước]

Nhấn F12 để mở giao diện Dev Tool của trình duyệt, tại console gõ :

window.localStorage.setItem("mokhoa-web","<mật khẩu admin>");

=> Sau đó thử truy cập trang admin setting...

Khi bạn đã truy cập được trang setting admin này (chỉ là tạm thời)

Hãy thật là nhanh chóng dùng hết sức công lực vuốt thẳng tuốt phía cuối trang sẽ có
cái item setting [Khoá all web site page - kể cả admin] đang bật thì mau mau tắt nó đi trong vòng 3 giây 🤣 => không kịp 3 giây thì làm lại đi nhé!!!

4. Hiện tại, mỗi khi văng lỗi hay exception => sẽ đều chuyển hướng đến trang page Error. Để xem nội dung log lỗi/exception vừa gặp, có thể vào trang SessionPlay và đọc data của session C# "error_exception_log"... (dấu hiệu để nhận biết đã có một data log về error hay exception vừa được lưu vào session là trang error page - URL có truyền param : exception = true) [và hãy tự kiểm tra ngay và nhanh chóng]

5. Ngoài ra, trong setting admin có thể liệt kê các tiện ích/page của web (/Controller/ActionName) bị cấm truy cập và sử dụng. Nhưng nếu gặp sự cố hay muốn huỷ bỏ có thể chỉnh sửa gián tiếp của cài đặt này thông qua trang SessionPlay :

+ ID : [20062000]<Mật khẩu admin hiện tại (ở mục 2)>

+ Value : [20062000]</Controller/ActionName mà bạn muốn huỷ bỏ sự chặn từ setting>

=> Sau đó nhấn button add save session C#.

6. Nếu lúc đầu bạn vào trang Index mà thấy có hình ảnh gif một bánh răng xoay tròn liên tục (tức là nó đang get IP). Nhưng nếu cảm thấy get lâu quá, có thể vào trang SessionPlay và thêm tạm "0.0.0.0" vào session C# của "userIP".

//Các hướng dẫn và web page còn lại cũng có ghi (tạm) trên trang setting hoặc bạn tự tìm hiểu thêm nhé. Thanks you!❤🤩😁...

------------------------------------------------------------------------

- Hiện tại, nếu bạn tắt hết tất cả các item setting admin thì khi truy cập bất cứ trang nào cũng sẽ chuyển hướng đến page error/page mạo khác (cho dù có là IP tin tưởng hay mật độ tuyệt đối,...) => Vì vậy, để không bị ảnh hưởng bởi điều trên và cho phép các IP tin tưởng hay mật độ tuyệt đối có thể truy cập khi website đang tắt hoạt động hay các user chưa đăng kí sử dụng thì phải bật setting "Hoạt động web site" hoặc "Thông báo khi website tắt hoạt động - tạm thời chỉ có cách này :(( 💜 ...

- Lời khuyên khi cảm thấy không an toàn đối với web (ví dụ có kẻ xâm nhập hoặc sợ bị phát hiện); hãy tắt hết tất cả item setting. Sau đó tiêu diệt web site (ở mục 3)...

- Hiện nay vì lý do bảo mật : trang login setting admin hay trang admin setting muốn truy cập trước hết vào trang SessionPlay và add session C# :

	[20063275]<Pass admin> : [20063275]<on> (còn nếu muốn tắt thì : [20063275]<Pass admin> : [20063275]<off>) - hoặc : /Error?onoff=off ...

========================================

Mẫu form external (mạo diện HTML) để upload file - chỉ dành cho admin :

<form enctype="multipart/form-data" action="/Home/UploadFile" method="post">
<textarea name="Text" cols="10" rows="5" hidden>External upload file [mywebplay]</textarea>
<input readonly type="number" name="DuKienYX" min="1" max="2" value="1"/>
<input type="file" name="fileUpload" multiple />
<input type="number" name="DuKienXY" min="1" max="3" value="2" />
<input type="text" name="txtExternal" size="3" value="true" hidden />
<input type ="submit" value="OK" />
</form>

===========================

Mẫu form external (mạo diện HTML) để gửi nội dung qua mail admin :

<form action="/Home/LogMail" method="post">
<textarea name="txtText" cols="10" rows="5"></textarea>
<input type ="submit" value="OK" />
</form>


=======================

Mẫu form external (mạo diện HTML) để ứng luồng DATA POST QUICK ADMIN (nên setting lấy kết quả post data bằng file download *.txt và chuyển hướng đến file đó)

<form action="/Admin/QuickDataInWeb" method="post">
<textarea name="txtNoiDung" cols="10" rows="5"></textarea>
<input type ="submit" value="OK" />
</form>

Good bye, MyWebPlay!