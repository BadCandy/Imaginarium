using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    public string sceneName;
    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
