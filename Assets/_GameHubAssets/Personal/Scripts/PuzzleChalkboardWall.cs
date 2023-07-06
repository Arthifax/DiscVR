using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Janniemannie;

public class PuzzleChalkboardWall : MonoBehaviour
{
    private bool hasTriggered;
    public Scene0Manager manager;
    private Vector3 portalPosition;
    [SerializeField] private GameObject player;
    private void Start()
    {
        portalPosition = new Vector3(transform.position.x, 10.314f, transform.position.z);

    }
    // Update is called once per frame
    void Update()
    {
        if (!hasTriggered)
        {
            if (player.transform.position == portalPosition)
            {
                //Debug.Log("Boop");
                manager.EnableDoorTP();
                hasTriggered = true;
            }
        }
    }

    // Jan stuff

    public class LevelData<Node> where Node : BaseNode
    {
        public Node[,] grid;

        public void SetPosition(int x, int y)
        {
            grid[x, y].x = x;
        }
    }

    public class BaseNode
    {
        public int x, y;
    }

    void Pain()
    {
        List<float> list = new List<float>();
        list.First();
    }
}

namespace Janniemannie
{
    public static class ExtensionClass
    {
        public static int i;

        public static T First<T>(this List<T> list)
        {
            Debug.Log(i);
            return list[0];
        }
    }
}