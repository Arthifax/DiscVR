using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    string levelToLoadName;
    public bool canGoNext = true;


    public void NextLevel(string levelToGoTo)
    {
        if (canGoNext)
        {
            levelToLoadName = levelToGoTo;
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name != levelToLoadName)
            {
                canGoNext = false;
                Camera.main.GetComponent<Screen_Fade>().FadeOut();
                StartCoroutine(LoadRoutine());
            }
        }
    }


    private IEnumerator LoadRoutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelToLoadName);
        op.allowSceneActivation = false;
        float timer = 0;
        while (op.progress < 0.9f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < 2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        op.allowSceneActivation = true;
    }
}
