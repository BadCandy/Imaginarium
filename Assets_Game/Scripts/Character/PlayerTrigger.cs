using UnityEngine;
using System.Collections;
using Blocks;

// 플레이어에게 전달되는 블럭 메시지를 받아 처리하기 위한 클래스
public class PlayerTrigger : MonoBehaviour
{
    void BlockBurned(Block block)
    {
        LevelManager.Instance.State = LevelManager.LevelState.Build;
    }
}
