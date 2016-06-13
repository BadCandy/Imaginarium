using UnityEngine;
using System.Collections;

public class AttachToMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.parent = Camera.main.transform;
	}
}
