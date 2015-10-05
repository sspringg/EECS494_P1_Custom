using UnityEngine;
using System.Collections;

public class FaintedViewer : MonoBehaviour {
	
	public static FaintedViewer S;
	
	void Awake () {
		S = this;
	}
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			gameObject.SetActive (false);
			if (TurnActionViewer.S.allDied) BattleScreen.DestroyHelper();
			else {
				TurnActionViewer.S.activeDied = "";
				BottomMenu.S.gameObject.SetActive(true);
			}
		}
	}
	
	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("FaintedText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
