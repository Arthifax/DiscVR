using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PuzzleManager : MonoBehaviour
{

    //[SerializeField] private int classroomSceneInt; // This should be 1 without license, 2 with license.
    [SerializeField] private GameObject portalChalkBoard;
    [SerializeField] private GameObject lastPortal;

    public void EnableChalkBoardTP()
    {
        portalChalkBoard.SetActive(true);
    }
    public void EnableDoorTP()
    {
        lastPortal.SetActive(true);
    }
}
