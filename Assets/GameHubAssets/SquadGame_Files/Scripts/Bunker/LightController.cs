using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject CashLamps;
    [SerializeField] private GameObject lampRows;
    private Color AmbientColor = new Color(70f/255f, 70f/255f, 70f/255f);
    private Color AmbientColorStart = Color.black;
    private bool LightenScene = false;
    [SerializeField] private float ChangeTime = 8f;
    private float time = 0;

    private void Start()
    {
        StartCoroutine(EnableLights());
    }
    private IEnumerator EnableLights()
    {
        yield return new WaitForSeconds(3f);
        CashLamps.SetActive(true);
        yield return new WaitForSeconds(5f);
        lampRows.SetActive(true);
        LightenScene = true;
    }

    private void Update()
    {
        /*if (LightenScene)
        {
            time += Time.deltaTime / ChangeTime;
            RenderSettings.ambientLight = Color.Lerp(AmbientColorStart, AmbientColor, time);
            if(time>=ChangeTime)
            {
                LightenScene = false;
            }
        }*/
    }
}
