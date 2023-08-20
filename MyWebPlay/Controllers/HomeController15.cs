using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Collections;
using System.Data;
using System.Numerics;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult FindValueCheckInSQL()
        {
            ViewBag.ViDu = "user_name\tvarchar(10)\r\nuser_birth\tdatetime\r\nuser_age\tint";
            return View();
        }

        [HttpPost]
        public ActionResult FindValueCheckInSQL(IFormCollection f)
        {
            ViewBag.ViDu = "user_name\tvarchar(10)\r\nuser_birth\tdatetime\r\nuser_age\tint";
            string txtFields = f["txtFields"].ToString();
            txtFields = txtFields.Replace("[TAB-TPLAY]", "\t");
            txtFields = txtFields.Replace("[ENTER-NPLAY]", "\n");
            txtFields = txtFields.Replace("[ENTER-RPLAY]", "\r");

            string txtTable = f["txtTable"].ToString();
            txtTable = txtTable.Replace("[TAB-TPLAY]", "\t");
            txtTable = txtTable.Replace("[ENTER-NPLAY]", "\n");
            txtTable = txtTable.Replace("[ENTER-RPLAY]", "\r");

            string txtCheck = f["txtCheck"].ToString();
            txtCheck = txtCheck.Replace("[TAB-TPLAY]", "\t");
            txtCheck = txtCheck.Replace("[ENTER-NPLAY]", "\n");
            txtCheck = txtCheck.Replace("[ENTER-RPLAY]", "\r");

            int txtLoai = int.Parse(f["txtLoai"].ToString());

            int txtLoas = int.Parse(f["txtLoas"].ToString());

            string[] listFields = txtFields.Split("\r\n");

            string result = "PRINT N'* DANH SÁCH CÁC FIELDS CÓ KẾT QUẢ TÌM THẤY - " + txtTable + " :'+CHAR(10)\n\n";

            for (int i =0; i< listFields.Length; i++)
            {
                string[] fix = listFields[i].Split("\t");

 
                    fix[0] = fix[0].ToLower();
                    fix[1] = fix[1].ToLower();

                    string fields = "";

                    string value = "";

                    if (txtLoai == 3 || txtLoai == 5)
                        fields = "CONVERT(nvarchar(10), " + fix[0] + ", 103)";
                    else
                        fields = fix[0];

                    if (txtLoai == 1 || txtLoai == 4 || txtLoai == 5)
                    {
                        if (txtLoas == 1)
                            value = " = '" + txtCheck + "'";
                        else if (txtLoas == 2)
                            value = " LIKE '" + txtCheck + "%'";
                        else if (txtLoas == 3)
                            value = " LIKE '%" + txtCheck + "%'";
                        else if (txtLoas == 4)
                            value = " LIKE '%" + txtCheck + "'";
                    }
                   else if (txtLoai == 2)
                        value = " = " + txtCheck + "";
                    else if (txtLoai == 3)
                        value = " = '" + txtCheck + "'";
                    else if (txtLoai == 6)
                    {
                        if (txtLoas == 1)
                            value = " = N'" + txtCheck + "'";
                        else if (txtLoas == 2)
                            value = " LIKE N'" + txtCheck + "%'";
                        else if (txtLoas == 3)
                            value = " LIKE N'%" + txtCheck + "%'";
                        else if (txtLoas == 4)
                            value = " LIKE N'%" + txtCheck + "'";
                    }

                    if ((txtLoai == 1 || txtLoai == 6) && (fix[1].Contains("char") == false && fix[1].Contains("text") == false && fix[1].Contains("binary") == false && fix[1].Contains("image") == false))
                        continue;

                    if ((txtLoai == 2) && (fix[1].Contains("memo") == false && fix[1].Contains("single") == false && fix[1].Contains("currency") == false && fix[1].Contains("money") == false && fix[1].Contains("double") == false && fix[1].Contains("long") == false && fix[1].Contains("byte") == false && fix[1].Contains("bit") == false && fix[1].Contains("int") == false && fix[1].Contains("decimal") == false && fix[1].Contains("numeric") == false && fix[1].Contains("money") == false && fix[1].Contains("float") == false && fix[1].Contains("real") == false))
                        continue;

                    if ((txtLoai == 3) && (fix[1].Contains("date") == false))
                        continue;

                    if ((txtLoai == 4) && (fix[1].Contains("memo") == false && fix[1].Contains("single") == false && fix[1].Contains("currency") == false && fix[1].Contains("money") == false && fix[1].Contains("double") == false && fix[1].Contains("long") == false && fix[1].Contains("byte") == false && fix[1].Contains("bit") == false && fix[1].Contains("int") == false && fix[1].Contains("decimal") == false && fix[1].Contains("numeric") == false && fix[1].Contains("money") == false && fix[1].Contains("float") == false && fix[1].Contains("real") == false
              && fix[1].Contains("identifier") == false && fix[1].Contains("var") == false && fix[1].Contains("char") == false && fix[1].Contains("text") == false && fix[1].Contains("binary") == false && fix[1].Contains("image") == false))
                        continue;

                    if ((txtLoai == 5) && (fix[1].Contains("date") == false
                        && fix[1].Contains("identifier") == false && fix[1].Contains("var") == false && fix[1].Contains("char") == false && fix[1].Contains("text") == false && fix[1].Contains("binary") == false && fix[1].Contains("image") == false))
                        continue;

                result += "IF ((SELECT COUNT(*) " + fix[0] + " FROM " + txtTable + " WHERE " + fields + value + ") >0)\nBEGIN\n\tPRINT '" + fix[0] + "'\nEND\n\n";
            }

            TextCopy.ClipboardService.SetText(result);

            // s = "<p style=\"color:blue\"" + s + "</p>";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult FindCompareValueInSQL()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindCompareValueInSQL(IFormCollection f)
        {
            string txtFields = f["txtFields"].ToString().ToLower();
            txtFields = txtFields.Replace("[TAB-TPLAY]", "\t");
            txtFields = txtFields.Replace("[ENTER-NPLAY]", "\n");
            txtFields = txtFields.Replace("[ENTER-RPLAY]", "\r");

            txtFields = txtFields.Replace(" ", "");
            txtFields = txtFields.Replace("\t", "");
            txtFields = txtFields.Replace(",", "");
            txtFields = txtFields.Replace("[", "");
            txtFields = txtFields.Replace("]", "");

            var listFields = txtFields.Split("\r\n");

            var listOld = f["txtOld"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n");

            var listNew = f["txtNew"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n");

            int dem = 1;
            var listChangeOld = new List<string>();
            var listChangeNew = new List<string>();

            var listWhere = f["txtWhere"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").ToLower().Split(",");

            int[] whereX = new int[listWhere.Length];

            var result = "* Các vị trí fields đang phát hiện sự thay đổi về giá trị :\n\n";

            for (int i = 0; i < listWhere.Length; i++)
            {
                for (int j = 0; j < listFields.Length; j++)
                {
                    if (listWhere[i] == listFields[j])
                    {
                        whereX[i] = j;
                        break;
                    }
                }
            }

            for (int i =0; i< listOld.Length; i++)
            {
                var oldX = listOld[i].Split("\t");
                var newX = listNew[i].Split("\t");
                int flag = 0;
                for (int j =0; j < oldX.Length; j++)
                {
                    if (oldX[j] != newX[j])
                    {
                        if (flag == 0)
                        {
                            result += dem + ". Đặc điểm nhận dạng :\n\n";
                            for (int u = 0; u < listWhere.Length; u++)
                            {
                                 result += "+ " + listWhere[u] + " : " + newX[whereX[u]] + "\n";
                            }
                            result += "\n";
                        }
                        flag = 1;
                        result += "- Tên field : " + listFields[j] + "\n";
                        result += "- Giá trị cũ : " + oldX[j] + "\n";
                        result += "- Giá trị mới : " + newX[j] + "\n\n";
                    }
                }
                if (flag == 1)
                {
                    result += "\n";
                    dem++;
                }
            }

            if (dem == 1)
                result += "\n=> Không phát hiện các field trong dữ liệu của bạn có sự thay đổi giá trị!";

            //TextCopy.ClipboardService.SetText(result);

            // s = "<p style=\"color:blue\"" + s + "</p>";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult XuLySQL1()
        {
            ViewBag.Table = "User\r\nProduct\r\nOrder";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL1(IFormCollection f)
        {
            string txtTable = f["txtTables"].ToString();
            txtTable = txtTable.Replace("[TAB-TPLAY]", "\t");
            txtTable = txtTable.Replace("[ENTER-NPLAY]", "\n");
            txtTable = txtTable.Replace("[ENTER-RPLAY]", "\r");
            ViewBag.Table = txtTable;
            var listTable = txtTable.Split("\r\n");

            var result = "CREATE PROC PROC_MYWEBPLAY\nAS\nBEGIN";

            for (int i =0; i < listTable.Length;i++)
            {
                result += "\n\tDECLARE @" + listTable[i]+" INT = (SELECT COUNT(*) FROM " + listTable[i]+")\n\tPRINT @" + listTable[i];
            }

            result += "\nEND\n\n";

            result += "--------------------SỬ DỤNG XONG HÃY NHỚ DROP PROC PROC_MYWEBPLAY---------------------\n\n\n";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult XuLySQL2()
        {
            ViewBag.Table = "User\r\nProduct\r\nOrder";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL2(IFormCollection f)
        {
            string chon = f["txtChon"].ToString();
            string txtTable = f["txtTable"].ToString();
            chon = chon.Replace("[TAB-TPLAY]", "\t");
            chon = chon.Replace("[ENTER-NPLAY]", "\n");
            chon = chon.Replace("[ENTER-RPLAY]", "\r");

            txtTable = txtTable.Replace("[TAB-TPLAY]", "\t");
            txtTable = txtTable.Replace("[ENTER-NPLAY]", "\n");
            txtTable = txtTable.Replace("[ENTER-RPLAY]", "\r");
            ViewBag.Table = txtTable;

            var listTable = txtTable.Split("\r\n");
            var result = "";
            if (chon != "on")
            {
                result += "CREATE TABLE TABLE_MYWEBPLAY\n(\n\tTableName nvarchar(100),\n\tXuLy nvarchar(100),\n\tNgayCapNhat DateTime\n)\n\n";
                result += "-------------------------------------------------------------------------------------\n\n\n";

                // INSERT
                for (int i =0; i< listTable.Length; i++)
                {
                    result += "CREATE TRIGGER TRIGGER_INSERT_MYWEBPLAY_" + listTable[i].ToUpper() + " ON " + listTable[i] + "\nFOR INSERT\nAS\nBEGIN\n\tINSERT INTO TABLE_MYWEBPLAY VALUES ('" + listTable[i] + "', 'Insert', GETDATE())\nEND\n\n";
                }

                // UPDATE
                for (int i = 0; i < listTable.Length; i++)
                {
                    result += "CREATE TRIGGER TRIGGER_UPDATE_MYWEBPLAY_" + listTable[i].ToUpper() + " ON " + listTable[i] + "\nFOR UPDATE\nAS\nBEGIN\n\tINSERT INTO TABLE_MYWEBPLAY VALUES ('" + listTable[i] + "', 'Update', GETDATE())\nEND\n\n";
                }

                // DELETE
                for (int i = 0; i < listTable.Length; i++)
                {
                    result += "CREATE TRIGGER TRIGGER_DELETE_MYWEBPLAY_" + listTable[i].ToUpper() + " ON " + listTable[i] + "\nFOR DELETE\nAS\nBEGIN\n\tINSERT INTO TABLE_MYWEBPLAY VALUES ('" + listTable[i] + "', 'Delete', GETDATE())\nEND\n\n";
                }
            }
            else
            {
                result += "DROP TABLE TABLE_MYWEBPLAY\n\n";
                result += "-------------------------------------------------------------------------------------\n\n\n";

                // INSERT
                for (int i = 0; i < listTable.Length; i++)
                {
                    result += "DROP TRIGGER TRIGGER_INSERT_MYWEBPLAY_" + listTable[i].ToUpper()+"\n";
                }

                // UPDATE
                for (int i = 0; i < listTable.Length; i++)
                {
                    result += "DROP TRIGGER TRIGGER_UPDATE_MYWEBPLAY_" + listTable[i].ToUpper() + "\n";
                }

                // DELETE
                for (int i = 0; i < listTable.Length; i++)
                {
                    result += "DROP TRIGGER TRIGGER_DELETE_MYWEBPLAY_" + listTable[i].ToUpper() + "\n";
                }
            }

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult XuLySQL3()
        {
            ViewBag.Chuoi = "Coumn1\r\nCoumn2\r\nCoumn3\r\n#3275#\r\nEm là hoa\r\nhồng\r\nnhỏ[TAB-TPLAY]12345[TAB-TPLAY]Em là búp\r\nmăng non\r\nhồng hào trắng\r\nsáng\r\n#3275#\r\nTôi là bông\r\nhồng\r\ngià[TAB-TPLAY]06789[TAB-TPLAY]Em là búp\r\nmăng già\r\nnếp nhăn\r\ngoá phụ";
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL3(IFormCollection f)
        {
            string chuoi = f["txtChuoi"].ToString();
            chuoi = chuoi.Replace("[TAB-TPLAY]", "\t");
            chuoi = chuoi.Replace("[ENTER-NPLAY]", "\n");
            chuoi = chuoi.Replace("[ENTER-RPLAY]", "\r");
            ViewBag.Chuoi = chuoi;
            var listCha = chuoi.Split("\r\n#3275#\r\n");
            var phan1 = listCha[0].Replace(" ", "").Replace("\t", "").Replace(",", "").Replace("[", "").Replace("]", "").Replace("\r\n","\t");
            var phan2 = listCha[1].Replace("\r\n", "  ");
            var phan3 = listCha[2].Replace("\r\n", "  ");

            var result = phan1 + "\n" + phan2 + "\n"+ phan3;

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }

        public ActionResult XuLySQL4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XuLySQL4(IFormCollection f)
        {
            var txtTable = f["txtTable"].ToString();
            var thieus = f["txtThieu"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n");
            var daydus = f["txtDayDu"].ToString().Replace("[TAB-TPLAY]", "\t").Replace("[ENTER-NPLAY]", "\n").Replace("[ENTER-RPLAY]", "\r").Split("\r\n");
            txtTable = txtTable.Replace("[TAB-TPLAY]", "\t");
            txtTable = txtTable.Replace("[ENTER-NPLAY]", "\n");
            txtTable = txtTable.Replace("[ENTER-RPLAY]", "\r");


            var has = new Hashtable();
            for (int i =0; i < daydus.Length;i++)
            {
                var duday = Regex.Split(daydus[i], "\t| ");
                has[duday[0]] = duday[1];
            }

            var result = "ALTER TABLE " + txtTable + " ADD";
            for (int i=0;i< thieus.Length;i++)
            {
                if (has.ContainsKey(thieus[i]) == true)
                    result += "\n\t"+thieus[i] + " " + has[thieus[i]]+",\n";
                else
                    result += "\n\t" + thieus[i] + " nvarchar(max),\n";
            }

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }
    }
}

