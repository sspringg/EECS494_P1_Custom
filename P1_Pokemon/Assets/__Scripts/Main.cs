using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {


	static public Main S;
	
	public bool inDialog = false;
	public bool printDialog = false;
	public bool paused = false;
	
	void Awake(){
		S = this;
	}
	
	// Update is called once per frame
	void Update () {
		//not already talking and try to get menu and not already paused
		if(!inDialog && Input.GetKeyDown(KeyCode.Return) && !paused){
			Menu.S.gameObject.SetActive(true);
			paused = true;
		}
		//exit menu when s is pushed
		else if(paused && Menu.S.gameObject.activeSelf && Input.GetKeyDown(KeyCode.S) && !Items_Menu.S.gameObject.activeSelf 
				&& !Pokemon_Menu.S.gameObject.activeSelf){
			Menu.S.gameObject.SetActive(false);
			paused = false;
		}
		else if(inDialog && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) && Player.S.ChoosingPokemon){
			Pokemon_Choose.S.gameObject.SetActive(true);
			Dialog.S.HideDialogBox();
			paused = true;
		}
		else if(!inDialog && Input.GetKeyDown(KeyCode.S) && Player.S.ChoosingPokemon){
			Pokemon_Choose.S.gameObject.SetActive(false);
			Player.S.speakDictionary["Pokemon_Choose_Table"] = -1;
			Player.S.playerSpeaking = null;
			Player.S.ChoosingPokemon = false;
			paused = false;
		}
		else if(inDialog && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) && Player.S.Healing_Pokemon){
			Heal_Pokemon.S.gameObject.SetActive(true);
			Dialog.S.HideDialogBox();
			paused = true;
		}
		else if(!inDialog && Input.GetKeyDown(KeyCode.S) && Player.S.Healing_Pokemon){
			Heal_Pokemon.S.gameObject.SetActive(false);
			Player.S.speakDictionary["Forward_Clerk"] = -1;
			Player.S.playerSpeaking = null;
			Player.S.Healing_Pokemon = false;
			paused = false;
		}
		else if(inDialog && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) && Player.S.Mart_Options){
			Mart_Options.S.gameObject.SetActive(true);
			Dialog.S.HideDialogBox();
			paused = true;
		}
		else if(!inDialog && Input.GetKeyDown(KeyCode.S) && Player.S.Mart_Options){
			Mart_Options.S.gameObject.SetActive(false);
			Player.S.speakDictionary["Checkout_Front"] = 4;
			Player.S.playerSpeaking = null;
			Player.S.Mart_Options = false;
			paused = false;
		}
	}
}
