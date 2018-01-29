using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public enum FE7WeaponType
	{
		None,
		Unknown,
		Sword,
		Axe,
		Lance,
		Anima,
		Light,
		Dark,
		Bow,
		Staff,
	}

	public enum FE7WeaponRank
	{
		A,
		B,
		C,
		D,
		E,
		PrfEliwood,
		PrfHector,
		PrfLyn,
	}

	public enum WeaponTriangle
	{
		doubleAdvantage = 2,
		advantage = 1,
		doubleDisadvantage = -2,
		disadvantage = -1,
		none = 0,
	}


	public class FE7Weapon : FEWeapon
	{
		public int Might { get; set; }
		public int Weight { get; set; }
		public int Hit { get; set; }
		public int Crit { get; set; }
		public FE7WeaponType WeaponType { get; set; }
		public string Name { get; set; }
		public FE7WeaponRank WeaponRank { get; internal set; }
		public List<FE7ClassGroup> Effective { get; internal set; }
		public string Text { get; internal set; }

		public bool IsMagic
		{
			get
			{
				switch(WeaponType)
				{
					case FE7WeaponType.Axe:
					case FE7WeaponType.Bow:
					case FE7WeaponType.Lance:
					case FE7WeaponType.Sword:
					case FE7WeaponType.None:
						return false;
					case FE7WeaponType.Anima:
					case FE7WeaponType.Dark:
					case FE7WeaponType.Light:
						return true;
					default:
						throw new WeaponException("can't tell if magic");
				}
			}
		}

		

	}
}
