using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine.LevelData
{
	class Chapter14
	{
		public static UnitEntity[] GetPlayerArmy()
		{
			//Player Units
			UnitEntity[] playerUnits = new UnitEntity[11] {
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Eliwood",
					Class = FE7Class.LordEliwood,
					Level = 5,
					Exp = 93,
					MaxHP = 21,
					StrengthMagic = 9,
					Skill = 7,
					Speed = 9,
					Luck = 9,
					Defense = 5,
					Resistance = 1,
					Move = 5,
					Constitution = 7,
					Affinity = FE7Affinity.Anima,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){ {FE7WeaponType.Sword, FE7WeaponRank.B } },
					WeaponRankXP = new Dictionary<FE7WeaponType, int>(){ {FE7WeaponType.Sword, 20 } },
					SupportRanks = new Dictionary<string, FE7WeaponRank>(){ { "Hector", FE7WeaponRank.C } },
					SupportRankXP = new Dictionary<string, int>(){ {"Hector", 4 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[3]
					{
						new FE7Weapon(){
							Name = "Rapier",
							WeaponType = FE7WeaponType.Sword,
							WeaponRank = FE7WeaponRank.PrfEliwood,
							Range = new List<int>(new int[1]{1}),
							Weight = 5,
							Might = 7,
							Hit = 95,
							Crit = 10,
							Effective = new List<FE7ClassGroup>(new FE7ClassGroup[1]{FE7ClassGroup.Infantry}),
							Text = "Effective against infantry.",
							Uses = 16,
							MaxUses = 40,
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						},
						new FE7Item(){
							Name = "Mine",
							ItemType = FE7ItemType.Mine,
							Text = "A trap that damages units that setp on it.",
							MaxUses = 1,
							Uses = 1,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Marcus",
					Class = FE7Class.Paladin,
					Level = 1,
					Exp = 30,
					MaxHP = 31,
					StrengthMagic = 15,
					Skill = 15,
					Speed = 11,
					Luck = 8,
					Defense = 10,
					Resistance = 8,
					Move = 8,
					Constitution = 11,
					Affinity = FE7Affinity.Frost,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Sword, FE7WeaponRank.A },
						{ FE7WeaponType.Axe, FE7WeaponRank.B },
						{ FE7WeaponType.Lance, FE7WeaponRank.A } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Sword, 20 },
						{ FE7WeaponType.Axe, 0 },
						{ FE7WeaponType.Lance, 0 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Steel sword",
							WeaponType = FE7WeaponType.Sword,
							WeaponRank = FE7WeaponRank.D,
							Range = new List<int>(new int[1]{1}),
							Weight = 10,
							Might = 8,
							Hit = 75,
							Crit = 0,
							Uses = 21,
							MaxUses = 30,
						},
						new FE7Item(){
							Name = "Torch",
							ItemType = FE7ItemType.Torch,
							Text = "A staff with burning pitch. Grows dimmer each turn.",
							MaxUses = 1,
							Uses = 1,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Lowen",
					Class = FE7Class.Cavalier,
					Level = 8,
					Exp = 27,
					MaxHP = 28,
					StrengthMagic = 9,
					Skill = 7,
					Speed = 9,
					Luck = 5,
					Defense = 8,
					Resistance = 3,
					Move = 7,
					Constitution = 10,
					Affinity = FE7Affinity.Fire,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Sword, FE7WeaponRank.C },
						{ FE7WeaponType.Lance, FE7WeaponRank.D } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Sword, 0 },
						{ FE7WeaponType.Lance, 70 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[4]
					{
						new FE7Weapon(){
							Name = "Iron sword",
							WeaponType = FE7WeaponType.Sword,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{1}),
							Weight = 5,
							Might = 5,
							Hit = 90,
							Crit = 0,
							Uses = 17,
							MaxUses = 46,
						},
						new FE7Weapon(){
							Name = "Steel lance",
							WeaponType = FE7WeaponType.Lance,
							WeaponRank = FE7WeaponRank.D,
							Range = new List<int>(new int[1]{1}),
							Weight = 13,
							Might = 10,
							Hit = 70,
							Crit = 0,
							Uses = 22,
							MaxUses = 30,
						},
						new FE7Weapon(){
							Name = "Javelin",
							WeaponType = FE7WeaponType.Lance,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[2]{1,2}),
							Weight = 11,
							Might = 6,
							Hit = 65,
							Crit = 0,
							Text = "Doubles as ranged attack",
							Uses = 18,
							MaxUses = 20,
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Rebecca",
					Class = FE7Class.Archer,
					Level = 1,
					Exp = 26,
					MaxHP = 17,
					StrengthMagic = 4,
					Skill = 5,
					Speed = 6,
					Luck = 4,
					Defense = 3,
					Resistance = 1,
					Move = 5,
					Constitution = 5,
					Affinity = FE7Affinity.Fire,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Bow, FE7WeaponRank.D } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Bow, 2 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Iron bow",
							WeaponType = FE7WeaponType.Bow,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{2}),
							Weight = 5,
							Might = 6,
							Hit = 85,
							Crit = 0,
							Uses = 43,
							MaxUses = 45,
						},
						new FE7Item(){
							Name = "Secret book",
							ItemType = FE7ItemType.SkillIncrease,
							Text = "Increases Skill by 2 points. Vanishes after use.",
							MaxUses = 1,
							Uses = 1,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Bartre",
					Class = FE7Class.Fighter,
					Level = 5,
					Exp = 77,
					MaxHP = 32,
					StrengthMagic = 11,
					Skill = 7,
					Speed = 3,
					Luck = 6,
					Defense = 5,
					Resistance = 1,
					Move = 5,
					Constitution = 13,
					Affinity = FE7Affinity.Thunder,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Axe, FE7WeaponRank.D } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Axe, 55 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[3]
					{
						new FE7Weapon(){
							Name = "Iron axe",
							WeaponType = FE7WeaponType.Axe,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{1}),
							Weight = 10,
							Might = 8,
							Hit = 75,
							Crit = 0,
							Uses = 30,
							MaxUses = 45,
						},
						new FE7Weapon(){
							Name = "Hand axe",
							WeaponType = FE7WeaponType.Axe,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[2]{1,2}),
							Weight = 12,
							Might = 7,
							Hit = 60,
							Crit = 0,
							Uses = 17,
							MaxUses = 20,
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Hector",
					Class = FE7Class.LordHector,
					Level = 6,
					Exp = 6,
					MaxHP = 23,
					StrengthMagic = 10,
					Skill = 8,
					Speed = 6,
					Luck = 4,
					Defense = 11,
					Resistance = 1,
					Move = 5,
					Constitution = 13,
					Affinity = FE7Affinity.Thunder,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Axe, FE7WeaponRank.C } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Axe, 60 } },
					SupportRanks = new Dictionary<string, FE7WeaponRank>(){ { "Eliwood", FE7WeaponRank.C } },
					SupportRankXP = new Dictionary<string, int>(){ {"Eliwood", 4 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Wolf Beil",
							WeaponType = FE7WeaponType.Axe,
							WeaponRank = FE7WeaponRank.PrfHector,
							Range = new List<int>(new int[1]{1}),
							Weight = 10,
							Might = 10,
							Hit = 75,
							Crit = 5,
							Uses = 14,
							MaxUses = 30,
							Effective = new List<FE7ClassGroup>(new FE7ClassGroup[1]{FE7ClassGroup.Infantry}),
							Text = "Effective against infantry",
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Oswin",
					Class = FE7Class.Knight,
					Level = 9,
					Exp = 33,
					MaxHP = 28,
					StrengthMagic = 13,
					Skill = 9,
					Speed = 5,
					Luck = 3,
					Defense = 13,
					Resistance = 3,
					Move = 4,
					Constitution = 14,
					Affinity = FE7Affinity.Anima,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Lance, FE7WeaponRank.B } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Lance, 4 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Iron lance",
							WeaponType = FE7WeaponType.Axe,
							WeaponRank = FE7WeaponRank.PrfHector,
							Range = new List<int>(new int[1]{1}),
							Weight = 8,
							Might = 7,
							Hit = 80,
							Crit = 0,
							Uses = 43,
							MaxUses = 45,
						},
						new FE7Weapon(){
							Name = "Javelin",
							WeaponType = FE7WeaponType.Lance,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[2]{1,2}),
							Weight = 11,
							Might = 6,
							Hit = 65,
							Crit = 0,
							Text = "Doubles as ranged attack",
							Uses = 20,
							MaxUses = 20,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Guy",
					Class = FE7Class.Myrmidon,
					Level = 3,
					Exp = 0,
					MaxHP = 21,
					StrengthMagic = 6,
					Skill = 11,
					Speed = 11,
					Luck = 5,
					Defense = 5,
					Resistance = 0,
					Move = 5,
					Constitution = 5,
					Affinity = FE7Affinity.Fire,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Sword, FE7WeaponRank.C } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Sword, 0 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Killing edge",
							WeaponType = FE7WeaponType.Sword,
							WeaponRank = FE7WeaponRank.C,
							Range = new List<int>(new int[1]{1}),
							Weight = 7,
							Might = 9,
							Hit = 75,
							Crit = 30,
							Uses = 20,
							MaxUses = 20,
							Text = "Improves critical hit rate."
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Dorcas",
					Class = FE7Class.Fighter,
					Level = 6,
					Exp = 22,
					MaxHP = 32,
					StrengthMagic = 8,
					Skill = 7,
					Speed = 6,
					Luck = 5,
					Defense = 4,
					Resistance = 0,
					Move = 5,
					Constitution = 14,
					Affinity = FE7Affinity.Fire,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Axe, FE7WeaponRank.C } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Axe, 80 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Steel axe",
							WeaponType = FE7WeaponType.Axe,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{1}),
							Weight = 15,
							Might = 11,
							Hit = 65,
							Crit = 0,
							Uses = 30,
							MaxUses = 30,
						},
						new FE7Item(){
							Name = "Dragonshield",
							ItemType = FE7ItemType.DefIncrease,
							Text = "Increases defense by 2 points. Vanishes after use.",
							MaxUses = 1,
							Uses = 1,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Matthew",
					Class = FE7Class.Thief,
					Level = 7,
					Exp = 53,
					MaxHP = 22,
					StrengthMagic = 6,
					Skill = 5,
					Speed = 15,
					Luck = 4,
					Defense = 5,
					Resistance = 1,
					Move = 6,
					Constitution = 7,
					Affinity = FE7Affinity.Wind,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Sword, FE7WeaponRank.C } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Sword, 10 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Weapon(){
							Name = "Iron sword",
							WeaponType = FE7WeaponType.Sword,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{1}),
							Weight = 5,
							Might = 5,
							Hit = 90,
							Crit = 0,
							Uses = 22,
							MaxUses = 46,
						},
						new FE7Item(){
							Name = "Lockpick",
							ItemType = FE7ItemType.Lockpick,
							Text = "Opens doors and chests. Usable only by thieves",
							MaxUses = 15,
							Uses = 15,
						}
					})
				},
				new FE7UnitEntity(){
					InitalPosition = new Position(0, 0),

					InternalName = "Serra",
					Class = FE7Class.Cleric,
					Level = 3,
					Exp = 67,
					MaxHP = 18,
					StrengthMagic = 2,
					Skill = 6,
					Speed = 8,
					Luck = 6,
					Defense = 2,
					Resistance = 5,
					Move = 5,
					Constitution = 4,
					Affinity = FE7Affinity.Thunder,
					WeaponRanks = new Dictionary<FE7WeaponType,FE7WeaponRank>(){
						{ FE7WeaponType.Staff, FE7WeaponRank.C } },
					WeaponRankXP = new Dictionary<FE7WeaponType,int>(){
						{ FE7WeaponType.Staff, 10 } },

					WeaponList = new List<IWeaponItem>(new IWeaponItem[2]
					{
						new FE7Staff(){
							Name = "Heal",
							WeaponType = FE7StaffType.Heal,
							WeaponRank = FE7WeaponRank.E,
							Range = new List<int>(new int[1]{1}),
							Uses = 21,
							MaxUses = 30,
							Text = "Restores HP to allies in adjacent spaces."
						},
						new FE7Item(){
							Name = "Vulnerary",
							ItemType = FE7ItemType.HealUse,
							Text = "A medicinal solution used for healing minor wounds.",
							MaxUses = 3,
							Uses = 3,
						}
					})
				}};

			return playerUnits;
		}

		public static TerrainType[] GetMapData()
		{

			TerrainType[] mapTerrain = new TerrainType[476]
			{
				TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sand,TerrainType.fort,TerrainType.plains,TerrainType.sand,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.cliff,TerrainType.plains,
				TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sand,TerrainType.sand,TerrainType.sea,TerrainType.sand,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.sand,TerrainType.sea,TerrainType.cliff,TerrainType.plains,
				TerrainType.sea,TerrainType.sea,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.outsideWall,TerrainType.village,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sand,TerrainType.plains,TerrainType.fort,TerrainType.plains,TerrainType.sand,TerrainType.sea,TerrainType.cliff,TerrainType.fort,
				TerrainType.sea,TerrainType.sea,TerrainType.cliff,TerrainType.forest,TerrainType.plains,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.vendor,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sand,TerrainType.plains,TerrainType.sand,TerrainType.cliff,TerrainType.cliff,TerrainType.sea,TerrainType.sand,
				TerrainType.sea,TerrainType.cliff,TerrainType.cliff,TerrainType.plains,TerrainType.forest,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.armory,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.sand,TerrainType.sand,TerrainType.sand,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sand,TerrainType.sand,TerrainType.sea,TerrainType.sea,
				TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.sand,TerrainType.sand,TerrainType.sand,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,
				TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.sand,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,
				TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,
				TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.cliff,TerrainType.cliff,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.sea,TerrainType.cliff,
				TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.sand,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.outsideWall,TerrainType.gate,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.bridge,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.cliff,TerrainType.cliff,TerrainType.cliff,TerrainType.cliff,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.river,TerrainType.river,TerrainType.river,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.forest,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.house,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.peak,TerrainType.peak,TerrainType.mountain,TerrainType.peak,TerrainType.peak,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.outsideWall,TerrainType.village,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.cliff,TerrainType.cliff,TerrainType.cliff,TerrainType.cliff,TerrainType.plains,TerrainType.plains,
				TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.forest,TerrainType.forest,TerrainType.plains,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.bridge,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.house,TerrainType.house,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,
				TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.peak,TerrainType.forest,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.outsideWall,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.river,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.forest,TerrainType.plains,TerrainType.plains,TerrainType.plains,TerrainType.plains
			};

			return mapTerrain;
		}
	}
}
