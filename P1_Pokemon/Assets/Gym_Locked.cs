using UnityEngine;
using System.Collections;

public class Gym_Locked : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if(Player.S.ChosenPokemon)
			gameObject.SetActive(false);
	}
	void OnTriggerEnter(Collider coll){
		Dialog.S.gameObject.SetActive(true);
		Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
		noAlpha.a = 255;
		GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
		Dialog.S.ShowMessage("Gym_Locked");
	}
}
