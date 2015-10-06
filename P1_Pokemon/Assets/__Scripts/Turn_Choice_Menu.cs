using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public enum Turn_Choices{
	Move,
	Place_Pokemon,
	Choose_Item,
	Battle
}
public class Turn_Choice_Menu : MonoBehaviour {
	public static Turn_Choice_Menu S;
	public int activeItem;
	public List<GameObject> Choices;
	public bool	pause_turn_menu = false;
	private float randomVal;
	// Use this for initialization
	public UnityEngine.Random random = new UnityEngine.Random();
	void Awake(){
		S = this;
	}
	
	void Start () {
		bool first = true;
		activeItem = 0;
		foreach(Transform child in transform){
			Choices.Add(child.gameObject);
		}
		Choices = Choices.OrderByDescending(m => m.transform.position.y).ToList();
		
		foreach(GameObject go in Choices){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false; 
		}
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.S.playerTurn){
			Player.S.allowedToMove = false;
			if(Input.GetKeyDown(KeyCode.A) && !pause_turn_menu){
				switch(activeItem){ 
				case(int)Turn_Choices.Move:
					Player.S.spacesMoved = 0;
					Player.S.allowedToMove = true;
					Player.S.moveLim = UnityEngine.Random.Range(1, 10);
					SpaceCounter.S.gameObject.SetActive(true);
					Main.S.choiceMade = 0;
					Player.S.allowedToMove = true;
					gameObject.SetActive(false);
					break;
				case(int)Turn_Choices.Place_Pokemon:
					if(Player.S.pokemon_list.Count == 1){
						Dialog.S.gameObject.SetActive(true);
						Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
						noAlpha.a = 255;
						Dialog.S.ShowMessage("It's too dangerous to have no pokemon");
					}
					Pokemon_Menu.S.place_pokemon_choice = true;
					Pokemon_Menu.S.gameObject.SetActive(true);
					Main.S.choiceMade = 1;
					gameObject.SetActive(false);
					break;
				case(int)Turn_Choices.Choose_Item:
					Main.S.choiceMade = 2;
					Items_Menu.S.gameObject.SetActive(true);
					gameObject.SetActive(false);
					break;
				case(int)Turn_Choices.Battle:
					Main.S.choiceMade = 3;
					if(checkDistance((int)Player.S.transform.position.x, (int)Player.S.transform.position.y)){
						Opponent.S.moveTowardPlayer = true;
						gameObject.SetActive(false);
					}
					else{
						Dialog.S.gameObject.SetActive(true);
						Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
						noAlpha.a = 255;
						Dialog.S.ShowMessage("Need to be in front of Opponent to call battle");
					}
					break;
				}
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) && Menu.S.gameObject.activeSelf == false){
				MoveDownMenu();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) && Menu.S.gameObject.activeSelf == false){
				MoveUpMenu();
			}
		}
	}
	public void MoveDownMenu(){
		Choices[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == Choices.Count - 1 ? 0: ++activeItem;
		Choices[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		Choices[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? Choices.Count - 1: --activeItem;
		Choices[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public bool checkDistance(int x, int y){
		double playDist = Math.Sqrt(Math.Pow(75 - x, 2) + Math.Pow(117 - y, 2)) + 1;
		double opponentDist = Math.Sqrt(Math.Pow(75 - Opponent.S.transform.position.x, 2) + Math.Pow(117 - Opponent.S.transform.position.y, 2));
		return playDist < opponentDist;
		
	}
}
