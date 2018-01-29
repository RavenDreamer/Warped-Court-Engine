using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	/// <summary>
	/// Contains a list of terrain types and the cost (in ADDITIONAL movement tiles) to enter them.
	/// </summary>
	public class MovementMap
	{

		public Dictionary<TerrainType, int> costDict = new Dictionary<TerrainType, int>();

		public MovementMap()
		{
			foreach (TerrainType terrain in Enum.GetValues(typeof(TerrainType)))
			{
				costDict[terrain] = 1;
			}

			//set defaults
			costDict[TerrainType.forest] = 2;
			costDict[TerrainType.mountain] = 2;
			costDict[TerrainType.river] = 4;
			costDict[TerrainType.impassible] = 9999; //aka impassible
			costDict[TerrainType.sea] = 6;
			costDict[TerrainType.outsideWall] = 9999; //aka impassible
			costDict[TerrainType.peak] = 6;
			costDict[TerrainType.cliff] = 9999; //aka impassible
		}

	}
}
