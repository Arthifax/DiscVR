using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UIInteractionRaycasts : MonoBehaviour
{
    [SerializeField] private HandScript handScript;
    [SerializeField] private Vector3 teleportLocation = new Vector3(-2.559f, 10.314f, -1.308f);
    //[SerializeField] private   = Quaternion.identity;
    void Update()
    {
        if (handScript.player.transform.position == teleportLocation)
        {
            var leftHandDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
            bool triggerButtonValue = false;

            if (leftHandDevices.Count == 1)
            {
                InputDevice device = leftHandDevices[0];
                RaycastHit hit;
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {

                    }
                }
            }
        }
    }
}
