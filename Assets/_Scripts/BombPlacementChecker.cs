using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacementChecker : MonoBehaviour
{

    private bool bombIsInCorrectSpot = false;
    [SerializeField] private VaultManager vaultManager;
    [SerializeField] GameObject placedBombObject;
    [SerializeField] GameObject draggableBombPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Bomb"))
        {
            placedBombObject.SetActive(true);
            draggableBombPrefab.SetActive(false);
            vaultManager.CompleteVaultPuzzle();
        }
    }
}
