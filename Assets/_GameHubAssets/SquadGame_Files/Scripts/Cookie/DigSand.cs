using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSand : MonoBehaviour
{
    private ParticleSystem[] sandParticles;
    private BoxCollider collider;
    private MeshRenderer meshRenderer;
    int index = 0;

    private void Start()
    {
        sandParticles = GetComponentsInChildren<ParticleSystem>();
        collider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    [SerializeField] private int DigCount = 3;
    public void Dig()
    {
        if (DigCount > 0)
        {
            DigCount--;
            sandParticles[index].Play();
            index++;
            if (DigCount == 0)
            {
                collider.enabled = false;
                meshRenderer.enabled = false;
            }
        }

    }
}
