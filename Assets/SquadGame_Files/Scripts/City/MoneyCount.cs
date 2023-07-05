using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private PointsScriptableObject moneyScript;

    private void Start()
    {
        moneyText.text += moneyScript.points.ToString();
    }
}
