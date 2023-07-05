using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{

    // Use this for initialization
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originScale;
    [SerializeField] private Vector3 tempScale;
    [SerializeField] private Vector3 rotationOffset = new Vector3(90, 0, -90);
    private bool isPickedUp = false;

    void Start()
    {
        originScale = transform.localScale;
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    public void HitByPlayer()
    {
        if (!isPickedUp)
        {
            Pickup();
            return;
        }
        PutBack();
    }

    public void PutBack()
    {
        transform.parent = originalParent;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        transform.localScale = originScale;
        isPickedUp = false;
    }

    public void Pickup()
    {
        transform.parent = GameObject.Find("TrackedRemote").transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(rotationOffset);
        transform.localScale = tempScale;
        isPickedUp = true;
    }
}
