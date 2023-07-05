using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchFinal : MonoBehaviour {

   // [SerializeField] private OVRScreenFade ovrFade;

    [SerializeField] private GameObject mainScene;
    [SerializeField] private GameObject LoseBox;
    [SerializeField] private GameObject WinBox;

    [SerializeField] private GameObject soundObj;


    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource source;

    public void SetToLose()
    {
   //     ovrFade.OnLevelFinishedLoading();
        StartCoroutine(LoseFadeWait());
    }

    public void SetToWin()
    {
    //    ovrFade.OnLevelFinishedLoading();
        StartCoroutine(WinFadeWait());
  //      ovrInput.tryAgainDelay = true;
    }

    IEnumerator LoseFadeWait()
    {
        mainScene.SetActive(false);
        LoseBox.SetActive(true);
        PlaySound(hitSound, 0.8f);
   //     yield return new WaitForSeconds(ovrFade.fadeTime);
   yield return null;
        soundObj.GetComponent<AudioSource>().Stop();
        soundObj.GetComponent<Animator>().SetBool("stresssoundVol", false);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    IEnumerator WinFadeWait()
    {
        mainScene.SetActive(false);
        WinBox.SetActive(true);
        //   yield return new WaitForSeconds(ovrFade.fadeTime);
        yield return null;
        StartCoroutine(TryAgainDelayTimer());
    }

    IEnumerator TryAgainDelayTimer()
    {
        yield return new WaitForSeconds(0.5f);
      //  ovrInput.tryAgainDelay = false;
    }
}
