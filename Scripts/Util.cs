
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    public class Util : UdonSharpBehaviour
    {
        public static DateTime getJST()
        {
            DateTime utc_input = DateTime.UtcNow;
            TimeZoneInfo jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            DateTime jst = TimeZoneInfo.ConvertTimeFromUtc(utc_input, jstZoneInfo);

            return jst;
        }
        public static Week getWeekFromToday(DayOfWeek toDay)
        {
            switch (toDay)
            {
                case DayOfWeek.Sunday:
                    return Week.Sunday;
                case DayOfWeek.Monday:
                    return Week.Monday;
                case DayOfWeek.Tuesday:
                    return Week.Tuesday;
                case DayOfWeek.Wednesday:
                    return Week.Wednesday;
                case DayOfWeek.Thursday:
                    return Week.Thursday;
                case DayOfWeek.Friday:
                    return Week.Friday;
                case DayOfWeek.Saturday:
                    return Week.Saturday;
            }

            return Week.None;
        }
        public static Week getWeekFromTodayJST()
        {
            return getWeekFromToday(getJST().DayOfWeek);
        }
    }

}
