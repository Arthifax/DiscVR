using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFallingCamera : MonoBehaviour
{
    private bool falling = false;
    [SerializeField] private AudioSource scream;
    public void SetFalling()
    {
        falling = true;
        StartCoroutine(WaitDelay());
    }

    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(.5f);
        scream.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (falling && this.transform.rotation.x < 150)
        {
            this.transform.Rotate(10*Time.deltaTime,0,0);
        }
    }
}
