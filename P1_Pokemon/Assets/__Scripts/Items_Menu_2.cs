using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Items_Menu_2 : MonoBehaviour {
	public static Items_Menu_2 S;
	public int activeItem;
	string key, value;
	public bool usingItem = false;
	public List<GameObject> UseToss_lists;
	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach(Transform child in transform){
			UseToss_lists.Add (child.gameObject);
		}
		
		foreach(GameObject go in UseToss_lists){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false; 
		}
		
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Items_Menu.S.items_menu_paused){
			if(Input.GetKeyDown(KeyCode.A)){
				switch(activeItem){ // at 1:14:00
					case 0:
						Menu.S.pokemon_menu_active = true;
						Pokemon_Menu.S.gameObject.SetActive(true);
						Dialog.S.gameObject.SetActive(true);
						Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
						noAlpha.a = 255;
						GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
						Dialog.S.ShowMessage("Use on which POKeMON?");
						Items_Menu.S.Items_Menu_2_active = false;
						Items_Menu.S.items_menu_paused = false;
						Items_Menu.S.gameObject.SetActive(false);
						gameObject.SetActive(false);
						usingItem = true;
						break;
					case 1:
						if(Items_Menu.S.itemChosen != "Prof_Oak_Package"){
							Player.S.itemsDictionary[Items_Menu.S.itemChosen]--;
							if(Player.S.itemsDictionary[Items_Menu.S.itemChosen] == 0){
								Player.S.itemsDictionary.Remove(Items_Menu.S.itemChosen);	//remove item if we have 0 of them
							}
							Items_Menu.S.items_menu_paused = false;
							Items_Menu.S.Items_Menu_2_active = false;
							Items_Menu.S.ItemMenu_lists[Items_Menu.S.ItemMenu_lists.Count - 1].GetComponent<GUIText>().color = Color.red; 
							gameObject.SetActive(false);
						}
					
						break;	
					}
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			MoveDownMenu();
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			MoveUpMenu();
		}
	}
	public void MoveDownMenu(){
		UseToss_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == UseToss_lists.Count - 1 ? 0: ++activeItem;
		UseToss_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		UseToss_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? UseToss_lists.Count - 1: --activeItem;
		UseToss_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
}
