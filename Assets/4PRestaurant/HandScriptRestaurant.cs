using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class HandScriptRestaurant : MonoBehaviour
{
    private float timeHeld = 0f;
    private float timeHeldSecondary = 0f;
    private float timeToHoldForExitUI = 3f;

    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private bool RightController;
    [SerializeField] private string exitToLevelName;

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
            bool buttonValue = false;
            bool secondaryButtonValue = false;
            InputDevice device = Controller[0];
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out buttonValue) && buttonValue)
            {
                timeHeld += Time.deltaTime;
            }
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
            {
                timeHeldSecondary += Time.deltaTime;
            }
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out buttonValue) && !buttonValue)
            {
                EnableUI(timeHeld);
                timeHeld = 0;
            }
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && !secondaryButtonValue)
            {
                EnableUI(timeHeldSecondary);
                timeHeldSecondary = 0;
            }
        }
    }
    private void EnableUI(float time)
    {
        if (time >= timeToHoldForExitUI && escapeMenu != null)
        {
            escapeMenu.GetComponent<Canvas>().enabled = true;
        }
        else if (time > 0.1f && time < timeToHoldForExitUI && !escapeMenu.GetComponent<Canvas>().enabled)
        {
            Camera.main.GetComponent<NextLevelScript>().NextLevel(exitToLevelName);
        }
    }
}
