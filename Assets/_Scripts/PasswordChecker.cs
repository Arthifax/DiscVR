using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    [SerializeField] Text displayText;
    [SerializeField] string correctPassword;
    
    [SerializeField] UnityEvent correctEvent, wrongEvent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip succesSfx;
    [SerializeField] AudioClip failureSfx;
    
    bool done;

    public void TryPassword()
    {
        if (displayText.text == correctPassword)
        {
            correctEvent.Invoke();
            audioSource.PlayOneShot(succesSfx);
            displayText.text = "";
        }
        else
        {
            wrongEvent.Invoke();
            audioSource.PlayOneShot(failureSfx);
            displayText.text = "";
        }
    }
    
}
