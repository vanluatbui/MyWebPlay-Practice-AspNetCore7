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

//Các hướng dẫn và web page còn lại cũng có ghi (tạm) trên trang setting hoặc bạn tự tìm hiểu thêm nhé. Thanks you!❤🤩😁...

------------------------------------------------------------------------


