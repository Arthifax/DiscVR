using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainTeleportMarker : MonoBehaviour
{
    [SerializeField] private bool nextLevel;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private MetroDoors metroDoors;
    [SerializeField] private Button nextLevelBT;
    private WaitForSeconds Delay1Second = new WaitForSeconds(1f);
    private WaitForSeconds DelayHalfSecond = new WaitForSeconds(.4f);
    // Start is called before the first frame update

    public void StartGame()
    {
        nextLevel = true;
        DisableButtons();
        StartCoroutine(DelayBeforeFade());
    }
    public void RestartGame()
    {
        nextLevel=false;
        DisableButtons();
        StartCoroutine(DelayBeforeFade());
    }

    private void DisableButtons()
    {
        nextLevelBT.interactable = false;
    }
    private IEnumerator DelayBeforeFade()
    {
        yield return DelayHalfSecond; 
        metroDoors.CloseTrainDoors();
        yield return Delay1Second;
        if (nextLevel)
        {
            levelLoader.LoadNextLevel();
        }
        else
        {
            levelLoader.ReloadLevel();
        }
    }
}
