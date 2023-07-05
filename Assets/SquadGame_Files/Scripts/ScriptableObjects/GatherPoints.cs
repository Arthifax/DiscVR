using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GatherPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxTimeSeconds = 0;
    public int huidigSeconden = 0;
    public int PointsForGame = 0;
    public int PointsFromComplete = 0;
    public bool completedLevel = false;
    public PointManager myManager;
    public List<bool> opdrachten;
    public int waardenPerOpdracht = 0;
    public bool questCheck = false;
    public UnityEvent onLevelCompleted;

    void Start()
    {
        StartCoroutine(countSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        if (completedLevel)
        {
            completedLevel = false; ;
            myManager.AddPoints(PointsForGame / maxTimeSeconds * (maxTimeSeconds - huidigSeconden));
            for (int i = opdrachten.Count - 1; i <= opdrachten.Count; i--)
            {
                if (i > -1)
                {
                    if (!opdrachten[i] && opdrachten.Count != 0)
                    {
                        myManager.AddPoints(-waardenPerOpdracht);
                        opdrachten.RemoveAt(i);
                        completedLevel = false;
                    }
                }

            }
        }
        if (questCheck)
        {
            questCheck = false;
            checkQuests();
        }
    }

    public void checkQuests()
    {
        foreach (bool opdracht in opdrachten)
        {
            if (opdracht)
            {
                myManager.AddPoints(waardenPerOpdracht);
                opdrachten.Remove(opdracht);
                checkQuests();
            }
        }
    }

    public void finishLevel()
    {
        onLevelCompleted.Invoke();
        completedLevel = true;
    }

    public void reloadLevel()
    {
        levelReload();
    }
    
    public int getTimerTime()
    {
        return maxTimeSeconds - huidigSeconden;
    }

    IEnumerator levelReload()
    {
        yield return new WaitForSeconds(3);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    IEnumerator countSeconds()
    {
        if (huidigSeconden != maxTimeSeconds)
        {
            yield return new WaitForSeconds(1);
            huidigSeconden += 1;
            PointsFromComplete = PointsForGame / maxTimeSeconds * (maxTimeSeconds - huidigSeconden);
            StopAllCoroutines();
            StartCoroutine(countSeconds());
        }

    }
}
