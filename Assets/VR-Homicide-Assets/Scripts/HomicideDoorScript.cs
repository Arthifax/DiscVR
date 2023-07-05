using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomicideDoorScript : MonoBehaviour
{
    public string levelToLoadName;
    public bool canGoNext = true;
    public AudioClip openSfx;
    public AudioClip lockedSfx;

    //private new MeshRenderer renderer;
    //private LockerScript lockerScript;
    //public Transform doorlock;

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

    //private void Awake()
    //{
    //    renderer = GetComponent<MeshRenderer>();
    //    lockerScript = doorlock.GetComponent<LockerScript>();
    //}

    //private void SetReady(Material mat)
    //{
    //    renderer.material = mat;
    //    tag = "interactable";
    //    lockerScript.LockCorrect();
    //    canGoNext = false;
    //}
}
