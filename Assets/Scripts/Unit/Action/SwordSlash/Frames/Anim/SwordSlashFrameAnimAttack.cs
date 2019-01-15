using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashFrameAnimAttack : FrameAnim {

	public const int DMG = 60;

	public SwordSlashFrameAnimAttack(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO attack anims
		return true;
	}
}
