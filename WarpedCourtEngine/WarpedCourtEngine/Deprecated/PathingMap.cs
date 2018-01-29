using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngine
{
	/// <summary>
	/// The entrypoint for interacting with the WarpedCourt Engine. Contains a tile-terrain map (with information about the individual tiles 
	/// composing it), and a list of special tiles (with corresponding event data(?))
	/// </summary>
	public class PathingMap
	{
		private TerrainType[,] terrainMap;

		private int Width
		{
			get
			{
				return terrainMap.GetLength(0);
			}
		}

		private int Height
		{
			get
			{
				return terrainMap.GetLength(1);
			}
		}

		/// <summary>
		/// Constructs a "width" x "height" grid, using tiles defined L-R starting
		/// from the bottom left.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="tiles"></param>
		public PathingMap(int width, int height, TerrainType[] tiles)
		{
			if (width < 1 || height < 1) throw new MapSizeException("Map had invalid height or width -- both must be > 0");
			if (tiles == null || width * height != tiles.Length) throw new MapSizeException("Map dimensions don't match tile count");

			terrainMap = new TerrainType[width, height];

			int tileIndex = 0;

			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					terrainMap[j, i] = tiles[tileIndex];
					tileIndex++;
				}
			}
		}

		/// <summary>
		/// Returns the TerrainType at a given position
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public TerrainType GetTileInfo(int x, int y)
		{
			if (x >= terrainMap.GetLength(0) || x < 0)
			{
				throw new MapSizeException("Selection outside of X bounds");
			}
			if (y >= terrainMap.GetLength(1) || y < 0)
			{
				throw new MapSizeException("Selection outside of Y bounds");
			}

			return terrainMap[x, y];
		}

		/// <summary>
		/// Returns a UnitRange containing up to 3 lists of points: which squares the unit can move to,
		/// which squares the unit can attack, and which squares the unit can utility-ize.
		/// </summary>
		/// <param name="unit">The unit we are querying for</param>
		/// <param name="blockedSpaces">spaces the unit is not allowed to occupy (e.g. due to enemy unit already being there)</param>
		/// <returns></returns>
		/// //TODO: Add this to a different class
		public UnitRange GetPathsForUnit(UnitEntity unit, List<Position> blockedSpaces)
		{
			UnitRange retRange = new UnitRange();

			//List<PathNode> unvisited = new List<PathNode>();
			//List<PathNode> accessible = new List<PathNode>();

			////Step 1
			////make a node for each tile within raw movement range. 
			//unvisited = GetUnvisitedTiles(unit.speed, unit.xPos, unit.yPos, unit.moveCosts);

			////Step 2
			////run Dijkstra's algorithm
			//accessible = DijkstraPathfinder.CalculatePathDistance(unvisited, unit.xPos, unit.yPos);

			////Accessible now has only the squares which the unit can actually reach.
			////break them back into Tuples for the UnitRange
			//HashSet<Position> validPositions = new HashSet<Position>();
			//foreach (PathNode node in accessible.Where(node => node.distance <= unit.speed))
			//{
			//	validPositions.Add(new Position(node.pos.xPos, node.pos.yPos));
			//}
			////blocked positions can be pathed through, but not ended in
			//validPositions.RemoveWhere(s => blockedSpaces.Contains(s));

			////Item ranges
			//HashSet<Position> attackRanges = unit.AllAttackRanges();
			//HashSet<Position> utilityRanges = unit.AllUtilityRanges();
			//HashSet<Position> attackablePositions = new HashSet<Position>();
			//HashSet<Position> utilityPositions = new HashSet<Position>();

			//foreach (Position pos in validPositions)
			//{
			//	foreach (Position offset in attackRanges)
			//	{
			//		Position targetTile = new Position(pos.xPos + offset.xPos, pos.yPos + offset.yPos);
			//		if (targetTile.xPos >= 0 && targetTile.xPos < Width && targetTile.yPos >= 0 && targetTile.yPos < Height)
			//		{
			//			attackablePositions.Add(targetTile);
			//		}
			//	}

			//	foreach (Position offset in utilityRanges)
			//	{
			//		Position targetTile = new Position(pos.xPos + offset.xPos, pos.yPos + offset.yPos);
			//		if (targetTile.xPos >= 0 && targetTile.xPos < Width && targetTile.yPos >= 0 && targetTile.yPos < Height)
			//		{
			//			utilityPositions.Add(targetTile);
			//		}
			//	}
			//}

			//retRange.moveRange = validPositions;
			//retRange.attackRange = attackablePositions;
			//retRange.utilityRange = utilityPositions;

			return retRange;
		}



		/// <summary>
		/// Generates a list of potentially visitable tiles and the costs to enter them. Tiles outside the mapbounds are not added.
		/// </summary>
		/// <param name="speed"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="moveCosts"></param>
		/// <param name="blockedSpaces"></param>
		/// <returns></returns>
		private List<PathNode> GetUnvisitedTiles(int speed, int xPos, int yPos, MovementMap moveCosts)
		{
			List<PathNode> unvisited = new List<PathNode>();

			for (int i = -speed; i <= speed; i++)
			{
				int jVal = 0;
				if (i >= 0)
				{
					jVal = speed - i;
				}
				else
				{
					jVal = speed + i;
				}
				for (int j = -jVal; j <= jVal; j++)
				{
					Position nodePos = new Position(i + xPos, j + yPos);
					PathNode node = new PathNode(nodePos);

					if (nodePos.xPos < 0 || nodePos.xPos >= Width) continue; //outside map bounds
					if (nodePos.yPos < 0 || nodePos.yPos >= Height) continue; //outside map bounds

					TerrainType terrain = terrainMap[nodePos.xPos, nodePos.yPos];

					node.cost = moveCosts.costDict[terrain]; ; //cost to enter the square


					unvisited.Add(node);
				}
			}
			return unvisited;
		}
	}
}
