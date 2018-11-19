using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFrame : Frame {
	public override bool CanExecute(Unit unit, Direction dir, Board board) {
		Vector2 next = GetRelativeDisplacement(unit, dir, board);
		return board.CheckCoord(next);
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
		}else {
			//Play some kind of collision
			return false;
		}
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		Vector2 relative = GetRelativeDisplacement(unit, dir, board);
		return new RelativeDisplacement(unit, relative);
	}

	private Vector2 GetRelativeDisplacement(Unit unit, Direction dir, Board board) {
		Direction absoluteDir = (Direction)(((int)relativeDir + (int)dir) % 4);
		Vector2 coord = unit.tile.coordinate;

		if(absoluteDir == Direction.UP) {
			return new Vector2(-1, 0);
		} else if (absoluteDir == Direction.RIGHT) {
			return new Vector2(0, 1);
		} else if (absoluteDir == Direction.DOWN) {
			return new Vector2(1, 0);
		} else if (absoluteDir == Direction.LEFT) {
			return new Vector2(0, -1);
		} else {
			return coord;
		}
	}
}