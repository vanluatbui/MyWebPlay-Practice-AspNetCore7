using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MyWebPlay.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult SpecialX_OrderBy()
        {
            ViewBag.ChuoiViDu = "1\tBùi Nguyễn Tú Dương\t01/01/2000\r\n2\tNguyễn Thị Xuân Hoài Anh\t02/01/2000\r\n3\tLê Trần Anh Huy\t03/01/2000\r\n4\tBùi Văn Luật\t04/01/2000\r\n5\tLê Đức Anh\t05/01/2000\r\n6\tLê Anh Huy\t06/01/2000\r\n7\tLê Gia Khiêm\t07/01/2000\r\n8\tPhạm Lưu Văn Sang\t08/01/2000\r\n9\tLê Thảo Lâm\t09/01/2000\r\n10\tLý Ngọc Thảo\t10/01/2000\r\n11\tTrần Tú Anh\t11/01/2000\r\n12\tNguyễn Tấn Lộc\t12/01/2000\r\n13\tLê Ngọc Thảo\t13/01/2000\r\n14\tLê Minh Tuấn Anh\t14/01/2000\r\n15\tLê Xuân Thảo\t15/01/2000\r\n16\tPhạm Ngọc Hoài Anh\t16/01/2000\r\n17\tNguyễn Đại Lộc\t17/01/2000\r\n18\tTrần Đức Lộc\t18/01/2000\r\n19\tPhạm Đức Anh\t19/01/2000\r\n20\tPhạm Hoàng Lan\t20/01/2000\r\n21\tTrần Ngọc Khánh Xuân\t21/01/2000\r\n22\tĐỗ Tấn Huệ\t22/01/2000\r\n23\tKim Đức Anh\t23/01/2000\r\n24\tPhạm Hoàng Lan Anh\t24/01/2000\r\n25\tPhạm Thị Mỹ Linh\t25/01/2000\r\n26\tTrần Ngọc Lê Hoài Anh\t26/01/2000\r\n27\tTrần Ngọc Hoài Anh\t27/01/2000\r\n28\tPhạm Ngọc Hải Yến\t28/01/2000\r\n29\tTrần Ngọc Duy Anh\t29/01/2000\r\n30\tLê Tú Anh\t30/01/2000\r\n31\tTrần Xuân Anh\t31/01/2000\r\n32\tNguyễn Phạm Lê Hoài Anh\t01/02/2000\r\n33\tDương Thảo Lâm\t02/02/2000\r\n34\tPhạm Bình Hoài Anh\t03/02/2000\r\n35\tBùi Ngọc Tú\t04/02/2000\r\n36\tLê Anh Hào\t05/02/2000\r\n37\tPhạm Xuân Anh\t06/02/2000\r\n38\tTrần Phương Thảo\t07/02/2000";
            
            return View();
        }

        [HttpPost]
        public ActionResult SpecialX_OrderBy(int index, IFormCollection f)
        {
            ViewBag.ChuoiViDu = "1\tBùi Nguyễn Tú Dương\t01/01/2000\r\n2\tNguyễn Thị Xuân Hoài Anh\t02/01/2000\r\n3\tLê Trần Anh Huy\t03/01/2000\r\n4\tBùi Văn Luật\t04/01/2000\r\n5\tLê Đức Anh\t05/01/2000\r\n6\tLê Anh Huy\t06/01/2000\r\n7\tLê Gia Khiêm\t07/01/2000\r\n8\tPhạm Lưu Văn Sang\t08/01/2000\r\n9\tLê Thảo Lâm\t09/01/2000\r\n10\tLý Ngọc Thảo\t10/01/2000\r\n11\tTrần Tú Anh\t11/01/2000\r\n12\tNguyễn Tấn Lộc\t12/01/2000\r\n13\tLê Ngọc Thảo\t13/01/2000\r\n14\tLê Minh Tuấn Anh\t14/01/2000\r\n15\tLê Xuân Thảo\t15/01/2000\r\n16\tPhạm Ngọc Hoài Anh\t16/01/2000\r\n17\tNguyễn Đại Lộc\t17/01/2000\r\n18\tTrần Đức Lộc\t18/01/2000\r\n19\tPhạm Đức Anh\t19/01/2000\r\n20\tPhạm Hoàng Lan\t20/01/2000\r\n21\tTrần Ngọc Khánh Xuân\t21/01/2000\r\n22\tĐỗ Tấn Huệ\t22/01/2000\r\n23\tKim Đức Anh\t23/01/2000\r\n24\tPhạm Hoàng Lan Anh\t24/01/2000\r\n25\tPhạm Thị Mỹ Linh\t25/01/2000\r\n26\tTrần Ngọc Lê Hoài Anh\t26/01/2000\r\n27\tTrần Ngọc Hoài Anh\t27/01/2000\r\n28\tPhạm Ngọc Hải Yến\t28/01/2000\r\n29\tTrần Ngọc Duy Anh\t29/01/2000\r\n30\tLê Tú Anh\t30/01/2000\r\n31\tTrần Xuân Anh\t31/01/2000\r\n32\tNguyễn Phạm Lê Hoài Anh\t01/02/2000\r\n33\tDương Thảo Lâm\t02/02/2000\r\n34\tPhạm Bình Hoài Anh\t03/02/2000\r\n35\tBùi Ngọc Tú\t04/02/2000\r\n36\tLê Anh Hào\t05/02/2000\r\n37\tPhạm Xuân Anh\t06/02/2000\r\n38\tTrần Phương Thảo\t07/02/2000";

            string chuoi = f["Chuoi"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi1"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }

            string sapxep = f["SapXep"].ToString();
            if (string.IsNullOrEmpty(chuoi))
            {
                ViewData["Loi2"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }

            string tanggiam = f["TangGiam"].ToString();
            if (string.IsNullOrEmpty(tanggiam))
            {
                ViewData["Loi3"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }

            string codinh = f["CoDinh"].ToString();
            if (string.IsNullOrEmpty(codinh))
            {
                ViewData["Loi4"] = "Trường này không được để trống!";
                return this.Special_OrderBy();
            }

            string[] DS = Regex.Split(chuoi, "\r\n");

            string[] so = sapxep.Split('=');

            while (true)
            {
                int l = 0;

                string kq1 = String.Join("\n", DS);
                while (l < so.Length)
                {
                    for (int i = 0; i < DS.Length - 1; i++)
                    {
                        string[] phan1 = DS[i].Split('\t')[int.Parse(codinh)-1].Split(' ');
                        int x1 = 0;

                        if (so[l].Contains("N-") == true)
                        {
                            x1 = phan1.Length - int.Parse(so[l].Split('-')[1]);
                        }
                        else
                        {
                            x1 = int.Parse(so[l]);
                        }

                        string ss1 = "";
                        for (int j = 0; j < l; j++)
                        {
                            int soX = 0;
                            if (so[j].Contains("N-") == true)
                            {
                                soX = phan1.Length - int.Parse(so[j].Split('-')[1]);
                            }
                            else
                            {
                                soX = int.Parse(so[j]);
                            }
                            ss1 += phan1[soX];
                        }

                        for (int jk = i + 1; jk < DS.Length; jk++)
                        {
                            string[] phan2 = DS[jk].Split('\t')[int.Parse(codinh)-1].Split(' ');
                            int x2 = 0;

                            if (so[l].Contains("N-") == true)
                            {
                                x2 = phan2.Length - int.Parse(so[l].Split('-')[1]);
                            }
                            else
                            {
                                x2 = int.Parse(so[l]);
                            }

                            string ss2 = "";
                            for (int jj = 0; jj < l; jj++)
                            {
                                int soX = 0;
                                if (so[jj].Contains("N-") == true)
                                {
                                    soX = phan2.Length - int.Parse(so[jj].Split('-')[1]);
                                }
                                else
                                {
                                    soX = int.Parse(so[jj]);
                                }
                                ss2 += phan2[soX];
                            }

                            if (int.Parse(tanggiam) == 0)
                            {
                                if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) > 0)
                                {
                                    string t = DS[i];
                                    DS[i] = DS[jk];
                                    DS[jk] = t;
                                }
                            }
                            else
                            {
                                if (String.Compare(ss1, ss2) == 0 && String.Compare(phan1[x1], phan2[x2]) < 0)
                                {
                                    string t = DS[i];
                                    DS[i] = DS[jk];
                                    DS[jk] = t;
                                }
                            }
                        }
                    }
                    l++;
                }
                string kq2 = String.Join("\n", DS);
                if (String.Compare(kq1, kq2) == 0)
                    break;
            }

            string dx = String.Join("\n", DS);
            TextCopy.ClipboardService.SetText(dx);

            dx = "<textarea style=\"color:blue\" rows=\"50\" cols=\"150\" readonly=\"true\" autofocus>" + dx + "</textarea>";

            ViewBag.Result = dx;

            ViewBag.KetQua = "Thành công! Một kết quả đã được hiển thị ở cuối trang này!";

            return View();
        }
    }
}
