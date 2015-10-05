using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum bMenuItem {
	fight,
	pkmn,
	item,
	run
}

public class BottomMenu : MonoBehaviour {

	public static BottomMenu S;

	public int activeItem;
	public List<GameObject> menuItems;
	
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
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			switch(activeItem){
			case(int)bMenuItem.fight:
				print("Fight selected");
				gameObject.SetActive (false);
				AttackMenu.S.gameObject.SetActive (true);
				AttackMenu.updateMoves(BattleScreen.playerPokemon);
				AttackMoveView.S.gameObject.SetActive (true);
				break;
			case(int)bMenuItem.pkmn:
				print("Pkmn selected");
				gameObject.SetActive (false);
				BattleScreen.S.gameObject.SetActive(false);
				PokemonSwitchMenu.S.gameObject.SetActive (true);
				PokemonSwitchMenu.UpdateSwitchMenu();
				HPSwitchTxt.S.gameObject.SetActive (true);
				HPSwitchTxt.UpdateHPSwitchTxt();
				break;
			case(int)bMenuItem.item:
				print("Item selected");
				gameObject.SetActive(false);
				BattleScreen.S.gameObject.SetActive(false);
				BattleItemMenu.S.gameObject.SetActive(true);
				BattleItemMenu.UpdateItemMenu();
				break;
			case(int)bMenuItem.run:
				print("Run selected");
				BottomMenu.S.gameObject.SetActive(false);
				TurnActionViewer.S.gameObject.SetActive(true);
				if (Player.S.enemyNo > 3){
					if (UnityEngine.Random.Range(0, 10) > 4){
						TurnActionViewer.S.run = true;
						TurnActionViewer.printMessage("Ran away successfully!");
					}
					else {
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						TurnActionViewer.printMessage("Failed to run!" + "\n\n" + BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.opponentPokemon.move1.moveName);
					}
				}
				else {
						TurnActionViewer.printMessage("Cannot run away from a trainer.");
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

}
