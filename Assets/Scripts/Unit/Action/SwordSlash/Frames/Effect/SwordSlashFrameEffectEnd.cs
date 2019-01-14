using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashFrameEffectEnd : FrameEffect {

	public SwordSlashFrameEffectEnd(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
        // ending lag doesn't need to fail
        return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
        // no effect
		return true;
	}
}