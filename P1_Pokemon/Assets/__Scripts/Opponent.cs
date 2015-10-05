using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Opponent : MonoBehaviour {
	
	public static Opponent S;
	
	public	Sprite	upSprite;
	public	Sprite	downSprite;
	public	Sprite	leftSprite;
	public	Sprite	rightSprite;
	public bool moveTowardPlayer = false, opponent_moving = false;
	public SpriteRenderer	sprend;
	private int randomVal, spacesMoved = 0;
	public int	moveSpeed = 4;
	public Vector3	opponent_targetPos;
	public Vector3	opponent_moveVec;
	public Vector3 opponent_pos{
		get {return transform.position;}
		set{transform.position = value;}
	}
	public RaycastHit	hitInfo;
	
	public List<Pokeball_Info> opponent_pokeball = new List<Pokeball_Info>();
	public List<PokemonObject> opponent_pokemon_list = new List<PokemonObject>();
	public Dictionary<string, int> opponent_itemsDictionary = new Dictionary<string,int>();
	// Use this for initialization
	public UnityEngine.Random random = new UnityEngine.Random();
	void Awake(){
		S = this;
	}
	
	void Start () {	
		sprend = gameObject.GetComponent<SpriteRenderer>();
		sprend.sprite = upSprite;
		opponent_pokemon_list.Add(PokemonObject.getPokemon ("Squirtle"));
		opponent_pokemon_list.Add(PokemonObject.getPokemon ("Caterpie"));
		opponent_itemsDictionary.Add("POTION",1);
		opponent_itemsDictionary.Add("POKeBALL",2);
	}
	
	// Update is called once per frame
	void Update () {
		if(spacesMoved != 0 && !opponent_moving)
			moveOpponent();		//keep moving until moved all spaces
			/*put game logic here. Right now I just have a basic switch statement that is always 1 to test the control of the game
			but change the structure however you like. Also it would be nice to display the opponents choice before he does anything"*/
		else if(!Main.S.playerTurn && !opponent_moving){
			print("! player turn");
			randomVal = UnityEngine.Random.Range(1, 4);
			randomVal = 1; //for testing
			switch(randomVal){
			case 1:		//oppponent	move
				print("opponent move");
				randomVal = UnityEngine.Random.Range(1, 10);
				moveOpponent();
				print("raval: " + randomVal);
				spacesMoved = randomVal;
				break;
			case 2:		//place pokemon
				print("place pokemon");
				
				break;
			case 3:		//use item
				print("use item");
				break;
			case 4:		//Battle
				print("battle");
				break;
			}	
		}
		else if(opponent_moving){
			//opponent is moving
			if((opponent_targetPos - opponent_pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				opponent_pos = opponent_targetPos; //around min 17
				opponent_moving = false;
				--spacesMoved;
				print("spaceMoved: " + spacesMoved);
				if(spacesMoved == 0){
					Main.S.playerTurn = true;
				}
			}
			else{
				opponent_pos += (opponent_targetPos - opponent_pos).normalized * moveSpeed * Time.fixedDeltaTime;
			}
			
		}
		else if(moveTowardPlayer){		//player called opponent into battle so move toward them
			moveOpponent();
		}
	}
	public void moveOpponent(){
		if((gameObject.transform.position.x - Player.S.transform.position.x) > 1 
		   && !Physics.Raycast(Player.S.GetRay(), out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.left;
			sprend.sprite = leftSprite;
			opponent_moving = true;
		}
		else if((Player.S.transform.position.x - gameObject.transform.position.x) > 1 
		        && !Physics.Raycast(Player.S.GetRay(), out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.right;
			sprend.sprite = rightSprite;
			opponent_moving = true;
		}
		else if((Player.S.transform.position.y - gameObject.transform.position.y) > 1 
		        && !Physics.Raycast(Player.S.GetRay(), out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.up;
			sprend.sprite = upSprite;
			opponent_moving = true;
		}
		else if((gameObject.transform.position.y - Player.S.transform.position.y) > 1 
		        && !Physics.Raycast(Player.S.GetRay(), out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.down;
			sprend.sprite = downSprite;
			opponent_moving = true;
		}
		else{
			if(moveTowardPlayer){
				//if character ends up kiddy korner move them up or down one square
				if(Math.Abs(gameObject.transform.position.y - Player.S.transform.position.y) + Math.Abs(gameObject.transform.position.x - Player.S.transform.position.x) > 1.9){
					if(Player.S.transform.position.y > gameObject.transform.position.y)
						gameObject.transform.position += Vector3.up;
					else
						gameObject.transform.position += Vector3.down;
				}	
				moveTowardPlayer = false;
				if(gameObject.transform.position.x > Player.S.transform.position.x)
					Player.S.sprend.sprite = Player.S.rightSprite;
				else if(Player.S.transform.position.x > gameObject.transform.position.x)
					Player.S.sprend.sprite = Player.S.leftSprite;
				else if(transform.position.y > Player.S.transform.position.y)
					Player.S.sprend.sprite = Player.S.upSprite;
				else if(Player.S.transform.position.y > gameObject.transform.position.y)
					Player.S.sprend.sprite = Player.S.downSprite;
				moveTowardPlayer = false;
				Player.S.inScene0 = false;
				Application.LoadLevelAdditive("_Scene_2");
			}
		}
		opponent_targetPos = opponent_pos + opponent_moveVec;
	}
}
