using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullSoundWhenClose : MonoBehaviour {

    [SerializeField] AudioClip gullSound;
    [SerializeField] GameObject player;

    bool hasPlayed = false;
	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckDistance", 0f, 1f);
	}
	
	void CheckDistance()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 1 && !hasPlayed){
            gameObject.GetComponent<AudioSource>().PlayOneShot(gullSound);
            hasPlayed = true;
        }
    }
}
