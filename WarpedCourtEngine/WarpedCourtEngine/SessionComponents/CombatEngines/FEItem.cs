using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class FEItem : IWeaponItem
	{
		static List<int> zeroRange = new List<int>(1) { 0 };

		public List<int> Range
		{
			get
			{
				return zeroRange;
			}
		}

		public int MaxUses { get; set; }

		public int Uses { get; set; }
	}
}
