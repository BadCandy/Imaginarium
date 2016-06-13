using UnityEngine;
using System.Collections;
using CnControls;

public class CameraDrag : MonoBehaviour
{
    public float multiplier = 10;
    public int mouseButton;

    private Vector3 _mouseOrigin;
    private Vector3 _cameraOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            _mouseOrigin = Input.mousePosition;
            _cameraOrigin = transform.position;
            return;
        }

        if (Input.GetMouseButton(mouseButton))
        {
            var position = _cameraOrigin + Camera.main.ScreenToViewportPoint(Input.mousePosition - _mouseOrigin) * multiplier;
            position.z = _cameraOrigin.z;
            transform.position = position;
        }

        Vector2 drag = new Vector2(CnInputManager.GetAxis(PC2D.Input.HORIZONTAL), CnInputManager.GetAxis(PC2D.Input.VERTICAL)) * Time.deltaTime * multiplier;
        transform.Translate(drag);
    }
}
