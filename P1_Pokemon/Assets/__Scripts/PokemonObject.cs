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

	void start(){
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
			pkmn.totHp = 45;
			pkmn.curHp = 45;
			pkmn.atk = 30;
			pkmn.def = 35;
			pkmn.spAtk = 20;
			pkmn.spDef = 20;
			pkmn.speed = 45;
			pkmn.move1 = AttackMove.getMove("Bug Bite");
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
		double modifier1 = 1.0;
		double modifier2 = 1.0;
		--atkMove.curPp;
		int dmg = (int)Math.Floor ((((2.0 * ((double)attacker.level+10.0)/250.0) * (double)attacker.atk/(double)def * (double)atkMove.pwr) + 2) * modifier1 * modifier2);
		if ((curHp - dmg) < 0)
			curHp = 0;
		else
			curHp -= dmg;
		if (curHp <= 0 && isPlayer) {
			stat = pkmnStatus.FAINTED;
			int i;
			for (i = 0; i < 6; ++i) {
				if (Player.S.pokemon_list [i].curHp > 0) {
					BattleScreen.updatePokemon (true, Player.S.pokemon_list [i]);
					break;
				}
			}
			if (i == 6) {
				Vector3 pos;
				pos.x = 21;
				pos.y = 102;
				pos.z = -0.01f;
				Player.S.MoveThroughDoor (pos);
				for (int j = 0; j < 6; ++j) {
					Player.S.pokemon_list [j].curHp = Player.S.pokemon_list [j].totHp;
					Player.S.pokemon_list [j].move1.curPp = Player.S.pokemon_list [j].move1.totPp;
					Player.S.pokemon_list [j].move2.curPp = Player.S.pokemon_list [j].move2.totPp;
					Player.S.pokemon_list [j].move3.curPp = Player.S.pokemon_list [j].move3.totPp;
					Player.S.pokemon_list [j].move4.curPp = Player.S.pokemon_list [j].move4.totPp;
					Player.S.pokemon_list [j].stat = pkmnStatus.OK;
				}
				BattleScreen.DestroyHelper ();
			}
		}
	}
}
