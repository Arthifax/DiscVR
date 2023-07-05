using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private Screen_Fade screenFade;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            screenFade.FadeOut();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
