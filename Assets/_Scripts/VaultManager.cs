using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VaultManager : MonoBehaviour
{
    [Header("Screws")]
    [SerializeField] public List<int> correctScrewNumbers;
    [SerializeField] public int maxChoosableAmount = 5;
    [SerializeField] public int amountChosen = 0;
    [SerializeField] public int amountChosenCorrectly;
    [SerializeField] public List<GameObject> correctScrews;
    [SerializeField] private Material defaultScrewMat;

    [Header("Bomb")]
    [SerializeField] private GameObject bombVisual;
    [SerializeField] private GameObject bombOnDoorVisual;
    [SerializeField] private GameObject bombPlaceLocation;
    [SerializeField] private GameObject explosionDecal;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject secondExplosion;
    [SerializeField] private GameObject teleportersInVault;
    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip vaultOpening;
    [SerializeField] private AudioClip countdownSound;

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
        StartCoroutine(OpenVaultDoor(doorTargetRotation, doorOpenSpeed));
    }

    public void CompleteVaultPuzzle()
    {
        StartCoroutine(OpenVaultDoor(doorTargetRotation, doorOpenSpeed));
    }

    private IEnumerator OpenVaultDoor(Vector3 targetRotation, float rotationSpeed)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = vaultDoor.transform.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);


        yield return new WaitForSeconds(1f);

        playerAudioSource.PlayOneShot(countdownSound);
        timerText.text = "00:04";

        yield return new WaitForSeconds(1f);

        timerText.text = "00:03";

        yield return new WaitForSeconds(1f);

        timerText.text = "00:02";

        yield return new WaitForSeconds(1f);

        timerText.text = "00:01";

        yield return new WaitForSeconds(1f);

        timerText.text = "00:00";

        explosion.SetActive(true);
        secondExplosion.SetActive(true);
        explosionDecal.SetActive(true);
        bombOnDoorVisual.SetActive(false);
        playerAudioSource.PlayOneShot(explosionSound);

        yield return new WaitForSeconds(2f);

        playerAudioSource.PlayOneShot(vaultOpening);
        teleportersInVault.SetActive(true);

        while (elapsedTime < 1f)
        {
            vaultDoor.transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        vaultDoor.transform.rotation = targetQuaternion; // Ensure we reach the exact target rotation.
    }
}
