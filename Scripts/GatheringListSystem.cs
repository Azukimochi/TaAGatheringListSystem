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
    enum Week
    {
        日曜日,
        月曜日,
        火曜日,
        水曜日,
        木曜日,
        金曜日,
        土曜日
    }
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private VRCUrl _URL;
        [SerializeField] private GameObject _button;
        [SerializeField] private Scrollbar bar;
        
        private DataList dataList;
        private DataList buttonList;

        private int count = 0;
        private float margin = 18.0f;

        void Start()
        {
            Debug.Log("load start");
            VRCStringDownloader.LoadUrl(_URL, this.GetComponent<UdonBehaviour>());

            if (String.IsNullOrEmpty(_URL.ToString()))
                Debug.Log("Error URL is Empty");
            
            SelectWeek(0);
        }

        public override void Interact()
        {

        }

        public void SelectWeek(int id)
        {
            buttonList = new DataList();
            
            for (int i = 0; i < dataList.Count; i++)
            {
                var jsonData = dataList[i].DataDictionary;
                
                if (jsonData.TryGetValue("曜日", out var dWeek))
                {
                    if (dWeek.Equals((Week)id)) 
                    {
                        GameObject button = GameObject.Instantiate(_button, _button.transform.parent);
                        button.name = buttonList.Count.ToString();
                        button.SetActive(true);
                    }
                    
                }
                Debug.Log(dWeek.String);
            }
        }

        public void SelectData(int id)
        {
            var TEXT = "\n";
            
            Debug.Log($"DataCount:{dataList.Count}");

            DataList list = dataList;

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
            if (VRCJson.TryDeserializeFromJson(result.Result, out var data))
            {
                Debug.Log("Load Success");
                this.dataList = data.DataList;
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
