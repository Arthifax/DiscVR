using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR;

public class HandScript : MonoBehaviour
{
    public GameObject player;
    private bool menuClicked = false;
    private bool clicked = false;
    private float timeHeld = 0f;
    private float timeHeldSecondary = 0f;
    private float timeToHoldForExitUI = 3f;
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private bool RightController;
    [SerializeField] private string exitToLevelName;
    public string tagName = "Teleporter";
    void Update()
    {
        RaycastHit hit;
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
            bool menuButtonValue = false;
            bool triggerButtonValue = false;
            bool secondaryValue = false;
            InputDevice device = Controller[0];
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && menuButtonValue)
            {
                timeHeld += Time.deltaTime;
            }
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue) && secondaryValue)
            {
                timeHeldSecondary += Time.deltaTime;
            }
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!clicked)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        if (hit.collider.isTrigger && hit.transform.tag == tagName)
                        {
                            player.transform.position = new Vector3(hit.transform.position.x, player.transform.position.y, hit.transform.position.z);
                        }
                    }
                    clicked = true;
                }
            }
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
            {
                clicked = false;
            }

            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && !menuButtonValue)
            {
                EnableUI(timeHeld);
                timeHeld = 0;
            }

            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue) && !secondaryValue)
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
        else if (time > 0.1f && time < timeToHoldForExitUI)
        {
            Camera.main.GetComponent<NextLevelScript>().NextLevel(exitToLevelName);
        }
    }
}
