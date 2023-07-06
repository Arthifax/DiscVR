using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LaCasaHandScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 teleportHeight;
    private bool clicked = false;
    private float timeHeld = 0f;
    private float timeHeldSecondary = 0f;
    private float timeToHoldForExitUI = 3f;
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject passOverMenu;
    [SerializeField] private GunController gunController;
    private bool menuButtonValue = false;
    private bool secondaryButtonValue = false;
    private bool passOverMenuEnabled = false;
    [SerializeField] private bool RightController;
    public string tagName = "Teleporter";
    private void Update()
    {
        var Controller = new List<InputDevice>();
        RaycastHit hit;
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
            InputDevice device = Controller[0];
            bool triggerButtonValue = false;
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
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && menuButtonValue)
            {
                timeHeld += Time.deltaTime;
            }
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
            {
                timeHeldSecondary += Time.deltaTime;
            }

            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && !menuButtonValue)
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
        if (time >= timeToHoldForExitUI)
        {
            passOverMenuEnabled = passOverMenu.activeSelf;
            passOverMenu.SetActive(false);
            if (gunController != null)
            {
                gunController.enabled = false;
            }
            escapeMenu.GetComponent<Canvas>().enabled = true;
        }
    }

    public void PassOverChange()
    {
        if (gunController != null)
        {
            gunController.enabled = true;
        }
        passOverMenu.SetActive(passOverMenuEnabled);
    }
}