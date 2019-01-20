using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFrameEffect : FrameEffect {

	public BlinkFrameEffect(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon snare
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect(0))) {
			return false;
		}
		if (board.CheckCoord(sim.GetCurrentVector())) {
			Tile tile = board.GetTile(sim.GetCurrentVector());
			return tile.tileType == TileType.PLAINS;
		}
		return false;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		Blink blink = (Blink)action;
		Vector2 movement = Action.GetDirectionVector(dir) * blink.distance;
		return new RelativeDisplacement(unit, movement);
	}
}