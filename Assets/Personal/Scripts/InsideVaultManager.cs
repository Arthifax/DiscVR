using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideVaultManager : MonoBehaviour {

    public int state;

    [SerializeField]
    private float alarmTime;

    [SerializeField]
    private Animator alarmAnim;

    [SerializeField] AudioSource alarmMusic;
    [SerializeField] AudioSource notAlarmMusic;
    [SerializeField] Animator vaultTextAnim;

    public void CountdownAlarm()
    {
        StartCoroutine(SoundAlarmDelay());
    }

    IEnumerator SoundAlarmDelay()
    {
        vaultTextAnim.SetTrigger("PlayAnim");
        yield return new WaitForSeconds(alarmTime);
        alarmAnim.SetTrigger("Alarm");
        state = 1;
        alarmAnim.transform.GetComponent<AudioSource>().Play();
        notAlarmMusic.Stop();
        alarmMusic.Play();
    }
}
