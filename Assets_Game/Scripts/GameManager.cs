using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Play,
        Pause,
        End,
    }

    public enum GameMode
    {
        Normal, // 일반
        Runner, // 러너게임 모드
    }

    public int CubeCount
    {
        get
        {
            return _cubeCount;
        }
    }

    GameState _state;
    GameMode _mode;

    int _cubeCount;
    int _collectedCubeCount;
    List<Blocks.Objective> _objectives;
    List<Blocks.Cube> _cubes;

    public void AddObjective(Blocks.Objective objective)
    {
        _objectives.Add(objective);
    }

    public void AddCube(Blocks.Cube cube)
    {
        _cubes.Add(cube);
        _cubeCount++;
    }

    public void CollectCube(Blocks.Cube cube)
    {
        if (cube == null)
            return;

        if (_cubes.Remove(cube))
        {
            _collectedCubeCount++;
            var newScale = new Vector3(1f, (float )_collectedCubeCount / _cubeCount, 1f);

            foreach (var objective in _objectives)
            {
                objective.innerCube.localScale = newScale;
            }
        }
    }

    public void Initialize()
    {
        _objectives = new List<Blocks.Objective>();
        _cubes = new List<Blocks.Cube>();
        _cubeCount = 0;
        _collectedCubeCount = 0;
    }

    // 목표를 달성하여 레벨 클리어
    void Sucess()
    {
        _state = GameState.End;
    }

    // 플레이어가 죽거나 시간이 초과되는 등 레벨 실패
    void Fail()
    {
        _state = GameState.End;
    }
}
