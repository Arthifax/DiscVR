using UnityEngine;

public class SeagullBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject seagull;
    [SerializeField]
    AudioClip flyingTrashcanNoise;
    AudioSource audioSource;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = seagull.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
	}

    void StartWalking()
    {
        anim.SetBool("isIdle", false);
    }

    void StopWalking()
    {
        anim.SetBool("isIdle", true); ;
    }

    // start making sound / being annoying
    void BeAnnoying()
    {
        audioSource.PlayOneShot(flyingTrashcanNoise);
    }
}
