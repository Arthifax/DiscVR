using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FlashLightHit : MonoBehaviour
{
    private bool clicked = false;
    private float timeHeld = 0f;
    private float timeToHoldForExitUI = 3f;
    [SerializeField] private bool RightController;
    // Update is called once per frame
    void Update()
    {

        var Controller = new List<InputDevice>();
        if (RightController)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, Controller);
        }
        else
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, Controller);
        }

        if (Controller.Count > 0)
        {
            RaycastHit hit;
            bool triggerButtonValue = false;
            InputDevice device = Controller[0];
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!clicked)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        hit.transform.gameObject.GetComponent<FlashlightScript>().HitByPlayer(this.transform);
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
