using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinFrameEffect : KnockbackFrameEffect {

	public const int STUN_DURATION = 3;

	public PinFrameEffect(Action instance) : base(instance) {}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
	{
		Unit victim = sim.displacement.unit;
		victim.FaceDirection(Action.GetOppositeDirection(dir));
		// TODO: also check if the victim was pushed against a wall tile/structure
		if (sim.conflict) {
			victim.statusController.QueueAddStatus(new StunEffect(STUN_DURATION));
			Debug.Log(victim + " was stunned!");
		}
		return true;
	}
}