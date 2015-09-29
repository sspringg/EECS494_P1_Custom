using UnityEngine;
using System.Collections;

public class HPSwitchTxt : MonoBehaviour {

	public static HPSwitchTxt S;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void UpdateHPSwitchTxt(){
		GUIText mytext; //BattleScreen.playerPokemon.curHp.ToString() + '/' + BattleScreen.playerPokemon.totHp.ToString();

		mytext = GameObject.Find ("HitPVal1").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[0].curHp.ToString() + '/' + Player.S.pokemon_list[0].totHp.ToString();
		mytext = GameObject.Find ("HitPVal2").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[1].curHp.ToString() + '/' + Player.S.pokemon_list[1].totHp.ToString();
		mytext = GameObject.Find ("HitPVal3").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[2].curHp.ToString() + '/' + Player.S.pokemon_list[2].totHp.ToString();
		mytext = GameObject.Find ("HitPVal4").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[3].curHp.ToString() + '/' + Player.S.pokemon_list[3].totHp.ToString();
		mytext = GameObject.Find ("HitPVal5").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[4].curHp.ToString() + '/' + Player.S.pokemon_list[4].totHp.ToString();
		mytext = GameObject.Find ("HitPVal6").GetComponent<GUIText> ();
		mytext.text = Player.S.pokemon_list[5].curHp.ToString() + '/' + Player.S.pokemon_list[5].totHp.ToString();
	}
}
