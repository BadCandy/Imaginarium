using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingScene : MonoBehaviour
{
    public GameObject visualChild;
    public GameObject progressBar;
    public GameObject background;
    public GameObject text;

    public static string levelToLoad;

    private Animator _animator;
    private int loadProgress = 0;
    private bool CR_running = false; 
    // Use this for initialization
    void Start()
    {
        visualChild.SetActive(true);
        background.SetActive(true);
        text.SetActive(true);
        progressBar.SetActive(true);
        _animator = visualChild.GetComponent<Animator>();
        _animator.Play("Walk");
        StartCoroutine(DisplayLoadingScreen());
    }

    // Update is called once per frame
    void Update()
    {
        if (!CR_running)
        {
            visualChild.SetActive(false);
            background.SetActive(false);
            text.SetActive(false);
            progressBar.SetActive(false);
        }
    }

    IEnumerator DisplayLoadingScreen()
    {
        CR_running = true;
        progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);

        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            Debug.Log("a");
            yield return null;
        }

        CR_running = false;
    }
}
