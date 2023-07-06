using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedHolder : MonoBehaviour
{
    [SerializeField] private GameObject leftBed;
    [SerializeField] private GameObject centerBed;
    [SerializeField] private GameObject rightBed;
    [SerializeField] private bool leftBedCorrect;
    [SerializeField] private bool centerBedCorrect;
    [SerializeField] private bool rightBedCorrect;
    [SerializeField] private GameObject correctBed;
    [SerializeField] private GameObject Light;
    [SerializeField] private Material[] lightColors;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private LevelController controller;
    private AudioSource audioSource;
    public bool correct;
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int correctPoints;
    [SerializeField] private int wrongPoints;
    [SerializeField] private BunkerInventory inventory;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ValidateBed()
    {
        bool correctBedplaced = false;
        bool otherBedPlacesClear = false;
        if (leftBedCorrect)
        {
            if (leftBed.transform.childCount > 2)
            {
                if (leftBed.transform.GetChild(2).gameObject == correctBed)
                {
                    correctBedplaced = true;
                }
                if (centerBed.transform.childCount == 2 && rightBed.transform.childCount == 2)
                {
                    otherBedPlacesClear = true;
                }
            }
        }
        if (centerBedCorrect)
        {
            if (centerBed.transform.childCount > 2)
            {
                if (centerBed.transform.GetChild(2).gameObject == correctBed)
                {
                    correctBedplaced = true;
                }
                if (leftBed.transform.childCount == 2 && rightBed.transform.childCount == 2)
                {
                    otherBedPlacesClear = true;
                }
            }
        }
        if (rightBedCorrect)
        {
            if (rightBed.transform.childCount > 2)
            {
                if (rightBed.transform.GetChild(2).gameObject == correctBed)
                {
                    correctBedplaced = true;
                }
                if (centerBed.transform.childCount == 2 && leftBed.transform.childCount == 2)
                {
                    otherBedPlacesClear = true;
                }
            }
        }

        if (correctBedplaced && otherBedPlacesClear)
        {
            ChangeLight(true);
            correct = true;
            inventory.isHoldingBed = false;
            inventory.justPlacedBed = false;
            //When done - remove spots around bed
            if (leftBedCorrect)
            {
                Destroy(centerBed);
                Destroy(rightBed);
            }
            if (centerBedCorrect)
            {
                Destroy(leftBed);
                Destroy(rightBed);
            }
            if (rightBedCorrect)
            {
                Destroy(leftBed);
                Destroy(centerBed);
            }
        }
        else
        {
            ChangeLight(false);
            correct = false;
            inventory.isHoldingBed = false;
            inventory.justPlacedBed = false;
        }
        controller.CheckBeds();
    }
    private void ChangeLight(bool correct)
    {
        Material[] changedMaterials = new Material[Light.GetComponent<Renderer>().materials.Length];
        changedMaterials[0] = lightColors[0];
        if (correct)
        {
            changedMaterials[1] = lightColors[2];
            audioSource.clip = sounds[1];
            audioSource.Play();
            pointManager.AddPoints(correctPoints);
            correctBed.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            changedMaterials[1] = lightColors[1];
            audioSource.clip = sounds[0];
            audioSource.Play();
            pointManager.AddPoints(-wrongPoints);
        }
        Light.GetComponent<Renderer>().materials = changedMaterials;
    }
}
