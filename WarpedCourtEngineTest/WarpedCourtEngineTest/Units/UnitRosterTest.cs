using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine;

namespace WarpedCourtEngineTest
{
	[TestClass]
	public class UnitRosterTest
	{
		[TestMethod]
		public void TestAddUnits()
		{
			UnitRoster constructorRoster = new UnitRoster(new UnitEntity[1] { new UnitEntity() }, UnitTeam.team1);
			Assert.AreEqual(1, constructorRoster.GetAllUnits().Count, "Roster constructor add unit was not successful");

			UnitRoster roster = new UnitRoster(null, UnitTeam.team1);
			UnitEntity mu = new UnitEntity();

			Assert.IsTrue(roster.AddUnit(mu), "Roster add unit was not successful");

		}

		[TestMethod]
		public void TestAddDuplicateUnits()
		{
			UnitRoster roster = new UnitRoster(null, UnitTeam.team1);
			UnitEntity mu = new UnitEntity();

			Assert.IsTrue(roster.AddUnit(mu), "Original add unit not successful");
			Assert.IsFalse(roster.AddUnit(mu), "Duplicate add unit was not prevented");
		}

		[TestMethod]
		public void TestGetAllUnits()
		{
			UnitRoster roster = new UnitRoster(null, UnitTeam.team1);
			UnitEntity mu;
			mu = new UnitEntity();
			roster.AddUnit(mu);
			mu = new UnitEntity();
			roster.AddUnit(mu);
			Assert.AreEqual(2, roster.GetAllUnits().Count, "Multiple add units not supported");
		}


		[TestMethod]
		public void TestFindUnitByName()
		{
			UnitRoster roster = new UnitRoster(null, UnitTeam.team1);
			UnitEntity mu = new UnitEntity();
			string name = "Sebastian";
			mu.InternalName = name;

			roster.AddUnit(mu);
			Assert.AreEqual(mu, roster.GetUnitByName(name), "Roster GetUnitByName was not successful");
		}

		//[TestMethod]
		//public void TestFindUnitByPosition()
		//{
		//	UnitRoster roster = new UnitRoster(null, UnitTeam.playerTeam);
		//	int xPos = 6;
		//	int yPos = 4;
		//	UnitEntity mu = new UnitEntity(xPos, yPos);

		//	roster.AddUnit(mu);
		//	Assert.AreEqual(mu, roster.GetUnitAt(xPos, yPos), "Roster GetUnitByPosition was not successful");
		//}
	}
}
