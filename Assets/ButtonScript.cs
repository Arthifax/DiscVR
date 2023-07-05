using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    [SerializeField] systemManager sManager;
    [SerializeField] string buttonColor = "";
    public void HitByPlayer()
    {
        sManager.AddPress(buttonColor);
        gameObject.GetComponent<Animator>().Play("Pressed");
    }
}
