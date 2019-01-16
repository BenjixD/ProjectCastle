using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaitFrameAnim : FrameAnim {

	public WaitFrameAnim(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}
}