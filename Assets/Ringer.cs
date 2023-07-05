using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringer : MonoBehaviour {

    [SerializeField]
    AudioClip source;
    public void HitByPlayer()
    {
        AudioSource player = gameObject.GetComponent<AudioSource>();
        player.PlayOneShot(source);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitByPlayer();
        }
    }
}
