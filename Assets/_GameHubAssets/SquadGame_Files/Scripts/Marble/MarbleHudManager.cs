using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleHudManager : MonoBehaviour
{
    [SerializeField] Camera playerCam;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCam.transform.position;
        transform.rotation = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y, 0);
    }
}
