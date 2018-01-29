using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngine
{
	/// <summary>
	/// The entrypoint for position-specific data in the WarpedCourt Engine. Contains a unit-position map,
	/// a tile-based terrain map, and a list of "regions" that can have delegates applied to them
	/// in order to set callbacks.
	/// </summary>
	public class GameMap
	{
		private MapUnit[,] unitMap; //contains which squares are currently occupied by which units
		private TerrainType[,] terrainMap; //contains which terrain is in which square
		private List<MapRegion> regions; //contains regions, which are lists of positions w/ delegates

		public int Width { get; private set; }
		public int Height { get; private set; }

		/// <summary>
		/// Constructs a "width" x "height" grid, using tiles defined L-R starting
		/// from the bottom left.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="tiles"></param>
		public GameMap(int width, int height, TerrainType[] tiles)
		{
			if (width < 1 || height < 1) throw new MapSizeException("Map had invalid height or width -- both must be > 0");
			if (tiles == null || width * height != tiles.Length) throw new MapSizeException("Map dimensions don't match tile count");

			this.Width = width;
			this.Height = height;
			regions = new List<MapRegion>();

			terrainMap = new TerrainType[width, height];
			unitMap = new MapUnit[width, height];

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
		/// Adds a UnitEntity to the map at the given position,
		/// and returns a reference to its corresponding MapUnit
		/// </summary>
		/// <param name="unitEntity"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public MapUnit AddUnit(UnitEntity unitEntity, Position position)
		{
			MapUnit mu;
			try
			{
				mu = unitMap[position.xPos, position.yPos];
			}
			catch (System.IndexOutOfRangeException)
			{
				//unit not added
				throw new MapSizeException("Selection outside of bounds");
			}
			if (mu != null)
			{
				throw new MapUnitOverlapException("Added unit on top of another unit");
			}
			else
			{
				mu = new MapUnit(unitEntity);
				mu.Position = new Position(position.xPos, position.yPos);
				unitMap[position.xPos, position.yPos] = mu;
				return mu;
			}
		}

		/// <summary>
		/// Adds a new region to this GameMap
		/// </summary>
		/// <param name="mRegion"></param>
		public void AddRegion(MapRegion mRegion)
		{
			regions.Add(mRegion);
		}

		/// <summary>
		/// Returns the TerrainType at a given position
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public TerrainType GetTileInfo(int x, int y)
		{
			if (x >= Width || x < 0)
			{
				throw new MapSizeException("Selection outside of X bounds");
			}
			if (y >= Height || y < 0)
			{
				throw new MapSizeException("Selection outside of Y bounds");
			}

			return terrainMap[x, y];
		}

		public UnitEntity GetUnitInfoByPosition(int x, int y)
		{
			if (x >= terrainMap.GetLength(0) || x < 0)
			{
				return null;
			}
			if (y >= terrainMap.GetLength(1) || y < 0)
			{
				return null;
			}
			MapUnit ue = unitMap[x, y];
			if (ue == null)
			{
				throw new UnitNotFoundException("No unit present at x,y");
			}

			return ue.Stats;
		}

		public UnitEntity QueryUnitPosition(int x, int y)
		{
			if (x >= terrainMap.GetLength(0) || x < 0)
			{
				return null;
			}
			if (y >= terrainMap.GetLength(1) || y < 0)
			{
				return null;
			}
			MapUnit ue = unitMap[x, y];
			if (ue == null)
			{
				return null;
			}

			return ue.Stats;
		}

		//public MapUnit GetUnitByPosition(int x, int y)
		//{
		//	if (x >= terrainMap.GetLength(0) || x < 0)
		//	{
		//		throw new MapSizeException("Selection outside of X bounds");
		//	}
		//	if (y >= terrainMap.GetLength(1) || y < 0)
		//	{
		//		throw new MapSizeException("Selection outside of Y bounds");
		//	}
		//	MapUnit mu = unitMap[x, y];
		//	if (mu == null)
		//	{
		//		throw new UnitNotFoundException("No unit present at x,y");
		//	}

		//	return mu;
		//}

		/// <summary>
		/// Gets the movable squares for the unit, based on their speed, etc.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		internal UnitRange GenerateMovesquares(UnitEntity value)
		{
			MapUnit mu = GetUnityByEntity(value);
			List<Position> blockedSpaces = GetUnitAllies(value.TeamID);

			return GetPathsForUnit(mu.Stats, mu.Position, blockedSpaces);
		}

		/// <summary>
		/// Returns the positions of all units on a given team.
		/// </summary>
		/// <param name="teamID"></param>
		/// <returns></returns>
		private List<Position> GetUnitAllies(UnitTeam teamID)
		{
			List<Position> retPos = new List<Position>();

			int w = unitMap.GetLength(0); // width
			int h = unitMap.GetLength(1); // height


			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					if (unitMap[x,y] != null && unitMap[x, y].Stats.TeamID == teamID)
					{
						retPos.Add(new Position(x, y));
					}
				}
			}
			return retPos;
		}


		/// <summary>
		/// Finds a path to the given space, using current path as a starting point
		/// </summary>
		/// <param name="selectedUnit"></param>
		/// <param name="targ"></param>
		/// <param name="currentPath"></param>
		/// <returns></returns>
		internal List<Position> FindPath(UnitEntity unit, Position targ, List<Position> currentPath)
		{

			List<PathNode> unvisited = new List<PathNode>();
			List<PathNode> accessible = new List<PathNode>();

			//Part 1 - make sure the target is not already in the path
			if(currentPath.Contains(targ))
			{
				int i = currentPath.IndexOf(targ);
				return currentPath.GetRange(0, i+1);
			}

			//Part 2 - find path from existing position
			if (unit.Move - currentPath.Count > 0) //fail fast for finding path from scratch
			{
				Position previousPosition = currentPath.Last();

				//Step 1
				//make a node for each tile within modified movement range. 
				unvisited = GetUnvisitedTiles(unit.Move - currentPath.Count, previousPosition, unit.MoveCosts, unit.TeamID);

				//Step 2
				//run Dijkstra's algorithm
				accessible = DijkstraPathfinder.CalculatePathDistance(unvisited, previousPosition.xPos, previousPosition.yPos);

				if(accessible.Find(s => s.pos.Equals(targ)) != null)
				{
					//we can reach the new square from our existing path. return the new path as an append to the existing path
					List<Position> appendList = new List<Position>();

					PathNode markerNode = accessible.Find(s => s.pos.Equals(targ));

					while(markerNode != null && !markerNode.pos.Equals(previousPosition))
					{
						appendList.Add(markerNode.pos);
						markerNode = markerNode.previousNode;
					}

					appendList.Reverse();
					currentPath.AddRange(appendList);
					return currentPath;
				}
				//else - we need to find the path from scratch
			}

			//Part 3 - calculate path from scratch

			//Step 1
			//make a node for each tile within modified movement range. 
			unvisited = GetUnvisitedTiles(unit.Move, currentPath[0], unit.MoveCosts, unit.TeamID);

			//Step 2
			//run Dijkstra's algorithm
			accessible = DijkstraPathfinder.CalculatePathDistance(unvisited, currentPath[0].xPos, currentPath[0].yPos);
			if (accessible.Find(s => s.pos.Equals(targ)) != null)
			{
				//we can reach the new square from our existing path. return the new path as an append to the existing path
				List<Position> retList = new List<Position>();

				PathNode markerNode = accessible.Find(s => s.pos.Equals(targ));

				while (markerNode != null)
				{
					retList.Add(markerNode.pos);
					markerNode = markerNode.previousNode;
				}

				retList.Reverse();

				return retList;
			}
			else
			{
				throw new PathFindingException("Could not find path to a navigable tile");
			}


		}

		/// <summary>
		/// Finds a unit on the map, based on their UnitEntity
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public MapUnit GetUnityByEntity(UnitEntity value)
		{
			int w = unitMap.GetLength(0); // width
			int h = unitMap.GetLength(1); // height


			for (int x = 0; x < w; ++x)
			{
				for (int y = 0; y < h; ++y)
				{
					if (unitMap[x,y] != null && unitMap[x, y].Stats.Equals(value))
						return unitMap[x, y];
				}
			}
			return null;
		}


		/// <summary>
		/// Generates a list of potentially visitable tiles and the costs to enter them. Tiles outside the mapbounds are not added.
		/// </summary>
		/// <param name="speed"></param>
		/// <param name="pos"></param>
		/// <param name="moveCosts"></param>
		/// <returns></returns>
		private List<PathNode> GetUnvisitedTiles(int speed, Position pos, MovementMap moveCosts, UnitTeam team)
		{
			List<PathNode> unvisited = new List<PathNode>();

			//only look at the diamond of squares around the given position
			//within the speed. This assumes no 0 move-cost tiles.
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
					Position nodePos = new Position(i + pos.xPos, j + pos.yPos);
					PathNode node = new PathNode(nodePos);

					if (nodePos.xPos < 0 || nodePos.xPos >= Width) continue; //outside map bounds
					if (nodePos.yPos < 0 || nodePos.yPos >= Height) continue; //outside map bounds

					//check for another team in the square. These squares cannot be entered.
					MapUnit mu = unitMap[i + pos.xPos, j + pos.yPos];
					if (mu != null)
					{
						if (mu.Stats.TeamID != team) continue;
					}

					TerrainType terrain = terrainMap[nodePos.xPos, nodePos.yPos];

					if (node.pos.Equals(pos))
					{
						node.cost = 0; //free to "enter" the square you're already in.
					}
					else
					{
						node.cost = moveCosts.costDict[terrain]; ; //cost to enter the square
					}

					unvisited.Add(node);
				}
			}
			return unvisited;
		}

		internal void MoveUnit(MapUnit mu, Position pnode, bool isFinalPosition)
		{
			if(unitMap[pnode.xPos, pnode.yPos] == null)
			{
				unitMap[pnode.xPos, pnode.yPos] = mu;

				if (unitMap[mu.Position.xPos, mu.Position.yPos] == mu)
				{
					unitMap[mu.Position.xPos, mu.Position.yPos] = null;
				}
				else
				{
					throw new MapUnitOverlapException("Unit not found where moving from");
				}
				mu.Position = new Position(pnode.xPos, pnode.yPos);
			}
			else
			{
				if (isFinalPosition)
				{
					throw new MapUnitOverlapException("Unit already present where moving to");
				}
				else
				{
					//do nothing unit exists in a superposition until the next MoveUnit call.
				}
			}
		}

		/// <summary>
		/// Returns a UnitRange containing up to 3 lists of points: which squares the unit can move to,
		/// which squares the unit can attack, and which squares the unit can utility-ize.
		/// </summary>
		/// <param name="unit">The unit we are querying for</param>
		/// <param name="blockedSpaces">spaces the unit is not allowed to occupy (e.g. due to ally unit already being there)</param>
		/// <returns></returns>
		/// //TODO: Add this to a different class
		public UnitRange GetPathsForUnit(UnitEntity unit, Position unitPosition, List<Position> blockedSpaces)
		{
			UnitRange retRange = new UnitRange();

			List<PathNode> unvisited = new List<PathNode>();
			List<PathNode> accessible = new List<PathNode>();

			//Step 1
			//make a node for each tile within raw movement range. 
			unvisited = GetUnvisitedTiles(unit.Move, unitPosition, unit.MoveCosts, unit.TeamID);

			//Step 2
			//run Dijkstra's algorithm
			accessible = DijkstraPathfinder.CalculatePathDistance(unvisited, unitPosition.xPos, unitPosition.yPos);

			//Accessible now has only the squares which the unit can actually reach.
			//break them back into Tuples for the UnitRange
			HashSet<Position> validPositions = new HashSet<Position>();
			foreach (PathNode node in accessible.Where(node => node.distance <= unit.Move))
			{
				validPositions.Add(new Position(node.pos.xPos, node.pos.yPos));
			}
			//blocked positions can be pathed through, but not ended in.
			//you can end in your own space though
			validPositions.RemoveWhere(s => blockedSpaces.Contains(s) && !(s.Equals(unitPosition)));

			//Item ranges
			HashSet<Position> attackRanges = unit.AllAttackRanges();
			HashSet<Position> utilityRanges = unit.AllUtilityRanges();
			HashSet<Position> attackablePositions = new HashSet<Position>();
			HashSet<Position> utilityPositions = new HashSet<Position>();

			foreach (Position pos in validPositions)
			{
				foreach (Position offset in attackRanges)
				{
					Position targetTile = new Position(pos.xPos + offset.xPos, pos.yPos + offset.yPos);
					if (targetTile.xPos >= 0 && targetTile.xPos < Width && targetTile.yPos >= 0 && targetTile.yPos < Height)
					{
						attackablePositions.Add(targetTile);
					}
				}

				foreach (Position offset in utilityRanges)
				{
					Position targetTile = new Position(pos.xPos + offset.xPos, pos.yPos + offset.yPos);
					if (targetTile.xPos >= 0 && targetTile.xPos < Width && targetTile.yPos >= 0 && targetTile.yPos < Height)
					{
						utilityPositions.Add(targetTile);
					}
				}
			}

			retRange.moveRange = validPositions;
			retRange.attackRange = attackablePositions;
			retRange.utilityRange = utilityPositions;

			return retRange;
		}

		public MapSelectionResponse QueryTileContents(int x, int y)
		{
			if (x >= Width || x < 0)
			{
				throw new MapSizeException("Selection outside of X bounds");
			}
			if (y >= Height || y < 0)
			{
				throw new MapSizeException("Selection outside of Y bounds");
			}

			if (unitMap[x, y] == null)
			{
				return MapSelectionResponse.emptyTile;
			}

			return MapSelectionResponse.unitTile;
		}
	}
}
