using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour {

    public GameObject hours;
    public GameObject minutes;
    public GameObject seconds;

    int registeredMinutes;
	// Use this for initialization
	void Start () {
        hours.transform.localRotation = Quaternion.Euler(180, 0, System.DateTime.Now.Hour * -30 + System.DateTime.Now.Minute / -2);
        minutes.transform.localRotation = Quaternion.Euler(180, 0, System.DateTime.Now.Minute * -6);
        registeredMinutes = System.DateTime.Now.Minute;
    }
	
	// Update is called once per frame
	void Update () {
        seconds.transform.localRotation = Quaternion.Euler(180, 0, System.DateTime.Now.Second * -6);
        if (System.DateTime.Now.Minute != registeredMinutes)
        {
            hours.transform.localRotation = Quaternion.Euler(180, 0, System.DateTime.Now.Hour * -30 + System.DateTime.Now.Minute / -2);
            minutes.transform.localRotation = Quaternion.Euler(180, 0, System.DateTime.Now.Minute * -6);
            registeredMinutes = System.DateTime.Now.Minute;
        }        
    }
}
