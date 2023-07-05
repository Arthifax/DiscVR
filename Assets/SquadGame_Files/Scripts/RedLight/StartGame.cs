using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CountDownClock countDownClock;
    [SerializeField] private GameObject startRow;
    [SerializeField] private List<NPCMoveAndFall> moveAndFalls;

    public void StartGameLogic()
    {
        startRow.SetActive(true);
        MoveNpc();
        countDownClock.StartClock();
    }
    private void MoveNpc()
    {
        foreach (NPCMoveAndFall moveAndFall in moveAndFalls)
        {
            moveAndFall.NextPosition();
        }
    }
}
