using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketScript4P : MonoBehaviour
{

    // Use this for initialization
    public TicketManager4P tManager;
    Transform originalParent;
    Vector3 originalPosition;
    Quaternion originalRotation;
    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    public void HitByPlayer()
    {
        tManager.playAudio();
        bool onlyPuttingBack = false;
        if (tManager.currentTicket != null)
            if (tManager.currentTicket == this) {
                tManager.currentTicket.PutBack();
                onlyPuttingBack = true;
            }

        if (!onlyPuttingBack)
        {
            if (tManager.currentTicket != null)
                tManager.currentTicket.PutBack();
            transform.parent = tManager.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(90, 0, -90);
            transform.localScale = new Vector3(0.18f, 1, 0.06f);

            tManager.currentTicket = this;
        }
    }

    public void PutBack()
    {
        transform.parent = originalParent;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        transform.localScale = new Vector3(0.03f, 1f, 0.01f);
        tManager.currentTicket = null;
    }
}
