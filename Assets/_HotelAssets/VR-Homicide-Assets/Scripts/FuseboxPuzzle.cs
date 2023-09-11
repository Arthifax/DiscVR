using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FuseboxPuzzle : MonoBehaviour
{
    [SerializeField] private VaultManager vaultManager;
    [SerializeField] private List<GameObject> wireList = new List<GameObject>();
    [SerializeField] private GameObject vfxSparks;
    [SerializeField] private int index = 0;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private Material greenAlarmMat;
    [SerializeField] private Material redAlarmMat;
    private bool isEqual;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip electricitySound;

    public void CheckWire(string wireColor)
    {
        if (wireList[index].name.Contains(wireColor))
        {
            wireList[index].SetActive(false);
            index++;
            if (index == 3)
            {
                StartCoroutine(PlayVictoryRoutine());
            }
        }
        else
        {
            alarmLight.GetComponent<Renderer>().material = redAlarmMat;
            index = 0;
            for (int i = 0; i < wireList.Count; i++)
            {
                wireList[i].SetActive(true);
            }
        }
    }

    private IEnumerator PlayVictoryRoutine()
    {
        playerAudio.clip = electricitySound;
        playerAudio.Play();
        vfxSparks.SetActive(true);
        yield return new WaitForSeconds(3f);
        playerAudio.clip = correctSound;
        playerAudio.Play();
        alarmLight.GetComponent<Renderer>().material = greenAlarmMat;
        vaultManager.wireBoxPuzzleCompleted = true;
        yield return new WaitForSeconds(2f);
        vfxSparks.SetActive(false);
    }
}
