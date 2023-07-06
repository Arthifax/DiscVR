using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
    public PointsScriptableObject pointsObject;
    public TextMeshPro myText;
    public float speed = 1;
    [Space]
    public bool reset = false;
    public bool update = false;
    public bool canUpdate = false;

    private int pointsAdded;
    private int startPoints;
    [Space] 
    public string scoreText = "Won: ";
    [Space]
    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip midSound;
    public AudioClip endSoundGood;
    public AudioClip endSoundBad;
    private AudioClip currentEndClip;
    
    public GameObject coinVFX;
    public GameObject wrongX;
    
    // Start is called before the first frame update
    void Start()
    {
        myText.SetText(scoreText + string.Format("{0:N0}", pointsObject.points));
        startPoints = pointsObject.points;
        pointsAdded = 0;
        canUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUpdate)
        {
            canUpdate = false;
            if (reset)
            {
                reset = false;
                pointsObject.points = 0;
                myText.SetText(scoreText + string.Format("{0:N0}", pointsObject.points));
                startPoints = 0;
                pointsAdded = 0;
            }
            if (update)
            {
                update = false;
                UpdatePoints();
                canUpdate = true;
            }
            if (pointsObject.points < (startPoints + pointsAdded) || pointsObject.points > (startPoints + pointsAdded))
            {
                wrongX.SetActive(false);
                if (Mathf.RoundToInt((startPoints + pointsAdded) - pointsObject.points) * speed > 1)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = midSound;
                        audioSource.Play();
                    }
                    pointsObject.points += Mathf.RoundToInt(((startPoints + pointsAdded) - pointsObject.points) * speed);
                    currentEndClip = endSoundGood;
                    coinVFX.SetActive(true);
                }
                else if (Mathf.RoundToInt(pointsObject.points - (startPoints + pointsAdded)) * speed > 1)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = midSound;
                        audioSource.Play();
                    }
                    pointsObject.points -= Mathf.RoundToInt((pointsObject.points - (startPoints + pointsAdded)) * speed);
                    currentEndClip = endSoundBad;
                    wrongX.SetActive(true);
                }
                else if (pointsObject.points < (startPoints + pointsAdded) && pointsAdded > 0)
                {
                    pointsObject.points += 1;
                    
                }
                else if (pointsObject.points > (startPoints + pointsAdded) && pointsAdded < 0)
                {
                    pointsObject.points -= 1;
                }
                UpdatePoints();
            }
            else
            {
                audioSource.clip = currentEndClip;
                audioSource.Play();
            }
        }
    }
    void UpdatePoints()
    {
        myText.SetText(scoreText + string.Format("{0:N0}", pointsObject.points));
        canUpdate = true;
    }

    public void AddPoints(int amountOfPoints)
    {
        if (pointsObject.points < (startPoints + pointsAdded))
        {
            pointsObject.points = (startPoints + pointsAdded);
        }
        if (pointsObject.points > (startPoints + pointsAdded))
        {
            pointsObject.points = (startPoints + pointsAdded);
        }
        startPoints = pointsObject.points;
        pointsAdded = amountOfPoints;
        canUpdate = true;

    }
}
