using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGlobe : MonoBehaviour
{
    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Rotate()
    {
        anim.SetTrigger("Rotate");
    }
}
