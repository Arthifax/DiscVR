using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        StartCoroutine(SwitchMusic());
    }

IEnumerator SwitchMusic()
    {
        yield return new WaitForSeconds(musicSource.clip.length+5f);
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();

    }
}
