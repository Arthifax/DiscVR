using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool clicked = false;
    private bool allowTeleport = true;
    public string tagName = "Teleporter";
    public void AllowTeleport()
    {
        allowTeleport = false;
    }

    void Update()
    {
        RaycastHit hit;

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
                            GameObject objectHit = hit.transform.gameObject;
                            player.transform.position = new Vector3(objectHit.transform.position.x, player.transform.position.y, objectHit.transform.position.z);
                        }
                    }
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
