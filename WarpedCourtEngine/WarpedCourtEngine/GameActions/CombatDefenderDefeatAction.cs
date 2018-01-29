using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class CombatDefenderDefeatAction : GameAction
	{

		public UnitEntity Attacker { get; private set; }
		public UnitEntity Defender { get; private set; }

		public CombatDefenderDefeatAction(UnitEntity attacker, UnitEntity defender) : base(GameActionType.CombatDefenderDefeat)
		{
			this.Attacker = attacker;
			this.Defender = defender;
		}
	}
}