using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class FileDialog : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        SetDialog();
    }

    public void SetDialog()
    {
  /*      int index = 1;
        string path = Application.persistentDataPath + "/" + "Map";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] fileInfo = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
        GameObject.Find("File Selection").GetComponent<Dropdown>().options.Clear();
        foreach (FileInfo file in fileInfo)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(file.Name);
            GameObject.Find("File Selection").GetComponent<Dropdown>().options.Add(optionData);
            GameObject.Find("File Selection").GetComponent<Dropdown>().value = index++; 
        }*/
    }
}
