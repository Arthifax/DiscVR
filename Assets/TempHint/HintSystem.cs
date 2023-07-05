using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintSystem : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private List<string> hintList;
    private int index;
    [SerializeField] private int waitTime = 0;
    [SerializeField] private TMP_Text myText;
    [SerializeField] List<GameObject> objectList;
    void Start()
    {
        StartCoroutine(Waiting());
    }


    public void DisplayHint()
    {
        if (hintList.Count != 0)
        {
            if (index < hintList.Count)
            {
                myText.SetText(hintList[index]);
                index++;
            }
            else
            {
                index = 0;
                DisplayHint();
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject obj in objectList)
        {
            obj.SetActive(true);
        }
    }
}
