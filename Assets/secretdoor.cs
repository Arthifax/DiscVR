using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secretdoor : MonoBehaviour {

    [SerializeField] Transform door;

    public void HitByPlayer()
    {
        if (door.transform.rotation.eulerAngles.y > 50)
        {
            SceneManager.LoadScene("office");
        }
    }
}
