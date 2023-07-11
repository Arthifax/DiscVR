using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Screen_Fade screenFade;
    [SerializeField] private string nextLevel;

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
        yield return new WaitForSeconds(0.5f);
        screenFade.FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevel);
    }
    private IEnumerator FadeAndReload()
    {
        yield return new WaitForSeconds(2f);
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
