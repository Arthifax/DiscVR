using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using MText;

public class TeleportAndBedPickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inventory;
    private bool clicked = false;
    private bool allowTeleport = true;
    private bool allowPickup = true;
    public string tagName = "Teleporter";
    public void AllowTeleport()
    {
        allowTeleport = false;
    }

    void Update()
    {
        RaycastHit hit;
        //if (Input.GetMouseButtonDown(0))
        //    if (!clicked)
        //    {
        //        if (Physics.Raycast(transform.position, transform.forward, out hit))
        //        {
        //            Debug.Log("hit");

        //            if (hit.collider.gameObject.GetComponent<BedController>())
        //            {
        //                hit.collider.gameObject.GetComponent<BedController>().CheckInventory(inventory);
        //            }
        //            if (hit.collider.gameObject.name == "CenterBed" || hit.collider.gameObject.name == "LeftBed" || hit.collider.gameObject.name == "RightBed")
        //            {
        //                inventory.GetComponent<Inventory>().PlaceText(hit.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<Modular3DText>());
        //                inventory.GetComponent<Inventory>().PlaceBed(hit.collider.gameObject.transform.parent.transform);
        //            }
        //        }
        //        clicked = true;
        //    }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    clicked = false;
        //}

        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            InputDevice device = leftHandDevices[0];
            bool triggerButtonValue = false;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!clicked)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        if (allowTeleport && hit.collider.isTrigger && hit.transform.tag == tagName)
                        {
                            allowPickup = false;
                            GameObject objectHit = hit.transform.gameObject;
                            player.transform.position = new Vector3(objectHit.transform.position.x, player.transform.position.y, objectHit.transform.position.z);
                        }
                        if (allowPickup&&hit.collider.gameObject.GetComponent<BedController>())
                        {
                            allowTeleport = false;
                            hit.collider.gameObject.GetComponent<BedController>().CheckInventory(inventory);
                        }

                    }
                    allowPickup = true;
                    allowTeleport = true;
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
