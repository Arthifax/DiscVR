using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VaultScript : MonoBehaviour {

    private Animator AC;

    [SerializeField]
    private List<string> correctScrews = new List<string>();
    [SerializeField]
    private List<string> screws = new List<string>();

    [SerializeField]
    private List<Transform> allScrews = new List<Transform>();

    [SerializeField]
    private GameObject[] toDestroy;

    [SerializeField]
    GameObject vaultRond1;
    [SerializeField]
    GameObject vaultRond2;
    [SerializeField]
    GameObject vaultRond3;

    public int vaultTarget1;
    public int vaultTarget2;
    public int vaultTarget3;

    [SerializeField] float errorMargin;

    [SerializeField]
    private GameObject C4;
    [SerializeField] private GameObject player;

    private int CompletedVaultState = 0;

    [SerializeField]
    Animator fadeAC;

    [SerializeField]
    Transform invaultpos;

    bool screwDelayed = false;

    [SerializeField] private AudioClip pipeSound;
    [SerializeField] private Vector3 pipeSoundTimer;

    [SerializeField] AudioSource winMusic;
    [SerializeField] AudioSource vault1Music;
    [SerializeField] private ParticleSystem moneyMoney;

    public C4Script c4;

    [SerializeField] GameObject ovrCanvas;
    [SerializeField] GameObject vaultText;

    private void Start()
    {
        AC = GetComponent<Animator>();
    }


    public void RotateVaultHandle(Transform handle)
    {
        if (CompletedVaultState != 0) return;

            //Debug.Log(handle.name + " rotation: " + handle.transform.eulerAngles.z);
            //Debug.Log(Mathf.Abs(vaultTarget1 - Mathf.Abs(vaultRond1.transform.eulerAngles.z)));
            handle.eulerAngles += new Vector3(0, 0, 30*Time.deltaTime);
            if(handle.GetComponent<AudioSource>().isPlaying == false)
            {
            handle.GetComponent<AudioSource>().Play();
            }

            if(IsRotationWithinThreshold(vaultTarget1, vaultRond1) &&
                IsRotationWithinThreshold(vaultTarget2, vaultRond2) &&
                IsRotationWithinThreshold(vaultTarget3, vaultRond3))
            {
                //Debug.Log("COMPLETED");
                ChangeVaultCompletedState(1);

                if (handle.GetComponent<AudioSource>().isPlaying)
                    handle.GetComponent<AudioSource>().Stop();
                StartCoroutine(PlayPipeSound());
                StartCoroutine(DestroyArrows());
                Invoke("ActivateScrews", 4f);
            }
    }

    // OwO
    bool IsRotationWithinThreshold(int vaultTarget, GameObject obj)
    {
        float angle = obj.transform.eulerAngles.z;
        float offset = Mathf.Abs(vaultTarget - angle),
            reversedOffset = Mathf.Abs(vaultTarget + 360 - angle);
        float correctOffset = Mathf.Min(offset, reversedOffset);
        return correctOffset < errorMargin;
    }
    
    private void ActivateScrews()
    {
        // start screw lightup here
        foreach (Transform g in allScrews)
        {
            g.GetComponent<Animator>().SetTrigger("Activate");
        }
    }

    IEnumerator DestroyArrows()
    {
        yield return new WaitForSeconds(0.75f);
        foreach (GameObject g in toDestroy)
        {
            Destroy(g.gameObject);
        }
    }

    public void ChangeScrew(Transform anchortrans)
    {
        if (CompletedVaultState != 1) return;

        if (screwDelayed) return;
        //Animation voor screws
        if (!anchortrans.GetComponentInChildren<Animator>().GetBool("open"))
        {
            anchortrans.GetComponentInChildren<Animator>().SetBool("open", true);
            screws.Add(anchortrans.gameObject.name);
            screws.Sort();
            if (CompareScrews())
            {
                SetToChangeVaultstate();
            }
        }
        else 
        {
            anchortrans.GetComponentInChildren<Animator>().SetBool("open", false);
            screws.Remove(anchortrans.gameObject.name);
            screws.Sort();
            if (CompareScrews())
            {
                ChangeVaultCompletedState(2);
            }
            else
            {
                //Debug.Log("INCORRECT");
            }
        }

        if (anchortrans.GetComponent<AudioSource>().isPlaying == false)
        {
            anchortrans.GetComponent<AudioSource>().Play();
            StartCoroutine(StopScrewSound(anchortrans.GetComponent<AudioSource>()));
        }
        StartCoroutine(ScrewDelayer());
    }

    IEnumerator StopScrewSound(AudioSource screwSound)
    {
        yield return new WaitForSeconds(0.85f);
        screwSound.Stop();
    }

    public void SetToChangeVaultstate()
    {   
        ChangeVaultCompletedState(2);
        c4.boxcol.enabled = true;
    }

    private bool CompareScrews()
    {
        bool isEqual = true;
        if(screws.Count != 4)isEqual = false;
        else {
            for(int i = 0; i < 4; i++)
            {
                if(screws[i] != correctScrews[i])
                {
                    isEqual = false;
                }
            }
        }
        return isEqual;
    }

    public void Win()
    {
        ChangeVaultCompletedState(3);
        StartCoroutine(Waitforvaultdoor());

        winMusic.Play();
        vault1Music.Stop();
        if (moneyMoney != null && moneyMoney.gameObject.activeInHierarchy == false)
            moneyMoney.gameObject.SetActive(true);

    }

    IEnumerator Waitforvaultdoor()
    {

        yield return new WaitForSeconds(0.75f);
        transform.Find("vaultWithCollider").GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2.25f);

        player.transform.position = invaultpos.position;
        ovrCanvas.SetActive(true);
        if (vaultText.activeInHierarchy == false) { vaultText.SetActive(true); }
        //ovrCanvas.GetComponentInParent<OculusGoRemoteInput>().mayShootRay = false;
    }

    private void ChangeVaultCompletedState(int toState){
        CompletedVaultState = toState;
        AC.SetInteger("vaultState", toState);
    }

    IEnumerator ScrewDelayer()
    {
        screwDelayed = true;
        yield return new WaitForSeconds(0.1f);
        screwDelayed = false;
    }

    IEnumerator PlayPipeSound()
    {
        yield return new WaitForSeconds(pipeSoundTimer.x);
        GetComponent<AudioSource>().PlayOneShot(pipeSound, 0.75f);
        yield return new WaitForSeconds(pipeSoundTimer.y);
        GetComponent<AudioSource>().PlayOneShot(pipeSound, 0.75f);
        yield return new WaitForSeconds(pipeSoundTimer.z);
        GetComponent<AudioSource>().PlayOneShot(pipeSound, 0.75f);
        yield return null;
    }
}
