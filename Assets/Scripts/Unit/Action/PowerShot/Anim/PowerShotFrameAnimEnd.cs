using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotFrameAnimEnd : FrameAnim {

	public PowerShotFrameAnimEnd(Action instance) : base(instance) {}
	
	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO ending lag anim
		return true;
	}
}