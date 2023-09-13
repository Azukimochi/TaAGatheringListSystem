using System;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;
using Object = System.Object;

namespace io.github.Azukimochi
{
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private VRCUrl _URL;
        [SerializeField] private GameObject _button;
        [SerializeField] private Text[] _text;
        [SerializeField] private RectTransform form;
        [SerializeField] private Scrollbar bar;
        
        private DataToken data;

        private int count = 0;
        private float margin = 18.0f;

        void Start()
        {
            Debug.Log("load start");
            VRCStringDownloader.LoadUrl(_URL, this.GetComponent<UdonBehaviour>());

            if (String.IsNullOrEmpty(_URL.ToString()))
                Debug.Log("Error URL is Empty");
            
            for (int i = 1; i < 10; i++)
            {
                GameObject button = GameObject.Instantiate(_button, _button.transform.parent);
                button.name = i.ToString();
                button.SetActive(true);
            }
        }

        public override void Interact()
        {

        }

        public void SelectData(int id)
        {
            var TEXT = "\n";
            
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
            
            bar.value = 1.0f;
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            if (VRCJson.TryDeserializeFromJson(result.Result, out data))
            {
                Debug.Log("Load Success");
                this.data = data;
            }
            else
            {
                Debug.Log("Load Failed");
            }

        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
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
