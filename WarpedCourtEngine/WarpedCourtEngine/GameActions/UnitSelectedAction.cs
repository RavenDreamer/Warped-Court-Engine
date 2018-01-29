using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class UnitSelectedAction : GameAction
	{
		public UnitEntity SelectedUnit { get; private set; }

		public UnitSelectedAction(UnitEntity selectedUnit): base(GameActionType.UnitSelected)
		{
			this.SelectedUnit = selectedUnit;
		}
	}
}