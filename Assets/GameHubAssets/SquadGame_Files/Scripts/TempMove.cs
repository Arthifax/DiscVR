using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    public float Speed;
    private float index;
    public float Friquancy;
    public float amplitude = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        index += Friquancy;
        this.transform.position += new Vector3(0, 0, Speed);
    }
}
