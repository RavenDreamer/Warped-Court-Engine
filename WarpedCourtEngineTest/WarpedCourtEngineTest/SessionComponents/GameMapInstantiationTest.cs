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
	public class GameMapInstantiationTest
	{
		[TestMethod]
		public void TestInvalidSize()
		{
			try
			{
				GameMap exceptionMap = new GameMap(-1, 6, new TerrainType[0]);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with negative width");
			}

			try
			{
				GameMap exceptionMap = new GameMap(5, -6, new TerrainType[0]);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with negative height");
			}

			try
			{
				GameMap exceptionMap = new GameMap(0, 6, new TerrainType[0]);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with zero-dimension width");
			}

			try
			{
				GameMap exceptionMap = new GameMap(5, 0, new TerrainType[0]);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with zero-dimension height");
			}
		}

		[TestMethod]
		public void TestInvalidTiles()
		{
			try
			{
				GameMap exceptionMap = new GameMap(5, 6, null);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with null tiles");
			}

			try
			{
				GameMap exceptionMap = new GameMap(5, 6, new TerrainType[33]);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapSizeException), "Map was created with invalid number of tiles");
			}

			try
			{
				GameMap validMap = new GameMap(5, 6, new TerrainType[30]);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception when instantiating GameMap: " + ex.ToString());
			}
		}
	}
}
