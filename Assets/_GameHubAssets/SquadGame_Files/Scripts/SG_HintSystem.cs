using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SG_HintSystem : MonoBehaviour
{
    public List<string> hintList;
    public PointManager pointManager;
    public TextMeshPro myText;
    [SerializeField] private int minusPointsHints;
    private bool canRemovePoints = true;
    
    private int index;
    public void getHint()
    {
        if (canRemovePoints)
        {
            pointManager.AddPoints(-minusPointsHints);
        }
        
        myText.SetText(hintList[index]);
        index++;

        if (index >= hintList.Count)
        {
            canRemovePoints = false;
            index = 0;
        }        
    }
}
