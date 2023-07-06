using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTeleportAbility : MonoBehaviour
{
    [SerializeField] private TeleportScript teleport;

    public void DisableTeleport()
    {
        StartCoroutine(StopTeleports());
    }
    private IEnumerator StopTeleports()
    {
        yield return new WaitForSeconds(.4f);
        teleport.AllowTeleport();
    }

}
