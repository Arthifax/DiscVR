using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDoorClicked : MonoBehaviour
{
    [SerializeField] private LaCasaLockerScript script;
    [SerializeField] private GameObject locker;
    [SerializeField] private Animation animation;
    [SerializeField] private string name;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 teleportLocationLock1 = new Vector3(-4.926f, 10.314f, 0.33f);

    public void IsClicked()
    {
        if (player.transform.position == teleportLocationLock1)
        {
            script.IsOpenable(locker, animation, name);
        }
    }
}
