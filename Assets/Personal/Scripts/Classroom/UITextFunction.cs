using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITextFunction : MonoBehaviour {

    public GameObject objectToDisable;
    public bool vaultOnce;

    public static bool cannotSpawnBoemboems;
    public int level;

    public void DisableObject()
    {
        //Debug.Log("Im hit in UIText Script");
        objectToDisable?.SetActive(false);

        switch ((CurrentLevel)level)
        {
            case CurrentLevel.Classroom:
                break;
            case CurrentLevel.Vault:
                if (!vaultOnce)
                {
                    vaultOnce = !vaultOnce;
                    cannotSpawnBoemboems = true;
                }
                if (vaultOnce)
                {
                }
                break;
            case CurrentLevel.Bank:
                break;
            case CurrentLevel.Subway:
                break;
            case CurrentLevel.Length:
                level = 2;
                break;
        }
    }
}


