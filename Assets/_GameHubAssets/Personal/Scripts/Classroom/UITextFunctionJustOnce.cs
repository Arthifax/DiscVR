using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITextFunctionJustOnce : MonoBehaviour {

    public GameObject objectToDisable;

    public void DisableObject()
    {
        //Debug.Log("Im hit in UIText Script");
        objectToDisable?.SetActive(false);

        switch (GameManagerJan.ActiveLevel)
        {
            case CurrentLevel.Classroom:
                break;
            case CurrentLevel.Vault:
                break;
            case CurrentLevel.Bank:
                break;
            case CurrentLevel.Subway:
                break;
        }
    }
}


