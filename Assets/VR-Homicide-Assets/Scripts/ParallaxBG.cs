using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour
{
    [Range(-0.0f, -10f)]
    public float parallaxSpeed;

    [SerializeField]
    Transform[] images;

    [SerializeField]
    Transform defaultImagePos;

    Vector3 defaultPos;

    void Awake()
    {
        defaultPos = defaultImagePos.localPosition;
    }

    void Update()
    {
        foreach(Transform t in images)
        {
                t.localPosition = t.localPosition + new Vector3(parallaxSpeed * Time.deltaTime, 0, 0);

            if (t.localPosition.x <= -19.25)
            {
                t.localPosition = defaultPos;
            }
        }
    }
}