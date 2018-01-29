using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class CombatMissAction : GameAction
	{
		public List<int> RandNumbersUsed { get; private set; }
		public UnitEntity AttackingUnit { get; private set; }

		public CombatMissAction(UnitEntity attackingUnit, List<int> randNumbersUsed) : base(GameActionType.CombatMiss)
		{
			this.RandNumbersUsed = randNumbersUsed;
			this.AttackingUnit = attackingUnit;
		}
	}
}