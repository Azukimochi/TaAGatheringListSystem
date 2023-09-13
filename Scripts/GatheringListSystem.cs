using System;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private VRCUrl _URL;
        [SerializeField] private Text _text;
        [SerializeField] private RectTransform form;
        [SerializeField] private Scrollbar bar;

        private int count = 0;
        private float margin = 18.0f;

        void Start()
        {
            VRCStringDownloader.LoadUrl(_URL, this.GetComponent<UdonBehaviour>());
            _text.text = "Loading...";

            if (String.IsNullOrEmpty(_URL.ToString()))
                _text.text = "Error URL is Empty";
        }

        public override void Interact()
        {

        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            DataToken data;
            var TEXT = "\n";

            if (VRCJson.TryDeserializeFromJson(result.Result, out data))
            {
                Debug.Log($"DataCount:{data.DataList.Count}");

                DataList list = data.DataList;

                int month = 0;
                int day = 0;

                for (int i = 0; i < list.Count; i++)
                {
                    var jsonData = list[i].DataDictionary;

                    if (jsonData.TryGetValue("日付", out var dData))
                    {
                        var d = DateTimeOffset.ParseExact(dData.String, "yyyy-MM-ddTHH:mm:ss.fffZ", null);

                        Debug.Log($"【{d.Month}月 {d.Day}日");
                        if ((day != d.Day) || (month != d.Month))
                        {
                            TEXT += $"【{d.Month}月 {d.Day}日】";
                            month = d.Month;
                            day = d.Day;
                            TEXT += "\n";
                        }
                        //TEXT += $"{d.Month}月 {d.Day}日 {d.Hour}:{d.Minute:D02} ～";
                    }

                    if (jsonData.TryGetValue("開始時間", out var sTime))
                        TEXT += $" {sTime.String}～";

                    TEXT += "\n";

                    if (jsonData.TryGetValue("テーマ", out var dTheme))
                        TEXT += $" {dTheme.String}";

                    TEXT += "\n";

                    if (jsonData.TryGetValue("発表者", out var dName))
                    {
                        TEXT += $" 発表者：{dName.String}";
                    }

                    TEXT += "\n \n";
                }
            }

            _text.text = TEXT;
            Debug.Log(TEXT);

            count = CountChar(TEXT, "\n");
            if (count > 28)
            {
                form.sizeDelta = new Vector2(353.2f, margin * count);
            }

            bar.value = 1.0f;
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            _text.text = "Error.";
            Debug.Log(result.Error);
        }

        public static int CountChar(string s, string target)
        {

            int at;
            int start = 0;
            int rep = 0;
            while (true)
            {
                at = s.IndexOf(target, start, StringComparison.Ordinal);
                if (at == -1) break;
                start = at + 1;
                rep++;
            }

            return rep;
        }
    }
}
