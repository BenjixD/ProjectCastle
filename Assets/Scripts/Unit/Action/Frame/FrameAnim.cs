using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FrameAnim {
	public Action action;
	
	public FrameAnim(Action instance) {
		this.action = instance;
	}

	public abstract bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board);
}