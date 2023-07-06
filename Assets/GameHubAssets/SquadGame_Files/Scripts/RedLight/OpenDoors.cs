using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField] private List<Animator> animators;
    [SerializeField] private GameObject FirstTeleport;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenHouseDoor());
    }

    private IEnumerator OpenHouseDoor()
    {
        yield return new WaitForSeconds(5f);
        foreach(Animator animator in animators)
        {
            animator.SetTrigger("DoorOpen");
        }
        yield return new WaitForSeconds(1.5f);
        FirstTeleport.SetActive(true);
    }
}
