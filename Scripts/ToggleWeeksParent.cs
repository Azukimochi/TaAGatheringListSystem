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

        [SerializeField] private Color _defaultColor = Color.black;
        [SerializeField] private Color _selectedColor = Color.gray;
        [SerializeField] private Color _todayColor = new Color(0.3f, 0.3f, 0.3f);

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
                    button.image.color = _selectedColor;
                else if (i == (int)today)
                    button.image.color = _todayColor;
                else
                    button.image.color = _defaultColor;
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
                    button.image.color = _todayColor;

                else if (i == (int)selectedWeek)
                    button.image.color = _selectedColor;
                
                else
                    button.image.color = _defaultColor;
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