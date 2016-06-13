using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneManagement : MonoBehaviour
{

    public float fadeDuration;
    public string levelToLoad;
    /*   public GameObject background;
       public GameObject text;
       public GameObject progressBar;
       public GameObject visualChild;*/
    //   public GameObject canvas;
    //   public GUITexture overlay;


    private Animator _animator;
    private bool loading = false;
    private int loadProgress = 0;

    private GUIStyle m_BackgroundStyle = new GUIStyle();		// Style for background tiling
    private Texture2D m_FadeTexture;				// 1x1 pixel texture used for fading
    private Color m_CurrentScreenOverlayColor = new Color(0, 0, 0, 0);	// default starting color: black and fully transparrent
    private Color m_TargetScreenOverlayColor = new Color(0, 0, 0, 0);	// default target color: black and fully transparrent
    private Color m_DeltaColor = new Color(0, 0, 0, 0);		// the delta-color is basically the "speed / second" at which the current color should change
    private int m_FadeGUIDepth = -1000;				// make sure this texture is drawn on top of everything
    private bool isClicked = false;
    void Awake()
    {
        //      overlay.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        //      StartCoroutine(FadeToClear());
        m_FadeTexture = new Texture2D(1, 1);
        m_BackgroundStyle.normal.background = m_FadeTexture;
        SetScreenOverlayColor(m_CurrentScreenOverlayColor);

        // TEMP:
        // usage: use "SetScreenOverlayColor" to set the initial color, then use "StartFade" to set the desired color & fade duration and start the fade
        //   SetScreenOverlayColor(new Color(0,0,0,1));
        //    StartFade(new Color(1,0,0,1), 5);
    }
    // Use this for initialization
    void Start()
    {
        //       _animator = visualChild.GetComponent<Animator>();
        //     _animator.Play("Walk"); 

        /*     visualChild.SetActive(false);
             background.SetActive(false);
             text.SetActive(false);
             progressBar.SetActive(false);*/
        LoadingScene.levelToLoad = levelToLoad;

        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            FadeIn();
            Invoke("DisplayLoadingScreen", 2);
            isClicked = false;
            //StartCoroutine(FadeIn());
        }
    }

    public void SetNextSceneName(string sceneName)
    {
        LoadingScene.levelToLoad = sceneName;
        //GetSceneByName(sceneName)
    }

    public void Click()
    {
        isClicked = true;
    }



    void DisplayLoadingScreen()
    {
        loading = true;
        /*     background.SetActive(true);
             text.SetActive(true);
             progressBar.SetActive(true);
             visualChild.SetActive(true);*/

        //    progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

        //  text.GetComponent<Text>().text = "Loading Progress " + loadProgress + "%";

        SceneManager.LoadScene("Loading");
        /*        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);
        
                while (!async.isDone)
                {
                   loadProgress = (int)(async.progress * 100);
           //         text.GetComponent<Text>().text = "Loading Progress " + loadProgress + "%";
                    progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
                    Debug.Log("a");
                    yield return null;
                }
                loading = false;*/
        //     StartCoroutine(FadeOut());
        //   CR_running = false;

    }

    private void OnGUI()
    {
        // if the current color of the screen is not equal to the desired color: keep fading!
        if (m_CurrentScreenOverlayColor != m_TargetScreenOverlayColor)
        {
            // if the difference between the current alpha and the desired alpha is smaller than delta-alpha * deltaTime, then we're pretty much done fading:
            if (Mathf.Abs(m_CurrentScreenOverlayColor.a - m_TargetScreenOverlayColor.a) < Mathf.Abs(m_DeltaColor.a) * Time.deltaTime)
            {
                m_CurrentScreenOverlayColor = m_TargetScreenOverlayColor;
                SetScreenOverlayColor(m_CurrentScreenOverlayColor);
                m_DeltaColor = new Color(0, 0, 0, 0);
            }
            else
            {
                // fade!
                SetScreenOverlayColor(m_CurrentScreenOverlayColor + m_DeltaColor * Time.deltaTime);
            }
        }

        // only draw the texture when the alpha value is greater than 0:
        if (m_CurrentScreenOverlayColor.a > 0)
        {
            GUI.depth = m_FadeGUIDepth;
            GUI.Label(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), m_FadeTexture, m_BackgroundStyle);
        }
    }

    void FadeIn()
    {
        SetScreenOverlayColor(Color.clear);
        StartFade(Color.black);
    }

    void FadeOut()
    {
        SetScreenOverlayColor(Color.black);
        StartFade(Color.clear);
    }


    // instantly set the current color of the screen-texture to "newScreenOverlayColor"
    // can be usefull if you want to start a scene fully black and then fade to opague
    public void SetScreenOverlayColor(Color newScreenOverlayColor)
    {
        m_CurrentScreenOverlayColor = newScreenOverlayColor;
        m_FadeTexture.SetPixel(0, 0, m_CurrentScreenOverlayColor);
        m_FadeTexture.Apply();
    }


    // initiate a fade from the current screen color (set using "SetScreenOverlayColor") towards "newScreenOverlayColor" taking "fadeDuration" seconds
    public void StartFade(Color newScreenOverlayColor)
    {
        if (fadeDuration <= 0.0f)		// can't have a fade last -2455.05 seconds!
        {
            SetScreenOverlayColor(newScreenOverlayColor);
        }
        else					// initiate the fade: set the target-color and the delta-color
        {
            m_TargetScreenOverlayColor = newScreenOverlayColor;
            m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;
        }
    }
    /*
        private IEnumerator FadeToClear()
        {
            //    CR_running = true;
            canvas.SetActive(false);
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
            if (!loading)
                canvas.SetActive(true);
            overlay.color = Color.clear;
            overlay.gameObject.SetActive(false);
       
            //   CR_running = false;
        }

        private IEnumerator FadeToBlack()
        {
            //     CR_running = true;
            canvas.SetActive(false);
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
            StartCoroutine(DisplayLoadingScreen(levelToLoad));
            if (!loading)
                canvas.SetActive(true);
            //     CR_running = false;
        } */
}
