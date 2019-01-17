using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinFrameEffect : KnockbackFrameEffect {

	public const int STUN_DURATION = 3;

	public PinFrameEffect(Action instance) : base(instance) {}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
	{
		Unit victim = sim.displacement.unit;
		// TODO: also check if the victim was pushed against a wall tile/structure
		if (sim.conflict) {
			victim.statusController.AddStatus(new StunEffect(STUN_DURATION));
			Debug.Log(victim + " was stunned!");
		}
		return true;
	}

	public override UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board)
	{
		Pin kb = (Pin)action;
		Vector2 movement = kb.vector;
		return new RelativeDisplacement(unit, movement);
	}
}