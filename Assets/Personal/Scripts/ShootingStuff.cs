using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShootingStuff : MonoBehaviour
{
    [SerializeField] private UnityEvent onBaddiesShot;
    [SerializeField] private UnityEvent onWrongShot;
    public int peopleToShoot;
    private int peopleShot;
    private bool complete;

    public AudioSource music;

    [SerializeField] private AudioClip wrong= null;
   // [SerializeField] private AudioClip victory = null;
    [SerializeField] private AudioSource source = null;
    public void Subscribe(GunController gunController)
    {
        onBaddiesShot.AddListener(gunController.DropGun);
    }

    public void ShotWrong()
    {
        if(!complete) StartCoroutine(ReloadScene());
    }

    public void ShotRight()
    {
        peopleShot++;
        if(peopleShot == peopleToShoot)
        {
            // NEXT SEGMENT
            complete = true;
            onBaddiesShot.Invoke();
            music.Stop();
            //Debug.Log("bad guys shot");
        }
    }

    IEnumerator ReloadScene()
    {
        PlaySound(wrong, .99f);
       
        Debug.Log("good guy shot");
        onWrongShot.Invoke();
        yield return new WaitForSeconds(1f);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
    private void OnDestroy()
    {
        onBaddiesShot.RemoveAllListeners();
        onWrongShot.RemoveAllListeners();
        onWrongShot = null;
        onBaddiesShot = null;
    }
}
