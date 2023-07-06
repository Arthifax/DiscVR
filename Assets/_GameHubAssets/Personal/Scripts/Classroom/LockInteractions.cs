using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LockInteractions : MonoBehaviour
{
    [SerializeField] private LaCasaLockerScript lockerScript;
    [SerializeField] private LaCasaLockerScriptTwo lockerScriptTwo;
    [SerializeField] private LaCasaHandScript handScript;
    [SerializeField] private PuzzleManager pzlManager;
    private bool pressed = false;
    [SerializeField] private bool RightController;

    // Update is called once per frame
    private void Update()
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
        bool triggerButtonValue = false;
        if (Controller.Count > 0)
        {
            InputDevice device = Controller[0];
            RaycastHit hit;

            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!pressed)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        //Unzoom lock if  anything else is pressed
                        if (lockerScript != null)
                        {
                            if (!hit.transform.name.Contains("Arrow") && !hit.transform.name.Contains("Lock"))
                            {
                                lockerScript.NotLockFocus();
                            }
                        }

                        if (lockerScriptTwo != null)
                        {
                            if (!hit.transform.name.Contains("Arrow") && !hit.transform.name.Contains("Lock"))
                            {
                                lockerScriptTwo.NotLockFocus();
                            }
                        }

                        // Rotate locker numbers when clicking an arrow4
                        if (hit.transform.name.Contains("Arrow"))
                        {
                            lockerScript.RotateNum(hit.transform.name);
                        }
                        if (hit.transform.name.Contains("Arrow"))
                        {
                            lockerScriptTwo.RotateNum(hit.transform.name);
                        }

                        if (hit.transform.GetComponent<LaCasaLockerScript>() != null)
                        {
                            lockerScript.ZoomLock();
                        }

                        if (hit.transform.GetComponent<LaCasaLockerScriptTwo>() != null)
                        {
                            lockerScriptTwo.ZoomLock();
                        }

                        if (!hit.transform.name.Contains("Arrow") && !hit.transform.name.Contains("Lock") && !hit.transform.GetComponent<LaCasaLockerScript>())
                        {
                            if (lockerScript != null)
                                lockerScript.NotLockFocus();
                        }

                        if (hit.transform.name.Contains("Arrow"))
                        {
                            lockerScript.RotateNum(hit.transform.name);
                        }

                        if (!hit.transform.name.Contains("Arrow") && !hit.transform.name.Contains("Lock") && !hit.transform.GetComponent<LaCasaLockerScriptTwo>())
                        {
                            if (lockerScriptTwo != null)
                                lockerScriptTwo.NotLockFocus();
                        }

                        if (hit.transform.name.Contains("Arrow"))
                        {
                            lockerScriptTwo.RotateNum(hit.transform.name);
                        }
                        pressed = true;
                    }
                }
            }
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
            {
                pressed = false;
            }
        }
    }
}