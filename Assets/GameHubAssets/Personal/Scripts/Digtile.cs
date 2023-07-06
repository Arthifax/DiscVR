using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digtile : MonoBehaviour
{
   [SerializeField] private InsideVaultManager insideVaultManager;

   [SerializeField] private Animator AC;

    private float diggingNeeded = 25f;

    public AudioSource dugUpSound;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform digPos;
    [SerializeField] private VaultControls leftHandControlsForPickaxeCheck;
    [SerializeField] private VaultControls rightHandControlsForPickaxeCheck;
    [SerializeField] GameObject floorMoney;
    [SerializeField] GameObject floorPickaxe;
    [SerializeField] private GameObject portals;

    public bool digging = false;
    public bool falling = false;

    private void Update()
    {
        if (insideVaultManager.state != 2)
        {
            return;
        }
        if (leftHandControlsForPickaxeCheck.PickaxePrefab != null||rightHandControlsForPickaxeCheck !=null)
        {
            digging = true;
        }
        else
        {
            digging = false;
        }
        if (!digging && !falling)
        {
            floorPickaxe.SetActive(true);
        }
    }

    public void Highlight()
    {
        if (insideVaultManager.state.Equals(2))
        {
            AC.SetTrigger("Highlight");
        }
    }
    public void UnHighlight()
    {
        if (insideVaultManager.state.Equals(2))
        {
            AC.SetTrigger("UnHighlight");
        }
    }

    IEnumerator UnHighlightDelay()
    {
        yield return new WaitForSeconds(0.05f);
        AC.SetBool("Highlight", false);
    }

    public void Dig()
    {
        if (playerPos.position.x == digPos.position.x && playerPos.position.z == digPos.position.z)
        {
            diggingNeeded--;
            floorPickaxe.SetActive(false);
            if (diggingNeeded <= 0)
            {
                if (dugUpSound != null) dugUpSound.Play();
                AC.SetTrigger("Dugup");
                if (leftHandControlsForPickaxeCheck.PickaxePrefab != null)
                {
                    Destroy(leftHandControlsForPickaxeCheck.PickaxePrefab);
                    leftHandControlsForPickaxeCheck.PickaxePrefab = null;
                }
                if (rightHandControlsForPickaxeCheck.PickaxePrefab != null)
                {
                    Destroy(rightHandControlsForPickaxeCheck.PickaxePrefab);
                    rightHandControlsForPickaxeCheck.PickaxePrefab = null;
                }
                //GoToNext();
                LetFall();
            }
        }
    }

    private void LetFall()
    {
        floorPickaxe.SetActive(false);
        portals.SetActive(false);
        if (playerRb != null)
            playerRb.isKinematic = false;
        floorMoney.SetActive(false);
    }
}
