using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTPOverride : MonoBehaviour {
    
    [SerializeField]
    private GameObject player;

    public void Awake()
    {
        //player.GetComponent<HandScript>().teleportHeight = new Vector3(0, 1.83f, 0);
    }
}
