using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmmison : MonoBehaviour
{
    // Start is called before the first frame update
    private Material aMaterial;
    [SerializeField] private bool state = false;
    private Color acolor = Color.white;
    public void UpdateEmmision()
    {
        state = !state;
        acolor = new Color(System.Convert.ToInt32(state), System.Convert.ToInt32(state), System.Convert.ToInt32(state), 1);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            aMaterial = this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material;
            aMaterial.SetColor("_EmissionColor", acolor);
        }
    }
}
