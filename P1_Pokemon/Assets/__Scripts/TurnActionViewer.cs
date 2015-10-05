using UnityEngine;
using System.Collections;

public class TurnActionViewer : MonoBehaviour {

	public static TurnActionViewer S;
	public string[] endText;
	public string printText;
	public int lim;
	public string activeDied;
	public bool allDied;
	public bool run;
	public bool pokecaught;
	public double diffmod;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		printText = "";
		lim = 0;
		activeDied = "";
		allDied = false;
		run = false;
		pokecaught = false;
		diffmod = 1;
		endText = new string[8];
		for (int i = 0; i < 8; ++i) {
			endText [i] = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			gameObject.SetActive (false);
			if (BattleScreen.opponentPokemon.curHp <= 0) {
				if (activeDied != "") endText[lim++] = activeDied + " has fainted.";
				int x, y;
				printText = "";
				endText[lim++] = BattleScreen.opponentPokemon.pkmnName + " has fainted.";
				for (int i = 0; i < 6; ++i) {
					if (Player.S.pokemon_list [i].curHp > 0 && Player.S.pokemon_list [i].fought) {
						y = 31 * BattleScreen.opponentPokemon.level;
						Player.S.pokemon_list [i].exp += y;
						endText[lim++] = Player.S.pokemon_list [i].pkmnName + " has gained " + y + " exp.";
						x = Player.S.pokemon_list [i].level + 1;
						Player.S.pokemon_list [i].fought = false;
						if (Player.S.pokemon_list [i].exp > x * x * x) {
							++Player.S.pokemon_list [i].level;
							Player.S.pokemon_list [i].totHp += 5;
							Player.S.pokemon_list [i].curHp += 5;
							Player.S.pokemon_list [i].atk += 5;
							Player.S.pokemon_list [i].def += 5;
							Player.S.pokemon_list [i].spAtk += 5;
							Player.S.pokemon_list [i].spDef += 5;
							Player.S.pokemon_list [i].speed += 5;
							printText += Player.S.pokemon_list [i].pkmnName + ", ";
						}
					}
				}
				endText[lim++] = "Player has gained 500 Pokemon money.";
				Player.S.money += 500;
				AttackMenu.S.gameObject.SetActive (false);
				AttackMoveView.S.gameObject.SetActive (false);
				EndTextViewer.S.gameObject.SetActive(true);
			}
			else if (allDied){
				AttackMenu.S.gameObject.SetActive (false);
				AttackMoveView.S.gameObject.SetActive (false);
				FaintedViewer.S.gameObject.SetActive(true);
				FaintedViewer.printMessage("All your Pokemon have fainted,\nyou will be transfered to the \nnearest Pokemon center.");
			}
			else if (activeDied != ""){
				AttackMenu.S.gameObject.SetActive (false);
				AttackMoveView.S.gameObject.SetActive (false);
				FaintedViewer.S.gameObject.SetActive(true);
				FaintedViewer.printMessage(activeDied + " has fainted, switching to next Pokemon.");
				diffmod = 1;
			}
			else if (run || pokecaught) {
				BattleScreen.DestroyHelper();
			}
			else if (diffmod != 1 && diffmod != 11) {
				print ("cur diffmod : " + diffmod.ToString());
				AttackMenu.S.gameObject.SetActive (false);
				AttackMoveView.S.gameObject.SetActive (false);
				FaintedViewer.S.gameObject.SetActive(true);
				if (diffmod < 10){
					if (diffmod == 0){
						FaintedViewer.printMessage(BattleScreen.opponentPokemon.pkmnName + "'s attack had no effect");
					}
					else if (diffmod == 0.5){
						FaintedViewer.printMessage(BattleScreen.opponentPokemon.pkmnName + "'s attack was not very effective");
					}
					else{
						FaintedViewer.printMessage(BattleScreen.opponentPokemon.pkmnName + "'s attack was super effective");
					}
				}
				else {
					if (diffmod == 10){
						FaintedViewer.printMessage(BattleScreen.playerPokemon.pkmnName + "'s attack had no effect");
					}
					else if (diffmod == 10.5){
						FaintedViewer.printMessage(BattleScreen.playerPokemon.pkmnName + "'s attack was not very effective");
					}
					else{
						FaintedViewer.printMessage(BattleScreen.playerPokemon.pkmnName + "'s attack was super effective");
					}
				}
				diffmod = 1;
			}
			else {
				BottomMenu.S.gameObject.SetActive (true);
			}
		}
	}

	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("TurnText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
