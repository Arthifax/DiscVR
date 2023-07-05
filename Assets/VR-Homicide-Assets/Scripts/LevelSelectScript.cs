using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] locks;
    public Material activeMaterial;

    void Start()
    {
        int level = PlayerPrefs.GetInt("progress");
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(PlayerPrefs.GetInt("progress"));
            if (i <= level && i != 0)
            {
                doors[i -1].GetComponent<MeshRenderer>().material = activeMaterial;
                //doors[i].tag = "interactable";
                locks[i -1].GetComponent<LockerScript>().LockCorrect();
                //Debug.Log(locks[i]);
            }
            else
            {
                doors[i -1].GetComponent<HomicideDoorScript>().canGoNext = false;
                //Debug.Log(doors[i]);
            }
        }
    }
}
