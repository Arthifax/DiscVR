using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFalling : MonoBehaviour
{
    [SerializeField] private Rigidbody vrFall;
    [SerializeField] SceneSwitcher _sceneSwitcher;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Rigidbody>() != null)
        {
            vrFall.isKinematic = true;
            StartCoroutine("EnableUI");
        }
    }
    private IEnumerator EnableUI()
    {
        yield return new WaitForSeconds(1f);
        _sceneSwitcher.SwitchScene();
    }
}
