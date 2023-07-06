using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageSelectRestart : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GunController> gunControllers;
    [SerializeField] private GameObject resetTP;

    [SerializeField] private Screen_Fade screenFade;


    public void Restart()
    {
        StartCoroutine( RestartHostageSelect());
    }
    IEnumerator RestartHostageSelect()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(2.5f);
        Player.transform.position = new Vector3(resetTP.transform.position.x, Player.transform.position.y, resetTP.transform.position.z);
        screenFade.FadeIn();
        yield return new WaitForSeconds(1f);
        foreach(GunController controller in gunControllers)
        {
            if(controller.gameObject.activeSelf)
            {
                controller.enabled = true;
                controller.ShootRays();
            }
        }
    }

}
