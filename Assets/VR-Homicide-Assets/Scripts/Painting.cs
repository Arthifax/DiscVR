using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Multiton<Painting> {

    [SerializeField]
    private Transform suitcasePosition;

    [SerializeField]
    private Animator animator;
    public Animator Animator
    {
        get
        {
            return animator;
        }
    }

    public Suitcase AttachedSuitcase { get; private set; }

    public void OnInteract()
    {
        var selectedSuitcase = Suitcase.SelectedSuitcase;
        if (!selectedSuitcase || AttachedSuitcase)
            return;

        AttachedSuitcase = selectedSuitcase;
        selectedSuitcase.SetPositionAndDeselect(suitcasePosition.position, suitcasePosition.rotation);
        HallwayPuzzle.CheckIfPuzzleIsCompleted();
    }

    public static void MovePaintingsTowardsPlayer()
    {
        // TODO RENSLOTH
        foreach(Painting painting in Instances)
        {
            painting.Animator.SetBool("Activated", true);
        }
    }

    public static void MovePaintingsBack()
    {
        foreach (Painting painting in Instances)
        {
            painting.Animator.SetBool("Activated", false);
        }
    }

    public void UnparentSuitcase()
    {
        AttachedSuitcase = null;
    }
}
