using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pokemon_Menu : MonoBehaviour {
	public int activeItem, pokemon_menu_chosen;
	public static Pokemon_Menu S;
	public List<GameObject> Poke_lists;
	public List<GameObject>	Perm_Items; 
	string key, value;
	public PokemonObject placePokemon;
	public bool pokemon_menu_2_active = false, Pokemon_Menu_paused = false, moving_pokemon = false, bad_choice = false;
	public bool place_pokemon_choice = false;
	void Awake(){
		S = this;
	}
	void Start () {
		bool first = true;
		foreach(Transform child in transform){
			Poke_lists.Add(child.gameObject);
		}
		foreach(GameObject go in Poke_lists){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false;  
		}
		Perm_Items = Poke_lists;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.S.pokemon_list.Count == 0){
			gameObject.SetActive(false);
			Menu.S.menuPaused = false;
			Menu.S.pokemon_menu_active = false;	
		}
		else if(Input.GetKeyDown(KeyCode.A) && place_pokemon_choice){
			place_pokemon_choice = false;
			POKeBALL.S.gameObject.SetActive(true);
			pokemon_menu_chosen = activeItem;
			POKeBALL.S.transform.position = new Vector3(Player.S.transform.position.x, Player.S.transform.position.y + 1, Player.S.transform.position.z);
			gameObject.SetActive(false);
		}
		else if (!pokemon_menu_2_active && Player.S.pokemon_list[0].pkmnName != "None"){ //removed Menu.S.menuPaused &&  for item turn menu
			setPlayerItems();
			if(Input.GetKeyDown(KeyCode.A) && !moving_pokemon && !Items_Menu_2.S.usingItem){
					Pokemon_Menu_2.S.gameObject.SetActive(true);
					pokemon_menu_2_active = true;
					Pokemon_Menu_paused = true;
			}
			else if(Input.GetKeyDown(KeyCode.A) && moving_pokemon){
				PokemonObject temp = Player.S.pokemon_list[activeItem];
				Player.S.pokemon_list[activeItem] = Player.S.pokemon_list[pokemon_menu_chosen];
				Player.S.pokemon_list[pokemon_menu_chosen] = temp;
				moving_pokemon = false;
			}
			else if(Input.GetKeyDown(KeyCode.A) && Items_Menu_2.S.usingItem){
				Dialog.S.gameObject.SetActive(true);
				Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
				noAlpha.a = 255;
				GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
				if(Items_Menu.S.itemChosen == "POKeBALL" || Items_Menu.S.itemChosen == "Prof_Oak_Package"){
					Items_Menu.S.gameObject.SetActive(true);
					Dialog.S.ShowMessage("This isn't the time to use that");
					gameObject.SetActive(false);
				}
				else if(Items_Menu.S.itemChosen == "Potion"){ //change to poison
					Player.S.itemsDictionary[Items_Menu.S.itemChosen]--;
					if(Player.S.itemsDictionary[Items_Menu.S.itemChosen] == 0)
						Player.S.itemsDictionary.Remove(Items_Menu.S.itemChosen);	//remove item if we have 0 of them
					Player.S.pokemon_list[activeItem].curHp += 10;
					if(Player.S.pokemon_list[activeItem].curHp > Player.S.pokemon_list[activeItem].totHp)
						Player.S.pokemon_list[activeItem].curHp = Player.S.pokemon_list[activeItem].totHp;
					Dialog.S.ShowMessage(Player.S.pokemon_list[activeItem].pkmnName + " is cured");
					Items_Menu_2.S.usingItem = false;
					Main.S.playerTurn = false;
					Main.S.inTurn = false;
					Main.S.choiceMade = false;
					gameObject.SetActive(false);
				}
				else{
					Items_Menu.S.gameObject.SetActive(true);
					Dialog.S.ShowMessage("This will have no effect");
					gameObject.SetActive(false);
				}
				
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow) && !Pokemon_Menu_paused){
				MoveDownMenu();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) && !Pokemon_Menu_paused){
				MoveUpMenu();
			}
		}
		if(Input.GetKeyDown(KeyCode.S) && pokemon_menu_2_active && !Main.S.choiceMade){ //added choiceMade
			Pokemon_Menu_paused = false;
			pokemon_menu_2_active = false;
			Pokemon_Menu_2.S.gameObject.SetActive(false);
		}
		else if(Input.GetKeyDown(KeyCode.S) && !Pokemon_Menu_paused && !Main.S.choiceMade){
			if(!pokemon_menu_2_active){
				Menu.S.menuPaused = false;
				Menu.S.pokemon_menu_active = false;
				Menu.S.gameObject.SetActive(true);
				Dialog.S.HideDialogBox();
				gameObject.SetActive(false);
			}
		}
		
	}
	private void MoveDownMenu(){
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		while(true){
			activeItem = activeItem == Poke_lists.Count - 1 ? 0: ++activeItem;
			if(Poke_lists[activeItem].GetComponent<GUIText>().text != ""){
				Poke_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
				break;	
			}
		}
	}
	private void MoveUpMenu(){
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		while(true){
			activeItem = activeItem == 0 ? Poke_lists.Count - 1: --activeItem;
			if(Poke_lists[activeItem].GetComponent<GUIText>().text != ""){
				Poke_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
				break;	
			}
		}
	}
	private void setPlayerItems(){
		Poke_lists = Perm_Items;
		int i = 0;
		foreach(PokemonObject entry in Player.S.pokemon_list){
			if(entry.pkmnName != "None")
				Poke_lists[i].GetComponent<GUIText>().text = entry.pkmnName + " " + entry.curHp + "/" + entry.totHp + " LVL: " + entry.level;
			else
				Poke_lists[i].GetComponent<GUIText>().text = "";
			++i;
		}
	}
}