using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ViewableItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject holdingPos;
    private bool isBeingHeld = false;
    private Vector3 startingLocalPos;
    private quaternion startingRotation;

    private void Start()
    {
        startingLocalPos = item.transform.localPosition;
        startingRotation = item.transform.localRotation;
    }

    public void ViewItem()
    {
        if (!player.isHoldingItem && !isBeingHeld)
        {
            item.transform.parent = holdingPos.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = holdingPos.transform.localRotation;
            player.isHoldingItem = true;
            isBeingHeld = true;
        }
        else if (player.isHoldingItem && isBeingHeld)
        {
            item.transform.parent = this.transform;
            item.transform.localPosition = startingLocalPos;
            item.transform.localRotation = startingRotation;
            player.isHoldingItem = false;
            isBeingHeld = false;
        }
    }
}
