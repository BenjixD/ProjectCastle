using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFrameEffect : FrameEffect {

	public StunFrameEffect(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}
}