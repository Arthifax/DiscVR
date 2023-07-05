using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHitbox : MonoBehaviour
{
    [SerializeField] private GameObject coll = null;

    public void EnableCol()
    {
        if (!coll)
            return;

        coll.SetActive(true);
    }

    public void DisableCol()
    {
        if (!coll)
            return;

        coll.SetActive(false);
    }
}
