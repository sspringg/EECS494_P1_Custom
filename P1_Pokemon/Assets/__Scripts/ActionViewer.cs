using UnityEngine;
using System.Collections;

public class ActionViewer : MonoBehaviour {

	public static ActionViewer S;

	void Awake() {
		S = this;
	}
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			switch(Opponent.S.cur_action){
			case 1:
				Opponent.S.moveOpponentForward();
				break;
			case 2:
				//Player.S.inScene0 = false;
				//Application.LoadLevelAdditive("_Scene_2");
				Player.S.enemyNo = 4;
				break;
			case 3:
				//place trap
				break;
			case 4:
				//place obstacle
				break;
			}
			gameObject.SetActive(false);
			Opponent.S.action_selected = false;
		}
	}

	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("ActionText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
