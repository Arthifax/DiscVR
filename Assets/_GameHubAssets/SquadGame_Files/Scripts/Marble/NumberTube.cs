using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NumberTube : MonoBehaviour
{
    [SerializeField] MarblePlayerManager player;
    [SerializeField] GameObject inventoryPos;
    [SerializeField] GameObject originalPos;
    [SerializeField] GameObject tubeIcon;
    [SerializeField] TextMeshPro testText;
    [SerializeField] int tubeNum;

    public void GrabTube()
    {
        if (player.holdingTube == false)
        {
            transform.parent = inventoryPos.transform;
            transform.localScale = transform.localScale * 0.5f;
            transform.localPosition = Vector3.zero;
            player.holdingTube = true;
            player.currentTubeNumber = tubeNum;
            //testText.text = "Holding: " + tubeNum;
            tubeIcon.SetActive(false);
        } 
        else if (player.holdingTube == true && tubeNum == player.currentTubeNumber)
        {
            transform.localScale = transform.localScale * 2f;
            transform.parent = null;
            player.holdingTube = false;
            transform.position = originalPos.transform.position;
            //testText.text = "Holding: nothing";
            tubeIcon.SetActive(true);
        }
    }
}
