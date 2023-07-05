using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#region AUTHOR
// MADE BY MITCHELL TUINSTRA
// https://www.mitchelltuinstra.nl
// 20/02/2020
#endregion

[RequireComponent(typeof(Rigidbody))]
public class KnockOnInteract : MonoBehaviour {

    [SerializeField]
    float maxSidewaysForce = 2;

    Rigidbody rBody;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitByPlayer();
            Debug.Log("key pressed");
        }
    }

    public void HitByPlayer()
    {
        rBody = gameObject.GetComponent<Rigidbody>();
        rBody.useGravity = true;
        rBody.AddForce(new Vector3(Random.Range(-maxSidewaysForce, maxSidewaysForce), 0, Random.Range(-maxSidewaysForce, maxSidewaysForce)), ForceMode.Impulse);
    }
}
