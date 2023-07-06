using Microsoft.AspNetCore.Mvc;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult FileValueCheckInSQL()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileValueCheckInSQL(IFormCollection f)
        {
            string txtFields = f["txtFields"].ToString();

            while (txtFields.Contains("  "))
            {
                txtFields = txtFields.Replace("  ", " ");
            }

            txtFields = txtFields.TrimStart(" ".ToCharArray()).TrimEnd(" ".ToCharArray());

            while (txtFields.Contains("\t\t"))
            {
                txtFields = txtFields.Replace("\t\t", "\t");
            }

            txtFields = txtFields.TrimStart("\t".ToCharArray()).TrimEnd("\t".ToCharArray());

            while (txtFields.Contains("\r\n\r\n"))
            {
                txtFields = txtFields.Replace("\r\n\r\n", "\r\n");
            }

            txtFields = txtFields.TrimStart("\r\n".ToCharArray()).TrimEnd("\r\n".ToCharArray());

            txtFields = txtFields.Replace(",", "");

            string txtTable = f["txtTable"].ToString();

            string txtCheck = f["txtCheck"].ToString();

            int txtLoai = int.Parse(f["txtLoai"].ToString());

            string[] listFields = txtFields.Split("\r\n");

            string result = "";

            for (int i =0; i< listFields.Length; i++)
            {
                listFields[i] = listFields[i].ToLower();

                    string fields = "";

                string value = "";

                if (txtLoai == 2)
                    fields = "CONVERT(nvarchar(10), " + listFields[i] +", 103)";
                else
                    fields = listFields[i];

                if (txtLoai == 1)
                    value = " = N'" + txtCheck + "'";
                if (txtLoai == 2)
                    value = " = '" + txtCheck + "'";
                else if (txtLoai == 3)
                    value = " LIKE N'" + txtCheck + "%'";
                else if (txtLoai == 4)
                    value = " LIKE N'%" + txtCheck + "%'";
                else if (txtLoai == 5)
                    value = " LIKE N'%" + txtCheck + "'";
                else if (txtLoai == 6)
                    value = " = " + txtCheck;

                result += "SELECT TOP 1 " + listFields[i] + " FROM " + txtTable + " WHERE " + fields + value + "\n\n";
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
