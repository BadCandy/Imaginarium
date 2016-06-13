using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScrollRectSnap : MonoBehaviour {

    public RectTransform panel; 
    public Button[] bttn;
    public GameObject[] gameName;
    public RectTransform center;
    public float distRepositionValue = 440;
    public float[] distance;
    public float[] distReposition;
    
/*    public float[] gameNamesDistance;
    public float[] gameNameDistReposition; */

    
    private int bttnDistance;   // 버튼 사이들의 거리를 유지한다
    private int minButtonNum;   // 중앙까지의 가장 짧은 거리인 버튼의 수를 유지하고  
    private int bttnLength;
    private int gameNameLength;
 /*   private int gameNameDistance;
    private int minGameNameNum;
    private int gameNameLength; */

    private bool dragging = false;

    void Start () {
        bttnLength = bttn.Length;
        gameNameLength = gameName.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);

        //     gameNamesDistance = new float[bttnLength];
        //     gameNameDistReposition = new float[bttnLength];

        // 버튼 사이의 거리를 얻는다.

        //    gameNameDistance = (int)Mathf.Abs(gameName[1].GetComponent<RectTransform>().anchoredPosition.x - gameName[0].GetComponent<RectTransform>().anchoredPosition.x);


    }
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

      //      gameNameDistReposition[i] = center.GetComponent<RectTransform>().position.x - gameName[i].GetComponent<RectTransform>().position.x;
      //      gameNamesDistance[i] = Mathf.Abs(gameNameDistReposition[i]);

            if (distReposition[i] > distRepositionValue)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;

                if (gameNameLength != 0)
                {
                    curX = gameName[i].GetComponent<RectTransform>().anchoredPosition.x;
                    curY = gameName[i].GetComponent<RectTransform>().anchoredPosition.y;

                    newAnchoredPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                    gameName[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
                }
            }

            if (distReposition[i] < -distRepositionValue)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;

                if (gameNameLength != 0)
                {
                    curX = gameName[i].GetComponent<RectTransform>().anchoredPosition.x;
                    curY = gameName[i].GetComponent<RectTransform>().anchoredPosition.y;

                    newAnchoredPos = new Vector2(curX - (gameNameLength * bttnDistance), curY);
                    gameName[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
                }
            }

  /*          if (gameNameDistReposition[i] > 350)
            {
                float curX = gameName[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = gameName[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (bttnLength * gameNameDistance), curY);
                gameName[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }

            if (gameNameDistReposition[i] < -350)
            {
                float curX = gameName[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = gameName[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (bttnLength * gameNameDistance), curY);
                gameName[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }*/
        }

        float minDistance = Mathf.Min(distance);    // 최소 거리를 얻는다
    //    float minGameNameDistance = Mathf.Min(gameNameDistance);
        for (int a = 0; a < bttn.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }

     /*       if (minGameNameDistance == gameNamesDistance[a])
            {
                minGameNameNum = a;
            } */
        }

        if (!dragging)
        {
            //    LerpToBttn(minButtonNum * -bttnDistance);
            LerpToBttn(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
            if (gameNameLength != 0)
            {
                LerpToBttn(-gameName[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
            }
            for (int i = 0; i < bttnLength; i++)
            {
                if (i == minButtonNum)
                {
                    bttn[minButtonNum].interactable = true;
                    if (gameNameLength != 0)
                        gameName[minButtonNum].SetActive(true);
                }
                    
                else
                {
                    bttn[i].image.color = new Color(255, 255, 255, 255);
                    bttn[i].interactable = false;
                    if (gameNameLength != 0)
                        gameName[i].SetActive(false);
                }

    /*            if (i == minGameNameNum)
                {
                    gameName[minGameNameNum].color = Color.black;
                }

                else
                {
                    gameName[i].color = Color.blue;
                }*/
                    
            }
            
        }
            

    }

    private void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 2.5f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;

    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
