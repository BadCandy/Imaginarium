using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ModeManager : MonoBehaviour {
    
    [HideInInspector]
    public static string currentGameFolder;

    public void SetGameFolder(string folderName)
    {
        String path = Application.persistentDataPath + "/" + folderName;
        if (!Directory.Exists(path))
        {
            Debug.Log("Directory not exist!");
            Directory.CreateDirectory(path);
            
        }
        currentGameFolder = folderName;
    }
	
}
