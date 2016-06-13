using UnityEngine;
using System.Collections;

public class ClampPosition : MonoBehaviour
{
    public float horizontalMin;
    public float horizontalMax;
    public float verticalMin;
    public float verticalMax;
    public float tolerance = 5;

	void Update ()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, horizontalMin - tolerance, horizontalMax + tolerance);
        newPosition.y = Mathf.Clamp(newPosition.y, verticalMin - tolerance, verticalMax + tolerance);
        transform.position = newPosition;
	}
}
