using UdonSharp;
using UnityEngine;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleWeeksButton : UdonSharpBehaviour
    {
        [SerializeField] private Week Week;
        [SerializeField] private ToggleWeeksParent parent;

        public void Pressed()
        {
            Debug.Log($"[TaAG Sys] ClickedWeek {Week}");
            parent.OnClicked(Week);
        }
    }
}