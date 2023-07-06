using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAtPlayer : MonoBehaviour
{
    public Transform player;


    void Start()
    {
        
    }
    
    void Update()
    {
        gameObject.transform.LookAt(player.transform);
    }
}
