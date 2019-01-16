using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalEffectStart : FrameEffect {
	
	public SignalEffectStart(Action instance) : base(instance) {}

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
        // True if not disabled
        return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
        // cancel move if disabled?
		return true;
	}
}