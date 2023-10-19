using System;
using UdonSharp;
using UnityEditor.Graphs;
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


        void Start()
        {
            weeks = GetComponentsInChildren<Button>();
        }

        public void OnClicked(Week week)
        {
            var today = getWeekFromToday();
            if (weeks.Length != 8)
                return;

            for (int i = 0; i < weeks.Length; i++)
            {
                Button button = weeks[i];
                
                if (i == (int)today)
                    button.image.color = _todayColor;

                else if (i == (int)week)
                    button.image.color = _selectedColor;
                
                else
                    button.image.color = _defaultColor;
            }

            parent.SelectWeek(week);
        }

        public void initDefaultSelectWeekOfDay()
        {
            var week = getWeekFromToday();
            OnClicked(week);
        }
        public Week getWeekFromToday()
        {
            var toDayOfWeek = DateTime.Now.DayOfWeek;

            switch (toDayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return Week.Sunday;
                case DayOfWeek.Monday:
                    return Week.Monday;
                case DayOfWeek.Tuesday:
                    return Week.Tuesday;
                case DayOfWeek.Wednesday:
                    return Week.Wednesday;
                case DayOfWeek.Thursday:
                    return Week.Thursday;
                case DayOfWeek.Friday:
                    return Week.Friday;
                case DayOfWeek.Saturday:
                    return Week.Saturday;
            }

            return Week.None;
        }
    }
}