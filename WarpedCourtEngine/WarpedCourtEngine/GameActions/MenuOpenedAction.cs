using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class MenuOpenedAction : GameAction
	{
		public MenuData OpenedMenu { get; private set; }

		public MenuOpenedAction(MenuData openedMenu) : base(GameActionType.MenuOpened)
		{
			this.OpenedMenu = openedMenu;
		}
	}
}
