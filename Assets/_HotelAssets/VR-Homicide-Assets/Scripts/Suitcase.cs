using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suitcase : Multiton<Suitcase> {

    public static Suitcase SelectedSuitcase { get; private set; }
    public Animator suitcaseAnim;

    private Vector3 startPosition;
    private Quaternion startRotation;

    #region Emission Behaviour
    [SerializeField]
    private float maxEmissionValue = 1, emissionValueLerpSpeed = 1;
    [SerializeField]
    private new Renderer renderer;

    private Color startingColor;
    private float emissionValue;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        startPosition = transform.position;
        startRotation = transform.rotation;
        renderer.material = Instantiate(renderer.material);
        startingColor = renderer.material.GetColor("_EmissionColor");
    }

    private void Update()
    {
        float modifier =  Time.deltaTime * (SelectedSuitcase == this ? 1 : -1) * emissionValueLerpSpeed * maxEmissionValue;
        emissionValue = Mathf.Clamp(emissionValue + modifier, 0, maxEmissionValue);
        renderer.material.SetColor("_EmissionColor", startingColor * emissionValue * maxEmissionValue);
    }

    /// <summary>
    /// OnCollissionEnter or something like that
    /// </summary>
    public void SelectSuitcase()
    {
        Func<Painting> isParented = delegate ()
        {
            foreach (Painting painting in Painting.Instances)
                if (painting.AttachedSuitcase == this)
                    return painting;
            return null;
        };

        var parentPainting = isParented();
        if (parentPainting)
        {
            parentPainting.UnparentSuitcase();
            transform.position = startPosition;
            transform.rotation = startRotation;
            return;
        }

        if (SelectedSuitcase != null)
        {
            bool isThis = SelectedSuitcase == this;
            transform.position = startPosition;
            transform.rotation = startRotation;
            SelectedSuitcase.DeselectSuitcase();
            if (isThis)
                return;
        }

        // Add anime
        SelectedSuitcase = this;
        suitcaseAnim.SetBool("Activated", true);
        Painting.MovePaintingsTowardsPlayer();
    }

    private void DeselectSuitcase()
    {
        // Add anime
        SelectedSuitcase = null;
        suitcaseAnim.SetBool("Activated", false);
        Painting.MovePaintingsBack();
    }

    public void SetPositionAndDeselect(Vector3 position, Quaternion rotation)
    {
        // add anime
        transform.position = position;
        transform.rotation = rotation;
        DeselectSuitcase();
    }
}
