using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [HideInInspector]
    public static int currentStageNumber;
    [HideInInspector]
    public static String[] stageNameList;
    public CameraManager cameraManager;
    private String _saveDataFileName;
    private String _tempFileName;
    private int _stageCount;
  //  public GameObject stageGroup;

    void Start()
    {
        
  /*      for (int i = 0; i < stageGroup.transform.childCount; i++)
        {
            stageGroup.transform.GetChild(i).gameObject.AddComponent<StageSelect>();
            stageGroup.transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
        }*/



        _stageCount = 20;
        _saveDataFileName = "StageData";
        stageNameList = new String[_stageCount];

        InitiateStageNameList();
        CheckDataFile();
        LoadStageInfo();
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && !UIManager.isUIActive)
        {
            cameraManager.MoveCamera();
        }
    }

  /*  void LateUpdate()
    {
              if (!useTwoTouchCameraMove) { 
                Vector3 position = Camera.main.transform.position;
                if (position.x < 2.0f)
                    Camera.main.transform.position = new Vector3(2.0f, position.y, position.z);
                if (position.x > 14.0f)
                    Camera.main.transform.position = new Vector3(14.0f, position.y, position.z);
                if (position.y < 2.0f)
                    Camera.main.transform.position = new Vector3(position.x, 2.0f, position.z);
                if (position.y > 10.0f)
                    Camera.main.transform.position = new Vector3(position.x, 10.0f, position.z);
                }*/
 /*       var colliderSize = new Vector2(
            _boxCollider.size.x * Mathf.Abs(transform.localScale.x),
            _boxCollider.size.y * Mathf.Abs(transform.localScale.y)) / 2;

        if (Above != BoundsBehavior.Nothing && transform.position.y + colliderSize.y > Bounds.bounds.max.y)
            ApplyBoundsBehavior(Above, new Vector2(transform.position.x, Bounds.bounds.max.y - colliderSize.y));

        if (Below != BoundsBehavior.Nothing && transform.position.y - colliderSize.y < Bounds.bounds.max.y)
            ApplyBoundsBehavior(Below, new Vector2(transform.position.x, Bounds.bounds.min.y + colliderSize.y));

        if (Right != BoundsBehavior.Nothing && transform.position.x + colliderSize.x > Bounds.bounds.max.x)
            ApplyBoundsBehavior(Right, new Vector2(Bounds.bounds.max.x - colliderSize.x, transform.position.y));

        if (Left != BoundsBehavior.Nothing && transform.position.x - colliderSize.x > Bounds.bounds.min.x)
            ApplyBoundsBehavior(Right, new Vector2(Bounds.bounds.min.x + colliderSize.x, transform.position.y));
    }

    private void ApplyBoundsBehavior(BoundsBehavior behavior, Vector2 constrainedPosition)
    {
        if (behavior == BoundsBehavior.Kill)
        {
            return;
        }
        transform.position = constrainedPosition;
    }*/

    public void CheckDataFile()
    {
        FileStream file = null;
        String path = Application.persistentDataPath + "/" + ModeManager.currentGameFolder + "/" + _saveDataFileName;
        if (!File.Exists(path))
        {
            Debug.Log("Create new stageDataFile");
            file = File.Create(path);
        }
    }

    public void InitiateStageNameList()
    {
        for (int i = 0; i < _stageCount; i++)
        {
            stageNameList[i] = "";
        }
    }
    public void SaveStageInfo()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = null;
        String path = Application.persistentDataPath + "/" + ModeManager.currentGameFolder + "/" + _saveDataFileName;
        if (File.Exists(path))
        {
            Debug.Log("Already exist filename");
            file = File.Open(path, FileMode.Open);
        }
        else
            file = File.Create(path);

        StageData data = new StageData();
        data.stageList = stageNameList;
        /*        data.stageList = new List<String>(stageCount);
                for (int i = 0; i < stageCount; i++)
                {
                    stageName[0] = "aaa";
                }*/

        bf.Serialize(file, data);
        file.Close();
    }

    public void ApplyStageInfo()
    {
        stageNameList[currentStageNumber - 1] = _tempFileName;
        SaveStageInfo();
    }

    public void SetStageLoadFileName(int value)
    {
        _tempFileName = GameObject.Find("File Selection").GetComponent<Dropdown>().options[value].text;
        Debug.Log(_tempFileName);
    }

    public void LoadStageInfo()
    {
        String path = Application.persistentDataPath + "/" + ModeManager.currentGameFolder + "/" + _saveDataFileName;
        FileStream file = null;
        try
        {
            if (!File.Exists(path))
            {
                Debug.Log("File not exist!");
                file = File.Create(path);
            }
            else
            {
                file = File.Open(path, FileMode.Open);
            }

            BinaryFormatter bf = new BinaryFormatter();
            StageData data = null;

            data = (StageData)bf.Deserialize(file);
            stageNameList = data.stageList;
        }
        catch (System.Runtime.Serialization.SerializationException e)  // CS0168
        {
            Debug.Log("file is empty");
        }
        catch (System.IO.IOException e)
        {
            Debug.Log("problem");
        }
        try
        {
            file.Close();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Null Reference");
        }
    }
}
