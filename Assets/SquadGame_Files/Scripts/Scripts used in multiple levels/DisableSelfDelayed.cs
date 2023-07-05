using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelfDelayed : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] float disableAfterSec = 0.5f;

    void Start()
    {
        StartCoroutine(DisableSelf());
    }

    private IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(disableAfterSec);
        self.SetActive(false);
    }
}
