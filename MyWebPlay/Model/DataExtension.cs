using System.Globalization;

namespace MyWebPlay.Model
{
    public static class DateTimeExtension
    {
        public static DateTime SendToDelaySetting(this DateTime xuxu, string delayTime)
        {
            var partDelayTime = delayTime.Split("#");
            var hourDL = partDelayTime[0].Replace("H", "");
            var minDL = partDelayTime[1].Replace("M", "");
            var secDL = partDelayTime[2].Replace("S", "");

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

            return xuxu;
        }

        public static string SendToDateTimeDelayForString(this DateTime xuxu)
        {
            return xuxu.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
        }
    }
}
