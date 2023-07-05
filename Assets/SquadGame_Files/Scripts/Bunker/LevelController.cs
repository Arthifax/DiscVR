using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<BedHolder> bedPositionList;
    [SerializeField] private CountDownClock clock;
    private LevelLoader levelLoader;
    private bool loading = false;
    [SerializeField] private GatherPoints gatherPoints;
    
    [SerializeField] private GameObject moneyRain;
    [SerializeField] float disableAfterSec = 10f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victoryMusic;

    private void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
    }

    public void CheckBeds()
    {
        if (!loading)
        {
            bool AllBedsCorrect = true;
            foreach (BedHolder bed in bedPositionList)
            {
                if (!bed.correct)
                {
                    AllBedsCorrect = false;
                    break;
                }
            }
            if (AllBedsCorrect)
            {
                gatherPoints.finishLevel();
                StartCoroutine(EndingSequence());
            }
        }

    }
    public void NextLevel()
    {
        loading = true;
        levelLoader.LoadNextLevel();
    }
    
    private IEnumerator EndingSequence()
    {
        moneyRain.SetActive(true);
        audioSource.clip = victoryMusic;
        audioSource.Play();
        clock.PauseClock();
        yield return new WaitForSeconds(disableAfterSec);
        NextLevel();
    }
}
