using UnityEngine;
using System.Collections;

public class Level 
{
    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y) { this.x = x; this.y = y; }
    }

    public enum BlockType { Empty, Dirt, GrassedDirt, Bomber, Spring, Flame, Water };

    BlockType[][] _blocks;
    Point _spawn;
    Point _objective;

    public Point Spawn
    {
        get
        {
            return _spawn;
        }
        set
        {
            SetSpawn(value);
        }
    }
    public int TempWidth
    {
        get;
        private set;
    }

    public int TempHeight
    {
        get;
        private set;
    }

    public int Width
    {
        get;
        private set;
    }

    public int Height
    {
        get;
        private set;
    }

    public Level(int width, int height)
    {
        Width = width;
        Height = height;
        _spawn = new Point(-1, -1);

        _blocks = new BlockType[width][];

        for (int i = 0; i < width; i++)
        {
            _blocks[i] = new BlockType[height];
        }
    }

    public BlockType GetBlockAt(int x, int y)
    {
        return _blocks[x][y];
    }

    public BlockType GetBlockAt(Point p)
    {
        return _blocks[p.x][p.y];
    }

    public void SetBlockAt(int x, int y, BlockType type)
    {
        _blocks[x][y] = type;
    }

    public void SetBlockAt(Point p, BlockType type)
    {
        _blocks[p.x][p.y] = type;
    }

    public Point GetSpawn()
    {
        return _spawn;
    }

    public void SetSpawn(Point p)
    {
        SetBlockAt(p, BlockType.Empty);
        _spawn = p;
    }

    public void SetSpawn(int x, int y)
    {
        SetBlockAt(x, y, BlockType.Empty);
        _spawn.x = x;
        _spawn.y = y;
    }

    public void SetWidth(int width)
    {
        Width = width;
    }

    public void SetHeight(int height)
    {
        Height = height;
    }

    public void SetTempWidth(int width)
    {
        TempWidth = width;
    }

    public void SetTempHeight(int height)
    {
        TempHeight = height;
    }
}
