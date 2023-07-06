using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class VaultControls : MonoBehaviour
{
    [SerializeField]
    private GameObject pickAxe;
    private GameObject HighlightedObject = null;
    [SerializeField] private InsideVaultManager vaultManager;
    public GameObject PickaxePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform portalDigPosition;
    [SerializeField] private Transform portalWirePosition;
    private Vector3 portalDigVector;
    private C4Script C4Bomb;
    [SerializeField] private bool RightController;
   
    [Space(10)]
    [SerializeField] private AudioSource[] vaultAudio;
    void Start()
    {
        portalDigVector = new Vector3(portalDigPosition.position.x, player.transform.position.y, portalDigPosition.transform.position.z);
    }

    private void Update()
    {
        var Controller = new List<InputDevice>();
        if (RightController)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, Controller);
        }
        else
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, Controller);
        }
        if (Controller.Count > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                if (!hit.transform.transform.name.Contains("Canvas VaultText"))
                {
                    if (vaultAudio != null)
                    {
                        foreach (AudioSource vault in vaultAudio)
                        {
                            if (vault.isPlaying)
                            {
                                vault.Stop();
                            }
                        }
                    }
                }
                if (hit.transform.GetComponent<Digtile>() != null)
                {
                    //if (player.transform.position == portalDigVector)
                    //{
                    if (FindObjectOfType<InsideVaultManager>().state == 2 && PickaxePrefab == null)
                    {
                        PickaxePrefab = Instantiate(pickAxe, hit.transform);
                        PickaxePrefab.name = "pickaxePrefab";
                    }
                    //}
                    hit.transform.GetComponent<Digtile>().Highlight();
                }
                InputDevice device = Controller[0];
                bool triggerButtonValue = false;
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && !triggerButtonValue)
                {

                    if (hit.transform.GetComponent<Digtile>() != null)
                    {
                        if (PickaxePrefab != null)
                        {
                            foreach (GameObject pick in GameObject.FindGameObjectsWithTag("pick"))
                                Destroy(pick);
                        }
                    }
                    if (C4Bomb != null)
                    {
                        C4Bomb.BombSet();
                    }
                }

                //Vault rotate
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
                {
                    if (hit.transform.name.Contains("VaultText"))
                    {
                        hit.transform.GetComponentInParent<VaultScript>().RotateVaultHandle(hit.transform.parent);
                    }

                    if (hit.transform.name.Contains("C4 Bomb Model"))
                    {
                        C4Bomb = hit.transform.gameObject.GetComponent<C4Script>();
                        hit.transform.position = new Vector3(hit.point.x, hit.point.y, hit.transform.position.z);
                    }

                    if (hit.transform.GetComponent<Digtile>() != null)
                    {
                        if (Vector3.Distance(hit.transform.position, transform.position) < 4f && hit.transform.GetComponent<Digtile>())
                        {
                            if (vaultManager.state == 2)
                            {
                                hit.transform.GetComponent<Digtile>().Dig();
                                if (PickaxePrefab != null && hit.transform.GetComponent<Digtile>().digging)
                                {
                                    if (PickaxePrefab.transform.parent != hit.transform)
                                    {
                                        PickaxePrefab.transform.parent = hit.transform;
                                    }
                                }
                            }
                        }
                    }

                    if (hit.transform.name.Contains("fuseDoor"))
                    {
                        hit.transform.GetComponent<Animator>().SetTrigger("FuseDoorOpen");
                    }

                    if (hit.transform.name.Contains("FuseWire"))
                    {
                        hit.transform.parent.GetComponent<FuseBox>().RemoveFuse(hit.transform);
                    }
                }
            }
        }
    }
}
