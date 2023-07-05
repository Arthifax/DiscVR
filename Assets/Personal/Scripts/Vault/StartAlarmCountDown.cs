using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAlarmCountDown : MonoBehaviour
{
    [SerializeField] private InsideVaultManager insideVaultManager;
    private int UICount = 0;
    public void StartCountDownOnDisable()
    {
        if (UICount > 0)
        {
            insideVaultManager.CountdownAlarm();
        }
        else
        {
            UICount++;
        }

    }

}
