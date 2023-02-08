using System.Text.RegularExpressions;

namespace MyWebPlay.Models
{
    public static class ReadNumber
    {
        private static string docsohangtramty(long n)
        {
            switch (n / 100000000000)
            {
                case 1:
                    return "Một Trăm ";

                case 2:
                    return "Hai Trăm ";

                case 3:
                    return "Ba Trăm ";

                case 4:
                    return "Bốn Trăm ";

                case 5:
                    return "Năm Trăm ";

                case 6:
                    return "Sáu Trăm ";

                case 7:
                    return "Bảy Trăm ";

                case 8:
                    return "Tám Trăm ";

                case 9:
                    return "Chín Trăm ";
            }
            return "";
        }

        private static string docsohangchucty(long n)
        {
            switch ((n / 10000000000) % 10)
            {
                case 0:
                    if (((n / 1000000000) % 10 != 0))
                        return "Linh ";
                    return "";
                case 1:
                    return "Mười ";

                case 2:
                    return "Hai Mươi ";

                case 3:
                    return "Ba Mươi ";

                case 4:
                    return "Bốn Mươi ";

                case 5:
                    return "Năm Mươi ";

                case 6:
                    return "Sáu Mươi ";

                case 7:
                    return "Bảy Mươi ";

                case 8:
                    return "Tám Mươi ";

                case 9:
                    return "Chín Mươi ";
            }
            return "";
        }

        private static string docsohangdonvity(long n)
        {
            switch ((n / 1000000000) % 10)
            {

                case 1:
                    return "Một ";

                case 2:
                    return "Hai ";

                case 3:
                    return "Ba ";

                case 4:
                    return "Bốn ";

                case 5:
                    if ((n / 10000000000) % 10 == 0)
                        return "Năm ";
                    return "Lăm ";

                case 6:
                    return "Sáu ";

                case 7:
                    return "Bảy ";

                case 8:
                    return "Tám ";

                case 9:
                    return "Chín ";
            }
            return "";
        }

        private static string docsohangtramtrieu(long n)
        {
            switch ((n / 100000000) % 10)
            {
                case 0:
                    if (((n / 10000000) % 10 != 0 || (n / 1000000) % 10 != 0) && n > 100000000)
                        return "Không Trăm ";
                    return "";

                case 1:

                    return "Một Trăm ";

                case 2:
                    return "Hai Trăm ";

                case 3:
                    return "Ba Trăm ";

                case 4:
                    return "Bốn Trăm ";

                case 5:
                    return "Năm Trăm ";

                case 6:
                    return "Sáu Trăm ";

                case 7:
                    return "Bảy Trăm ";

                case 8:
                    return "Tám Trăm ";

                case 9:
                    return "Chín Trăm ";
            }
            return "";
        }

        private static string docsohangchuctrieu(long n)
        {
            switch ((n / 10000000) % 10)
            {
                case 0:
                    if ((n / 1000000) % 10 != 0)
                        return "Linh ";
                    return "";
                case 1:
                    return "Mười ";

                case 2:
                    return "Hai Mươi ";

                case 3:
                    return "Ba Mươi ";

                case 4:
                    return "Bốn Mươi ";

                case 5:
                    return "Năm Mươi ";

                case 6:
                    return "Sáu Mươi ";

                case 7:
                    return "Bảy Mươi ";

                case 8:
                    return "Tám Mươi ";

                case 9:
                    return "Chín Mươi ";
            }
            return "";
        }

        private static string docsohangdonvitrieu(long n)
        {
            switch ((n / 1000000) % 10)
            {

                case 1:
                    return "Một ";

                case 2:
                    return "Hai ";

                case 3:
                    return "Ba ";

                case 4:
                    return "Bốn ";

                case 5:
                    if ((n / 10000000) % 10 == 0)
                        return "Năm ";
                    return "Lăm ";

                case 6:
                    return "Sáu ";

                case 7:
                    return "Bảy ";

                case 8:
                    return "Tám ";

                case 9:
                    return "Chín ";
            }
            return "";
        }

        private static string docsohangtramnghin(long n)
        {
            switch ((n / 100000) % 10)
            {
                case 0:
                    if (((n / 10000) % 10 != 0 || (n / 1000) % 10 != 0) && n > 100000)
                        return "Không Trăm ";
                    return "";

                case 1:
                    return "Một Trăm ";

                case 2:
                    return "Hai Trăm ";

                case 3:
                    return "Ba Trăm ";

                case 4:
                    return "Bốn Trăm ";

                case 5:
                    return "Năm Trăm ";

                case 6:
                    return "Sáu Trăm ";

                case 7:
                    return "Bảy Trăm ";

                case 8:
                    return "Tám Trăm ";

                case 9:
                    return "Chín Trăm ";
            }
            return "";
        }

        private static string docsohangchucnghin(long n)
        {
            switch ((n / 10000) % 10)
            {
                case 0:
                    if ((n / 1000) % 10 != 0)
                        return "Linh ";
                    return "";
                case 1:
                    return "Mười ";

                case 2:
                    return "Hai Mươi ";

                case 3:
                    return "Ba Mươi ";

                case 4:
                    return "Bốn Mươi ";

                case 5:
                    return "Năm Mươi ";

                case 6:
                    return "Sáu Mươi ";

                case 7:
                    return "Bảy Mươi ";

                case 8:
                    return "Tám Mươi ";

                case 9:
                    return "Chín Mươi ";
            }
            return "";
        }

        private static string docsohangdonvinghin(long n)
        {
            switch ((n / 1000) % 10)
            {

                case 1:
                    return "Một ";

                case 2:
                    return "Hai ";

                case 3:
                    return "Ba ";

                case 4:
                    return "Bốn ";

                case 5:
                    if ((n / 10000) % 10 == 0)
                        return "Năm ";
                    return "Lăm ";

                case 6:
                    return "Sáu ";

                case 7:
                    return "Bảy ";

                case 8:
                    return "Tám ";

                case 9:
                    return "Chín ";
            }
            return "";
        }

        private static string docsohangtramDV(long n)
        {
            switch ((n / 100) % 10)
            {
                case 0:
                    if (((n / 10) % 10 != 0 || n % 10 != 0) && n > 100)
                        return "Không Trăm ";
                    return "";

                case 1:
                    return "Một Trăm ";

                case 2:
                    return "Hai Trăm ";

                case 3:
                    return "Ba Trăm ";

                case 4:
                    return "Bốn Trăm ";

                case 5:
                    return "Năm Trăm ";

                case 6:
                    return "Sáu Trăm ";

                case 7:
                    return "Bảy Trăm ";

                case 8:
                    return "Tám Trăm ";

                case 9:
                    return "Chín Trăm ";
            }
            return "";
        }

        private static string docsohangchucDV(long n)
        {
            switch ((n / 10) % 10)
            {
                case 0:
                    if (n % 10 != 0)
                        return "Linh ";
                    return "";
                case 1:
                    return "Mười ";

                case 2:
                    return "Hai Mươi ";

                case 3:
                    return "Ba Mươi ";

                case 4:
                    return "Bốn Mươi ";

                case 5:
                    return "Năm Mươi ";

                case 6:
                    return "Sáu Mươi ";

                case 7:
                    return "Bảy Mươi ";

                case 8:
                    return "Tám Mươi ";

                case 9:
                    return "Chín Mươi ";
            }
            return "";
        }

        private static string docsohangdonviDV(long n)
        {
            switch (n % 10)
            {

                case 1:
                    return "Một";

                case 2:
                    return "Hai";

                case 3:
                    return "Ba";

                case 4:
                    return "Bốn";

                case 5:


                    if (n < 10 || (n / 10) % 10 == 0)
                        return "Năm";

                    return "Lăm";

                case 6:
                    return "Sáu";

                case 7:
                    return "Bảy";

                case 8:
                    return "Tám";

                case 9:
                    return "Chín";
            }
            return "";
        }

       public static string hienthicachdocso(long n)
        {
            string result = "";

            result += docsohangtramty(n);
            result += docsohangchucty(n);
            result += docsohangdonvity(n);
            if (n / 100000000000 != 0 || (n / 10000000000) % 10 != 0 || (n / 1000000000) % 10 != 0)
                result += "Tỷ ";

            result += docsohangtramtrieu(n);
            result += docsohangchuctrieu(n);
            result += docsohangdonvitrieu(n);
            if ((n / 100000000) % 10 != 0 || (n / 10000000) % 10 != 0 || (n / 1000000) % 10 != 0)
                result += "Triệu ";

            result += docsohangtramnghin(n);
            result += docsohangchucnghin(n);
            result += docsohangdonvinghin(n);
            if ((n / 100000) % 10 != 0 || (n / 10000) % 10 != 0 || (n / 1000) % 10 != 0)
                result += "Nghìn ";

            result += docsohangtramDV(n);
            result += docsohangchucDV(n);
            result += docsohangdonviDV(n);

            if (result.StartsWith("Linh ") == true)
            {
                Regex regex = new Regex("Linh ");
                result = regex.Replace(result, "", 1);
            }

            return result;
        }
    }
}
