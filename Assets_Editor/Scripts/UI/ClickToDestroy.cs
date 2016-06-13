using UnityEngine;
using System.Collections;

public class ClickToDestroy : MonoBehaviour {

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
