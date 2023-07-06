using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardPickupPlaceBack card;
    [SerializeField] private AudioSource cardSource;
    [SerializeField] public bool hasSeenCard = false;
    
    [SerializeField] private GameObject finishLevelCard;
    [SerializeField] private GameObject forgotSomethingCard;

    public void playAudio()
    {
        cardSource.Play();
    }
    
    public void CheckIfSeenCard()
    {
        if (hasSeenCard)
        {
            finishLevelCard.SetActive(true);
        }
        else
        {
            forgotSomethingCard.SetActive(true);
        }
    }
}
