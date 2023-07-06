using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubwayManager : MonoBehaviour
{

    private bool isPressable;

    [SerializeField]
    private TextMeshProUGUI Counter;
    public int maxLevelcount;
    public int punishCount;
    public int rewardCount;
    private int currentLevelcount;
    private int currentCount;

    [SerializeField] private Screen_Fade fade;

    private bool fadingOut = false;
    private bool horn = false;

    [SerializeField] private AudioSource stressSound;

    [SerializeField] private AudioClip hornSound;
    [SerializeField] private AudioSource source;

    void Start()
    {
        isPressable = true;

        Counter.text = maxLevelcount.ToString();
        currentLevelcount = maxLevelcount;
        currentCount = currentLevelcount;
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    public void StartLevel()
    {
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {

        if (currentCount <= 0)
        {
            Counter.text = "0";
            currentCount = 0;
            StartCoroutine(GameOver());
            StartCoroutine(FadeToWhite());
        }

        yield return new WaitForSeconds(1);
        currentCount--;
        if (currentCount > 0)
        {
            Counter.text = currentCount.ToString();
            CountDownRecursion();
        }
        else
        {
            Counter.text = "0";
            currentCount = 0;
            StartCoroutine(GameOver());
            StartCoroutine(FadeToWhite());
        }

        if (currentCount <= 6 && currentCount >= 4) TrainWarning();
        if (currentCount.Equals(2)) TrainComing();
        if (currentCount.Equals(1)) StartCoroutine(FadeToWhite());
    }

    //Recursion pls
    private void CountDownRecursion()
    {
        StartCoroutine(CountDown());
    }

    public void ToNextSubway(bool isCorrect)
    {
        if (!isPressable) return;

        fadingOut = true;

        StartCoroutine(SwitchHall(isCorrect));
    }

    IEnumerator SwitchHall(bool isCorrect)
    {
        isPressable = false;
        if (!isCorrect && currentLevelcount <= punishCount)
        {
            Counter.text = "0";
            currentCount = 0;
            StartCoroutine(GameOver());
            StartCoroutine(FadeToWhite());
            yield break;
        }

        fade.FadeOut(); 
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2f);
        currentCount = currentLevelcount;
        Counter.text = currentCount.ToString();
        //Play running sound

 
        if (!isCorrect) currentLevelcount -= punishCount;
        else if (currentLevelcount < maxLevelcount) currentLevelcount += rewardCount;
        FindObjectOfType<SubwayLevelManager>().NextLevel();
        fade.FadeIn();
        yield return new WaitForSeconds(2.2f);
        foreach (HallTrain train in FindObjectsOfType<HallTrain>())
        {
            train.transform.GetComponent<Animator>().SetBool("Riding", false);
            train.transform.GetComponent<Animator>().SetBool("Lights", false);
            train.transform.GetChild(0).gameObject.SetActive(false);
            train.transform.GetComponent<AudioSource>().Stop();
        }

        stressSound.transform.GetComponent<Animator>().SetBool("stresssoundVol", false);
        stressSound.Stop();

        if (currentLevelcount > 0)
        {
            //  ovrFade.OnLevelFinishedLoading();
            //  yield return new WaitForSeconds(ovrFade.fadeTime);
            GetComponent<AudioSource>().Stop();
            isPressable = true;
            fadingOut = false;
        }
    }

    public void TrainWarning()
    {
        //Debug.Log("10S");
        foreach (HallTrain train in FindObjectsOfType<HallTrain>())
        {
            if (train.trainHall)
            {
                if (train.transform.parent.GetComponent<AudioSource>().isPlaying) return;

                train.transform.parent.GetComponent<AudioSource>().volume = .2f;
                train.transform.parent.GetComponent<AudioSource>().Play();

                train.transform.GetComponent<AudioSource>().volume = .5f;
                train.transform.GetComponent<AudioSource>().Play();
                //Doe iets met lighting
                train.transform.GetComponent<Animator>().SetBool("Lights", true);
                train.transform.GetChild(0).gameObject.SetActive(true);

                if (!stressSound.isPlaying)
                {
                    stressSound.transform.GetComponent<Animator>().SetBool("stresssoundVol", true);
                    stressSound.Play();
                }
            }
        }
    }

    public void TrainComing()
    {
        //Debug.Log("5S");
        foreach (HallTrain train in FindObjectsOfType<HallTrain>())
        {
            if (train.trainHall)
            {
                horn = true;
                PlaySound(hornSound, 0.8f);
                train.transform.GetComponent<Animator>().SetBool("Riding", true);
                //train.transform.GetComponent<AudioSource>().Play();
            }
        }

    }

    private IEnumerator FadeToWhite()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (fadingOut)
        {
            Debug.Log("FADING BROKEN");
            yield break;
        }

        StartCoroutine(fadeDelay());
    }

    IEnumerator fadeDelay()
    {
        fadingOut = true;
        yield return new WaitForSeconds(1.5f);

        //ovrFade.fadeColor = new Color32(255, 255, 255, 0); //pure white
        // ovrFade.fadeColor = new Color32(180, 180, 175, 0); //grey-ish  //180,180,175 
        // ovrFade.fadeTime = 1;

        // ovrFade.FadeOut();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        if (currentCount <= 0)
        {
            FindObjectOfType<SceneSwitchFinal>().SetToLose();
        }
        else
        {
            CountDownRecursion();
        }
    }

    private void Win()
    {
        FindObjectOfType<SceneSwitchFinal>().SetToWin();
    }
}
