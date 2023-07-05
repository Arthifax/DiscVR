using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBlimp : MonoBehaviour
{
    [SerializeField] GameObject winScene = null;
    [SerializeField] GameObject blimpParent = null;

    [SerializeField] public float rotationSpeed = 0.5f;
    float customSpeed = 0.0f;

    void FixedUpdate()
    {
        if (winScene.activeSelf)
        {
            if ((360 - blimpParent.transform.eulerAngles.y) % 360 >= 136 && (360 - blimpParent.transform.eulerAngles.y) % 360 <= 224)
            {
                if (rotationSpeed != -1)
                {
                    customSpeed = rotationSpeed;
                    rotationSpeed = -1;
                }
            }
            else
            {
                if (rotationSpeed == -1)
                {
                    rotationSpeed = customSpeed;
                }
            }
            blimpParent.transform.eulerAngles = new Vector3(blimpParent.transform.eulerAngles.x, blimpParent.transform.eulerAngles.y + rotationSpeed, blimpParent.transform.eulerAngles.z);
        }
    }
}
