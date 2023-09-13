
using io.github.Azukimochi;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleWeeksParent : UdonSharpBehaviour
{

    [SerializeField] private GameObject[] weeks;
    [SerializeField] private GatheringListSystem parent;
    
    
    void Start()
    {
        weeks[0].GetComponent<Button>().image.color = Color.gray;
    }

    public void OnClicked(int id)
    {
        if (weeks.Length != 8)
            return;
        
        for (int i = 0; i < weeks.Length; i++)
        {
            Button button = weeks[i].GetComponent<Button>();
            if (i == id)
                button.image.color = Color.gray;
            else
                button.image.color = Color.black;
        }
        parent.SelectWeek(id);
    }
}
