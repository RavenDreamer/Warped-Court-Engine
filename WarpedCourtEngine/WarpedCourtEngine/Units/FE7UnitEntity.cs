using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public enum FE7Class
	{
		Unknown,
		LordEliwood,
		Brigand,
		Cavalier,
		Pirate,
		Dancer,
		Mercenary,
		Mage,
		Cleric,
		Knight,
		LordHector,
		Bard,
		Archer,
		Thief,
		Paladin,
		Fighter,
		Berserker,
		Warrior,
		Myrmidon,
		Swordmaster,
	}

	public enum FE7Affinity
	{
		Anima,
		Darkness,
		Light,
		Thunder,
		Fire,
		Wind,
		Frost,
	}

	public enum FE7ClassGroup
	{
		Infantry,
		Armored,
		Mounted,
		Flier,
		Dragon,
	}


	public class FE7UnitEntity : UnitEntity
	{

		private static FE7Weapon noWeaponWeapon;

		static FE7UnitEntity()
		{
			noWeaponWeapon = new FE7Weapon()
			{
				Range = new List<int>(new int[1] { 0 }),
				Weight = 0,
				Might = 0,
				Hit = 0,
				Crit = 0,
				WeaponType = FE7WeaponType.None,
				Name = "--",
			};
		}




		public FE7Weapon EquippedWeapon
		{
			get
			{
				if(WeaponList == null || WeaponList.Count == 0)
				{
					return noWeaponWeapon;
				}
				return WeaponList[0] as FE7Weapon;
			}
		}

		private int _MaxHP = -1;
		public int MaxHP
		{
			get
			{
				return _MaxHP;
			}
			set
			{
				_MaxHP = value;
				if (HP == 0)
				{
					HP = value;
				}
			}
		}
		public int HP { get; set; }
		public int Constitution { get; set; }
		public int Speed { get; set; }
		public int Skill { get; set; }
		public int Luck { get; set; }
		public int Resistance { get; set; }
		public int Defense { get; set; }
		public int StrengthMagic { get; set; }


		/// <summary>
		///  Speed – (Weapon Weight – Constitution, take as 0 if negative) [Minimum is 0]
		/// </summary>
		public int AttackSpeed
		{
			get
			{
				int weightPenalty = (this.EquippedWeapon.Weight - this.Constitution);
				if (weightPenalty < 0) weightPenalty = 0;

				return this.Speed - weightPenalty;
			}
		}

		/// <summary>
		/// Weapon Hit + (Skill x 2) + (Luck / 2) + [These can't be pre-calculated] Support bonus + Weapon Triangle bonus + S Rank bonus + Tactician bonus
		/// </summary>
		public int HitChance
		{
			get
			{
				return EquippedWeapon.Hit + (2 * this.Skill) + (int)(.5 * this.Luck);
			}
		}


		/// <summary>
		/// (Attack Speed x 2) + Luck + [These can't be pre-calculated]Support bonus + Terrain bonus + Tactician bonus
		/// </summary>
		public int Avoid
		{
			get
			{
				return (2 * AttackSpeed) + this.Luck;
			}

		}

		/// <summary>
		/// Weapon Critical + (Skill / 2) [Not Precalculated: + Support bonus + Critical bonus + S Rank bonus - Crit Avoid [opponent's Luck + Support + Tactician Bonus] ]
		/// </summary>
		public int CritChance
		{
			get
			{
				return (int) ( EquippedWeapon.Crit + (.5 * this.Skill));
			}
		}

		/// <summary>
		/// StrengthMagic + Weapon Might + [ Weapon Triangle bonus x Effective coefficient] + Support bonus
		/// </summary>
		public int Might

		{
			get
			{
				return this.StrengthMagic + this.EquippedWeapon.Might;
			}
		}

		public FE7Class Class { get; internal set; }
		public int Level { get; internal set; }
		public int Exp { get; internal set; }
		public FE7Affinity Affinity { get; internal set; }
		public Dictionary<FE7WeaponType, FE7WeaponRank> WeaponRanks { get; internal set; }
		public Dictionary<string, FE7WeaponRank> SupportRanks { get; internal set; }
		public Dictionary<FE7WeaponType, int> WeaponRankXP { get; internal set; }
		public Dictionary<string, int> SupportRankXP { get; internal set; }

		internal void TakeDamage(GameAction attackerResult)
		{
			switch(attackerResult.ActionType)
			{
				case GameActionType.CombatCrit:
					this.HP -= (attackerResult as CombatCritAction).AttackerDamage;
					return;
				case GameActionType.CombatHit:
					this.HP -= (attackerResult as CombatHitAction).AttackerDamage;
					return;
				default:
					break;
			}
		}
	}
}
