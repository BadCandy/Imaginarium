using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelController : MonoBehaviour
{
    LevelManager _levelManager;

    void Start()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_levelManager.State == LevelManager.LevelState.Build)
        {
            HandlePaint();
        }

        if (Input.GetKeyDown("p"))
            if (_levelManager.State == LevelManager.LevelState.Build)
                _levelManager.State = LevelManager.LevelState.Test;
            else if (_levelManager.State == LevelManager.LevelState.Test)
                _levelManager.State = LevelManager.LevelState.Build;

        if (Input.GetKeyDown("1"))
            _levelManager.blockToPaint = Level.BlockType.Empty;

        if (Input.GetKeyDown("2"))
            _levelManager.blockToPaint = Level.BlockType.Dirt;

        if (Input.GetKeyDown("3"))
            _levelManager.blockToPaint = Level.BlockType.GrassedDirt;

        if (Input.GetKeyDown("4"))
            _levelManager.blockToPaint = Level.BlockType.Spring;

        if (Input.GetKeyDown("5"))
            _levelManager.blockToPaint = Level.BlockType.Bomber;

        if (Input.GetKeyDown("6"))
            _levelManager.blockToPaint = Level.BlockType.Flame;

        if (Input.GetKeyDown("7"))
            _levelManager.blockToPaint = Level.BlockType.Water;

        if (Input.GetKeyDown("8"))
            _levelManager.blockToPaint = Level.BlockType.Sticky;

        if (Input.GetKeyDown("9"))
            _levelManager.blockToPaint = Level.BlockType.Iron;

        if (Input.GetKeyDown("0"))
            _levelManager.blockToPaint = Level.BlockType.Cube;
    }

    void HandlePaint()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        int x = (int)(worldPoint.x + 0.5f);
        int y = (int)(worldPoint.y + 0.5f);

        if (_levelManager.drawType == LevelManager.LevelDrawType.Brush && Input.GetMouseButton(0))
            _levelManager.PaintAt(x, y);
        else if (_levelManager.drawType == LevelManager.LevelDrawType.Point && Input.GetMouseButtonDown(0))
            _levelManager.PaintAt(x, y);

        if (_levelManager.drawType == LevelManager.LevelDrawType.Spawn && Input.GetMouseButton(0))
        {
            _levelManager.SpawnAt(x, y);
        }
        if (Input.GetMouseButton(1))
        {
            _levelManager.SpawnAt(x, y);
        }
    }
}