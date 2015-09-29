using UnityEngine;
using System.Collections;

public class Grass_Shield : MonoBehaviour {
	// Use this for initialization
	void OnTriggerEnter(Collider coll){
		if(Player.S.ChosenPokemon)
			gameObject.SetActive(false);
		else{
			Dialog.S.gameObject.SetActive(true);
			Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
			noAlpha.a = 255;
			GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
			Dialog.S.ShowMessage( "See Professor Oak in his lab before you enter the long grass. Dangerous Pokemon live in there");
		}
	}
}
