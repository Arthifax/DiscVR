using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MarbleInteraction : MonoBehaviour
{
    [SerializeField] MarblePlayerManager player;
    [SerializeField] GameObject inventoryPos;
    [SerializeField] GameObject holdingPos;
    [SerializeField] GameObject marbleParent;
    [SerializeField] GameObject squareIcon;
    [SerializeField] public string marbleShape;

    public string getMarbleShape
    {
        get{ return marbleShape; }
    }

    public void GrabMarble()
    {
        if (player.holdingMarble == false)
        {
            transform.parent = holdingPos.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = transform.localScale * 1.2f;
            player.holdingMarble = true;
            player.currentMarbleShape = marbleShape;
            squareIcon.SetActive(true);
        }
        else if (player.holdingMarble == true && transform.parent == holdingPos.transform && marbleShape == player.currentMarbleShape)
        {
            transform.parent = marbleParent.transform;
            player.holdingMarble = false;
            transform.position = inventoryPos.transform.position;
            transform.localScale = transform.localScale * 0.8f;
            squareIcon.SetActive(false);
        }
    }
}
