using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCaseAlarm : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip alarmSound;

    public void CheckingWatch()
    {
        if (player.hasTakenOneWatch)
        {
            alarmLight.SetActive(true);
            StartCoroutine(PlayAlarmSound());
        }
    }

    public void TakingWatch()
    {
        if (!player.hasTakenOneWatch)
        {
            player.hasTakenOneWatch = true;
        }
    }

    private IEnumerator PlayAlarmSound()
    {
        playerAudio.clip = alarmSound;
        playerAudio.Play();
        yield return new WaitForSeconds(30f);
        playerAudio.Stop();
    }
}
