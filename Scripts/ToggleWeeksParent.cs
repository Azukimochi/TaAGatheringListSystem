
using io.github.Azukimochi;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;


namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleWeeksParent : UdonSharpBehaviour
    {
        private Button[] weeks;
        [SerializeField] private GatheringListSystem parent;


        void Start()
        {
            weeks = GetComponentsInChildren<Button>();
        }

        public void OnClicked(Week week)
        {
            if (weeks.Length != 8)
                return;

            for (int i = 0; i < weeks.Length; i++)
            {
                Button button = weeks[i];
                if (i == (int)week)
                    button.image.color = Color.gray;
                else
                    button.image.color = Color.black;
            }
            parent.SelectWeek(week);
        }
    }
}