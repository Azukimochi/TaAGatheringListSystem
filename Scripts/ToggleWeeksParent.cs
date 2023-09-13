
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleWeeksParent : UdonSharpBehaviour
{

    [SerializeField] private GameObject[] weeks;
    
    
    void Start()
    {
        
    }

    public void OnClicked(int id)
    {
        Debug.Log($"Clicked id:{id}");
        if (weeks.Length != 7)
            return;
        
        Debug.Log($"SetColor");

        for (int i = 0; i < weeks.Length; i++)
        {
            Button button = weeks[i].GetComponent<Button>();
            if (i == id)
                button.image.color = Color.gray;
            else
                button.image.color = Color.black;
        }
    }
}
