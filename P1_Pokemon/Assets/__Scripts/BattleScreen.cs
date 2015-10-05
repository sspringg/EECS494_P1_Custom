using UnityEngine;
using System.Collections;

public class BattleScreen : MonoBehaviour {

	public static BattleScreen S;
	public static PokemonObject playerPokemon;
	public static PokemonObject opponentPokemon;

	void Awake(){
		S = this;
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 6; ++i) {
			if (Player.S.pokemon_list[i].curHp > 0){
				updatePokemon (true, Player.S.pokemon_list[i]);
				break;
			}
		}
		switch (Player.S.enemyNo) {
		case 1:
			updatePokemon (false, Player.S.BC_pkmn);
			break;
		case 2:
			updatePokemon (false, Player.S.Lass_pkmn);
			break;
		case 3:
			updatePokemon (false, Player.S.YS_pkmn);
			break;
		case 4:
			updatePokemon (false, PokemonObject.getPokemon ("Caterpie"));
			break;
		case 5:
			updatePokemon (false, PokemonObject.getPokemon ("Pidgey"));
			break;
		default:
			updatePokemon (false, PokemonObject.getPokemon ("Caterpie"));
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		GUIText myText;
		myText = GameObject.Find ("HPVal1").GetComponent<GUIText> ();
		myText.text = playerPokemon.curHp.ToString () + '/' + playerPokemon.totHp.ToString();
		myText = GameObject.Find ("HPVal2").GetComponent<GUIText>();
		myText.text = opponentPokemon.curHp.ToString () + '/' + opponentPokemon.totHp.ToString();
	}

	public static void updatePokemon (bool isPlayer, PokemonObject curPkmn){
		GUIText myText;
		if (isPlayer) {
			BattleScreen.playerPokemon = curPkmn;
			curPkmn.fought = true;
			myText = GameObject.Find ("NameVal1").GetComponent<GUIText> ();
			myText.text = curPkmn.pkmnName;
			
			myText = GameObject.Find ("LevelVal1").GetComponent<GUIText> ();
			myText.text = curPkmn.level.ToString ();
			
			myText = GameObject.Find ("HPVal1").GetComponent<GUIText> ();
			myText.text = curPkmn.curHp.ToString () + '/' + curPkmn.totHp.ToString();
		} else {
			BattleScreen.opponentPokemon = curPkmn;
			myText = GameObject.Find ("NameVal2").GetComponent<GUIText>();
			myText.text = curPkmn.pkmnName;
			
			myText = GameObject.Find ("LevelVal2").GetComponent<GUIText>();
			myText.text = curPkmn.level.ToString();
			
			myText = GameObject.Find ("HPVal2").GetComponent<GUIText>();
			myText.text = curPkmn.curHp.ToString () + '/' + curPkmn.totHp.ToString();
		}
	}

	public static void DestroyHelper(){
		Player.S.inScene0 = true;
		Destroy (GameObject.Find ("BattleScene"));
		Player.S.enemyNo = 0;
	}
}
