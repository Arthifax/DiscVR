using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene0ManagerShortVersion : PuzzleManagerShortVersion
{

    [SerializeField]
    private GameObject nextPortal;
    private bool canExit;
    public SceneSwitcher sceneSwitcher;

    protected override void NextSceneEvent()
    {
        //TODO: PLAY PHONE SOUND
        StartCoroutine(WaitForPhone());
        //Debug.Log("Scene0good");
    }

    IEnumerator WaitForPhone()
    {
        yield return new WaitForSeconds(1f);
        if (!nextPortal.activeInHierarchy)
            nextPortal.SetActive(true);
        canExit = true;
    }

    public void TryToExit()
    {
        if (canExit) sceneSwitcher.SwitchScene();
    }
}
