using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class FEWeapon : IWeaponItem
	{
		public List<int> Range { get; set; }
		public int MaxUses { get; set; }
		public int Uses { get; set; }
	}
}
