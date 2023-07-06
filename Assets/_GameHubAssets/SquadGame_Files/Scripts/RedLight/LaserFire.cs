using System.Collections;
using System.Collections.Generic;
using Trickstorm;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LevelLoaderRedLight levelLoaderRedLight;
    [SerializeField] private GameObject head;
    [SerializeField] private AudioSource laserPew;
    [SerializeField] private List<LaserBeam> lasers;
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private GameObject player;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private List<LookAtPlayer> laserPoint;
    [SerializeField] private Material laserEyes;
    public Color startColor, endColor;
    private List<GameObject> laserTargetsList;
    private bool playerShot = false;
    private WaitForSeconds Delay2Seconds = new WaitForSeconds(2f);
    private WaitForSeconds Delay1Second = new WaitForSeconds(1f);
    private WaitForSeconds DelayHalfSecond = new WaitForSeconds(.5f);
    private WaitForSeconds DelayQuarterSecond = new WaitForSeconds(.2f);

    [Space]
    [Header("Point System")]
    [SerializeField] private PointManager pointManager;

    [SerializeField] private int correctAnswerPoints, wrongAnswerPoints;

    private void Awake()
    {
        laserTargetsList = new List<GameObject>();
    }
    // Update is called once per frame
    public void OutOfTime()
    {
        animator.GetComponent<Animator>().enabled = false;
        StartCoroutine(EndLaser());
    }
    public void AddLaserTarget(GameObject shootObject)
    {
        laserTargetsList.Add(shootObject);
    }

    private IEnumerator EndLaser()
    {
        int index = 0;
        backgroundAudio.Stop();
        laserPew.clip = clips[0];
        laserPew.Play();
        head.GetComponent<Animator>().SetTrigger("OutOfTime");
        yield return Delay2Seconds;
        if (laserTargetsList.Count > 0)
        {
            foreach (LookAtPlayer lookAtPlayer1 in laserPoint)
            {
                lookAtPlayer1.AddTargets(laserTargetsList);
            }
            foreach (GameObject laserTarget in laserTargetsList)
            {
                head.transform.LookAt(laserTargetsList[index].transform.position);
                yield return DelayQuarterSecond;
                foreach (LookAtPlayer lookAtPlayer in laserPoint)
                {
                    lookAtPlayer.AlignShot(index);
                }
                ColorChange(startColor, endColor);
                yield return DelayHalfSecond;
                laserPew.clip = clips[1];
                laserPew.Play();
                if (laserTarget.GetComponentInParent<NPCMoveAndFall>() != null)
                {
                    laserTarget.GetComponentInParent<NPCMoveAndFall>().deadNpc();
                }
                else
                {
                    playerShot = true;
                }
                foreach (LaserBeam laser in lasers)
                {
                    laser.LaserOn = true;
                }
                yield return Delay1Second;
                foreach (LaserBeam laser in lasers)
                {
                    laser.LaserOn = false;
                }
                ColorChange(endColor, startColor);
                index++;
            }
        }
        laserPew.clip = clips[0];
        laserPew.Play();
        head.GetComponent<Animator>().SetTrigger("Reset");
        yield return Delay2Seconds;
        if (playerShot)
        {
            pointManager.AddPoints(-wrongAnswerPoints);
            yield return Delay2Seconds;
            levelLoaderRedLight.ReloadLevel();
        }
        else
        {
            laserTargetsList.Clear();
            animator.GetComponent<Animator>().enabled = true;
            backgroundAudio.Play();
            pointManager.AddPoints(correctAnswerPoints);
        }
        levelLoaderRedLight.MethodComplete();
    }
    private void ColorChange(Color startColor, Color endColor)
    {
        laserEyes.color = Color.Lerp(startColor, endColor, 1.5f);
    }
}
