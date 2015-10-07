﻿using UnityEngine;
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
	public int randomVal, spacesMoved = 0;
	public int	moveSpeed = 4;
	public int cur_action = 0;
	public bool action_selected = false;
	public bool continue_moving = false;
	public Vector3	opponent_targetPos;
	public Vector3	opponent_moveVec;
	public Vector3 opponent_pos{
		get {return transform.position;}
		set{transform.position = value;}
	}
	public RaycastHit	hitInfo;
	
	public List<Pokeball_Info> opponent_pokeball = new List<Pokeball_Info>();
	public List<PokemonObject> opponent_pokemon_list = new List<PokemonObject>();
	public List<Vector3> dir_list = new List<Vector3>();
	public int dir_pos = 0;
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
		opponent_pokemon_list [0].totHp = 50;
		opponent_pokemon_list [0].curHp = 50;
		opponent_pokemon_list [0].atk = 52;
		opponent_pokemon_list [0].level = 6;
		opponent_pokemon_list.Add(PokemonObject.getPokemon ("Caterpie"));
		opponent_itemsDictionary.Add("POTION",1);
		opponent_itemsDictionary.Add("POKeBALL",2);

		dir_list.Add (Vector3.left);
		for (int i = 0; i < 8; ++i)
			dir_list.Add (Vector3.up);
		for (int i = 0; i < 2; ++i)
			dir_list.Add (Vector3.left);
		for (int i = 0; i < 5; ++i)
			dir_list.Add (Vector3.up);
		for (int i = 0; i < 4; ++i)
			dir_list.Add (Vector3.right);
		for (int i = 0; i < 4; ++i)
			dir_list.Add (Vector3.up);
		for (int i = 0; i < 3; ++i)
			dir_list.Add (Vector3.left);
		for (int i = 0; i < 4; ++i)
			dir_list.Add (Vector3.up);
		for (int i = 0; i < 5; ++i)
			dir_list.Add (Vector3.right);
		for (int i = 0; i < 14; ++i)
			dir_list.Add (Vector3.up);
		for (int i = 0; i < 3; ++i)
			dir_list.Add (Vector3.left);
		for (int i = 0; i < 10; ++i)
			dir_list.Add (Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		if (spacesMoved != 0 && !opponent_moving && continue_moving)
			moveOpponentForward ();		//keep moving until moved all spaces
		else if (!Main.S.playerTurn && !opponent_moving && !action_selected) {
			action_selected = true;
			cur_action = 0;
			print ("! player turn");
			if (S.opponent_pos.y < Player.S.pos.y) {
				randomVal = UnityEngine.Random.Range (1, 10);
				print ("raval: " + randomVal);
				spacesMoved = randomVal;
				cur_action = 1;
				ActionViewer.S.gameObject.SetActive(true);
				ActionViewer.printMessage("Opponent chose to move");
				continue_moving = false;
			} else {
				randomVal = UnityEngine.Random.Range (0, 2);
				if (S.opponent_pos.y - Player.S.pos.y <= 4 && Mathf.Abs(S.opponent_pos.x - Player.S.pos.x) <= 4){
					cur_action = 2;
					ActionViewer.S.gameObject.SetActive(true);
					ActionViewer.printMessage("Opponent chose to battle");
				}
				else if(randomVal == 0){
					cur_action = 3;
					ActionViewer.S.gameObject.SetActive(true);
					ActionViewer.printMessage("Opponent chose to place trap");
				}
				else{
					cur_action = 4;
					ActionViewer.S.gameObject.SetActive(true);
					ActionViewer.printMessage("Opponent chose to place obstacle");
				}
			}
		}
		else if(opponent_moving){
			//opponent is moving
			if((opponent_targetPos - opponent_pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				opponent_pos = opponent_targetPos; //around min 17
				opponent_moving = false;
				--spacesMoved;
				print("space move: " + spacesMoved);
				if(spacesMoved == 0){
					Turn_Choice_Menu.S.gameObject.SetActive(true);
					Main.S.playerTurn = true;
				}
			}
			else{
				opponent_pos += (opponent_targetPos - opponent_pos).normalized * moveSpeed * Time.fixedDeltaTime;
			}
			
		}
		else if(moveTowardPlayer){		//player called opponent into battle so move toward them
			moveOpponentToward();
		}
	}
	public void moveOpponentToward(){
		if((Player.S.transform.position.y - gameObject.transform.position.y) > 1 
		        && !Physics.Raycast(Player.S.GetRay(), out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.up;
			sprend.sprite = upSprite;
			opponent_moving = true;
		}
		else if((gameObject.transform.position.x - Player.S.transform.position.x) > 1 
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
	public void moveOpponentForward(){
		if (dir_list [dir_pos] == Vector3.left) {
			opponent_moveVec = Vector3.left;
			sprend.sprite = leftSprite;
			opponent_moving = true;
		}
		else if (dir_list [dir_pos] == Vector3.right) {
			opponent_moveVec = Vector3.right;
			sprend.sprite = rightSprite;
			opponent_moving = true;
		}
		else if (dir_list [dir_pos] == Vector3.up) {
			opponent_moveVec = Vector3.up;
			sprend.sprite = upSprite;
			opponent_moving = true;
		}
		else if (dir_list [dir_pos] == Vector3.down) {
			opponent_moveVec = Vector3.down;
			sprend.sprite = downSprite;
			opponent_moving = true;
		}
		/*
		if(dir_list[dir_pos] == Vector3.left && !Physics.Raycast(gameObject.transform.position, Vector3.left, out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.left;
			sprend.sprite = leftSprite;
			opponent_moving = true;
		}
		else if(dir_list[dir_pos] == Vector3.right && !Physics.Raycast(gameObject.transform.position, Vector3.right, out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.right;
			sprend.sprite = rightSprite;
			opponent_moving = true;
		}
		else if(dir_list[dir_pos] == Vector3.up && !Physics.Raycast(gameObject.transform.position, Vector3.up, out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.up;
			sprend.sprite = upSprite;
			opponent_moving = true;
		}
		else if(dir_list[dir_pos] == Vector3.down && !Physics.Raycast(gameObject.transform.position, Vector3.down, out hitInfo, 1f, Player.S.GetLayerMask(new string[] {"Immovable", "NPC","Ledge", "Player"}))){
			opponent_moveVec = Vector3.down;
			sprend.sprite = downSprite;
			opponent_moving = true;
		}*/
		opponent_targetPos = opponent_pos + opponent_moveVec;
		++dir_pos;
		if (dir_pos >= dir_list.Count) {
			print ("Opponent Wins");
			//stop game
			S.gameObject.SetActive (false);
		}
	}
}
