using System;
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
        [SerializeField] private GameObject _button;
        [SerializeField] private Scrollbar bar;
        [Space(15)]
        [SerializeField] private InputField _joinInfo;
        [SerializeField] private InputField _Discord;
        [SerializeField] private InputField _X;
        [SerializeField] private InputField _Tag;
        
        private DataList dataList;
        private DataList infoList = new DataList();
        private DataList buttonList = new DataList();


        void Start()
        {
            Debug.Log("load start");
            VRCStringDownloader.LoadUrl(_URL, this.GetComponent<UdonBehaviour>());

            if (String.IsNullOrEmpty(_URL.ToString()))
                Debug.Log("Error URL is Empty");
            
        }

        public override void Interact()
        {

        }

        public void SelectWeek(int id)
        {
            InfoToClear();
            
            for (int i = 0; i < buttonList.Count; i++)
            {
                //GameObject obj = buttonList[i].Reference as GameObject;
                GameObject obj = (GameObject) buttonList[i].Reference;
                Destroy(obj);
            }
            for(int i = 0; i < infoList.Count; i++)
            {
                infoList.RemoveAt(i);
            }
            if (dataList == null)
            {
                Debug.Log("DataList is null");
                return;
            }
            buttonList.Clear();
            infoList.Clear();
            
            for (int i = 0; i < dataList.Count; i++)
            {
                var jsonData = dataList[i].DataDictionary;
                
                if (jsonData.TryGetValue("曜日", out var dWeek))
                {
                    if (dWeek.String == Id2Week(id))
                    {
                        string Text = "";
                        
                        GameObject button = GameObject.Instantiate(_button, _button.transform.parent);
                        button.name = buttonList.Count.ToString();
                        button.SetActive(true);
                        buttonList.Add(button);
                        
                        infoList.Add(dataList[i]);
                        
                        if (jsonData.TryGetValue("開始時刻", out var dData))
                        {
                            Text += dData;
                        }
                        if(jsonData.TryGetValue("開催周期", out var eCycle))
                        {
                            Text += $" {eCycle}";
                        }
                        if (jsonData.TryGetValue("イベント名", out var eName))
                        {
                            Text += $"\n{eName}";
                        }
                        
                        if(jsonData.TryGetValue("主催・副主催", out var eHost))
                        {
                            Text += $"\n主催・副主催：{eHost}";
                        }
                        button.GetComponentInChildren<Text>().text = Text;
                    }
                }
            }
        }

        public void SelectData(int id)
        {
            var jsonData = infoList[id].DataDictionary;
            
            if(jsonData.TryGetValue("Join先", out var join))
            {
                _joinInfo.text = join.String;
            }
            if(jsonData.TryGetValue("Discord", out var discord))
            {
                _Discord.text = discord.String;
            }
            if(jsonData.TryGetValue("Twitter", out var twitter))
            {
                _X.text = twitter.String;
            }
            if(jsonData.TryGetValue("ハッシュタグ", out var tag))
            {
                _Tag.text = tag.String;
            }

        }
        public void InfoToClear()
        {
            _joinInfo.text = "";
            _Discord.text = "";
            _X.text = "";
            _Tag.text = "";
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            if (VRCJson.TryDeserializeFromJson(result.Result, out var data))
            {
                Debug.Log("Load Success");
                this.dataList = data.DataList;
                
                _button.SetActive(false);
                SelectWeek(0);
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

        private string Id2Week(int id)
        {
            switch (id) 
            {
                case 0: return "日曜日";
                case 1: return "月曜日";
                case 2: return "火曜日";
                case 3: return "水曜日";
                case 4: return "木曜日";
                case 5: return "金曜日";
                case 6: return "土曜日";
                default: return "その他";
            }
        }
    }
}
