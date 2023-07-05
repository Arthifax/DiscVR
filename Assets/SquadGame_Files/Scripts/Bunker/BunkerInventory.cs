using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MText;

public class BunkerInventory : MonoBehaviour
{
    public GameObject SelectedBed { get; set; }
    private Collider selectedBedCollider;
    public Transform SelectedBedTransform { get; set; }

    public bool isHoldingBed;
    public bool justPlacedBed;
    public bool canPickUpAgain = true;


    public void PlaceBed(Transform newParent)
    {
        if (isHoldingBed)
        {
            if (SelectedBed == null)
            {
                return;
            }
            SelectedBed.GetComponent<BedController>().SetParent(newParent);
            StartCoroutine(TimeOutBed());
            SelectedBed = null;
        }
    }
    
    public void PlaceQuestBed(Transform newParent)
    {
        if (isHoldingBed)
        {
            if (SelectedBed == null)
            {
                return;
            }
            SelectedBed.GetComponent<BedController>().SetParent(newParent);
            SelectedBed = null;

            justPlacedBed = true;
        }
    }
    
    public void PlaceText(Modular3DText TextParent)
    {
        if (isHoldingBed)
        {
            if (SelectedBed == null)
            {
                return;
            }
            SelectedBed.GetComponent<BedController>().PlaceText(TextParent);   
        }
    }
    public void startValidation(BedHolder bedHolder)
    {
        if (justPlacedBed)
        {
            bedHolder.ValidateBed();
        }
    }
    
    public void SwitchBed(GameObject bed, GameObject inventory, Modular3DText textPlace)
    {
        GameObject parent = bed.transform.parent.gameObject;
        bed.GetComponent<BedController>().SetParent(inventory.transform);
        SelectedBed.GetComponent<BedController>().SetParent(parent.transform);
        if (textPlace != null)
        {
            bed.GetComponentInParent<BedController>().PlaceText(textPlace);
        }
        SelectedBed = bed;
    }
    
    IEnumerator TimeOutBed()
    {
        canPickUpAgain = false;
        yield return new WaitForSeconds(0.5f);
        canPickUpAgain = true;
    }
}