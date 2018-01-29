using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	/// <summary>
	/// A roster contains all units for a given team on the map. It can return units
	/// by name, or any other needed criteria.
	/// </summary>
	public class UnitRoster
	{
		private HashSet<UnitEntity> units;

		public UnitTeam TeamID { get; private set; }

		public UnitRoster(UnitEntity[] unit, UnitTeam teamID)
		{
			TeamID = teamID;

			this.units = new HashSet<UnitEntity>();

			if (unit == null)
			{
				return;
			}

			foreach (UnitEntity mu in unit)
			{
				AddUnit(mu);
			}
		}


		/// <summary>
		///  dds the given unit to this Roster
		/// </summary>
		/// <param name="unit"></param>
		/// <returns>True if unit was added, false otherwise</returns>
		public bool AddUnit(UnitEntity unit)
		{
			if (unit.TeamID != TeamID)
			{
				unit.TeamID = this.TeamID;
				return this.units.Add(unit);
			}
			return false;
		}

		/// <summary>
		/// returns a reference to any unit at the given location, or null otherwise.
		/// </summary>
		/// <param name="x">x position of the unit</param>
		/// <param name="y">y position of the unit</param>
		/// <returns></returns>
		//public UnitEntity GetUnitAt(Position pos)
		//{
		//	return units.FirstOrDefault(s => s == x && s.yPos == y);
		//}

		/// <summary>
		/// returns a reference to any unit by the given name, or null otherwise
		/// </summary>
		/// <param name="name">internal name of the unit</param>
		/// <returns></returns>
		public UnitEntity GetUnitByName(string name)
		{
			return units.FirstOrDefault(s => s.InternalName.Equals(name));
		}

		/// <summary>
		/// returns a reference to all units present in the roster
		/// </summary>
		/// <returns></returns>
		public List<UnitEntity> GetAllUnits()
		{
			return new List<UnitEntity>(units);
		}

	}
}
