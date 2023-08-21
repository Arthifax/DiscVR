using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuseboxPuzzle : MonoBehaviour
{
    [SerializeField] private VaultManager vaultManager;
    [SerializeField] private List<GameObject> wireList = new List<GameObject>();
    [SerializeField] private GameObject vfxSparks;
    [SerializeField] private int index = 0;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private Material redAlarmMat;

    public void CheckWire(string wireColor)
    {
        if (wireList[index].name.Contains(wireColor))
        {
            index++;
            wireList[index].GetComponent<GameObject>().SetActive(false);
            if(index == 2)
            {
                vfxSparks.SetActive(true);
                vaultManager.wireBoxPuzzleCompleted = true;
            }
        }
        else
        {
            alarmLight.GetComponent<Renderer>().material = redAlarmMat;
        }
    }
}
