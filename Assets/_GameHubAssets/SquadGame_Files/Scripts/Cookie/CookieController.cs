using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    Transform originalParent;
    [SerializeField] private Vector3 originalPosition;
    Quaternion originalRotation;

    [SerializeField] private CompletedCookiesValidator cookiesValidator;
    [SerializeField] private List<GameObject> CookiePartsInOrder;
    [SerializeField] private CookieClick cookieClick;
    [SerializeField] private List<Transform> CookieClickablePositions;
    [SerializeField] private List<Transform> CookieMovePoints;
    [SerializeField] private bool CorrectCookie;
    [SerializeField] private BoxCollider CookieLid;
    private List<Vector3> CookieClickPositionVector = new List<Vector3>();
    private int CookieIndex = 0;
    public bool CookieComplete = false;
    private bool allowClicking = true;
    private Transform cookieTeleport;
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> soundClips;
    [SerializeField] private GatherPoints gatherPoints;
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int minusPointsWrong;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        //foreach (Transform ClickTransform in CookieClickablePositions)
        //{
        //    Vector3 ClickVector = ClickTransform.position;
        //    ClickVector.y = ClickVector.y + 1.634f;
        //    CookieClickPositionVector.Add(ClickVector);
        //}
        foreach (GameObject CookiePart in CookiePartsInOrder)
        {
            if (CookiePart.GetComponent<MeshCollider>())
                CookiePart.GetComponent<MeshCollider>().enabled = false;
        }
        if (CookieLid != null)
            CookieLid.enabled = false;
    }

    public void CookieClicked(GameObject cookieClicked)
    {
        if (allowClicking)
        {
            if (!CookieComplete)
            {
                if (cookieClicked != CookiePartsInOrder[CookieIndex])
                {
                    pointManager.AddPoints(-minusPointsWrong);
                    ResetCookie();
                }
                else
                {
                    CookieIndex++;
                    cookieClicked.SetActive(false);
                }

                if (CookieIndex == CookiePartsInOrder.Count)
                {
                    cookieClick.ClearCookieComplete();
                    CookieComplete = true;
                    cookiesValidator.CookieCompleted();
                    gatherPoints.opdrachten[0] = true;
                    gatherPoints.questCheck = true;
                    audioSource.clip = soundClips[1];
                    audioSource.Play();
                    ReturnCookie();
                }
            }
        }
    }

    public void ZoomCookie()
    {
        if (CookieComplete)
        {
            return;
        }
        if (CorrectCookie)
        {
            foreach (GameObject Cookie in CookiePartsInOrder)
            {
                Cookie.GetComponent<MeshCollider>().enabled = true;
            }
            CookieLid.enabled = true;
        }
        else
        {
            
            audioSource.clip = soundClips[0];
            audioSource.Play();
            pointManager.AddPoints(-minusPointsWrong);
        }

        //bool match = false;
        //int index = 0;
        //foreach (Vector3 Position in CookieClickPositionVector)
        //{
        //    if (Player.transform.position == Position)
        //    {
        //        match = true;
        //        break;
        //    }
        //    else
        //    {
        //        index++;
        //    }
        //}
        //if (!match)
        //{
        //    m_TextMeshPro.text = match.ToString();
        //    return;
        //}
        //transform.parent = CookieMovePoints[index];
        transform.parent = cookieTeleport;
        GetComponent<BoxCollider>().enabled = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void ReturnCookie()
    {
        if (CorrectCookie)
        {
            foreach (GameObject Cookie in CookiePartsInOrder)
            {
                if (Cookie.activeSelf)
                    Cookie.GetComponent<MeshCollider>().enabled = false;
            }
        }

        if (!CookieComplete)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        CookieLid.enabled = false;
        transform.parent = originalParent;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }
    private void ResetCookie()
    {
        audioSource.clip = soundClips[0];
        audioSource.Play();
        CookieIndex = 0;
        foreach (GameObject gameObject in CookiePartsInOrder)
        {
            gameObject.SetActive(true);
        }
    }
    public void SetCookiePosition(Transform transform)
    {
        cookieTeleport = transform;
    }
}
