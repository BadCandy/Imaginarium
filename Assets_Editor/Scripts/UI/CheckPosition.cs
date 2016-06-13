using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    float mouseX;
    float mouseY;
    Vector3 mousePosition;
    public GameObject tileBox;


    /*   public void OnPointerClick (PointerEventData eventData)
       {
           mouseX = (Input.mousePosition.x);
           mouseY = (Input.mousePosition.y);

           mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
           print(mousePosition);

           if (!(GameManager.Instance.State == GameManager.GameState.Pause))
               Instantiate(tileBox, new Vector3((int)mousePosition.x, (int)mousePosition.y, -6), new Quaternion(0, 0, 0, 0));
       }*/

    void OnMouseDown()
    {
        mouseX = (Input.mousePosition.x);
        mouseY = (Input.mousePosition.y);

        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
        print(mousePosition);

        if (!(GameManager.Instance.State == GameManager.GameState.Pause))
            Instantiate(tileBox, new Vector3((int)mousePosition.x, (int)mousePosition.y, -6), new Quaternion(0, 0, 0, 0));
    }
}
