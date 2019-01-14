using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFrame : Frame {

	public const int DMG = 5;

	public PoisonFrame(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
	{
		Unit victim = sim.displacement.unit;
		victim.TakeDamage(DMG);
		return true;
	}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		//TODO poison anim
		return true;
	}
}
