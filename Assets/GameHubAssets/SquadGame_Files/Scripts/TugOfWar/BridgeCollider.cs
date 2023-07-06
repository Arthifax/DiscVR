using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollider : MonoBehaviour
{
    [SerializeField]private List<GameObject> colliders;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I collided with something");
        colliders.Add(other.transform.gameObject);
    }

    private void Update()
    {
        if (colliders.Count > 0)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                colliders[i].transform.position += new Vector3(0, -1f, 0);
            }
        }
    }
}
