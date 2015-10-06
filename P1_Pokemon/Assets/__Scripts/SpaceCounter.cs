using UnityEngine;
using System.Collections;

public class SpaceCounter : MonoBehaviour {
	public static SpaceCounter S;
	// Use this for initialization
	void Awake(){
		S = this;
	}
	
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		GUIText mytext;
		mytext = GameObject.Find ("SpacesCounter").GetComponent<GUIText> ();
		mytext.text = "SPACES MOVED : " + Player.S.spacesMoved + '/' + Player.S.moveLim;
		
		if (Player.S.spacesMoved >= Player.S.moveLim){
			Player.S.moving = false;
			Main.S.playerTurn = false;
			Main.S.inTurn = false;
			Main.S.choiceMade = -1;
			Main.S.paused = true;
			Player.S.allowedToMove = false;
			gameObject.SetActive(false);	
		}
	}
}
