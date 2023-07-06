using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTeleport : MonoBehaviour
{
    [SerializeField] private CountDownClock countDownClock;
    [SerializeField] private GameObject finalTeleportRow;
    [SerializeField] private LevelLoaderRedLight levelLoader;
    [SerializeField] private List<NPCMoveAndFall> moveAndFalls;
    [Space]
    [SerializeField] private GatherPoints gatherPoints;
    
    public void disableLastTeleports()
    {
        MoveNpc();
        countDownClock.PauseClock();
        finalTeleportRow.SetActive(false);
        gatherPoints.completedLevel = true;
        levelLoader.LoadNextLevel();
    }
    private void MoveNpc()
    {
        foreach (NPCMoveAndFall moveAndFall in moveAndFalls)
        {
            moveAndFall.NextPosition();
        }
    }
}
