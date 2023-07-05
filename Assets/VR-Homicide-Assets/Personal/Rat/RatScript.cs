using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatScript : MonoBehaviour {

    NavMeshAgent agent;
    Transform target;
    public Transform[] targets;

    public Animator myAnimator;

    public GameObject[] bloodSplatters;
    bool dead;

    public AudioClip[] deathSounds;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = targets[Random.Range(0,targets.Length)];
        agent.destination = target.position;
	}

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (Vector3.Distance(agent.transform.position, target.position) <= agent.stoppingDistance)
            {
                target = targets[Random.Range(0, targets.Length)];
                agent.destination = target.position;
                // Target reached
            }
        }
    }

    public void HitByPlayer()
    {
        if (!dead)
        {
            dead = true;
            myAnimator.SetBool("hit", true);
            tag = "Untagged";
            agent.destination = transform.position;
            Destroy(agent);
            Instantiate(bloodSplatters[Random.Range(0, bloodSplatters.Length)], transform.position, Quaternion.Euler(-90, 0, 0));
            GetComponent<AudioSource>().PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)]);
        }
    }
}
