using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dialog : MonoBehaviour {
	
	public static Dialog S;
	void Awake(){
		S = this;
	}
	
	// Use this for initialization
	void Start () {
		HideDialogBox();
	}
	public void ShowMessage(string message){
		GameObject dialogBox = transform.Find("Text").gameObject;
		Text goText = dialogBox.GetComponent<Text>();
		goText.text = message;
		Main.S.inDialog = true;
	}
	// Update is called once per frame
	void Update () {
		if(Main.S.inDialog && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) && !Menu.S.pokemon_menu_active){
			HideDialogBox();
			if(Player.S.playerSpeaking != null)
				Player.S.CheckForAction();
		}
	}
	public void HideDialogBox(){
		Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
		noAlpha.a = 0;
		GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
		gameObject.SetActive(false);
		Main.S.inDialog = false;
	}
}
