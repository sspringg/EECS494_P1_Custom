using UnityEngine;
using System.Collections;

public class POKeBALL : MonoBehaviour {
	public static POKeBALL S;
	public Vector3	moveVec, targetPos;
	public int PokeballSpeed;
	public bool Pokeball_Moving = false;
	// Use this for initialization
	void Awake(){
		S = this;
	}
	
	void Start () {
		gameObject.SetActive(false);
	}
	public Vector3 pos{
		get {return transform.position;}
		set{transform.position = value;}
	}
	// Update is called once per frame
	void Update () {
		if(!Pokeball_Moving){
			if(Input.GetKeyDown(KeyCode.A)){
				Main.S.player_pokeball.Add(new Pokeball_Info((int)transform.position.x, (int)transform.position.y,
				                                             Player.S.pokemon_list[Pokemon_Menu.S.pokemon_menu_chosen]));
				Player.S.pokemon_list.RemoveAt(Pokemon_Menu.S.pokemon_menu_chosen);
				Main.S.paused = false;
				Main.S.playerTurn = false;
				Main.S.choiceMade = -1;
				gameObject.SetActive(false);
			}
			else if(Input.GetKey(KeyCode.RightArrow)){
				moveVec = Vector3.right;
				Pokeball_Moving = true;
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				moveVec = Vector3.left;
				Pokeball_Moving = true;
			}
			else if(Input.GetKey(KeyCode.UpArrow)){
				moveVec = Vector3.up;
				Pokeball_Moving = true;
			}
			else if(Input.GetKey(KeyCode.DownArrow)){
				moveVec = Vector3.down;
				Pokeball_Moving = true;
			}
			targetPos = pos + moveVec;
		}
		else{
			if((targetPos - pos).magnitude < PokeballSpeed * Time.fixedDeltaTime){
				pos = targetPos; //around min 17
				Pokeball_Moving = false;
			}
			else{
				pos += (targetPos - pos).normalized * PokeballSpeed * Time.fixedDeltaTime;
			}
		}
	}
}
