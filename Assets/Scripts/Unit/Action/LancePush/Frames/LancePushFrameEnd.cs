﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePushFrameEnd : Frame {
	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// ending lag doesn't need to fail
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// no effect
		return true;
	}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO ending lag anim
		return true;
	}
}