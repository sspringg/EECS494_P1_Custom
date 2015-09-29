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
	public bool pokemon_menu_2_active = false, Pokemon_Menu_paused = false, moving_pokemon = false;
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
		if(Player.S.pokemon_list.Length == 0){
			gameObject.SetActive(false);
			Menu.S.menuPaused = false;
			Menu.S.pokemon_menu_active = false;	
		}
		if (Menu.S.menuPaused && !pokemon_menu_2_active){
			setPlayerItems();
			if(Input.GetKeyDown(KeyCode.A) && !moving_pokemon && !Items_Menu_2.S.usingItem){
				pokemon_menu_chosen = activeItem;
				Pokemon_Menu_2.S.gameObject.SetActive(true);
				pokemon_menu_2_active = true;
				Pokemon_Menu_paused = true;
			}
			else if(Input.GetKeyDown(KeyCode.A) && moving_pokemon){
				PokemonObject temp = Player.S.pokemon_list[activeItem];
				Player.S.pokemon_list[activeItem] = Player.S.pokemon_list[activeItem];
				Player.S.pokemon_list[activeItem] = temp;
				moving_pokemon = false;
			}
			else if(Input.GetKeyDown(KeyCode.A) && Items_Menu_2.S.usingItem){
				Dialog.S.gameObject.SetActive(true);
				Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
				noAlpha.a = 255;
				GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
				if(Items_Menu.S.itemChosen == "POKeBALL" || Items_Menu.S.itemChosen == "Prof_Oak_Package"){
							print("pokeball");
					Dialog.S.ShowMessage("This isn't the time to use that");
				}
				else if(Items_Menu.S.itemChosen == "ANTIDOTE" && Player.S.pokemon_list[activeItem].pkmnName == "Bulbasaur"){ //change to poison
					print("cured");
					Player.S.itemsDictionary[Items_Menu.S.itemChosen]--;
					if(Player.S.itemsDictionary[Items_Menu.S.itemChosen] == 0)
						Player.S.itemsDictionary.Remove(Items_Menu.S.itemChosen);	//remove item if we have 0 of them
					Dialog.S.ShowMessage(Player.S.pokemon_list[activeItem].pkmnName + " is cured");
				}
				else{
					Dialog.S.ShowMessage("This will have no effect");
					print("none");	
				}
				Items_Menu_2.S.usingItem = false;
				Menu.S.items_menu_active = false;
				Menu.S.menuPaused = false;
				Menu.S.pokemon_menu_active = false;
				Menu.S.gameObject.SetActive(true);
				gameObject.SetActive(false);
				
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow) && !Pokemon_Menu_paused){
				MoveDownMenu();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) && !Pokemon_Menu_paused){
				MoveUpMenu();
			}
		}
		if(Input.GetKeyDown(KeyCode.S) && pokemon_menu_2_active){
			Pokemon_Menu_paused = false;
			pokemon_menu_2_active = false;
			Pokemon_Menu_2.S.gameObject.SetActive(false);
		}
		else if(Input.GetKeyDown(KeyCode.S) && !Pokemon_Menu_paused){
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