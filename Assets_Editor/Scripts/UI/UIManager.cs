using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static bool isUIActive;
    
	void Start () {

	}

    public void ActiveObj(GameObject obj)
    {
        isUIActive = true;
        obj.SetActive(true);
    }

    public void InactiveObj(GameObject obj)
    {
        isUIActive = false;
        obj.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
