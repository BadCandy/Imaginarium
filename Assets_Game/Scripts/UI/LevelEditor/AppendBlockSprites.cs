using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppendBlockSprites : MonoBehaviour
{
    public GameObject element;
    public float interval = 500;

    LevelManager _levelManager;

    void Awake()
    {
        int length = (int)Level.BlockType.Length;
        _levelManager = LevelManager.Instance;

        for (int i = 0; i < length; i++)
        {
            GameObject instantiatedObject = Instantiate(element,
                                                        new Vector3(i * interval, 0, 0),
                                                        Quaternion.identity) as GameObject;
            instantiatedObject.GetComponent<Image>().sprite = _levelManager.blockSprites[i];
            instantiatedObject.transform.SetParent(transform, false);
            // 갓- 람다
            //int copy = i;
            //_scrollRectSnap.bttn[i].onClick.AddListener(() =>
            //                                        {
            //                                            _levelManager.blockToPaint = (Level.BlockType)copy;
            //                                        });
        }
    }
}
