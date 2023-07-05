using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBirds : MonoBehaviour
{
    [SerializeField] private List<Transform> birdPositions;
    private bool Moving = true;
    private bool MoveRight = true;
    private bool setup = false;

    void Update()
    {
        if (Moving)
        {
            var step = 1 * Time.deltaTime;
            if (MoveRight)
            {
                if (!setup)
                {
                    setupBird(birdPositions[0].position);
                }
                transform.position = Vector3.MoveTowards(transform.position, birdPositions[1].position, step);
                if (transform.position == birdPositions[1].position)
                {
                    if (transform.rotation != birdPositions[1].rotation)
                    {
                        MoveDelay();
                        transform.rotation = birdPositions[1].rotation;
                    }

                }
            }
            else
            {
                if (!setup)
                {
                    setupBird(birdPositions[1].position);
                }
                transform.position = Vector3.MoveTowards(transform.position, birdPositions[0].position, step);
                if (transform.position == birdPositions[0].position)
                {
                    if (transform.rotation != birdPositions[0].rotation)
                    {
                        MoveDelay();
                        transform.rotation = birdPositions[0].rotation;
                    }
                }
            }
        }
    }
    private void setupBird(Vector3 position)
    {
        transform.position = position;
        setup = true;
    }

    private IEnumerator MoveDelay()
    {
        Moving = false;
        setup = false;
        yield return new WaitForSeconds(10f);
        MoveRight = !MoveRight;
        Moving = true;
    }
}
