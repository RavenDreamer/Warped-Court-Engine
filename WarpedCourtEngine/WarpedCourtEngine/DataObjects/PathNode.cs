using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{

	public class PathNode : IComparable
	{
		public Position pos;
		public bool visited = false;
		public bool isGoal = false;
		public int cost = 1;
		public int distance = 1000; ///arbitrarily high

		public PathNode previousNode = null;

		public PathNode(Position position)
		{
			pos = position;
		}

		#region IComparable implementation
		public int CompareTo(object obj)
		{
			PathNode castObj = (PathNode)obj;
			if (this.distance == castObj.distance) return 0;

			if (this.distance > castObj.distance) return 1;

			return -1;
		}
		#endregion
	}
}
