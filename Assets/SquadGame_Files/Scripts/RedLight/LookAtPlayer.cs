using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private List<GameObject> gameObjects;

    public void AddTargets(List<GameObject> targets)
    {
        gameObjects = targets;
    }

    public void AlignShot(int index)
    {
        transform.LookAt(gameObjects[index].transform.position);
    }
}
