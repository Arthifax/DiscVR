using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class RaycastMovement : MonoBehaviour
{
    private bool cardSelected = false;
    public void CardisSelected()
    {
        cardSelected = !cardSelected;
    }
    public bool movingOnMap;
    private GameObject heldMapItem;
    private MapItem heldMapItemScript;
    public RaycastHit raycastHit;
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
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                raycastHit = hit;
                if (hit.collider.CompareTag("MapInteractable"))
                {
                    if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
                    {
                        if (hit.collider.gameObject.GetComponent<MapItem>() != null && heldMapItem == null)
                        {
                            heldMapItem = hit.collider.gameObject;
                            heldMapItemScript = hit.collider.gameObject.GetComponent<MapItem>();
                            heldMapItemScript.StartFollow(this);
                        }
                    }
                    if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
                    {
                        if (heldMapItem != null)
                        {
                            heldMapItemScript.StopFollow();
                            heldMapItem = null;
                            heldMapItemScript = null;
                        }
                    }
                }
                else
                {
                    if (heldMapItem != null)
                    {
                        heldMapItemScript.StopFollow();
                        heldMapItem = null;
                        heldMapItemScript = null;
                    }
                }
            }
        }
    }
}