using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class MenuClosedAction : GameAction
	{
		public MenuData ClosedMenu { get; private set; } 

		public MenuClosedAction(MenuData menuData) : base(GameActionType.MenuClosed)
		{
			this.ClosedMenu = menuData;
		}
	}
}