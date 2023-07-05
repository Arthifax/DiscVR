using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToSwitchTo;
    [SerializeField] private Screen_Fade screenFade;

    public void SwitchScene()
    {
        StartCoroutine(SwitchSceneWithFade());
    }

    public void SwitchSceneDelayed(float fadeTimer)
    {
        StartCoroutine(SwitchSceneWithFadeDelayed(fadeTimer));
    }

    IEnumerator SwitchSceneWithFade()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneToSwitchTo);
    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneWithFade());
    }

    IEnumerator ReloadSceneWithFade()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator SwitchSceneWithFadeDelayed(float timer)
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(sceneToSwitchTo);
    }
}
