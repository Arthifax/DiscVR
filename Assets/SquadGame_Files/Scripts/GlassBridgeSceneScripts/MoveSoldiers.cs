using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSoldiers : MonoBehaviour
{
    public Vector3 newPosition;

    public void MoveSoldier()
    {
        transform.localPosition = newPosition;
    }
}
