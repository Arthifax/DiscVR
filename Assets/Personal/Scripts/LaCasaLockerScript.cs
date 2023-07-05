using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class LaCasaLockerScript : MonoBehaviour
{

    private bool[] lockersOpened;
    private bool middleLockerLocked;

    [SerializeField]
    private GameObject[] numPad;
    [SerializeField] private GameObject mapTeleport;

    [SerializeField]
    private GameObject Lock;
    private Animator LockAC;
    private BoxCollider LockCol;

    public bool lockZoomed;
    public bool lockCorrect;

    [SerializeField]
    private int[] correctAnswers;
    private int[] currentAnswers;

    [SerializeField]
    private PuzzleManager pzlManager;
    [SerializeField]
    private bool isLockerLock;

    private bool rotatingNum = false;

    [SerializeField] private AudioClip correct = null;
    [SerializeField] private AudioSource source = null;


    [SerializeField]
    private UnityEvent OnLockCorrect = new UnityEvent();

    private void Start()
    {
        lockCorrect = false;
        middleLockerLocked = true;

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

    public bool IsLockerOpenable(GameObject locker)
    {
        if (lockZoomed) return false;

        if (locker.name.Contains("0") && !lockersOpened[0])
        {
            locker.transform.GetComponent<AudioSource>().Play();
            return true;
        }
        else if (locker.name.Contains("1") && !lockersOpened[1])
        {
            if (!middleLockerLocked)
            {
                locker.transform.GetComponent<AudioSource>().Play();
                return true;
            }
            else
            {
                ZoomLock();
                return false;
            }

        }
        else if (locker.name.Contains("2") && !lockersOpened[2])
        {
            locker.transform.GetComponent<AudioSource>().Play();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SetLockerOpen(GameObject locker)
    {
        if (locker.name.Contains("0"))
        {
            lockersOpened[0] = true;
        }
        else if (locker.name.Contains("1"))
        {
            lockersOpened[1] = true;
            mapTeleport.SetActive(true);
        }
        else if (locker.name.Contains("2"))
        {
            lockersOpened[2] = true;
        }
    }

    /// <summary>
    /// Wordt geactiveerd als lock niet gekraakt is en speler klikt op deur
    /// </summary>
    public void ZoomLock()
    {
        if (!lockZoomed)
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
        if (currentAnswers[whatLock] == 9 && multiplier == 1)
        {
            currentAnswers[whatLock] = 0;
        }
        else if (currentAnswers[whatLock] == 0 && multiplier == -1)
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
        //Debug.Log("----------------------------------CORRECT--------------------------------------");
        LockAC.SetBool("Correct", true);
        lockCorrect = true;
        StartCoroutine(UnLockLock());
        Lock.transform.Find("Lock").GetComponent<AudioSource>().Play();
        PlaySound(correct, .5f);
        OnLockCorrect?.Invoke();

    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
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
        if (lockZoomed)
        {
            lockZoomed = false;
            //Debug.Log(isLockerLock);
        }
        isLockerLock = true;
        if (isLockerLock)
        {
            StartCoroutine(MiddleOpenable());
        }
    }

    IEnumerator MiddleOpenable()
    {
        //yield return new WaitForSeconds(0.45f);
        yield return new WaitForSeconds(0.05f);
        middleLockerLocked = false;
        Lock.GetComponent<AudioSource>().Play();
    }

    public void IsOpenable(GameObject locker, Animation animation,string lockerName)
    {
        if (IsLockerOpenable(locker))
        {
            animation.Play();
            SetLockerOpen(locker);
        }

    }
}
