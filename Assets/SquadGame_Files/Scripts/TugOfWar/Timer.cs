using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public List<Didgit> didgits;
    public GatherPoints pointManager;
    [Space]
    [Header("Level Time")]
    private int timerClock = 0;
    private int index = 0;
    private bool canEnd = true;
    [Space]
    public UnityEvent TimeDoneEvent;
    [Space]
    public bool Test;

    private void Start()
    {
        Test = true;
        timerClock = pointManager.getTimerTime();
    }
    private void Update()
    {
        if (Test)
        {
            Test = false;
            StopAllCoroutines();
            StartCoroutine(timer());
        }
        if (timerClock <= 0 && canEnd)
        {
            canEnd = false;
            TimeDoneEvent.Invoke();
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        int currentTime = timerClock;
        didgits[index].setNumber(Mathf.FloorToInt(currentTime / (600)));
        index += 1;
        currentTime -= 600 * Mathf.FloorToInt(currentTime / (600));

        didgits[index].setNumber(Mathf.FloorToInt(currentTime / (60)));
        index += 1;
        currentTime -= 60 * Mathf.FloorToInt(currentTime / (60));

        didgits[index].setNumber(Mathf.FloorToInt(currentTime / (10)));
        index += 1;
        currentTime -= 10 * Mathf.FloorToInt(currentTime / (10));

        didgits[index].setNumber(Mathf.FloorToInt(currentTime));
        timerClock--;
        index = 0;
        Test = true;
    }
}
