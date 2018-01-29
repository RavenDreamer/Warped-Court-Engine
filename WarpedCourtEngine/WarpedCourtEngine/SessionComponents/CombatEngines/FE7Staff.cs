using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{

	public enum FE7StaffType
	{
		Heal,
		HealMore,
		HealEveryone,
		HealFull,
		Berserk,
		Sleep,
		Torch,
		Cleanse,
		Barrier,
	}


	public class FE7Staff : IWeaponItem
	{
		public List<int> Range { get; set; }
		public int MaxUses { get; set; }
		public int Uses { get; set; }
		public string Name { get; internal set; }
		public FE7WeaponRank WeaponRank { get; internal set; }
		public FE7StaffType WeaponType { get; internal set; }
		public string Text { get; internal set; }
	}
}
