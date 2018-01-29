using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class AttackResult
	{
		public AttackRollResult Result {get; private set;}
		public List<int> RandNumbersUsed { get; private set; }
		public int BaseDamage { get; private set; }

		public AttackResult (AttackRollResult result, List<int> rngs, int attackDamage)
		{
			Result = result;
			RandNumbersUsed = rngs;
			BaseDamage = attackDamage;
		}
	}
}
