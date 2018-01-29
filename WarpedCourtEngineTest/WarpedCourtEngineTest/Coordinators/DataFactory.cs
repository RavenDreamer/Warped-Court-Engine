using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine;

namespace WarpedCourtEngineTest
{
	class DataFactory
	{
		public static GameMap Get5x6Map()
		{
			TerrainType[] mapTerrain = new TerrainType[30];

			for(int i = 0; i < 30; i++)
			{
				mapTerrain[i] = TerrainType.plains;
			}

			return new GameMap(5, 6, mapTerrain);
		}

		public static GameMap GetLausMap()
		{
			//Approx move cost in squares 9 == impassible
			// Forests =/= Beaches and forts, etc. not represented this is inverted, unfortunately, so it's only for really awkward reference.
			//4,4,4,4,4,4,4,2,9,9,9,1,1,1,1,1,1,3,2,1,1,1,1,2,1,1,1,1,
			//4,4,4,4,4,2,2,1,9,9,9,1,1,1,1,1,1,1,1,2,1,1,1,H,1,1,1,1,
			//4,4,2,4,4,2,1,1,9,1,9,1,1,9,1,1,1,3,1,1,1,1,9,9,9,9,1,1,
			//2,1,2,1,1,1,2,1,1,1,1,1,9,1,1,1,1,3,1,1,1,1,1,1,1,1,1,1,
			//9,9,9,1,1,1,1,9,9,9,9,9,2,1,1,1,1,3,3,3,3,1,1,1,1,1,1,1,
			//9,9,9,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
			//9,1,9,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,3,1,1,1,1,1,1,1,
			//1,1,1,1,1,1,1,2,1,1,2,2,1,1,1,1,2,1,1,1,3,1,1,9,2,1,1,1,
			//1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,3,9,9,4,4,4,4,9,
			//1,1,1,1,1,1,1,1,1,1,2,1,1,1,2,2,1,1,9,9,4,4,4,4,4,4,4,4,
			//1,1,1,1,1,1,1,1,9,1,1,1,2,1,1,1,1,1,2,4,4,4,4,4,4,4,4,4,
			//9,1,1,1,2,1,1,1,9,9,1,1,1,1,1,1,2,2,2,4,4,4,4,4,4,4,4,4,
			//4,9,9,1,2,9,9,9,1,1,9,1,1,1,1,2,2,2,4,4,4,4,4,4,2,2,4,4,
			//4,4,9,2,1,9,9,9,1,1,1,1,1,1,9,4,4,4,4,4,4,2,1,2,9,9,4,2,
			//4,4,9,1,1,9,1,9,1,1,1,1,1,1,9,4,4,4,4,4,2,1,1,1,2,4,9,1,
			//4,4,4,9,1,1,1,1,1,1,1,1,1,9,4,4,4,2,2,4,2,1,1,1,2,4,9,1,
			//4,4,4,9,1,1,1,1,1,1,1,1,9,4,4,4,2,1,1,2,4,4,4,4,4,4,9,1,

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
			return new GameMap(28, 17, mapTerrain);
		}

		internal static UnitEntity[] GetFE7EngineWeaponTestUnits()
		{

			UnitEntity[] testUnits = new UnitEntity[4] {
				new FE7UnitEntity(){
					InternalName = "Swordman",
					InitalPosition = new Position(0, 0),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{1}),
							Weight = 0,
							Might = 1,
							Hit = 85,
							Crit = 0,
							WeaponType = FE7WeaponType.Sword,
							Name = "Test Sword",
						}
					})
				},
				new FE7UnitEntity(){
					InternalName = "Axeguy",
					InitalPosition = new Position(0, 0),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{1}),
							Weight = 0,
							Might = 1,
							Hit = 85,
							Crit = 0,
							WeaponType = FE7WeaponType.Axe,
							Name = "Test Axe",
						}
					})
				},
				new FE7UnitEntity(){
					InternalName = "Lancedude",
					InitalPosition = new Position(0, 0),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{1}),
							Weight = 0,
							Might = 1,
							Hit = 85,
							Crit = 0,
							WeaponType = FE7WeaponType.Lance,
							Name = "Test Lance",
						}
					})
				},
				new FE7UnitEntity(){
					InternalName = "Bowperson",
					InitalPosition = new Position(0, 0),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{2}),
							Weight = 0,
							Might = 1,
							Hit = 85,
							Crit = 0,
							WeaponType = FE7WeaponType.Bow,
							Name = "Test Bow",
						}
					})
				}
			};

			return testUnits;
		}

			internal static UnitBook GetLausUnits()
		{
			UnitBook ub = new UnitBook();

			//Enemy Units
			UnitEntity[] enemyUnits = new UnitEntity[4] {
				new FE7UnitEntity(){
					InternalName = "Erik",
					InitalPosition = new Position(1, 10),
					MaxHP = 30,
					Constitution = 12,
					Move = 7,
				},
				new FE7UnitEntity(){
					InternalName = "Cavalier1",
					InitalPosition = new Position(2, 9),
					MaxHP = 20,
					Constitution = 11,
					Move = 7,
				},
				new FE7UnitEntity(){
					InternalName = "Cavalier2",
					InitalPosition = new Position(2, 8),
					MaxHP = 20,
					Constitution = 11,
					Move = 7,
				},
				new FE7UnitEntity(){
					InternalName = "EasternMerc",
					InitalPosition = new Position(20, 16),
					MaxHP = 10,
					Constitution = 9,
					Move = 5,
				}
			};

			UnitRoster enemyRoster = new UnitRoster(enemyUnits, UnitTeam.team2); ;

			//Player Units
			UnitEntity[] playerUnits = new UnitEntity[3] {
				new FE7UnitEntity(){
					InternalName = "Hector",
					InitalPosition = new Position(20, 13),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{1}),
							Weight = 10,
							Might = 10,
							Hit = 75,
							Crit = 5,
							WeaponType = FE7WeaponType.Axe,
							Name = "Wolf Beil",
						}
					})
				},
				new FE7UnitEntity(){
					InternalName = "Eliwood",
					InitalPosition = new Position(20, 14),
					Move = 5,
					MaxHP = 20,
					Constitution = 12,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[1]{1}),
							Weight = 5,
							Might = 7,
							Hit = 95,
							Crit = 10,
							WeaponType = FE7WeaponType.Sword,
							Name = "Rapier",
						}
					})
				},
				new FE7UnitEntity(){
					InternalName = "Serra",
					InitalPosition = new Position(23, 11),
					Move = 5,
					MaxHP = 15,
					Constitution = 7,
				}};

			UnitRoster playerRoster = new UnitRoster(playerUnits, UnitTeam.team1);

			//team Erk
			UnitEntity[] erkUnits = new UnitEntity[1] {
				new FE7UnitEntity(){
					InternalName = "Erk",
					InitalPosition = new Position(14, 15),
					Move = 7
				}
			};

			UnitRoster erkRoster = new UnitRoster(erkUnits, UnitTeam.team3);

			ub.AddRoster(enemyRoster, TeamRelation.enemy);
			ub.AddRoster(playerRoster, TeamRelation.player);
			ub.AddRoster(erkRoster, TeamRelation.ally);

			return ub;
		}

		internal static UnitEntity GetUnitForTeam(UnitTeam team)
		{
			UnitEntity ue = new UnitEntity();
			ue.TeamID = team;
			return ue;
		}
	}
}
