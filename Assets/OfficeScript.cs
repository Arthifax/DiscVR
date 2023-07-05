using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeScript : MonoBehaviour {
    bool mainLightOn = true;
    public GameObject chandelier;
    public GameObject mainlight;
    public GameObject flashlightLight;
    public GameObject flashlight;

    public GameObject whenLightsOn;
    public GameObject whenLightsOff;

    public Material[] chandelierMats;

    public AudioClip switchSfx;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void HitByPlayer()
    {
        GetComponent<AudioSource>().PlayOneShot(switchSfx);
        if (mainLightOn)//was on, now OFF
        {
            mainLightOn = false;
            chandelier.GetComponent<MeshRenderer>().material = chandelierMats[1];
            flashlightLight.SetActive(true);
            mainlight.SetActive(false);

            whenLightsOff.SetActive(true);
            whenLightsOn.SetActive(false);
        }
        else
        {
            mainLightOn = true;
            chandelier.GetComponent<MeshRenderer>().material = chandelierMats[0];
            flashlightLight.SetActive(false);
            mainlight.SetActive(true);

            whenLightsOff.SetActive(false);
            whenLightsOn.SetActive(true);
        }
    }
}
