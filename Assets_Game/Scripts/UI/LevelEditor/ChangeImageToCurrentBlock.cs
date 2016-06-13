using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeImageToCurrentBlock : MonoBehaviour
{
    Image _image;
    LevelManager _levelManager;

    void Start()
    {
        _image = GetComponent<Image>();
        _levelManager = LevelManager.Instance;
    }

	// Update is called once per frame
	void Update ()
    {
        _image.sprite = _levelManager.blockSprites[(int)_levelManager.blockToPaint];
        //_image.sprite = null;
	}
}
