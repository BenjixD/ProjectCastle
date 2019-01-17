using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotFrameEffectAttack : FrameEffect {

	HitboxTypes[] hitTypesOrder = new HitboxTypes[] { HitboxTypes.SOUR, HitboxTypes.SWEET, HitboxTypes.SWEET, HitboxTypes.OK, HitboxTypes.SOUR, HitboxTypes.SOUR };
	Dictionary<HitboxTypes, int> damageValues = new Dictionary<HitboxTypes, int>()
	{
		{ HitboxTypes.SWEET, 30 },
		{ HitboxTypes.OK, 30 },
		{ HitboxTypes.SOUR, 15 }
	};

	public PowerShotFrameEffectAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon silence
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect(0))) {
			return false;
		}
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		Vector2 frontVect = Action.GetDirectionVector(dir);
		if (frontVect == Vector2.zero) {
			// bad direction input
			return false;
		}
		for (int i = 0; i <= hitTypesOrder.Length; i++) {
			if (board.CheckCoord(sim.result + frontVect * (i + 1))) {
				Tile targetTile = board.GetTile(sim.result + frontVect * (i + 1));
				Unit target = targetTile.unit;
				if (target) {
					target.TakeDamage(damageValues[hitTypesOrder[i]]);
					if (hitTypesOrder[i] == HitboxTypes.SWEET) {
						target.statusController.QueueAddStatus(new PinEffect(frontVect));
						Debug.Log("Ouch! " + target.unitName + " just got knocked back and took " + damageValues[hitTypesOrder[i]] + " damage!");
					}
					else {
						Debug.Log("Ouch! " + target.unitName + " just took " + damageValues[hitTypesOrder[i]] + " damage!");
					}
					// Hit one target only
					break;
				}
			}
		}
		return true;
	}
}
