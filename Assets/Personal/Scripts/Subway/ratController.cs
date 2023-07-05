using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratController : MonoBehaviour
{
    public Animator animator;
    int loop = 0;
    [SerializeField] private GameObject rat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle1"))
        {
            loop = 0;
            if (rat.tag == "BigRat")
            {
                animator.SetBool("up", false);
            }
            if (!AnimatorIsPlaying()){
                if (animator.GetBool("up").Equals(true))
                {
                    animator.Play("rat_walk");
                }
                else
                {
                    animator.SetBool("up", true);
                    animator.Play("rat_idle_2");
                }
            }
        }

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
            {
            animator.SetBool("up", false);
            if (!AnimatorIsPlaying() && loop >= 10){
                animator.SetBool("up", false);
                animator.Play("rat_idle_1");
            }
            //loop += (int)Time.deltaTime % 60;
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle2"))
            {
            animator.SetBool("up", true);
            if (!AnimatorIsPlaying())
            {
                animator.Play("rat_idle_1");
            }
        }
    }
}
