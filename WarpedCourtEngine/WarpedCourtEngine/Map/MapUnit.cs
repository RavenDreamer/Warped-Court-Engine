using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class MapUnit
	{
		public Position Position { get; set; }
		public UnitEntity Stats { get; private set; }

		public MapUnit(UnitEntity ue)
		{
			this.Stats = ue;
		}

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Position return false.
			MapUnit mu = obj as MapUnit;
			if (mu == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (Position.Equals(mu.Position) && Stats.Equals(mu.Stats));
		}

		public bool Equals(MapUnit mu)
		{
			// If parameter is null return false:
			if (mu == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (Position.Equals(mu.Position) && Stats.Equals(mu.Stats));
		}

		public override int GetHashCode()
		{
			return Position.xPos ^ Position.yPos;
		}

		public override string ToString()
		{
			return Stats.ToString() + Position.ToString();
		}
	}
}
