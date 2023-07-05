using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWithEachother : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(9, 9);
	}
	
}
