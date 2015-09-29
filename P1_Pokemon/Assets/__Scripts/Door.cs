using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Vector3 doorPos;
	
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag == "Player"){
			Player.S.MoveThroughDoor(doorPos);
		}
	}
}
