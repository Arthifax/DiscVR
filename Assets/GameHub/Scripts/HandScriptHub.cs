using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class HandScriptHub : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        if (leftHandDevices.Count == 1)
        {
            bool buttonValue = false;
            InputDevice device = leftHandDevices[0];
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out buttonValue) && buttonValue || Input.GetKeyDown(KeyCode.I))
            {

            }
        }
        //else
        //{
        //    Debug.Log("2 left hands?");
        //}
    }

}
