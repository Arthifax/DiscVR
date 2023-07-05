using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    [SerializeField] private Transform resetTransform;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        ResetPosition();
    }

    public void ResetPosition()
    {
        float rotationAngleY = resetTransform.rotation.eulerAngles.y - playerCamera.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);
    }
}
