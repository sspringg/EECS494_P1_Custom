using UnityEngine;
using System.Collections;
using System;
public class NPC : MonoBehaviour {
	
	public	Sprite	upSprite;
	public	Sprite	downSprite;
	public	Sprite	leftSprite;
	public	Sprite	rightSprite;
	
	public bool	moveTowardPlayer = false;
	
	public int	leftRightDist;
	public int	upDownDist;
	private	int horizDist = 0;
	private	int	vertDist = 0;
	public bool	allowedToMove;
	private float randomVal;
	private	int	dictionaryPos;
	public bool trainer;
	public UnityEngine.Random random = new UnityEngine.Random();
	
	public float chanceToMove = 1;
	
	public SpriteRenderer	sprend;
	// Use this for initialization
	void Start () {
		sprend = gameObject.GetComponent<SpriteRenderer>();
	}
	public void Play_Dialog(string playerName){
		string sentance = TalkTo(playerName);
		if(sentance == "EXIT")
			return;
		Dialog.S.gameObject.SetActive(true);
		Color noAlpha = GameObject.Find("DialogBackground").GetComponent<GUITexture>().color;
		noAlpha.a = 255;
		GameObject.Find("DialogBackground").GetComponent<GUITexture>().color = noAlpha;
		Dialog.S.ShowMessage(sentance);
	}
	public void FacePlayer(Direction playerDir){
		switch(playerDir){
		case Direction.down:
			sprend.sprite = upSprite;
			break;
		case Direction.up:
			sprend.sprite = downSprite;
			break;
		case Direction.left:
			sprend.sprite = rightSprite;
			break;
		case Direction.right:
			sprend.sprite = leftSprite;
			break;
		}
	}
	void FixedUpdate(){
		if(allowedToMove){
			randomVal = UnityEngine.Random.Range(0, 100);
			if(randomVal < chanceToMove && (Main.S.inDialog != true)){
				randomVal = UnityEngine.Random.Range(0, 100);
				if(randomVal < 25 && (horizDist > (-leftRightDist)) && (gameObject.transform.position + Vector3.left != Player.S.transform.position)){ //try to move left
					sprend.sprite = leftSprite;
					gameObject.transform.position += Vector3.left;
					--horizDist;
				}
				else if(randomVal > 25 && randomVal < 50 && (horizDist < leftRightDist) && (gameObject.transform.position + Vector3.right != Player.S.transform.position)){ //try to move right
					sprend.sprite = rightSprite;
					gameObject.transform.position += Vector3.right;
					++horizDist;
				}
				else if(randomVal > 50 && randomVal < 75 && (vertDist > (-upDownDist)) && (gameObject.transform.position + Vector3.down != Player.S.transform.position)){ //try to move down
					sprend.sprite = downSprite;
					gameObject.transform.position += Vector3.down;
					--vertDist;
				}
				else if(randomVal > 75 && (vertDist < (upDownDist)) && (gameObject.transform.position + Vector3.up != Player.S.transform.position)){ //try to move up
					sprend.sprite = upSprite;
					gameObject.transform.position += Vector3.up;
					++vertDist;
				}
			}
		}
		if(moveTowardPlayer && !Main.S.inDialog){
			if((gameObject.transform.position.x - Player.S.transform.position.x) > 1){
				transform.position += Vector3.left * (Time.deltaTime * 4);
			}
			else if((Player.S.transform.position.x - gameObject.transform.position.x) > 1){
				gameObject.transform.position += Vector3.right * (Time.deltaTime * 4);
			}
			else if((gameObject.transform.position.y - Player.S.transform.position.y) > 1){
				gameObject.transform.position += Vector3.down * (Time.deltaTime * 4);
			}
			else if((Player.S.transform.position.y - gameObject.transform.position.y) > 1){
				gameObject.transform.position += Vector3.up * (Time.deltaTime * 4);
			}
			else{
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
				if(trainer){
					Player.S.inScene0 = false;
					Application.LoadLevelAdditive("_Scene_2");
				}
			}
		}
	}
	private string TalkTo(string playerName) {
		if (!Player.S.speakDictionary.ContainsKey(playerName)) {
			Player.S.speakDictionary.Add(playerName, -1);
		}
		if(playerName == "Professor_Oak"){
			dictionaryPos = Player.S.speakDictionary["Professor_Oak"];
			if(Player.S.itemsDictionary.ContainsKey("Prof_Oak_Package"))
				Player.S.itemsDictionary["Professor_Oak"] = 3;
			switch(dictionaryPos){ //PROFESSOR OAK
			case -1:
				Player.S.speakDictionary["Professor_Oak"] = 0;
				return "";
			case 0:
				Player.S.speakDictionary["Professor_Oak"] = 1;
				return "Hello, Red! Welcome to my lab. I have a couple pokemon leftover from my training days, ";
			case 1:
				Player.S.speakDictionary["Professor_Oak"] = 2;
				Player.S.playerSpeaking = null;
				return "Choose one from the table to get started.";
			case 2:
				Player.S.speakDictionary["Professor_Oak"] = 3;
				return "";
			case 3:
				Player.S.playerSpeaking = null;
				return "Hi Red! What are you doing here? Don't forget your mission!";
			case 4:
				Player.S.speakDictionary["Professor_Oak"] = 5;
				return "";
			case 5:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Professor_Oak"] = 6;
				return "AHH, my super POKeBALL! Thank you!";
			case 6:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Professor_Oak"] = 7;
				return "While you are here, look over on that table";
			case 7:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Professor_Oak"] = 8;
				return "It's new technology called a POKeDEX. My dream was to study every POKeMON in the  world but I got to old!";
			case 8:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Professor_Oak"] = 2;
				return "I want you to do it for me. It would be a great feat in the POKeMON world";
			}
			
			
		}
		else if(playerName == "Pokemon_Choose_Table"){	//INITIAL Pokemon choosing table
			switch(Player.S.speakDictionary["Pokemon_Choose_Table"]){
			case -1:
				Player.S.allowedToMove = false;
				Player.S.speakDictionary["Pokemon_Choose_Table"] = 0;
				return "";
			case 0:
				Player.S.speakDictionary["Pokemon_Choose_Table"] = 1;
				Player.S.speakDictionary["Mom"] = 1;
				Player.S.speakDictionary["Professor_Oak"] = 2;
				Player.S.playerSpeaking = null;
				Player.S.ChoosingPokemon = true;
				return "Choose your first Pokemon between Squirttle, Bulbasaur, and Charmander";
			case 1:
				Player.S.speakDictionary["Pokemon_Choose_Table"] = 2;
				return "";
			case 2:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Pokemon_Choose_Table"] = 1;
				return "There are plenty of POKeMON to go catch in the wild";
			}
		}
		else if(playerName == "Gym_Locked"){
			switch(Player.S.speakDictionary["Gym_Locked"]){
			case -1:
				Player.S.speakDictionary["Gym_Locked"] = 0;
				return "";
			case 0:
				Player.S.speakDictionary["Gym_Locked"] = -1;
				Player.S.playerSpeaking = null;
				return "You're not strong enough to compete with the big time trainers yet.";
			}
		}
		else if(playerName == "Outside_Lab_NPC"){
			switch(Player.S.speakDictionary["Outside_Lab_NPC"]){
			case -1:
				Player.S.speakDictionary["Outside_Lab_NPC"] = 0;
				return "";
			case 0:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Outside_Lab_NPC"] = -1;
				return "Technology is incredible, you can now store and recall items and POKeMON as data via PC!";
			}
		}
		else if(playerName == "Red_House_Sign"){
			switch(Player.S.speakDictionary["Red_House_Sign"]){
			case -1:
				Player.S.speakDictionary["Red_House_Sign"] = 0;
				return "";
			case 0:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Red_House_Sign"] = -1;
				return "Red House";
			}
		}
		else if(playerName == "Blue_House_Sign"){
			switch(Player.S.speakDictionary["Blue_House_Sign"]){
			case -1:
				Player.S.speakDictionary["Blue_House_Sign"] = 0;
				return "";
			case 0:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Blue_House_Sign"] = -1;
				return "Blue House";
			}
		}
		else if(playerName == "Bug_Catcher"){
			switch(Player.S.speakDictionary["Bug_Catcher"]){
			case -1:
				Player.S.speakDictionary["Bug_Catcher"] = 0;
				Player.S.playerSpeaking = null;
				return "Not so fast Rookie! It's time to teach you a lesson";
			case 0:
				Player.S.speakDictionary["Bug_Catcher"] = 1;
				return "";
			case 1:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Bug_Catcher"] = 0;
				return "You got lucky this time. Just wait until I level up my POKeMON";
				
			}
		}
		else if(playerName == "Lass"){
			switch(Player.S.speakDictionary["Lass"]){
			case -1:
				Player.S.speakDictionary["Lass"] = 0;
				Player.S.playerSpeaking = null;
				return "You may have beaten Bug Catcher but you will be no match for me!";
			case 0:
				Player.S.speakDictionary["Lass"] = 1;
				return "";
			case 1:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Lass"] = 0;
				return "I will have my revenge";
			}
		}
		else if(playerName == "Youngster"){
			switch(Player.S.speakDictionary["Youngster"]){
			case -1:
				Player.S.speakDictionary["Youngster"] = 0;
				Player.S.playerSpeaking = null;
				return "Impressive I must say. Now it is time for me to teach you what being a Pokemon trainer is really about";
			case 0:
				Player.S.speakDictionary["Youngster"] = 1;
				return "";
			case 1:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Youngster"] = 0;
				return "You have a bright future Red!";
			}
		}
		else if(playerName == "Couch_Potato"){
			switch(Player.S.speakDictionary["Couch_Potato"]){
			case -1:
				Player.S.speakDictionary["Couch_Potato"] = 0;
				return "";
			case 0:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Couch_Potato"] = -1;
				return "Pokemon centers heal your tired, hurt, or fainted Pokemon!";
			}
			
		}
		else if(playerName == "Checkout_Front")
		switch(Player.S.speakDictionary["Checkout_Front"]){
			case -1:
			Player.S.speakDictionary["Checkout_Front"] = 0;
			return "";
			case 0:
			Player.S.speakDictionary["Checkout_Front"] = 1;
			return "The Mart is a store to buy POKeBALLS to capture new Pokemon,";
			case 1:
			Player.S.speakDictionary["Checkout_Front"] = 2;
			return "antidote to help heal your POKeMON, and a variety of other items.";
			case 2:
			Player.S.speakDictionary["Checkout_Front"] = 3;
			Player.S.itemsDictionary.Add("Prof_Oak_Package",1);
			return "Wiat a second! I recognize you. You're Professor Oaks new prodigy. Here is a package, can you take it to him?";
			case 3:
			Player.S.playerSpeaking = null;
			Player.S.speakDictionary["Checkout_Front"] = 4;
			return "[Red Received Professor Oak's package]";
			case 4:
			Player.S.speakDictionary["Checkout_Front"] = 5;
			return "";
			case 5:
			Player.S.playerSpeaking = null;
			Player.S.Mart_Options = true;
			return "We have plenty of great stuff in stock today! What would you like?";
			case 6:
			Player.S.speakDictionary["Checkout_Front"] = 10;
			return "Pokeball, great choice! Good luck and be careful with it";
			case 7:
			Player.S.speakDictionary["Checkout_Front"] = 10;
			return "That's the best Potion money can buy!";
			case 8:
			Player.S.speakDictionary["Checkout_Front"] = 10;
			return "This will get your POKeMON feeling better in no time!";
			case 9:
			Player.S.speakDictionary["Checkout_Front"] = 4;
			Player.S.playerSpeaking = null;
			return "You don't have enough money to buy that. You can earn money by winning POKeMON battles.";
			case 10:
			Player.S.speakDictionary["Checkout_Front"] = 4;
			Player.S.playerSpeaking = null;
			return "Thank you for your business, have a great day!";
			
		}
		else if(playerName == "Forward_Clerk"){
			switch(Player.S.speakDictionary["Forward_Clerk"]){
			case -1:
				Player.S.speakDictionary["Forward_Clerk"] = 0;
				return "";
			case 0:
				Player.S.speakDictionary["Forward_Clerk"] = 1;
				return "Welcome to our POKeMON center.";
			case 1:
				Player.S.speakDictionary["Forward_Clerk"] = 2;
				return "We heal your POKeMON back to perfect health.";
			case 2:
				Player.S.playerSpeaking = null;
				Player.S.Healing_Pokemon = true;
				//HEAL (Ok we'll need your pokemon (after 2 seconds) Thank you! Your POKeMoN are fighting fit > We Hope to see you again 
				// Cancel menu > we hope to see you again
				return "Shall we heal your POKeMON?";
			case 3: 
				Player.S.speakDictionary["Forward_Clerk"] = 4;
				return "Ok we'll need your pokemon.";
			case 4:
				Player.S.speakDictionary["Forward_Clerk"] = 5;
				return "...";
			case 5:
				Player.S.speakDictionary["Forward_Clerk"] = 6;
				return "Thank you! Your POKeMoN are fighting fit!";
			case 6:
				Player.S.speakDictionary["Forward_Clerk"] = -1;
				Player.S.playerSpeaking = null;
				return "We hope to see you again!";	
			}
		}
		else if(playerName == "Field_NPC"){
			switch(Player.S.speakDictionary["Field_NPC"]){
			case -1:
				Player.S.speakDictionary["Field_NPC"] = 0;
				return "";
			case 0:
				Player.S.speakDictionary["Field_NPC"] = 1;
				return "See those ledges along the road?";
			case 1:
				Player.S.speakDictionary["Field_NPC"] = 2;
				return "It's a bit scary but you can jump from them";
			case 2:
				Player.S.speakDictionary["Field_NPC"] = -1;
				Player.S.playerSpeaking = null;
				return "You can get back to PALLET TOWN quikcer that way";
			}	
			
		}
		else if(playerName == "Mom"){
			switch(Player.S.speakDictionary["Mom"]){
			case -1:
				Player.S.speakDictionary["Mom"] = 0;
				return "";
			case 0:
				Player.S.playerSpeaking = null;
				Player.S.speakDictionary["Mom"] = -1;
				return "Mom: It's time to go out and explore the world";
			case 1:
				Player.S.speakDictionary["Mom"] = 2;
				return "";
			case 2:
				Player.S.speakDictionary["Mom"] = 3;
				return "Red, you should take a quick rest";
			case 3:
				Player.S.speakDictionary["Mom"] = 4;
				return "...";
			case 4:
				Player.S.speakDictionary["Mom"] = 1;
				for(int i = 0; i < Player.S.pokemon_list.Count; ++i){
					PokemonObject po = Player.S.pokemon_list[i];
					if (po == null) continue;
					Player.S.pokemon_list[i].curHp = Player.S.pokemon_list[i].totHp;
				}
				Player.S.playerSpeaking = null;
				return "You and your pokemon are looking great, take care now";
			}
		}
		else if(playerName == "Grass_Shield"){
			switch(Player.S.speakDictionary["Grass_Shield"]){
			case -1:
				Opponent.S.sprend.sprite = Opponent.S.leftSprite;
				Player.S.sprend.sprite = Player.S.rightSprite;
				Player.S.speakDictionary["Grass_Shield"] = 2;
				return "Blue: You think you are so mighty with your new pokemon from Gramps?";
			case 2:
				Player.S.speakDictionary["Grass_Shield"] = 3;
				return "Blue: Well now its time for me to teach you who is really boss in this town";
			case 3:
				Player.S.speakDictionary["Grass_Shield"] = 4;
				return "Blue: You vs me, race to Viridian city";
			case 4:
				Player.S.speakDictionary["Grass_Shield"] = 5;
				return "Blue: We will take turns moving where each move...";
			case 5:
				Player.S.speakDictionary["Grass_Shield"] = 6;
				return "Blue: you can select 1 of 4 choices";
			case 6:
				Player.S.speakDictionary["Grass_Shield"] = 7;
				return "1: Move a total number of steps as rolled by the die";
			case 7:
				Player.S.speakDictionary["Grass_Shield"] = 8;
				return "2: Place one of your POKeMON at a location in the field...";
			case 8:
				Player.S.speakDictionary["Grass_Shield"] = 9;
				return "If I run into it while moving, I will have to stop and fight...";
			case 9:
				Player.S.speakDictionary["Grass_Shield"] = 10;
				return "but be careful...";
			case 10:
				Player.S.speakDictionary["Grass_Shield"] = 11;
				return "becaue after the POKeMON fights, it will run away";
			case 11:
				Player.S.speakDictionary["Grass_Shield"] = 13;
				return "3: Use an item on a POKeMON to help heal them";
			case 13:
				Player.S.speakDictionary["Grass_Shield"] = 14;
				return "4: Call me to battle you. ";
			case 14:
				Player.S.speakDictionary["Grass_Shield"] = 15;
				return "This can only be done if you are in front of me because I will come to your location";
			case 15:
				Player.S.speakDictionary["Grass_Shield"] = 16;
				return "If you win, I will give you $250 to buy items";
			case 16:
				Player.S.speakDictionary["Grass_Shield"] = 17;
				return "and if at any point either in Trainer or POKeMON battle all of...";
			case 17:
				Player.S.speakDictionary["Grass_Shield"] = 18;
				return "a players POKeMON faint, they will be reset back to the previous checkpoint";
			case 18:
				Player.S.speakDictionary["Grass_Shield"] = 19;
				return "You can open the main menu at any point to check your POKeMON health and buy new items";
			case 19:
				Player.S.speakDictionary["Grass_Shield"] = 20;
				return "Grandpa has given us both 2 POKeBALLs and 1 healing potion to start..";
			case 20:
				Player.S.speakDictionary["Grass_Shield"] = 21;
				return "Let the race begin. Your turn first";
			case 21:
				Player.S.playerSpeaking = null;
				Player.S.OpeningDialog = true;
				Main.S.playerTurn = true;
				Grass_Shield.S.gameObject.SetActive(false);
				Player.S.speakDictionary["Grass_Shield"] = -1;
				return "EXIT";
			}
		}
		else if(playerName == "Winning_Star"){
			switch(Player.S.speakDictionary["Winning_Star"]){
			case -1:
				Player.S.speakDictionary["Winning_Star"] = 0;
				Opponent.S.moveOpponentToward();
				return "Blue: Congratulations Red! I never thought I'd say this but...";
			case 1:
				Player.S.speakDictionary["Winning_Star"] = 2;
				return "Blue: you were a better trainer than me today";
			case 2:
				Player.S.speakDictionary["Winning_Star"] = 3;
				return "Blue: Grandpa gave this to me when I was a young child...";
			case 3:
				Player.S.speakDictionary["Winning_Star"] = 4;
				return "Blue: but I think today you showed you are the one that deserves it";
			case 4:
				Player.S.speakDictionary["Winning_Star"] = 5;
				return "Blue: but I think today you showed you are the one that deserves it";
			case 5:
				Player.S.playerSpeaking = null;
//				Winning_Screen.gameObject.SetActive(true);
				Winning_Star.S.gameObject.SetActive(false);
				return "EXIT";
			}	
		}
		return "";		
	}
	
}