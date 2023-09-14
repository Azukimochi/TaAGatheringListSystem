
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


namespace io.github.Azukimochi
{
    public class ToggleWeeksButton : UdonSharpBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private ToggleWeeksParent parent;

        void Start()
        {

        }
        public void Pressed()
        {
            Debug.Log($"ClickedWeek{id}");
            parent.OnClicked(id);
        }
    }
}