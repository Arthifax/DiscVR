using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class MapInteraction : MonoBehaviour
{
    private MapItem mapItem;
    private bool pressedDownOnMapItem = false;
    private GameObject heldMapItem;
    private float timerHeld = 0f;
    private float timerMax = 0.1f;

    void Update()
    {
        //var leftHandDevices = new List<InputDevice>();
        //InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        //bool triggerButtonValue = false;

        //if (leftHandDevices.Count == 1)
        //{
        //    InputDevice device = leftHandDevices[0];
        //    RaycastHit hit;
        //    if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
        //    {
        //        if (Physics.Raycast(transform.position, transform.forward, out hit))
        //        {

        //        }
        //    }
        //}
    }
}
