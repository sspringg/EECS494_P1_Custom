using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum menuItem{
	pokedex,
	pokemon,
	item,
	player,
	save,
	option,
	exit
}

public class Menu : MonoBehaviour {
	public static Menu S;
	public 	bool items_menu_active = false;
	public	bool pokemon_menu_active = false;
	public int activeItem;
	public bool	menuPaused = false;
	public List<GameObject> menuItems;
	
	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		foreach(Transform child in transform){
			menuItems.Add (child.gameObject);
		}
		menuItems = menuItems.OrderByDescending(m => m.transform.position.y).ToList();
		
		foreach(GameObject go in menuItems){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false; 
		}
		
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Main.S.paused && !items_menu_active && !pokemon_menu_active){
			if(Input.GetKeyDown(KeyCode.A)){
				switch(activeItem){ // at 1:14:00
					case(int)menuItem.pokedex:
						print("Pokedex menu selected");
						break;
					case(int)menuItem.pokemon:
						print("Pokemon menu");
						pokemon_menu_active = true;
						Pokemon_Menu.S.gameObject.SetActive(true);
						Dialog.S.gameObject.SetActive(true);
						Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
						noAlpha.a = 255;
						GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
						Dialog.S.ShowMessage("Choose a POKeMON");
						gameObject.SetActive(false);
						menuPaused = true;
						break;
					case(int)menuItem.item:
						items_menu_active = true;
						Items_Menu.S.gameObject.SetActive(true);
						menuPaused = true;
						break;
					case(int)menuItem.player:
						print("player menu selected");
						break;
					case(int)menuItem.save:
						print("save menu selected");
						break;
					case(int)menuItem.option:
						print("option menu selected");
						break;
					case(int)menuItem.exit:
						gameObject.SetActive(false);
						Main.S.paused = false;
						break;
				
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){
			print("s down");
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) && !menuPaused){
			MoveDownMenu();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) && !menuPaused){
			MoveUpMenu();
		}
		else if(Input.GetKeyDown(KeyCode.S) && items_menu_active){
			menuPaused = false;
			items_menu_active = false;
			Items_Menu.S.gameObject.SetActive(false);
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
