/// <summary>
/// 
/// Demonic doll
/// 
/// This is a simple script JUST for showcase purposes (test scene). It coordinates some sounds based on variable iterations and pre-made transitions (animController demo).
/// 
/// NOTE> I do not give support for this script. Feel free to tweak and use it as a base for your own sounds/transitions.
/// 
/// 
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class HauntedDemonicDoll : MonoBehaviour
{
    [SerializeField] LaserFire laserFire;
    [SerializeField] AudioClip[] sounds;

    private bool gameOver = false;
    private AudioSource _audioSource;
    private int _iterations = 0;

    //anim hashes
    int neckTurn = Animator.StringToHash("Look right");
    int neckBack = Animator.StringToHash("Return");
    int evilLaugh = Animator.StringToHash("Subtle Laugh");
    int lookLeft = Animator.StringToHash("Look left");
    int neckBack2 = Animator.StringToHash("Return from look left");
    int seek = Animator.StringToHash("Seek");
    int demonicMov = Animator.StringToHash("Creepy Movement");




    void Awake()
    {

        _audioSource = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start()
    {

        StartCoroutine(_mecanimSound());

    }


    private IEnumerator _mecanimSound()
    {

        Animator thisAnim = GetComponent<Animator>();

        while (!gameOver)
        {
            switch (_iterations)
            {

                case (int)iterationsName.neckTurn:

                    yield return StartCoroutine(_msc(thisAnim, neckTurn));

                    break;


                case (int)iterationsName._return:


                    yield return StartCoroutine(_msc(thisAnim, neckBack));

                    break;



                case (int)iterationsName.subtleLaugh:

                    yield return StartCoroutine(_msc(thisAnim, evilLaugh));


                    break;


                case (int)iterationsName.neckturn02:

                    yield return StartCoroutine(_msc(thisAnim, lookLeft));


                    break;

                case (int)iterationsName._return2:

                    yield return StartCoroutine(_msc(thisAnim, neckBack2));


                    break;

                case (int)iterationsName.seek:

                    yield return StartCoroutine(_msc(thisAnim, seek));


                    break;

                case (int)iterationsName.demonicMov:

                    yield return StartCoroutine(_msc(thisAnim, demonicMov));


                    break;

            }
            yield return null;
        }
    }



    private IEnumerator _msc(Animator thisAnim, int clip)
    {

        //first iteration
        if (_iterations == (int)iterationsName.neckTurn)
        {

            while (!gameOver)
            {

                if ((thisAnim.GetCurrentAnimatorStateInfo(0).shortNameHash == clip) && !GetComponent<AudioSource>().isPlaying)
                {

                    //neck turn
                    _audioSource.clip = sounds[0];
                    _audioSource.Play();

                    yield return new WaitForSeconds(_audioSource.clip.length);
                    _audioSource.Stop();

                    yield return StartCoroutine(__nextIteration());
                    yield break;

                }
                else
                {

                    yield return null;
                }

            }


        }

        //Return iteration
        if (_iterations == (int)iterationsName._return)
        {

            yield return new WaitForSeconds(1.35f);

            if (_audioSource.clip != sounds[1])
            {
                _audioSource.clip = sounds[1];
            }

            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);

            yield return StartCoroutine(__nextIteration());
            yield break;

        }


        if (_iterations == (int)iterationsName.subtleLaugh)
        {

            yield return new WaitForSeconds(.3f);
            _audioSource.clip = sounds[2];
            _audioSource.volume = 0.5f;
            _audioSource.Play();

            //Debug.Log("Laugh");
            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.Stop();


            yield return StartCoroutine(__nextIteration());
            yield break;

        }

        if (_iterations == (int)iterationsName.neckturn02)
        {

            _audioSource.clip = sounds[0];
            _audioSource.volume = 1f;
            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.Stop();


            yield return StartCoroutine(__nextIteration());
            yield break;

        }

        if (_iterations == (int)iterationsName._return2)
        {

            yield return new WaitForSeconds(1.55f);

            if (_audioSource.clip != sounds[1])
            {
                _audioSource.clip = sounds[1];
            }

            _audioSource.Play();

            //	Debug.Log ("Laugh");
            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.Stop();


            yield return StartCoroutine(__nextIteration());
            yield break;

        }


        if (_iterations == (int)iterationsName.seek)
        {


            if (_audioSource.clip != sounds[3])
            {
                _audioSource.clip = sounds[3];
            }

            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.Stop();


            yield return StartCoroutine(__nextIteration());
            yield break;

        }

        if (_iterations == (int)iterationsName.demonicMov)
        {


            yield return new WaitForSeconds(1f);


            if (_audioSource.clip != sounds[4])
            {
                _audioSource.clip = sounds[4];
            }

            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.Stop();


            yield return StartCoroutine(__nextIteration());
            yield break;

        }


    }

    private IEnumerator __nextIteration()
    {

        if (!gameOver)
        {
            if (_iterations == 6)
            {

                _iterations = 0;

            }
            else
            {

                ++_iterations;
            }
        }


        yield return null;

    }


    private enum iterationsName
    {

        neckTurn, _return, subtleLaugh, neckturn02, _return2, seek, demonicMov
    }
}
