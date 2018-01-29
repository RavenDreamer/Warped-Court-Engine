using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngine
{
	/// <summary>
	/// Contains all units in play (whether currently deployed or not) by virtue of maintaining
	/// a list of rosters (which store data about the units potentially on the map, per team).
	/// </summary>
	public class UnitBook
	{
		//keeps track of which team is which
		private UnitTeam playerTeam;
		private HashSet<UnitTeam> enemyTeams = new HashSet<UnitTeam>();
		private HashSet<UnitTeam> otherTeams = new HashSet<UnitTeam>();

		//all units in the book
		private Dictionary<UnitTeam, UnitRoster> unitLists = new Dictionary<UnitTeam, UnitRoster>();

		public int RosterCount
		{
			get
			{
				return unitLists.Count;
			}
		}

		public List<UnitRoster> GetAllUnits()
		{
			return unitLists.Values.ToList();
		}

		/// <summary>
		/// Instantiates a new UnitBook and sets the team of the player (defaults to team1)
		/// </summary>
		/// <param name="player"></param>
		public UnitBook(UnitTeam player = UnitTeam.team1)
		{
			this.playerTeam = player;
		}


		/// <summary>
		/// Adds a UnitRoster to the UnitBook and their relation to the playerTeam
		/// </summary>
		/// <param name="roster"></param>
		/// <param name="relation"></param>
		/// <returns>True - the roster was added. False - the roster was not added</returns>
		public bool AddRoster(UnitRoster roster, TeamRelation relation)
		{
			if (unitLists.ContainsKey(roster.TeamID)) return false; //already exists, can't add
			switch (relation)
			{
				case TeamRelation.enemy:
					enemyTeams.Add(roster.TeamID);
					break;
				case TeamRelation.ally:
					otherTeams.Add(roster.TeamID);
					break;
			}

			unitLists.Add(roster.TeamID, roster);
			return true;
		}

		internal UnitRoster GetRoster(UnitTeam team)
		{
			return unitLists[team];
		}

		/// <summary>
		/// Sets the unit's turn to be complete. 
		/// </summary>
		/// <param name="unit"></param>
		/// <returns>True if all units on that roster are now inactive, false otherwise</returns>
		//public bool RosterTurnComplete(UnitTeam team)
		//{
		//	if (unitLists[team].GetAllUnits().FindIndex(s => s.hasActed == true) != -1)
		//	{
		//		return true;
		//	}
		//	return false;
		//}

		/// <summary>
		/// Sets all units to be active once again
		/// </summary>
		/// <param name="ut"></param>
		//public void SetRosterActive(UnitTeam ut)
		//{
		//	foreach (UnitEntity u in unitLists[ut].GetAllUnits())
		//	{
		//		u.hasActed = true;
		//	}
		//}

	}
}
