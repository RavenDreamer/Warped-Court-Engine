using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class UnitWaitAction : GameAction
	{
		public UnitEntity WaitingUnit { get; private set; }

		public UnitWaitAction(UnitEntity selectedUnit) : base(GameActionType.UnitWait)
		{
			this.WaitingUnit = selectedUnit;
		}
	}
}