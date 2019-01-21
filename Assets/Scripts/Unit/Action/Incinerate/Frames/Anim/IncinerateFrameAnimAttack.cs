using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerateFrameAnimAttack : FrameAnim {

	public IncinerateFrameAnimAttack(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO attack anims
		return true;
	}
}
