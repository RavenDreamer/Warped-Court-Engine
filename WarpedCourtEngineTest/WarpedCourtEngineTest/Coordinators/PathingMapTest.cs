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
	public class PathingMapTest
	{
		//[TestMethod]
		//public void TestInvalidSize()
		//{
		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(-1, 6, new TerrainType[0]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with negative width");
		//	}

		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(5, -6, new TerrainType[0]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with negative height");
		//	}

		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(0, 6, new TerrainType[0]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with zero-dimension width");
		//	}

		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(5, 0, new TerrainType[0]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with zero-dimension height");
		//	}
		//}

		//[TestMethod]
		//public void TestInvalidTiles()
		//{
		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(5, 6, null);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with null tiles");
		//	}

		//	try
		//	{
		//		PathingMap exceptionMap = new PathingMap(5, 6, new TerrainType[33]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with invalid number of tiles");
		//	}

		//	try
		//	{
		//		PathingMap validMap = new PathingMap(5, 6, new TerrainType[30]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.Fail("Exception when instantiating GameMap: " + ex.ToString());
		//	}
		//}


		//[TestMethod]
		//public void TestGetPathsUniformMove()
		//{
		//	UnitEntity testUnit = new UnitEntity(0, 0);
		//	testUnit.speed = 2;
		//	testUnit.SetPosition(3, 3);

		//	PathingMap validMap = new PathingMap(5, 6, new TerrainType[30]);

		//	UnitRange range = validMap.GetPathsForUnit(testUnit, new List<Position>());

		//	Assert.AreEqual(12, range.moveRange.Count, "movement range not as expected"); //includes starting space
		//	Assert.AreEqual(20, range.attackRange.Count, "attack range not as expected");
		//	Assert.AreEqual(26, range.utilityRange.Count, "utility range not as expected");
		//}

		//[TestMethod]
		//public void TestUnpathableTile()
		//{
		//	UnitEntity testUnit = new UnitEntity(0, 0);
		//	testUnit.speed = 2;
		//	testUnit.SetPosition(3, 3);
		//	testUnit.SetMovementCost(TerrainType.river, 1001);

		//	TerrainType[] terrain = new TerrainType[30];
		//	terrain[13] = TerrainType.river;
		//	terrain[25] = TerrainType.river; //should not be part of valid ranges

		//	PathingMap validMap = new PathingMap(5, 6, terrain);

		//	UnitRange range = validMap.GetPathsForUnit(testUnit, new List<Position>());

		//	Assert.AreEqual(10, range.moveRange.Count, "movement range not as expected"); //includes starting space
		//	Assert.AreEqual(18, range.attackRange.Count, "attack range not as expected");
		//	Assert.AreEqual(25, range.utilityRange.Count, "utility range not as expected");
		//}

		//[TestMethod]
		//public void TestBlockedTile()
		//{
		//	UnitEntity testUnit = new UnitEntity(0, 0);
		//	testUnit.speed = 2;
		//	testUnit.SetPosition(3, 3);

		//	TerrainType[] terrain = new TerrainType[36];

		//	PathingMap validMap = new PathingMap(6, 6, terrain);

		//	List<Position> blocked = new List<Position>();
		//	blocked.Add(new Position(3, 2));
		//	blocked.Add(new Position(3, 4));
		//	blocked.Add(new Position(2, 3));
		//	blocked.Add(new Position(4, 3));

		//	UnitRange range = validMap.GetPathsForUnit(testUnit, blocked);

		//	Assert.AreEqual(9, range.moveRange.Count, "movement range not as expected"); //includes starting space
		//	Assert.AreEqual(14, range.attackRange.Count, "attack range not as expected");
		//	Assert.AreEqual(31, range.utilityRange.Count, "utility range not as expected");
		//}
	}
}
