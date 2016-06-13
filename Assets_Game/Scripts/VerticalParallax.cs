using UnityEngine;
using System.Collections;

public class VerticalParallax : MonoBehaviour
{
    public GameObject camera;
    public float multiflier = 1;
    public float initialY;

    void Start ()
    {
        if (camera == null)
            camera = Camera.main.gameObject;
    }

    void Update ()
    {
        var position = transform.position;

        position.y = initialY - (camera.transform.position.y - 3.4f) * multiflier;

        transform.position = position;
	}
}
