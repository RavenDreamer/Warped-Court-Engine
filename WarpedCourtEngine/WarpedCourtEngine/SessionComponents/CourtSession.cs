using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarpedCourtEngine.Exceptions;

namespace WarpedCourtEngine
{
	public class CourtSession
	{
		ICombatEngine CombatEngine { get; set; }
		GameMap SessionMap { get; set; }
		UnitBook UnitBook { get; set; }

		private SelectionState state = SelectionState.NoSelection;
		private UnitTeam teamTurn = UnitTeam.none;
		private int turnCount = -1;

		Stack<MenuData> openMenus = new Stack<MenuData>();

		public bool MenuOpen
		{
			get
			{
				return openMenus.Count > 0;
			}
		}
		public bool UnitSelected
		{
			get
			{
				return selectedUnit != null;
			}
		}

		UnitEntity _selectedUnit;
		UnitEntity selectedUnit
		{
			get
			{
				return _selectedUnit;
			}
			set
			{
				if (value == null)
				{
					state = SelectionState.NoSelection;
					_selectedUnit = value;
					moveSquares = null;
					lastPath = new List<Position>(); ;
				}
				else
				{
					state = SelectionState.UnitSelected;
					_selectedUnit = value;
					//calculate moveSquares
					moveSquares = SessionMap.GenerateMovesquares(value);
				}

			}
		}

		/// <summary>
		/// Makes MapUnits from the Unitbook and otherwise laods the units onto the map.
		/// </summary>
		public void DeployUnits()
		{
			foreach(UnitRoster ur in UnitBook.GetAllUnits())
			{
				foreach (UnitEntity ue in ur.GetAllUnits())
				{
					SessionMap.AddUnit(ue, ue.InitalPosition);
					ue.HasActed = true;
				}
			}
		}

		//this is the region (in squares) that the unit can move within.
		UnitRange moveSquares { get; set; }
		//this is the path travelled to reach the last selected tile within the
		//movesquares
		List<Position> lastPath = new List<Position>();

		public UnitTeam PlayerTeam { get; set; }


		public void AddMap(GameMap map)
		{
			this.SessionMap = map;
		}

		public void AddUnitBook(UnitBook book)
		{
			this.UnitBook = book;
		}

		public void AddCombatEngine(CombatEngine engine, int seed)
		{
			switch (engine)
			{
				case WarpedCourtEngine.CombatEngine.GBA:
					this.CombatEngine = new FE7CombatEngine(new Random(seed));
					break;
				default:
					throw new NotImplementedException("Combat Engine Not Implemented");
			}

		}


		/// <summary>
		/// Returns a copy of the units, + the current turn # / team's turn
		/// </summary>
		/// <param name="isManualSave"></param>
		public SaveSessionBundle SaveGameState(bool isManualSave)
		{
			return new SaveSessionBundle()
			{
				PlayerTeam = this.PlayerTeam,
				CurrentTeamTurn = this.teamTurn,
				UB = this.UnitBook
			};
		}

		/// <summary>
		/// Goes through all units for the active player and sets "HasActed" to true. This will end the turn.
		/// </summary>
		public void EndPlayerTurn()
		{
			EndTurn(PlayerTeam);
		}

		/// <summary>
		/// Goes through all units for the active player and sets "HasActed" to true. This will end the turn for this team.
		/// </summary>
		/// <param name="team"></param>
		public void EndTurn(UnitTeam team)
		{
			if (teamTurn == UnitTeam.none) return;
			if(teamTurn != team)
			{
				throw new TurnOrderException("Not the player's turn");
			}
			foreach (UnitEntity ue in UnitBook.GetRoster(team).GetAllUnits())
			{
				ue.HasActed = true;
			}
		}

		///// <summary>
		///// Returns a list of actions available to the given unit, were it at the given position (presumably already
		///// calculated to be a viable movement target)
		///// </summary>
		///// <param name="unit"></param>
		///// <param name="newPosition"></param>
		///// <returns></returns>
		//public List<MapActionCommand> GetMapActionsForUnit(UnitEntity unit, Position newPosition)
		//{
		//	List<MapActionCommand> retList = new List<MapActionCommand>();
		//	//retList.Add(MapActionCommand.Wait); //wait always available.

		//	////get attack range, and check team relationship in relevant tiles
		//	//foreach (Position range in unit.AllAttackRanges())
		//	//{
		//	//	Position targ = newPosition + range;
		//	//	UnitTeam targTeam = GetTile(targ.xPos, targ.yPos);
		//	//	if (targ.xPos == unit.xPos && targ.yPos == unit.yPos) continue; //can't attack yourself

		//	//	if (targTeam != UnitTeam.none && targTeam != UnitTeam.playerTeam)
		//	//	{
		//	//		retList.Add(MapActionCommand.Attack);
		//	//		break;
		//	//	}
		//	//}

		//	////get utility range, and check team relationship in relevant tiles
		//	//foreach (Position range in unit.AllUtilityRanges())
		//	//{
		//	//	Position targ = newPosition + range;
		//	//	UnitTeam targTeam = GetTile(targ.xPos, targ.yPos);
		//	//	if (targ.xPos == unit.xPos && targ.yPos == unit.yPos) continue; //can't utility yourself

		//	//	if (targTeam != UnitTeam.none && targTeam == UnitTeam.playerTeam)
		//	//	{
		//	//		retList.Add(MapActionCommand.Heal);
		//	//		break;
		//	//	}
		//	//}

		//	return retList;
		//}

	

		/// <summary>
		/// Returns a UnitRoster of all Units in the Session for the given team
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public UnitRoster GetUnits(UnitTeam team)
		{
			return UnitBook.GetRoster(team);
		}

		public UnitRoster GetPlayerUnits()
		{
			return GetUnits(PlayerTeam);
		}

		/// <summary>
		/// Starts the given team's turn, ending the current turn if needed
		/// </summary>
		/// <param name="team"></param>
		public void StartTurn(UnitTeam team)
		{
			if (teamTurn != team)
			{
				EndTurn(teamTurn);
				teamTurn = team;
			}

			foreach (UnitEntity ue in UnitBook.GetRoster(team).GetAllUnits())
			{
				ue.HasActed = false;
			}
		}

		/// <summary>
		/// Returns information about the hover position - the underlying terrain tile
		/// and any units or objects currently occupying it.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public MapSelectionResponse OnPositionHover(int x, int y)
		{

			if (SessionMap == null)
			{
				throw new SessionComponentMissingException("No GameMap found");
			}

			if (selectedUnit != null && MenuOpen == false)
			{
				Position targ = new Position(x, y);
				if (moveSquares.moveRange.Contains(targ))
				{
					//finds a path to the given square, from existing path if possible.
					lastPath = SessionMap.FindPath(selectedUnit, targ, lastPath);
				}
			}

			return SessionMap.QueryTileContents(x, y);
		}

		/// <summary>
		/// Gets the stats of a unit at the given location
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public UnitEntity GetUnit(int x, int y)
		{
			if (SessionMap == null)
			{
				throw new SessionComponentMissingException("No GameMap found");
			}
			return SessionMap.GetUnitInfoByPosition(x, y);
		}

		/// <summary>
		/// Depending on the state, returns objects to render:
		///  - if no unit selected -> GetActions(x,y), OpenMenu();
		///  - if new unit selected -> SelectUnit(x,y);
		///  - if unit already selected -> GetActions(x,y [selectedUnit]), OpenMenu();
		///  - if In Menu -> SelectFromMenu();
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public List<GameAction> OnConfirmationInputReceived(int x, int y)
		{
			List<GameAction> retList = new List<GameAction>();

			switch (state)
			{
				case SelectionState.NoSelection:
					//attempt to select a unit
					if (SessionMap.QueryTileContents(x, y) == MapSelectionResponse.unitTile)
					{
						UnitEntity target = SessionMap.GetUnitInfoByPosition(x, y);
						//only select units who haven't already acted are valid
						if (target.HasActed == false)
						{
							SelectUnit(x, y);
							retList.Add(new UnitSelectedAction(selectedUnit));
							return retList;
						}
					}
					//open the map menu (e.g. end turn, etc.)
					openMenus.Push(GetActions());
					retList.Add(new MenuOpenedAction(openMenus.Peek()));
					return retList;

				case SelectionState.UnitSelected:
					//if the current position is in the valid move-space of the unit, 
					//generate the actions from there
					if (moveSquares.moveRange.Contains(new Position(x, y)))
					{
						//open the unit menu (attack, wait, etc.)
						openMenus.Push(GetActions(x, y));
						retList.Add(new MenuOpenedAction(openMenus.Peek()));
						return retList;
					}
					//the current position is past the valid move-space of the unit,
					//generate the actions from the last valid position
					else if (lastPath != null)
					{
						openMenus.Push(GetActions(lastPath.Last().xPos, lastPath.Last().yPos));
						retList.Add(new MenuOpenedAction(openMenus.Peek()));
						return retList;
					}
					else
					{
						throw new SessionStateInvalidException("Could not determine where to calculate actions from");
					}

				case SelectionState.MenuOpen:
					throw new NotImplementedException();
				default:
					throw new SessionStateInvalidException("Could not determine a course of action");
			}

		}

		/// <summary>
		/// Navigate backwards through a given menu, returning the previous menu, or unselecting the unit if 
		/// selected and no open menu
		/// </summary>
		/// <returns></returns>
		public List<GameAction> OnNegationInputReceived()
		{
			List<GameAction> retList = new List<GameAction>();

			if (MenuOpen)
			{
				retList.Add(new MenuClosedAction(openMenus.Pop()));
				return retList;

			}
			if (selectedUnit != null)
			{
				UnitEntity ue = selectedUnit;
				selectedUnit = null;

				retList.Add(new UnitDeselectedAction(ue));
				return retList;
			}

			//nothing to back out of.
			return retList;

		}

		/// <summary>
		/// Navigate through a given menu, return the action performed (if applicable), or a submenu (if applicable)
		/// </summary>
		/// <param name="menu"></param>
		/// <param name="action"></param>
		/// <param name="cir"></param>
		/// <returns></returns>
		public List<GameAction> OnConfirmationInputReceived(MenuTypes menu, string action)
		{
			List<GameAction> retList = new List<GameAction>();

			switch (menu)
			{
				case MenuTypes.Unit:
					switch(action)
					{
						case "Wait":
							retList.AddRange(MoveSelectedUnitAndWait());
							openMenus.Clear();
							return retList;
						case "Attack":
							//Go to Attack Details Menu (Pick Weapon / Pick Target)
							
							List<UnitEntity> targets = GetAttackTargets();
							List<IWeaponItem> weapons = selectedUnit.WeaponList.FindAll(c => c is FEWeapon);
							AttackDetailsMenuData attackDetails = new AttackDetailsMenuData(new List<string>() { "Attack" }) { MenuType = MenuTypes.AttackDetails, Targets = targets, Weapons = weapons, EngagementSquare = lastPath[lastPath.Count - 1] } ;
							openMenus.Push(attackDetails);

							retList.Add(new MenuOpenedAction(openMenus.Peek()));
							return retList;


						default:
							return null;
					}
				case MenuTypes.AttackDetails:
					switch(action)
					{
						case "Attack":
							//read attack details from the top most menu
							AttackDetailsMenuData admd = openMenus.Peek() as AttackDetailsMenuData;
							selectedUnit.EquipWeapon(admd.Weapons[0]);
						
							List<GameAction> results = CombatEngine.InitiateCombat(selectedUnit, admd.Targets[0], CalculateEngagementRange(admd.EngagementSquare,admd.Targets[0]));
							openMenus.Clear();

							return results;
						default:
							return null;
					}
				
				default:
					return null;
			}

		}

		private int CalculateEngagementRange(Position engageSquare, UnitEntity target)
		{
			
			Position targPos = SessionMap.GetUnityByEntity(target).Position;

			Position diff = engageSquare - targPos;

			return Math.Abs(diff.xPos) + Math.Abs(diff.yPos);
		}

		private List<UnitEntity> GetAttackTargets()
		{
			Position attackPosition = lastPath.Last();
			List<UnitEntity> retList = new List<UnitEntity>();


			foreach (Position pos in selectedUnit.AllAttackRanges())
			{
				UnitEntity unitEntity = SessionMap.QueryUnitPosition(attackPosition.xPos + pos.xPos, attackPosition.yPos + pos.yPos);
				if (unitEntity == null) continue;
				if (unitEntity.TeamID != selectedUnit.TeamID)
				{
					retList.Add(unitEntity);
				}
			}

			return retList;
		}

		/// <summary>
		/// Moves the selected unit to the given square, then calls the wait command
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private List<GameAction> MoveSelectedUnitAndWait()
		{
			List<GameAction> retList = new List<GameAction>();

			retList.AddRange(MoveUnit(selectedUnit, lastPath));
			
			//the "wait" part
			selectedUnit.HasActed = true;
			retList.Add(new UnitWaitAction(selectedUnit));
			selectedUnit = null;

			return retList;
		}


		/// <summary>
		/// Moves the unit along the path
		/// </summary>
		/// <param name="path"></param>
		/// <param name="position"></param>
		private List<GameAction> MoveUnit(UnitEntity unit, List<Position> path)
		{
			List<GameAction> retList = new List<GameAction>();
			MapUnit mu = SessionMap.GetUnityByEntity(unit);
			foreach(Position pnode in path)
			{
				bool isFinal = path.Last().Equals(pnode);
				if (!mu.Position.Equals(pnode))
				{
					SessionMap.MoveUnit(mu, pnode, isFinal);
					retList.Add(new UnitMoveAction(mu, pnode, isFinal));
					//Unit Moved event
				}
			}

			return retList;
		}

		/// <summary>
		/// Gets actions for the currently selected unit at the given position
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private MenuData GetActions(int x, int y)
		{
			//UnitActionCommand enum
			List<string> unitActions = new List<string>
			{
				//always add wait
				UnitActionCommand.Wait.ToString()
			};

			//attack
			foreach (Position pos in selectedUnit.AllAttackRanges())
			{
				UnitEntity unitEntity = SessionMap.QueryUnitPosition(x + pos.xPos, y + pos.yPos);
				if (unitEntity == null) continue;
				if (unitEntity.TeamID != selectedUnit.TeamID)
				{
					unitActions.Add(UnitActionCommand.Attack.ToString());
					break;
				}
			}

			//Staff
			foreach (Position pos in selectedUnit.AllUtilityRanges())
			{
				UnitEntity unitEntity = SessionMap.QueryUnitPosition(x + pos.xPos, y + pos.yPos);
				if (unitEntity == null) continue;
				if (unitEntity.TeamID == selectedUnit.TeamID)
				{
					unitActions.Add(UnitActionCommand.Staff.ToString());
					break;
				}
			}

			return new MenuData(unitActions) { MenuType = MenuTypes.Unit };
		}

		/// <summary>
		/// Get Position independent actions. This means the main map menu.
		/// </summary>
		/// <returns></returns>
		private MenuData GetActions()
		{
			List<string> mapActions = new List<string>(Enum.GetNames(typeof(MapActionCommand)));
			return new MenuData(mapActions) { MenuType = MenuTypes.Map };
		}

		/// <summary>
		/// Changes the state to "Unit selected", removes the previous Unit Selected (and clears it's movesquares), then 
		/// calculates the new unit's moveSquares and sets the unit at the given location to be the new selected unit
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private void SelectUnit(int x, int y)
		{
			selectedUnit = SessionMap.GetUnitInfoByPosition(x, y);
			lastPath.Add(new Position(x, y)); //always start with the current position, no matter what.
			//call eventHandlers here as appropriate, or something?
		}
		
	}
}
