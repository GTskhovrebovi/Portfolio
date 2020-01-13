using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGui : MonoBehaviour {

	public Texture cursorImage1;
	public Texture cursorImage2;

	Texture cursorImage;


	void Start () {
		cursorImage = cursorImage1;
		Cursor.visible = false;
	}

	void OnGUI() { 
		GUI.depth = 0;
		Vector3 mousePos = Input.mousePosition;
		Rect pos = new Rect(mousePos.x, Screen.height - mousePos.y, cursorImage.width, cursorImage.height);
		GUI.Label(pos,cursorImage);
	}
	
	public void ChangeCursor (int playerNum) {
		if (playerNum == 1)
			cursorImage = cursorImage1;
		else if(playerNum == 2){
			cursorImage = cursorImage2;
		}
	}
}
