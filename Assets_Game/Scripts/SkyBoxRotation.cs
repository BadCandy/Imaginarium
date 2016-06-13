using UnityEngine;
using System.Collections;

public class SkyBoxRotation : MonoBehaviour
{
    public bool randomX;
    public bool randomY;
    public bool randomZ;
    public float rotatationSpeed = 0.01f;

	void OnEnable()
    {
        var euler = transform.eulerAngles;

        if (randomX)
            euler.x = Random.Range(0.0f, 360.0f);

        if (randomY)
            euler.y = Random.Range(0.0f, 360.0f);

        if (randomZ)
            euler.z = Random.Range(0.0f, 360.0f);

        transform.eulerAngles = euler;
	}

    void Update()
    {
        var euler = transform.eulerAngles;
        euler.y += rotatationSpeed;
        transform.eulerAngles = euler;
    }
}
