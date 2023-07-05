using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class MarbleShooter : MonoBehaviour
{
    private bool clicked = false;
    [SerializeField] float shootForce;
    [SerializeField] private GameObject marblePrefab;
    [SerializeField] Transform shootLocation;
    [SerializeField] MarblePlayerManager player;

    [SerializeField] bool hasShotMarble = false;
    [SerializeField] public bool marbleIsMoving;
    [SerializeField] GameObject marbleInWorld;
    [SerializeField] private MarbleInteraction marbleInteraction;

    [SerializeField] MeshRenderer marbleMesh;
    [SerializeField] MeshRenderer marbleShapeMesh;

    [SerializeField] private BoxCollider marbleHolderCollider;

    [SerializeField] private MarbleTossArea victoryArea;

    [SerializeField] private AudioSource shootSound;

    bool hasWaitedForVelocity;
    Rigidbody marbleRb;


    void Update()
    {
        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            InputDevice device = leftHandDevices[0];
            bool primary2DAxisClickValue = false;
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primary2DAxisClickValue) && primary2DAxisClickValue)
            {
                if (!clicked)
                {
                    if (player.holdingMarble && !hasShotMarble)
                    {
                        if (marbleInteraction.marbleShape == player.currentMarbleShape)
                        {
                            FireMarble();
                            shootSound.Play();
                        }
                    }

                    clicked = true;
                }
            }

            if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primary2DAxisClickValue) && !primary2DAxisClickValue)
            {
                clicked = false;
            }
        }

        if(marbleInWorld != null && marbleRb != null)
        {
            if(hasWaitedForVelocity && marbleRb.velocity.magnitude <= 0.01f)
            {
                marbleIsMoving = false;
                marbleRb = null;
                hasShotMarble = false;
                marbleMesh.enabled = true;
                marbleShapeMesh.enabled = true;
                player.holdingMarble = true;
                hasWaitedForVelocity = false;
                marbleHolderCollider.enabled = true;
                if (!victoryArea.isInArea) //Remove marble in world unless it's in the victory area
                {
                    Destroy(marbleInWorld);
                    marbleInWorld = null;
                }
            }
            else
            {
                marbleIsMoving = true;
            }
        }
    }

    public void FireMarble()
    {
        if (player.holdingMarble && !hasShotMarble)
        {
            marbleInWorld = Instantiate(marblePrefab, shootLocation.position, shootLocation.rotation) as GameObject;
            marbleInWorld.GetComponent<Rigidbody>().AddForce(marbleInWorld.transform.forward * 200);
            marbleRb = marbleInWorld.GetComponent<Rigidbody>();
            player.holdingMarble = false;
            marbleHolderCollider.enabled = false;
        
            StartCoroutine(waitOneSec());
            hasShotMarble = true;
            marbleMesh.enabled = false;
            marbleShapeMesh.enabled = false;
            player.holdingMarble = false;
        }
    }


    //We need this delay to happen to avoid destroying the marble right when it spawns
    public IEnumerator waitOneSec()
    {
        yield return new WaitForSeconds(0.5f);
        hasWaitedForVelocity = true;
    }
}
