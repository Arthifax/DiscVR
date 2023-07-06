using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public float ChangeSpeed = 1;
    [Space]
    public Color startColor = Color.black;
    public Color endColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        if (startColor == Color.black)
        {
            startColor = this.GetComponent<Image>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float percentage = Mathf.PingPong(Time.time * ChangeSpeed, 1);
        transform.GetComponent<Image>().color = Color.Lerp(startColor, endColor, percentage);
    }
}
