using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform location;
    public GameObject objectToTeleport;

    public void TeleportObject(Transform aPoint)
    {
        objectToTeleport.transform.position = aPoint.position;
        objectToTeleport.transform.rotation = aPoint.rotation;
    }
}
