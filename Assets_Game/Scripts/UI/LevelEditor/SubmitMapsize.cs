using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmitMapsize : MonoBehaviour {
    public InputField width;
    public InputField height;

    void OnEnable()
    {
        var levelManager = LevelManager.Instance;

        width.text = levelManager.GetLevelWidth().ToString();
        height.text = levelManager.GetLevelHeight().ToString();
    }

    public void Submit()
    {
        var levelManager = LevelManager.Instance;

        height.text = levelManager.SetLevelHeight(int.Parse(height.text)).ToString();
        width.text = levelManager.SetLevelWidth(int.Parse(width.text)).ToString();
    }
}
