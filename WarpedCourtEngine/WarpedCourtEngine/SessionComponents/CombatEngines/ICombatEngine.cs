using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public interface ICombatEngine
	{
		List<GameAction> InitiateCombat(UnitEntity attacker, UnitEntity defender, int range);
	}
}
