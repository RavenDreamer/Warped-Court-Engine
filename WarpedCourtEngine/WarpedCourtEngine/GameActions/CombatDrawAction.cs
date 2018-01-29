using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class CombatDrawAction : GameAction
	{

		public UnitEntity Attacker { get; private set; }
		public UnitEntity Defender { get; private set; }

		public CombatDrawAction(UnitEntity attacker, UnitEntity defender) : base(GameActionType.CombatDraw)
		{
			this.Attacker = attacker;
			this.Defender = defender;
		}
	}
}