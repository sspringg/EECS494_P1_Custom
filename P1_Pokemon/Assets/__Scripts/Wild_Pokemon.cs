using UnityEngine;
using System.Collections;

public class Wild_Pokemon : MonoBehaviour {
	private	int horizDist = 0, vertDist = 0;
	public int	leftRightDist, upDownDist;
	private float randomVal;
	public float chanceToMove = 1, chanceToFight = 10;
	public UnityEngine.Random random = new UnityEngine.Random();
	void OnTriggerEnter(Collider coll){
		randomVal = UnityEngine.Random.Range(0, 100);
		if (randomVal < chanceToFight) {
			Player.S.inScene0 = false;
			Application.LoadLevelAdditive ("_Scene_2");
			Player.S.enemyNo = UnityEngine.Random.Range(4, 6);
		}
	}
	void FixedUpdate(){
			randomVal = UnityEngine.Random.Range(0, 100);
			if(randomVal < chanceToMove && (Main.S.inDialog != true) && Player.S.inScene0){
				randomVal = UnityEngine.Random.Range(0, 100);
				if(randomVal < 25 && (horizDist > (-leftRightDist)) && (gameObject.transform.position + Vector3.left != Player.S.transform.position)){ //try to move left
					gameObject.transform.position += Vector3.left;
					--horizDist;
				}
				else if(randomVal > 25 && randomVal < 50 && (horizDist < leftRightDist) && (gameObject.transform.position + Vector3.right != Player.S.transform.position)){ //try to move right
					gameObject.transform.position += Vector3.right;
					++horizDist;
				}
				else if(randomVal > 50 && randomVal < 75 && (vertDist > (-upDownDist)) && (gameObject.transform.position + Vector3.down != Player.S.transform.position)){ //try to move down
					gameObject.transform.position += Vector3.down;
					--vertDist;
				}
				else if(randomVal > 75 && (vertDist < (upDownDist)) && (gameObject.transform.position + Vector3.up != Player.S.transform.position)){ //try to move up
					gameObject.transform.position += Vector3.up;
					++vertDist;
				}
			}
		}
}
