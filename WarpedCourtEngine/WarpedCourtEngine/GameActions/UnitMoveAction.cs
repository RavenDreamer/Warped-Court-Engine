using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	public class UnitMoveAction : GameAction
	{
		public MapUnit MoveUnit { get; private set; }
		public Position MovePosition { get; private set; }
		public bool IsFinalMove { get; private set; }

		public UnitMoveAction(MapUnit mu, Position pnode, bool isFinal) : base (GameActionType.UnitMove)
		{
			this.MoveUnit = mu;
			this.MovePosition = pnode;
			this.IsFinalMove = isFinal;
		}
	}
}