using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class UnitDeselectedAction : GameAction
	{
		public UnitEntity DeselectedUnit { get; private set; }

		public UnitDeselectedAction(UnitEntity ue) : base (GameActionType.UnitDeselected)
		{
			this.DeselectedUnit = ue;
		}
	}
}