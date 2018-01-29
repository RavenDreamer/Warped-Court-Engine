using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	/// <summary>
	/// An immutable type that holds 2 integer values
	/// </summary>
	public class Position
	{
		public readonly int xPos;
		public readonly int yPos;

		public Position(int x, int y)
		{
			xPos = x;
			yPos = y;
		}

		//copying this method from the MSDN implementation of "TwoDPoint"
		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Position return false.
			Position p = obj as Position;
			if ((System.Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (xPos == p.xPos) && (yPos == p.yPos);
		}

		public bool Equals(Position p)
		{
			// If parameter is null return false:
			if ((object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (xPos == p.xPos) && (yPos == p.yPos);
		}

		//copied from MSDN's "TwoDPoint" implementation
		public override int GetHashCode()
		{
			return xPos ^ yPos;
		}

		//adds two positions together into a new point
		public static Position operator +(Position a, Position b)
		{
			return new Position(a.xPos + b.xPos, a.yPos + b.yPos);
		}

		//subtracts two positions together into a new point
		public static Position operator -(Position a, Position b)
		{
			return new Position(a.xPos - b.xPos, a.yPos - b.yPos);
		}

		public override string ToString()
		{
			return "X: " + xPos + " Y: " + yPos;
		}
	}
}
