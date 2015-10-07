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
	public List<PokemonObject> pokemon_list = new List<PokemonObject>();
	public PokemonObject BC_pkmn;
	public PokemonObject Lass_pkmn;
	public PokemonObject YS_pkmn;
	public PokemonObject wildPkmn1;
	public PokemonObject wildPkmn2;
	public int enemyNo;
	public float 	moveSpeed;
	public int		tileSize;
	public int spacesMoved;
	public int moveLim;
	
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
	public bool		OpeningDialog = false;
	public bool		inScene0 = true;
	public bool 	allowedToMove = true;
	
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
	public Vector3 cur_checkpoint;
	
	public Dictionary<string, int> speakDictionary = new Dictionary<string, int>();
	public Dictionary<string, int> itemsDictionary = new Dictionary<string,int>();
	void Awake(){
		S = this;
	}
	
	void Start(){
		sprend = gameObject.GetComponent<SpriteRenderer>();
		spacesMoved = 0;
		moveLim = 15;
		PokemonObject.start ();
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
		pokemon_list.Add(PokemonObject.getPokemon ("None"));
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
		itemsDictionary ["POKeBALL"] = 2;
		itemsDictionary["POTION"] = 2;

		cur_checkpoint.x = 68;
		cur_checkpoint.y = 69;
		cur_checkpoint.z = -0.01f;
	}
	
	new public Rigidbody rigidbody{
		get {return gameObject.GetComponent<Rigidbody>();}
	}
	public Vector3 pos{
		get {return transform.position;}
		set{transform.position = value;}
	}
	void FixedUpdate(){
		if(!moving && !Main.S.inDialog && inScene0 && allowedToMove){
			if(Input.GetKeyDown(KeyCode.A)){ //min 40
				CheckForAction();
			}
			//ARROW KEYS		
			else if(Input.GetKey(KeyCode.RightArrow)){
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
			if ((S.pos.x == 68 || S.pos.x == 69) && S.pos.y >= 114){
				print ("You Win!");
				Opponent.S.gameObject.SetActive(false);
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
			}
			targetPos = pos + moveVec;
		}
		else{
			if((targetPos - pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				pos = targetPos; //around min 17
				moving = false;
				++spacesMoved;
			}
			else{
				pos += (targetPos - pos).normalized * moveSpeed * Time.fixedDeltaTime;
			}
		}
	}
	
	public void CheckForAction(){
		if(Physics.Raycast(GetRay(), out hitInfo, 2f, GetLayerMask(new string[] {"NPC", "Bug_Catcher", "Lass", "Youngster"}))){
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

