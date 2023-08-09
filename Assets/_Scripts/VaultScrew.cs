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
        if (!isClicked)
        {
            renderer.material = selectedMat;
            isClicked = true;

            if (isCorrectScrew)
            {
                vaultManager.amountChosenCorrectly++;
                vaultManager.CheckVaultScrews();
            }
        }
        else
        {
            renderer.material = defaultMat;
            isClicked = false;

            if (isCorrectScrew)
            {
                vaultManager.amountChosenCorrectly--;
                vaultManager.CheckVaultScrews();
            }
        }
    }
}
