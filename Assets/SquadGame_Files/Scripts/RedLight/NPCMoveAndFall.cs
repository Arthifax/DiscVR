using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveAndFall : MonoBehaviour
{
    private Vector3 livingNpcRotation = new Vector3(0, 180, 0);
    [SerializeField] private List<Transform> positionsToJump;
    [SerializeField] private LaserFire laser;
    private int jumpPointIndex = 0;
    [SerializeField] private bool NpcDies;
    private Animator animator;
    private bool endOfPath = false;
    private bool shot = false;
    private GameObject target;
    private void Start()
    {
        target = this.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        NextPosition();
        livingNpc();
    }
    public void NextPosition()
    {
        if (!endOfPath)
        {
            transform.position = positionsToJump[jumpPointIndex].position;
            if (jumpPointIndex == 0)
            {
                int startAnim = Random.Range(0, 7);
                switch (startAnim)
                {
                    case 0:
                        animator.SetTrigger("Pose4");
                        break;
                    case 1:
                        animator.SetTrigger("Pose6");
                        break;
                    case 2:
                        animator.SetTrigger("Pose7");
                        break;
                    case 3:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 4:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 5:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 6:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 7:
                        animator.SetTrigger("defaultPose");
                        break;
                }
            }

            else
            {
                int RedLightPose = Random.Range(0, 16);
                switch (RedLightPose)
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
                        animator.SetTrigger("defaultPose");
                        break;
                    case 4:
                        animator.SetTrigger("Pose1");
                        break;
                    case 5:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 6:
                        animator.SetTrigger("Pose2");
                        break;
                    case 7:
                        animator.SetTrigger("Pose1");
                        break;
                    case 8:
                        animator.SetTrigger("Pose5");
                        break;
                    case 9:
                        animator.SetTrigger("Pose5");
                        break;
                    case 10:
                        animator.SetTrigger("Pose1");
                        break;
                    case 11:
                        animator.SetTrigger("defaultPose");
                        break;
                    case 12:
                        animator.SetTrigger("Pose5");
                        break;
                    case 13:
                        animator.SetTrigger("Pose5");
                        break;
                    case 14:
                        animator.SetTrigger("Pose3");
                        break;
                    case 15:
                        animator.SetTrigger("Pose3");
                        break;
                    case 16:
                        animator.SetTrigger("Pose3");
                        break;
                }
            }
            jumpPointIndex++;
            if (jumpPointIndex == positionsToJump.Count)
            {
                endOfPath = true;
                if (NpcDies)
                {
                    laser.AddLaserTarget(target);
                }
                else
                {
                    animator.SetTrigger("Celebrate");
                }
            }
        }
    }

    private void livingNpc()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(livingNpcRotation);
    }
    public void deadNpc()
    {
        int deathPose = Random.Range(0, 1);

        switch (deathPose)
        {
            case 0:
                animator.SetTrigger("Death1");
                break;
            case 1:
                animator.SetTrigger("Death2");
                break;
        }

    }
}
