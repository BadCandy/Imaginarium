using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y) { this.x = x; this.y = y; }
    }

    public enum BlockType : byte { Empty, Dirt, GrassedDirt, Bomber, Spring, Flame, Water, Sticky, Iron, Cube, Objective, Length };

    public const int MinWidth = 10;
    public const int MaxWidth = 255;
    public const int MinHeight = 10;
    public const int MaxHeight = 255;

    List<BlockType> _blocks;
    Point _spawn;
    int _width;
    int _height;

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
    public int Width
    {
        get
        {
            return _width;
        }
        set
        {
            SetWidth(value);
        }
    }

    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            SetHeight(value);
        }
    }

    public Level(int width, int height)
    {
        _width = Mathf.Clamp(width, MinWidth, MaxWidth);
        _height = Mathf.Clamp(height, MinHeight, MaxHeight);
        _spawn = new Point(-1, -1);

        // 2배만큼 미리 할당
        _blocks = new List<BlockType>(_width * _height * 2);
        _blocks.AddRange(new BlockType[_width * _height]);
    }

    public BlockType GetBlockAt(int x, int y)
    {
        return _blocks[x * Height + y];
    }

    public BlockType GetBlockAt(Point p)
    {
        return _blocks[p.x * Height + p.y];
    }

    public BlockType[] GetBlocks()
    {
        _blocks.Capacity = _width * _height;
        _blocks.TrimExcess();
        return _blocks.ToArray();
    }

    public void SetBlockAt(int x, int y, BlockType type)
    {
        _blocks[x * Height + y] = type;
    }

    public void SetBlockAt(Point p, BlockType type)
    {
        _blocks[p.x * Height + p.y] = type;
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
        width = Mathf.Clamp(width, MinWidth, MaxWidth);

        if (width == _width)
        {
            return;
        }
        // 너비가 늘어남
        else if (width > _width)
        {
            
        }
        // 너비가 줄어듦
        else if (width < _width)
        {
            for (int i = width; i < _width; i++)
            {
                // 지울 때는 역방향으로
                for (int j = _height - 1; j >= 0; i--)
                {
                    _blocks.RemoveAt(i * _height + j);
                }
            }
        }

        _width = width;
    }

    public void SetHeight(int height)
    {
        height = Mathf.Clamp(height, MinHeight, MaxHeight);

        if (height == _height)
        {
            return;
        }
        // 높이가 늘어남
        else if (height > _height)
        {
            // 너비만큼 마지막에 빈 요소 추가
            for (int i = _height; i < height; i++)
                _blocks.AddRange(new BlockType[_width]);
        }
        // 높이가 줄어듦
        else if (height < _height)
        {
            // 너비만큼 마지막 요소 삭제
            for (int i = 0; i < _width; i++)
            {
                _blocks.RemoveAt(_blocks.Count - 1);
            }
        }

        _height = height;
    }
}
