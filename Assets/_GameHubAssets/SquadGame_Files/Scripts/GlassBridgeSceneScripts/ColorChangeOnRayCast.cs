using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ColorChangeOnRayCast : MonoBehaviour
{
    private GameObject glass;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit.GetComponent<BoxCollider>() != null && objectHit.GetComponent<BoxCollider>().enabled == true && objectHit.GetComponent<GlassScript>() != null)
            {
                if ( objectHit.GetComponent<GlassScript>().enabled)
                {
                    if (glass == null)
                    {
                        glass = hit.collider.gameObject;
                        glass.GetComponent<GlassScript>().colliderHit = true;
                        glass.GetComponent<GlassScript>().ChangeColor();
                    }
                }
            }
        }
        else
        {
            glass.GetComponent<GlassScript>().colliderHit = false;
            glass.GetComponent<GlassScript>().ChangeColor();
            glass = null;
        }
    }
}
