using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CompletedCookiesValidator : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private CountDownClock clock;
    [SerializeField] private int CookieCount = 4;
    private int CurrentCookiesCompleted = 0;
    public UnityEvent completedLevelEvent;
    public void CookieCompleted()
    {
        CurrentCookiesCompleted++;
        if (CurrentCookiesCompleted == CookieCount)
        {
            completedLevelEvent.Invoke();
            clock.PauseClock();
            levelLoader.LoadNextLevel();
        }
    }
}
