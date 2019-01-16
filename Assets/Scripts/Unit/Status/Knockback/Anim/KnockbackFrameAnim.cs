using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFrameAnim : FrameAnim {

	public KnockbackFrameAnim(Action instance) : base(instance) {}


	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		Unit unit = sim.displacement.unit;
		bool collision = sim.conflict;
		if(!collision) {
			//Unit's logical location is already on the target
			unit.gameObject.transform.position = unit.tile.gameObject.transform.position;
			return true;
		} else {
			//Play some kind of collision
			return false;
		}
	}
}