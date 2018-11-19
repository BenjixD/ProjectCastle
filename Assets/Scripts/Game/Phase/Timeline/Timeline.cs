using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline : Phase {
	public int maxFrame;
	public float frameDelay;

    public UIManager uiManager;

    public ActionType[] actionOrder;

    public override IEnumerator Play(Game game, IEnumerator next) {
        Debug.Log("Playing Timeline!");
		for(int i = 0 ; i < maxFrame; i++) {
            uiManager.UpdateTimelineDisplay(i);
            PlayFrame(game.players, game.board);
			yield return new WaitForSeconds(frameDelay);
		}

		StartCoroutine(next);
		yield return null;
	}

	private void PlayFrame(List<Player> players, Board board) {
		Dictionary<ActionType, Queue<Unit>> actionDict = new Dictionary<ActionType, Queue<Unit>>();

		//Adding ActionTypes
		foreach (ActionType type in actionOrder) {
            actionDict.Add(type, new Queue<Unit>());	
		}

		//Put the unit actions in the correct queue
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				if(unit.plan.Count > 0) {
					ActionType actionType = unit.plan.Peek().type;
                    actionDict[actionType].Enqueue(unit);
				}
			}
		}

		//Play Execution Based on Order
		foreach(ActionType type in actionOrder) {
			Queue<Unit> actions = FilterCanExecute(actionDict[type], board);
			HashSet<Unit> toExecute = UnitsToExecute(actions);
			Dictionary<Unit, UnitDisplacement> displacements = GetDisplacements(players, actions, board);
			List<SimulatedDisplacement> result = SimulateDisplacements(displacements, board);

			foreach(SimulatedDisplacement sim in result) {
				Unit unit = sim.displacement.unit;
				if(toExecute.Contains(unit)) {
					Command command = unit.plan.Dequeue();
					command.frame.ExecuteEffect(sim, command.dir, board);
					sim.DisplaceUnit(board);
					command.frame.ExecuteAnimation(sim, command.dir, board);
				}
			}
		}

		//kill all units who satisfy death condition
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				KillUnitWhenDead(unit, board);
			}
		}
	}

	private Queue<Unit> FilterCanExecute(Queue<Unit> units, Board board) {
		Queue<Unit> filtered = new Queue<Unit>();
		while(units.Count > 0) {
			Unit unit = units.Dequeue();
			Command command = unit.plan.Peek();
			if(command.frame.CanExecute(unit, command.dir, board)) {
				filtered.Enqueue(unit);
			}
		}
		return filtered;
	}

	private HashSet<Unit> UnitsToExecute(Queue<Unit> units) {
		HashSet<Unit> result = new HashSet<Unit>();
		Queue<Unit> clone = new Queue<Unit>(units);
		while(clone.Count > 0) {
			result.Add(clone.Dequeue());
		}

		return result;
	}

	private Dictionary<Unit, UnitDisplacement> GetDisplacements(List<Player> players, Queue<Unit> units, Board board) {
		Dictionary<Unit, UnitDisplacement> results = new Dictionary<Unit, UnitDisplacement>();
		//Add all units making actions
		while(units.Count > 0) {
			Unit unit = units.Dequeue();
			Command command = unit.plan.Peek();
			UnitDisplacement displacement = command.frame.GetDisplacement(unit, command.dir, board);

			//Amalgamate displacement
			if(results.ContainsKey(unit)) {
				results[unit] = results[unit] + displacement;
			} else {
				results.Add(unit, displacement);				
			}
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

	private List<SimulatedDisplacement> SimulateDisplacements(Dictionary<Unit, UnitDisplacement> units, Board board) {
		Dictionary<Vector2, Queue<SimulatedDisplacement>> simulation = new Dictionary<Vector2, Queue<SimulatedDisplacement>>();
		List<SimulatedDisplacement> results = new List<SimulatedDisplacement>();

		//Initialize Simulation
		foreach(KeyValuePair<Unit, UnitDisplacement> pair in units) {
			SimulatedDisplacement sim = new SimulatedDisplacement(pair.Value, pair.Value.GetTargetCoordinate());
			Vector2 next = sim.GetNextSimulationStep();
			if(!simulation.ContainsKey(next)) {
				simulation.Add(next, new Queue<SimulatedDisplacement>());
			}
			simulation[next].Enqueue(sim);
		}

		//Simulate until relaxed
		simulation = ResolveConflicts(simulation);
		//All simulation entries should only have one unit at this point
		foreach(KeyValuePair<Vector2, Queue<SimulatedDisplacement>> pair in simulation) {
			results.Add(pair.Value.Dequeue());
		}

		return results;
	}

	private Dictionary<Vector2, Queue<SimulatedDisplacement>> ResolveConflicts(Dictionary<Vector2, Queue<SimulatedDisplacement>> simulation) {
		bool changed;

		do {
			Dictionary<Vector2, Queue<SimulatedDisplacement>> nextIter =  new Dictionary<Vector2, Queue<SimulatedDisplacement>>();
			changed = false;

			foreach(KeyValuePair<Vector2, Queue<SimulatedDisplacement>> pair in simulation) {
				Queue<SimulatedDisplacement> simQueue = pair.Value;
				if(simQueue.Count < 2) {
					//No Conflict Resolution Needed, update unit values
					while(simQueue.Count > 0) {
						SimulatedDisplacement sim = simQueue.Dequeue();
						Vector2 current = sim.current;
						Vector2 next;

						if(!sim.conflict) {
							next = sim.GetNextSimulationStep();
							changed = changed || next != current;
						} else {
							next = current; //do not move forward
						}

						//Create and push to simulation our new location
						if(!nextIter.ContainsKey(next)) {
							nextIter.Add(next, new Queue<SimulatedDisplacement>());
						}
						nextIter[next].Enqueue(sim);
					}
				} else {
					//Create and push to simulation our new location
					while(simQueue.Count > 0) {
						SimulatedDisplacement sim = simQueue.Dequeue();
						sim.conflict = true;
						Vector2 current = sim.current;
						Vector2 prev = sim.GetPreviousSimulationStep();
						changed = changed || prev != current;

						//Create and push to simulation our new location
						if(!nextIter.ContainsKey(prev)) {
							nextIter.Add(prev, new Queue<SimulatedDisplacement>());
						}
						nextIter[prev].Enqueue(sim);
					}
				}
			}

			//Shallow Copy
			simulation = nextIter;
		} while(changed);

		return simulation;
	}

	private void KillUnitWhenDead(Unit unit, Board board) {
		if(unit.IsDead()) {
			//TODO: Death Anim then death
		}
	}
}