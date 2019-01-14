using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePushFrameAnimAttack : FrameAnim {

	public LancePushFrameAnimAttack(Action instance) : base(instance) {}
	
	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
        // TODO attack anims
        return true;
	}
}
