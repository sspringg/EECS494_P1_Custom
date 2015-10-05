using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum pkmnType
{
	bug,
	dragon,
	ice,
	fighting,
	fire,
	flying,
	grass,
	ghost,
	ground,
	electric,
	normal,
	poision,
	psychic,
	rock,
	water,
	none
}

public enum pkmnStatus
{
	OK,
	FAINTED
}

public class PokemonObject{

	public string pkmnName;
	public pkmnType type1;
	public pkmnType type2;
	public int totHp;
	public int curHp;
	public int atk;
	public int def;
	public int spAtk;
	public int spDef;
	public int speed;
	public int level;
	public int exp;
	public bool fought;
	public pkmnStatus stat;

	public AttackMove move1;
	public AttackMove move2;
	public AttackMove move3;
	public AttackMove move4;

	public static Dictionary<pkmnType, Dictionary<pkmnType, double>> modifierTable;

	public static void start(){
		modifierTable = new Dictionary<pkmnType, Dictionary<pkmnType, double>> ();
		modifierTable.Add (pkmnType.normal, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.normal].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.fighting, 2);
		modifierTable [pkmnType.normal].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.ghost, 0);
		modifierTable [pkmnType.normal].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.water, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.normal].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.fighting, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.fighting].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.flying, 2);
		modifierTable [pkmnType.fighting].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.rock, 0.5);
		modifierTable [pkmnType.fighting].Add (pkmnType.bug, 0.5);
		modifierTable [pkmnType.fighting].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.water, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.psychic, 2);
		modifierTable [pkmnType.fighting].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.fighting].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.flying, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.flying].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.fighting, 0.5);
		modifierTable [pkmnType.flying].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.ground, 0);
		modifierTable [pkmnType.flying].Add (pkmnType.rock, 2);
		modifierTable [pkmnType.flying].Add (pkmnType.bug, 0.5);
		modifierTable [pkmnType.flying].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.water, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.flying].Add (pkmnType.electric, 2);
		modifierTable [pkmnType.flying].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.ice, 2);
		modifierTable [pkmnType.flying].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.flying].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.poision, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.poision].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.fighting, 0.5);
		modifierTable [pkmnType.poision].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.poision, 0.5);
		modifierTable [pkmnType.poision].Add (pkmnType.ground, 2);
		modifierTable [pkmnType.poision].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.bug, 2);
		modifierTable [pkmnType.poision].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.water, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.poision].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.psychic, 2);
		modifierTable [pkmnType.poision].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.poision].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.ground, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.ground].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.poision, 0.5);
		modifierTable [pkmnType.ground].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.rock, 0.5);
		modifierTable [pkmnType.ground].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.water, 2);
		modifierTable [pkmnType.ground].Add (pkmnType.grass, 2);
		modifierTable [pkmnType.ground].Add (pkmnType.electric, 0);
		modifierTable [pkmnType.ground].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.ice, 2);
		modifierTable [pkmnType.ground].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.ground].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.rock, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.rock].Add (pkmnType.normal, 0.2);
		modifierTable [pkmnType.rock].Add (pkmnType.fighting, 2);
		modifierTable [pkmnType.rock].Add (pkmnType.flying, 0.5);
		modifierTable [pkmnType.rock].Add (pkmnType.poision, 0.5);
		modifierTable [pkmnType.rock].Add (pkmnType.ground, 2);
		modifierTable [pkmnType.rock].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.fire, 0.5);
		modifierTable [pkmnType.rock].Add (pkmnType.water, 2);
		modifierTable [pkmnType.rock].Add (pkmnType.grass, 2);
		modifierTable [pkmnType.rock].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.rock].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.bug, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.bug].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.fighting, 0.5);
		modifierTable [pkmnType.bug].Add (pkmnType.flying, 2);
		modifierTable [pkmnType.bug].Add (pkmnType.poision, 2);
		modifierTable [pkmnType.bug].Add (pkmnType.ground, 0.5);
		modifierTable [pkmnType.bug].Add (pkmnType.rock, 2);
		modifierTable [pkmnType.bug].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.fire, 2);
		modifierTable [pkmnType.bug].Add (pkmnType.water, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.bug].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.bug].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.ghost, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.ghost].Add (pkmnType.normal, 0);
		modifierTable [pkmnType.ghost].Add (pkmnType.fighting, 0);
		modifierTable [pkmnType.ghost].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.poision, 0.5);
		modifierTable [pkmnType.ghost].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.bug, 0.5);
		modifierTable [pkmnType.ghost].Add (pkmnType.ghost, 2);
		modifierTable [pkmnType.ghost].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.water, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.ghost].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.fire, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.fire].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.ground, 2);
		modifierTable [pkmnType.fire].Add (pkmnType.rock, 2);
		modifierTable [pkmnType.fire].Add (pkmnType.bug, 0.5);
		modifierTable [pkmnType.fire].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.fire, 0.5);
		modifierTable [pkmnType.fire].Add (pkmnType.water, 2);
		modifierTable [pkmnType.fire].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.fire].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.fire].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.water, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.water].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.water].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.water].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.water].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.water].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.water].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.water].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.water].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.water].Add (pkmnType.fire, 0.5);
		modifierTable [pkmnType.water].Add (pkmnType.water, 0.5);
		modifierTable [pkmnType.water].Add (pkmnType.grass, 2);
		modifierTable [pkmnType.water].Add (pkmnType.electric, 2);
		modifierTable [pkmnType.water].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.water].Add (pkmnType.ice, 0.5);
		modifierTable [pkmnType.water].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.water].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.grass, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.grass].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.flying, 2);
		modifierTable [pkmnType.grass].Add (pkmnType.poision, 2);
		modifierTable [pkmnType.grass].Add (pkmnType.ground, 0.5);
		modifierTable [pkmnType.grass].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.bug, 2);
		modifierTable [pkmnType.grass].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.fire, 2);
		modifierTable [pkmnType.grass].Add (pkmnType.water, 0.5);
		modifierTable [pkmnType.grass].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.grass].Add (pkmnType.electric, 0.5);
		modifierTable [pkmnType.grass].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.ice, 2);
		modifierTable [pkmnType.grass].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.grass].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.electric, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.electric].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.flying, 0.5);
		modifierTable [pkmnType.electric].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.ground, 2);
		modifierTable [pkmnType.electric].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.water, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.electric, 0.5);
		modifierTable [pkmnType.electric].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.electric].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.psychic, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.psychic].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.fighting, 0.5);
		modifierTable [pkmnType.psychic].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.bug, 2);
		modifierTable [pkmnType.psychic].Add (pkmnType.ghost, 0);
		modifierTable [pkmnType.psychic].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.water, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.psychic, 0.5);
		modifierTable [pkmnType.psychic].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.psychic].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.ice, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.ice].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.fighting, 2);
		modifierTable [pkmnType.ice].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.rock, 2);
		modifierTable [pkmnType.ice].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.fire, 2);
		modifierTable [pkmnType.ice].Add (pkmnType.water, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.ice, 0.5);
		modifierTable [pkmnType.ice].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.ice].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.dragon, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.dragon].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.fire, 0.5);
		modifierTable [pkmnType.dragon].Add (pkmnType.water, 0.5);
		modifierTable [pkmnType.dragon].Add (pkmnType.grass, 0.5);
		modifierTable [pkmnType.dragon].Add (pkmnType.electric, 0.5);
		modifierTable [pkmnType.dragon].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.dragon].Add (pkmnType.ice, 2);
		modifierTable [pkmnType.dragon].Add (pkmnType.dragon, 2);
		modifierTable [pkmnType.dragon].Add (pkmnType.none, 1);

		modifierTable.Add (pkmnType.none, new Dictionary<pkmnType, double> ());

		modifierTable [pkmnType.none].Add (pkmnType.normal, 1);
		modifierTable [pkmnType.none].Add (pkmnType.fighting, 1);
		modifierTable [pkmnType.none].Add (pkmnType.flying, 1);
		modifierTable [pkmnType.none].Add (pkmnType.poision, 1);
		modifierTable [pkmnType.none].Add (pkmnType.ground, 1);
		modifierTable [pkmnType.none].Add (pkmnType.rock, 1);
		modifierTable [pkmnType.none].Add (pkmnType.bug, 1);
		modifierTable [pkmnType.none].Add (pkmnType.ghost, 1);
		modifierTable [pkmnType.none].Add (pkmnType.fire, 1);
		modifierTable [pkmnType.none].Add (pkmnType.water, 1);
		modifierTable [pkmnType.none].Add (pkmnType.grass, 1);
		modifierTable [pkmnType.none].Add (pkmnType.electric, 1);
		modifierTable [pkmnType.none].Add (pkmnType.psychic, 1);
		modifierTable [pkmnType.none].Add (pkmnType.ice, 1);
		modifierTable [pkmnType.none].Add (pkmnType.dragon, 1);
		modifierTable [pkmnType.none].Add (pkmnType.none, 1);
	}

	public static PokemonObject getPokemon(string inputName){
		PokemonObject pkmn = new PokemonObject ();
		pkmn.level = 5;
		pkmn.exp = 5 * 5 * 5;
		pkmn.fought = false;
		pkmn.stat = pkmnStatus.OK;
		switch (inputName) {
		case "Bulbasaur":
			pkmn.pkmnName = "Bulbasaur";
			pkmn.type1 = pkmnType.grass;
			pkmn.type2 = pkmnType.poision;
			pkmn.totHp = 45;
			pkmn.curHp = 45;
			pkmn.atk = 49;
			pkmn.def = 49;
			pkmn.spAtk = 65;
			pkmn.spDef = 65;
			pkmn.speed = 45;
			pkmn.move1 = AttackMove.getMove("Tackle");
			pkmn.move2 = AttackMove.getMove("Scratch");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			break;
		case "Charmander":
			pkmn.pkmnName = "Charmander";
			pkmn.type1 = pkmnType.fire;
			pkmn.type2 = pkmnType.none;
			pkmn.totHp = 39;
			pkmn.curHp = 39;
			pkmn.atk = 52;
			pkmn.def = 43;
			pkmn.spAtk = 60;
			pkmn.spDef = 50;
			pkmn.speed = 65;
			pkmn.move1 = AttackMove.getMove("Scratch");
			pkmn.move2 = AttackMove.getMove("Tackle");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			break;
		case "Squirtle":
			pkmn.pkmnName = "Squirtle";
			pkmn.type1 = pkmnType.water;
			pkmn.type2 = pkmnType.none;
			pkmn.totHp = 44;
			pkmn.curHp = 44;
			pkmn.atk = 48;
			pkmn.def = 65;
			pkmn.spAtk = 50;
			pkmn.spDef = 64;
			pkmn.speed = 43;
			pkmn.move1 = AttackMove.getMove("Tackle");
			pkmn.move2 = AttackMove.getMove("Scratch");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			break;
		case "Pikachu":
			pkmn.pkmnName = "Pikachu";
			pkmn.type1 = pkmnType.electric;
			pkmn.type2 = pkmnType.none;
			pkmn.totHp = 35;
			pkmn.curHp = 35;
			pkmn.atk = 55;
			pkmn.def = 40;
			pkmn.spAtk = 50;
			pkmn.spDef = 50;
			pkmn.speed = 90;
			pkmn.move1 = AttackMove.getMove("Thunder Shock");
			pkmn.move2 = AttackMove.getMove("Tackle");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			break;
		case "Caterpie":
			pkmn.pkmnName = "Caterpie";
			pkmn.type1 = pkmnType.bug;
			pkmn.type2 = pkmnType.none;
			pkmn.totHp = 30;
			pkmn.curHp = 30;
			pkmn.atk = 20;
			pkmn.def = 25;
			pkmn.spAtk = 20;
			pkmn.spDef = 20;
			pkmn.speed = 35;
			pkmn.move1 = AttackMove.getMove("Bug Bite");
			pkmn.move2 = AttackMove.getMove("Tackle");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			pkmn.level = 3;
			break;
		case "Pidgey":
			pkmn.pkmnName = "Pidgey";
			pkmn.type1 = pkmnType.normal;
			pkmn.type2 = pkmnType.flying;
			pkmn.totHp = 35;
			pkmn.curHp = 35;
			pkmn.atk = 30;
			pkmn.def = 35;
			pkmn.spAtk = 20;
			pkmn.spDef = 20;
			pkmn.speed = 45;
			pkmn.move1 = AttackMove.getMove("Gust");
			pkmn.move2 = AttackMove.getMove("Tackle");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			pkmn.level = 3;
			break;
		default :
			pkmn.pkmnName = "None";
			pkmn.type1 = pkmnType.none;
			pkmn.type2 = pkmnType.none;
			pkmn.totHp = 0;
			pkmn.curHp = 0;
			pkmn.atk = 0;
			pkmn.def = 0;
			pkmn.spAtk = 0;
			pkmn.spDef = 0;
			pkmn.speed = 0;
			pkmn.move1 = AttackMove.getMove("None");
			pkmn.move2 = AttackMove.getMove("None");
			pkmn.move3 = AttackMove.getMove("None");
			pkmn.move4 = AttackMove.getMove("None");
			break;
		}
		return pkmn;
	}

	public void takeHit(AttackMove atkMove, PokemonObject attacker, bool isPlayer){
		if (atkMove.moveName == "None")
			return;
		TurnActionViewer.S.diffmod = 1;
		double modifier1 = modifierTable[type1][atkMove.type];
		double modifier2 = modifierTable[type2][atkMove.type];
		double totmod = modifier1 * modifier2;
		Debug.Log ("total mod : " + totmod.ToString ());
		if (totmod != 1)
			TurnActionViewer.S.diffmod = totmod;
		if (!isPlayer)
			TurnActionViewer.S.diffmod += 10;
		--atkMove.curPp;
		int dmg = (int)Math.Floor ((((2.0 * ((double)attacker.level+10.0)/250.0) * (double)attacker.atk/(double)def * (double)atkMove.pwr) + 2) * totmod);
		if ((curHp - dmg) < 0)
			curHp = 0;
		else
			curHp -= dmg;
		if (curHp <= 0 && isPlayer) {
			stat = pkmnStatus.FAINTED;
			int i;
			TurnActionViewer.S.activeDied = pkmnName;
			for (i = 0; i < 6; ++i) {
				if (Player.S.pokemon_list [i].curHp > 0) {
					BattleScreen.updatePokemon (true, Player.S.pokemon_list [i]);
					break;
				}
			}
			if (i == 6) {
				TurnActionViewer.S.allDied = true;
				Vector3 pos;
				pos.x = 21;
				pos.y = 52;
				pos.z = -0.01f;
				Player.S.inScene0 = true;
				Player.S.MoveThroughDoor (pos);
				for (int j = 0; j < 6; ++j) {
					Player.S.pokemon_list [j].curHp = Player.S.pokemon_list [j].totHp;
					Player.S.pokemon_list [j].move1.curPp = Player.S.pokemon_list [j].move1.totPp;
					Player.S.pokemon_list [j].move2.curPp = Player.S.pokemon_list [j].move2.totPp;
					Player.S.pokemon_list [j].move3.curPp = Player.S.pokemon_list [j].move3.totPp;
					Player.S.pokemon_list [j].move4.curPp = Player.S.pokemon_list [j].move4.totPp;
					Player.S.pokemon_list [j].stat = pkmnStatus.OK;
				}
			}
		}
	}
}
