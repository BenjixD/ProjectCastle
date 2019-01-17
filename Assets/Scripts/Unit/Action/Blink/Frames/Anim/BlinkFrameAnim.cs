using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFrameAnim : MoveFrameAnim {

	public BlinkFrameAnim(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		Unit unit = sim.displacement.unit;
		bool collision = sim.conflict;
		if(!collision) {
			//Unit's logical location is already on the target
			//TODO: Play blink anim
			unit.gameObject.transform.position = unit.tile.gameObject.transform.position;
			return true;
		} else {
			//TODO: Play some kind of collision
			return false;
		}
	}
}