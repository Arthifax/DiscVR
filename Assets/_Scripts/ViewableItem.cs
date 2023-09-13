using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ViewableItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private String itemText;
    [SerializeField] private String itemDescription;
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject holdingPos;
    [SerializeField] private TextMeshPro itemTextField;
    [SerializeField] private TextMeshPro itemDescriptionField;
    [SerializeField] private GameObject deactivationObject;
    [SerializeField] private Vector3 itemHoldingRotation;
    private bool isBeingHeld = false;
    private Vector3 startingLocalPos;
    private quaternion startingRotation;

    private void Start()
    {
        startingLocalPos = item.transform.localPosition;
        startingRotation = item.transform.localRotation;
        //var gameControllers = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.GameController, gameControllers);
    }

    public void ViewItem()
    {
        if (!player.isHoldingItem && !isBeingHeld)
        {
            item.transform.parent = holdingPos.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = holdingPos.transform.localRotation;
            item.transform.rotation *=
                UnityEngine.Quaternion.Euler(itemHoldingRotation.x, itemHoldingRotation.y, itemHoldingRotation.z);
            itemTextField.text = itemText;
            itemDescriptionField.text = itemDescription;
            deactivationObject.SetActive(true);
            player.isHoldingItem = true;
            isBeingHeld = true;
        }
        else if (player.isHoldingItem && isBeingHeld)
        {
            item.transform.parent = this.transform;
            item.transform.localPosition = startingLocalPos;
            item.transform.localRotation = startingRotation;
            itemTextField.text = "";
            itemDescriptionField.text = "";
            deactivationObject.SetActive(false);
            player.isHoldingItem = false;
            isBeingHeld = false;
        }
    }
}
