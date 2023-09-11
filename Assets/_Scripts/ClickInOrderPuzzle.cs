using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ClickInOrderPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] private List<GameObject> alarmLights = new List<GameObject>();
    [SerializeField] private Material neutralAlarmMat;
    [SerializeField] private Material greenAlarmMat;
    [SerializeField] private int index = 0;
    [SerializeField] UnityEvent victoryEvent;

    [SerializeField] private TextMeshPro doorStatusText;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;

    public void CheckPuzzleButton(string buttonName)
    {
        if (buttonList[index].name.Contains(buttonName)) //if the name of the button you pressed is the same as the name in the index
        {
            alarmLights[index].GetComponent<Renderer>().material = greenAlarmMat;
            index++;
            if (index == alarmLights.Count)
            {
                playerAudio.PlayOneShot(correctSound);
                doorStatusText.text = "OPEN";
                doorStatusText.color = Color.green;
                victoryEvent.Invoke();
            }
        }
        else
        {
            index = 0;
            playerAudio.PlayOneShot(wrongSound);
            for (int i = 0; i < alarmLights.Count; i++)
            {
                alarmLights[i].GetComponent<Renderer>().material = neutralAlarmMat;
            }
        }
    }
}
