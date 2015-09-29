using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum aMenuItem {
	move1,
	move2,
	move3,
	move4
}

public class AttackMenu : MonoBehaviour {
	
	public static AttackMenu S;
	
	public int activeItem;
	public List<GameObject> menuItems;
	public string msg;
	
	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach (Transform child in transform) {
			menuItems.Add (child.gameObject);
		}
		menuItems = menuItems.OrderByDescending(m => m.transform.position.y).ToList();
		
		foreach (GameObject go in menuItems) {
			GUIText itemText = go.GetComponent<GUIText> ();
			if (first)
				itemText.color = Color.red;
			first = false;
		}
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		AttackMoveView.updateMoveView(activeItem, BattleScreen.playerPokemon);
		PokemonObject playerPkmn = BattleScreen.playerPokemon;
		PokemonObject oppoPkmn = BattleScreen.opponentPokemon;
		if(Input.GetKeyDown(KeyCode.A)){
			switch(activeItem){
			case(int)aMenuItem.move1:
				print("Move1 selected");
				if (playerPkmn.move1.moveName == "None" || playerPkmn.move1.curPp <= 0){
					print ("this move isn't available");
				}
				else if (playerPkmn.speed >= oppoPkmn.speed){
					oppoPkmn.takeHit(playerPkmn.move1, playerPkmn, false);
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true); 
					msg = playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move1.moveName + '\n'+ '\n';
					msg += oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				} else{
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					oppoPkmn.takeHit(playerPkmn.move1, playerPkmn, false);
					msg = oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName + '\n'+ '\n';
					msg += playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move1.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)aMenuItem.move2:
				print("Move2 selected");
				if (playerPkmn.move2.moveName == "None" || playerPkmn.move1.curPp <= 0){
					print ("this move isn't available");
				}
				else if (playerPkmn.speed >= oppoPkmn.speed){
					oppoPkmn.takeHit(playerPkmn.move2, playerPkmn, false);
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					msg = playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move2.moveName + '\n'+ '\n';
					msg += oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				} else{
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					oppoPkmn.takeHit(playerPkmn.move2, playerPkmn, false);
					msg = oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName + '\n'+ '\n';
					msg += playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move2.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)aMenuItem.move3:
				print("Move3 selected");
				if (playerPkmn.move3.moveName == "None" || playerPkmn.move1.curPp <= 0){
					print ("this move isn't available");
				}
				else if (playerPkmn.speed >= oppoPkmn.speed){
					oppoPkmn.takeHit(playerPkmn.move3, playerPkmn, false);
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					msg = playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move3.moveName + '\n'+ '\n';
					msg += oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				} else{
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					oppoPkmn.takeHit(playerPkmn.move3, playerPkmn, false);
					msg = oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName + '\n'+ '\n';
					msg += playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move3.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)aMenuItem.move4:
				print("Move4 selected");
				if (playerPkmn.move4.moveName == "None" || playerPkmn.move1.curPp <= 0){
					print ("this move isn't available");
				}
				else if (playerPkmn.speed >= oppoPkmn.speed){
					oppoPkmn.takeHit(playerPkmn.move4, playerPkmn, false);
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					msg = playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move4.moveName + '\n'+ '\n';
					msg += oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				} else{
					playerPkmn.takeHit(oppoPkmn.move1, oppoPkmn, true);
					oppoPkmn.takeHit(playerPkmn.move4, playerPkmn, false);
					msg = oppoPkmn.pkmnName + " attacks " + playerPkmn.pkmnName + " with " + oppoPkmn.move1.moveName + '\n'+ '\n';
					msg += playerPkmn.pkmnName + " attacks " + oppoPkmn.pkmnName + " with " + playerPkmn.move4.moveName;
					AttackMenu.S.gameObject.SetActive (false);
					AttackMoveView.S.gameObject.SetActive (false);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			MoveDownMenu();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			MoveUpMenu();
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			gameObject.SetActive(false);
			AttackMoveView.S.gameObject.SetActive(false);
			BottomMenu.S.gameObject.SetActive(true);
		}
	}
	public void MoveDownMenu(){
		menuItems[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == menuItems.Count - 1 ? 0: ++activeItem;
		menuItems[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		menuItems[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? menuItems.Count - 1: --activeItem;
		menuItems[activeItem].GetComponent<GUIText>().color = Color.red;	
	}

	public static void updateMoves(PokemonObject curPkmn){
		GUIText myText;

		myText = GameObject.Find ("Move1").GetComponent<GUIText>();
		myText.text = curPkmn.move1.moveName;
		myText = GameObject.Find ("Move2").GetComponent<GUIText>();
		myText.text = curPkmn.move2.moveName;
		myText = GameObject.Find ("Move3").GetComponent<GUIText>();
		myText.text = curPkmn.move3.moveName;
		myText = GameObject.Find ("Move4").GetComponent<GUIText>();
		myText.text = curPkmn.move4.moveName;
	}
}