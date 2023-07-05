using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketScript : MonoBehaviour
{

    // Use this for initialization
    public TicketManager tManager;
    Transform originalParent;
    [SerializeField] private Vector3 originalPosition;
    Quaternion originalRotation;
    [SerializeField] private Vector3 originScale;
    [SerializeField] private Vector3 tempScale;

    void Start()
    {
        originScale = transform.localScale;
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            tManager.playAudio();
            bool onlyPuttingBack = false;
            if (tManager.currentTicket != null)
            {
                if (tManager.currentTicket == this)
                {
                    tManager.currentTicket.PutBack();
                    onlyPuttingBack = true;
                }

            }
            if (!onlyPuttingBack)
            {
                if (tManager.currentTicket != null)
                    tManager.currentTicket.PutBack();
                transform.parent = tManager.transform;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(90, 0, -90);
                transform.localScale = tempScale;

                tManager.currentTicket = this;
            }
        }
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
            transform.localScale = tempScale;

            tManager.currentTicket = this;
        }
    }


    public void PutBack()
    {
        transform.parent = originalParent;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        transform.localScale = originScale;
        tManager.currentTicket = null;
    }
}
