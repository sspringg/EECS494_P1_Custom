using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction{
	down,
	left,
	up,
	right
}
public enum Pokemon{
	Squirttle,
	Bulbasaur,
	Charmander,
	Pikachu,
	none
}
public class Player : MonoBehaviour {

//initilizing variable
	public static Player S;
	public PokemonObject[] pokemon_list;
	public PokemonObject BC_pkmn;
	public PokemonObject Lass_pkmn;
	public PokemonObject YS_pkmn;
	public PokemonObject wildPkmn1;
	public PokemonObject wildPkmn2;
	public int enemyNo;
	public float 	moveSpeed;
	public int		tileSize;
	
	public SpriteRenderer	sprend;
	public	Sprite	upSprite;
	public	Sprite	downSprite;
	public	Sprite	leftSprite;
	public	Sprite	rightSprite;
//////////////////////////////	
//State variables
	public bool ChosenPokemon = false;
	public bool ChoosingPokemon = false;
	public bool		_______;
	public bool		fought_BC = false;
	public bool		fought_Lass = false;
	public bool		fought_YS = false;
	
	public bool		Healing_Pokemon = false;
	public bool		Mart_Options = false;
	public bool		pokedexEnable = false;
	public RaycastHit	hitInfo;
	public bool		moving = false;
	public string	playerSpeaking = null;
	public int	money = 500;
	
	public Vector3	targetPos;
	public Direction direction;
	public Vector3	moveVec;
	
	public Dictionary<string, int> speakDictionary = new Dictionary<string, int>();
	public Dictionary<string, int> itemsDictionary = new Dictionary<string,int>();
	void Awake(){
		S = this;
	}
	
	void Start(){
		sprend = gameObject.GetComponent<SpriteRenderer>();
		pokemon_list = new PokemonObject[6];
		pokemon_list [0] = PokemonObject.getPokemon ("None");
		pokemon_list [1] = PokemonObject.getPokemon ("None");
		pokemon_list [2] = PokemonObject.getPokemon ("None");
		pokemon_list [3] = PokemonObject.getPokemon ("None");
		pokemon_list [4] = PokemonObject.getPokemon ("None");
		pokemon_list [5] = PokemonObject.getPokemon ("None");
		BC_pkmn = PokemonObject.getPokemon ("Caterpie");
		Lass_pkmn = PokemonObject.getPokemon ("Squirtle");
		Lass_pkmn.level = 3;
		Lass_pkmn.totHp -= 10;
		Lass_pkmn.curHp -= 10;
		Lass_pkmn.atk -= 10;
		Lass_pkmn.def -= 10;
		YS_pkmn = PokemonObject.getPokemon ("Bulbasaur");
		YS_pkmn.level = 3;
		YS_pkmn.totHp -= 10;
		YS_pkmn.curHp -= 10;
		YS_pkmn.atk -= 10;
		YS_pkmn.def -= 10;
		wildPkmn1 = PokemonObject.getPokemon ("Caterpie");
		wildPkmn2 = PokemonObject.getPokemon ("Caterpie");
		pokemon_list[0] = PokemonObject.getPokemon ("Bulbasaur"); //for testing only
	}
	
	new public Rigidbody rigidbody{
		get {return gameObject.GetComponent<Rigidbody>();}
	}
	public Vector3 pos{
		get {return transform.position;}
		set{transform.position = value;}
	}
	void FixedUpdate(){
		if(!moving && !Main.S.inDialog && !Main.S.paused){
			if(Input.GetKeyDown(KeyCode.A)){ //min 40
				CheckForAction();
			}
			///ACTIONS IF PLAYER COMES INTO LINE OF SIGHT OF TRAINERS
			//these actions need to come before arrows become they need to happen even if trying to move 
			else if(Physics.Raycast(gameObject.transform.position, Vector3.left, out hitInfo, 6f, GetLayerMask(new string[] {"Bug_Catcher"})) && !fought_BC){
				fought_BC = true;
				NPC npc = hitInfo.collider.gameObject.GetComponent<NPC>();
				moving = false;
				npc.moveTowardPlayer = true;
				npc.Play_Dialog("Bug_Catcher");
				enemyNo = 1;
			}
			else if(Physics.Raycast(gameObject.transform.position, Vector3.right, out hitInfo, 10f, GetLayerMask(new string[] {"Lass"})) && !fought_Lass){
				fought_Lass = true;
				NPC npc = hitInfo.collider.gameObject.GetComponent<NPC>();
				moving = false;
				npc.moveTowardPlayer = true;
				npc.Play_Dialog("Lass");
				enemyNo = 2;
			}
			else if((Physics.Raycast(gameObject.transform.position, Vector3.left, out hitInfo, 8f, GetLayerMask(new string[] {"Youngster"})) || 
					Physics.Raycast(gameObject.transform.position, Vector3.down, out hitInfo, 5f, GetLayerMask(new string[] {"Youngster"})))&& !fought_YS){
				fought_YS = true;
				NPC npc = hitInfo.collider.gameObject.GetComponent<NPC>();
				moving = false;
				npc.moveTowardPlayer = true;
				npc.Play_Dialog("Youngster");
				enemyNo = 3;
			}
//ARROW KEYS		
			if(Input.GetKey(KeyCode.RightArrow)){
				moveVec = Vector3.right;
				direction = Direction.right;
				sprend.sprite = rightSprite;
				moving = true;
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				moveVec = Vector3.left;
				direction = Direction.left;
				sprend.sprite = leftSprite;
				moving = true;
			}
			else if(Input.GetKey(KeyCode.UpArrow)){
				moveVec = Vector3.up;
				direction = Direction.up;
				sprend.sprite = upSprite;
				moving = true;
			}
			else if(Input.GetKey(KeyCode.DownArrow)){
				moveVec = Vector3.down;
				direction = Direction.down;
				sprend.sprite = downSprite;
				moving = true;
			}
	
			else{
				moveVec = Vector3.zero;
				moving = false;
			}
//////////////////////////////////////
			//minute 25
			//ray cast sends out a ray in any direction for however long 
			//we want to see if there is an immovable object within 1 tile of dir we face
			//jump over ledges
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Ledge"})) && (direction == Direction.down && moving)){
				transform.position += Vector3.down;
			}
			else if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Lass", "Youngster", "Bug_Catcher", "Ledge"}))){
				moveVec = Vector3.zero;
				moving = false;
			};
			targetPos = pos + moveVec;
		}
		else{
			if((targetPos - pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				pos = targetPos; //around min 17
				moving = false;
			}
			else{
				pos += (targetPos - pos).normalized * moveSpeed * Time.fixedDeltaTime;
			}
		}
	}
	
	public void CheckForAction(){
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"NPC", "Bug_Catcher", "Lass", "Youngster"}))){
			NPC npc = hitInfo.collider.gameObject.GetComponent<NPC>();
			npc.FacePlayer(direction);
			playerSpeaking = npc.name;
			npc.Play_Dialog(npc.name);
		}
	}
	
	public Ray GetRay(){
		switch(direction){
			case Direction.down:
				return new Ray (pos, Vector3.down);
			case Direction.up:
				return new Ray (pos, Vector3.up);
			case Direction.left:
				return new Ray (pos, Vector3.left);
			case Direction.right:
				return new Ray (pos, Vector3.right);
			default:
				return new Ray();	
		}
	}
	//each layer has a number
	public int GetLayerMask(string[] layerNames){
		int layerMask = 0;
		foreach(string layer in layerNames){
			layerMask = layerMask | (1 << LayerMask.NameToLayer(layer)); //looks up name in layermask table
		}
		return layerMask;
	}
	public void MoveThroughDoor(Vector3 doorLoc){
		if(doorLoc.z <= 0) 
			doorLoc.z = transform.position.z; //make sure player doesnt get put into same z plane as scene and get lost
		moving = false;
		moveVec = Vector3.zero;
		transform.position = doorLoc;
	}
}

