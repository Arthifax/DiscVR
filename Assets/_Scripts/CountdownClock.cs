using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountdownClock : MonoBehaviour
{
    [SerializeField] private List<Didgit> didgits;
    [SerializeField] private int maxTimeSeconds = 0;
    [SerializeField] private int huidigSeconden = 0;
    [Space]
    [Header("Level Time")]
    private int timerClock = 0;
    private int index = 0;
    private bool canEnd = true;
    [Space]
    [SerializeField] private UnityEvent TimeDoneEvent;
    [Space]
    [SerializeField] private bool Test;

    private void Start()
    {
        Test = true;
        StartCoroutine(countSeconds());
        timerClock = getTimerTime();
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

    public int getTimerTime()
    {
        return maxTimeSeconds - huidigSeconden;
    }

    IEnumerator countSeconds()
    {
        if (huidigSeconden != maxTimeSeconds)
        {
            yield return new WaitForSeconds(1);
            huidigSeconden += 1;
            StopAllCoroutines();
            StartCoroutine(countSeconds());
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
