using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithTestamentManager : MonoBehaviour {

    [SerializeField] private float timeToWaitBeforeInvoking;
    [SerializeField] private GameObject[] objectsToEnable;

	void Start () {
        Invoke("WaitToEnable", timeToWaitBeforeInvoking);
	}

    private void WaitToEnable()
    {
        foreach(GameObject g in objectsToEnable)
        {
            if (!g.activeInHierarchy)
            {
                g.SetActive(true);
            }
        }
    }
}
