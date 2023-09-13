
using io.github.Azukimochi;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ButtonGatheringData : UdonSharpBehaviour
{
    [SerializeField] private GatheringListSystem parent;
    void Start()
    {
        
    }
    public void Pressed()
    {
        int id = int.Parse(this.name);
        Debug.Log($"ClickedWeek{id}");
        parent.SelectData(id);
    }
}
