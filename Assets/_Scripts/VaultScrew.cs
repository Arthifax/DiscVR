using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultScrew : MonoBehaviour
{
    [SerializeField] private VaultManager vaultManager;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material selectedMat;
    [SerializeField] private int screwNumber;
    [SerializeField] private bool isCorrectScrew = false;
    

    private Renderer renderer;
    private bool isClicked = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        
        // Check if this screw is a correct one
        for (int i = 0; i < vaultManager.correctScrewNumbers.Count; i++)
        {
            if (screwNumber == vaultManager.correctScrewNumbers[i]) //if this screw is one of the correct numbers
            {
                isCorrectScrew = true;
            }
        }
    }

    public void VaultScrewInteract()
    {
        
        if (!isClicked && vaultManager.wireBoxPuzzleCompleted) //check if the screw wasn't clicked and the fusebox puzzle is completed
        {
            if (vaultManager.amountChosen < vaultManager.maxChoosableAmount) //check if we aren't yet at the choosing limit
            {
                renderer.material = selectedMat;
                isClicked = true;
                vaultManager.amountChosen++;

                if (isCorrectScrew)
                {
                    vaultManager.amountChosenCorrectly++;
                    vaultManager.CheckVaultScrews();
                }
            }
        }
        else
        {
            renderer.material = defaultMat;
            isClicked = false;
            if(vaultManager.amountChosen > 0)
            {
                vaultManager.amountChosen--;
            }
            if (isCorrectScrew)
            {
                vaultManager.amountChosenCorrectly--;
                vaultManager.CheckVaultScrews();
            }
        }
    }
}
