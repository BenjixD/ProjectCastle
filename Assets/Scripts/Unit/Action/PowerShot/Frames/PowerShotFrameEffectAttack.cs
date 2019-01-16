﻿using System.Collections;
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
		// TODO Fail upon stun or silence
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO deal damage to enemies on tiles
		Vector2 frontVect = Action.GetDirectionVector(dir);
		if (frontVect == Vector2.zero) {
			// bad direction input
			return false;
		}
		for (int i = 0; i <= hitTypesOrder.Length; i++) {
			if (board.CheckCoord(sim.result + frontVect * (i + 1))) {
				Tile t = board.GetTile(sim.result + frontVect * (i + 1));
				if (t.unit) {
					t.unit.TakeDamage(damageValues[hitTypesOrder[i]]);
					if (hitTypesOrder[i] == HitboxTypes.SWEET) {
						t.unit.statusController.QueueAddStatus(new PinEffect(dir));
						Debug.Log("Ouch! " + t.unit.unitName + " just got knocked back and took " + damageValues[hitTypesOrder[i]] + " damage!");
					}
					else {
						Debug.Log("Ouch! " + t.unit.unitName + " just took " + damageValues[hitTypesOrder[i]] + " damage!");
					}
					// Hit one target only
					break;
				}
			}
		}
		return true;
	}
}
