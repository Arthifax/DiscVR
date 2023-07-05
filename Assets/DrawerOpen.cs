using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{

    [SerializeField]
    Transform drawer;
    [SerializeField]
    AudioClip drawerSound;
    AudioSource audioSource;

    // Smoothly open a door
    public float drawerOpenLimit = 90.0f; //Set either positive or negative number to open the door inwards or outwards
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster

    bool open = false;

    float defaultPos;
    float currentPos;
    float openTime = 0;

    void Start()
    {
        defaultPos = drawer.localPosition.z;
        currentPos = drawer.localPosition.z;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Main function
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitByPlayer();
        }
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        drawer.localPosition = new Vector3(drawer.localPosition.x, drawer.localPosition.y, Mathf.Lerp(currentPos, defaultPos + (open ? drawerOpenLimit : 0), openTime));
    }

    public void HitByPlayer()
    {
        open = !open;
        currentPos = drawer.localPosition.z;
        openTime = 0;
        audioSource.PlayOneShot(drawerSound);
    }
}