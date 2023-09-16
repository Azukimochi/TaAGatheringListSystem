
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    public class ReloadButton : UdonSharpBehaviour
    {
        [SerializeField] private GatheringListSystem parent;

        public void Pressed()
        {
            parent.InitLoadJson();
        }
    }
}