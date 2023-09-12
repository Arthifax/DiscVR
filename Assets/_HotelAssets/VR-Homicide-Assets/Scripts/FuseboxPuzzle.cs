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
    [SerializeField] private Material defaultAlarmMat;
    [SerializeField] private bool lostGame = false;
    private bool isEqual;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip electricitySound;

    public void CheckWire(string wireColor)
    {
        // Cut the wire and check if it's right or wrong. If wrong. Set a failure bool. At three tries. If you've got the right one and you haven't failed. You win.
        if(index < 2)
        {
            if (wireList[index].name.Contains(wireColor))
            {
                index++;
            }
            else
            {
                lostGame = true;
                index++;
            }
        }
        else //after you've chosen 2 wires, check the third one you cut to see if the game is done
        {
            if (wireList[index].name.Contains(wireColor) && !lostGame) //if you get the correct one and haven't chosen a wrong one yet
            {
                StartCoroutine(PlayVictoryRoutine());
                index = 0;
            }
            else
            {
                StartCoroutine(PlayLoseRoutine());
                index = 0;
                lostGame = false;
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

    private IEnumerator PlayLoseRoutine()
    {
        alarmLight.GetComponent<Renderer>().material = redAlarmMat;
        playerAudio.clip = wrongSound;
        playerAudio.Play();

        yield return new WaitForSeconds(2f);

        alarmLight.GetComponent<Renderer>().material = defaultAlarmMat;
        for (int i = 0; i < wireList.Count; i++)
        {
            wireList[i].SetActive(true);
        }
    }
}
