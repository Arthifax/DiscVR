using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CountDownClock : MonoBehaviour
{
    [SerializeField] private int countDownMinutes;
    [SerializeField, Range(0, 59)] private int countDownSeconds;
    [SerializeField] private CountdownNumberController10 TwoDigitMinute;
    [SerializeField] private CountdownNumberController1 OneDigitMinute;
    [SerializeField] private CountdownNumberController10 TwoDigitSecond;
    [SerializeField] private CountdownNumberController1 OneDigitSecond;
    private bool GameOver = false;
    private float countdownSecondsTotal;
    [SerializeField] private bool clockStarted = false;
    private bool resettingClock = false;

    public UnityEvent OutOfTime;

    private void Awake()
    {
        if (OutOfTime == null)
        {
            OutOfTime = new UnityEvent();
        }
    }

    void Start()
    {
        countdownSecondsTotal = countDownMinutes * 60 + countDownSeconds;
    }
    public void ResetClock()
    {
        resettingClock = true;
        countdownSecondsTotal = countDownMinutes * 60 + countDownSeconds;
        resettingClock = false;
    }
    public void PauseClock()
    {
        clockStarted = false;
    }
    public void StartClock()
    {
        clockStarted = true;
    }

    void Update()
    {
        if (countdownSecondsTotal > 0)
        {
            if (clockStarted)
                countdownSecondsTotal -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(countdownSecondsTotal / 60);
            int seconds = Mathf.FloorToInt(countdownSecondsTotal % 60);
            SetMinutes(minutes);
            SetSeconds(seconds);
        }
        else
        {
            if (!GameOver&&!resettingClock)
            {
                GameOver = true;
                OutOfTime.Invoke();
            }
        }
    }
    private void SetMinutes(int minutes)
    {
        if (minutes > 0)
        {
            int minutesInTens = Mathf.FloorToInt(minutes / 10);
            int minutesSeperate = minutes - (minutesInTens * 10);
            TwoDigitMinute.GetNumber(minutesInTens);
            OneDigitMinute.GetNumber(minutesSeperate);
        }
        else
        {
            TwoDigitMinute.GetNumber(0);
            OneDigitMinute.GetNumber(0);
        }
    }
    private void SetSeconds(int seconds)
    {
        if (seconds > 0)
        {
            int secondsInTens = Mathf.FloorToInt(seconds / 10);
            int secondsSeperate = seconds - (secondsInTens * 10);
            TwoDigitSecond.GetNumber(secondsInTens);
            OneDigitSecond.GetNumber(secondsSeperate);
        }
        else
        {
            TwoDigitSecond.GetNumber(0);
            OneDigitSecond.GetNumber(0);
        }
    }
    private void OnDestroy()
    {
        OutOfTime.RemoveAllListeners();
        OutOfTime = null;
    }
}
