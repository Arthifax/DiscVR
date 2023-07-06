using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class CheckNumberTube : MonoBehaviour
{
    [SerializeField] MarblePlayerManager player;
    [SerializeField] GatherPoints pointsManager;
    [SerializeField] GameObject tubeInWorld;
    [SerializeField] GameObject prePlacedTube;
    [SerializeField] GameObject thisCheckerCube;
    [SerializeField] GameObject tubeIcon;
    [SerializeField] GameObject qMarkIcon;
    [SerializeField] int requiredNumber;
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int wrongPenaltyAmount;

    public void CheckNumber()
    {
        if(player.currentTubeNumber == requiredNumber)
        {
            Destroy(tubeInWorld);
            Destroy(qMarkIcon);
            prePlacedTube.gameObject.SetActive(true);
            pointsManager.opdrachten[0] = true;
            pointsManager.questCheck = true;
            player.holdingTube = false;
            player.placedObjects.Add(prePlacedTube);
            tubeIcon.SetActive(true);
            Destroy(thisCheckerCube);
        }
        else 
        {
            pointManager.AddPoints(-wrongPenaltyAmount);
        }
    }
}
