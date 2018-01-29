using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public delegate void OnRegionEntered(UnitEntity eu);
	public delegate void OnRegionExited(UnitEntity eu);
	public delegate void OnRegionInput(List<MapActionCommand> unitCommands);


	public class MapRegion
	{
		public readonly string id;

		private List<Position> _region;
		//public IReadOnlyList<Position> RegionTiles
		//{
		//	get
		//	{
		//		return _region.AsReadOnly();
		//	}
		//}

		public bool ContainsPoint(Position pos)
		{
			return _region.Contains(pos);
		}

		public event OnRegionEntered OnEntered;
		public event OnRegionExited OnExited;
		public event OnRegionInput OnInput;

		public MapRegion(string regionId, List<Position> regionTiles)
		{
			id = regionId;
			_region = regionTiles;
		}
	}
}
