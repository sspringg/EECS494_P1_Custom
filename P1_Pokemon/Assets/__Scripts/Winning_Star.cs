using UnityEngine;
using System.Collections;

public class Winning_Star : MonoBehaviour {
	// Use this for initialization
	public static Winning_Star S;
	
	void Awake(){
		S = this;
	}
	void OnTriggerEnter(Collider coll){
//			star_reached = false;
			Player.S.CheckForAction();
	}
}
