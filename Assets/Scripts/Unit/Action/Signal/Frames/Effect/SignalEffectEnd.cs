using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalEffectEnd : FrameEffect {
	
	public SignalEffectEnd(Action instance) : base(instance) {}

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
        // ending lag doesn't need to fail
        return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
        // no effect
		return true;
	}
}