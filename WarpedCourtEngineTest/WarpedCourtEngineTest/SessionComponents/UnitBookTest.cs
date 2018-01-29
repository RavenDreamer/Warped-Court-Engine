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
	public class UnitBookTest
	{

		[TestMethod]
		public void TestAddRoster()
		{
			UnitBook book = new UnitBook(UnitTeam.team1);

			UnitRoster rosterOne = new UnitRoster(null, UnitTeam.team1);
			UnitEntity mu1 = new UnitEntity();
			rosterOne.AddUnit(mu1);

			//expect success the first time	
			Assert.IsTrue(book.AddRoster(rosterOne, TeamRelation.player), "Roster not added to GameMap");

			//and failure the next
			Assert.IsFalse(book.AddRoster(rosterOne, TeamRelation.player), "Duplicate roster added");

			UnitRoster rosterTwo = new UnitRoster(null, UnitTeam.team2);
			UnitEntity mu2 = new UnitEntity();
			rosterTwo.AddUnit(mu2);

			Assert.IsTrue(book.AddRoster(rosterTwo, TeamRelation.enemy), "second roster not successfully added");
		}



		//[TestMethod]
		//public void TestSelectionResponse()
		//{
		//	UnitBook validMap = new UnitBook(5, 6, new TerrainType[30]);
		//	UnitRoster mapRoster = new UnitRoster(new UnitEntity[1] { new UnitEntity(0, 0) }, UnitTeam.playerTeam);
		//	UnitRoster enemyRoster = new UnitRoster(new UnitEntity[1] { new UnitEntity(0, 2) }, UnitTeam.team1);
		//	UnitRoster neutralRoster = new UnitRoster(new UnitEntity[1] { new UnitEntity(2, 0) }, UnitTeam.team2);

		//	validMap.AddRoster(mapRoster);
		//	validMap.AddRoster(enemyRoster, true);
		//	validMap.AddRoster(neutralRoster);

		//	Assert.AreEqual(MapSelectionResponse.emptyTile, validMap.SelectionTriggered(0, 5));

		//	Assert.AreEqual(MapSelectionResponse.playerTeam, validMap.SelectionTriggered(0, 0));

		//	Assert.AreEqual(MapSelectionResponse.enemyTeam, validMap.SelectionTriggered(0, 2));

		//	Assert.AreEqual(MapSelectionResponse.neutralTeam, validMap.SelectionTriggered(2, 0));
		//}

		//[TestMethod]
		//public void TestWaitCommand()
		//{
		//	UnitBook map = new UnitBook(5, 6, new TerrainType[30]);

		//	UnitRoster roster = new UnitRoster(null, UnitTeam.playerTeam);
		//	UnitEntity player = new UnitEntity(1, 1);
		//	roster.AddUnit(player);
		//	map.AddRoster(roster);

		//	List<MapActionCommand> waitAction = map.GetMapActionsForUnit(player, new Position(1, 1));
		//	Assert.IsTrue(waitAction.Contains(MapActionCommand.Wait), "Wait action not added");

		//	List<MapActionCommand> waitActionMove = map.GetMapActionsForUnit(player, new Position(0, 1));
		//	Assert.IsTrue(waitActionMove.Contains(MapActionCommand.Wait), "Wait action not added");
		//}

		//[TestMethod]
		//public void TestAttackCommand()
		//{
		//	UnitBook map = new UnitBook(5, 6, new TerrainType[30]);

		//	UnitRoster roster = new UnitRoster(null, UnitTeam.playerTeam);
		//	UnitEntity player = new UnitEntity(1, 1);
		//	roster.AddUnit(player);
		//	map.AddRoster(roster);

		//	UnitRoster enemyRoster = new UnitRoster(null, UnitTeam.team1);
		//	UnitEntity enemy = new UnitEntity(0, 1);
		//	enemyRoster.AddUnit(enemy);
		//	map.AddRoster(enemyRoster);


		//	List<MapActionCommand> waitAndAttackActions = map.GetMapActionsForUnit(player, new Position(1, 1));
		//	Assert.IsTrue(waitAndAttackActions.Contains(MapActionCommand.Attack), "Attack action not added");

		//	List<MapActionCommand> waitActionOnly = map.GetMapActionsForUnit(player, new Position(2, 2));
		//	Assert.IsFalse(waitActionOnly.Contains(MapActionCommand.Attack), "Attack action added inappropriately");

		//	List<MapActionCommand> noAttackSameSpace = map.GetMapActionsForUnit(player, new Position(0, 1));
		//	Assert.IsFalse(noAttackSameSpace.Contains(MapActionCommand.Attack), "Attack action added inappropriately");
		//}

		//[TestMethod]
		//public void TestHealCommand()
		//{
		//	UnitBook map = new UnitBook(5, 6, new TerrainType[30]);

		//	UnitRoster roster = new UnitRoster(null, UnitTeam.playerTeam);
		//	UnitEntity player = new UnitEntity(2, 1);
		//	UnitEntity teammate = new UnitEntity(0, 1);
		//	roster.AddUnit(player);
		//	roster.AddUnit(teammate);
		//	map.AddRoster(roster);


		//	List<MapActionCommand> waitAndHealActions = map.GetMapActionsForUnit(player, new Position(2, 1));
		//	Assert.IsTrue(waitAndHealActions.Contains(MapActionCommand.Heal), "Heal action not added");

		//	List<MapActionCommand> waitActionOnly = map.GetMapActionsForUnit(player, new Position(3, 1));
		//	Assert.IsFalse(waitActionOnly.Contains(MapActionCommand.Heal), "Heal action inappropriately added");
		//}


	}
}
