using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ItemMenu_list{
	POKeBALL,
	ANTIDOTE,
	PALYZ_HEAL,
	BURN_HEAL,
	CANCEL
}

public class Items_Menu : MonoBehaviour {
	public int activeItem;
	public static Items_Menu S;
	public List<GameObject> ItemMenu_lists;
	public List<GameObject>	Perm_Items; 
	public bool Items_Menu_2_active = false, items_menu_paused = false;
	string key, value;
	public string itemChosen;
	void Awake(){
		S = this;
	}
	void Start () {
		activeItem = 4;
		foreach(Transform child in transform){
			ItemMenu_lists.Add(child.gameObject);
		}
		foreach(GameObject go in ItemMenu_lists){
			GUIText itemText = go.GetComponent<GUIText>();
			if(itemText.text == "CANCEL") itemText.color = Color.red; 
		}
		Perm_Items = ItemMenu_lists;
		activeItem = ItemMenu_lists.Count - 1;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Menu.S.menuPaused && !Items_Menu_2_active){
			setPlayerItems();
			if(Input.GetKeyDown(KeyCode.A)){
				if(activeItem == 5){
					gameObject.SetActive(false);
					Menu.S.menuPaused = false;
					Menu.S.items_menu_active = false;
				}
				else{
					itemChosen = ItemMenu_lists[activeItem].GetComponent<GUIText>().text.Substring(0, ItemMenu_lists[activeItem].GetComponent<GUIText>().text.Length - 4);
					Items_Menu_2.S.gameObject.SetActive(true);
					Items_Menu_2_active = true;
					items_menu_paused = true;
				}
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) && !items_menu_paused){
				MoveDownMenu();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) && !items_menu_paused){
				MoveUpMenu();
			}
		}
	}
	private void MoveDownMenu(){
		ItemMenu_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		while(true){
			activeItem = activeItem == ItemMenu_lists.Count - 1 ? 0: ++activeItem;
			if(ItemMenu_lists[activeItem].GetComponent<GUIText>().text != ""){
				ItemMenu_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
				break;	
			}
		}
	}
	private void MoveUpMenu(){
		ItemMenu_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		while(true){
			activeItem = activeItem == 0 ? ItemMenu_lists.Count - 1: --activeItem;
			if(ItemMenu_lists[activeItem].GetComponent<GUIText>().text != ""){
				ItemMenu_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
				break;	
			}
		}
	}
	private void setPlayerItems(){
		int i = 0;
		foreach(KeyValuePair<string, int> entry in Player.S.itemsDictionary){
				ItemMenu_lists[i].GetComponent<GUIText>().text = entry.Key + " X " + entry.Value;
				++i;
		}
		for(int j = i; j < ItemMenu_lists.Count - 1; ++j){
			ItemMenu_lists[j].GetComponent<GUIText>().text = "";
		}
	}
}