using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDrawerController : MonoBehaviour
{
    GameObject Table;

    [Header("Movement Variables")]
    [SerializeField] Vector3 _goalPosition;
    [SerializeField] float _speed = 0.5f;

    [SerializeField] private bool isOpen = false;

    private void Start()
    {
        Table = transform.parent.gameObject;
    }

    public void DrawerInteract()
    {
        StartCoroutine(SmoothLerp(_speed));
    }

    private IEnumerator SmoothLerp(float time)
    {
        float elapsedTime = 0;

        if (isOpen)
        {
            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(Table.transform.position + _goalPosition, Table.transform.position, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isOpen = false;
        }
        else
        {
            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(Table.transform.position, Table.transform.position + _goalPosition, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isOpen = true;
        }
    }
}
