using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Pokemon_Stats_Menu : MonoBehaviour {
	public static Pokemon_Stats_Menu S;
	public List<GameObject> Poke_Stats_lists;
	public List<GameObject> Poke_Stats_perm;
	void Awake(){
		S = this;
	}
	
	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			Poke_Stats_lists.Add(child.gameObject);
		}
	
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Pokemon_Menu_2.S.Pokemon_Menu_2_paused){
			setPlayerItems();
			if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A)) && Pokemon_Menu_2.S.Pokemon_Menu_Stats_active){
				print("set false");
				gameObject.SetActive(false);
				Pokemon_Menu_2.S.Pokemon_Menu_Stats_active = false;
				Pokemon_Menu_2.S.Pokemon_Menu_2_paused = false;
				Pokemon_Menu.S.pokemon_menu_2_active = false;
				Dialog.S.gameObject.SetActive(true);
				Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
				noAlpha.a = 255;
				GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
				Dialog.S.ShowMessage("Choose a POKeMON");
			}
		}
	}
	private void setPlayerItems(){
		PokemonObject cur_poke = Player.S.pokemon_list[Pokemon_Menu.S.pokemon_menu_chosen];
		Poke_Stats_lists[1].GetComponent<GUIText>().text = "ATTACK " + cur_poke.atk.ToString();
		Poke_Stats_lists[2].GetComponent<GUIText>().text = "DEFENSE " + cur_poke.def.ToString();
		Poke_Stats_lists[3].GetComponent<GUIText>().text = "SPEED " + cur_poke.speed.ToString();
		Poke_Stats_lists[4].GetComponent<GUIText>().text = "SPECIAL " + cur_poke.spAtk.ToString();
		Poke_Stats_lists[5].GetComponent<GUIText>().text = cur_poke.pkmnName;
		Poke_Stats_lists[6].GetComponent<GUIText>().text = "L" + cur_poke.level.ToString();
		Poke_Stats_lists[7].GetComponent<GUIText>().text = "HP " + cur_poke.curHp.ToString() + "/" + cur_poke.totHp.ToString();
		Poke_Stats_lists[8].GetComponent<GUIText>().text = "STATUS/" + cur_poke.stat;
		Poke_Stats_lists[9].GetComponent<GUIText>().text = "Type1/" + cur_poke.type1;
		Poke_Stats_lists [10].GetComponent<GUIText> ().text = "Type2/" + cur_poke.type2;

	}
}
