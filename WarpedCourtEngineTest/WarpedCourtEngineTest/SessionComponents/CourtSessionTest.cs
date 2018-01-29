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
	public class CourtSessionTest
	{
		CourtSession session;

		[TestInitialize]
		public void TestInitialize()
		{
			session = new CourtSession
			{
				PlayerTeam = UnitTeam.team1
			};

			session.AddMap(DataFactory.GetLausMap());
			session.AddUnitBook(DataFactory.GetLausUnits());
			session.AddCombatEngine(CombatEngine.GBA,42);
			session.DeployUnits();
		}

		[TestMethod]
		public void TestDeployUnitsStatic()
		{
			//test for enemy units (static deployment)
			//Erik deployed to the castle at 1,10
			Assert.AreEqual("Erik", session.GetUnit(1, 10).InternalName);
		}

		[TestMethod]
		public void TestMapConfirmation()
		{
			//use tile 0,0, since there are no units there.
			List<string> expectedActions = new List<string>
			{
				"Units",
				"Save",
				"End"
			};
			//we don't test navigating the menu, because that's UI and will be handled in Unity. 
			//Instead, we have separate "Display Units", "Save", and "End Turn" test methods

			MenuOpenedAction mapOptions = session.OnConfirmationInputReceived(0, 0)[0] as MenuOpenedAction;

			Assert.IsNotNull(mapOptions, "Map Options was null");

			foreach (string s in mapOptions.OpenedMenu.Actions)
			{
				Assert.IsTrue(expectedActions.Contains(s), "Expected action did not contain: " + s);
			}

			foreach (string s in expectedActions)
			{
				Assert.IsTrue(mapOptions.OpenedMenu.Actions.Contains(s), "Map Options contained additionally: " + s);
			}
		}

		[TestMethod]
		public void TestUnitSelection()
		{
			session.StartTurn(session.PlayerTeam);

			//we don't test navigating the menu, because that's UI and will be handled in Unity. 
			//First confirm that we can't select someone who's on the wrong team

			MenuOpenedAction mapOptions = session.OnConfirmationInputReceived(1, 10)[0] as MenuOpenedAction;
			Assert.IsNotNull(mapOptions, "Mistakenly selected Erik. It's not his turn");
			Assert.IsTrue(mapOptions.OpenedMenu.MenuType == MenuTypes.Map, "Did not open map on enemy unit");

			MenuOpenedAction mapOptions2 = session.OnConfirmationInputReceived(20, 13)[0] as MenuOpenedAction;
			Assert.IsNull(mapOptions2, "Somehow wound up with a menu instead of Hector");
		}

		[TestMethod]
		public void TestUnitOrderWaitInPlace()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			UnitSelectedAction hectorEntity = session.OnConfirmationInputReceived(20, 13)[0] as UnitSelectedAction;
			Assert.IsNotNull(hectorEntity, "Did not select Hector");

			//re-select Hector's square to "move" him there.
			MenuOpenedAction unitOptions = session.OnConfirmationInputReceived(20, 13)[0] as MenuOpenedAction;
			Assert.IsNotNull(unitOptions, "Did not get the unit menu as expected");
			Assert.AreEqual(unitOptions.OpenedMenu.MenuType, MenuTypes.Unit, "Did not get a unit menu");
			Assert.IsTrue(unitOptions.OpenedMenu.Actions.Contains("Wait"), "Did not have Wait as expected");

			//select "Wait"
			UnitWaitAction waitCommand = session.OnConfirmationInputReceived(MenuTypes.Unit, "Wait")[0] as UnitWaitAction;
			Assert.IsNotNull(waitCommand);
		}

		[TestMethod]
		public void TestUnitOrderWaitAfterMove()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			session.OnConfirmationInputReceived(20, 13);

			//move one square to the right
			session.OnPositionHover(21, 13);

			//select the square to move him there.
			session.OnConfirmationInputReceived(21, 13);

			//select "Wait"
			UnitWaitAction waitCommand = session.OnConfirmationInputReceived(MenuTypes.Unit, "Wait")[1] as UnitWaitAction;
			Assert.IsNotNull(waitCommand, "Wait did not occur");

			//refresh Hector
			session.StartTurn(session.PlayerTeam);

			//select Hector
			UnitSelectedAction hectorEntity = session.OnConfirmationInputReceived(21, 13)[0] as UnitSelectedAction;
			Assert.IsNotNull(hectorEntity, "Did not select Hector");
		}

		[TestMethod]
		public void TestUnitOrderAttackAfterMove()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			session.OnConfirmationInputReceived(20, 13);

			//move two squares north
			session.OnPositionHover(20, 14);
			session.OnPositionHover(20, 15); //there should be an "Eastern Merc" directly north of us"

			//we should have both "Attack" and "Wait"
			MenuOpenedAction unitOptions = session.OnConfirmationInputReceived(20, 15)[0] as MenuOpenedAction;
			Assert.IsNotNull(unitOptions, "Did not get the unit menu as expected");
			Assert.AreEqual(unitOptions.OpenedMenu.MenuType, MenuTypes.Unit, "Did not get a unit menu");
			Assert.IsTrue(unitOptions.OpenedMenu.Actions.Contains("Attack"), "Did not have Attack as expected");

			//select "Attack"
			AttackDetailsMenuData attackMenu = (session.OnConfirmationInputReceived(MenuTypes.Unit, "Attack")[0] as MenuOpenedAction).OpenedMenu as AttackDetailsMenuData;
			Assert.IsNotNull(attackMenu, "Did not get the attack details menu as expected");
			Assert.AreEqual(attackMenu.MenuType, MenuTypes.AttackDetails, "Did not get a attack details menu");
			Assert.IsTrue(attackMenu.Actions.Contains("Attack"), "Did not have Attack as expected");

			//Trigger the attack
			List<GameAction> attackResults = session.OnConfirmationInputReceived(MenuTypes.AttackDetails, "Attack");
			CombatHitAction cha = attackResults[0] as CombatHitAction;
			CombatDefenderDefeatAction cdda = attackResults[1] as CombatDefenderDefeatAction;
			Assert.IsNotNull(cha, "Hector did not Hit");
			Assert.IsNotNull(cdda, "Hector did not win outright.");
			Assert.IsTrue(attackResults.Count > 3, "Did not include move actions");

		}

		[TestMethod]
		public void TestUnitNoWeaponCantAttack()
		{
			Assert.Fail("Not Implemented");
		}

		[TestMethod]
		public void TestUnitMovePathingPathBacktrack()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			session.OnConfirmationInputReceived(20, 13);

			//move one square to the right
			session.OnPositionHover(21, 13);

			//move one square to the right
			session.OnPositionHover(22, 13);

			//move one square to the left
			session.OnPositionHover(21, 13);

			//select the square to move him there.
			session.OnConfirmationInputReceived(21, 13);

			//select "Wait". Action 0 is the move action to move 1 space
			UnitWaitAction waitCommand = session.OnConfirmationInputReceived(MenuTypes.Unit, "Wait")[1] as UnitWaitAction;
			Assert.IsNotNull(waitCommand, "Action [1] wasn't the wait command");

			//refresh Hector
			session.StartTurn(session.PlayerTeam);

			//select Hector
			UnitSelectedAction hectorEntity = session.OnConfirmationInputReceived(21, 13)[0] as UnitSelectedAction;
			Assert.IsNotNull(hectorEntity, "Did not select Hector");

			//move three squares to the right
			session.OnPositionHover(22, 13);
			session.OnPositionHover(23, 13);
			session.OnPositionHover(24, 13);

			//move one square to the down
			session.OnPositionHover(24, 12);

			//move two squares to the left
			session.OnPositionHover(23, 12);
			session.OnPositionHover(22, 12);

			//select the square to move him there.
			session.OnConfirmationInputReceived(22, 12);

			//select "Wait". Action 0 & 1 are moves now.
			waitCommand = session.OnConfirmationInputReceived(MenuTypes.Unit, "Wait")[2] as UnitWaitAction;
			Assert.IsNotNull(waitCommand, "Action [2] wasn't the wait command, 2nd time");

			//refresh Hector
			session.StartTurn(session.PlayerTeam);

			//select Hector
			hectorEntity = session.OnConfirmationInputReceived(22, 12)[0] as UnitSelectedAction;
			Assert.IsNotNull(hectorEntity, "Did not select Hector");
		}

		[TestMethod]
		public void TestUnitMoveCosts()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			session.OnConfirmationInputReceived(20, 13);

			//move three squares to the left
			session.OnPositionHover(19, 13); 
			session.OnPositionHover(18, 13);
			session.OnPositionHover(17, 13); //river, 4 movement, can't actually enter


			//select the square to move him "there".
			session.OnConfirmationInputReceived(17, 13);

			//select "Wait"
			session.OnConfirmationInputReceived(MenuTypes.Unit, "Wait");

			//refresh Hector
			session.StartTurn(session.PlayerTeam);

			//select Hector
			UnitSelectedAction hectorEntity = session.OnConfirmationInputReceived(18, 13)[0] as UnitSelectedAction;
			Assert.IsNotNull(hectorEntity, "Hector not where expected due to river");
		}

		[TestMethod]
		public void TestCancelUnitOrderMenu()
		{
			session.StartTurn(session.PlayerTeam);

			//select Hector
			session.OnConfirmationInputReceived(20, 13);

			//move one square to the right
			session.OnPositionHover(21, 13);

			//select the square to move him there.
			session.OnConfirmationInputReceived(21, 13);

			MenuClosedAction menData = session.OnNegationInputReceived()[0] as MenuClosedAction;
			Assert.IsNotNull(menData, "Did not pop a menu");
			Assert.IsTrue(!session.MenuOpen && session.UnitSelected, "Closed both selection & unit actions at once");

			session.OnNegationInputReceived();
			Assert.IsFalse(session.MenuOpen, "Failed to close all open windows");
		}

		[TestMethod]
		public void TestCancelMapMenu()
		{
			session.StartTurn(session.PlayerTeam);

			//select empty space
			session.OnConfirmationInputReceived(0, 0);

			MenuClosedAction menData = session.OnNegationInputReceived()[0] as MenuClosedAction;
			Assert.IsNotNull(menData, "Did not pop a menu");
			Assert.IsTrue(menData.ClosedMenu.MenuType == MenuTypes.Map, "Wasn't the map menu");
		}

		[TestMethod]
		public void TestGetUnitsInfo()
		{
			UnitRoster playerUnits = session.GetPlayerUnits();

			Assert.IsTrue(playerUnits.GetAllUnits().Count == 3, "Found more units than expected. (Expecting 3)");

			Assert.AreEqual(playerUnits.GetAllUnits().Find(s => s.InternalName.Equals("Hector")).InternalName, "Hector", "Could not find Hector");
			Assert.AreEqual(playerUnits.GetAllUnits().Find(s => s.InternalName.Equals("Eliwood")).InternalName, "Eliwood", "Could not find Eliwood");
			Assert.AreEqual(playerUnits.GetAllUnits().Find(s => s.InternalName.Equals("Serra")).InternalName, "Serra", "Could not find Serra");

			UnitRoster playerUnits2 = session.GetUnits(UnitTeam.team1);

			Assert.IsTrue(playerUnits2.GetAllUnits().Count == 3, "2 Found more units than expected. (Expecting 3)");

			Assert.AreEqual(playerUnits2.GetAllUnits().Find(s => s.InternalName.Equals("Hector")).InternalName, "Hector", "2 Could not find Hector");
			Assert.AreEqual(playerUnits2.GetAllUnits().Find(s => s.InternalName.Equals("Eliwood")).InternalName, "Eliwood", "2 Could not find Eliwood");
			Assert.AreEqual(playerUnits2.GetAllUnits().Find(s => s.InternalName.Equals("Serra")).InternalName, "Serra", "2 Could not find Serra");


			UnitRoster enemyUnits = session.GetUnits(UnitTeam.team2);

			Assert.IsTrue(enemyUnits.GetAllUnits().Count == 4, "Found more units than expected. (Expecting 4)");

			Assert.AreEqual(enemyUnits.GetAllUnits().Find(s => s.InternalName.Equals("Erik")).InternalName, "Erik", "Could not find Erik");

			try
			{
				UnitRoster noSuchTeam = session.GetUnits(UnitTeam.team5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(KeyNotFoundException), "Able to get roster for non-existing team");
			}
		}

		[TestMethod]
		public void TestManualSaveGameState()
		{
			bool isManualSave = true;
			SaveSessionBundle result = session.SaveGameState(isManualSave);
			Assert.IsNotNull(result, "SaveSessionBundle created successfully");
		}

		[TestMethod]
		public void TestEndTurn()
		{
			session.StartTurn(session.PlayerTeam);

			UnitRoster playerUnits = session.GetPlayerUnits();
			foreach (UnitEntity ue in playerUnits.GetAllUnits())
			{
				Assert.IsTrue(ue.HasActed == false, "2 Unit not ready to act");
			}

			session.EndPlayerTurn();

			playerUnits = session.GetPlayerUnits();
			foreach (UnitEntity ue in playerUnits.GetAllUnits())
			{
				Assert.IsTrue(ue.HasActed == true, "3 Unit still waiting to act");
			}
		}

		[TestMethod]
		public void TestStartTurn()
		{

			session.StartTurn(UnitTeam.team1);

			UnitRoster playerUnits = session.GetPlayerUnits();
			foreach (UnitEntity ue in playerUnits.GetAllUnits())
			{
				Assert.IsTrue(ue.HasActed == false, "Unit not ready to act");
			}
		}
	}
}