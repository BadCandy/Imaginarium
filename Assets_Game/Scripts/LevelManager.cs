using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LevelManager : Singleton<LevelManager>
{
    public enum LevelState
    {
        Play, // 게임 플레이중
        Build, // 레벨 에디터에서 맵 수정중
        Test, // 레벨 에디터에서 맵 테스트중
        Pause, // 일시정지
    }

    public enum LevelDrawType
    {
        Point,
        Brush,
        Spawn,
    }

    public GameObject gameCamera;
    public GameObject editorCamera;

    public Sprite[] blockSprites;
    public GameObject[] blocks;
    public Sprite playerSprite;
    public GameObject player;
    public GameObject ground;
    public GameObject border;

    public Level.BlockType blockToPaint;
    public LevelDrawType drawType;

    public GameObject inGameObjects;
    public GameObject editorObjects;

    GameObject _levelEditorObjects;
    SpriteRenderer[][] _levelObjectSpriteRenderers;
    GameObject[][] _levelObjects;
    GameObject _map;
    GameObject _levelEditorBackground;

    Level _level;
    LevelState _state;

    public LevelState State
    {
        get
        {
            return _state;
        }

        set
        {
            if (_state == value)
                return;

            if (value == LevelState.Build)
            {
                GameManager.Instance.Initialize();
                editorObjects.SetActive(true);
                inGameObjects.SetActive(false);
                DestroyGameLevel();
            }
            else if (value == LevelState.Test)
            {
                editorObjects.SetActive(false);
                inGameObjects.SetActive(true);
                InstantiateGameLevel();
            }

            _state = value;
        }
    }

    void StartBuild(int width, int height)
    {
        _state = LevelState.Build;
        _level = new Level(width, height);
        editorCamera.transform.position = new Vector3(width / 2, height / 2, -10);

        if (_levelEditorObjects == null)
        {
            _levelEditorObjects = new GameObject();
            _levelEditorObjects.name = "Level Editor Objects";
            _levelEditorObjects.transform.parent = editorObjects.transform;
            InstantiateLevel();
        }

        var cameraClamp = editorCamera.GetComponent<ClampPosition>();

        var vertExtent = editorCamera.GetComponent<Camera>().orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        cameraClamp.horizontalMin = horzExtent - 1.5f;
        cameraClamp.horizontalMax = width - horzExtent + 0.5f;//width / 2.0f - horzExtent;
        cameraClamp.verticalMin = vertExtent - 1.5f;
        cameraClamp.verticalMax = height - vertExtent + 0.5f;
    }

    void Awake()
    {
        StartBuild(width: 30, height: 10); // 기본 크기로 레벨 초기화
        blockToPaint = Level.BlockType.Dirt;
        drawType = LevelDrawType.Brush;
    }

    public int SetLevelWidth(int width)
    {
        width = Mathf.Clamp(width, Level.MinWidth, Level.MaxWidth);

        if (width == _level.Width)
        {
            // 별도 처리 필요 없음
            return width;
        }
        else if (width > _level.Width)
        {
            // 너비 넓어짐
            for (int i = _level.Width; i < width; i++)
            {
                if (_levelObjects[i] == null)
                {
                    _levelObjects[i] = new GameObject[Level.MaxHeight];
                    _levelObjectSpriteRenderers[i] = new SpriteRenderer[Level.MaxHeight];

                    for (int j = 0; j < _level.Height; j++)
                    {
                        _levelObjects[i][j] = new GameObject();
                        _levelObjects[i][j].transform.position = new Vector3(i, j, 0);
                        _levelObjects[i][j].transform.parent = _levelEditorObjects.transform;
                        _levelObjectSpriteRenderers[i][j] = _levelObjects[i][j].AddComponent<SpriteRenderer>();
                        _levelObjectSpriteRenderers[i][j].sprite = blockSprites[0];
                    }
                }
                else
                {
                    // 재활용
                    for (int j = 0; j < _level.Height; j++)
                        _levelObjectSpriteRenderers[i][j].sprite = blockSprites[0];
                }
            }
        }
        else if (width < _level.Width)
        {
            for (int i = width; i < _level.Width; i++)
            {
                for (int j = 0; j < _level.Height; j++)
                {
                    _levelObjectSpriteRenderers[i][j].sprite = null;
                }
            }
        }

        return _level.Width = width;
    }

    public int SetLevelHeight(int height)
    {
        height = Mathf.Clamp(height, Level.MinHeight, Level.MaxHeight);

        if (height == _level.Height)
        {
            // 별도 처리 필요 없음
            return height;
        }
        else if (height > _level.Height)
        {
            for (int j = 0; j < _level.Width; j++)
            {
                for (int i = _level.Height; i < height; i++)
                {
                    if (_levelObjects[i][j] == null)
                    {
                        _levelObjects[i][j] = new GameObject();
                        _levelObjects[i][j].transform.position = new Vector3(i, j, 0);
                        _levelObjects[i][j].transform.parent = _levelEditorObjects.transform;
                        _levelObjectSpriteRenderers[i][j] = _levelObjects[i][j].AddComponent<SpriteRenderer>();
                        _levelObjectSpriteRenderers[i][j].sprite = blockSprites[0];
                    }
                    else
                    {
                        // 재활용
                        _levelObjectSpriteRenderers[i][j] = _levelObjects[i][j].GetComponent<SpriteRenderer>();
                        _levelObjectSpriteRenderers[i][j].sprite = blockSprites[0];
                    }
                }
            }
        }
        else if (height < _level.Height)
        {
            for (int i = 0; i < _level.Width; i++)
            {
                for (int j = height; j < _level.Height; j++)
                {
                    _levelObjectSpriteRenderers[i][j].sprite = null;
                }
            }
        }

        return _level.Height = height;
    }

    public int GetLevelWidth()
    {
        return _level.Width;
    }

    public int GetLevelHeight()
    {
        return _level.Height;
    }

    public void InstantiateLevel()
    {
        _levelObjects = new GameObject[Level.MaxWidth][];
        _levelObjectSpriteRenderers = new SpriteRenderer[Level.MaxWidth][];

        for (int i = 0; i < _level.Width; i++)
        {
            _levelObjects[i] = new GameObject[Level.MaxHeight];
            _levelObjectSpriteRenderers[i] = new SpriteRenderer[Level.MaxHeight];
            for (int j = 0; j < _level.Height; j++)
            {
                _levelObjects[i][j] = new GameObject();
                _levelObjects[i][j].transform.position = new Vector3(i, j, 0);
                _levelObjects[i][j].transform.parent = _levelEditorObjects.transform;
                _levelObjectSpriteRenderers[i][j] = _levelObjects[i][j].AddComponent<SpriteRenderer>();
                _levelObjectSpriteRenderers[i][j].sprite = blockSprites[0];
            }
        }
    }

    public void PaintAt(int x, int y)
    {
        PaintAt(x, y, blockToPaint);
    }

    public void PaintAt(int x, int y, Level.BlockType block)
    {
        if (_level.Spawn.x == x && _level.Spawn.y == y ||
            (x < 0 || x >= _level.Width || y < 0 || y >= _level.Height))
            return;

        _levelObjectSpriteRenderers[x][y].sprite = blockSprites[(int)block];

        _level.SetBlockAt(x, y, block);
    }

    public void StartTest()
    {
        State = LevelState.Test;
    }

    void InstantiateGameLevel()
    {
        _map = new GameObject();
        _map.name = "Level";
        _map.transform.parent = inGameObjects.transform;

        GameObject instantiatedObject;

        for (int i = 0; i < _level.Width; i++)
        {
            for (int j = 0; j < _level.Height; j++)
            {
                Level.BlockType block = _level.GetBlockAt(i, j);

                if (block == Level.BlockType.Empty)
                    continue;

                instantiatedObject = Instantiate(blocks[(int)block - 1],
                                                        new Vector3(i, j, 0),
                                                        Quaternion.identity) as GameObject;
                instantiatedObject.transform.parent = _map.transform;
            }
        }

        // Spawn player
        instantiatedObject = Instantiate(player, new Vector3(_level.Spawn.x, _level.Spawn.y, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.parent = _map.transform;
        var followCamera = gameCamera.GetComponent<FollowCamera>();
        followCamera.target = instantiatedObject;
        followCamera.clamp = true;
        var cameraPosition = followCamera.transform.position;
        cameraPosition.x = _level.Spawn.x;
        cameraPosition.y = _level.Spawn.y;
        followCamera.transform.position = cameraPosition;

        // Spawn Borders
        // Ground
        instantiatedObject = Instantiate(ground, new Vector3(_level.Width / 2, -1, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.localScale = new Vector3(_level.Width + 3, 1, 1);
        instantiatedObject.transform.parent = _map.transform;

        // Bottom
        instantiatedObject = Instantiate(border, new Vector3(_level.Width / 2, -1 - 1, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.localScale = new Vector3(_level.Width + 3, 1, 1);
        instantiatedObject.transform.parent = _map.transform;
        followCamera.groundBorderRenderer = instantiatedObject.GetComponent<MeshRenderer>();

        // Left
        instantiatedObject = Instantiate(border, new Vector3(-1, _level.Height / 2, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.localScale = new Vector3(1, _level.Height + 1, 1);
        instantiatedObject.transform.parent = _map.transform;
        followCamera.leftBorderRenderer = instantiatedObject.GetComponent<MeshRenderer>();

        // Right
        instantiatedObject = Instantiate(border, new Vector3(_level.Width, _level.Height / 2, 0), Quaternion.identity) as GameObject;
        instantiatedObject.transform.localScale = new Vector3(1, _level.Height + 1, 1);
        instantiatedObject.transform.parent = _map.transform;
        followCamera.rightBorderRenderer = instantiatedObject.GetComponent<MeshRenderer>();
    }

    void DestroyGameLevel()
    {
        if (_map != null)
            Destroy(_map);
    }

    public void SpawnAt(int x, int y)
    {
        if ((x < 0 || x >= _level.Width || y < 0 || y >= _level.Height))
            return;

        if (_level.Spawn.x >= 0 && _level.Spawn.y >= 0)
            _levelObjectSpriteRenderers[_level.Spawn.x][_level.Spawn.y].sprite = blockSprites[0];
        _levelObjectSpriteRenderers[x][y].sprite = playerSprite;
        _level.SetSpawn(x, y);
    }

    public string SaveToXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("glml");
        xmlDoc.AppendChild(rootNode);

        // /Game Node
        XmlNode gameNode = xmlDoc.CreateElement("game");
        XmlAttribute id = xmlDoc.CreateAttribute("id");
        id.Value = "1";
        XmlAttribute version = xmlDoc.CreateAttribute("version");
        version.Value = "1";
        gameNode.Attributes.Append(id);
        gameNode.Attributes.Append(version);
        rootNode.AppendChild(gameNode);

        // /Map node
        XmlNode mapNode = xmlDoc.CreateElement("map");
        XmlAttribute width = xmlDoc.CreateAttribute("width");
        width.Value = _level.Width.ToString();
        XmlAttribute height = xmlDoc.CreateAttribute("height");
        height.Value = _level.Height.ToString();
        mapNode.Attributes.Append(width);
        mapNode.Attributes.Append(height);
        rootNode.AppendChild(mapNode);

        // /Map/Spawn Node
        XmlNode spawnNode = xmlDoc.CreateElement("spawn");
        XmlAttribute x = xmlDoc.CreateAttribute("x");
        x.Value = _level.GetSpawn().x.ToString();
        XmlAttribute y = xmlDoc.CreateAttribute("y");
        y.Value = _level.GetSpawn().y.ToString();
        spawnNode.Attributes.Append(x);
        spawnNode.Attributes.Append(y);
        mapNode.AppendChild(spawnNode);

        // /Map/Data Node
        XmlNode dataNode = xmlDoc.CreateElement("blocks");
        dataNode.InnerText = Convert.ToBase64String(Array.ConvertAll(_level.GetBlocks(), value => (byte)value));
        mapNode.AppendChild(dataNode);

        using (var stringWriter = new StringWriter())
        using (var xmlTextWriter = XmlWriter.Create(stringWriter))
        {
            xmlDoc.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            Debug.Log(stringWriter.GetStringBuilder().ToString());
        }

        return "";
    }

    public void LoadFromXml(string xmlString)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlString);

        XmlNodeList nodeList = xml.GetElementsByTagName("game");
        int version, width, height;

        foreach (XmlNode node in nodeList)
        {
            version = Int32.Parse(node.Attributes["version"].Value);
        }

        nodeList = xml.GetElementsByTagName("map");
        foreach (XmlNode node in nodeList)
        {
            width = Int32.Parse(node.Attributes["width"].Value);
            height = Int32.Parse(node.Attributes["height"].Value);
            byte[] bytes = Convert.FromBase64String(node.InnerText);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                byte[] blocks = new BinaryFormatter().Deserialize(stream) as byte[];
                _level = new Level(width, height);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        PaintAt(i, j, (Level.BlockType)blocks[i * j]);
                    }
                }
            }
        }
    }
}
