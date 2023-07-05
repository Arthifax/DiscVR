using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    [SerializeField] private CountDownClock countDownClock;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GlassTeleportScript teleportScript;
    [SerializeField] private RotateFallingCamera rotateFallingCamera;
    [SerializeField] private bool Breakable;
    [SerializeField] private List<BoxCollider> colliders;
    [SerializeField] private List<BoxCollider> previousColliders;
    private AudioSource audioSource;
    [SerializeField] private Material material;
    [SerializeField] private Color mainColorStart;
    [SerializeField] private Color specularStart;
    [SerializeField] private Color mainColorEnd;
    [SerializeField] private Color specularEnd;
    private Color currentColor;
    private Color currentSpecular;
    public bool colliderHit;
    private bool isCycling = false;
    private float interruptedChange = 0;
    private float switchDelaySeconds = 5;
    private float time = 0;
    private bool TimeOut = false;
    private WaitForSeconds colliderDisableDelay = new WaitForSeconds(.2f);
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int correctPoints, wrongPoints;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainColorStart.a = 255;
        mainColorEnd.a = 255;
        specularStart.a = 255;
        specularEnd.a = 255;
        currentColor = mainColorStart;
        currentSpecular = specularStart;
        material.SetColor("_BaseColor", mainColorStart);
        material.SetColor("_SpecColor", specularStart);
        countDownClock.OutOfTime.AddListener(SetTimeOut);
    }
    public void SetTimeOut()
    {
        TimeOut = true;
        GlassBreaks();
    }

    public void GlassBreaks()
    {
        if (Breakable || TimeOut)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioSource.Play();
            rotateFallingCamera.SetFalling();
            teleportScript.enabled = false;
            playerRb.isKinematic = false;
            playerRb.useGravity = true;
            pointManager.AddPoints(-wrongPoints);
        }
        else
        {
            pointManager.AddPoints(correctPoints);
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }
    public void ChangeColor()
    {
        if (colliderHit)
        {
            CoroutineStop();

            SetTime();

            StartCoroutine(CycleMaterial(currentColor, mainColorEnd, currentSpecular, specularEnd, time));

        }
        else
        {
            CoroutineStop();

            SetTime();

            StartCoroutine(CycleMaterial(currentColor, mainColorStart, currentSpecular, specularStart, time));
        }
    }

    private void CoroutineStop()
    {
        if (isCycling)
        {
            StopAllCoroutines();
            isCycling = false;
        }
    }

    private void SetTime()
    {
        if (interruptedChange != switchDelaySeconds)
        {
            time = switchDelaySeconds - interruptedChange;
        }
        else
        {
            time = switchDelaySeconds;
        }
    }


   private IEnumerator CycleMaterial(Color startColor, Color endColor, Color startSpecularColor, Color endSpecularColor, float switchtime)
    {
        Debug.Log("Cycling");
        Debug.Log(switchtime);
        float cycleTime = switchtime;
        interruptedChange = 0;
        isCycling = true;
        while (interruptedChange < cycleTime)
        {
            interruptedChange += Time.deltaTime;
            float t = interruptedChange / cycleTime;
            currentColor = Color.Lerp(startColor, endColor, t);
            Debug.Log(currentColor);
            material.SetColor("_BaseColor", currentColor);
            currentSpecular = Color.Lerp(startSpecularColor, endSpecularColor, t);
            Debug.Log(currentSpecular);
            material.SetColor("_SpecColor", currentSpecular);
            yield return null;
        }
        isCycling = false;
    }
    public void disableColliders()
    {
        if (previousColliders.Count > 0)
        {
            foreach (BoxCollider collider in previousColliders)
            {
                Destroy(collider);
            }
            previousColliders.Clear();
        }
        colliderHit = false;
    }
}
