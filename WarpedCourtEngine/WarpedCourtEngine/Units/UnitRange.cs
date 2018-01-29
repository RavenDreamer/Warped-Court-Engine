using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class UnitRange
	{
		public HashSet<Position> moveRange { get; set; }
		public HashSet<Position> attackRange { get; set; }
		public HashSet<Position> utilityRange { get; set; }
	}
}
