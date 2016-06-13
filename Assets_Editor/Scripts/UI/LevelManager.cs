using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {
    GameObject _levelObjects;

    public GameObject player;
    public LevelEditor levelEditor;
    public GameObject[] blocks;

    private String _saveFileName;
    private String _loadFileName;


    public void Play()
    {
        _levelObjects = new GameObject();
        _levelObjects.name = "Level";

        GameObject instantiatedObject;

        for (int i = 0; i < levelEditor.level.Width; i++)
        {
            for (int j = 0; j < levelEditor.level.Height; j++)
            {
                Level.BlockType block = levelEditor.level.GetBlockAt(i, j);

                if (block == Level.BlockType.Empty)
                    continue;

                instantiatedObject = Instantiate(blocks[(int)block-1],
                                                        new Vector3(i, j, 0),
                                                        Quaternion.identity) as GameObject;
                instantiatedObject.transform.parent = _levelObjects.transform;
            }
        }

        instantiatedObject = Instantiate(player, new Vector3(levelEditor.level.Spawn.x, levelEditor.level.Spawn.y, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.parent = _levelObjects.transform;

        instantiatedObject = new GameObject();
        instantiatedObject.AddComponent<BoxCollider2D>();
        instantiatedObject.transform.localScale = new Vector3(levelEditor.level.Width, 1, 1);
        instantiatedObject.transform.position = new Vector3(levelEditor.level.Width / 2, -1, 0);
        instantiatedObject.name = "Ground";
        instantiatedObject.transform.parent = _levelObjects.transform;
        instantiatedObject.layer = LayerMask.NameToLayer("Block");
    }

    public void TogglePlay()
    {
        if (levelEditor.editMode == LevelEditor.LevelEditMode.Build)
        {
            //   _levelManager.Play(_level);
            if (levelEditor.level.GetSpawn().x == -1 && levelEditor.level.GetSpawn().y == -1)
            {
                Debug.Log("Not Spawn Player");
                return;
            }
            levelEditor.EditMode = LevelEditor.LevelEditMode.Test;
        }
        else if (levelEditor.editMode == LevelEditor.LevelEditMode.Test)
        {
            //  _levelManager.Stop();
            levelEditor.EditMode = LevelEditor.LevelEditMode.Build;
        }
    }

    public void Stop()
    {
        if (_levelObjects != null)
            Destroy(_levelObjects);
    }

    public void SetBlockToPaint(int blockType)
    {
        if (levelEditor.drawType == LevelEditor.LevelDrawType.Empty)
            levelEditor.drawType = levelEditor.beforeDrawType;
        levelEditor.blockToPaint = (Level.BlockType)blockType;
    }

    public void SetDrawTypeToPaint(int drawType)
    {
        if ((LevelEditor.LevelDrawType)drawType == LevelEditor.LevelDrawType.Empty && levelEditor.drawType != LevelEditor.LevelDrawType.Empty)
            levelEditor.beforeDrawType = levelEditor.drawType;
        levelEditor.drawType = (LevelEditor.LevelDrawType)drawType;
    }

    public void SetMapWidth(string width)
    {
        if (width.Equals(""))
        {
            levelEditor.level.SetTempWidth(0);
            return;
        }
        levelEditor.level.SetTempWidth(int.Parse(width));
    }

    public void SetMapHeight(string height)
    {
        if (height.Equals(""))
        {
            levelEditor.level.SetTempHeight(0);
            return;
        }
        levelEditor.level.SetTempHeight(int.Parse(height));
    }

    public void SetMapWidth(int width)
    {
        levelEditor.level.SetTempWidth(width);
    }

    public void SetMapHeight(int height)
    {
        levelEditor.level.SetTempHeight(height);
    }

    public void SetLevelSaveFileName(String fileName)
    {
        if (fileName.Equals(""))
        {
            Debug.Log("Empty FileName");
            return;
        }
        _saveFileName = fileName;
    }



    public void SetLevelLoadFileName(int value)
    {
        _loadFileName = GameObject.Find("File Selection").GetComponent<Dropdown>().options[value].text;
    }

    public void LevelSave()
    {
        if (_saveFileName == null || _saveFileName.Equals(""))
        {
            Debug.Log("Invalid FileName");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = null;

        String path = Application.persistentDataPath + "/" + "Map";
        if (!Directory.Exists(path))
        {
            Debug.Log("Already exist filename");
            Directory.CreateDirectory(path);
        }

        path = Application.persistentDataPath + "/" + "Map" + "/" + _saveFileName;
        if (File.Exists(path))
        {
            Debug.Log("Already exist filename");
            file = File.Open(path, FileMode.Open);
        }
        else
            file = File.Create(path);
        _saveFileName = "";

        LevelData data = new LevelData();
        data.width = levelEditor.level.Width;
        data.height = levelEditor.level.Height;
        data.characterX = levelEditor.level.GetSpawn().x;
        data.characterY = levelEditor.level.GetSpawn().y;
        data.checkBlock = levelEditor.checkLevelObjectSpriteRenderers;
        data.checkBlock = new Int16[levelEditor.level.Width][];
        for (int i = 0; i < levelEditor.level.Width; i++)
               {
                   data.checkBlock[i] = new Int16[levelEditor.level.Height];
               }

        for (int i = 0; i < levelEditor.level.Width; i++) 
               {
                   for (int j = 0; j < levelEditor.level.Height; j++)
                   {
                       data.checkBlock[i][j] = levelEditor.checkLevelObjectSpriteRenderers[i][j];
                   }
               }

        bf.Serialize(file, data);
        file.Close();
    }

    public void LevelLoad()
    {
        String path = Application.persistentDataPath + "/" + "Map" + "/" + _loadFileName;
        if (!File.Exists(path))
        {
            Debug.Log("File not exist!");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        LevelData data = (LevelData)bf.Deserialize(file);

        file.Close();
        SetMapWidth(data.width);
        SetMapHeight(data.height);
        RenewLevel();
        levelEditor.checkLevelObjectSpriteRenderers = data.checkBlock;

        if ((data.characterX >= 0 && data.characterX < levelEditor.level.Width) 
            && (data.characterY >= 0 && data.characterY < levelEditor.level.Height))
        {
            levelEditor.SpawnAt(data.characterX, data.characterY);
        }
        for (int i = 0; i < levelEditor.level.Width; i++)
        {
            for (int j = 0; j < levelEditor.level.Height; j++)
            {
                levelEditor.PaintAt(i, j, levelEditor.checkLevelObjectSpriteRenderers[i][j]);
            }
        }
    }

    public void LevelLoad(String fileName)
    {
        String path = Application.persistentDataPath + "/" + "Map" + "/" + fileName;
        if (!File.Exists(path))
        {
            Debug.Log(fileName + "File not exist!");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        LevelData data = (LevelData)bf.Deserialize(file);

        file.Close();
        SetMapWidth(data.width);
        SetMapHeight(data.height);
        RenewLevel();
        levelEditor.checkLevelObjectSpriteRenderers = data.checkBlock;

        if ((data.characterX >= 0 && data.characterX < levelEditor.level.Width)
            && (data.characterY >= 0 && data.characterY < levelEditor.level.Height))
        {
            levelEditor.SpawnAt(data.characterX, data.characterY);
        }
        for (int i = 0; i < levelEditor.level.Width; i++)
        {
            for (int j = 0; j < levelEditor.level.Height; j++)
            {
                levelEditor.PaintAt(i, j, levelEditor.checkLevelObjectSpriteRenderers[i][j]);
            }
        }
    }

    public void RenewLevel()
    {
        if (levelEditor.level.TempWidth <= 0 || levelEditor.level.TempHeight <= 0)
        {
            Debug.Log("You can't input invaild value");
            return;
        }

        for (int i = 0; i < levelEditor.level.Width; i++)
        {
            for (int j = 0; j < levelEditor.level.Height; j++)
            {
                Destroy(levelEditor.levelObjects[i][j], 0.0F);
            }
        }

        levelEditor.level = new Level(levelEditor.level.TempWidth, levelEditor.level.TempHeight);
        levelEditor.InstantiateLevel();
    }
}
