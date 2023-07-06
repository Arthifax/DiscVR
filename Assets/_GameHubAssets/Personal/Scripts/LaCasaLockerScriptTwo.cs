using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaCasaLockerScriptTwo : MonoBehaviour {

    private bool[] lockersOpened;

    [SerializeField]
    private GameObject[] numPad;

    [SerializeField]
    private GameObject Lock;
    public Animator LockAC;
    private BoxCollider LockCol;

    public bool lockZoomed;
    public bool lockCorrect;

    [SerializeField]
    private int[] correctAnswers;
    private int[] currentAnswers;

    //public LevelSelectScript levelscript;

    [SerializeField]
    private Scene0Manager Manager;

    private bool rotatingNum = false;

    [SerializeField] private AudioClip victory = null;
    [SerializeField] private AudioSource source = null;

    [SerializeField] private Transform player;
    [SerializeField] private Transform teleportLocationLock2;

    private void Awake()
    {
        lockCorrect = false;

        StartCoroutine(WaitForFirstFrame());
        lockersOpened = new bool[3];
        for (int i = 0; i < lockersOpened.Length; i++)
        {
            lockersOpened[i] = false;
        }

        LockAC = Lock.GetComponent<Animator>();
        LockCol = Lock.GetComponent<BoxCollider>();
        LockCol.enabled = false;
        lockZoomed = false;

        currentAnswers = new int[4];
        currentAnswers[0] = 5;
        currentAnswers[1] = 0;
        currentAnswers[2] = 3;
        currentAnswers[3] = 8;
    }

    IEnumerator WaitForFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        foreach (Animation lockerAnim in transform.GetComponentsInChildren<Animation>())
        {
            lockerAnim.Stop();
        }
    }

    public void ZoomLock()
    {
        if (!lockZoomed && lockCorrect == false&&player.position.x==teleportLocationLock2.position.x&&player.position.z==teleportLocationLock2.position.z)
        {
            LockAC.SetBool("MoveForward", true);
            StartCoroutine(LockFocusBoolDelay(true));
        }
    }

    public void NotLockFocus()
    {
        if (!lockZoomed) return;
        if (lockCorrect) return;

        LockAC.SetBool("MoveForward", false);
        StartCoroutine(LockFocusBoolDelay(false));
    }

    IEnumerator LockFocusBoolDelay(bool setZoomed)
    {
        yield return new WaitForSeconds(0.67f);
        lockZoomed = setZoomed;
    }

    public void RotateNum(string objName)
    {
        if (lockCorrect) return;
        if (!lockZoomed) return;
        if (rotatingNum) return;

        Lock.GetComponentInChildren<Canvas>().GetComponent<AudioSource>().Play();

        int multiplier = 0;
        if (objName.Contains("L"))
        {
            multiplier = -1;
        }
        else if (objName.Contains("R"))
        {
            multiplier = 1;
        }

        if (objName.Contains("0"))
        {
            AddToCurrentLockNumber(0, multiplier);
            numPad[0].transform.Rotate(0, 36 * multiplier, 0);
        }
        else if (objName.Contains("1"))
        {
            AddToCurrentLockNumber(1, multiplier);
            numPad[1].transform.Rotate(0, 36 * multiplier, 0);
        }
        else if (objName.Contains("2"))
        {
            AddToCurrentLockNumber(2, multiplier);
            numPad[2].transform.Rotate(0, 36 * multiplier, 0);
        }
        else if (objName.Contains("3"))
        {
            AddToCurrentLockNumber(3, multiplier);
            numPad[3].transform.Rotate(0, 36 * multiplier, 0);
        }

        StartCoroutine(RotateDelay());
        
    }

    private void AddToCurrentLockNumber(int whatLock, int multiplier)
    {
        if(currentAnswers[whatLock] == 9 && multiplier == 1)
        {
            currentAnswers[whatLock] = 0;
        }
        else if(currentAnswers[whatLock] == 0 && multiplier == -1)
        {
            currentAnswers[whatLock] = 9;
        }
        else
        {
            currentAnswers[whatLock] += multiplier;
        }

        if (currentAnswers.SequenceEqual(correctAnswers))
        {
            LockCorrect();
        }
    }

    public void LockCorrect()
    {
        lockCorrect = true;
        LockAC.SetBool("Correct", true);
        StartCoroutine(UnLockLock());
        Lock.transform.Find("Lock").GetComponent<AudioSource>().Play();
        PlaySound(victory, .5f);
        Manager.LastPuzzleCompleted();
    }

    IEnumerator RotateDelay()
    {
        rotatingNum = true;
        yield return new WaitForSeconds(0.1f);
        rotatingNum = false;
    }

    IEnumerator UnLockLock()
    {
        yield return new WaitForSeconds(2.2f);
        Destroy(LockAC);
        Lock.AddComponent<Rigidbody>();
        Lock.GetComponent<Rigidbody>().angularVelocity = new Vector3(3, -3, 0);
        LockCol.enabled = true;
        StartCoroutine(MiddleOpenable());
    }

    IEnumerator MiddleOpenable()
    {
        yield return new WaitForSeconds(0.45f);
        lockZoomed = false;
        Lock.GetComponent<AudioSource>().Play();
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
}
