using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class CombatAttackerDefeatAction : GameAction
	{

		public UnitEntity Attacker { get; private set; }
		public UnitEntity Defender { get; private set; }

		public CombatAttackerDefeatAction(UnitEntity attacker, UnitEntity defender) : base(GameActionType.CombatAttackerDefeat)
		{
			this.Attacker = attacker;
			this.Defender = defender;
		}
	}
}