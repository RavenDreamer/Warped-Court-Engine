using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngineTest
{
	[TestClass]
	public class GameMapTest
	{
		[TestMethod]
		public void TestSelectionBounds()
		{
			GameMap validMap;
			validMap = new GameMap(5, 6, new TerrainType[30]);
			try
			{
				validMap.QueryTileContents(-5, 5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Selection was outside of X bounds");
			}
			finally
			{
				validMap = new GameMap(5, 6, new TerrainType[30]);
			}
			try
			{
				validMap.QueryTileContents(5, 5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Selection was outside of X bounds");
			}
			finally
			{
				validMap = new GameMap(5, 6, new TerrainType[30]);
			}

			try
			{
				validMap.QueryTileContents(3, -5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Selection was outside of Y bounds");
			}
			finally
			{
				validMap = new GameMap(5, 6, new TerrainType[30]);
			}
			try
			{
				validMap.QueryTileContents(3, 6);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Selection was outside of Y bounds");
			}
			finally
			{
				validMap = new GameMap(5, 6, new TerrainType[30]);
			}

			try
			{
				validMap.QueryTileContents(2, 2);
			}
			catch (Exception ex)
			{
				Assert.Fail("Valid selection failed unexpectedly: " + ex.ToString());
			}

		}

		[TestMethod]
		public void TestUnitAddition()
		{
			GameMap map = DataFactory.Get5x6Map();

			MapUnit mu = map.AddUnit(DataFactory.GetUnitForTeam(UnitTeam.team1), new Position(0, 0));
			Assert.IsNotNull(mu, "Unable to add unit to map");

			try
			{
				map.AddUnit(DataFactory.GetUnitForTeam(UnitTeam.team1), new Position(0, 0));
			}
			catch (Exception e)
			{

				Assert.IsInstanceOfType(e, typeof(MapUnitOverlapException), "Added two units to same position");
			}

			try
			{
				map.AddUnit(DataFactory.GetUnitForTeam(UnitTeam.team1), new Position(-1, -3));
			}
			catch (Exception e)
			{

				Assert.IsInstanceOfType(e, typeof(MapSizeException), "Added unit in invalid position");
			}

			UnitEntity ue = DataFactory.GetUnitForTeam(UnitTeam.team1);
			MapUnit mau = map.AddUnit(ue, new Position(2, 1));

			Assert.AreSame(ue, mau.Stats, "Added unit link broken");

			UnitEntity ue2 = DataFactory.GetUnitForTeam(UnitTeam.team1);

			try
			{
				map.AddUnit(ue2, new Position(2, 1));
			}
			catch(Exception e)
			{
				Assert.IsInstanceOfType(e, typeof(MapUnitOverlapException), "Added unit on top of another unit");
			}

			map.AddUnit(ue2, new Position(2, 2));


			Assert.AreNotSame(map.GetUnitInfoByPosition(2,2), map.GetUnitInfoByPosition(2, 1), "Added unit somehow identical");
		}

		[TestMethod]
		public void TestFindUnit()
		{
			GameMap map = DataFactory.Get5x6Map();

			try
			{
				map.GetUnitInfoByPosition(2, 2);
			}
			catch(Exception e)
			{
				Assert.IsInstanceOfType(e, typeof(UnitNotFoundException), "Able to query a non-existant unit");
			}

			map.AddUnit(DataFactory.GetUnitForTeam(UnitTeam.team1), new Position(2, 1));
			Assert.AreEqual(MapSelectionResponse.unitTile, map.QueryTileContents(2, 1));

		}

		[TestMethod]
		public void TestQueryTerrain()
		{
			//all the default enum
			GameMap map = DataFactory.Get5x6Map();

			Assert.AreEqual(map.GetTileInfo(1, 1), TerrainType.plains, "Was not a plains as expected");

			Assert.AreEqual(MapSelectionResponse.emptyTile, map.QueryTileContents(1, 1), "Was not an empty tile as expected");
		}

	}
}
