using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScriptRestaurant : MonoBehaviour
{
    public string levelToLoadName;
    public bool canGoNext = true;
    public AudioClip openSfx;
    public AudioClip lockedSfx;

    public void HitByPlayer()
    {
        if (!GetComponent<AudioSource>().isPlaying && Camera.main.GetComponent<NextLevelScript>().canGoNext == true)
        {
            if (canGoNext)//only for level select, to disable some
            {
                GetComponent<AudioSource>().PlayOneShot(openSfx);
                Camera.main.GetComponent<NextLevelScript>().NextLevel(levelToLoadName);
                GetComponent<Animation>().Play();
            }
            else
                GetComponent<AudioSource>().PlayOneShot(lockedSfx);
        }
    }
}
