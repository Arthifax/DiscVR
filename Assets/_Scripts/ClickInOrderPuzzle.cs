using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ClickInOrderPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] private List<GameObject> progressLights = new List<GameObject>();
    [SerializeField] private Material neutralLightsMat;
    [SerializeField] private Material greenLightsmMat;
    [SerializeField] private int index = 0;
    [SerializeField] UnityEvent victoryEvent;

    [SerializeField] private TextMeshPro doorStatusText;

    [SerializeField] private GameObject alarmLight;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioSource gameMusicSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip alarmSound;
    [SerializeField] private float alarmTime = 30f;


    public void CheckPuzzleButton(string buttonName)
    {
        if (buttonList[index].name.Contains(buttonName)) //if the name of the button you pressed is the same as the name in the index
        {
            progressLights[index].GetComponent<Renderer>().material = greenLightsmMat;
            index++;
            if (index == progressLights.Count)
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
            TriggerAlarm();
            for (int i = 0; i < progressLights.Count; i++)
            {
                progressLights[i].GetComponent<Renderer>().material = neutralLightsMat;
            }
        }
    }

    public void TriggerAlarm()
    {
        alarmLight.SetActive(true);
        StartCoroutine(PlayAlarmRoutine());
    }

    private IEnumerator PlayAlarmRoutine()
    {
        gameMusicSource.clip = alarmSound;
        gameMusicSource.Play();
        yield return new WaitForSeconds(alarmTime);
        alarmLight.SetActive(false);
        gameMusicSource.Stop();
        gameMusicSource.clip = gameMusic;
        gameMusicSource.Play();
    }
}
