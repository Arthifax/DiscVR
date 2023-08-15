using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultManager : MonoBehaviour
{
    [Header("Screws")]
    [SerializeField] public List<int> correctScrewNumbers;
    [SerializeField] public int amountChosenCorrectly;
    [SerializeField] public List<GameObject> correctScrews;

    [Header("Bomb")]
    [SerializeField] private GameObject bombVisual;
    [SerializeField] private GameObject bombPlaceLocation;

    [Header("Vault Door")]
    [SerializeField] private GameObject vaultDoor;
    [SerializeField] private Vector3 doorTargetRotation;
    [SerializeField] private float doorOpenSpeed = 3f;


    public void CheckVaultScrews()
    {
        if(amountChosenCorrectly == correctScrewNumbers.Count)
        {
            StartBombStep();
        }
    }

    private void StartBombStep()
    {
        bombVisual.SetActive(true);
        bombPlaceLocation.SetActive(true);
    }

    public void CompleteVaultPuzzle()
    {
        StartCoroutine(OpenVaultDoor(doorTargetRotation, doorOpenSpeed));
        Debug.Log("Check, one");
    }

    private IEnumerator OpenVaultDoor(Vector3 targetRotation, float rotationSpeed)
    {
        Debug.Log("two");
        float elapsedTime = 0f;
        Quaternion startRotation = vaultDoor.transform.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);

        while (elapsedTime < 1f)
        {
            vaultDoor.transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        vaultDoor.transform.rotation = targetQuaternion; // Ensure we reach the exact target rotation.
    }
}
