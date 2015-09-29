using UnityEngine;
using System.Collections;

public class EndTextViewer : MonoBehaviour {
	
	public static EndTextViewer S;
	public int i;
	
	void Awake () {
		S = this;
	}
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if (i < TurnActionViewer.S.lim){
				if (TurnActionViewer.S.endText[i] != "")
					printMessage(TurnActionViewer.S.endText[i]);
				++i;
			}
			else {
				i = 0;
				TurnActionViewer.S.lim = 0;
				for (int j = 0; j < 7; ++j) {
					TurnActionViewer.S.endText[j] = "";
				}
				if (TurnActionViewer.S.printText != "") {
					TurnActionViewer.S.printText += "have leveled up";
					gameObject.SetActive(false);
					LevelUpViewer.S.gameObject.SetActive (true);
					LevelUpViewer.printMessage (TurnActionViewer.S.printText);
				} else {
					BattleScreen.DestroyHelper ();
				}
			}
		}
	}
	
	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("EndText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
