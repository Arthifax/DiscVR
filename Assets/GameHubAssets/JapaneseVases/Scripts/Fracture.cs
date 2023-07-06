using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour {
    public Transform parts;
    private Transform breaked = null;
    private Vector3 velocity;


    private void OnCollisionEnter(Collision collision)
    {
        velocity = transform.GetComponent<Rigidbody>().velocity;
        Debug.Log(velocity.magnitude);

        if (velocity.magnitude < 0.7f) return;
        Execute();
    }


    public void Execute()
    { 
        if (breaked) return;

        breaked = (Transform)Instantiate(parts, transform.position, transform.rotation);
        breaked.localScale = transform.localScale;

        foreach (Transform part in breaked)
        {
            part.gameObject.GetComponent<Renderer>().materials[0].CopyPropertiesFromMaterial(gameObject.GetComponent<Renderer>().material);

            float time = Random.Range(5f, 30f);
            Destroy(part.gameObject, time);
        }

        Destroy(breaked.gameObject, 30f);
        Destroy(gameObject);
    }
}
