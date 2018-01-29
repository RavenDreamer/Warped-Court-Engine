using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	class TileQueryResponse
	{
		public TerrainType tile { get; set; }
		public UnitEntity unit { get; set; }
		public List<PathNode> pathTo { get; set; }
		//public TileEntity object {get; set; }

	}
}
