using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LockerScript : MonoBehaviour {

    private bool[] lockersOpened;
    public Transform door;
    public Transform rope;
    [SerializeField] private float ropeDisableWaitTime = 2;
    public Animator ropeAnim;
    public Transform chandelier;
    [SerializeField] private AudioSource chandelierCrashSfx;

    [SerializeField]
    private GameObject[] numPad;

    [SerializeField]
    private GameObject Lock;
    public Animator LockAC;
    private BoxCollider LockCol;

    public bool lockZoomed;
    private bool lockCorrect;

    [SerializeField]
    private int[] correctAnswers;
    private int[] currentAnswers;

    public LevelSelectScript levelScript;

    //[SerializeField]
    //private PuzzleManager pzlManager;

    private bool rotatingNum = false;

    private void Awake()
    {
        lockCorrect = false;

        //StartCoroutine(WaitForFirstFrame());
        //lockersOpened = new bool[3];
        //for (int i = 0; i < lockersOpened.Length; i++)
        //{
        //    lockersOpened[i] = false;
        //}

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

    //public bool IsLockerOpenable(GameObject locker)
    //{
    //    if (lockZoomed) return false;

    //    if(locker.name.Contains("0") && !lockersOpened[0])
    //    {
    //        locker.transform.GetComponent<AudioSource>().Play();
    //        return true;
    //    }
    //    else if(locker.name.Contains("1") && !lockersOpened[1])
    //    {
    //        if (!middleLockerLocked)
    //        {
    //            locker.transform.GetComponent<AudioSource>().Play();
    //            return true;
    //        }
    //        else
    //        {
    //            ZoomLock();
    //            return false;
    //        }
    //    }
    //    else if(locker.name.Contains("2") && !lockersOpened[2])
    //    {
    //        locker.transform.GetComponent<AudioSource>().Play();
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
        
    //}

    //public void SetLockerOpen(GameObject locker)
    //{
    //    if (locker.name.Contains("0"))
    //    {
    //        lockersOpened[0] = true;
    //    }
    //    else if (locker.name.Contains("1"))
    //    {
    //        lockersOpened[1] = true;
    //    }
    //    else if (locker.name.Contains("2"))
    //    {
    //        lockersOpened[2] = true;
    //    }
    //}
    
    /// <summary>
    /// Wordt geactiveerd als lock niet gekraakt is en speler klikt op deur
    /// </summary>
    public void ZoomLock()
    {
        if (!lockZoomed && lockCorrect == false)
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

        //for(int i = 0; i < currentAnswers.Length; i++)
        //{
        //    Debug.Log("Answer" + i + ": " + currentAnswers[i]);
        //}

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
        //Debug.Log("----------------------------------CORRECT--------------------------------------");
        lockCorrect = true;
        //Debug.Log(LockAC.GetBool("Correct"));
        LockAC.SetBool("Correct", true);
        StartCoroutine(UnLockLock());
        Lock.transform.Find("Lock").GetComponent<AudioSource>().Play();
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
        //pzlManager.PuzzleCompleted();
        Lock.GetComponent<AudioSource>().Play();
        if (door != null && door.GetComponent<RestaurantDoorScript>() == true)
        {
            door.GetComponent<HomicideDoorScript>().canGoNext = true;
            door.GetComponent<MeshRenderer>().material = levelScript.activeMaterial;
        }
        if (rope != null)
        {
            chandelier.GetComponent<Rigidbody>().isKinematic = false;
            //rope should dissappear here
            ropeAnim.SetTrigger("Activate");
            yield return new WaitForSeconds(.88f);
            chandelierCrashSfx.Play();
            yield return new WaitForSeconds(ropeDisableWaitTime);
            rope.gameObject.SetActive(false);
        }
    }
}
