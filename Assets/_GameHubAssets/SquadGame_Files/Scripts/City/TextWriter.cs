using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FrontManTextObject;
    [SerializeField] private WaitForSeconds writeDelay = new WaitForSeconds(.045f);
    [SerializeField] private GameObject ChoiceUI;
    [SerializeField, Multiline] private string firstTextToWrite;
    private bool EnableChoiceUI = true;

    private void Start()
    {
       TypeString(firstTextToWrite);
    }
    public void TypeString(string textToType)
    {
        FrontManTextObject.text = "";
        StartCoroutine(TextWriteRoutine(textToType));

    }
    private IEnumerator TextWriteRoutine(string textToType)
    {
        for (int i = 0; i < textToType.Length; i++)
        {
            FrontManTextObject.text+=textToType[i];
            yield return writeDelay;
        }
        if (EnableChoiceUI)
        {
            yield return new WaitForSeconds(2f);
            EnableChoiceUI = false;
            ChoiceUI.SetActive(true);
        }
    }
}
