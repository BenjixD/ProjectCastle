using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSimulator {

	private Dictionary<Unit, UnitDisplacement> displacements;
	private Board board;

	public ActionSimulator(Dictionary<Unit, UnitDisplacement> displacements, Board board) {
		this.displacements = displacements;
		this.board = board;
	}

	public Dictionary<Unit, SimulatedDisplacement> Simulate() {
		ActionSimulatorGraph graph = new ActionSimulatorGraph(displacements, board);
		graph.Relax();
		return graph.GetSimulationResults();
	}
}