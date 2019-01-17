using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFrameEffect : FrameEffect {

	public const int DMG = 5;

	public PoisonFrameEffect(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		Unit victim = sim.displacement.unit;
		victim.TakeDamage(DMG);
		return true;
	}
}
