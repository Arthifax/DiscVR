using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderRedLight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Screen_Fade screenFade;
    [SerializeField] private string nextLevel;
    [SerializeField] private LaserFire hauntedDemonicDoll;
    [SerializeField] private Timer countDownClock;
    private bool methodComplete = true;
    private string row;
    [SerializeField] private List<GameObject> nextTeleports;
    int index = 0;
    private bool GameOver = false;

    [SerializeField] private PointManager mrPoints;
    
    private void Start()
    {
        countDownClock.TimeDoneEvent.AddListener(ClockOutOfTime);
    }
    public void ReloadLevel()
    {
        StartCoroutine(FadeAndReload());
    }
    public void LoadNextLevel()
    {
        StartCoroutine(FadeAndChange());
    }

    private IEnumerator FadeAndChange()
    {
        yield return new WaitForSeconds(1f);
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(nextLevel);
    }
    private IEnumerator FadeAndReload()
    {
        yield return new WaitForSeconds(2f);
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MethodComplete()
    {
        if (!GameOver)
        {
            nextTeleports[index].SetActive(true);
            //countDownClock.ResetClock();
            //countDownClock.StartClock();
            index++;
        }
        methodComplete = true;
    }

    public void ClockOutOfTime()
    {
        //countDownClock.PauseClock();
        hauntedDemonicDoll.AddLaserTarget(player);
        hauntedDemonicDoll.OutOfTime();
    }

    public void OutOfTime(string rowNR)
    {
        if (methodComplete && rowNR != row)
        {
            //countDownClock.PauseClock();
            row = rowNR;
            methodComplete = false;
            hauntedDemonicDoll.AddLaserTarget(player);
            hauntedDemonicDoll.OutOfTime();
            GameOver = true;
        }
    }
    public void ShootNPC(string rowNR)
    {
        if (methodComplete && rowNR != row)
        {
            //countDownClock.PauseClock();
            row = rowNR;
            methodComplete = false;
            hauntedDemonicDoll.OutOfTime();
        }
    }
}
