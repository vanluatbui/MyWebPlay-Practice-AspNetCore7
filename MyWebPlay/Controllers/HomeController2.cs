using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public ActionResult SQL_InsertDoc()
        {
            ViewBag.VD = "SV001\tBùi Văn Luật\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
            return View();
        }

        int[] listType = new int[100];
        int nx = 0;


        [HttpPost]
        public ActionResult SQL_InsertDoc(IFormCollection f)
        {
            ViewBag.VD = "SV001\tBùi Văn Luật\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
            string table = f["Table"].ToString();
            string trangthai = f["TrangThai"].ToString();
            string dulieu = f["DuLieu"].ToString();

            if (string.IsNullOrEmpty(table))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.SQL_InsertDoc();
            }

            if (string.IsNullOrEmpty(trangthai))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.SQL_InsertDoc();
            }

            if (string.IsNullOrEmpty(dulieu))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.SQL_InsertDoc();
            }

            string[] ds_trangthai = trangthai.Split('-');
            for (int i = 0; i < ds_trangthai.Length; i++)
                listType[nx++] = int.Parse(ds_trangthai[i]);

            String sql = "\r\n\r\nset dateformat dmy\r\n\r\n";

            String[] s1 = Regex.Split(dulieu, "\r\n");

            for (int i = 0; i < s1.Length; i++)
            {
                String[] s2 = s1[i].Split('\t');

                String s = "INSERT INTO " + table + " VALUES (";

                for (int j = 0; j < s2.Length; j++)
                {

                    if (listType[j] == 1)
                        s += s2[j];
                    else
                        if (listType[j] == 2)
                        s += "N'" + s2[j] + "'";
                    else
                         if (listType[j] == 3)
                        s += "'" + s2[j] + "'";

                    if (j < s2.Length - 1)
                        s += ",";
                    else
                        s += ")";
                }

                sql += s + "\r\n";
            }

            TextCopy.ClipboardService.SetText(sql);

            // sql = sql.Replace("\r\n", "<br>");

            sql = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            ViewBag.Result = sql;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        //---------------------------------------------------

        [HttpGet]
        public ActionResult JSON_InsertDoc()
        {
            ViewBag.VD = "SV001\tBùi Văn Luật\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
            return View();
        }

        int[] listTypeX = new int[100];
        String[] nameType = new String[100];
        int nX = 0;
        int nY = 0;

        [HttpPost]
        public ActionResult JSON_InsertDoc(IFormCollection f)
        {
            ViewBag.VD = "SV001\tBùi Văn Luật\t10\t20/06/2000\r\nSV002\tNguyễn Văn Đạt\t9.5\t15/08/2001\r\nSV003\tTrần Chí Khôi\t2.5\t29/07/1990\r\nSV004\tLê Tuấn Kiệt\t9.2\t05/12/1995\r\nSV005\tĐào Vũ Hạnh\t4.8\t28/03/1992";
            string trangthai = f["TrangThai"].ToString();
            string dulieu = f["DuLieu"].ToString();

            if (string.IsNullOrEmpty(trangthai))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.JSON_InsertDoc();
            }

            if (string.IsNullOrEmpty(dulieu))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.JSON_InsertDoc();
            }

            string[] ds_trangthai = trangthai.Split(",");

            for (int i = 0; i< ds_trangthai.Length;i++)
            {
                string[] DS = ds_trangthai[i].Split("-");
                nameType[nX++] = DS[0];
                listTypeX[nY++] = int.Parse(DS[1]);
            }

            String sql = "\r\n\r\n[\r\n";

            String[] s1 = Regex.Split(dulieu, "\r\n");

            for (int i = 0; i < s1.Length; i++)
            {
                String[] s2 = s1[i].Split('\t');

                String s = "  {\r\n    ";

                for (int j = 0; j < s2.Length; j++)
                {

                    if (listTypeX[j] == 1)
                        s += "\"" + nameType[j] + "\" : " + s2[j];
                    else
                        if (listTypeX[j] == 2)
                        s += "\"" + nameType[j] + "\" : \"" + s2[j] + "\"";
                    else
                         if (listTypeX[j] == 3)
                        s += "\"" + nameType[j] + "\" : '" + s2[j] + "'";

                    if (j < s2.Length - 1)
                        s += ",\r\n    ";
                    else
                        s += "\r\n  }";
                }

                if (i < s1.Length - 1)
                    s += ",\r\n";
                else
                    s += "\r\n]";
                sql += s;
            }

            TextCopy.ClipboardService.SetText(sql);

            //sql = sql.Replace("\r\n", "<br>");

            sql = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + sql + "</textarea>";

            ViewBag.Result = sql;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View();
        }

        //--------------------------------------------

        [HttpGet]
        public ActionResult Copy_CreateDatabase()
        {
            TextCopy.ClipboardService.SetText("create database SinhVien\r\non\r\n  (name ='SinhVien _DATA', filename = 'C:\\SinhVien.MDF')\r\nlog on\r\n   (name ='SinhVien_LOG', filename = 'C:\\SinhVien.LDF')\r\n\r\nuse SinhVien");
            String sql = "\r\n\r\ncreate database SinhVien\r\non\r\n  (name ='SinhVien _DATA', filename = 'C:\\SinhVien.MDF')\r\nlog on\r\n   (name ='SinhVien_LOG', filename = 'C:\\SinhVien.LDF')\r\n\r\nuse SinhVien";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc","Home");
        }

        [HttpGet]
        public ActionResult Copy_BackupDatabase1()
        {
            TextCopy.ClipboardService.SetText("backup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'");

            String sql = "\r\n\r\nbackup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_BackupDatabase2()
        {
            TextCopy.ClipboardService.SetText("backup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'\r\nwith password = '12345'");
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            String sql = "\r\n\r\nbackup database SinhVien\r\nto disk = 'D:\\SinhVien.bak'\r\nwith password = '12345'";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase1()
        {
            TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'");
            String sql = "\r\n\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase2()
        {
            TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak', replace");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak', replace";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase3()
        {
            TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345'");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345'";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RestoreDatabase4()
        {
            TextCopy.ClipboardService.SetText("restore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345', replace");
            String sql = "\r\nrestore database SinhVien\r\nfrom disk = 'C:\\SinhVien.bak'\r\nwith password = '12345', replace";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_AttachDatabase()
        {
            TextCopy.ClipboardService.SetText("create database SinhVien\r\non\r\n  (filename='C:\\SinhVien.MDF')\r\nfor attach");
            String sql = "\r\ncreate database SinhVien\r\non\r\n  (filename='C:\\SinhVien.MDF')\r\nfor attach";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_DetachDatabase()
        {
            TextCopy.ClipboardService.SetText("sp_detach_db SinhVien");
            String sql = "\r\nsp_detach_db SinhVien";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Index()
        {
            TextCopy.ClipboardService.SetText("CREATE INDEX <Tên index> ON <Tên Table> (<Nhóm các cột> ASC|DESC)\r\n");
            String sql = "\r\nCREATE INDEX &lt;Tên index&gt; ON &lt;Tên Table&gt; (&lt;Nhóm các cột&gt; ASC|DESC)\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_View()
        {
            TextCopy.ClipboardService.SetText("CREATE VIEW <Tên View>\r\nAS\r\n\t<Câu lệnh Select>\r\n\r\n-- Thực thi View\r\nSELECT * FROM <Tên View đã tạo>\r\n");
            String sql = "\r\nCREATE VIEW &lt;Tên View&gt;\r\nAS\r\n\t&lt;Câu lệnh Select&gt;\r\n\r\n-- Thực thi View\r\nSELECT * FROM &lt;Tên View đã tạo&gt;\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Procedure()
        {
            TextCopy.ClipboardService.SetText("\r\nCREATE PROC <Tên Procedure> (@<Danh sách các tham số và kiểu dữ liệu> OUTPUT)\r\nAS\r\n<Câu lệnh truy vấn>\r\n\r\n-- Thực thi PROC\r\nEXECUTE <Tên Procedure> <Danh sách các giá trị của tham số>)");
            String sql = "\r\n\r\nCREATE PROC &lt;Tên Procedure&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt; OUTPUT)\r\nAS\r\n&lt;Câu lệnh truy vấn&gt;\r\n\r\n-- Thực thi PROC\r\nEXECUTE &lt;Tên Procedure&gt; &lt;Danh sách các giá trị của tham số&gt;)";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Function()
        {
            TextCopy.ClipboardService.SetText("-- Trả về giá trị\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS <Kiểu dữ liệu trả về>\r\nAS\r\n\r\n\tBEGIN\r\n\r\nDECLARE @<Danh sách các biến và kiểu dữ liệu>\r\nSET @<Tên biến> = <Giá Trị Gán>\r\nIF (<...>)\r\n(<...>)\r\nELSE\r\n<...>\r\nRETURN <Biến cần trả về>\r\n\r\n\tEND\r\n\r\n-- Trả về Table (không có điều kiện)\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS Table\r\nAS\r\nRETURN (<Câu lệnh Select>)\r\n\r\n-- Trả về Table (có điều kiện)\r\nCREATE FUNCTION <Tên Function> (@<Danh sách các tham số và kiểu dữ liệu>) RETURNS @<Tên biến bảng> Table (<Danh sách các cột cần xuất cùng kiểu dữ liệu>)\r\nAS\r\n\tBEGIN\r\n\r\nIF(<...>) INSERT INTO @<Tên biến bảng>\r\n<Câu lệnh Select - Chỉ Select với đúng tên và đúng số lượng cột đã khai báo ở trên>\r\nELSE\r\n<...Tương tự...>\r\n\r\n\tEND\r\n\r\n---- Thực thi Function\r\nSELECT DBO.<Tên Function>(@<Danh sách các giá trị của tham số)\r\n");
            String sql = "\r\n- Trả về giá trị\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS &lt;Kiểu dữ liệu trả về&gt;\r\nAS\r\n\r\n\tBEGIN\r\n\r\nDECLARE @&lt;Danh sách các biến và kiểu dữ liệu&gt;\r\nSET @&lt;Tên biến&gt; = &lt;Giá Trị Gán&gt;\r\nIF (&lt;...&gt;)\r\n(&lt;...&gt;)\r\nELSE\r\n&lt;...&gt;\r\nRETURN &lt;Biến cần trả về&gt;\r\n\r\n\tEND\r\n\r\n-- Trả về Table (không có điều kiện)\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS Table\r\nAS\r\nRETURN (&lt;Câu lệnh Select&gt;)\r\n\r\n-- Trả về Table (có điều kiện)\r\nCREATE FUNCTION &lt;Tên Function&gt; (@&lt;Danh sách các tham số và kiểu dữ liệu&gt;) RETURNS @&lt;Tên biến bảng&gt; Table (&lt;Danh sách các cột cần xuất cùng kiểu dữ liệu&gt;)\r\nAS\r\n\tBEGIN\r\n\r\nIF(&lt;...&gt;) INSERT INTO @&lt;Tên biến bảng&gt;\r\n&lt;Câu lệnh Select - Chỉ Select với đúng tên và đúng số lượng cột đã khai báo ở trên&gt;\r\nELSE\r\n&lt;...Tương tự...&gt;\r\n\r\n\tEND\r\n\r\n---- Thực thi Function\r\nSELECT DBO.&lt;Tên Function&gt;(@&lt;Danh sách các giá trị của tham số)\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_Trigger()
        {
            TextCopy.ClipboardService.SetText("CREATE TRIGGER <Tên Trigger> ON <Tên Table>\r\nFOR <INSERT | UPDATE | DELETE>\r\nAS\r\nIF UPDATE(<Tên Cột Của Bảng Nếu Muốn Sửa Sẽ Phải Gặp Trigger bên dưới- ?chỉ dành cho [for update]?>) -- Không thì có thể bỏ qua dòng này\r\nBEGIN\r\n\tIF (SELECT COUNT(*) FROM <INSERTED || DELETED || Table Khác> <..>) <...>\r\n\t--- (INSERTED : Các dữ liệu mới vừa Insert Into hay Dữ liệu mới vừa Set Cập Nhật Update)\r\n\t--- (DELETED : Các dữ liệu cũ trước khi Update Set Thành Giá Trị mới hoặc Giá Trị vừa mới bị Delete)\r\n\tBEGIN\r\n\tROLLBACK TRAN | <Hoặc công việc nào đó>\r\n\tEND\r\nEND\r\n");
            String sql = "\r\nCREATE TRIGGER &lt;Tên Trigger&gt; ON &lt;Tên Table&gt;\r\nFOR &lt;INSERT | UPDATE | DELETE&gt;\r\nAS\r\nIF UPDATE(&lt;Tên Cột Của Bảng Nếu Muốn Sửa Sẽ Phải Gặp Trigger bên dưới- ?chỉ dành cho [for update]?&gt;) -- Không thì có thể bỏ qua dòng này\r\nBEGIN\r\n\tIF (SELECT COUNT(*) FROM &lt;INSERTED || DELETED || Table Khác&gt; &lt;..&gt;) &lt;...&gt;\r\n\t--- (INSERTED : Các dữ liệu mới vừa Insert Into hay Dữ liệu mới vừa Set Cập Nhật Update)\r\n\t--- (DELETED : Các dữ liệu cũ trước khi Update Set Thành Giá Trị mới hoặc Giá Trị vừa mới bị Delete)\r\n\tBEGIN\r\n\tROLLBACK TRAN | &lt;Hoặc công việc nào đó&gt;\r\n\tEND\r\nEND\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_AddColumn()
        {
            TextCopy.ClipboardService.SetText("ALTER TABLE SinhVien ADD\r\nNgaySinh Date,\r\nDiemTB float,\r\nGioiTinh Bit");
            String sql = "\r\nALTER TABLE SinhVien ADD\r\nNgaySinh Date,\r\nDiemTB float,\r\nGioiTinh Bit";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_DeleteColumn()
        {
            TextCopy.ClipboardService.SetText("ALTER TABLE SinhVien DROP\r\nNgaySinh,\r\nDiemTB,\r\nGioiTinh");
            String sql = "\r\nALTER TABLE SinhVien DROP\r\nNgaySinh,\r\nDiemTB,\r\nGioiTinh";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn1()
        {
            TextCopy.ClipboardService.SetText("ALTER TABLE <TênTable> ALTER COLUMN <TênCột> <Kiểu dữ liệu mới>");
            String sql = "\r\nALTER TABLE &lt;TênTable&gt; ALTER COLUMN &lt;TênCột&gt; &lt;Kiểu dữ liệu mới&gt;";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn2()
        {
            TextCopy.ClipboardService.SetText("-- Thay đổi ràng buộc cho cột là không được phép NULL\r\nALTER TABLE <TênTable> ALTER COLUMN <TênCột> <KiểuDữLiệu> NOT NULL\r\n-- ...P/s : Nếu nhiều cột cần làm khoá chính thì hãy tương tự cho các cột khác\r\n\r\n-- Cập nhật các cột làm khoá chính (phải chạy dòng trên trước)\r\nALTER TABLE <TênTable> ADD CONSTRAINT <TênConstraint> PRIMARY KEY (<Nhóm các cột cần làm khoá chính>)\r\n");
            String sql = "\r\n-- Thay đổi ràng buộc cho cột là không được phép NULL\r\nALTER TABLE &lt;TênTable&gt; ALTER COLUMN &lt;TênCột&gt; &lt;KiểuDữLiệu&gt; NOT NULL\r\n-- ...P/s : Nếu nhiều cột cần làm khoá chính thì hãy tương tự cho các cột khác\r\n\r\n-- Cập nhật các cột làm khoá chính (phải chạy dòng trên trước)\r\nALTER TABLE &lt;TênTable&gt; ADD CONSTRAINT &lt;TênConstraint&gt; PRIMARY KEY (&lt;Nhóm các cột cần làm khoá chính&gt;)\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }

        [HttpGet]
        public ActionResult Copy_RepairColumn3()
        {
            TextCopy.ClipboardService.SetText("ALTER TABLE <Tên Table> ADD CONSTRAINT <Tên Ràng buộc>\r\nFOREIGN KEY (<Cột cần làm khoá ngoại>) REFERENCES <Table Cha> (<Cột của bảng cha cần nối kết khoá ngoại>)\r\n");
            String sql = "\r\nALTER TABLE &lt;Tên Table&gt; ADD CONSTRAINT &lt;Tên Ràng buộc&gt;\r\nFOREIGN KEY (&lt;Cột cần làm khoá ngoại&gt;) REFERENCES &lt;Table Cha&gt; (&lt;Cột của bảng cha cần nối kết khoá ngoại&gt;)\r\n";
            sql = sql.Replace("\r\n", "<br>");

            sql = "<p style=\"color:blue\"" + sql + "</p>";

            ViewBag.Result = sql;
            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";
            return View("SQL_InsertDoc", "Home");
        }
    }
}
