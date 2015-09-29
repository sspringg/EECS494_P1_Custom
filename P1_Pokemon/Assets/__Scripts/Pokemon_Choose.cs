using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum Poke_list{
	Squirttle,
	Bulbasaur,
	Charmander
}

public class Pokemon_Choose : MonoBehaviour {
	public static Pokemon_Choose S;
	
	public int activeItem;
	public List<GameObject> Poke_lists;
	
	void Awake(){
		S = this;
	}
	// Use this for initialization
	void Start () {
		bool first = true;
		activeItem = 0;
		
		foreach(Transform child in transform){
			Poke_lists.Add (child.gameObject);
		}
		Poke_lists = Poke_lists.OrderByDescending(m => m.transform.position.y).ToList();
		
		foreach(GameObject go in Poke_lists){
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
				print(activeItem);
				switch(activeItem){ // at 1:14:00
				case 0:
					print("char");
					Player.S.pokemon_list[0] = PokemonObject.getPokemon("Charmander");
					break;
				case 1:
					Player.S.pokemon_list[0] = PokemonObject.getPokemon("Bulbasaur");
					break;
				case 2:
					Player.S.pokemon_list[0] = PokemonObject.getPokemon("Squirtle");
					print("squir");
					break;	
				}
				Player.S.pokemon_list[1] = PokemonObject.getPokemon("Pikachu");
				gameObject.SetActive(false);
				Main.S.paused = false;
				Dialog.S.HideDialogBox();
				Player.S.ChosenPokemon = true;
				Player.S.ChoosingPokemon = false;
				Color noAlpha = GameObject.Find("Oak_Lab/Pokeball").GetComponent<SpriteRenderer>().color;
				noAlpha.a = 0;
				GameObject.Find("Oak_Lab/Pokeball").GetComponent<SpriteRenderer>().color = noAlpha;
				gameObject.SetActive(false);
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			MoveDownMenu();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			MoveUpMenu();
		}
	}
	public void MoveDownMenu(){
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == Poke_lists.Count - 1 ? 0: ++activeItem;
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
	public void MoveUpMenu(){
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.black;
		activeItem = activeItem == 0 ? Poke_lists.Count - 1: --activeItem;
		Poke_lists[activeItem].GetComponent<GUIText>().color = Color.red;	
	}
}