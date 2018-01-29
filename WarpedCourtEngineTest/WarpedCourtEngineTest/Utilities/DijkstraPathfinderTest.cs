using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine;

namespace WarpedCourtEngineTest
{
	[TestClass]
	public class DijkstraPathfinderTest
	{
		[TestMethod]
		public void TestUniformCostUnbounded()
		{
			List<PathNode> testPathNodes = Get5x5PathNodeList();
			List<PathNode> accessible = DijkstraPathfinder.CalculatePathDistance(testPathNodes, 0, 0);

			Assert.AreEqual(61, accessible.Count, "Some pathnodes were trimmed inappropriately");

			int[] distArray = new int[] { 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
			int distIndex = 0;
			foreach (PathNode pnode in accessible)
			{
				Assert.AreEqual(distArray[distIndex++], pnode.distance, "A pathnode was not the expected distance");
			}
		}

		[TestMethod]
		public void TestCostPocket()
		{
			List<PathNode> testPathNodes = Get5x5PathNodeList();
			testPathNodes.First(node => node.pos.xPos == 1 && node.pos.yPos == 1).cost = 100;
			testPathNodes.First(node => node.pos.xPos == 1 && node.pos.yPos == 2).cost = 100;
			testPathNodes.First(node => node.pos.xPos == 2 && node.pos.yPos == 1).cost = 100;
			testPathNodes.First(node => node.pos.xPos == 3 && node.pos.yPos == 1).cost = 100;
			testPathNodes.First(node => node.pos.xPos == 3 && node.pos.yPos == 2).cost = 100;

			List<PathNode> accessible = DijkstraPathfinder.CalculatePathDistance(testPathNodes, 0, 0);

			PathNode pocketNode = accessible.First(node => node.pos.xPos == 2 && node.pos.yPos == 2);
			Assert.AreEqual(6, pocketNode.distance, "Pocket cost not accounted for");

			PathNode edgeNode1 = accessible.First(node => node.pos.xPos == 1 && node.pos.yPos == 1);
			Assert.AreEqual(101, edgeNode1.distance, "edge node cost not accounted for");

			PathNode edgeNode2 = accessible.First(node => node.pos.xPos == 3 && node.pos.yPos == 2);
			Assert.AreEqual(106, edgeNode2.distance, "edge node cost not accounted for");
		}

		[TestMethod]
		public void TestNonContinuous()
		{
			List<PathNode> testPathNodes = Get5x5PathNodeList();
			testPathNodes.RemoveAll(node => node.pos.xPos == -4);

			List<PathNode> accessible = DijkstraPathfinder.CalculatePathDistance(testPathNodes, 0, 0);

			Assert.AreEqual(57, accessible.Count, "Some pathnodes were trimmed inappropriately");
		}

		public static List<PathNode> Get5x5PathNodeList()
		{
			List<PathNode> testPathNodes = new List<PathNode>();
			for (int i = -5; i <= 5; i++)
			{
				int jVal;
				if (i > 0)
				{
					jVal = 5 - i;
				}
				else
				{
					jVal = 5 + i;
				}

				for (int j = -jVal; j <= jVal; j++)
				{
					PathNode ijNode = new PathNode(new Position(i, j));
					testPathNodes.Add(ijNode);
				}
			}

			return testPathNodes;
		}
	}
}
