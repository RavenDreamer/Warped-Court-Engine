using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class DijkstraPathfinder
	{
		/// <summary>
		/// Implements Dijkstra's Algorithm to assign distance to a list of unvisited nodes, given a starting
		/// position of xPos, yPos. (Actually a Uniform Cost Search, I think?)
		/// </summary>
		/// <param name="unvisited"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <returns></returns>
		public static List<PathNode> CalculatePathDistance(List<PathNode> unvisited, int xPos, int yPos)
		{
			List<PathNode> resultList = new List<PathNode>();
			List<PathNode> currentList = new List<PathNode>();
			List<PathNode> frontier = new List<PathNode>();

			PathNode startNode = unvisited.Find(node => node.pos.xPos == xPos && node.pos.yPos == yPos);
			startNode.distance = 0; //starting distance
			frontier.Add(startNode);

			//just run for all nodes
			while (frontier.Count > 0)
			{
				PathNode current = frontier[0];
				current.visited = true;

				//get the up to 4 nodes which are within 1 of this.
				List<PathNode> adjacentList = unvisited.FindAll(node => (node.pos.xPos == current.pos.xPos - 1 && node.pos.yPos == current.pos.yPos) || //left
																	(node.pos.xPos == current.pos.xPos + 1 && node.pos.yPos == current.pos.yPos) || //right
																	(node.pos.xPos == current.pos.xPos && node.pos.yPos == current.pos.yPos + 1) || //above
																	(node.pos.xPos == current.pos.xPos && node.pos.yPos == current.pos.yPos - 1));  //below
				adjacentList.RemoveAll(node => node.visited == true);

				foreach (PathNode adj in adjacentList)
				{
					//set adj.distance to the minimum of either the adj.cost + current.distance OR adj.distance
					if (adj.distance > adj.cost + current.distance)
					{
						adj.distance = adj.cost + current.distance;
						adj.previousNode = current;
					}

					if (frontier.Contains(adj) == false)
					{
						frontier.Add(adj);
					}

				}

				frontier.Remove(current);
				frontier.Sort();

				resultList.Add(current);
			}
			return resultList;
		}
	}
}
