
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleWeeksButton: UdonSharpBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private ToggleWeeksParent pareint;
    
    void Start()
    {
        
    }
    public void Pressed()
    {
        Debug.Log("Clicked");
        pareint.OnClicked(id);
    }
}
