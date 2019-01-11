using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFrame : Frame {

	public KnockbackFrame(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		if(board.CheckCoord(sim.GetCurrentVector())) {
			Tile tile = board.GetTile(sim.GetCurrentVector());
			return tile.tileType == TileType.PLAINS;
		}
		return false;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		Unit unit = sim.displacement.unit;
		bool collision = sim.conflict;
		if(!collision) {
			//Unit's logical location is already on the target
			unit.gameObject.transform.position = unit.tile.gameObject.transform.position;
			return true;
		} else {
			//Play some kind of collision
			return false;
		}
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		Vector2 movement = GetMovement(unit, dir, board);
		return new RelativeDisplacement(unit, movement);
	}

	private Vector2 GetMovement(Unit unit, Direction dir, Board board) {
		Direction absoluteDir = (Direction)(((int)relativeDir + (int)dir) % 4);

		if(absoluteDir == Direction.UP) {
			return new Vector2(-1, 0);
		} else if (absoluteDir == Direction.RIGHT) {
			return new Vector2(0, 1);
		} else if (absoluteDir == Direction.DOWN) {
			return new Vector2(1, 0);
		} else if (absoluteDir == Direction.LEFT) {
			return new Vector2(0, -1);
		} else {
			return new Vector2(0, 0);
		}
	}
}