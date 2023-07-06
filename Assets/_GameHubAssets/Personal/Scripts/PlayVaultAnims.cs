using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVaultAnims : MonoBehaviour {

    public Animator animDoor;

	void Start () {
        animDoor = this.GetComponent<Animator>();
	}
	
	void Update () {
		
        if (Input.GetButtonDown("Jump"))
        {
            animDoor.SetBool("Open-Close", true);
            //animDoor.Play("VaultAnim");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animDoor.SetBool("Open-Close", false);
        }
	}
}
