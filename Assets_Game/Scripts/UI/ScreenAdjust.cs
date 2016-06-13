using UnityEngine;
using System.Collections;

public class ScreenAdjust : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.aspect = 1366f / 768f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
