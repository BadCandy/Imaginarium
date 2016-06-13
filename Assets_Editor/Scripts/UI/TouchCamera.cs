using UnityEngine;
using System.Collections;

public class TouchCamera : MonoBehaviour
{

    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;
    public bool updateZoomSensitivity = true;
    public float orthoZoomSpeed = 0.05f;
    public float minZoom = 1.0f;
    public float maxZoom = 20.0f;
    public bool invertMoveX = false;
    public bool invertMoveY = false;
    public bool useOneTouchCameraMove = false;
    public bool useTwoTouchCameraMove = true;

    private Transform _transform;
    private Camera _camera;

    void Start()
    {
        _transform = transform;
        _camera = Camera.main;
   //     _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateZoomSensitivity)
        {
            moveSensitivityX = _camera.orthographicSize / 5.0f;
            moveSensitivityY = _camera.orthographicSize / 5.0f;
        }
        Touch[] touches = Input.touches;

        if (touches.Length > 0)
        {
            if (touches.Length == 1 && useOneTouchCameraMove)        // 이동
            {
                if (touches[0].phase == TouchPhase.Moved)   //이동
                {
                    Vector2 delta = touches[0].deltaPosition;

                    float positionX = delta.x * moveSensitivityX * Time.deltaTime;
                    positionX = invertMoveX ? positionX : positionX * -1;

                    float positionY = delta.y * moveSensitivityY * Time.deltaTime;
                    positionY = invertMoveY ? positionY : positionY * -1;

                    _camera.transform.position += new Vector3(positionX, positionY, 0);
                }
            }
        }

        if (touches.Length == 2)
        {
            Touch touchOne = touches[0];        
            Touch touchTwo = touches[1];
            float distance = Mathf.Abs(Vector2.Distance(touchOne.position, touchTwo.position));
            if (distance < 50.0f && useTwoTouchCameraMove)
            {
                if (touches[0].phase == TouchPhase.Moved)   //이동
                {
                    Vector2 delta = touches[0].deltaPosition;

                    float positionX = delta.x * moveSensitivityX * Time.deltaTime;
                    positionX = invertMoveX ? positionX : positionX * -1;

                    float positionY = delta.y * moveSensitivityY * Time.deltaTime;
                    positionY = invertMoveY ? positionY : positionY * -1;

                    _camera.transform.position += new Vector3(positionX, positionY, 0);
                }
            }

            else        // 확대
            {
                if (useTwoTouchCameraMove)
                {
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                    Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

                    float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
                    float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

                    float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

                    _camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
                    _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
                }
            }
        }
    }

  /*  void LateUpdate()
    {
              if (!useTwoTouchCameraMove) { 
                Vector3 position = Camera.main.transform.position;
                if (position.x < 2.0f)
                    Camera.main.transform.position = new Vector3(2.0f, position.y, position.z);
                if (position.x > 14.0f)
                    Camera.main.transform.position = new Vector3(14.0f, position.y, position.z);
                if (position.y < 2.0f)
                    Camera.main.transform.position = new Vector3(position.x, 2.0f, position.z);
                if (position.y > 10.0f)
                    Camera.main.transform.position = new Vector3(position.x, 10.0f, position.z);
                }
    }*/
}
