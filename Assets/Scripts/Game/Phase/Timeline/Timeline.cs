using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline : Phase {
	public int maxFrame;
	public float frameDelay;

    public ActionType[] actionOrder;

    public override IEnumerator Play(Game game, IEnumerator next) {
        game.uiManager.ShowTimelinePhaseUI();
        Debug.Log("Playing Timeline!");
		for(int i = 0 ; i < maxFrame; i++) {
            game.uiManager.UpdateTimelineDisplay(i);
            PlayFrame(game.players, game.board);
			yield return new WaitForSeconds(frameDelay);
		}

		StartCoroutine(next);
		yield return null;
	}

	private void PlayFrame(List<Player> players, Board board) {
		//Queue frames into the correct order
		Dictionary<ActionType, List<KeyValuePair<Unit, Command>>> actionDict = InitializeActionOrder(players);

		//Play Execution Based on Order
		foreach(ActionType type in actionOrder) {
			ExecuteSingleFrameOrder(actionDict[type], players, board);
		}

		//Queue Status Effects Actions
		List<KeyValuePair<Unit, Command>> statusCommands = FinalizeStatusEffect(players);
		ExecuteSingleFrameOrder(statusCommands, players, board);

		//kill all units who satisfy death condition
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				KillUnitWhenDead(unit, board);
			}
		}
	}

	private Dictionary<ActionType, List<KeyValuePair<Unit, Command>>> InitializeActionOrder(List<Player> players) {
		Dictionary<ActionType, List<KeyValuePair<Unit, Command>>> actionDict = new Dictionary<ActionType, List<KeyValuePair<Unit, Command>>>();

		//Adding ActionTypes
		foreach (ActionType type in actionOrder) {
            actionDict.Add(type, new List<KeyValuePair<Unit, Command>>());	
		}

		//Put the unit actions in the correct queue
		foreach (Player player in players) {
			foreach(Unit unit in player.units) {
				//queue move
				if(unit.plan.GetRemainingFramesCount() > 0) {
					Command peek = unit.plan.PeekNext().Value;
					ActionType actionType = peek.type;

					//Interrupt Next Skill
					if(peek.frame.IsInterrupted(unit.statusController)) {
						unit.plan.InterruptNext(new Wait(null));
					}

					Command command = unit.plan.ExecuteNext().Value;
					actionType = command.type;
					actionDict[actionType].Add(new KeyValuePair<Unit, Command>(unit, command));
				}
			}
		}

		return actionDict;
	}

	private void ExecuteSingleFrameOrder(List<KeyValuePair<Unit, Command>> commands, List<Player> players, Board board) {
		Dictionary<Unit, UnitDisplacement> displacements = GetAggregatedDisplacements(players, commands, board);
		ActionSimulator simulator = new ActionSimulator(displacements, board);
		Dictionary<Unit, SimulatedDisplacement> result = simulator.Simulate();
		foreach(KeyValuePair<Unit, Command> pair in commands) {
			Unit unit = pair.Key;
			Command command = pair.Value;
			SimulatedDisplacement sim = result[unit];
			if(command.frame.CanExecute(sim, command.dir, board)) {
				sim.displacement.unit.FaceDirection(command.dir);
				command.frame.ExecuteEffect(sim, command.dir, board);
				Board.MoveUnit(sim, board);
				command.frame.ExecuteAnimation(sim, command.dir, board);
			}
		}
	}

	private List<KeyValuePair<Unit, Command>> FinalizeStatusEffect(List<Player> players) {
		List<KeyValuePair<Unit, Command>> statusCommands = new List<KeyValuePair<Unit, Command>>();
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				unit.statusController.DecrementDuration(1);
				unit.statusController.ExpireStatus();
				unit.statusController.ApplyAddStatusEffects();
				unit.statusController.ApplyRemoveStatusEffects();
				foreach(KeyValuePair<string, StatusEffect> pair in unit.statusController) {
					statusCommands.Add(new KeyValuePair<Unit, Command>(unit, pair.Value.GetCommand()));	
				}
			}
		}

		return statusCommands;
	}

	private Dictionary<Unit, UnitDisplacement> GetAggregatedDisplacements(List<Player> players, List<KeyValuePair<Unit, Command>> commands, Board board) {
		Dictionary<Unit, UnitDisplacement> results = new Dictionary<Unit, UnitDisplacement>();
		
		foreach(KeyValuePair<Unit, Command> pair in commands) {
			Unit unit = pair.Key;
			Command command = pair.Value;
			UnitDisplacement displacement = command.frame.GetDisplacement(unit, command.dir, board);
			results.Add(unit, displacement);
		}

		//Add all units who wait
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				if(!results.ContainsKey(unit)) {
					results.Add(unit, new AbsoluteDisplacement(unit, unit.tile.coordinate));
				}
			}
		}

		return results;
	}

	private void KillUnitWhenDead(Unit unit, Board board) {
		if(unit.IsDead()) {
			//TODO: Death Anim then death
		}
	}
}