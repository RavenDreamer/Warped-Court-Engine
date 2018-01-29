using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpedCourtEngine
{

	public enum FE7ItemType
	{
		HealUse,
		Promotion,
		Mine,
		Lockpick,
		Gem,
		Torch,
		SkillIncrease,
		StrMagIncrease,
		DefIncrease,
		ResIncrease,
		SpeedIncrease,
		ConIncrease,
		HPIncrease,
	}

	public class FE7Item : FEItem
	{
		public string Name { get; set; }
		public string Text { get; internal set; }
		public FE7ItemType ItemType { get; internal set; }
	}
}
