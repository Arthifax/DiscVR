using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemSnapper : MonoBehaviour
{
    private MapController mapController;
    private bool holdingItem;
    private GameObject heldItem;
    public string identifierHeldItem;

    private void Start()
    {
        mapController = FindObjectOfType<MapController>();
    }

    private void SetPosition()
    {
        MapItem mapItem = heldItem.GetComponent<MapItem>();
        mapItem.attachedToPoint = true;
        holdingItem = true;
        mapItem.heldBy = this;
        identifierHeldItem = mapItem.identifier;
        heldItem.transform.position = transform.position;
        mapController.CheckOrder();
    }

    public void ReleaseItem()
    {
        holdingItem = false;
        heldItem = null;
        identifierHeldItem = "";
        mapController.CheckOrder();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        if(heldItem == null)
        {
            if(other.transform.tag == "MapInteractable")
            {
                if (other.transform.GetComponent<MapItem>() != null)
                {
                    MapItem mapItem = other.gameObject.GetComponent<MapItem>();
                    if (!mapItem.attachedToPoint && !mapItem.shouldFollow)
                    {
                        heldItem = other.gameObject;
                        SetPosition();
                        
                    }
                }
            }
        }
    }
}
