
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
    }

}
