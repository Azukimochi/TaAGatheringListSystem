
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleWeeksButton: UdonSharpBehaviour
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
