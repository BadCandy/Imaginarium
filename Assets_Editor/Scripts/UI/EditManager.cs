using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditManager : MonoBehaviour {
    public GameObject currentBlock;
    public GameObject currentBrush;

    private int blockBtnLength;
    private int burshBtnLength;

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void ChangeCurrentBlockImgs(GameObject block)
    {
        currentBlock.GetComponent<Image>().sprite = block.GetComponent<Image>().sprite;
    }

    public void ChangeCurrentBrushImgs(GameObject brush)
    {
        currentBrush.GetComponent<Image>().sprite = brush.GetComponent<Image>().sprite;
    }
}
