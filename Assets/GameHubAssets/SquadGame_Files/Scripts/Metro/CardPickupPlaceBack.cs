using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickupPlaceBack : MonoBehaviour
{
    public CardManager cManager;
    Transform originalParent;
    [SerializeField] private Vector3 originalPosition;
    Quaternion originalRotation;
    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    public void HitByPlayer()
    {
        cManager.playAudio();
        cManager.hasSeenCard = true;
        bool onlyPuttingBack = false;
        if (cManager.card != null)
            if (cManager.card == this)
            {
                cManager.card.PutBack();
                onlyPuttingBack = true;
            }

        if (!onlyPuttingBack)
        {
            if (cManager.card != null)
                cManager.card.PutBack();
            transform.parent = cManager.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(-90, 0, 0);

            cManager.card = this;
        }
    }


    public void PutBack()
    {
        transform.parent = originalParent;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        cManager.card = null;
    }
}
