using UdonSharp;
using UnityEngine;

namespace io.github.Azukimochi
{
    public class FilterButton : UdonSharpBehaviour
    {
        [SerializeField] private GatheringListSystem parent;

        public void Pressed()
        {
            parent.changeFilter();
        }
    }
}