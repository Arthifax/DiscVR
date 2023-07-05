using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PuzzleManagerShortVersion : MonoBehaviour {

    [SerializeField]
    private int puzzleAmount;
    public int puzzlesDoneCurrently;
    public float breakTimer = 4f;

    public GameObject nextLevelLock;
    public GameObject lastPortal;

    public Transform[] objectsFornextPuzzles;
    public Transform[] noObjectsFornextPuzzles;


    private void Start()
    {
        puzzlesDoneCurrently = 0;
    }

    public void PuzzleCompleted()
    {
        puzzlesDoneCurrently++;

        if (puzzlesDoneCurrently == 2)
            if (!lastPortal.activeInHierarchy)
                lastPortal.SetActive(true);

            if (puzzlesDoneCurrently.Equals(puzzleAmount))
        {
                NextSceneEvent();
        }
    }

    public IEnumerator WaitAbit()
    {
        yield return new WaitForSeconds(breakTimer);
        Proceed();
    }

    private void Proceed()
    {
        foreach (Transform t in objectsFornextPuzzles)
        {
            if (!t.gameObject.activeSelf)
                t.gameObject.SetActive(true);

            if (t.gameObject.GetComponent<Collider>() != null)
                t.gameObject.GetComponent<Collider>()?.Do(x => x.enabled = true);
            
        }
        foreach (Transform t in noObjectsFornextPuzzles)
        {
            if (t.gameObject.activeSelf)
                t.gameObject.SetActive(false);
        }
    }

    protected abstract void NextSceneEvent();
    
}
