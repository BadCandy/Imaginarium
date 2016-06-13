using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectDrawType : MonoBehaviour
{
    public Sprite[] images;
    Image _image;
    LevelManager _levelManager;

    void Start()
    {
        _levelManager = LevelManager.Instance;
        _image = GetComponent<Image>();
        _image.sprite = images[(int)_levelManager.drawType];
    }

    void Update()
    {
        _image.sprite = images[(int)_levelManager.drawType];
    }

    public void NextDrawType()
    {
        _levelManager.drawType = (LevelManager.LevelDrawType)(((int)_levelManager.drawType + 1) % 3);
    }
}
