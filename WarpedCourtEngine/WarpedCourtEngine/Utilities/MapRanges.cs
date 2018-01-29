using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class MapRanges
	{
		/// <summary>
		/// Returns a HashSet of Positions for all points within the given magnitude
		/// </summary>
		/// <param name="magnitude"></param>
		/// <returns></returns>
		public static HashSet<Position> GetRangesInclusive(int magnitude)
		{
			HashSet<Position> retRanges = new HashSet<Position>();
			for (int i = -magnitude; i <= magnitude; i++)
			{
				int jVal = 0;
				if (i >= 0)
				{
					jVal = magnitude - i;
				}
				else
				{
					jVal = magnitude + i;
				}
				for (int j = -jVal; j <= jVal; j++)
				{
					retRanges.Add(new Position(i,j));
				}
			}
			return retRanges;
		}

		/// <summary>
		/// Returns a HashSet of Positions for all points exactly matching the given magnitude
		/// </summary>
		/// <param name="magnitude"></param>
		/// <returns></returns>
		public static HashSet<Position> GetRangesExactly(int magnitude)
		{
			HashSet<Position> retRanges = new HashSet<Position>();
			for (int i = -magnitude; i <= magnitude; i++)
			{
				int jVal = 0;
				if (i >= 0)
				{
					jVal = magnitude - i;
				}
				else
				{
					jVal = magnitude + i;
				}
				for (int j = -jVal; j <= jVal; j++)
				{
					if (Math.Abs(j) + Math.Abs(i) == magnitude)
					{
						retRanges.Add(new Position(i, j));
					}
				}
			}
			return retRanges;
		}
	}
}
