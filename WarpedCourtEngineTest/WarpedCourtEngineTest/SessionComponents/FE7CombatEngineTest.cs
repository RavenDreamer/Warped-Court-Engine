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
	public class FE7CombatEngineTest
	{

		FE7CombatEngine engine;
	

		[TestInitialize]
		public void TestInitialize()
		{
			engine = new FE7CombatEngine(new Random(39));
		}

		[TestMethod]
		[TestCategory("FE7 Combat Engine")]
		public void WeaponTriangleSwordAxeTest()
		{
			//hit, nocrit, miss, nocrit
			engine.ResetRandomNumbers();
			List<int> randNumbs = new List<int> { 99, 99, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);

			//sword, axe, lance, bow
			UnitEntity[] testUnits = DataFactory.GetFE7EngineWeaponTestUnits();

			
			var result =  engine.InitiateCombat(testUnits[0], testUnits[1],1);
			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction cha = result[0] as CombatHitAction; //WTA hit increase works correctly.
			CombatMissAction cma = result[1] as CombatMissAction; //WTD hit reduction works correctly.
			Assert.IsNotNull(cha, "First action wasn't a hit");
			Assert.IsNotNull(cma, "Second action wasn't a miss");
			Assert.AreEqual(cha.AttackerDamage, 2, "Didn't get weapon triangle bonus damage"); //WTA damage increase works correctly

			//test WTD damage reduction
			randNumbs = new List<int> { 0, 0, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);
			result = engine.InitiateCombat(testUnits[1], testUnits[0],1);

			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction ZeroCHA = result[0] as CombatHitAction;
			Assert.AreEqual(ZeroCHA.AttackerDamage, 0, "Didn't get weapon triangle damage penalty");
		}

		[TestMethod]
		[TestCategory("FE7 Combat Engine")]
		public void WeaponTriangleAxeLanceTest()
		{
			//hit, nocrit, miss, nocrit
			engine.ResetRandomNumbers();
			List<int> randNumbs = new List<int> { 99, 99, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);

			//sword, axe, lance, bow
			UnitEntity[] testUnits = DataFactory.GetFE7EngineWeaponTestUnits();


			var result = engine.InitiateCombat(testUnits[1], testUnits[2],1);
			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction cha = result[0] as CombatHitAction; //WTA hit increase works correctly.
			CombatMissAction cma = result[1] as CombatMissAction; //WTD hit reduction works correctly.
			Assert.IsNotNull(cha, "First action wasn't a hit");
			Assert.IsNotNull(cma, "Second action wasn't a miss");
			Assert.AreEqual(cha.AttackerDamage, 2, "Didn't get weapon triangle bonus damage"); //WTA damage increase works correctly

			//test WTD damage reduction
			randNumbs = new List<int> { 0, 0, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);
			result = engine.InitiateCombat(testUnits[2], testUnits[1],1);

			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction ZeroCHA = result[0] as CombatHitAction;
			Assert.AreEqual(ZeroCHA.AttackerDamage, 0, "Didn't get weapon triangle damage penalty");
		}

		[TestMethod]
		[TestCategory("FE7 Combat Engine")]
		public void WeaponTriangleLanceSwordTest()
		{
			//hit, nocrit, miss, nocrit
			engine.ResetRandomNumbers();
			List<int> randNumbs = new List<int> { 99, 99, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);

			//sword, axe, lance, bow
			UnitEntity[] testUnits = DataFactory.GetFE7EngineWeaponTestUnits();


			var result = engine.InitiateCombat(testUnits[2], testUnits[0],1);
			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction cha = result[0] as CombatHitAction; //WTA hit increase works correctly.
			CombatMissAction cma = result[1] as CombatMissAction; //WTD hit reduction works correctly.
			Assert.IsNotNull(cha, "First action wasn't a hit");
			Assert.IsNotNull(cma, "Second action wasn't a miss");
			Assert.AreEqual(cha.AttackerDamage, 2, "Didn't get weapon triangle bonus damage"); //WTA damage increase works correctly

			//test WTD damage reduction
			randNumbs = new List<int> { 0, 0, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);
			result = engine.InitiateCombat(testUnits[0], testUnits[2],1);

			Assert.AreEqual(result.Count, 3, "Did not have 3 actions");
			CombatHitAction ZeroCHA = result[0] as CombatHitAction;
			Assert.AreEqual(ZeroCHA.AttackerDamage, 0, "Didn't get weapon triangle damage penalty");
		}

		[TestMethod]
		[TestCategory("FE7 Combat Engine")]
		public void WeaponTriangleSwordBowTest()
		{
			//hit, nocrit, miss, nocrit
			engine.ResetRandomNumbers();
			List<int> randNumbs = new List<int> { 99, 99, 0, 0, 0, 0 };
			engine.AddRandomNumbers(randNumbs);

			//sword, axe, lance, bow
			UnitEntity[] testUnits = DataFactory.GetFE7EngineWeaponTestUnits();

			var result = engine.InitiateCombat(testUnits[0], testUnits[3],1);
			Assert.AreEqual(result.Count, 2, "Did not have 2 actions");
			CombatMissAction cma = result[0] as CombatMissAction; //Did not get a WTA bonus as expected
			Assert.IsNotNull(cma, "First action wasn't a miss");

			result = engine.InitiateCombat(testUnits[0], testUnits[3], 1);
			CombatHitAction cha = result[0] as CombatHitAction;
			Assert.AreEqual(cha.AttackerDamage, 1, "Erroneously got weapon triangle bonus damage"); //Did not get a WTA damage bonus as expected

			//test WTD damage reduction
			randNumbs = new List<int> { 0, 0, 0, 70, 70, 0 };
			engine.AddRandomNumbers(randNumbs);
			result = engine.InitiateCombat(testUnits[3], testUnits[0],2);

			Assert.AreEqual(result.Count, 2, "Did not have 2 actions");
			CombatHitAction OneCHA = result[0] as CombatHitAction; //Did not get a WDA penalty as expected
			Assert.AreEqual(OneCHA.AttackerDamage, 1, "Erroneously got weapon triangle bonus damage"); //Did not get a WDA damage penalty as expected
		}

		[TestMethod]
		[TestCategory("FE7 Combat Engine")]
		public void WeaponEngagementRangeTest()
		{
			engine.ResetRandomNumbers();
			List<int> randNumbs = new List<int> { 0, 0, 100 };
			engine.AddRandomNumbers(randNumbs);

			//sword, axe, lance, bow
			UnitEntity[] testUnits = DataFactory.GetFE7EngineWeaponTestUnits();

			//bow vs. sword - bow attacks @ range 2, sword can't counter
			var result = engine.InitiateCombat(testUnits[3], testUnits[0], 2);
			Assert.AreEqual(result.Count, 2, "Did not have 2 actions");

			//bow vs. sword - bow can't attack @ range 1 so error
			try
			{
				engine.InitiateCombat(testUnits[3], testUnits[0], 1);
				Assert.Fail("Did not trigger exception");
			}
			catch(Exception e)
			{
				Assert.IsInstanceOfType(e, typeof(WeaponException), "Able to attack at inappropriate weapon range");
			}

			UnitEntity mageFriend =
				new FE7UnitEntity()
				{
					InternalName = "Magefriend",
					InitalPosition = new Position(0, 0),
					Move = 5,
					MaxHP = 20,
					Constitution = 15,
					WeaponList = new List<IWeaponItem>(new IWeaponItem[1]
					{
						new FE7Weapon(){
							Range = new List<int>(new int[2]{1,2}),
							Weight = 0,
							Might = 1,
							Hit = 85,
							Crit = 0,
							WeaponType = FE7WeaponType.Anima,
							Name = "Test !!FIRE!!",
						}
					})
				};

			//mage vs. axe @ both 1 & 2. Retaliate at 1, not at 2.
			result = engine.InitiateCombat(mageFriend, testUnits[1], 2);
			Assert.AreEqual(result.Count, 2, "Did not have 2 actions");

			result = engine.InitiateCombat(mageFriend, testUnits[1], 1);
			Assert.AreEqual(result.Count, 3, "Did not have 2 actions");


		}
	}
}
