using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MText;

public class BedController : MonoBehaviour
{
    [SerializeField] private Modular3DText TextObject;
    private Vector3 pickupScale = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private GameObject PlacePosition;
    private BoxCollider collider;
    public bool pickedUp = false;
    private Modular3DText TextPlace;

    public BunkerInventory inventory;


    private void Start()
    {
        PlacePosition = transform.parent.GetChild(0).gameObject;
        collider = GetComponent<BoxCollider>();
        normalScale = transform.localScale;
    }
    public void PlaceText(Modular3DText BedPlaceText)
    {
        TextPlace = BedPlaceText;
        if (TextObject != null)
        {
            TextPlace.Text =TextObject.Text;
            TextObject.gameObject.SetActive(false);
        }
    }
    private void ResetText()
    {
        if (TextObject != null)
        {
            TextObject.gameObject.SetActive(true);
        }
        if (TextPlace != null)
        {
            TextPlace.UpdateText("");
            TextPlace = null;
        }
    }
    public void CheckInventory(GameObject inventory)
    {
        ResetText();
        //collider.enabled = false;
        BunkerInventory inventoryScript = inventory.GetComponent<BunkerInventory>();
        if (inventoryScript.SelectedBed == null && inventoryScript.canPickUpAgain)
        {
            inventoryScript.SelectedBedTransform = this.transform.parent;
            PlacePosition.SetActive(true);
            SetParent(inventory.transform);
            inventoryScript.SelectedBed = this.gameObject;
        }
        else if (!inventoryScript.canPickUpAgain)
        {
            Debug.Log("Hold up!");
        }
        /*else
        {
            inventoryScript.SwitchBed(this.gameObject, inventory, TextPlace);
        }*/
    }

    public void SetParent(Transform newParent)
    {
        transform.parent = null;
        transform.parent = newParent;
        if (!pickedUp)
        {
            ResetText();
            transform.localScale = pickupScale;
            collider.enabled = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            pickedUp = true;
            inventory.isHoldingBed = true;
        }
        else
        {
            PlacePosition = transform.parent.GetChild(0).gameObject;
            PlacePosition.SetActive(false);
            transform.SetParent(newParent);
            collider.enabled = true;
            transform.localScale = normalScale;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            pickedUp = false;
            inventory.isHoldingBed = false;
        }
    }
}
