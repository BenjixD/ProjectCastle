using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaitFrameEffect : FrameEffect {

	public WaitFrameEffect(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		Vector2 relative = GetRelativeDisplacement(unit, dir, board);
		return new RelativeDisplacement(unit, relative);
	}

	private Vector2 GetRelativeDisplacement(Unit unit, Direction dir, Board board) {
		return new Vector2(0,0);
	}
}