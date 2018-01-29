using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public interface IWeaponItem
	{
		List<int> Range { get; }
		int MaxUses { get; }
		int Uses { get; }
	}
}
