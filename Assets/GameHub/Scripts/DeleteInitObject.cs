using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInitObject : MonoBehaviour
{
    private void Awake()
    {  if (FindObjectOfType<DontDestroy>().gameObject!=null)
            Destroy(FindObjectOfType<DontDestroy>().gameObject);
    }
}
