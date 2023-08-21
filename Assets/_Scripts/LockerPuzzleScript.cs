using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class LockerPuzzleScript : MonoBehaviour
{
    [SerializeField] private LockerPuzzleKeyScript keyScript;
    [SerializeField] private int lockerNumber;
    [SerializeField] private TextMeshPro codeText;
    private bool vaultIsUnlocked = false;


    public void CheckHeldKeyNr(string rewardCode)
    {
        if (keyScript.currentHeldKeyNr == lockerNumber || vaultIsUnlocked) //if you're holding the correct key or vault is already unlocked
        {
            vaultIsUnlocked = true;
            codeText.text = rewardCode; //show reward
            keyScript.isHoldingKey = false; //you are no longer holding a key
            keyScript.currentHeldKeyNr = 0; //current held number is reset
            keyScript.keyVisual.SetActive(false); //stop showing key in hand

            for (int i = 0; i < keyScript.keyList.Count; i++) //go through the list of keys
            {
                if (keyScript.keyList[i].name.Equals("Keys_Hanging " + lockerNumber)) //find key with same number as the locker
                {
                    keyScript.keyList[i].gameObject.SetActive(false); //disable the key since it's used
                }
            }
        }
        else
        {
            codeText.text = "You can't open this locker";
        }
    }
}
