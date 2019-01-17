using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotFrameEffectAttack : FrameEffect {

	private Dictionary<Vector2, HitboxType> targetTiles;
	Dictionary<HitboxType, int> damageValues = new Dictionary<HitboxType, int>()
	{
		{ HitboxType.SWEET, 30 },
		{ HitboxType.OK, 30 },
		{ HitboxType.SOUR, 15 }
	};

	public PowerShotFrameEffectAttack(Action instance) : base(instance) {}

	private void InitializeTargetTiles(Direction dir) {
		targetTiles = new Dictionary<Vector2, HitboxType>();
		Vector2 frontVect = Action.GetDirectionVector(dir);
		targetTiles.Add(frontVect, HitboxType.SOUR);
		targetTiles.Add(frontVect * 2, HitboxType.SWEET);
		targetTiles.Add(frontVect * 3, HitboxType.SWEET);
		targetTiles.Add(frontVect * 4, HitboxType.OK);
		targetTiles.Add(frontVect * 5, HitboxType.SOUR);
		targetTiles.Add(frontVect * 6, HitboxType.SOUR);
	}

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
		InitializeTargetTiles(dir);
		foreach (KeyValuePair<Vector2, HitboxType> pair in targetTiles) {
			if (board.CheckCoord(sim.result + pair.Key)) {
				Tile targetTile = board.GetTile(sim.result + pair.Key);
				Unit target = targetTile.unit;
				if (target) {
					target.TakeDamage(damageValues[pair.Value]);
					if (pair.Value == HitboxType.SWEET) {
						target.statusController.QueueAddStatus(new PinEffect(frontVect));
						Debug.Log("Ouch! " + target.unitName + " just got knocked back and took " + damageValues[pair.Value] + " damage!");
					}
					else {
						Debug.Log("Ouch! " + target.unitName + " just took " + damageValues[pair.Value] + " damage!");
					}
					// Hit one target only
					break;
				}
			}
		}
		return true;
	}
}
