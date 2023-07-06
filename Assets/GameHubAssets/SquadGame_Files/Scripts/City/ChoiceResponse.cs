using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceResponse : MonoBehaviour
{
    [SerializeField] private GameObject TextUI;
    [SerializeField] private TextWriter TextWriter;
    [SerializeField] private int Codekeep = 6148;
    [SerializeField] private int CodeShare = 7109;
    [SerializeField] private string codeLine = "Very well. Give the following code to the rest of your team to finish the game: ";
    private void OnEnable()
    {
        TextUI.SetActive(false);
    }
    public void Keep()
    {
        codeLine+=Codekeep.ToString();
        WriteTextUI();
    }
    public void Share()
    {
        codeLine += CodeShare.ToString();
        WriteTextUI();
    }
    private void WriteTextUI()
    {
        TextUI.SetActive(true);
        TextWriter.TypeString(codeLine);
        this.gameObject.SetActive(false);
    }
}
