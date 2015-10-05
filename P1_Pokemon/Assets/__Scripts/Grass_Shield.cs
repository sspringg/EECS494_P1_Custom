using UnityEngine;
using System.Collections;

public class Grass_Shield : MonoBehaviour {
	// Use this for initialization
	public static Grass_Shield S;
	
	void Awake(){
		S = this;
	}
	void OnTriggerEnter(Collider coll){
		if(Player.S.ChosenPokemon && Player.S.OpeningDialog)
			gameObject.SetActive(false);
		else if(!Player.S.ChosenPokemon){
			Dialog.S.gameObject.SetActive(true);
			Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
			noAlpha.a = 255;
			GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
			Dialog.S.ShowMessage( "See Professor Oak in his lab before you enter the long grass. Dangerous Pokemon live in there");
		}
		else{
			Player.S.CheckForAction();}
	}
}
