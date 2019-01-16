using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFrameAnim : FrameAnim {

	public StunFrameAnim(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO stun anim
		return true;
	}
}