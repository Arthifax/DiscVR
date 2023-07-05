using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{

    public MapItemSnapper[] slottedMapItems;
    public string correctOrder;
    private string currentOrder;

    public LineRenderer lineRenderer;
    public Material matVisible;
    public Material matInvisible;
    public Animator lineRendererAnimator;
    public Scene0Manager puzzleManager;
    private bool hasTriggeredAnimation;

    [SerializeField] private AudioClip correct = null;
    [SerializeField] private AudioClip wrong = null;
    [SerializeField] private AudioSource source = null;

    [SerializeField] private GameObject professor = null;

    [SerializeField]
    private UnityEvent OnPuzzleCorrect = new UnityEvent();


    private void Start()
    {
        lineRenderer.positionCount = slottedMapItems.Length;
        for (int i = 0; i < slottedMapItems.Length; i++)
        {
            lineRenderer.SetPosition(i, slottedMapItems[i].transform.position - new Vector3(-1.492f, 10.394f, -3.2347f) /*+ new Vector3(0, 0, 0.1f)*/);
        }
        lineRenderer.material = matInvisible;
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 endPos, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = endPos;
    }

    public void CheckOrder()
    {
        currentOrder = "";
        for (int i = 0; i < slottedMapItems.Length; i++)
        {
            currentOrder += slottedMapItems[i].identifierHeldItem;
        }
        if (currentOrder.Equals(correctOrder))
        {
            if (!hasTriggeredAnimation)
            {
                hasTriggeredAnimation = true;
                lineRenderer.material = matVisible;
                lineRendererAnimator.SetTrigger("Activate");

                puzzleManager.EnableChalkBoardTP();
                PlaySound(correct, .5f);
                // puzzle counter increase here;

                OnPuzzleCorrect?.Invoke();
            }
        }
        if (currentOrder.Length == correctOrder.Length && (currentOrder != correctOrder))
        {
            PlaySound(wrong, .9f);
        }
    }
    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
    private void OnDestroy()
    {
        OnPuzzleCorrect.RemoveAllListeners();
        OnPuzzleCorrect = null;
    }
}
