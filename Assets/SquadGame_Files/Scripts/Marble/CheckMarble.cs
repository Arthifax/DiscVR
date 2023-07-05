using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMarble : MonoBehaviour
{
    [SerializeField] MarblePlayerManager player;
    [SerializeField] GatherPoints pointsManager;
    [SerializeField] GameObject marbleInWorld;
    [SerializeField] GameObject thisCheckerCube;
    [SerializeField] GameObject inventorySlot;
    [SerializeField] string requiredShape;
    [SerializeField] GameObject requiredPlacedTube;
    [SerializeField] GameObject tubeAnimation;
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int wrongPenaltyAmount;

    public void CheckMarbleShape()
    {
        if (player.currentMarbleShape == requiredShape && player.placedObjects.Contains(requiredPlacedTube))
        {
            Destroy(marbleInWorld);
            Destroy(inventorySlot);
            pointsManager.opdrachten[0] = true;
            pointsManager.questCheck = true;
            player.holdingMarble = false;
            tubeAnimation.SetActive(true);
            Destroy(thisCheckerCube);
        }
        else 
        {
            pointManager.AddPoints(-wrongPenaltyAmount);
        }
    }
}
