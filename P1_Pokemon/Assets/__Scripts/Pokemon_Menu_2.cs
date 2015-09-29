using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum Option_list{
	STATS,
	SWITCH,
	QUIT
}

public class Pokemon_Menu_2 : MonoBehaviour {
	public static Pokemon_Menu_2 S;
	
	public int activeItem;
	public List<GameObject> Options_lists;
	public bool Pokemon_Menu_Stats_active = false, Pokemon_Menu_2_paused = false;
	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach(Transform child in transform){
			Options_lists.Add (child.gameObject);
		}
		
		foreach(GameObject go in Options_lists){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false; 
		}
		
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Pokemon_Menu.S.Pokemon_Menu_paused && !Pokemon_Menu_Stats_active){
			if(Input.GetKeyDown(KeyCode.A)){
				switch(activeItem){ // at 1:14:00
				case 0:
					Pokemon_Stats_Menu.S.gameObject.SetActive(true);
					Pokemon_Menu_Stats_active = true;
					Pokemon_Menu_2_paused = true;
					Dialog.S.HideDialogBox();
					break;
				case 1:
					Pokemon_Menu.S.Pokemon_Menu_paused = false;
					Pokemon_Menu.S.pokemon_menu_2_active = false;
					Pokemon_Menu.S.moving_pokemon = true;
					Dialog.S.ShowMessage("Move POKeMON where");
					gameObject.SetActive(false);
					break;
				case 2:
					print("cancel");
					gameObject.SetActive(false);
					Pokemon_Menu.S.Pokemon_Menu_paused = false;
					gameObject.SetActive(false);
					break;	
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) && !Pokemon_Menu_2_paused){
			MoveDownMenu();
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) && !Pokemon_Menu_2_paused){
			MoveUpMenu();
		}
	}
	public void MoveDownMenu(){
		Options_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == Options_lists.Count - 1 ? 0: ++activeItem;
		Options_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		Options_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? Options_lists.Count - 1: --activeItem;
		Options_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
}