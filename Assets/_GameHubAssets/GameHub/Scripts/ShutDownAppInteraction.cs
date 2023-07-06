using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShutDownAppInteraction : MonoBehaviour
{
public void ShutDown()
    {
        SceneManager.LoadScene(0);
    }
}
