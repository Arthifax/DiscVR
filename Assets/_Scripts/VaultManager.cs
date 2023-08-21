using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultManager : MonoBehaviour
{
    [Header("Screws")]
    [SerializeField] public List<int> correctScrewNumbers;
    [SerializeField] public int maxChoosableAmount = 4;
    [SerializeField] public int amountChosen = 0;
    [SerializeField] public int amountChosenCorrectly;
    [SerializeField] public List<GameObject> correctScrews;
    [SerializeField] private Material defaultScrewMat;

    [Header("Bomb")]
    [SerializeField] private GameObject bombVisual;
    [SerializeField] private GameObject bombPlaceLocation;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip vaultOpening;

    [Header("Vault Door")]
    [SerializeField] private GameObject vaultDoor;
    [SerializeField] private Vector3 doorTargetRotation;
    [SerializeField] private float doorOpenSpeed = 3f;

    public bool wireBoxPuzzleCompleted = false;


    public void CheckVaultScrews()
    {
        if(amountChosenCorrectly == correctScrewNumbers.Count)
        {
            StartBombStep();

            for (int i = 0; i < correctScrews.Count; i++) //set all screws back to their default material
            {
                correctScrews[i].GetComponent<Renderer>().material = defaultScrewMat;
            }
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

        explosion.SetActive(true);
        playerAudioSource.PlayOneShot(explosionSound);

        yield return new WaitForSeconds(2f);

        playerAudioSource.PlayOneShot(vaultOpening);

        while (elapsedTime < 1f)
        {
            vaultDoor.transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        vaultDoor.transform.rotation = targetQuaternion; // Ensure we reach the exact target rotation.
    }
}
