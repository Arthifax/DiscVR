using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportQuestion : MonoBehaviour
{
    [SerializeField] private GameObject previousTeleports;
    [SerializeField] private bool correctTeleport;
    [SerializeField] private List<NPCMoveAndFall> moveAndFalls;
    bool clicked = false;

    private void DisableOldTeleports()
    {
        Destroy(previousTeleports);
    }

    public void ChangeTeleports()
    {
        if (!clicked)
        {
            clicked = true;
            DisableOldTeleports();
            MoveNpc();
        }
    }
    private void MoveNpc()
    {
        foreach (NPCMoveAndFall moveAndFall in moveAndFalls)
        {
            moveAndFall.NextPosition();
        }
    }
}
