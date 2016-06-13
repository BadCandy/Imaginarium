using UnityEngine;
using System.Collections;

public class AnyClicked : MonoBehaviour {
    public SceneManagement sceneManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
            sceneManager.Click();
    }
}
