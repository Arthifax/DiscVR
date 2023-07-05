using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootablePerson : MonoBehaviour
{
    public bool badGuy;
    public GameObject muzzleFlash;
    private ShootingStuff shootingStuff;
    private bool shot;
    private Animator animator;

	void Start ()
    {
        shootingStuff = FindObjectOfType<ShootingStuff>();
        animator = GetComponent<Animator>();
	}
    public void ResetShot()
    {
        shot = false;
    }

    public void Shot()
    {
        if(!shot)
        {
            shot = true;

            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
            if (muzzleFlash != null) muzzleFlash.SetActive(false);
            animator.enabled = false;

            if (badGuy)
            {
                shootingStuff.ShotRight();
            }
            else
            {
                shootingStuff.ShotWrong();
            }
        }
    }
}
