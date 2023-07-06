using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTeleport : MonoBehaviour
{
    [SerializeField] private CountDownClock[] clock;
    public void EndLevel()
    {
        clock[0].PauseClock();
        clock[1].PauseClock();
        StartCoroutine(LevelComplete());
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(.1f);
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<LevelLoader>().LoadNextLevel();
    }
}
