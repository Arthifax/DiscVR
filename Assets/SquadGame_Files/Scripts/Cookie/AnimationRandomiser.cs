using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomiser : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        int animation = Random.Range(0, 4);

        switch (animation)
        {
            case 0:
                animator.SetTrigger("Pose1");
                break;
            case 1:
                animator.SetTrigger("Pose2");
                break;
            case 2:
                animator.SetTrigger("Pose3");
                break;
            case 3:
                animator.SetTrigger("Pose4");
                break;
            case 4:
                animator.SetTrigger("Pose5");
                break;
        }
    }
}
