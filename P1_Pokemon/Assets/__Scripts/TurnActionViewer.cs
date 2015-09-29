using UnityEngine;
using System.Collections;

public class TurnActionViewer : MonoBehaviour {

	public static TurnActionViewer S;
	public string[] endText;
	public string printText;
	public int lim;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		printText = "";
		lim = 0;
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
