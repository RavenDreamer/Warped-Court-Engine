using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	/// <summary>
	/// Holds all information pertaining to the unit as it exists on the battle map.
	/// </summary>
	public class UnitEntity
	{
		public string InternalName { get; set; }

		public TerrainType Terrain { get; set; }

		public Position InitalPosition { get; set; }

		public bool HasActed { get; set; }

		public UnitTeam TeamID { get; set; }

		private int baseMove = -1;
		private int _move = -1;
		public int Move
		{
			get
			{
				return _move;
			}
			set
			{
				_move = value;
				if (baseMove == -1) baseMove = value;
			}
		}

		public List<IWeaponItem> WeaponList { get; set; }


		public MovementMap MoveCosts { get; private set; }

		public UnitEntity()
		{
			MoveCosts = new MovementMap();
		}

		/// <summary>
		/// Sets the movement cost of the unit for the given terrain type
		/// </summary>
		/// <param name="terrain">The terrain type to set</param>
		/// <param name="moveCost">The cost in movement points</param>
		public void SetMovementCost(TerrainType terrain, int moveCost)
		{
			if (moveCost < 0)
			{
				MoveCosts.costDict[terrain] = 0;
				return;
			}
			MoveCosts.costDict[terrain] = moveCost;
		}

		/// <summary>
		/// Looks through all equipped weapons to determine all applicable attack ranges (in tiles)
		/// and returns a unique list of possible ranges.
		/// </summary>
		/// <returns></returns>
		public HashSet<Position> AllAttackRanges()
		{
			HashSet<Position> retList = new HashSet<Position>();

			foreach(int i in GetWeaponRanges())
			{
				foreach(Position p in MapRanges.GetRangesExactly(i))
				{
					retList.Add(p);
				}
			}

			return retList;
		}

		private List<int> GetWeaponRanges()
		{
			HashSet<int> ranges = new HashSet<int>();
			foreach(FEWeapon iw in WeaponList)
			{
				foreach(int i in iw.Range)
				{
					ranges.Add(i);
				}
			}

			return ranges.ToList();
		}

		/// <summary>
		/// Looks through all equipped weapons to determine all applicable utility ranges (in tiles)
		/// and returns a unique list of possible ranges.
		/// </summary>
		/// <returns></returns>
		public HashSet<Position> AllUtilityRanges()
		{
			HashSet<Position> retList = new HashSet<Position>();
			HashSet<int> ranges = new HashSet<int>();
			foreach (IWeaponItem iw in WeaponList)
			{
				if (iw is FEWeapon) continue;

				foreach (int i in iw.Range)
				{
					ranges.Add(i);
				}
			}

			foreach (int i in ranges)
			{
				foreach (Position p in MapRanges.GetRangesExactly(i))
				{
					retList.Add(p);
				}
			}

			return retList;
		}

		public void EquipWeapon(IWeaponItem weapon)
		{
			//move weapon to top of list.
			int weapInd = WeaponList.IndexOf(weapon);
			if(weapInd == -1)
			{
				throw new WeaponException("Could not find item within inventory");
			}
			else

			{
				IWeaponItem tempWeap = WeaponList[weapInd];
				WeaponList[weapInd] = WeaponList[0];
				WeaponList[0] = tempWeap;
			}
		}

	}
}
