using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaBehaviourHolder : MonoBehaviour
{
    public CasaCharacterBehaviour[] behaviours;
    private void Start()
    {
        StartFight();
    }
    public void StartFight()
    {
        foreach (CasaCharacterBehaviour item in behaviours)
        {
            item.StartFightScene();
        }
    }

    public void StopFight()
    {
        foreach (CasaCharacterBehaviour item in behaviours)
        {
            item.StopFightScene();
        }
    }
}
