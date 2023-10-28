using System;
using UdonSharp;

namespace io.github.Azukimochi
{
    public class Util : UdonSharpBehaviour
    {
        public static DateTime getJST()
        {
            TimeZoneInfo jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, jstZoneInfo);
        }
    }

}
