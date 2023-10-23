using System;
using System.Data;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private VRCUrl _URL;
        [SerializeField] private GameObject _button;
        [SerializeField] private ToggleWeeksParent _toggleWeeksParent;

        [Space(15)] [SerializeField] private InputField _joinInfo;
        [SerializeField] private InputField _Discord;
        [SerializeField] private InputField _X;
        [SerializeField] private InputField _Tag;
        [SerializeField] private Text _Description;
        
        [Space(height:15)] 
        [SerializeField] private Color _highlightColor  = new Color(0.7f, 0.4f, 0.4f);
        
        [Space(height:15)] 
        [SerializeField] int _UpdateIntervalMinutes = 5;
        [SerializeField] bool _isDebug = false;
        [SerializeField] private string _DebugTime;
        
        private Week _initialDisplayWeek;
        private GameObject[] _loadedDatas;
        private Week _currentWeek = Week.None;
        private bool _isLoaded;
        

        //定期実行なのでJSTじゃなくてOK.
        private DateTime LastUpdate;

        void Start()
        {
            InitLoadJson();
            LastUpdate = DateTime.Now;
        }

        void Update()
        {
            if (LastUpdate.AddMinutes(_UpdateIntervalMinutes) < DateTime.Now)
            {
                Debug.Log($"[TaAG Sys] Update interval {_UpdateIntervalMinutes}min");
                if (!_isDebug)
                {
                    _toggleWeeksParent.UpdateWeekFromToday();
                    highlightButton();
                    LastUpdate = DateTime.Now;
                }
                else
                {
                    const string Format = "yyyy-MM-dd HH:mm:ss";
                    DateTime newValue;
                    if (DateTime.TryParse(_DebugTime, out newValue))
                    {
                        Debug.Log(newValue.ToString(Format));
                        Debug.Log(newValue);
                        _toggleWeeksParent.UpdateWeekFromToday(newValue);
                        LastUpdate = DateTime.Now;
                    }
                }
            }
        }

        public void InitLoadJson()
        {
            Debug.Log("load start");
            VRCStringDownloader.LoadUrl(_URL, this.GetComponent<UdonBehaviour>());

            if (String.IsNullOrEmpty(_URL.ToString()))
                Debug.Log("Error URL is Empty");
        }

        public override void Interact()
        {

        }

        public void SelectWeek(Week week)
        {
            if (week == _currentWeek)
                return;
            _currentWeek = week;

            InfoToClear();
            foreach (var obj in _loadedDatas)
            {
                var info = obj.GetComponent<EventInfo>();
                
                if (info.Week == week)
                    obj.SetActive(true);
                else
                    obj.SetActive(false);
            }
        }
        public void highlightButton() 
        {
            foreach (var obj in _loadedDatas)
            {
                var info = obj.GetComponent<EventInfo>();
                String format = @"hh\:mm";
                obj.GetComponentInChildren<Image>().color = Color.white;
                
                if (TimeSpan.TryParseExact(info.StartTime, format, null, out var startTime))
                {
                    var endTime = startTime.Add(info.HoldingTime);
                    var currentTime = Util.getJST().TimeOfDay;

                    if (info.Week == Util.getWeekFromTodayJST() && startTime < currentTime && currentTime < endTime)
                        obj.GetComponentInChildren<Image>().color = _highlightColor;
                    Debug.Log($"Onheld {info.EventName} {startTime} {endTime} {currentTime}");
                }
            }
        }

        public void ShowEventInfo(EventInfo info)
        {
            _joinInfo.text = info.JoinTo;
            _Discord.text = info.Discord;
            _X.text = info.Twitter;
            _Tag.text = info.HashTag;
            _Description.text = info.Description;
        }

        public void InfoToClear()
        {
            _joinInfo.text = "Copy from here";
            _Discord.text = "Copy from here";
            _X.text = "Copy from here";
            _Tag.text = "Copy from here";
            _Description.text = "Description";
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            if (VRCJson.TryDeserializeFromJson(result.Result, out var data))
            {
                Debug.Log("Load Success");
                _button.SetActive(false);
                _isLoaded = true;

                ClearLoadedData();
                _loadedDatas = new GameObject[data.DataList.Count];
                
                for (int i = 0; i < _loadedDatas.Length; i++)
                {
                    var obj = Instantiate(_button, _button.transform.parent);
                    var info = obj.GetComponent<EventInfo>();
                    info.Parse(data.DataList[i].DataDictionary);
                    obj.name = info.EventName;
                    obj.GetComponentInChildren<Text>().text = $@"{info.StartTime} {info.HoldingCycle}
{info.EventName}
主催・副主催：{info.Organizers}";

                    _loadedDatas[i] = obj;
                }
                
                //デバッグ用
                if (_isDebug)
                {
                    var newArray = new GameObject[_loadedDatas.Length + 1];
                    Array.Copy(_loadedDatas, newArray, _loadedDatas.Length);
                    _loadedDatas = newArray;
                    
                    var obj = Instantiate(_button, _button.transform.parent);
                    var info = obj.GetComponent<EventInfo>();
                    info.Week = Week.Monday;
                    info.StartTime = "19:00";
                    info.HoldingTime = TimeSpan.FromHours(1);
                    obj.GetComponentInChildren<Text>().text = $@"{info.StartTime} 1億年に一回
                    デバッグ集会
                    主催・副主催：誰？";
                    _loadedDatas[_loadedDatas.Length - 1] = obj;
                }

                GetComponentInChildren<ToggleWeeksParent>().OnClicked(_initialDisplayWeek);
                SelectWeek(Week.Sunday);
            }
            else
            {
                Debug.Log("Load Failed");
            }

            //ロード完了後に初期表示曜日を設定
            //if Asyncに置き換え
            _toggleWeeksParent.initDefaultSelectWeekOfDay();
            highlightButton();
        }

        private void ClearLoadedData()
        {
            if (_loadedDatas == null)
                return;

            foreach (var obj in _loadedDatas)
            {
                Destroy(obj);
            }
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.Log(result.Error);
        }

        private void DebugLog(string message)
        {
            DateTime newValue;
            
        }
    }
}