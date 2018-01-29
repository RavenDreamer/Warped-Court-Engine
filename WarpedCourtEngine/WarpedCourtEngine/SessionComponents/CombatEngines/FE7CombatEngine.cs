using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngine
{

	
	

	public class FE7CombatEngine : ICombatEngine
	{
		

		Random myRandom;
		Queue<int> NumberQueue = new Queue<int>();

		/// <summary>
		/// Adds "Random" numbers to the combat engine. They will be used
		/// before actual random numbers are pulled.
		/// </summary>
		/// <param name="rngs"></param>
		public void AddRandomNumbers(List<int> rngs)
		{
			foreach (int i in rngs)
			{
				NumberQueue.Enqueue(i);
			}
		}

		public void ResetRandomNumbers()
		{
			NumberQueue.Clear();
		}

		public FE7CombatEngine(Random rand)
		{
			myRandom = rand;
		}

		List<GameAction> InitiateCombat(FE7UnitEntity attacker, FE7UnitEntity defender, int engagementRange)
		{
			List<GameAction> ret = new List<GameAction>();

			if(!attacker.EquippedWeapon.Range.Contains(engagementRange))
			{
				throw new WeaponException("Attack at improper range");
			}
			bool defenderCanRetalliate = defender.EquippedWeapon.Range.Contains(engagementRange);

			int attackerAttackSpeed = attacker.AttackSpeed;
			int defenderAttackSpeed = defender.AttackSpeed;

			bool attackerDouble = attackerAttackSpeed - defenderAttackSpeed >= 4;
			bool defenderDouble = defenderAttackSpeed - attackerAttackSpeed >= 4;

			WeaponTriangle attackerWeapTriBonus = CalculateWeaponTriangle(attacker.EquippedWeapon.WeaponType, defender.EquippedWeapon.WeaponType);
			WeaponTriangle defenderWeapTriBonus = CalculateWeaponTriangle(defender.EquippedWeapon.WeaponType, attacker.EquippedWeapon.WeaponType);

			//[Precalculated -- Weapon Hit + (Skill x 2) + (Luck / 2) ]+ Support bonus + Weapon Triangle bonus + S Rank bonus + Tactician bonus
			int attackerHit = attacker.HitChance  + 0 + ((int)attackerWeapTriBonus * 15) + 0 + 0;
			int defenderHit = defender.HitChance + 0 + ((int)defenderWeapTriBonus * 15) + 0 + 0;

			// [Precalculated -- (Attack Speed x 2) + Luck ]+ Support bonus + Terrain bonus + Tactician bonus
			defenderHit -= attacker.Avoid + 0 + CalcTerrainAvoidBonus(attacker.Terrain) + 0;
			attackerHit -= defender.Avoid + 0 + CalcTerrainAvoidBonus(defender.Terrain) + 0;

			// Weapon Critical + (Skill / 2) + Support bonus + Critical bonus + S Rank bonus - Crit Avoid [opponent's Luck + Support + Tactician Bonus]
			int attackerCrit = attacker.CritChance + 0 + 0 + 0 - defender.Luck - 0 - 0;
			int defenderCrit = defender.CritChance + 0 + 0 + 0 - attacker.Luck - 0 - 0;

			// StrengthMagic + Weapon Might + [ Weapon Triangle bonus x Effective coefficient] + Support bonus
			int attackerDamage = attacker.Might + ((int)attackerWeapTriBonus * 1) + 0 - CalcTerrainDefBonus(defender.Terrain);
			int defenderDamage = defender.Might + ((int)defenderWeapTriBonus * 1) + 0 - CalcTerrainDefBonus(attacker.Terrain);


			//attacker attacks first.
			GameAction attackerResult = ProcessAttack(MakeAttack(attackerHit, attackerCrit, attackerDamage), attacker, defender);
			ret.Add(attackerResult);

			//check to see if Defender is still alive.
			defender.TakeDamage(attackerResult);

			if(defender.HP > 0 && defenderCanRetalliate && defender.EquippedWeapon.WeaponType != FE7WeaponType.None)
			{
				//defender attacks

				GameAction defenderResult = ProcessAttack(MakeAttack(defenderHit, defenderCrit, defenderDamage), defender, attacker);
				ret.Add(defenderResult);

				attacker.TakeDamage(defenderResult);
			}

			//check to see if attacker is still alive
			if(attacker.HP > 0 && attackerDouble)
			{
				//attack again.
				GameAction followupResult = ProcessAttack(MakeAttack(attackerHit, attackerCrit, attackerDamage), attacker, defender);
				ret.Add(followupResult);

				//check to see if Defender is still alive.
				defender.TakeDamage(followupResult);
			}
			else if(defender.HP > 0 && defenderDouble && defender.EquippedWeapon.WeaponType != FE7WeaponType.None)
			{
				//defender attacks again.
				GameAction followupResult = ProcessAttack(MakeAttack(defenderHit, defenderCrit, defenderDamage), defender, attacker);
				ret.Add(followupResult);

				attacker.TakeDamage(followupResult);
			}

			//Now we check if either died.
			if(defender.HP > 0 && attacker.HP > 0)
			{
				//no one died.
				ret.Add(new CombatDrawAction(attacker, defender));
			}
			else if(attacker.HP > 0)
			{
				//defender died
				ret.Add(new CombatDefenderDefeatAction(attacker, defender));
			}
			else if (defender.HP > 0)
			{
				//attacker died
				ret.Add(new CombatAttackerDefeatAction(attacker, defender));
			}
			else
			{
				//they both died. This shouldn't happen. Oops.
				throw new CombatEngineException("Both units dead after combat");
			}

			return ret;
		}

		private GameAction ProcessAttack(AttackResult attackerResult, FE7UnitEntity attacker, FE7UnitEntity defender)
		{
			int actualDamage = attackerResult.BaseDamage;
			if (attackerResult.Result != AttackRollResult.Miss) //hit or crit
			{
				// Calculate damage
				if (attacker.EquippedWeapon.IsMagic)
				{
					actualDamage -= defender.Resistance;
					if (actualDamage < 0)
					{
						actualDamage = 0;
					}
				}
				else
				{
					actualDamage -= defender.Defense;
					if (actualDamage < 0)
					{
						actualDamage = 0;
					}
				}
				if (attackerResult.Result == AttackRollResult.Crit)
				{
					actualDamage *= 3;
					return new CombatCritAction(attacker, actualDamage, attackerResult.RandNumbersUsed);
				}
				else
				{
					return new CombatHitAction(attacker, actualDamage, attackerResult.RandNumbersUsed);
				}
			}
			else
			{
				return new CombatMissAction(attacker, attackerResult.RandNumbersUsed);
			}
		}

		/// <summary>
		/// Determines whether or not a given attack is a hit, crit, or miss
		/// </summary>
		/// <param name="attackerHit"></param>
		/// <param name="attackerCrit"></param>
		/// <returns></returns>
		private AttackResult MakeAttack(int toHit, int toCrit, int baseDamage)
		{
			List<int> randInts = new List<int>();
			randInts.Add(GetNextRandom());
			randInts.Add(GetNextRandom());

			double twoRands = (randInts[0] + randInts[1]) / 2;

			if(twoRands < toHit)
			{
				//check if we crit.
				randInts.Add(GetNextRandom());
				if (randInts[2] < toCrit)
				{
					//we crit
					return new AttackResult(AttackRollResult.Crit, randInts, baseDamage);
				}
				else
				{
					//just a hit
					return new AttackResult(AttackRollResult.Hit, randInts, baseDamage);
				}
			}
			else
			{
				//miss
				return new AttackResult(AttackRollResult.Miss, randInts, baseDamage);
			}
		}

		/// <summary>
		/// Gets a random number from 0 - 100. Prioritizes manually added 
		/// numbers when available.
		/// </summary>
		/// <returns></returns>
		private int GetNextRandom()
		{
			if(NumberQueue.Count > 0)
			{
				return NumberQueue.Dequeue();
			}

			return myRandom.Next(0, 100);
		}

		private int CalcTerrainAvoidBonus(TerrainType terrain)
		{
			switch(terrain)
			{
				
				case TerrainType.forest:
				case TerrainType.fort:
					return 20;
				case TerrainType.mountain:
					return 30;
				case TerrainType.bridge:
				case TerrainType.plains:
				case TerrainType.impassible:
				case TerrainType.unknown:
				case TerrainType.river:
				default:
					return 0;

			}
		}

		private int CalcTerrainDefBonus(TerrainType terrain)
		{
			switch (terrain)
			{

				case TerrainType.forest:
				case TerrainType.mountain:
					return 1;
				case TerrainType.fort:
					return 2;
				case TerrainType.bridge:
				case TerrainType.plains:
				case TerrainType.impassible:
				case TerrainType.unknown:
				case TerrainType.river:
				default:
					return 0;

			}
		}

		private WeaponTriangle CalculateWeaponTriangle(FE7WeaponType attackerWeapon, FE7WeaponType defenderWeapon)
		{
			if((attackerWeapon == FE7WeaponType.Sword && defenderWeapon == FE7WeaponType.Axe) ||
			   (attackerWeapon == FE7WeaponType.Lance && defenderWeapon == FE7WeaponType.Sword) ||
			   (attackerWeapon == FE7WeaponType.Axe && defenderWeapon == FE7WeaponType.Lance) ||
			   (attackerWeapon == FE7WeaponType.Anima && defenderWeapon == FE7WeaponType.Light) ||
			   (attackerWeapon == FE7WeaponType.Light && defenderWeapon == FE7WeaponType.Dark) ||
			   (attackerWeapon == FE7WeaponType.Dark && defenderWeapon == FE7WeaponType.Anima))
			{
				//reaver weapons would check here
				return WeaponTriangle.advantage;
			}
			else if ((defenderWeapon == FE7WeaponType.Sword && attackerWeapon == FE7WeaponType.Axe) ||
					 (defenderWeapon == FE7WeaponType.Lance && attackerWeapon == FE7WeaponType.Sword) ||
					 (defenderWeapon == FE7WeaponType.Axe && attackerWeapon == FE7WeaponType.Lance) ||
					 (defenderWeapon == FE7WeaponType.Anima && attackerWeapon == FE7WeaponType.Light) ||
					 (defenderWeapon == FE7WeaponType.Light && attackerWeapon == FE7WeaponType.Dark) ||
					 (defenderWeapon == FE7WeaponType.Dark && attackerWeapon == FE7WeaponType.Anima))
			{
				//reaver weapons would check here
				return WeaponTriangle.disadvantage;
			}
			return WeaponTriangle.none;
		}

		public List<GameAction> InitiateCombat(UnitEntity attacker, UnitEntity defender, int range)
		{
			return InitiateCombat((FE7UnitEntity)attacker, (FE7UnitEntity) defender, range);
		}

	}
}
