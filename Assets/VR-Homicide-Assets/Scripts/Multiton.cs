using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiton<T> : MonoBehaviour where T : Multiton<T> {

    public static List<T> Instances { get; private set; }

    protected virtual void Awake()
    {
        if (Instances == null)
            Instances = new List<T>();
        Instances.Add((T)this);
    }
}
