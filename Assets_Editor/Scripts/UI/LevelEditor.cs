using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    public enum LevelEditMode
    {
        Build,
        Test,
        Pause
    }

    public enum LevelDrawType
    {
        Point,
        Brush,
        Empty
    }

    private GameObject _levelEditorObjects;
    private SpriteRenderer[][] _levelObjectSpriteRenderers;

    public CameraManager cameraManager;
    [HideInInspector]
    public LevelDrawType beforeDrawType;
    [HideInInspector]
    public GameObject[][] levelObjects;
    [HideInInspector]
    public Level level;
    [HideInInspector]
    public LevelEditMode editMode;
    [HideInInspector]
    public Int16[][] checkLevelObjectSpriteRenderers;

    public LevelManager _levelManager;
    public Sprite[] blockSprites;
    public Sprite spawnSprite;
    public Level.BlockType blockToPaint;
    public LevelDrawType drawType;
    

    public LevelEditMode EditMode
    {
        get
        {
            return editMode;
        }

        set
        {
            if (editMode == value)
                return;

            if (value == LevelEditMode.Build)
            {
                _levelEditorObjects.SetActive(true);
                _levelManager.Stop();
            }
            else if (value == LevelEditMode.Test)
            {
                _levelEditorObjects.SetActive(false);
                _levelManager.Play();
            }

            editMode = value;
        }
    }


	// Use this for initialization
	void Start () {
        level = new Level(10, 10);
        LevelManager.applicationIsQuitting = false;
    //    _levelManager = LevelManager.Instance;
        Camera.main.transform.position = new Vector3(5, 5, -10);
        _levelEditorObjects = new GameObject();
        _levelEditorObjects.name = "Level Editor Objects";
        InstantiateLevel();

        blockToPaint = Level.BlockType.Dirt;
        drawType = LevelDrawType.Brush;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Touch[] touches = Input.touches;

        if (editMode == LevelEditMode.Build)
        {
            if (!UIManager.isUIActive && touches.Length <= 1)
            {
                HandlePaint();   
            }

            if (Input.GetMouseButton(1) && !UIManager.isUIActive)
            {
                cameraManager.MoveCamera();
            }
        }
        
        if (Input.GetKeyDown("p"))
            if (editMode == LevelEditMode.Build)
                EditMode = LevelEditMode.Test;
            else if (editMode == LevelEditMode.Test)
                EditMode = LevelEditMode.Build;

        if (Input.GetKeyDown("1"))
            blockToPaint = Level.BlockType.Empty;

        if (Input.GetKeyDown("2"))
            blockToPaint = Level.BlockType.Dirt;

        if (Input.GetKeyDown("3"))
            blockToPaint = Level.BlockType.GrassedDirt;

        if (Input.GetKeyDown("4"))
            blockToPaint = Level.BlockType.Spring;

        if (Input.GetKeyDown("5"))
            blockToPaint = Level.BlockType.Bomber;

        if (Input.GetKeyDown("6"))
            blockToPaint = Level.BlockType.Flame;

        if (Input.GetKeyDown("7"))
            blockToPaint = Level.BlockType.Water;
	}

    public void InstantiateLevel()
    {
        levelObjects = new GameObject[level.Width][];
        _levelObjectSpriteRenderers = new SpriteRenderer[level.Width][];
        checkLevelObjectSpriteRenderers = new Int16[level.Width][];
        for (int i = 0; i < level.Width; i++)
        {
            levelObjects[i] = new GameObject[level.Height];
            _levelObjectSpriteRenderers[i] = new SpriteRenderer[level.Height];
            checkLevelObjectSpriteRenderers[i] = new Int16[level.Height];
            for (int j = 0; j < level.Height; j++)
            {
                checkLevelObjectSpriteRenderers[i][j] = -1;
                levelObjects[i][j] = new GameObject();
                levelObjects[i][j].transform.position = new Vector3(i, j, 0);
                levelObjects[i][j].transform.parent = _levelEditorObjects.transform;
                _levelObjectSpriteRenderers[i][j] = levelObjects[i][j].AddComponent<SpriteRenderer>();
                //_levelObjectSpriteRenderers[i][j].sprite = blockSprites[(int)Level.BlockType.Dirt-1];
            }
        }
    }

    void HandlePaint()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        int x = (int)(worldPoint.x + 0.5f);
        int y = (int)(worldPoint.y + 0.5f);

        if (x < 0 || x >= level.Width || y < 0 || y >= level.Height)
        {
            return;
        }
        if (drawType == LevelDrawType.Brush && Input.GetMouseButton(0))
            PaintAt(x, y);
        else if (drawType == LevelDrawType.Point && Input.GetMouseButtonDown(0))
            PaintAt(x, y);

        if (drawType == LevelDrawType.Empty && Input.GetMouseButton(0))
        {
            SpawnAt(x, y);
        }
 /*       if (Input.GetMouseButton(1))
        {
            SpawnAt(x, y);
        }*/
    }

    public void PaintAt(int x, int y)
    {
        if (level.Spawn.x == x && level.Spawn.y == y)
            return;

        int spriteIndexToPaint = (int)blockToPaint - 1;

        if (spriteIndexToPaint >= 0)
        {
            _levelObjectSpriteRenderers[x][y].sprite = blockSprites[spriteIndexToPaint];
            checkLevelObjectSpriteRenderers[x][y] = (Int16)spriteIndexToPaint;
        }
        else
        { 
            _levelObjectSpriteRenderers[x][y].sprite = null; // empty
            checkLevelObjectSpriteRenderers[x][y] = -1;
        }
        level.SetBlockAt(x, y, blockToPaint);
    }

    public void PaintAt(int x, int y, Int16 spriteIndexToPaint)
    {
        if (level.Spawn.x == x && level.Spawn.y == y)
            return;

        //int spriteIndexToPaint = (int)blockToPaint - 1;
        checkLevelObjectSpriteRenderers[x][y] = spriteIndexToPaint;

        if (spriteIndexToPaint >= 0)
        {
            _levelObjectSpriteRenderers[x][y].sprite = blockSprites[spriteIndexToPaint];
        }
        else
            _levelObjectSpriteRenderers[x][y].sprite = null; // empty
        level.SetBlockAt(x, y, (Level.BlockType)(spriteIndexToPaint+1));
    }

    public void SpawnAt(int x, int y)
    {
        if(level.Spawn.x >= 0 && level.Spawn.y >= 0)
            _levelObjectSpriteRenderers[level.Spawn.x][level.Spawn.y].sprite = null;
        _levelObjectSpriteRenderers[x][y].sprite = spawnSprite;
        level.SetSpawn(x, y);
    }

    void StartTest()
    {
        _levelEditorObjects.SetActive(false);
        LevelManager.Instance.Play();
    }
}
