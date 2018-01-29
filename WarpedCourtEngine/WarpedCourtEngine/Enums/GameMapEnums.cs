using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public enum TerrainType
	{
		unknown,
		cliff,
		impassible,
		plains,
		forest,
		peak,
		mountain,
		village,
		outsideWall,
		house,
		armory,
		vendor,
		gate,
		sand,
		sea,
		bridge,
		fort,
		river,
	}

	public enum MapSelectionResponse
	{
		emptyTile,
		unitTile,
		featureTile, //e.g. Dragonveins
	}


}
