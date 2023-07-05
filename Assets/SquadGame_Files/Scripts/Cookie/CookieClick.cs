using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CookieClick : MonoBehaviour
{
    private bool clicked = false;
    private bool zoomed = false;
    private GameObject zoomedCookie;
    public void ClearCookieComplete()
    {
        zoomedCookie = null;
        zoomed = false;
    }

    void Update()
    {
        RaycastHit hit;

        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            InputDevice device = leftHandDevices[0];
            bool triggerButtonValue = false;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!clicked)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        if (hit.collider.gameObject.name.Contains("Cookie") && zoomed)
                        {
                            return;
                        }
                        if (!hit.collider.gameObject.name.Contains("Cookie") && zoomed)
                        {
                            zoomedCookie.GetComponent<CookieController>().ReturnCookie();
                            zoomedCookie = null;
                            zoomed = false;
                        }
                        if (hit.collider.gameObject.name.Contains("Cookie") && !zoomed)
                        {
                            zoomedCookie = hit.collider.gameObject;
                            zoomedCookie.GetComponent<CookieController>().ZoomCookie();
                            zoomed = true;
                        }
                    }
                    else
                    {
                        if (zoomed)
                        {
                            zoomedCookie.GetComponent<CookieController>().ReturnCookie();
                            zoomed = false;
                            zoomedCookie = null;
                        }
                    }
                    clicked = true;
                }
            }

            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
            {
                clicked = false;
            }
        }
    }
}
