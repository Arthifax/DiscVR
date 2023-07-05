using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarbleTossArea : MonoBehaviour
{

    [SerializeField] MarbleShooter marbleShooter; //Make sure to place the marbleShooter of the correct marble shape
    [SerializeField] MarblePlayerManager player;
    [SerializeField] GatherPoints pointsManager;
    [SerializeField] GameObject marbleInHand;
    [SerializeField] GameObject inventorySlot;
    [SerializeField] GameObject thisGameArea;
    [SerializeField] string requiredShape;
    [SerializeField] public bool isInArea = false;
    [SerializeField] private bool gameCompleted = false;

    [SerializeField] TextMeshPro testText;
    string marbleInAreaShape;

    [SerializeField] private GameObject lightGreenAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Marble")
        {
            marbleInAreaShape = other.GetComponent<MarbleInteraction>().getMarbleShape;
            testText.text = "Waiting...";
            isInArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Marble")
        {
            testText.text = "Too bad!";
            isInArea = false;
        }
    }


    void Update()
    {
        if (isInArea && !marbleShooter.marbleIsMoving && marbleInAreaShape == requiredShape && gameCompleted == false)
        {
            testText.text = "You Win!";
            pointsManager.opdrachten[0] = true;
            pointsManager.questCheck = true;
            player.holdingMarble = false;
            lightGreenAnimation.SetActive(true);
            gameCompleted = true;
            Destroy(inventorySlot);
            Destroy(marbleInHand);
            Destroy(thisGameArea);
        }
    }
}
