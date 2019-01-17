using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFrameAnim : FrameAnim {

	public PoisonFrameAnim(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO poison anim
		return true;
	}
}
