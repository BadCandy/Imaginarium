using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BlockScrollCell : MonoBehaviour
{
    public Image image;
    LevelManager _levelManager;
    int _blockType;

    void Awake()
    {
        _levelManager = LevelManager.Instance;
    }

    void ScrollCellIndex(int idx)
    {
        int length = (int)Level.BlockType.Length;
        gameObject.name = "BlockCell " + idx.ToString();
        
        if(idx >= 0)
            image.sprite = _levelManager.blockSprites[_blockType = (idx % length)];
        else
            image.sprite = _levelManager.blockSprites[_blockType = ((length - ((-idx) % length)) % length)];
    }

    public void OnClick()
    {
        _levelManager.blockToPaint = (Level.BlockType)_blockType;
    }
}
