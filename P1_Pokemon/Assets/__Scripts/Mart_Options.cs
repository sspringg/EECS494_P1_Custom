using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum Item_list{
	Pokeball,
	Antidote,
	Palyz_Heal,
	Burn_Heal,
	Cancel
}

public class Mart_Options : MonoBehaviour {
	public int activeItem;
	public static Mart_Options S;
	public List<GameObject> Item_lists;
	
	void Awake(){
		S = this;
	}
	void Start () {
		bool first = true;
		activeItem = 0;
		foreach(Transform child in transform){
			Item_lists.Add (child.gameObject);
		}
		foreach(GameObject go in Item_lists){
			GUIText itemText = go.GetComponent<GUIText>();
			if(first) itemText.color = Color.red;
			first = false; 
		}
		
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Main.S.paused){
			if(Input.GetKeyDown(KeyCode.A)){
				switch(activeItem - 1){ 
					case(int)Item_list.Pokeball:
						if(Player.S.money >= 200){
							Player.S.money -= 200;
							addPlayerItem("POKeBALL");
							Player.S.speakDictionary["Checkout_Front"] = 6;
						}
						else
							Player.S.speakDictionary["Checkout_Front"] = 9;
						Player.S.Mart_Options = false;
						break;
					case(int)Item_list.Antidote:
						if(Player.S.money >= 100){
							Player.S.money -= 100;
							addPlayerItem("ANTIDOTE");
							Player.S.speakDictionary["Checkout_Front"] = 7;
						}
						else
							Player.S.speakDictionary["Checkout_Front"] = 9;
						Player.S.Mart_Options = false;
						break;
					case(int)Item_list.Palyz_Heal:
						if(Player.S.money >= 200){
							Player.S.money -= 200;
							addPlayerItem("PALYZ_HEAL");
							Player.S.speakDictionary["Checkout_Front"] = 8;
						}
						else
							Player.S.speakDictionary["Checkout_Front"] = 9;
						Player.S.Mart_Options = false;
						break;
					case (int)Item_list.Burn_Heal:
						if(Player.S.money >= 250){
							Player.S.money -= 250;
							addPlayerItem("BURN_HEAL");
							Player.S.speakDictionary["Checkout_Front"] = 8;
						}
						else
							Player.S.speakDictionary["Checkout_Front"] = 8;
						Player.S.Mart_Options = false;
						break;
					case -1:
						Player.S.speakDictionary["Checkout_Front"] = 10;
						Player.S.Mart_Options = false;
						break;
				}
				
				gameObject.SetActive(false);
				Main.S.paused = false;
				Player.S.CheckForAction();
			}
			
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				MoveDownMenu();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow)){
				MoveUpMenu();
			}
		}
	}
	public void MoveDownMenu(){
		Item_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == Item_lists.Count - 1 ? 0: ++activeItem;
		Item_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		Item_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? Item_lists.Count - 1: --activeItem;
		Item_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void addPlayerItem(string itemType){
		if (!Player.S.itemsDictionary.ContainsKey(itemType)) {
			Player.S.itemsDictionary.Add(itemType,0);
		}
		++Player.S.itemsDictionary[itemType]; 
	}
}
