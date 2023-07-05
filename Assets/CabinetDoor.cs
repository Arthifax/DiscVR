using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{

    [SerializeField]
    Transform pivot;

    // Smoothly open a door
    public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster

    bool open = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start()
    {
        defaultRotationAngle = pivot.localEulerAngles.y;
        currentRotationAngle = pivot.localEulerAngles.y;
    }

    // Main function
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), pivot.localEulerAngles.z);
    }

    public void HitByPlayer()
    {
        open = !open;
        currentRotationAngle = pivot.localEulerAngles.y;
        openTime = 0;
    }
}