using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public GUITexture overlay;
    public float fadeTime;

    void Awake()
    {
        overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        StartCoroutine(FadeToDark());
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeToDark()
    {
        //     CR_running = true;

        overlay.gameObject.SetActive(true);
        overlay.color = Color.clear;

        float rate = 1.0f / fadeTime;

        float progress = 0.0f;

        while (progress < 1.0f)
        {
            overlay.color = Color.Lerp(Color.clear, Color.black, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        overlay.color = Color.black;
        overlay.gameObject.SetActive(false);
        //     CR_running = false;
    }
}
