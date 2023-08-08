using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    [SerializeField] List<Question> questions;

    private Question selectedQuestion;

    [Header("Quiz UI")] 
    [SerializeField] private TextMeshPro questionText;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctCol, wrongCol, normalCol;
    [SerializeField] private GameObject completedScreen;

    private Question question;
    private bool answered;
    private int amtAnsweredCorrectly = 0;

    private void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    void Start()
    {
        SelectQuestion();
    }

    void SelectQuestion()
    {
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        
        SetQuestion(selectedQuestion);
    }

    private bool Answer(string answered)
    {
        bool correctAns = false;

        if (answered == selectedQuestion.correctAns)
        {
            //yes
            correctAns = true;
            amtAnsweredCorrectly++;
            if (amtAnsweredCorrectly == questions.Count)
            {
                completedScreen.SetActive(true);
            }
        }
        else
        {
            //no
        }

        Invoke("SelectQuestion", 0.4f);

        return correctAns;
    }

    private void SetQuestion(Question question)
    {
        this.question = question;

        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(this.question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }

        answered = false;
    }

    private void OnClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            bool val = Answer(btn.name);

            if (val)
            {
                btn.image.color = Color.green;
            }
            else
            {
                btn.image.color = Color.red;
            }
        }
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAns;
}
