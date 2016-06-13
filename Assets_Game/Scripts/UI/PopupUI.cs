using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupUI : MonoBehaviour
{
    GameObject currentShowingObject;

    public void Show(GameObject obj)
    {
        if (currentShowingObject != null)
            Hide(currentShowingObject);

        currentShowingObject = obj;
        obj.SetActive(true);
    }

    public void Hide()
    {
        Hide(currentShowingObject);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
        currentShowingObject = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Hide();
    }
}
