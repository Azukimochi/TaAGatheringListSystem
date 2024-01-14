using System;
using UdonSharp;

namespace io.github.Azukimochi
{
    public class Util : UdonSharpBehaviour
    {
        public static DateTime getJST()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            var jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
#else
            var jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
#endif
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, jstZoneInfo);
        }
    }

}
