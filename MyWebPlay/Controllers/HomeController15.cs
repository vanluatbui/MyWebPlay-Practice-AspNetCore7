using Microsoft.AspNetCore.Mvc;

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

            string txtTable = f["txtTable"].ToString();

            string txtCheck = f["txtCheck"].ToString();

            int txtLoai = int.Parse(f["txtLoai"].ToString());

            int txtLoas = int.Parse(f["txtLoas"].ToString());

            string[] listFields = txtFields.Split("\r\n");

            string result = "";

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

                    result += "SELECT TOP 1 " + fix[0] + " FROM " + txtTable + " WHERE " + fields + value + "\n\n";
            }

            TextCopy.ClipboardService.SetText(result);

            // s = "<p style=\"color:blue\"" + s + "</p>";

            result = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + result + "</textarea>";

            ViewBag.Result = result;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }
    }
}
