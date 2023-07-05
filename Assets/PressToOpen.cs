using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToOpen : MonoBehaviour {

    [SerializeField]
    GameObject door;

    public void HitByPlayer()
    {
        door.GetComponent<DoorHandler>().buttonPressed();
    }

    private void OnMouseDown()
    {
        door.GetComponent<DoorHandler>().buttonPressed();
    }
}
