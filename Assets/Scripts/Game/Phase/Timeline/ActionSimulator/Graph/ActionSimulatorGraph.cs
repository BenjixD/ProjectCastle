using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionSimulatorGraph {
	public Dictionary<Vector2, SimulatorNode> nodes;
	public Board board;

	public ActionSimulatorGraph(Dictionary<Unit, UnitDisplacement> displacements, Board board) {
		this.nodes = new Dictionary<Vector2, SimulatorNode>();
		this.board = board;

		foreach(KeyValuePair<Unit, UnitDisplacement> pair in displacements) {
			SimulatedDisplacement sim = new SimulatedDisplacement(pair.Value);
			Vector2 startCoord = pair.Value.GetStartCoordinate();
			Vector2 endCoord = sim.GetNextSimulationStep(board);

			if(!nodes.ContainsKey(startCoord)) {
				nodes.Add(startCoord, new SimulatorNode(startCoord));
			}
			if(!nodes.ContainsKey(endCoord)) {
				nodes.Add(endCoord, new SimulatorNode(endCoord));
			}
			nodes[endCoord].AddEdge(new SimulatorEdge(nodes[startCoord], nodes[endCoord], sim));
		}
	}

	public Dictionary<Unit, SimulatedDisplacement> GetSimulationResults() {
		Dictionary<Unit, SimulatedDisplacement> results = new Dictionary<Unit, SimulatedDisplacement>();
		foreach(KeyValuePair<Vector2, SimulatorNode> pair in nodes) {
			SimulatorNode node = pair.Value;
			if(node.edges.Count > 1) {
				throw new Exception("Conflict still exists!");
			}

			if(node.edges.Count == 1) {
				results.Add(node.edges[0].sim.displacement.unit, node.edges[0].sim);
			}
		}

		return results;
	}

	public void Relax() {
		bool changed;
		do {
			Dictionary<Vector2, SimulatorNode> nextIter = new Dictionary<Vector2, SimulatorNode>();
			changed = false;

			foreach(KeyValuePair<Vector2, SimulatorNode> pair in nodes) {
				SimulatorNode node = pair.Value;
				if(IsRelaxed(node)) {
					//No Conflict Resolution Needed, update unit values
					foreach(SimulatorEdge edge in node.edges) {
						SimulatedDisplacement sim = edge.sim;
						Vector2 current = sim.GetCurrentVector();
						Vector2 next;

						if(!sim.conflict && !sim.outOfBounds) {
							next = sim.GetNextSimulationStep(board);
							changed = changed || next != current;
						} else {
							next = current; //do not move forward
						}

						//Create and push to simulation our new location
						if(!nextIter.ContainsKey(current)) {
							nextIter.Add(current, new SimulatorNode(current));	
						}
						if(!nextIter.ContainsKey(next)) {
							nextIter.Add(next, new SimulatorNode(next));
						}
						nextIter[next].AddEdge(new SimulatorEdge(nextIter[current], nextIter[next], sim));
					}
				} else {
					//Create and push to simulation our new location
					foreach(SimulatorEdge edge in node.edges) {
						SimulatedDisplacement sim = edge.sim;
						Vector2 current = sim.GetCurrentVector();
						Vector2 prev = sim.GetPreviousSimulationStep(board);

						sim.conflict = true;
						changed = changed || prev != current;

						//Create and push to simulation our new location
						if(!nextIter.ContainsKey(current)) {
							nextIter.Add(current, new SimulatorNode(current));
						}
						if(!nextIter.ContainsKey(prev)) {
							nextIter.Add(prev, new SimulatorNode(prev));
						}
						nextIter[prev].AddEdge(new SimulatorEdge(nextIter[current], nextIter[prev], sim));
					}
				}
			}
			nodes = nextIter;
		} while(changed);
	}

	private bool IsRelaxed(SimulatorNode thisNode) {
		//Check if multiple units on the same tile
		if(thisNode.edges.Count > 1) {
			return false;
		}

		//Check if two units are going to run (relative displacement) into each other by swapping
		foreach(SimulatorEdge edge in thisNode.edges) {
			if(edge.sim.displacement.type == UnitDisplacementType.RELATIVE) {
				SimulatorNode otherNode = nodes[edge.startNode.coordinate];
				if(otherNode != thisNode) {
					foreach(SimulatorEdge otherEdge in otherNode.edges) {
						if(otherEdge.startNode == thisNode &&
							otherEdge.sim.displacement.type == UnitDisplacementType.RELATIVE) {
							return false;
						}
					}
				}	
			}
		}

		return true;
	}

	//===================================COMPONENTS==========================================//
	public class SimulatorNode {
		public Vector2 coordinate;
		public List<SimulatorEdge> edges;

		public SimulatorNode(Vector2 coord) {
			coordinate = coord;
			edges = new List<SimulatorEdge>();
		}

		public void AddEdge(SimulatorEdge edge) {
			edges.Add(edge);
		}
	}

	public class SimulatorEdge {
		public SimulatorNode startNode;
		public SimulatorNode endNode;
		public SimulatedDisplacement sim;

		public SimulatorEdge(SimulatorNode start, SimulatorNode end, SimulatedDisplacement sim) {
			this.startNode = start;
			this.endNode = end;
			this.sim = sim;
		}
	}
}