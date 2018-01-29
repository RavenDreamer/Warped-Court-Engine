using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{
	[Serializable]
	public class SaveSessionBundle
	{
		public UnitBook UB { get; set; }
		public int TurnCount { get; set; }
		public UnitTeam CurrentTeamTurn { get; set; }
		public UnitTeam PlayerTeam { get; set; }
	}
}
