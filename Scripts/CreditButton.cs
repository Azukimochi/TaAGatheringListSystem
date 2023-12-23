
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CreditButton : UdonSharpBehaviour
{
    [SerializeField] private GameObject _creditPanel;
    
    public void Pressed()
    {
        if(_creditPanel.activeSelf)
            _creditPanel.SetActive(false);
        else if(!_creditPanel.activeSelf)
            _creditPanel.SetActive(true);
    }
}
