using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TugOfWarManager : MonoBehaviour
{
    [SerializeField] private List<int> contestantList;
    [SerializeField] private int index = 0;
    [SerializeField] private int tries = 0;
    [SerializeField] private List<GameObject> Left;
    [SerializeField] private List<GameObject> Right;
    [SerializeField] private List<GameObject> enemys;
    [SerializeField] private List<GameObject> ally;
    [SerializeField] private List<GameObject> moveObjects;
    [SerializeField] private GameObject teleportPoints;
    [SerializeField] private bool move;
    [SerializeField] private bool updateMove;
    [SerializeField] private float moveDirection;
    [SerializeField] private float moveSpeed = 1;
    private bool enemyWon;
    private bool allyWon;
    public UnityEvent tryEvent;
    public UnityEvent failEvent;
    public UnityEvent winEvent;
    [Space]
    [Header("Audio")]
    public AudioSource AudioOutput;
    public AudioSource AmbientSounds;
    public AudioClip TugAudio;
    public AudioClip CheerAudio;
    public AudioClip screemAudio;
    public AudioClip Yell;
    [Space]
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int correctPoints, wrongPoints;
    private bool canComplete = true;



    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in enemys)
        {
            moveObjects.Add(obj);
        }
        foreach (GameObject obj in ally)
        {
            moveObjects.Add(obj);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()  
    {
        if (move)
        {
            Debug.Log("AHHHHHHHHHHHHHHHHHHAAAHAHAHAH");
            foreach (GameObject obj in moveObjects)
            {
                obj.transform.position += new Vector3(moveDirection, 0, 0);
            }
            updateMove = true;
            tryEvent.Invoke();
        }
        if (allyWon)
        {
            foreach (GameObject obj in enemys)
            {
                obj.transform.position += new Vector3(0.2f, 0, 0);
            }
            updateMove = true;
            if (canComplete)
            {
            winEvent.Invoke();
            canComplete = false;
            }

        }
        if (enemyWon)
        {
            foreach (GameObject obj in ally)
            {
                obj.transform.position += new Vector3(-0.2f, 0, 0);
            }
            updateMove = true;
            if (canComplete)
            {
                failEvent.Invoke();
                canComplete = false;
            }

        }

    }
    public void GetContestant(int number)
    {
        tries++;
        if (number == contestantList[index])
        {
            index++;
            StartCoroutine(pullWait());
            PlayMatch(1);

        }
        else
        {
            StartCoroutine(pullWait());
            PlayMatch(0);

        }
    }

    private void PlayMatch(int value)
    {
        updateMove = true;
        Debug.Log("playing match");
        if (value == 1)
        {
            
            Debug.Log("correct number");
            StartCoroutine(ChangeAnimation(0.0f, true, "NotCorrect", "Correct"));
            moveDirection = moveSpeed;
            pointManager.AddPoints(correctPoints);
        }
        if (value == 0)
        {
            StartCoroutine(ChangeAnimation(0.0f, true, "Correct", "NotCorrect"));
            moveDirection = -moveSpeed;
            pointManager.AddPoints(wrongPoints);
        }
    }

    public void klaarstaan()
    {
        StartCoroutine(klaarstaanwachten(6));
        AmbientSounds.clip = TugAudio;
        AmbientSounds.Play();
    }

    IEnumerator pullWait()
    {
        teleportPoints.SetActive(false);
        move = false;
        yield return new WaitForSeconds(0.3f);
        move = true;
        yield return new WaitForSeconds(0.1f);
        move = false;
        yield return new WaitForSeconds(0.3f);
        move = true;
        yield return new WaitForSeconds(0.1f);
        move = false;
        yield return new WaitForSeconds(3f);
        if (index <= 1 && tries <= 1)
        {
            StartCoroutine(ChangeAnimation(0.0f, false, "Correct", "NotCorrect"));
            StartCoroutine(ChangeAnimation(0.0f, false, "NotCorrect", "Correct"));
            StartCoroutine(ChangeAnimation(0.0f, true, "Back", "Back"));
            StartCoroutine(ChangeAnimation(2f, false, "Back", "Back"));
            PlayAudioTeams(Yell, Yell);
        }
        if (index <= 1 && tries > 1)
        {
            StartCoroutine(ChangeAnimation(0.0f, true, "Win", "Fail"));
            AmbientSounds.Stop();
            PlayAudioTeams(CheerAudio,screemAudio);
            enemyWon = true;
        }
        if (index > 1 && tries > 1)
        {
            StartCoroutine(ChangeAnimation(0.0f, true, "Fail", "Win"));
            AmbientSounds.Stop();
            PlayAudioTeams(screemAudio, CheerAudio);
            allyWon = true;
        }
        teleportPoints.SetActive(true);
    }

    IEnumerator klaarstaanwachten(int amount)
    {
        yield return new WaitForSeconds(amount);
        foreach (GameObject objects in Left)
        {
            if (objects.GetComponent<Animator>() != null)
            {
                objects.GetComponent<Animator>().SetInteger("Direction", 1);
            }
        }
        foreach (GameObject objects in Right)
        {
            if (objects.GetComponent<Animator>() != null)
            {
                objects.GetComponent<Animator>().SetInteger("Direction", 0);
            }
        }
    }

    IEnumerator ChangeAnimation(float amount, bool animationState, string animationNameEnemy, string animationNameAlly)
    {
        yield return new WaitForSeconds(amount);
        foreach (GameObject objects in enemys)
        {
            Debug.Log("Changing animation");
            if (objects.GetComponent<Animator>() != null)
            {
                Debug.Log("Has a animation");
                objects.GetComponent<Animator>().SetBool(animationNameEnemy, animationState);
            }
        }
        foreach (GameObject objects in ally)
        {
            if (objects.GetComponent<Animator>() != null)
            {
                objects.GetComponent<Animator>().SetBool(animationNameAlly, animationState);
            }
        }

    }
    public void PlayAudioTeams(AudioClip EnemyAudio, AudioClip AllyAudio)
    {
        foreach (GameObject objects in enemys)
        {
            if (objects.GetComponent<AudioSource>() != null)
            {
                AudioSource audio = objects.GetComponent<AudioSource>();
                audio.pitch = Random.Range(0.75f, 1.3f);
                audio.clip = EnemyAudio;
                audio.Play();
            }
        }
        foreach (GameObject objects in ally)
        {
            if (objects.GetComponent<AudioSource>() != null)
            {
                AudioSource audio = objects.GetComponent<AudioSource>();
                audio.pitch = Random.Range(0.75f, 1.3f);
                audio.clip = AllyAudio;
                audio.Play();
            }
        }

    }




}
