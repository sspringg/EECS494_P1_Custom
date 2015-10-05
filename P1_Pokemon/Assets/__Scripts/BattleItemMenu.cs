using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum iMenuitem	{
	Item1,
	Item2,
	Item3,
	Item4,
	Item5,
	Item6
}

public class BattleItemMenu : MonoBehaviour {

	public static BattleItemMenu S;

	public int activeItem;
	public List<GameObject> menuItems;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach (Transform child in transform) {
			if (child.gameObject.name == "Header") continue;
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
			case(int)iMenuitem.Item1:
				useItem(0);
				break;
			case(int)iMenuitem.Item2:
				useItem(1);
				break;
			case(int)iMenuitem.Item3:
				useItem(2);
				break;
			case(int)iMenuitem.Item4:
				useItem(3);
				break;
			case(int)iMenuitem.Item5:
				useItem(4);
				break;
			case(int)iMenuitem.Item6:
				useItem(5);
				break;
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			MoveDownMenu();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			MoveUpMenu();
		}
		else if (Input.GetKeyDown(KeyCode.S)){
			gameObject.SetActive(false);
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
	
	public static void UpdateItemMenu(){
		GUIText mytext;
		int i = 0;
		foreach (KeyValuePair<string, int> pair in Player.S.itemsDictionary) {
			if (i > 5) break;
			mytext = GameObject.Find(((iMenuitem)i).ToString()).GetComponent<GUIText>();
			mytext.text = pair.Key + ' ' + pair.Value;
			++i;
		}
	}

	public void useItem(int index){
		if (Player.S.itemsDictionary.Keys.ElementAt(index) == "POKeBALL"){
			--Player.S.itemsDictionary[Player.S.itemsDictionary.Keys.ElementAt(index)];
			gameObject.SetActive(false);
			BattleScreen.S.gameObject.SetActive(true);
			TurnActionViewer.S.gameObject.SetActive(true);
			if (Player.S.itemsDictionary.Values.ElementAt(index) == 0) Player.S.itemsDictionary.Remove("POKeBALL");
			if (UnityEngine.Random.Range(0, 9) > 2){
				for (int i = 0; i < 6; ++i){
					if (Player.S.pokemon_list[i].pkmnName == "None"){
						Player.S.pokemon_list[i] = BattleScreen.opponentPokemon;
						TurnActionViewer.S.pokecaught = true;
						TurnActionViewer.printMessage("You caught " + BattleScreen.opponentPokemon.pkmnName + '.');
						break;
					}
				}
			}
			else {
				BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
				TurnActionViewer.printMessage("You couldn't catch " + BattleScreen.opponentPokemon.pkmnName + '.' + "\n\n" + BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.opponentPokemon.move1.moveName);
			}
		}
		else if (Player.S.itemsDictionary.Keys.ElementAt(index) == "POTION") {
			--Player.S.itemsDictionary[Player.S.itemsDictionary.Keys.ElementAt(index)];
			gameObject.SetActive(false);
			BattleScreen.S.gameObject.SetActive(true);
			TurnActionViewer.S.gameObject.SetActive(true);
			if (Player.S.itemsDictionary.Values.ElementAt(index) == 0) Player.S.itemsDictionary.Remove("POTION");
			BattleScreen.playerPokemon.takeHit(BattleScreen.opponentPokemon.move1, BattleScreen.opponentPokemon, true);
			if (BattleScreen.playerPokemon.curHp + 10 > BattleScreen.playerPokemon.totHp) BattleScreen.playerPokemon.curHp = BattleScreen.playerPokemon.totHp;
			else BattleScreen.playerPokemon.curHp += 10;
			TurnActionViewer.printMessage("You healed " + BattleScreen.playerPokemon.pkmnName + '.' + "\n\n" + BattleScreen.opponentPokemon.pkmnName + " attacks " + BattleScreen.playerPokemon.pkmnName + " with " + BattleScreen.opponentPokemon.move1.moveName);
		}
	}
}
