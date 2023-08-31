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
            playerAudio.clip = alarmSound;
            playerAudio.Play();
        }
    }

    public void TakingWatch()
    {
        if (!player.hasTakenOneWatch)
        {
            player.hasTakenOneWatch = true;
        }
    }
}
