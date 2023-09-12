using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCaseAlarm : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip alarmSound;
    [SerializeField] private CountdownClock countdownClock;
    [SerializeField] private float alarmTime = 30f;

    public void CheckingWatch()
    {
        if (player.hasTakenOneWatch)
        {
            TriggerAlarm();
        }
    }

    public void TakingWatch()
    {
        if (!player.hasTakenOneWatch)
        {
            player.hasTakenOneWatch = true;
        }
    }

    public void TriggerAlarm()
    {
        alarmLight.SetActive(true);
        StartCoroutine(PlayAlarmRoutine());
    }

    private IEnumerator PlayAlarmRoutine()
    {
        playerAudio.clip = alarmSound;
        playerAudio.Play();
        countdownClock.timerTickSpeed = 0.5f;
        yield return new WaitForSeconds(alarmTime);
        alarmLight.SetActive(false);
        countdownClock.timerTickSpeed = 1f;
        playerAudio.Stop();
        playerAudio.clip = gameMusic;
        playerAudio.Play();
    }
}
