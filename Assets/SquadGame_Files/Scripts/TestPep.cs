using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPep : MonoBehaviour
{
    private Transform attachTransform;
    private bool attached;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attached)
        {
            this.transform.position = attachTransform.position;
        }
    }

    public void attach(GameObject muGameObject)
    {
        attachTransform = muGameObject.transform;
        attached = true;
    }

    public void detach()
    {

    }
}
