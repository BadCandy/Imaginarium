using System;

[Serializable]
class LevelData
{
    public int width;   // level width
    public int height;  // level height
    public int characterX;
    public int characterY;
    public Int16[][] checkBlock;  // 블록의 유무
}