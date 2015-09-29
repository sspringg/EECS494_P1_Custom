using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum sMenuItem {
	Pokemon1,
	Pokemon2,
	Pokemon3,
	Pokemon4,
	Pokemon5,
	Pokemon6
}

public class PokemonSwitchMenu : MonoBehaviour {

	public static PokemonSwitchMenu S;

	public int activeItem;
	public List<GameObject> menuItems;
	public string msg;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach (Transform child in transform) {
			menuItems.Add (child.gameObject);
		}
		menuItems = menuItems.OrderByDescending (m => m.transform.position.y).ToList ();
		
		foreach (GameObject go in menuItems) {
			GUIText itemText = go.GetComponent<GUIText> ();
			if (first)
				itemText.color = Color.red;
			first = false;
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			switch(activeItem){
			case(int)sMenuItem.Pokemon1:
				print("Pk1 selected");
				if (Player.S.pokemon_list[0].pkmnName == "None" || Player.S.pokemon_list[0].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[0].pkmnName;
					}
					else{
						Player.S.pokemon_list[0].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + Player.S.pokemon_list[0].pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[0].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[0]);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)sMenuItem.Pokemon2:
				print("Pk2 selected");
				if (Player.S.pokemon_list[1].pkmnName == "None" || Player.S.pokemon_list[1].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[1].pkmnName;
					}
					else{
						Player.S.pokemon_list[1].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + Player.S.pokemon_list[1].pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[1].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[1]);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)sMenuItem.Pokemon3:
				print("Pk3 selected");
				if (Player.S.pokemon_list[2].pkmnName == "None" || Player.S.pokemon_list[2].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[2].pkmnName;
					}
					else{
						Player.S.pokemon_list[2].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + Player.S.pokemon_list[2].pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[2].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[2]);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)sMenuItem.Pokemon4:
				print("Pk4 selected");
				if (Player.S.pokemon_list[3].pkmnName == "None" || Player.S.pokemon_list[3].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[3].pkmnName;
					}
					else{
						Player.S.pokemon_list[3].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + BattleScreen.playerPokemon.pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[3].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[3]);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)sMenuItem.Pokemon5:
				print("Pk5 selected");
				if (Player.S.pokemon_list[4].pkmnName == "None" || Player.S.pokemon_list[4].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[4].pkmnName;
					}
					else{
						Player.S.pokemon_list[4].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + BattleScreen.playerPokemon.pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[4].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[4]);
					TurnActionViewer.S.gameObject.SetActive (true);
					TurnActionViewer.printMessage (msg);
				}
				break;
			case(int)sMenuItem.Pokemon6:
				print("Pk6 selected");
				if (Player.S.pokemon_list[5].pkmnName == "None" || Player.S.pokemon_list[5].curHp <= 0){
					print("this pkmn cannot be selected");
				}
				else {
					if (BattleScreen.opponentPokemon.speed > BattleScreen.playerPokemon.speed){
						BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName + "\n\n";
						msg += "Red switches his Pokemon to " + Player.S.pokemon_list[5].pkmnName;
					}
					else{
						Player.S.pokemon_list[5].takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
						msg = "Red switches his Pokemon to " + Player.S.pokemon_list[5].pkmnName + "\n\n";
						msg += BattleScreen.opponentPokemon.pkmnName + " attacks " + Player.S.pokemon_list[5].pkmnName + " with " + BattleScreen.playerPokemon.move1.moveName;
					}
					gameObject.SetActive(false);
					HPSwitchTxt.S.gameObject.SetActive(false);
					BattleScreen.S.gameObject.SetActive(true);
					BattleScreen.updatePokemon(true, Player.S.pokemon_list[5]);
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
			HPSwitchTxt.S.gameObject.SetActive(false);
			BottomMenu.S.gameObject.SetActive(true);
			BattleScreen.S.gameObject.SetActive(true);
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

	public static void UpdateSwitchMenu(){
		GUIText mytext; //BattleScreen.playerPokemon.pkmnName;
		
		mytext = GameObject.Find ("Pokemon1").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [0].pkmnName;
		mytext = GameObject.Find ("Pokemon2").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [1].pkmnName;
		mytext = GameObject.Find ("Pokemon3").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [2].pkmnName;
		mytext = GameObject.Find ("Pokemon4").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [3].pkmnName;
		mytext = GameObject.Find ("Pokemon5").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [4].pkmnName;
		mytext = GameObject.Find ("Pokemon6").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list [5].pkmnName;
	}
}
