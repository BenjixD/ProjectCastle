using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerateFrameAnimStart : FrameAnim {

	public IncinerateFrameAnimStart(Action instance) : base(instance) {}
	
	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO startup anim
		return true;
	}
}
