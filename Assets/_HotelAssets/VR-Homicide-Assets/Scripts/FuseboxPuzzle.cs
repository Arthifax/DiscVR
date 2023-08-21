using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FuseboxPuzzle : MonoBehaviour
{
    [SerializeField] private VaultManager vaultManager;
    [SerializeField] private List<GameObject> wireList = new List<GameObject>();
    [SerializeField] private GameObject vfxSparks;
    [SerializeField] private int index = 0;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private Material greenAlarmMat;
    [SerializeField] private Material redAlarmMat;
    private bool isEqual;

    public void CheckWire(string wireColor)
    {
        if (wireList[index].name.Equals(wireColor))
        {
            wireList[index].SetActive(false);
            index++;
            if (index == 3)
            {
                vfxSparks.SetActive(true);
                alarmLight.GetComponent<Renderer>().material = greenAlarmMat;
                vaultManager.wireBoxPuzzleCompleted = true;
            }
        }
        else if (wireList[index].name.Contains("Yellow") || !wireList[index].name.Equals(wireColor))
        {
            alarmLight.GetComponent<Renderer>().material = redAlarmMat;
        }
    }
}
