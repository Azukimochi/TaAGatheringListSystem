using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EventInfo : UdonSharpBehaviour
    {
        [SerializeField]
        private GatheringListSystem Parent;

        /// <summary>
        /// ジャンル
        /// </summary>
        public EventGenre Genre;

        /// <summary>
        /// 曜日
        /// </summary>
        public Week Week;

        /// <summary>
        /// イベント名
        /// </summary>
        public string EventName;

        /// <summary>
        /// 開始時刻
        /// </summary>
        public string StartTime;

        /// <summary>
        /// 開催周期
        /// </summary>
        public string HoldingCycle;

        /// <summary>
        /// 主催・副主催
        /// </summary>
        public string Organizers;

        /// <summary>
        /// Join先
        /// </summary>
        public string JoinTo;
        
        /// <summary>
        /// イベント紹介
        /// </summary>
        public string Description;

        public string Discord;
        public string Twitter;
        public string HashTag;

        public void Parse(DataDictionary dictionary)
        {
            var genreStr = dictionary["ジャンル"].String;
            Genre = genreStr == "技術系" ? EventGenre.Technical : genreStr == "学術系" ? EventGenre.Academic : EventGenre.Other;

            Week = Str2Week(dictionary["曜日"].String);

            EventName = dictionary["イベント名"].String;

            StartTime = dictionary["開始時刻"].String;

            HoldingCycle = dictionary["開催周期"].String;

            Organizers = dictionary["主催・副主催"].String;

            JoinTo = dictionary["Join先"].String;

            Discord = dictionary["Discord"].String;

            Twitter = dictionary["Twitter"].String;

            HashTag = dictionary["ハッシュタグ"].String;
            
            Description = dictionary["イベント紹介"].String;
        }

        private static Week Str2Week(string s)
        {
            switch (s)
            {
                case "日曜日":
                    return Week.Sunday;
                case "月曜日":
                    return Week.Monday;
                case "火曜日":
                    return Week.Tuesday;
                case "水曜日":
                    return Week.Wednesday;
                case "木曜日":
                    return Week.Thursday;
                case "金曜日":
                    return Week.Friday;
                case "土曜日":
                    return Week.Saturday;
                default:
                    return Week.Other;
            }
        }

        public void OnClicked()
        {
            if (Parent != null)
                Parent.ShowEventInfo(this);
        }
    }


    public enum EventGenre
    {
        Other,

        /// <summary>
        /// 学術系
        /// </summary>
        Academic,

        /// <summary>
        /// 技術系
        /// </summary>
        Technical,
    }

    public enum Week
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Other,

        None,
    }
}