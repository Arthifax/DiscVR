using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{

    [SerializeField]
    private InsideVaultManager insidevaultscript;

    [SerializeField]
    private GameObject sparks;
    public Animator alarmAnim;
    [SerializeField]private  Animator fuseDoor;
    [SerializeField] Transform player;
    [SerializeField] Transform teleportPoint;
    [SerializeField] bool canShoot;
    [SerializeField] float punishmentTime;
    [SerializeField] Transform light;
    [SerializeField] Material lightOn;
    [SerializeField] Animator lightAnim;

    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip wrong;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource Alarmsource;
    [SerializeField] private GameObject alarm;

    [SerializeField] public GameObject floorPickaxe;

    [SerializeField] Transform invaultpos;
    [SerializeField] private Screen_Fade screenFade;


    public bool canSelect;
    private bool selected;

    //    public bool doneFading = true;

    public GameObject cam = null;
    public GameObject cameraRig = null;


    public void openDoor()
    { Vector3 doorOpenPosition = new Vector3(teleportPoint.position.x,player.position.y,teleportPoint.position.z);
        if (player.position == doorOpenPosition)
        {
            fuseDoor.SetTrigger("FuseDoorOpen");
        }
    }

    public IEnumerator FadeoutPositionAndMore(Transform wireName)
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(2f);

        cameraRig.transform.position = invaultpos.position;
        wireName.gameObject.SetActive(true);

        screenFade.FadeIn();

    }

    public IEnumerator Fadeout()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(2f); // was 0.5f

    }

    public IEnumerator Fadein()
    {
        screenFade.FadeIn();
        yield return new WaitForSeconds(2f);

    }


    public void RemoveFuse(Transform wireName)
    {
        Vector3 doorOpenPosition = new Vector3(teleportPoint.position.x, player.position.y, teleportPoint.position.z);
        if (player.position == doorOpenPosition)
        {
            if (insidevaultscript.state != 1)
            {
                //Debug.Log("help!");
                return;
            }

            GameObject sparkInstance = Instantiate(sparks, transform);
            Destroy(sparkInstance, 0.25f);

            wireName.gameObject.SetActive(false);

            if (wireName.name == "FuseWire3")
            {
                floorPickaxe.gameObject.SetActive(true);
                Win();
                PlaySound(correct, 0.7f);
                //wireName.gameObject.SetActive(false);

            }
            else
            {
                StartCoroutine(WasteTime(punishmentTime));
                lightAnim.SetTrigger("Activate");
                PlaySound(wrong, 0.9f);
                //            PlaySound2(1.2f);
                //            alarm.GetComponent<Animator>().speed *= 1.2f;
                StartCoroutine(FadeoutPositionAndMore(wireName));
                //            cameraRig.transform.position = invaultpos.position;
                //           StartCoroutine(Fadein());
                //            wireName.gameObject.SetActive(true);
            }
            //wireName.gameObject.SetActive(false);
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
    private void PlaySound2(float pitch)
    {
        Alarmsource.pitch *= pitch;
    }

    private void Win()
    {
        insidevaultscript.state = 2;
        alarmAnim.SetTrigger("Off");
        alarmAnim.transform.GetComponent<AudioSource>().Stop();
    }

    IEnumerator WasteTime(float timer)
    {
        //  player.GetComponent<OculusGoRemoteInput>().mayShootRay = false;
        yield return new WaitForSeconds(timer);
        //   player.GetComponent<OculusGoRemoteInput>().mayShootRay = true;
    }
}
