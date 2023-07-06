using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFX_MoneyCollision : MonoBehaviour {

    ParticleSystem.CollisionModule coll;
    ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start () {
        ps = GetComponent<ParticleSystem>();
        coll = ps.collision;
    }
	
	void Update () {
		//if (ParticleSystem.CollisionModule 
	}

    private void OnParticleCollision(GameObject other)
    {
        if (ps.noise.enabled)
        {
            //ps.GetParticles()
        }
    }
}
