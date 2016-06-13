using UnityEngine;
using System.Collections;

public class ResetPositionOnEnable : MonoBehaviour {
    private Vector3 _initialPosition;

	void Start ()
    {
        _initialPosition = transform.position;
	}

    void OnEnable()
    {
        transform.position = _initialPosition;
    }
}
