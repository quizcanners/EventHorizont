using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangableText : MonoBehaviour {

	public Text text;
	int count=666;

    private void OnEnable()
    {
        if (text == null)
            text = GetComponent<Text>();

    }

    public void ChangeCountText(string NewText, int NewCount){
		if (count != NewCount) {
				text.text = NewText+NewCount;
				count=NewCount;

			}
	}


public void ChangeText(string NewText){
		
		text.text = NewText; 
	}
}
