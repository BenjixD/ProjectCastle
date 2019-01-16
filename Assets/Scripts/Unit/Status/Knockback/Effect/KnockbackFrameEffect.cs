using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFrameEffect : FrameEffect {

	public KnockbackFrameEffect(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		if(board.CheckCoord(sim.GetCurrentVector())) {
			Tile tile = board.GetTile(sim.GetCurrentVector());
			return tile.tileType == TileType.PLAINS;
		}
		return false;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
	{
		return true;
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		Knockback kb = (Knockback)action;
		Vector2 movement = kb.vector;
		return new RelativeDisplacement(unit, movement);
	}
}