using UnityEngine;
using System.Collections;

public class LevelUpViewer : MonoBehaviour {
	
	public static LevelUpViewer S;
	
	void Awake () {
		S = this;
	}
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			BattleScreen.DestroyHelper ();
		}
	}
	
	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("LevelUpText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
