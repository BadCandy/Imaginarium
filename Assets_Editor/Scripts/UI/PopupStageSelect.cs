using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PopupStageSelect : MonoBehaviour {
    private string stageName;
    GameObject button6;

	// Use this for initialization
	void Start () {
   //     button6 = GameObject.Find("stage6");
    //    button6.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setStageName(string stageName)
    {
        this.stageName = stageName;
        Debug.Log(this.stageName);
    }
    public void SetStageNumber(int number)
    {
        StageManager.currentStageNumber = number;
    }
    public void clickStage(GameObject obj)
    {
        obj.SetActive(!obj.active);
    }
    public void changeStageName(GameObject obj)
    {
        obj.GetComponent<Text>().text = stageName;
    }
} 
