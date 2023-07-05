using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NumberBoard4P : MonoBehaviour
{
    [SerializeField]
    string whatAmI = "";
    public void HitByPlayer()
    {
        transform.parent.GetComponent<NumberBoardManager4P>().AddThis(whatAmI);
    }
}
