using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroDoors : MonoBehaviour
{
    [SerializeField] private List<Animator> animators;
    [SerializeField] private GameObject teleportSpotParent;
    [SerializeField] private List<AudioSource> trainDoorSources;
    [SerializeField] private AudioClip trainDoorOpenSound;
    [SerializeField] private AudioClip trainDoorCloseSound;
    private bool doorsClosed = true;
    // Start is called before the first frame update


    public void OpenTrainDoors()
    {
        if (doorsClosed)
        {
            doorsClosed = false;
            foreach (AudioSource trainDoor in trainDoorSources)
            {
                trainDoor.clip = trainDoorOpenSound;
                trainDoor.Play();
            }
            StartCoroutine(OpenDoorDelay());
        }
    }
    public void CloseTrainDoors()
    {

        foreach (AudioSource trainDoor in trainDoorSources)
        {
            trainDoor.clip = trainDoorCloseSound;
            trainDoor.Play();
        }
        StartCoroutine(CloseDoorDelay());

    }
    IEnumerator OpenDoorDelay()
    {
        yield return new WaitForSeconds(.5f);
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("OpenDoor");
        }
        teleportSpotParent.SetActive(true);
    }
    IEnumerator CloseDoorDelay()
    {
        yield return new WaitForSeconds(.5f);
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("CloseDoor");
        }
    }
}
