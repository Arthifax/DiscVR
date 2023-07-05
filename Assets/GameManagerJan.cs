using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CurrentLevel { Classroom = 0 , Bank = 1, Vault = 2, Subway = 3, Length = 4 }

public static class GameManagerJan
{
    public static CurrentLevel ActiveLevel => (CurrentLevel)SceneManager.GetActiveScene().buildIndex;
}

public class Renslet : MonoBehaviour
{
    public static Renslet Instance;
    public int i;

    private void Awake()
    {
        Instance = this;
    }

    public static void DoTheThing()
    {
        Debug.Log("I'm sleepy");
    }
}

public class Riwana : MonoBehaviour
{
    private void Start()
    {
        Renslet.Instance.i = 10;
        Renslet.DoTheThing();

        SleepyExtensions.ResetTransform(transform);
        transform.ResetTransformExt();

        List<int> list = new List<int>() {1, 2, 3, 4, 69 };

        list.DoForEachItem(x => Debug.Log(x));
    }
}

public static class SleepyExtensions
{
    public static void ResetTransform(Transform trans)
    {
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
    }

    public static void ResetTransformExt(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
    }

    // Jan's favoriete extension
    public static void DoForEachItem<T>(this List<T> list, Action<T> action)
    {
        foreach (T item in list)
            action(item);
    }
}