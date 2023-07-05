using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubwayLevelManager : MonoBehaviour {

    private int currentLevel = 0;

    [Tooltip("Input -1 till 1. -1 for left, 0 for mid, 1 for right")]
    [SerializeField]
    private int[] currentLevelCorrect;

    [Tooltip("Input -1 till 1. -1 for left, 0 for mid, 1 for right\n" +
        "You don't want this to be the same as currentLevelCorrect")]
    [SerializeField]
    private int[] trainSpot;


    [SerializeField]
    private HallTrain[] Trains;

    [SerializeField]
    private SubwayHall[] Answers;

    [SerializeField] Image leftImage, middleImage, rightImage;

    [System.Serializable]
    public class ColorPerHall
    {
        public Color[] leftHall, midHall, rightHall;
    }

    public ColorPerHall colorPerHall;

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        if (currentLevel >= currentLevelCorrect.Length)
        {
            Win();
            return;
        }

        //RESET BOOLS
        foreach (HallTrain train in Trains)
        {
            train.trainHall = false;  
        }

        foreach (SubwayHall hall in Answers)
        {
            hall.correct = false;
        }

        //SET CORRECT BOOLS ACTIVE
        for (int i = -1; i <= 1; i++)
        {
            if(i == currentLevelCorrect[currentLevel])
            {
                Answers[i + 1].correct = true;
            }

            if(i == trainSpot[currentLevel])
            {
                Trains[i + 1].trainHall = true;
            }
        }

        leftImage.color = colorPerHall.leftHall[currentLevel];
        middleImage.color = colorPerHall.midHall[currentLevel];
        rightImage.color = colorPerHall.rightHall[currentLevel];
        currentLevel++;
    }

    private void Win()
    {
        FindObjectOfType<SceneSwitchFinal>().SetToWin();
    }
}
