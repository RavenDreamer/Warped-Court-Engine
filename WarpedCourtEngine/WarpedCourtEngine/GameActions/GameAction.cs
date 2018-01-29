using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{

	public enum GameActionType
	{
		UnitMove,
		UnitAttack,
		CombatHit,
		CombatCrit,
		CombatMiss,
		CombatGlance,
		CombatDraw,
		CombatAttackerDefeat,
		CombatDefenderDefeat,
		MenuOpened,
		MenuClosed,
		UnitWait,
		UnitSelected,
		UnitDeselected,
			
	}

	public class GameAction
	{
		public GameActionType ActionType { get; private set; }

		public GameAction(GameActionType type)
		{
			ActionType = type;
		}
	}
}
