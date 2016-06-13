using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    private Vector3 _dragOrigin;
    private bool _isStartGame;

    public float dragSpeed = 2;
    public static bool isActiveCamera;


    void Start()
    {
        isActiveCamera = false;
        _isStartGame = false;
    }

  /*  void Update()
    {
        if (isActiveCamera)
            MoveCamera();
    }*/

    public void MoveCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        Camera.main.transform.Translate(move, Space.World);
    }

    public void SetActiveCamera()
    {
        UIManager.isUIActive = true;
        isActiveCamera = !isActiveCamera;
    }

    public void ChangeCameraSize()
    {
        if (!_isStartGame)
            Camera.main.orthographicSize = 3;

        else
            Camera.main.orthographicSize = 8;

        _isStartGame = !_isStartGame;
    }

    public void ReturnCamera()
    {
        Camera.main.transform.position = new Vector3(5, 5, -10);
    }
}
