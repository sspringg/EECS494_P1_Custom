using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager S;
	// Use this for initialization
	bool playerTurn = true;
	public bool gameStart = false;
	void Start () {
		S = this;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(playerTurn){
			playerTurn = false;
			Turn_Choice_Menu.S.gameObject.SetActive(true);
		
		}
	}
}
