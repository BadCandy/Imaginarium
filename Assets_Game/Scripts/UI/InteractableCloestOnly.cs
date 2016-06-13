using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InteractableCloestOnly : MonoBehaviour
{
    public LoopScrollRect scrollRect;
    public bool invokeOnClickOnInteractable;

	// Update is called once per frame
	void Update ()
    {
        int childCount = scrollRect.content.childCount;

        foreach (RectTransform child in scrollRect.content)
        {
            child.GetComponent<Button>().interactable = false;
        }

        if ((childCount & 1) == 1)
        {
            var button = scrollRect.content.GetChild(childCount >> 1).GetComponent<Button>();

            if (button.interactable == false)
            {
                button.interactable = true;
                if (invokeOnClickOnInteractable) button.onClick.Invoke();
            }
        }
	}
}
