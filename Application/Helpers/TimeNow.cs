using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.Helpers
{
    public class TimeNow
    {
        public static DateTime UtcPlus7()
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime localTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, timeZoneInfo);

            return utcTime;
        }
    }
}
