using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public enum SelectionState
	{
		NoSelection, //no unit selected
		UnitSelected, //unit selected
		MenuOpen, //a gameMenu is open
	}

	public enum UnitActionCommand
	{
		Wait,
		Attack,
		Trade,
		Staff,
		Seize,
	}

	public enum MapActionCommand
	{
		Units,
		Save,
		End,
	}

	public enum MenuTypes
	{
		Map,
		Unit,
		AttackDetails,
		Staff,
		Trade,
	}

	public enum CombatEngine
	{
		GBA,
		ThreeDS,
		WarpedCourt,
	}

	public enum AttackOutcome
	{
		AttackerDefeated,
		DefenderDefeated,
		Draw,
		AttackerWounded,
		DefenderWounded,
	}

	public enum AttackRollResult
	{
		Hit,
		Miss,
		Crit,
		Glance,
	}
}
