using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantLight : MonoBehaviour
{
    [SerializeField]Light myLight;
    private bool isFlickering;
    [SerializeField] private float minTimeFlick = 0.01f, maxTimeFlick = 0.2f, minTimePauze = 0.01f, maxTimePauze = 20;
    private float tempDelay;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false && myLight != null)
        {
            StartCoroutine(FlickLight());
        }
    }

    IEnumerator FlickLight()
    {
        isFlickering = true;
        myLight.enabled = true;

        tempDelay = Random.Range(minTimePauze, maxTimePauze);
        yield return new WaitForSeconds(tempDelay);
        myLight.enabled = false;
        tempDelay = Random.Range(minTimeFlick, maxTimeFlick);
        yield return new WaitForSeconds(tempDelay);
        isFlickering = false;

    }
}
