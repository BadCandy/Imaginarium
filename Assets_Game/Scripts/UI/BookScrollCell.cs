using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BookScrollCell : MonoBehaviour
{
    public Text text;
    public string[] bookNames;


    void ScrollCellIndex(int idx)
    {
        if (idx >= 0)
            text.text = bookNames[idx % bookNames.Length];
        else
            text.text = bookNames[(bookNames.Length - (-idx % bookNames.Length)) % bookNames.Length];
    }
}
