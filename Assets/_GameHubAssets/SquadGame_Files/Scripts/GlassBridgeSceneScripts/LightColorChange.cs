using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private int startIndex;
    [SerializeField] private Material lightMaterial;
    private float switchTime = 5f;

    private void Start()
    {
        lightMaterial.EnableKeyword("_EMISSION");
        InvokeRepeating("ChangeLights", 0f, switchTime);
    }
    private void ChangeLights()
    {
        if (startIndex < colors.Count - 1)
        {
            startIndex++;
        }
        else
        {
            startIndex = 0;
        }

        lightMaterial.color = colors[startIndex];
    }
    private void OnDestroy()
    {

    }
}
