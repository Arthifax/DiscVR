using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;

    [SerializeField] private GameObject[] checkpoints = new GameObject[0];

    private GameObject currentCheckpoint = null;
    private int checkpointIndex = 0;

    [SerializeField] private AnimState state = AnimState.None;

    private void Start()
    {
        if (state == AnimState.None || state == AnimState.Idle)
            return;

        currentCheckpoint = checkpoints[0];
    }

    void FixedUpdate()
    {
        if (state == AnimState.None || state == AnimState.Idle)
            return;

        Vector3 checkPos = new Vector3(currentCheckpoint.transform.position.x, 0, currentCheckpoint.transform.position.z);
        Vector3 thisPos = new Vector3(transform.position.x, 0, transform.position.z);

        if(Vector3.Distance(thisPos, checkPos) <= .5f)
        {
            if (checkpoints.Length == checkpointIndex+1)
                checkpointIndex = 0;
            else
                checkpointIndex += 1;

            currentCheckpoint = checkpoints[checkpointIndex];
        }
        else
        {
            Vector3 target = currentCheckpoint.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, rotateSpeed * Time.deltaTime, 0.0f);

            Debug.DrawRay(transform.position, newDir, Color.red);

            transform.rotation = Quaternion.LookRotation(newDir);
        }

        transform.position += movementSpeed * Time.deltaTime * transform.forward;
    }

    private enum AnimState { Idle, Move, None }
}
