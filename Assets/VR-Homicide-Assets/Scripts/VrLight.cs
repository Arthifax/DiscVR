using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrLight : MonoBehaviour
{
    private Light myLight;
    private bool state = false;
    // Start is called before the first frame update
    private void Start()
    {
        myLight = this.transform.gameObject.GetComponent<Light>();
    }
    public void UpdateLight()
    {
        state = !state;
        myLight.enabled = state;
    }

}
