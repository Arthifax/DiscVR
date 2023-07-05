using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionBehaviour : MonoBehaviour {

    [SerializeField]
    private Color color = Color.white;

    private Material material;

	private float EmissionValue
    {
        set
        {
            material.SetColor("_EmissionColor", color * value);
        }
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
}
