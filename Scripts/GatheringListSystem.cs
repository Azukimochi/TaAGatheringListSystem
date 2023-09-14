using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;
using YamlDotNet.Core.Tokens;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private VRCUrl _URL;
        [SerializeField] private GameObject _button;

        [SerializeField] private Week _initialDisplayWeek;

        [Space(15)]
        [SerializeField] private InputField _joinInfo;
        [SerializeField] private InputField _Discord;
        [SerializeField] private InputField _X;
        [SerializeField] private InputField _Tag;
        
        private GameObject[] _loadedDatas;
        private Week _currentWeek = Week.None;

        private bool _isLoaded;
        public bool IsLoaded => _isLoaded;

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

        public void SelectWeek(Week week)
        {
            if (week == _currentWeek)
                return;
            _currentWeek = week;

            InfoToClear();
            foreach(var obj in _loadedDatas)
            {
                var info = obj.GetComponent<EventInfo>();
                obj.SetActive(info.Week == week);
            }
        }

        public void ShowEventInfo(EventInfo info)
        {
            _joinInfo.text = info.JoinTo;
            _Discord.text = info.Discord;
            _X.text = info.Twitter;
            _Tag.text = info.HashTag;
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
                _button.SetActive(false);
                _isLoaded = true;

                ClearLoadedData();
                _loadedDatas = new GameObject[data.DataList.Count];
                for(int i = 0; i < _loadedDatas.Length; i++)
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

                GetComponentInChildren<ToggleWeeksParent>().OnClicked(_initialDisplayWeek);
            }
            else
            {
                Debug.Log("Load Failed");
            }

        }

        private void ClearLoadedData()
        {
            if (_loadedDatas == null)
                return;

            foreach(var obj in _loadedDatas)
            {
                Destroy(obj);
            }
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.Log(result.Error);
        }
    }
}
