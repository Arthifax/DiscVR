using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore_SG : MonoBehaviour
{
    [SerializeField] PointsScriptableObject squidgameScore;
    [SerializeField] int setScore = 0;
    
    //The scriptable should be set to 0 in the metro level
    void Start()
    {
        squidgameScore.points = setScore;
    }
}
