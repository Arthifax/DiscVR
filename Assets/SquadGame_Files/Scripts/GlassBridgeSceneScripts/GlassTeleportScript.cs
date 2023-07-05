using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GlassTeleportScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool clicked = false;
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
                        GameObject objectHit = hit.transform.gameObject;
                        if (objectHit.GetComponent<BoxCollider>() != null && objectHit.GetComponent<BoxCollider>().enabled == true && objectHit.GetComponent<BoxCollider>().isTrigger == false)
                        {
                            player.transform.position = new Vector3(objectHit.transform.position.x, player.transform.position.y, objectHit.transform.position.z);
                            GlassScript glassScript = hit.transform.gameObject.GetComponent<GlassScript>();
                            if (glassScript != null)
                            {
                                glassScript.GlassBreaks();
                                glassScript.disableColliders();
                            }
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
