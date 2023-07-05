using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaloezieCounter : MonoBehaviour
{

    private int counter;

    [SerializeField] private Scene0Manager man;

    public void SetCounter()
    {
        counter++;
    }
}
