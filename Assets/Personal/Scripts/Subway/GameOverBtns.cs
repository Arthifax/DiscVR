using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverBtns : MonoBehaviour
{
    public TextMeshProUGUI Retry;
    public TextMeshProUGUI Exit;

    public void SetToHighlighted(GameObject hit)
    {
        DeselectBoth();
        hit.transform.GetComponent<TextMeshProUGUI>().fontSize = 84f;
    }

    public void DeselectBoth()
    {
        Retry.transform.GetComponent<TextMeshProUGUI>().fontSize = 80f;
        Exit.transform.GetComponent<TextMeshProUGUI>().fontSize = 80f;
    }
}
