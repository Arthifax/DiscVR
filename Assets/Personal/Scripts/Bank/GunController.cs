using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    public bool switchingToNextScene;

    [Header("Gun Objects")]
    public bool holdingGun;
    public AudioSource gunSounds;
    public GameObject gunObject;
    private bool mayShootRays = true;
    private bool shot = false;
    [SerializeField] private bool RightController;
    [SerializeField] private ShootingStuff shootingStuff;
    // Start is called before the first frame update
    void Start()
    {
        shootingStuff.Subscribe(this);
        switchingToNextScene = false;
        GetGun();
    }
    public void GetGun()
    {
        holdingGun = true;
        gunObject.SetActive(true);
    }

    public void DropGun()
    {
        holdingGun = false;
        gunObject.SetActive(false);
    }
    public void NoRays()
    {
        mayShootRays = false;
    }
    public void ShootRays()
    {
        Debug.Log("Reset");
        mayShootRays = true;
    }

    private void Update()
    {
        if (mayShootRays)
        {
            RaycastHit hit;
            var Controller = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, Controller);
            if (RightController)
            {
                InputDevices.GetDevicesAtXRNode(XRNode.RightHand, Controller);
            }
            else
            {
                InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, Controller);
            }
            bool triggerButtonValue = false;
            if (Controller.Count > 0)
            {
                InputDevice device = Controller[0];
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
                {
                    if (!shot)
                    {
                        if (holdingGun)
                        {
                            gunSounds.pitch = RNG.GimmeNumber(0.9f, 1.1f);
                            gunSounds.Play();
                        }
                        if (Physics.Raycast(transform.position, transform.forward, out hit))
                        {
                            if (hit.transform.GetComponent<Animator>() == true)
                            {
                                hit.transform.GetComponent<Animator>().SetTrigger("Activate");
                            }
                            if (hit.collider.CompareTag("Person"))
                            {
                                if (holdingGun)
                                {
                                    hit.collider.GetComponent<ShootablePerson>().Shot();
                                }
                                if (!holdingGun && hit.collider.GetComponent<HostageSelection>() != null)
                                {
                                    hit.collider.GetComponent<HostageSelection>().SelectHostage();
                                    this.mayShootRays = false;
                                }
                            }
                        }
                        shot = true;
                    }
                }

                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
                {
                    shot = false;
                }
            }
        }
    }
}

