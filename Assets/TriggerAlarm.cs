using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAlarm : MonoBehaviour {

    [SerializeField]
    AudioClip alarm;

    AudioSource source;
	void Awake () {
        source = gameObject.GetComponent<AudioSource>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            HitByPlayer();
        }
    }

    public void HitByPlayer()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(alarm);
        }
    }
}
