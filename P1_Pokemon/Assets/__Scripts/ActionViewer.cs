using UnityEngine;
using System.Collections;

public class ActionViewer : MonoBehaviour {

	public static ActionViewer S;

	void Awake() {
		S = this;
	}
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			switch(Opponent.S.cur_action){
			case 1:
				if (!Opponent.S.opponent_moving){
					Opponent.S.moveOpponentForward();
					Opponent.S.continue_moving = true;
				}
				break;
			case 2:
				Player.S.inScene0 = false;
				Application.LoadLevelAdditive("_Scene_2");
				Player.S.enemyNo = 4;
				break;
			case 3:
				if(Player.S.transform.position.y < 75){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(69, 75, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 80){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(65, 80, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 84){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(71, 84, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 88){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(67, 88, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 88){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(67, 88, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 106){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(69, 106, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				else if(Player.S.transform.position.y < 112){
					Opponent.S.opponent_pokeball.Add(new Pokeball_Info(68, 112, Opponent.S.opponent_pokemon_list[0]));
					Opponent.S.opponent_pokemon_list[0] = PokemonObject.getPokemon ("None");
				}
				break;
			case 4:
				//place obstacle
				break;
			}
			if (Opponent.S.cur_action > 1 && Player.S.inScene0){
				gameObject.SetActive(false);
				Opponent.S.action_selected = false;
				Turn_Choice_Menu.S.gameObject.SetActive(true);
				Main.S.playerTurn = true;
			}
			else if (Player.S.inScene0){
				gameObject.SetActive(false);
				Opponent.S.action_selected = false;	
			}
			else {
				gameObject.SetActive(false);
			}
		}
	}

	public static void printMessage(string inMsg){
		GUIText myText;
		
		myText = GameObject.Find ("ActionText").GetComponent<GUIText> ();
		myText.text = inMsg;
	}
}
