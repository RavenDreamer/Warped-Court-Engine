using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class CombatCritAction : GameAction
	{
		public int AttackerDamage { get; private set; }
		public List<int> RandNumbersUsed { get; private set; }
		public UnitEntity AttackingUnit { get; private set; }

		public CombatCritAction(UnitEntity attacker, int attackerDamage, List<int> randNumbersUsed) : base(GameActionType.CombatCrit)
		{
			this.AttackingUnit = attacker;
			this.AttackerDamage = attackerDamage;
			this.RandNumbersUsed = randNumbersUsed;
		}
	}
}