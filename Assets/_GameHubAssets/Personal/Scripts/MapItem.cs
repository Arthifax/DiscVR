using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour
{
    private Vector3 prevPosition;
    [HideInInspector] public bool shouldFollow;
    [HideInInspector] public bool broughtForward;
    [HideInInspector] public bool attachedToPoint;
    [HideInInspector] public MapItemSnapper heldBy;
    public string identifier;
    [SerializeField] private RaycastMovement raycastMovement;


    public void StartFollow(RaycastMovement raycastMovementController)
    {
        raycastMovement = raycastMovementController;
        prevPosition = transform.position;
        gameObject.layer = 2;
        shouldFollow = true;
        attachedToPoint = false;
        if (heldBy != null)
        {
            heldBy.ReleaseItem();
            heldBy = null;
        }
    }

    public void ResetPos()
    {
        transform.position = prevPosition;
    }

    public void StopFollow()
    {
        raycastMovement = null;
        gameObject.layer = 0;
        shouldFollow = false;
    }

    private void Update()
    {
        if (shouldFollow)
        {
            transform.position = raycastMovement.raycastHit.point;
        }

    }
}
