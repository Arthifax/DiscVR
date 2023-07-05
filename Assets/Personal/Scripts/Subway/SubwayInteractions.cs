using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class SubwayInteractions : MonoBehaviour
{
    private bool tryAgainDelay;
    private GameObject HighlightedObject = null;
    [SerializeField] private SubwayManager subwayManager;
    [SerializeField] private GameOverBtns gameOverBtns;
    private bool clicked;
    private float timeHeld = 0f;
    private float timeHeldSecondary = 0f;
    private float timeToHoldForExitUI = 3f;
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject passOverMenu;
    private bool menuButtonValue = false;
    private bool secondaryButtonValue = false;
    private bool passOverMenuEnabled = false;
    [SerializeField] private bool RightController;

    private void Update()
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
            InputDevice device = Controller[0];
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                bool triggerButtonValue = false;
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
                {
                    if (!clicked)
                    {
                        if (hit.transform.name.Equals("ExitGame(ENDUI)"))
                        {
                            Application.Quit();
                        }

                        else if (hit.transform.name.Equals("TryAgain(ENDUI)"))
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }

                        if (hit.transform.GetComponent<SubwayHall>() != null && !tryAgainDelay)
                        {
                            subwayManager.ToNextSubway(hit.transform.GetComponent<SubwayHall>().correct);
                        }
                    }
                    if (hit.transform.name.Contains("(ENDUI)"))
                    {
                        if (hit.transform.parent != null)
                        {
                            hit.transform.GetComponentInParent<GameOverBtns>().SetToHighlighted(hit.transform.gameObject);
                        }
                    }
                    else if (!hit.transform.name.Contains("(ENDUI)"))
                    {
                        gameOverBtns.DeselectBoth();
                    }
                    clicked = true;
                }
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
                {
                    clicked = false;
                }
                ChangeHighlightedObject(hit.transform.gameObject);
            }
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && menuButtonValue)
            {
                timeHeld += Time.deltaTime;
            }
            if(device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue)&& secondaryButtonValue)
            {
                timeHeldSecondary += Time.deltaTime;
            }

            if (device.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && !menuButtonValue)
            {
                EnableUI(timeHeld);
                timeHeld = 0;
            }
            if(device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && !secondaryButtonValue)
            {
                EnableUI(timeHeldSecondary);
                timeHeldSecondary = 0;
            }
        }
    }
    private void ChangeHighlightedObject(GameObject ObjToReplaceWith)
    {
        if (HighlightedObject != null && HighlightedObject.name.Contains("SubwayImg"))
        {
            HighlightedObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }

        if (ObjToReplaceWith.name.Contains("SubwayImg"))
        {
            ObjToReplaceWith.GetComponent<Image>().color = new Color32(255, 255, 255, 60);
        }
        HighlightedObject = ObjToReplaceWith;
    }
    private void EnableUI(float time)
    {
        if (time >= timeToHoldForExitUI)
        {
            passOverMenu.SetActive(false);
            escapeMenu.GetComponent<Canvas>().enabled = true;
        }
    }
        IEnumerator TryAgainDelay()
    {
        tryAgainDelay = true;
        yield return new WaitForSeconds(1f);
        tryAgainDelay = false;

    }
    public void PassOverDisabled()
    {
        passOverMenuEnabled = false;
    }
    public void PassOverChange()
    {
        passOverMenu.SetActive(passOverMenuEnabled);
    }
}