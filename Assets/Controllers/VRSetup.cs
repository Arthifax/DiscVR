using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> controllers;
    private List<InputDevice> devices = new List<InputDevice>();
    private List<InputDevice> leftHandDevice = new List<InputDevice>();
    private List<InputDevice> rightHandDevice = new List<InputDevice>();
    public string deviceName;

    void Start()
    {
        CheckDevices();
    }

    private void CheckDevices()
    {
        InputDevices.GetDevices(devices);
        deviceName = devices[0].name;
        CheckControllerToEnable();
    }

    private void CheckControllerToEnable()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevice);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevice);
        if (leftHandDevice.Count > 0)
        {
            controllers[0].SetActive(true);
            controllers[1].SetActive(false);
        }
        if(rightHandDevice.Count > 0)
        {
            controllers[0].SetActive(false);
            controllers[1].SetActive(true);
        }
        if(rightHandDevice.Count>0&&leftHandDevice.Count>0)
        {
            controllers[0].SetActive(true);
            controllers[1].SetActive(true);
        }
        else
        {
            controllers[0].SetActive(false);
            controllers[1].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckControllerToEnable();

    }
}
