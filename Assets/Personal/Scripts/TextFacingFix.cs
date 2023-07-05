using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFacingFix : MonoBehaviour
{
    public GameObject objectToLookAt;

	void Start ()
    {
        this.transform.LookAt(objectToLookAt.transform);
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 180, 0);
	}
	
}
