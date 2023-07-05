using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    void FixedUpdate()
    {
        float yRotation = cameraObject.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x, yRotation, transform.eulerAngles.x);
    }
}
