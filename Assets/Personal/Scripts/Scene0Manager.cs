using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene0Manager : PuzzleManager
{
    [SerializeField] private LaCasaLockerScriptTwo nextLevelLock;
    private bool canExit = false;
    public SceneSwitcher sceneSwitcher;
    [SerializeField] private BoxCollider collider;

    public void LastPuzzleCompleted()
    {
        canExit = true;
    }

    public void TryToExit()
    {
        if (canExit)
        {
            collider.enabled = false;
            sceneSwitcher.SwitchScene();
        }
        else
        {
            nextLevelLock.ZoomLock();
        }
    }
}
