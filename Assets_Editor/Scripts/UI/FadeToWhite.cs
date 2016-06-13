using UnityEngine;
using System.Collections;

public class FadeToWhite : MonoBehaviour {
    public GUITexture overlay;
    public float fadeTime;

    void Awake()
    {
        overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        StartCoroutine(FadeToClear());
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private IEnumerator FadeToClear()
    {
        //    CR_running = true;

        overlay.gameObject.SetActive(true);
        overlay.color = Color.black;

        float rate = 1.0f / fadeTime;

        float progress = 0.0f;

        while (progress < 1.0f)
        {
            overlay.color = Color.Lerp(Color.black, Color.clear, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        overlay.color = Color.clear;
        overlay.gameObject.SetActive(false);

        //   CR_running = false;
    }
}
