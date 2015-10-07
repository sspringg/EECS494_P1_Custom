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
	public int randomVal, spacesMoved = 0;
	public int	moveSpeed = 4;
	public double epsilon = .05;
	public int cur_action = 0;
	public bool action_selected = false;
	public bool continue_moving = false;
	public int checkpoint_dir = 1;
	public Vector3 cur_checkpoint;
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
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list.Add (PokemonObject.getPokemon ("None"));
		opponent_pokemon_list[0] = PokemonObject.getPokemon ("Squirtle");
		opponent_pokemon_list[1] = PokemonObject.getPokemon ("Caterpie");
		opponent_itemsDictionary.Add("POTION",2);
		opponent_itemsDictionary.Add("POKeBALL",2);

		cur_checkpoint.x = 68;
		cur_checkpoint.y = 69;
		cur_checkpoint.z = -0.01f;

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
			if (S.opponent_pos.y < Player.S.pos.y) {
				randomVal = UnityEngine.Random.Range (1, 10);
				spacesMoved = randomVal;
				cur_action = 1;
				ActionViewer.S.gameObject.SetActive(true);
				ActionViewer.printMessage("Opponent chose to move");
				continue_moving = false;
			} else {
				randomVal = UnityEngine.Random.Range (0, 2);
				int pokeCount = 0;
				foreach(PokemonObject obj in opponent_pokemon_list){
					if(obj.pkmnName != "None")
						++pokeCount;
				}
				if (S.opponent_pos.y - Player.S.pos.y <= 4 && Mathf.Abs(S.opponent_pos.x - Player.S.pos.x) <= 4){
					cur_action = 2;
					ActionViewer.S.gameObject.SetActive(true);
					ActionViewer.printMessage("Opponent chose to battle");
				}
				else if(randomVal == 0 && pokeCount > 1){
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
			//see if they run into player_Pokemon
			for(int i = 0; i < Main.S.player_pokeball.Count; ++i){
				if(Math.Abs(Main.S.player_pokeball[i].x - transform.position.x) < epsilon && Math.Abs(Main.S.player_pokeball[i].x - transform.position.x) < epsilon){
					Dialog.S.gameObject.SetActive(true);
					Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
					noAlpha.a = 255;
					GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
					Dialog.S.ShowMessage("Opponent ran into " + Main.S.player_pokeball[i].place_pokemon.pkmnName);
					//logic to take pokeMon Health
					randomVal = UnityEngine.Random.Range (35, 70);
					int hp_take = Main.S.player_pokeball[i].place_pokemon.curHp * randomVal/100;
					bool alivePokemon = false;	
					foreach(PokemonObject obj in opponent_pokemon_list){
						if(obj.curHp < hp_take){
							obj.curHp = 0;
							hp_take -= obj.curHp;
						}
						else{
							obj.curHp -= hp_take;
							alivePokemon = true;
							break;
						}
					}
					Main.S.player_pokeball.RemoveAt(i);
					if(!alivePokemon)
						checkpoint();
					Turn_Choice_Menu.S.gameObject.SetActive(true);
					Main.S.playerTurn = true;
					spacesMoved = 1;
				}	
			}
			//////possibly make opponent run into wild pokemon
			if((transform.position.x >= 67 && transform.position.x <= 68 && transform.position.y >= 70 && transform.position.x <= 75) || 
			   (transform.position.x >= 64 && transform.position.x <= 67 && transform.position.y >= 78 && transform.position.x <= 79) ||
			   (transform.position.x >= 70 && transform.position.x <= 73 && transform.position.y >= 82 && transform.position.x <= 85) || 
			   (transform.position.x >= 72 && transform.position.x <= 75 && transform.position.y >= 92 && transform.position.x <= 95) ||
			   (transform.position.x >= 68 && transform.position.x <= 75 && transform.position.y >= 98 && transform.position.x <= 101) && !Main.S.playerTurn){
				randomVal = UnityEngine.Random.Range(0, 400);
			   	if(randomVal < 2){
					Dialog.S.gameObject.SetActive(true);
					Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
					noAlpha.a = 255;
					GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
					randomVal = UnityEngine.Random.Range(0, 1);
					int hp_take;
					string pokemon_Encounter;
					if(randomVal == 0){
						hp_take = 30;
						pokemon_Encounter = "Caterpie";
					}
					else{
						pokemon_Encounter = "Pidgey";
						hp_take = 35;
					}
					int pokeCount = 0;
					foreach(PokemonObject obj in opponent_pokemon_list){
						if(obj.pkmnName != "None")
							++pokeCount;
					}
					randomVal = UnityEngine.Random.Range(0, 100);
					if(randomVal < 20 && pokeCount < 4 && opponent_itemsDictionary.ContainsKey("POKeBALL")){
						opponent_itemsDictionary["POKeBALL"]--;
						if(opponent_itemsDictionary["POKeBALL"] == 0)
							opponent_itemsDictionary.Remove("POKeBALL");
						Dialog.S.ShowMessage("Opponent ran into a wild " + pokemon_Encounter + "/n and captured her");
						for(int i = 0; i < opponent_pokemon_list.Count; ++i){
							if(opponent_pokemon_list[i].pkmnName != "None"){
								opponent_pokemon_list[i] = PokemonObject.getPokemon(pokemon_Encounter);
								break;	
							}
						}
					}
					else
						Dialog.S.ShowMessage("Opponent ran into a wild " + pokemon_Encounter);
					bool alivePokemon = false;
					foreach(PokemonObject obj in opponent_pokemon_list){
						if(obj.curHp < hp_take){
							obj.curHp = 0;
							hp_take -= obj.curHp;
						}
						else{
							obj.curHp -= hp_take;
							alivePokemon = true;
							break;
						}
					}
					if(!alivePokemon){
						checkpoint();
					}
					Turn_Choice_Menu.S.gameObject.SetActive(true);
					Main.S.playerTurn = true;
					spacesMoved = 1;
	
			   	}
			}
			if((opponent_targetPos - opponent_pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				opponent_pos = opponent_targetPos; //around min 17
				opponent_moving = false;
				--spacesMoved;
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
		switch (dir_pos) {
		case 11:
			cur_checkpoint.x = 66;
			cur_checkpoint.y = 77;
			checkpoint_dir = dir_pos;
			break;
		case 16:
			cur_checkpoint.x = 66;
			cur_checkpoint.y = 82;
			checkpoint_dir = dir_pos;
			break;
		case 24:
			cur_checkpoint.x = 70;
			cur_checkpoint.y = 86;
			checkpoint_dir = dir_pos;
			break;
		case 31:
			cur_checkpoint.x = 67;
			cur_checkpoint.y = 90;
			checkpoint_dir = dir_pos;
			break;
		case 36:
			cur_checkpoint.x = 72;
			cur_checkpoint.y = 90;
			checkpoint_dir = dir_pos;
			break;
		case 50:
			cur_checkpoint.x = 72;
			cur_checkpoint.y = 104;
			checkpoint_dir = dir_pos;
			break;
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

	public static void checkpoint(){
		Player.S.inScene0 = true;
		S.opponent_moving = false;
		S.continue_moving = false;
		S.opponent_moveVec = Vector3.zero;
		S.transform.position = S.cur_checkpoint;
		for (int j = 0; j < 6; ++j) {
			Opponent.S.opponent_pokemon_list [j].curHp = Opponent.S.opponent_pokemon_list [j].totHp;
			Opponent.S.opponent_pokemon_list [j].move1.curPp = Opponent.S.opponent_pokemon_list [j].move1.totPp;
			Opponent.S.opponent_pokemon_list [j].move2.curPp = Opponent.S.opponent_pokemon_list [j].move2.totPp;
			Opponent.S.opponent_pokemon_list [j].move3.curPp = Opponent.S.opponent_pokemon_list [j].move3.totPp;
			Opponent.S.opponent_pokemon_list [j].move4.curPp = Opponent.S.opponent_pokemon_list [j].move4.totPp;
			Opponent.S.opponent_pokemon_list [j].stat = pkmnStatus.OK;
		}
		S.dir_pos = S.checkpoint_dir;
	}
}
