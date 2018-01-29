using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class MenuData
	{
		public MenuTypes MenuType { get; set; }
		public List<string> Actions { get; set; }

		public MenuData()
		{
			Actions = new List<string>();
		}

		public MenuData(List<string> actions)
		{
			Actions = new List<string>(actions);
		}

	}

	public class AttackDetailsMenuData : MenuData
	{
		public List<UnitEntity> Targets { get; set; }
		public List<IWeaponItem> Weapons { get; set; } 
		public Position EngagementSquare { get; set; }

		public AttackDetailsMenuData(List<string> actions)
		{
			Actions = new List<string>(actions);
		}

		public AttackDetailsMenuData()
		{
			Actions = new List<string>();
		}
	}
}
