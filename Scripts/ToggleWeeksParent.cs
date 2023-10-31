using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleWeeksParent : UdonSharpBehaviour
    {
        private Button[] weeks;
        [SerializeField] private GatheringListSystem parent;
        [SerializeField] private Settings _settings;

        private Week selectedWeek = Week.None;

        void Start()
        {
            weeks = GetComponentsInChildren<Button>();
        }

        public void OnClicked(Week week)
        {
            selectedWeek = week;
            
            var today = getWeekFromToday(Util.getJST().DayOfWeek);
            if (weeks.Length != 8)
                return;

            for (int i = 0; i < weeks.Length; i++)
            {
                Button button = weeks[i];
                
                if (i == (int)week)
                    button.image.color = _settings._selectedColor;
                else if (i == (int)today)
                    button.image.color = _settings._todayColor;
                else
                    button.image.color = _settings._defaultColor;
            }
            parent.SelectWeek(week);
        }
        public void UpdateWeekFromToday()
        {
            DateTime time = Util.getJST();
            UpdateWeekFromToday(time);
        }
        public void UpdateWeekFromToday(DateTime time)
        {
            var today = time.DayOfWeek;
            if (weeks.Length != 8)
                return;

            for (int i = 0; i < weeks.Length; i++)
            {
                Button button = weeks[i];
                
                if (i == (int)today)
                    button.image.color = _settings._todayColor;

                else if (i == (int)selectedWeek)
                    button.image.color = _settings._selectedColor;
                
                else
                    button.image.color = _settings._defaultColor;
            }
        }
        public void initDefaultSelectWeekOfDay()
        {
            var week = getWeekFromToday(Util.getJST().DayOfWeek);
            OnClicked(week);
        }
        public Week getWeekFromToday(DayOfWeek toDay)
        {
            if ((int)toDay < 7)
                return (Week)toDay;

            return Week.None;
        }
    }
}